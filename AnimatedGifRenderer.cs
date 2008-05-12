using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using BrightIdeasSoftware;

namespace ObjectListViewDemo
{
    /// <summary>
    /// This renderer displays an animated gif. 
    /// The aspect for this column must be a string that contains path to a file that contains an animated gif.
    /// </summary>
    public class AnimatedGifRenderer : ImageRenderer
    {
        const int PropertyTagTypeShort = 3;
        const int PropertyTagTypeLong = 4;
        const int PropertyTagFrameDelay = 0x5100;
        const int PropertyTagLoopCount = 0x5101;
        
        private class AnimationState
        {
            /// <summary>
            /// Create an AnimationState in a quiet state
            /// </summary>
            public AnimationState()
            {
                this.currentFrame = 0;
                this.frameCount = 1;
                this.imageDuration = new List<int>();
                this.image = null;
                this.fileName = String.Empty;
            }

            /// <summary>
            /// Create an animation state for the given image, which may or may not
            /// be an animation
            /// </summary>
            /// <param name="image">The image to be rendered</param>
            public AnimationState(Image image) : this()
            {
                this.InitFromImage(image);
            }

            /// <summary>
            /// Create an animation state by loading an image from a file
            /// </summary>
            /// <param name="fileName">File containing the image</param>
            public AnimationState(string fileName) : this()
            {
                this.fileName = fileName;
                try {
                    this.InitFromImage(Image.FromFile(fileName));
                } catch (FileNotFoundException) {
                } catch (OutOfMemoryException) {
                }
            }

            private void InitFromImage(Image image) {
                this.image = image;
                if (this.image == null)
                    return;
                
                // Does the image contain an animation?
                if (!(new List<Guid>(this.image.FrameDimensionsList)).Contains(FrameDimension.Time.Guid))
                    return;

                this.frameCount = this.image.GetFrameCount(FrameDimension.Time);

                // Find the delay between each frame. The delays are stored an array of
                // 4-byte ints. Each int is the number of 1/100th of a second that should elapsed
                // before the frame expires
                foreach (PropertyItem pi in this.image.PropertyItems) {
                    if (pi.Id == PropertyTagFrameDelay) {
                        for (int i = 0; i < pi.Len; i += 4) {
                            //TODO: There must be a better way to convert 4-bytes to an int
                            int delay = (pi.Value[i + 3] << 24) + (pi.Value[i + 2] << 16) + (pi.Value[i + 1] << 8) + pi.Value[i];
                            this.imageDuration.Add(delay * 10); // store delays as milliseconds
                        }
                        break;
                    }
                }

                // There should be as many frame durations as frames
                Debug.Assert(this.imageDuration.Count == this.frameCount);
            }

            public bool IsValid {
                get {
                    return this.image != null;
                }
            }

            public bool IsAnimation {
                get {
                    return this.frameCount > 1;
                }
            }
            public string fileName;
            public int currentFrame;
            public long currentFrameExpiresAt;
            public Image image;
            public List<int> imageDuration;
            public int frameCount;
        }

        /// <summary>
        /// Create a renderer that will draw animated gifs into a column.
        /// </summary>
        /// <remarks>The one renderer draws all the animated gifs in the column. The state required to draw the gifs
        /// is stored in the Tag of the associate subitem.</remarks>
        public AnimatedGifRenderer()
        {
            this.isPaused = false;
            this.tickler = new System.Threading.Timer(new TimerCallback(this.OnTimer), null, 1000, Timeout.Infinite);
            this.stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Should the animations in this renderer be paused?
        /// </summary>
        public bool Paused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }
	
        delegate void OnTimerCallback(Object state);

        /// <summary>
        /// This is the method that is invoked by the timer. It basically switches control to the listview thread.
        /// </summary>
        /// <param name="state">not used</param>
        public void OnTimer(Object state)
        {
            if (this.ListView == null || this.Paused) {
                this.tickler.Change(1000, Timeout.Infinite);
            } else {
                if (this.ListView.InvokeRequired) {
                    this.ListView.Invoke(new OnTimerCallback(this.OnTimer), new object[] { state });
                } else {
                    this.OnTimerInThread();
                }
            }
        }

        /// <summary>
        /// This is the OnTimer callback, but invoked in the same thread as the creator of the ListView.
        /// This method can use all of ListViews methods without creating a CrossThread exception.
        /// </summary>
        public void OnTimerInThread()
        {
            // If this listview has been destroyed, we can't do anything
            if (this.ListView.IsDisposed) 
                return;
            
            // If we're not in Detail view, we can't do anything at the moment, but we still reschedule
            // the tickler because the view may change later.
            if (this.ListView.View != System.Windows.Forms.View.Details) {
                this.tickler.Change(1000, Timeout.Infinite);
                return;
            }

            long elapsedMilliseconds = this.stopwatch.ElapsedMilliseconds;
            int subItemIndex = this.Column.Index;
            long nextCheckAt = elapsedMilliseconds + 1000; // wait at most one second before checking again
            Rectangle updateRect = new Rectangle(); // what part of the view must be updated to draw the changed gifs?

            // Run through all the subitems in the view for our column, and for each one that 
            // has a gif attached to it, see if the frame needs updating.
            foreach (ListViewItem lvi in this.ListView.Items) {
                // Get the gif state from the subitem. If there isn't a gif state, skip this row.
                ListViewItem.ListViewSubItem lvsi = lvi.SubItems[subItemIndex];
                AnimationState state = lvsi.Tag as AnimationState;
                if (state == null || !state.IsValid || !state.IsAnimation) 
                    continue;

                // Has this frame of the animation expired?
                if (elapsedMilliseconds >= state.currentFrameExpiresAt) {
                    state.currentFrame = (state.currentFrame + 1) % state.frameCount;
                    state.currentFrameExpiresAt = elapsedMilliseconds + state.imageDuration[state.currentFrame];
                    state.image.SelectActiveFrame(FrameDimension.Time, state.currentFrame);

                    // Track the area of the view that needs to be redrawn to show the changed images
                    if (updateRect.IsEmpty)
                        updateRect = lvsi.Bounds;
                    else
                        updateRect = Rectangle.Union(updateRect, lvsi.Bounds);
                }

                // Remember the minimum time at which a frame is next due to change
                nextCheckAt = Math.Min(nextCheckAt, state.currentFrameExpiresAt);
            }
            if (!updateRect.IsEmpty)
                this.ListView.Invalidate(updateRect);
            this.tickler.Change(nextCheckAt - elapsedMilliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Calculate the image that should be drawn. 
        /// If the image is an animation, we setup an object to track its state.
        /// </summary>
        /// <returns>Return the image that should be displayed</returns>
        override protected Image GetImageFromAspect()
        {
            Image image = base.GetImageFromAspect();
            if (image == null)
                return null;

            // If the tag is set but set to something we don't expect, we don't setup a state
            if (this.OLVSubItem.Tag != null && !(this.OLVSubItem.Tag is AnimationState))
                return image;

            // If we don't have a gif state, make one. If we do, check that it's for the right file
            AnimationState state = null;
            if (this.OLVSubItem.Tag == null)
                this.OLVSubItem.Tag = state = new AnimationState(image);
            else {
                state = (AnimationState)this.OLVSubItem.Tag;
            }

            // If we couldn't decode the file, don't show anything
            if (!state.IsValid)
                return null;

            // After all that, finally return the image we want to show
            return state.image;
        }

        private System.Threading.Timer tickler;
        private Stopwatch stopwatch;
        private bool isPaused;
    }
}
