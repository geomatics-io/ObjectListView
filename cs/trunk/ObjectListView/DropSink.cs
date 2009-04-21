/*
 * DropSink.cs - Add drop sink ability to an ObjectListView
 *
 * Author: Phillip Piper
 * Date: 2009-03-17 5:15 PM
 *
 * Change log:
 * 2009-04-15   JPP  - Separated DragDrop.cs into DropSink.cs
 * 2009-03-17   JPP  - Initial version
 * 
 * Copyright (C) 2009 Phillip Piper
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * If you wish to use this code in a closed source application, please contact phillip_piper@bigfoot.com.
 */

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Objects that implement this interface can acts as the receiver for drop
    /// operation for an ObjectListView.
    /// </summary>
    public interface IDropSink
    {
        /// <summary>
        /// Gets or sets the ObjectListView that is the drop sink
        /// </summary>
        ObjectListView ListView { get; set; }

        /// <summary>
        /// Draw any feedback that is appropriate to the current drop state.
        /// </summary>
        /// <remarks>
        /// Any drawing is done over the top of the ListView. This operation should disturb
        /// the Graphic as little as possible. Specifically, do not erase the area into which
        /// you draw. 
        /// </remarks>
        /// <param name="g">A Graphic for drawing</param>
        /// <param name="bounds">The contents bounds of the ListView (not including any header)</param>
        void DrawFeedback(Graphics g, Rectangle bounds);

        /// <summary>
        /// The user has released the drop over this control
        /// </summary>
        /// <remarks>
        /// Implementators should set args.Effect to the appropriate DragDropEffects. This value is returned
        /// to the originator of the drag.
        /// </remarks>
        /// <param name="args"></param>
        void Drop(DragEventArgs args);

        /// <summary>
        /// A drag has entered this control.
        /// </summary>
        /// <remarks>Implementators should set args.Effect to the appropriate DragDropEffects.</remarks>
        /// <param name="args"></param>
        void Enter(DragEventArgs args);

        /// <summary>
        /// Change the cursor to reflect the current drag operation.
        /// </summary>
        /// <param name="args"></param>
        void GiveFeedback(GiveFeedbackEventArgs args);

        /// <summary>
        /// The drag has left the bounds of this control
        /// </summary>
        void Leave();

        /// <summary>
        /// The drag is moving over this control.
        /// </summary>
        /// <remarks>This is where any drop target should be calculated.
        /// Implementators should set args.Effect to the appropriate DragDropEffects.
        /// </remarks>
        /// <param name="args"></param>
        void Over(DragEventArgs args);

        /// <summary>
        /// Should the drag be allowed to continue?
        /// </summary>
        /// <param name="args"></param>
        void QueryContinue(QueryContinueDragEventArgs args);
    }

    /// <summary>
    /// This is a do-nothing implementation of IDropSink that is a useful
    /// base class for more sophisicated implementations.
    /// </summary>
    public class AbstractDropSink : IDropSink
    {
        #region IDropSink Members

        public virtual ObjectListView ListView {
            get { return listView; }
            set { this.listView = value; }
        }
        private ObjectListView listView;

        public virtual void DrawFeedback(Graphics g, Rectangle bounds) {
        }

        public virtual void Enter(DragEventArgs args) {
        }

        public virtual void Leave() {
            this.Cleanup();
        }

        public virtual void Over(DragEventArgs args) {
        }

        public virtual void Drop(DragEventArgs args) {
            this.Cleanup();
        }

        /// <summary>
        /// Change the cursor to reflect the current drag operation.
        /// </summary>
        /// <remarks>You only need to override this if you want non-standard cursors.
        /// The standard cursors are supplied automatically.</remarks>
        /// <param name="args"></param>
        public virtual void GiveFeedback(GiveFeedbackEventArgs args) {
            args.UseDefaultCursors = true;
        }

        /// <summary>
        /// Should the drag be allowed to continue?
        /// </summary>
        /// <remarks>
        /// You only need to override this if you want the user to be able
        /// to end the drop in some non-standard way, e.g. dragging to a
        /// certain point even without releasing the mouse, or going outside
        /// the bounds of the application. 
        /// </remarks>
        /// <param name="args"></param>
        public virtual void QueryContinue(QueryContinueDragEventArgs args) {
        }


        #endregion

        #region Commands

        /// <summary>
        /// This is called when the mouse leaves the drop region and after the
        /// drop has completed.
        /// </summary>
        protected virtual void Cleanup() {
        }

        #endregion
    }

    /// <summary>
    /// The enum indicates which target has been found for a drop operation
    /// </summary>
    [Flags]
    public enum DropTargetLocation
    {
        /// <summary>
        /// No applicable target has been found
        /// </summary>
        None = 0,

        /// <summary>
        /// The list itself is the target of the drop
        /// </summary>
        Background = 0x01,

        /// <summary>
        /// An item is the target
        /// </summary>
        Item = 0x02,

        /// <summary>
        /// Between two items (or above the top item or below the bottom item)
        /// can be the target. This is not actually ever a target, only a value indicate
        /// that it is valid to drop between items
        /// </summary>
        BetweenItems = 0x04,

        /// <summary>
        /// Above an item is the target
        /// </summary>
        AboveItem = 0x08,

        /// <summary>
        /// Below an item is the target
        /// </summary>
        BelowItem = 0x10,

        /// <summary>
        /// A subitem is the target of the drop
        /// </summary>
        SubItem = 0x20,

        /// <summary>
        /// On the right of an item is the target (not currently used)
        /// </summary>
        RightOfItem = 0x40,

        /// <summary>
        /// On the left of an item is the target (not currently used)
        /// </summary>
        LeftOfItem = 0x80
    }

    /// <summary>
    /// This class represents a simple implementation of a drop sink.
    /// </summary>
    /// <remarks>
    /// Actually, it's far from simple and can do quite a lot in its own right.
    /// </remarks>
    public class SimpleDropSink : AbstractDropSink
    {
        #region Life and death

        public SimpleDropSink() {
            this.timer = new Timer();
            this.timer.Interval = 250;
            this.timer.Tick += new EventHandler(this.timer_Tick);

            this.CanDropOnItem = true;
            //this.CanDropOnSubItem = true;
            //this.CanDropOnBackground = true;
            //this.CanDropBetween = true;

            this.FeedbackColor = Color.FromArgb(180, Color.MediumBlue);
            this.billboard = new BillboardOverylay();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Get or set the locations where a drop is allowed to occur
        /// </summary>
        public DropTargetLocation AcceptableLocations {
            get { return this.acceptableLocations; }
            set { this.acceptableLocations = value; }
        }
        private DropTargetLocation acceptableLocations;

        /// <summary>
        /// Gets or sets whether the ListView should scroll when the user drags
        /// something near to the top or bottom rows.
        /// </summary>
        public bool AutoScroll {
            get { return this.autoScroll; }
            set { this.autoScroll = value; }
        }
        private bool autoScroll = true;

        /// <summary>
        /// Gets the billboard overlay that will be used to display feedback
        /// messages during a drag operation.
        /// </summary>
        public BillboardOverylay Billboard {
            get { return this.billboard; }
        }
        private BillboardOverylay billboard;

        /// <summary>
        /// Get or set whether a drop can occur between items of the list
        /// </summary>
        public bool CanDropBetween {
            get { return (this.AcceptableLocations & DropTargetLocation.BetweenItems) == DropTargetLocation.BetweenItems; }
            set {
                if (value)
                    this.AcceptableLocations |= DropTargetLocation.BetweenItems;
                else 
                    this.AcceptableLocations &= ~DropTargetLocation.BetweenItems;
            }
        }

        /// <summary>
        /// Get or set whether a drop can occur on the listview itself
        /// </summary>
        public bool CanDropOnBackground {
            get { return (this.AcceptableLocations & DropTargetLocation.Background) == DropTargetLocation.Background; }
            set {
                if (value)
                    this.AcceptableLocations |= DropTargetLocation.Background;
                else
                    this.AcceptableLocations &= ~DropTargetLocation.Background;
            }
        }

        /// <summary>
        /// Get or set whether a drop can occur on items in the list
        /// </summary>
        public bool CanDropOnItem {
            get { return (this.AcceptableLocations & DropTargetLocation.Item) == DropTargetLocation.Item; }
            set {
                if (value)
                    this.AcceptableLocations |= DropTargetLocation.Item;
                else
                    this.AcceptableLocations &= ~DropTargetLocation.Item;
            }
        }

        /// <summary>
        /// Get or set whether a drop can occur on a subitem in the list
        /// </summary>
        public bool CanDropOnSubItem {
            get { return (this.AcceptableLocations & DropTargetLocation.SubItem) == DropTargetLocation.SubItem; }
            set {
                if (value)
                    this.AcceptableLocations |= DropTargetLocation.SubItem;
                else
                    this.AcceptableLocations &= ~DropTargetLocation.SubItem;
            }
        }

        /// <summary>
        /// Get or set the index of the item that is the target of the drop
        /// </summary>
        public int DropTargetIndex {
            get { return dropTargetIndex; }
            set {
                if (this.dropTargetIndex != value) {
                    this.dropTargetIndex = value;
                    this.ListView.Invalidate();
                }
            }
        }
        private int dropTargetIndex = -1;

        /// <summary>
        /// Get the item that is the target of the drop
        /// </summary>
        public OLVListItem DropTargetItem {
            get {
                return this.ListView.GetItem(this.DropTargetIndex);
            }
        }

        /// <summary>
        /// Get or set the location of the target of the drop
        /// </summary>
        public DropTargetLocation DropTargetLocation {
            get { return dropTargetLocation; }
            set {
                if (this.dropTargetLocation != value) {
                    this.dropTargetLocation = value;
                    this.ListView.Invalidate();
                }
            }
        }
        private DropTargetLocation dropTargetLocation;

        /// <summary>
        /// Get or set the index of the subitem that is the target of the drop
        /// </summary>
        public int DropTargetSubItemIndex {
            get { return dropTargetSubItemIndex; }
            set {
                if (this.dropTargetSubItemIndex != value) {
                    this.dropTargetSubItemIndex = value;
                    this.ListView.Invalidate();
                }
            }
        }
        private int dropTargetSubItemIndex = -1;

        /// <summary>
        /// Get or set the color that will be used to provide drop feedback
        /// </summary>
        public Color FeedbackColor {
            get { return this.feedbackColor; }
            set { this.feedbackColor = value; }
        }
        private Color feedbackColor;

        /// <summary>
        /// Get whether the alt key was down during this drop event
        /// </summary>
        public bool IsAltDown {
            get { return (this.KeyState & 32) == 32; }
        }

        /// <summary>
        /// Get whether any modifier key was down during this drop event
        /// </summary>
        public bool IsAnyModifierDown {
            get { return (this.KeyState & (4 + 8 + 32)) != 0; }
        }

        /// <summary>
        /// Get whether the control key was down during this drop event
        /// </summary>
        public bool IsControlDown {
            get { return (this.KeyState & 8) == 8; }
        }

        /// <summary>
        /// Get whether the left mouse button was down during this drop event
        /// </summary>
        public bool IsLeftMouseButtonDown {
            get { return (this.KeyState & 1) == 1; }
        }

        /// <summary>
        /// Get whether the right mouse button was down during this drop event
        /// </summary>
        public bool IsMiddleMouseButtonDown {
            get { return (this.KeyState & 16) == 16; }
        }

        /// <summary>
        /// Get whether the right mouse button was down during this drop event
        /// </summary>
        public bool IsRightMouseButtonDown {
            get { return (this.KeyState & 2) == 2; }
        }

        /// <summary>
        /// Get whether the shift key was down during this drop event
        /// </summary>
        public bool IsShiftDown {
            get { return (this.KeyState & 4) == 4; }
        }

        /// <summary>
        /// Get or set the state of the keys during this drop event
        /// </summary>
        public int KeyState {
            get { return this.keyState; }
            set { this.keyState = value; }
        }
        private int keyState;

        #endregion

        #region Events

        public event EventHandler<DropEventArgs> CanDrop;
        public event EventHandler<DropEventArgs> Dropped;

        public event EventHandler<ModelDropEventArgs> ModelCanDrop;
        public event EventHandler<ModelDropEventArgs> ModelDropped;
        
        #endregion

        #region DropSink Interface

        protected override void Cleanup() {
            //System.Diagnostics.Debug.WriteLine("Cleanup");
            this.DropTargetLocation = DropTargetLocation.None;
            this.ListView.FullRowSelect = this.originalFullRowSelect;
            this.ListView.RemoveOverlay(this.billboard);
        }

        /// <summary>
        /// Draw any feedback that is appropriate to the current drop state.
        /// </summary>
        /// <remarks>
        /// Any drawing is done over the top of the ListView. This operation should disturb
        /// the Graphic as little as possible. Specifically, do not erase the area into which
        /// you draw. 
        /// </remarks>
        /// <param name="g">A Graphic for drawing</param>
        /// <param name="bounds">The contents bounds of the ListView (not including any header)</param>
        public override void DrawFeedback(Graphics g, Rectangle bounds) {
            g.SmoothingMode = SmoothingMode.HighQuality;

            switch (this.DropTargetLocation) {
                case DropTargetLocation.Background:
                    this.DrawFeedbackBackgroundTarget(g, bounds);
                    break;
                case DropTargetLocation.Item:
                    this.DrawFeedbackItemTarget(g, bounds);
                    break;
                case DropTargetLocation.AboveItem:
                    this.DrawFeedbackAboveItemTarget(g, bounds);
                    break;
                case DropTargetLocation.BelowItem:
                    this.DrawFeedbackBelowItemTarget(g, bounds);
                    break;
            }
        }

        /// <summary>
        /// The user has released the drop over this control
        /// </summary>
        /// <param name="args"></param>
        public override void Drop(DragEventArgs args) {
            //System.Diagnostics.Debug.WriteLine("Drop");
            this.TriggerDroppedEvent(args);

            this.timer.Stop();
            this.Cleanup();
        }

        /// <summary>
        /// A drag has entered this control.
        /// </summary>
        /// <remarks>Implementators should set args.Effect to the appropriate DragDropEffects.</remarks>
        /// <param name="args"></param>
        public override void Enter(DragEventArgs args) {
            //System.Diagnostics.Debug.WriteLine("Enter");

            /* 
             * When FullRowSelect is true, we have two problems:
             * 1) GetItemRect(ItemOnly) returns the whole row rather than just the icon/text, which messes
             *    up our calculation of the drop rectangle.
             * 2) during the drag, the Timer events will not fire! This is the major problem, since without
             *    those events we can't autoscroll. 
             * 
             * The first problem we can solve through coding, but the second is more difficult. 
             * We avoid both problems by turning off FullRowSelect during the drop operation.
             */    
            this.originalFullRowSelect = this.ListView.FullRowSelect;
            this.ListView.FullRowSelect = false;

            this.Over(args);
        }

        /// <summary>
        /// The drag is moving over this control.
        /// </summary>
        /// <param name="args"></param>
        public override void Over(DragEventArgs args) {
            //System.Diagnostics.Debug.WriteLine("Over");
            this.KeyState = args.KeyState;
            Point pt = this.ListView.PointToClient(new Point(args.X, args.Y));
            this.CalculateDropTarget(pt);
            args.Effect = this.CalculateDropAction(args, pt);
            this.CheckScrolling(pt);
        }

        #endregion
        
        #region Events

        protected virtual void TriggerCanDropEvent(DragEventArgs args, Point pt) {
            OLVDataObject olvData = args.Data as OLVDataObject;
            if (olvData == null) {
                this.dropEventArgs = new ModelDropEventArgs(args.Data, this, pt, null, null, null);
            } else {
                object target = this.ListView.GetModelObject(this.DropTargetIndex);
                this.dropEventArgs = new ModelDropEventArgs(args.Data, this, pt,
                    olvData.ModelObjects, target, olvData.ListView);
                this.OnModelCanDrop(this.dropEventArgs);
            }
            this.OnCanDrop(this.dropEventArgs);
            this.HandleInfoMsg(pt);
        }
        private ModelDropEventArgs dropEventArgs;

        /// <summary>
        /// Show any info msg that the CanDrop event handler may have given
        /// </summary>
        /// <param name="pt">Current mouse location</param>
        protected virtual void HandleInfoMsg(Point pt) {
            if (this.billboard == null)
                return;

            if (String.IsNullOrEmpty(this.dropEventArgs.InfoMessage)) {
                if (this.ListView.HasOverlay(this.billboard))
                    this.ListView.RemoveOverlay(this.billboard);
            } else {
                this.billboard.Text = this.dropEventArgs.InfoMessage;
                pt.Offset(5, 5);
                this.billboard.Location = pt;
                if (this.ListView.HasOverlay(this.billboard))
                    this.ListView.Invalidate();
                else
                    this.ListView.AddOverlay(this.billboard);
            }
        }

        protected virtual void TriggerDroppedEvent(DragEventArgs args) {
            OLVDataObject olvData = args.Data as OLVDataObject;
            if (olvData != null) {
                this.OnModelDropped(this.dropEventArgs);
            }
            this.OnDropped(this.dropEventArgs);
        }

        protected virtual void OnCanDrop(DropEventArgs args) {
            if (this.CanDrop != null)
                this.CanDrop(this, args);
        }

        protected virtual void OnDropped(DropEventArgs args) {
            if (this.Dropped != null)
                this.Dropped(this, args);
        }

        protected virtual void OnModelCanDrop(ModelDropEventArgs args) {
            if (this.ModelCanDrop != null)
                this.ModelCanDrop(this, args);
        }

        protected bool HasModelCanDropHandlers {
            get { return this.ModelCanDrop != null; }
        }

        protected bool HasModelDroppedHandlers {
            get { return this.ModelDropped != null; }
        }

        protected virtual void OnModelDropped(ModelDropEventArgs args) {
            if (this.ModelDropped != null)
                this.ModelDropped(this, args);
        }

        #endregion

        #region Implementation

        private void timer_Tick(object sender, EventArgs e) {
            this.HandleTimerTick();
        }

        /// <summary>
        /// Handle the timer tick event, which is sent when the listview should
        /// scroll
        /// </summary>
        protected virtual void HandleTimerTick() {

            // If the mouse has been released, stop scrolling.
            // This is only necessary if the mouse is released outside of the control. 
            // If the mouse is released inside the control, we would receive a Drop event.
            if ((this.IsLeftMouseButtonDown && (Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left) ||
                (this.IsMiddleMouseButtonDown && (Control.MouseButtons & MouseButtons.Middle) != MouseButtons.Middle) ||
                (this.IsRightMouseButtonDown && (Control.MouseButtons & MouseButtons.Right) != MouseButtons.Right)) {
                this.timer.Stop();
                this.Cleanup();
                return;
            }

            // Auto scrolling will continune while the mouse is close to the ListView
            const int GRACE_PERIMETER = 30;

            Point pt = this.ListView.PointToClient(Cursor.Position);
            Rectangle r2 = this.ListView.ClientRectangle;
            r2.Inflate(GRACE_PERIMETER, GRACE_PERIMETER);
            if (r2.Contains(pt)) {
                this.ListView.LowLevelScroll(0, this.scrollAmount);
            }
        }

        /// <summary>
        /// When the mouse is at the given point, what should the target of the drop be?
        /// </summary>
        /// <remarks>This method should update DropTargetLocation and DropTargetIndex</remarks>
        /// <param name="pt">The mouse point, in client co-ordinates</param>
        protected virtual void CalculateDropTarget(Point pt) {
            const int SMALL_VALUE = 3;
            DropTargetLocation location = DropTargetLocation.None;
            int targetIndex = -1;
            int targetSubIndex = 0;

            if (this.CanDropOnBackground)
                location = DropTargetLocation.Background;

            // Which item is the mouse over?
            // If it is not over any item, it's over the background.
            ListViewHitTestInfo info = this.ListView.HitTest(pt.X, pt.Y);
            if (info.Item != null && this.CanDropOnItem) {
                location = DropTargetLocation.Item;
                targetIndex = info.Item.Index;
                if (info.SubItem != null && this.CanDropOnSubItem)
                    targetSubIndex = info.Item.SubItems.IndexOf(info.SubItem);
            }

            // Check to see if the mouse is "between" rows.
            // ("between" is somewhat loosely defined)
            if (this.CanDropBetween && this.ListView.GetItemCount() > 0) {

                // If the mouse is over an item, check to see if it is near the top or bottom
                if (location == DropTargetLocation.Item) {
                    if (pt.Y - SMALL_VALUE <= info.Item.Bounds.Top)
                        location = DropTargetLocation.AboveItem;
                    if (pt.Y + SMALL_VALUE >= info.Item.Bounds.Bottom)
                        location = DropTargetLocation.BelowItem;
                } else {
                    // Is there an item a little below the mouse?
                    // If so, we say the drop point is above that row
                    info = this.ListView.HitTest(pt.X, pt.Y + SMALL_VALUE);
                    if (info.Item != null) {
                        targetIndex = info.Item.Index;
                        location = DropTargetLocation.AboveItem;
                    } else {
                        // Is there an item a little above the mouse?
                        info = this.ListView.HitTest(pt.X, pt.Y - SMALL_VALUE);
                        if (info.Item != null) {
                            targetIndex = info.Item.Index;
                            location = DropTargetLocation.BelowItem;
                        }
                    }
                }
            }

            this.DropTargetLocation = location;
            this.DropTargetIndex = targetIndex;
            this.DropTargetSubItemIndex = targetSubIndex;
        }

        /// <summary>
        /// Can the given drag object be released?
        /// </summary>
        /// <remarks>
        /// <para>
        /// Subclasses should examine DropTargetLocation and DropTargetIndex to see if the given
        /// data object can be dropped
        /// </para>
        /// </remarks>
        /// <param name="args"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public virtual DragDropEffects CalculateDropAction(DragEventArgs args, Point pt) {
            this.TriggerCanDropEvent(args, pt);
            return this.dropEventArgs.Effect;
        }

        /// <summary>
        /// Based solely on the state of the modifier keys, what drop operation should
        /// be used?
        /// </summary>
        /// <returns>The drop operation that matches the state of the keys</returns>
        public DragDropEffects CalculateStandardDropActionFromKeys() {
            if (this.IsControlDown) {
                if (this.IsShiftDown)
                    return DragDropEffects.Link;
                else
                    return DragDropEffects.Copy;
            } else {
                return DragDropEffects.Move;
            }
        }

        /// <summary>
        /// Should the listview be made to scroll when the mouse is at the given point?
        /// </summary>
        /// <param name="pt"></param>
        protected virtual void CheckScrolling(Point pt) {
            if (!this.AutoScroll)
                return;

            Rectangle r = this.ListView.ContentRectangle;
            int rowHeight = this.ListView.RowHeightEffective;
            int close = rowHeight;
            if (pt.Y <= (r.Top + close)) {
                // Scroll faster if the mouse is closer to the top
                this.timer.Interval = ((pt.Y <= (r.Top + close / 2)) ? 100 : 350);
                this.timer.Start();
                this.scrollAmount = -rowHeight;
            } else {
                if (pt.Y >= (r.Bottom - close)) {
                    this.timer.Interval = ((pt.Y >= (r.Bottom - close / 2)) ? 100 : 350);
                    this.timer.Start();
                    this.scrollAmount = rowHeight;
                } else {
                    this.timer.Stop();
                }
            }
        }

        #endregion

        #region Rendering

        protected virtual void DrawFeedbackBackgroundTarget(Graphics g, Rectangle bounds) {
            Rectangle r = bounds;
            using (Pen p = new Pen(this.FeedbackColor, 15.0f)) {
                g.DrawRectangle(p, r);
            }
        }

        protected virtual void DrawFeedbackItemTarget(Graphics g, Rectangle bounds) {
            Rectangle r = this.CalculateDropTargetRectangle(this.DropTargetItem, this.DropTargetSubItemIndex);
            r.Inflate(1, 1);
            float diameter = r.Height / 3;
            using (GraphicsPath path = this.GetRoundedRect(r, diameter)) {
                using (SolidBrush b = new SolidBrush(Color.FromArgb(48, this.FeedbackColor))) {
                    g.FillPath(b, path);
                }
                using (Pen p = new Pen(this.FeedbackColor, 3.0f)) {
                    g.DrawPath(p, path);
                }
            }
        }

        protected virtual void DrawFeedbackAboveItemTarget(Graphics g, Rectangle bounds) {
            Rectangle r = this.CalculateDropTargetRectangle(this.DropTargetItem, this.DropTargetSubItemIndex);
            this.DrawBetweenLine(g, r.Left, r.Top, r.Right, r.Top);
        }

        protected virtual void DrawFeedbackBelowItemTarget(Graphics g, Rectangle bounds) {
            Rectangle r = this.CalculateDropTargetRectangle(this.DropTargetItem, this.DropTargetSubItemIndex);
            this.DrawBetweenLine(g, r.Left, r.Bottom, r.Right, r.Bottom);
        }

        /// <summary>
        /// Return a GraphicPath that is round corner rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="diameter"></param>
        /// <returns></returns>
        protected GraphicsPath GetRoundedRect(Rectangle rect, float diameter) {
            GraphicsPath path = new GraphicsPath();

            RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected Rectangle CalculateDropTargetRectangle(OLVListItem item, int subItem) {
            if (subItem > 0)
                return item.SubItems[subItem].Bounds;
            
            Rectangle r = this.ListView.CalculateTightCellBounds(item, subItem);

            // Allow for indent
            if (item.IndentCount > 0) {
                int indentWidth = (this.ListView.SmallImageList == null) ? 16 : this.ListView.SmallImageList.ImageSize.Width;
                r.X += (indentWidth * item.IndentCount);
                r.Width -= (indentWidth * item.IndentCount);
            }

            return r;
        }

        protected virtual void DrawBetweenLine(Graphics g, int x1, int y1, int x2, int y2) {
            using (Pen p = new Pen(this.FeedbackColor, 3.0f)) {
                int x = x1;
                int y = y1;
                using (GraphicsPath gp = new GraphicsPath()) {
                    gp.AddLine(
                        x, y + 5,
                        x, y - 5);
                    gp.AddBezier(
                        x, y - 6,
                        x + 3, y - 2,
                        x + 6, y - 1,
                        x + 11, y);
                    gp.AddBezier(
                        x + 11, y,
                        x + 6, y + 1,
                        x + 3, y + 2,
                        x, y + 6);
                    gp.CloseFigure();
                    g.FillPath(new SolidBrush(p.Color), gp);
                }
                x = x2;
                y = y2;
                using (GraphicsPath gp = new GraphicsPath()) {
                    gp.AddLine(
                        x, y + 6,
                        x, y - 6);
                    gp.AddBezier(
                        x, y - 7,
                        x - 3, y - 2,
                        x - 6, y - 1,
                        x - 11, y);
                    gp.AddBezier(
                        x - 11, y,
                        x - 6, y + 1,
                        x - 3, y + 2,
                        x, y + 7);
                    gp.CloseFigure();
                    g.FillPath(new SolidBrush(p.Color), gp);
                }
                g.DrawLine(p, x1, y1, x2, y2);
            }
        }

        #endregion

        private Timer timer;
        private int scrollAmount;
        private bool originalFullRowSelect;
    }

    /// <summary>
    /// This drop sink allows items within the same list to be rearranged,
    /// as well as allowing items to be dropped from other lists.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class can only be used on plain ObjectListViews and FastObjectListViews.
    /// </para>
    /// <para>
    /// Users of this class should listen for the CanDrop event to decide
    /// if models from another OLV can be moved to OLV under this sink.
    /// </para>
    /// </remarks>
    public class RearrangingDropSink : SimpleDropSink
    {
        public RearrangingDropSink() {
            this.CanDropBetween = true;
            this.CanDropOnBackground = true;
            this.CanDropOnItem = false;
        }

        public RearrangingDropSink(bool acceptDropsFromOtherLists)
            : this() {
            this.AcceptExternal = acceptDropsFromOtherLists;
        }

        public bool AcceptExternal {
            get { return this.acceptExternal; }
            set { this.acceptExternal = value; }
        }
        private bool acceptExternal = false;

        protected override void OnModelCanDrop(ModelDropEventArgs args) {
            if (this.HasModelCanDropHandlers) {
                base.OnModelCanDrop(args);
            } else {
                if (!this.AcceptExternal && args.SourceListView != this.ListView) {
                    args.Effect = DragDropEffects.None;
                    args.InfoMessage = "This list doesn't accept drops from other lists";
                } else {
                    if (args.DropTargetLocation == DropTargetLocation.Background &&
                        args.SourceListView == this.ListView) {
                        args.Effect = DragDropEffects.None;
                    } else {
                        args.Effect = DragDropEffects.Move;
                    }
                }
            }
        }

        protected override void OnModelDropped(ModelDropEventArgs args) {
            // If an event handler has been set up, use that.
            if (this.HasModelDroppedHandlers) {
                base.OnModelDropped(args);
            } else {
                this.RearrangeModels(args);
            }
        }

        public virtual void RearrangeModels(ModelDropEventArgs args) {
            switch (this.DropTargetLocation) {
                case DropTargetLocation.AboveItem:
                    this.ListView.MoveObjects(args.DropTargetIndex, args.DragModels);
                    break;
                case DropTargetLocation.BelowItem:
                    this.ListView.MoveObjects(args.DropTargetIndex + 1, args.DragModels);
                    break;
                case DropTargetLocation.Background:
                    this.ListView.AddObjects(args.DragModels);
                    break;
                default:
                    return;
            }

            if (args.SourceListView != this.ListView) {
                args.SourceListView.RemoveObjects(args.DragModels);
            }
        }
    }

    /// <summary>
    /// When a drop sink needs to know if something can be dropped, or
    /// to notify that a drop has occured, it uses an instance of this class.
    /// </summary>
    public class DropEventArgs : EventArgs
    {
        public DropEventArgs(object dataObject, SimpleDropSink dropSink, Point location) {
            this.dataObject = dataObject;
            this.dropSink = dropSink;
            this.location = location;
        }

        #region Data Properties

        /// <summary>
        /// Get the data object that is being dragged
        /// </summary>
        public object DataObject {
            get { return this.dataObject; }
        }
        private object dataObject;

        /// <summary>
        /// Get the drop sink that originated this event
        /// </summary>
        public SimpleDropSink DropSink {
            get { return this.dropSink; }
        }
        private SimpleDropSink dropSink;

        /// <summary>
        /// Get or set the drag effect that should be used for this operation
        /// </summary>
        public DragDropEffects Effect {
            get { return this.effect; }
            set { this.effect = value; }
        }
        private DragDropEffects effect;

        /// <summary>
        /// Get the location of the mouse (in target ListView co-ords)
        /// </summary>
        public Point Location {
            get { return this.location; }
            internal set { this.location = value; }
        }
        private Point location;

        /// <summary>
        /// Get or set the feedback message for this operation
        /// </summary>
        /// <remarks>
        /// If this is not null, it will be displayed as a feedback message
        /// during the drag.
        /// </remarks>
        public string InfoMessage {
            get { return this.infoMessage; }
            set { this.infoMessage = value; }
        }
        private string infoMessage;

        #endregion

        #region Convenience Properties

        /// <summary>
        /// Get the ObjectListView that is being dropped on
        /// </summary>
        public ObjectListView ListView {
            get { return this.dropSink.ListView; }
        }

        /// <summary>
        /// Get the index of the item that is the target of the drop
        /// </summary>
        public int DropTargetIndex {
            get { return this.dropSink.DropTargetIndex; }
        }

        /// <summary>
        /// Get the item that is the target of the drop
        /// </summary>
        public OLVListItem DropTargetItem {
            get {
                return this.ListView.GetItem(this.DropTargetIndex);
            }
        }

        /// <summary>
        /// Get the location of the target of the drop
        /// </summary>
        public DropTargetLocation DropTargetLocation {
            get { return this.dropSink.DropTargetLocation; }
        }

        /// <summary>
        /// Get the index of the subitem that is the target of the drop
        /// </summary>
        public int DropTargetSubItemIndex {
            get { return this.dropSink.DropTargetSubItemIndex; }
        }

        #endregion
    }

    /// <summary>
    /// These events are triggered when the drag source is an ObjectListView.
    /// </summary>
    public class ModelDropEventArgs : DropEventArgs
    {
        public ModelDropEventArgs(object dataObject, SimpleDropSink dropSink, Point location,
            IList dragObjects, object targetModel, ObjectListView sourceListView)
            : base(dataObject, dropSink, location)
        {
            this.dragModels = dragObjects;
            this.targetModel = targetModel;
            this.sourceListView = sourceListView;
        }

        /// <summary>
        /// Gets the model objects that are being dragged.
        /// </summary>
        public IList DragModels {
            get { return this.dragModels; }
        }
        private IList dragModels;

        /// <summary>
        /// Gets the ObjectListView that is the source of the dragged objects.
        /// </summary>
        public ObjectListView SourceListView {
            get { return this.sourceListView; }
        }
        private ObjectListView sourceListView;

        /// <summary>
        /// Get the model object that is being dropped upon.
        /// </summary>
        /// <remarks>This is only value for TargetLocation == Item</remarks>
        public object TargetModel {
            get { return this.targetModel; }
        }
        private object targetModel;
    }
}
