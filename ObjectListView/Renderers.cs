/*
 * Renderers - A collection of useful renderers that are used to owner drawn a column in an ObjectListView
 *
 * Author: Phillip Piper
 * Date: 27/09/2008 9:15 AM
 *
 * Change log:
 * 2008-12-29   JPP  - Render text correctly when HideSelection is true.
 * 2008-12-26   JPP  - BaseRenderer now works correctly in all Views
 * 2008-12-23   JPP  - Fixed two small bugs in BarRenderer
 * v2.0
 * 2008-10-26   JPP  - Don't owner draw when in Design mode
 * 2008-09-27   JPP  - Separated from ObjectListView.cs
 * 
 * Copyright (C) 2006-2008 Phillip Piper
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Renderers are responsible for drawing a single cell within an owner drawn ObjectListView.
    /// </summary>
    /// <remarks>
    /// <para>Methods on this class are called during the DrawItem or DrawSubItemEvent.
    /// Subclasses can tell which type of event they are handling by examining DrawItemEvent: if this
    /// is not null, it is a DrawItem event.</para>
    /// <para>Subclasses will normally override the RenderWithDefault or Render method, and use the other
    /// methods as helper functions.</para>
    /// <para>If a renderer is installed on the primary column (column 0), it will be given a chance
    /// to draw the whole item in all views (Details, Tile, etc.). If the renderer returns true,
    /// default processing will continue. If it returns false, no other rendering will happen.</para>
    /// <para>This means that when an ObjectListView is in Details view, the renderer on column 0
    /// will be called twice: once to handle the DrawItem event, and then again to draw only the
    /// first cell. Subclasses must distinguish between these two very different events (using
    /// the "this.DrawItemEvent == null" test).</para>
    /// </remarks>
    [Browsable(false)]
    public class BaseRenderer
    {
        /// <summary>
        /// Make a simple renderer
        /// </summary>
        public BaseRenderer()
        {
        }

        #region Properties

        /// <summary>
        /// Get/set the event that caused this renderer to be called
        /// </summary>
        public DrawListViewSubItemEventArgs Event
        {
            get { return eventArgs; }
            set { eventArgs = value; }
        }
        private DrawListViewSubItemEventArgs eventArgs;

        /// <summary>
        /// Get/set the event that caused this renderer to be called
        /// </summary>
        public DrawListViewItemEventArgs DrawItemEvent
        {
            get { return drawItemEventArgs; }
            set { drawItemEventArgs = value; }
        }
        private DrawListViewItemEventArgs drawItemEventArgs;

        /// <summary>
        /// Get/set the listview for which the drawing is to be done
        /// </summary>
        public ObjectListView ListView
        {
            get { return objectListView; }
            set { objectListView = value; }
        }
        private ObjectListView objectListView;

        /// <summary>
        /// Get or set the OLVColumn that this renderer will draw
        /// </summary>
        public OLVColumn Column
        {
            get { return column; }
            set { column = value; }
        }
        private OLVColumn column;

        /// <summary>
        /// Get or set the model object that this renderer should draw
        /// </summary>
        public Object RowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }
        private Object rowObject;

        /// <summary>
        /// Get or set the aspect of the model object that this renderer should draw
        /// </summary>
        public Object Aspect
        {
            get {
                if (aspect == null)
                    aspect = column.GetValue(this.rowObject);
                return aspect;
            }
            set { aspect = value; }
        }
        private Object aspect;

        /// <summary>
        /// Get or set the listitem that this renderer will be drawing
        /// </summary>
        public OLVListItem ListItem
        {
            get { return listItem; }
            set { listItem = value; }
        }
        private OLVListItem listItem;

        /// <summary>
        /// Get or set the list subitem that this renderer will be drawing
        /// </summary>
        public ListViewItem.ListViewSubItem SubItem
        {
            get { return listSubItem; }
            set { listSubItem = value; }
        }
        private ListViewItem.ListViewSubItem listSubItem;

        /// <summary>
        /// Get the specialized OLVSubItem that this renderer is drawing
        /// </summary>
        /// <remarks>This returns null for column 0.</remarks>
        public OLVListSubItem OLVSubItem
        {
            get { return listSubItem as OLVListSubItem; }
        }

        /// <summary>
        /// Cache whether or not our item is selected
        /// </summary>
        public bool IsItemSelected
        {
            get { return isItemSelected; }
            set { isItemSelected = value; }
        }
        private bool isItemSelected;


        /// <summary>
        /// Return the font to be used for text in this cell
        /// </summary>
        /// <returns>The font of the subitem</returns>
        public Font Font
        {
            get {
                if (this.font == null) {
                    if (this.SubItem == null || this.ListItem.UseItemStyleForSubItems)
                        return this.ListItem.Font;
                    else
                        return this.SubItem.Font;
                } else
                    return this.font;
            }
            set {
                this.font = value;
            }
        }
        private Font font;

        /// <summary>
        /// The brush that will be used to paint the text
        /// </summary>
        public Brush TextBrush
        {
            get {
                if (textBrush == null)
                    return new SolidBrush(this.GetForegroundColor());
                else
                    return this.textBrush;
            }
            set { textBrush = value; }
        }
        private Brush textBrush;

        /// <summary>
        /// Should this renderer fill in the background before drawing?
        /// </summary>
        public bool IsDrawBackground
        {
            get { return isDrawBackground; }
            set { isDrawBackground = value; }
        }
        private bool isDrawBackground = true;

        /// <summary>
        /// Can the renderer wrap lines that do not fit completely within the cell?
        /// </summary>
        public bool CanWrap
        {
            get { return canWrap; }
            set { canWrap = value; }
        }
        private bool canWrap = false;

        /// <summary>
        /// When rendering multiple images, how many pixels should be between each image?
        /// </summary>
        public int Spacing
        {
            get { return spacing; }
            set { spacing = value; }
        }
        private int spacing = 1;

        #endregion

        #region Utilities

        /// <summary>
        /// Return the string that should be drawn within this
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            if (this.SubItem == null)
                return this.ListItem.Text;
            else
                return this.SubItem.Text;
        }

        /// <summary>
        /// Return the image that should be drawn against this subitem
        /// </summary>
        /// <returns>An Image or null if no image should be drawn.</returns>
        public Image GetImage()
        {
            if (this.Column.Index == 0)
                return this.GetImage(this.ListItem.ImageSelector);
            else
                return this.GetImage(this.OLVSubItem.ImageSelector);
        }

        /// <summary>
        /// Return the actual image that should be drawn when keyed by the given image selector.
        /// An image selector can be: <list>
        /// <item>an int, giving the index into the image list</item>
        /// <item>a string, giving the image key into the image list</item>
        /// <item>an Image, being the image itself</item>
        /// </list>
        /// </summary>
        /// <param name="imageSelector">The value that indicates the image to be used</param>
        /// <returns>An Image or null</returns>
        public Image GetImage(Object imageSelector)
        {
            if (imageSelector == null || imageSelector == System.DBNull.Value)
                return null;

            ImageList il = this.ListView.BaseSmallImageList;
            if (il != null) {
                if (imageSelector is Int32) {
                    Int32 index = (Int32)imageSelector;
                    if (index < 0 || index >= il.Images.Count)
                        return null;
                    else
                        return il.Images[index];
                }

                String str = imageSelector as String;
                if (str != null) {
                    if (il.Images.ContainsKey(str))
                        return il.Images[str];
                    else
                        return null;
                }
            }

            return imageSelector as Image;
        }

        /// <summary>
        /// Return the Color that is the background color for this item's cell
        /// </summary>
        /// <returns>The background color of the subitem</returns>
        public Color GetBackgroundColor()
        {
            if (this.IsItemSelected && this.ListView.FullRowSelect) {
                if (this.ListView.Focused)
                    return this.ListView.HighlightBackgroundColorOrDefault;
                else
                    if (!this.ListView.HideSelection)
                        return SystemColors.Control; //TODO: What color should this be?
            }
            if (this.SubItem == null || this.ListItem.UseItemStyleForSubItems)
                return this.ListItem.BackColor;
            else
                return this.SubItem.BackColor;
        }

        /// <summary>
        /// Return the Color that is the background color for this item's text
        /// </summary>
        /// <returns>The background color of the subitem's text</returns>
        protected Color GetTextBackgroundColor()
        {
            if (this.IsItemSelected && (this.Column.Index == 0 || this.ListView.FullRowSelect))
                return this.ListView.HighlightBackgroundColorOrDefault;
            else
                if (this.SubItem == null || this.ListItem.UseItemStyleForSubItems)
                    return this.ListItem.BackColor;
                else
                    return this.SubItem.BackColor;
        }

        /// <summary>
        /// Return the color to be used for text in this cell
        /// </summary>
        /// <returns>The text color of the subitem</returns>
        protected Color GetForegroundColor()
        {
            if (this.IsItemSelected && (this.Column.Index == 0 || this.ListView.FullRowSelect)) {
                if (this.ListView.Focused)
                    return this.ListView.HighlightForegroundColorOrDefault;
                else
                    if (!this.ListView.HideSelection)
                        return SystemColors.ControlText; //TODO: What color should this be?
            } 
            if (this.SubItem == null || this.ListItem.UseItemStyleForSubItems)
                return this.ListItem.ForeColor;
            else
                return this.SubItem.ForeColor;
        }


        /// <summary>
        /// Align the second rectangle with the first rectangle,
        /// according to the alignment of the column
        /// </summary>
        /// <param name="outer">The cell's bounds</param>
        /// <param name="inner">The rectangle to be aligned within the bounds</param>
        /// <returns>An aligned rectangle</returns>
        protected Rectangle AlignRectangle(Rectangle outer, Rectangle inner)
        {
            Rectangle r = new Rectangle(outer.Location, inner.Size);

            // Centre horizontally depending on the column alignment
            if (inner.Width < outer.Width) {
                switch (this.Column.TextAlign) {
                    case HorizontalAlignment.Left:
                        r.X = outer.Left;
                        break;
                    case HorizontalAlignment.Center:
                        r.X = outer.Left + ((outer.Width - inner.Width) / 2);
                        break;
                    case HorizontalAlignment.Right:
                        r.X = outer.Right - inner.Width - 1;
                        break;
                }
            }
            // Centre vertically too
            if (inner.Height < outer.Height)
                r.Y = outer.Top + ((outer.Height - inner.Height) / 2);

            return r;
        }

        /// <summary>
        /// Draw the given image aligned horizontally within the column.
        /// </summary>
        /// <remarks>
        /// Over tall images are scaled to fit. Over-wide images are
        /// truncated. This is by design!
        /// </remarks>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        /// <param name="image">The image to be drawn</param>
        protected void DrawAlignedImage(Graphics g, Rectangle r, Image image)
        {
            if (image == null)
                return;

            // By default, the image goes in the top left of the rectangle
            Rectangle imageBounds = new Rectangle(r.Location, image.Size);

            // If the image is too tall to be drawn in the space provided, proportionally scale it down.
            // Too wide images are not scaled.
            if (image.Height > r.Height) {
                float scaleRatio = (float)r.Height / (float)image.Height;
                imageBounds.Width = (int)((float)image.Width * scaleRatio);
                imageBounds.Height = r.Height - 1;
            }

            // Align and draw our (possibly scaled) image
            g.DrawImage(image, this.AlignRectangle(r, imageBounds));
        }

        /// <summary>
        /// Fill in the background of this cell
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        protected void DrawBackground(Graphics g, Rectangle r)
        {
            if (this.IsDrawBackground) {
                using (Brush brush = new SolidBrush(this.GetBackgroundColor())) {
                    g.FillRectangle(brush, r);
                }
            }
        }

        #endregion


        /// <summary>
        /// The delegate that is called from the list view. This is the main entry point, but
        /// subclasses should override Render instead of this method.
        /// </summary>
        /// <param name="e">The event that caused this redraw</param>
        /// <param name="g">The context that our drawing should be done using</param>
        /// <param name="r">The bounds of the cell within which the renderer can draw</param>
        /// <param name="rowObject">The model object for this row</param>
        /// <returns>A boolean indicating whether the default process should occur</returns>
        public bool HandleRendering(EventArgs e, Graphics g, Rectangle r, Object rowObject)
        {
            this.ListView = (ObjectListView)this.Column.ListView;
            if (e is DrawListViewSubItemEventArgs) {
                this.Event = (DrawListViewSubItemEventArgs)e;
                this.ListItem = this.Event.Item as OLVListItem;
                this.SubItem = this.Event.SubItem;
                this.Column = this.ListView.GetColumn(this.Event.ColumnIndex);
            } else {
                this.DrawItemEvent = (DrawListViewItemEventArgs)e;
                this.ListItem = this.DrawItemEvent.Item as OLVListItem;
                this.SubItem = null;
                this.Column = this.ListView.GetColumn(0);
            }
            this.RowObject = rowObject;
            this.Aspect = null; // uncache previous result
            this.IsItemSelected = this.ListItem.Selected; // ((e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected);
            this.IsDrawBackground = true;
            this.Font = null;
            this.TextBrush = null;
            return this.OptionalRender(g, r);
        }

        /// <summary>
        /// Draw our data into the given rectangle using the given graphics context.
        /// </summary>
        /// <remarks>
        /// <para>Subclasses should override this method.</para></remarks>
        /// <param name="g">The graphics context that should be used for drawing</param>
        /// <param name="r">The bounds of the subitem cell</param>
        /// <returns>Returns whether the renderering has already taken place.
        /// If this returns false, the default processing will take over.
        /// </returns>
        public virtual bool OptionalRender(Graphics g, Rectangle r)
        {
            this.Render(g, r);
            return true;
        }

        /// <summary>
        /// Draw our data into the given rectangle using the given graphics context.
        /// </summary>
        /// <remarks>
        /// <para>Subclasses should override this method if they never want
        /// to fall back on the default processing</para></remarks>
        /// <param name="g">The graphics context that should be used for drawing</param>
        /// <param name="r">The bounds of the subitem cell</param>
        public virtual void Render(Graphics g, Rectangle r)
        {
            this.DrawBackground(g, r);

            // Adjust the rectangle to match the padding used by the native mode of the ListView
            Rectangle r2 = r;
            r2.X += 4;
            r2.Width -= 4;
            this.DrawImageAndText(g, r2);
        }

        /// <summary>
        /// Draw our subitems image and text
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        protected void DrawImageAndText(Graphics g, Rectangle r)
        {
            if (this.ListView.CheckBoxes && this.Column.Index == 0) {
                int spaceUsed = this.DrawCheckBox(g, r);
                r.X += spaceUsed;
                r.Width -= spaceUsed;
            }
            
            this.DrawImageAndText(g, r, this.GetText(), this.GetImage());
        }

        /// <summary>
        /// Draw the check box of this row
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        protected int DrawCheckBox(Graphics g, Rectangle r)
        {
            ImageList il = this.ListView.StateImageList;
            if (il == null || this.ListItem.StateImageIndex < 0)
                return 0;
            try {
                Image image = il.Images[this.ListItem.StateImageIndex];
                Point pt = r.Location;
                pt.Offset(2, (r.Height - il.ImageSize.Height) / 2);
                g.DrawImage(image, pt);
            }
            catch (ArgumentOutOfRangeException) {
                return 0;
            }
            return il.ImageSize.Width + 4;
        }

        /// <summary>
        /// Draw the given text and optional image in the "normal" fashion
        /// </summary>
        /// <param name="g">Graphics context to use for drawing</param>
        /// <param name="r">Bounds of the cell</param>
        /// <param name="txt">The string to be drawn</param>
        /// <param name="image">The optional image to be drawn</param>
        protected void DrawImageAndText(Graphics g, Rectangle r, String txt, Image image)
        {
            // Draw the image
            if (image != null) {
                int top = r.Y;
                if (image.Size.Height < r.Height)
                    top += ((r.Height - image.Size.Height) / 2);

                g.DrawImageUnscaled(image, r.X, top);
                r.X += image.Width + 2;
                r.Width -= image.Width + 2;
            }

            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            if (!this.CanWrap)
                fmt.FormatFlags = StringFormatFlags.NoWrap;
            switch (this.Column.TextAlign) {
                case HorizontalAlignment.Center:
                    fmt.Alignment = StringAlignment.Center;
                    break;
                case HorizontalAlignment.Left:
                    fmt.Alignment = StringAlignment.Near;
                    break;
                case HorizontalAlignment.Right:
                    fmt.Alignment = StringAlignment.Far;
                    break;
            }

            // Draw the background of the text as selected, if it's the primary column
            // and it's selected and it's not in FullRowSelect mode.
            if (this.IsDrawBackground && this.IsItemSelected && this.Column.Index == 0 && !this.ListView.FullRowSelect) {
                SizeF size = g.MeasureString(txt, this.Font, r.Width, fmt);
                // This is a tighter selection box
                //Rectangle r2 = this.AlignRectangle(r, new Rectangle(0, 0, (int)(size.Width + 1), (int)(size.Height + 1)));
                Rectangle r2 = r;
                r2.Width = (int)size.Width + 1;
                using (Brush brush = new SolidBrush(this.ListView.HighlightBackgroundColorOrDefault))
                    g.FillRectangle(brush, r2);
            }

            RectangleF rf = r;
            g.DrawString(txt, this.Font, this.TextBrush, rf, fmt);

            // We should put a focus rectange around the column 0 text if it's selected --
            // but we don't because:
            // - I really dislike this UI convention
            // - we are using buffered graphics, so the DrawFocusRecatangle method of the event doesn't work

            //if (this.Column.Index == 0) {
            //    Size size = TextRenderer.MeasureText(this.SubItem.Text, this.ListView.ListFont);
            //    if (r.Width > size.Width)
            //        r.Width = size.Width;
            //    this.Event.DrawFocusRectangle(r);
            //}
        }
    }

    /// <summary>
    /// This class maps a data value to an image that should be drawn for that value.
    /// </summary>
    /// <remarks><para>It is useful for drawing data that is represented as an enum or boolean.</para></remarks>
    public class MappedImageRenderer : BaseRenderer
    {
        /// <summary>
        /// Return a renderer that draw boolean values using the given images
        /// </summary>
        /// <param name="trueImage">Draw this when our data value is true</param>
        /// <param name="falseImage">Draw this when our data value is false</param>
        /// <returns>A Renderer</returns>
        static public MappedImageRenderer Boolean(Object trueImage, Object falseImage)
        {
            return new MappedImageRenderer(true, trueImage, false, falseImage);
        }

        /// <summary>
        /// Return a renderer that draw tristate boolean values using the given images
        /// </summary>
        /// <param name="trueImage">Draw this when our data value is true</param>
        /// <param name="falseImage">Draw this when our data value is false</param>
        /// <param name="nullImage">Draw this when our data value is null</param>
        /// <returns>A Renderer</returns>
        static public MappedImageRenderer TriState(Object trueImage, Object falseImage, Object nullImage)
        {
            return new MappedImageRenderer(new Object[] { true, trueImage, false, falseImage, null, nullImage });
        }

        /// <summary>
        /// Make a new empty renderer
        /// </summary>
        public MappedImageRenderer()
            : base()
        {
            map = new System.Collections.Hashtable();
        }

        /// <summary>
        /// Make a new renderer that will show the given image when the given key is the aspect value
        /// </summary>
        /// <param name="key">The data value to be matched</param>
        /// <param name="image">The image to be shown when the key is matched</param>
        public MappedImageRenderer(Object key, Object image)
            : this()
        {
            this.Add(key, image);
        }

        /// <summary>
        /// Make a new renderer that will show the given images when it receives the given keys
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="image1"></param>
        /// <param name="key2"></param>
        /// <param name="image2"></param>
        public MappedImageRenderer(Object key1, Object image1, Object key2, Object image2)
            : this()
        {
            this.Add(key1, image1);
            this.Add(key2, image2);
        }

        /// <summary>
        /// Build a renderer from the given array of keys and their matching images
        /// </summary>
        /// <param name="keysAndImages">An array of key/image pairs</param>
        public MappedImageRenderer(Object[] keysAndImages)
            : this()
        {
            if ((keysAndImages.GetLength(0) % 2) != 0)
                throw new ArgumentException("Array must have key/image pairs");

            for (int i = 0; i < keysAndImages.GetLength(0); i += 2)
                this.Add(keysAndImages[i], keysAndImages[i + 1]);
        }

        /// <summary>
        /// Register the image that should be drawn when our Aspect has the data value.
        /// </summary>
        /// <param name="value">Value that the Aspect must match</param>
        /// <param name="image">An ImageSelector -- an int, string or image</param>
        public void Add(Object value, Object image)
        {
            if (value == null)
                this.nullImage = image;
            else
                map[value] = image;
        }

        /// <summary>
        /// Render our value
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            this.DrawBackground(g, r);

            if (this.Aspect is ICollection)
                this.RenderCollection(g, r, (ICollection)this.Aspect);
            else
                this.RenderOne(g, r, this.Aspect);
        }

        private void RenderCollection(Graphics g, Rectangle r, ICollection imageSelectors)
        {
            Image image = null;
            Point pt = r.Location;
            foreach (Object selector in imageSelectors) {
                if (selector == null)
                    image = this.GetImage(this.nullImage);
                else if (map.ContainsKey(selector))
                    image = this.GetImage(map[selector]);
                else
                    image = null;

                if (image != null) {
                    g.DrawImage(image, pt);
                    pt.X += (image.Width + this.Spacing);
                }
            }
        }

        private void RenderOne(Graphics g, Rectangle r, Object selector)
        {
            Image image = null;
            if (selector == null)
                image = this.GetImage(this.nullImage);
            else
                if (map.ContainsKey(selector))
                    image = this.GetImage(map[selector]);

            if (image != null)
                this.DrawAlignedImage(g, r, image);
        }

        #region Private variables

        private Hashtable map; // Track the association between values and images
        private Object nullImage; // image to be drawn for null values (since null can't be a key)

        #endregion
    }

    /// <summary>
    /// Render an image that comes from our data source.
    /// </summary>
    /// <remarks>The image can be sourced from:
    /// <list>
    /// <item>a byte-array (normally when the image to be shown is
    /// stored as a value in a database)</item>
    /// <item>an int, which is treated as an index into the image list</item>
    /// <item>a string, which is treated first as a file name, and failing that as an index into the image list</item>
    /// </list>
    /// <para>If an image is an animated GIF, it's state is stored in the SubItem object.</para>
    /// <para>By default, the image renderer does not render animations (it begins life with animations paused).
    /// To enable animations, you must call Unpause().</para>
    /// </remarks>
    public class ImageRenderer : BaseRenderer
    {
        /// <summary>
        /// Make an empty image renderer
        /// </summary>
        public ImageRenderer()
            : base()
        {
            this.tickler = new System.Threading.Timer(new TimerCallback(this.OnTimer), null, Timeout.Infinite, Timeout.Infinite);
            this.stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Make an empty image renderer that begins life ready for animations
        /// </summary>
        public ImageRenderer(bool startAnimations)
            : this()
        {
            this.Paused = !startAnimations;
        }

        /// <summary>
        /// Draw our image
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            this.DrawBackground(g, r);
            this.DrawAlignedImage(g, r, this.GetImageFromAspect());
        }

        /// <summary>
        /// Translate our Aspect into an image.
        /// </summary>
        /// <remarks>The strategy is:<list type="unordered">
        /// <item>If its a byte array, we treat it as an in-memory image</item>
        /// <item>If it's an int, we use that as an index into our image list</item>
        /// <item>If it's a string, we try to load a file by that name. If we can't, we use the string as an index into our image list.</item>
        ///</list></remarks>
        /// <returns>An image</returns>
        protected Image GetImageFromAspect()
        {
            if (this.Aspect == null || this.Aspect == System.DBNull.Value)
                return null;

            // If we've already figured out the image, don't do it again
            if (this.OLVSubItem != null && this.OLVSubItem.ImageSelector is Image) {
                if (this.OLVSubItem.AnimationState == null)
                    return (Image)this.OLVSubItem.ImageSelector;
                else
                    return this.OLVSubItem.AnimationState.image;
            }

            // Try to convert our Aspect into an Image
            // If its a byte array, we treat it as an in-memory image
            // If it's an int, we use that as an index into our image list
            // If it's a string, we try to find a file by that name.
            //    If we can't, we use the string as an index into our image list.
            Image image = null;
            if (this.Aspect is System.Byte[]) {
                using (MemoryStream stream = new MemoryStream((System.Byte[])this.Aspect)) {
                    try {
                        image = Image.FromStream(stream);
                    }
                    catch (ArgumentException) {
                        // ignore
                    }
                }
            } else if (this.Aspect is Int32) {
                image = this.GetImage(this.Aspect);
            } else {
                String str = this.Aspect as String;
                if (!String.IsNullOrEmpty(str)) {
                    try {
                        image = Image.FromFile(str);
                    }
                    catch (FileNotFoundException) {
                        image = this.GetImage(this.Aspect);
                    }
                    catch (OutOfMemoryException) {
                        image = this.GetImage(this.Aspect);
                    }
                }
            }

            // If this image is an animation, initialize the animation process
            if (this.OLVSubItem != null && AnimationState.IsAnimation(image)) {
                this.OLVSubItem.AnimationState = new AnimationState(image);
            }

            // Cache the image so we don't repeat this dreary process
            if (this.OLVSubItem != null)
                this.OLVSubItem.ImageSelector = image;

            return image;
        }

        /// <summary>
        /// Should the animations in this renderer be paused?
        /// </summary>
        public bool Paused
        {
            get { return isPaused; }
            set {
                if (isPaused != value) {
                    isPaused = value;
                    if (isPaused) {
                        this.tickler.Change(Timeout.Infinite, Timeout.Infinite);
                        this.stopwatch.Stop();
                    } else {
                        this.tickler.Change(1, Timeout.Infinite);
                        this.stopwatch.Start();
                    }
                }
            }
        }
        private bool isPaused = true;

        /// <summary>
        /// Pause any animations
        /// </summary>
        public void Pause()
        {
            this.Paused = true;
        }

        /// <summary>
        /// Unpause any animations
        /// </summary>
        public void Unpause()
        {
            this.Paused = false;
        }

        /// <summary>
        /// This is the method that is invoked by the timer. It basically switches control to the listview thread.
        /// </summary>
        /// <param name="state">not used</param>
        public void OnTimer(Object state)
        {
            if (this.ListView == null || this.Paused)
                this.tickler.Change(1000, Timeout.Infinite);
            else {
                if (this.ListView.InvokeRequired)
                    this.ListView.Invoke((MethodInvoker)delegate { this.OnTimer(state); });
                else
                    this.OnTimerInThread();
            }
        }

        /// <summary>
        /// This is the OnTimer callback, but invoked in the same thread as the creator of the ListView.
        /// This method can use all of ListViews methods without creating a CrossThread exception.
        /// </summary>
        protected void OnTimerInThread()
        {
            // MAINTAINER NOTE: This method must renew the tickler. If it doesn't the animations will stop.

            // If this listview has been destroyed, we can't do anything, so we return without
            // renewing the tickler, effectively killing all animations on this renderer
            if (this.ListView.IsDisposed)
                return;

            // If we're not in Detail view or our column has been removed from the list,
            // we can't do anything at the moment, but we still renew the tickler because the view may change later.
            if (this.ListView.View != System.Windows.Forms.View.Details || this.Column.Index < 0) {
                this.tickler.Change(1000, Timeout.Infinite);
                return;
            }

            long elapsedMilliseconds = this.stopwatch.ElapsedMilliseconds;
            int subItemIndex = this.Column.Index;
            long nextCheckAt = elapsedMilliseconds + 1000; // wait at most one second before checking again
            Rectangle updateRect = new Rectangle(); // what part of the view must be updated to draw the changed gifs?

            // Run through all the subitems in the view for our column, and for each one that
            // has an animation attached to it, see if the frame needs updating.
            foreach (ListViewItem lvi in this.ListView.Items) {
                // Get the gif state from the subitem. If there isn't an animation state, skip this row.
                OLVListSubItem lvsi = (OLVListSubItem)lvi.SubItems[subItemIndex];
                AnimationState state = lvsi.AnimationState;
                if (state == null || !state.IsValid)
                    continue;

                // Has this frame of the animation expired?
                if (elapsedMilliseconds >= state.currentFrameExpiresAt) {
                    state.AdvanceFrame(elapsedMilliseconds);

                    // Track the area of the view that needs to be redrawn to show the changed images
                    if (updateRect.IsEmpty)
                        updateRect = lvsi.Bounds;
                    else
                        updateRect = Rectangle.Union(updateRect, lvsi.Bounds);
                }

                // Remember the minimum time at which a frame is next due to change
                nextCheckAt = Math.Min(nextCheckAt, state.currentFrameExpiresAt);
            }

            // Update the part of the listview where frames have changed
            if (!updateRect.IsEmpty)
                this.ListView.Invalidate(updateRect);

            // Renew the tickler in time for the next frame change
            this.tickler.Change(nextCheckAt - elapsedMilliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Instances of this class kept track of the animation state of a single image.
        /// </summary>
        internal class AnimationState
        {
            const int PropertyTagTypeShort = 3;
            const int PropertyTagTypeLong = 4;
            const int PropertyTagFrameDelay = 0x5100;
            const int PropertyTagLoopCount = 0x5101;

            /// <summary>
            /// Is the given image an animation
            /// </summary>
            /// <param name="image">The image to be tested</param>
            /// <returns>Is the image an animation?</returns>
            static public bool IsAnimation(Image image)
            {
                if (image == null)
                    return false;
                else
                    return (new List<Guid>(image.FrameDimensionsList)).Contains(FrameDimension.Time.Guid);
            }

            /// <summary>
            /// Create an AnimationState in a quiet state
            /// </summary>
            public AnimationState()
            {
                this.imageDuration = new List<int>();
            }

            /// <summary>
            /// Create an animation state for the given image, which may or may not
            /// be an animation
            /// </summary>
            /// <param name="image">The image to be rendered</param>
            public AnimationState(Image image)
                : this()
            {
                if (!AnimationState.IsAnimation(image))
                    return;

                // How many frames in the animation?
                this.image = image;
                this.frameCount = this.image.GetFrameCount(FrameDimension.Time);

                // Find the delay between each frame.
                // The delays are stored an array of 4-byte ints. Each int is the
                // number of 1/100th of a second that should elapsed before the frame expires
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
                Debug.Assert(this.imageDuration.Count == this.frameCount, "There should be as many frame durations as there are frames.");
            }

            /// <summary>
            /// Does this state represent a valid animation
            /// </summary>
            public bool IsValid
            {
                get {
                    return (this.image != null && this.frameCount > 0);
                }
            }

            /// <summary>
            /// Advance our images current frame and calculate when it will expire
            /// </summary>
            public void AdvanceFrame(long millisecondsNow)
            {
                this.currentFrame = (this.currentFrame + 1) % this.frameCount;
                this.currentFrameExpiresAt = millisecondsNow + this.imageDuration[this.currentFrame];
                this.image.SelectActiveFrame(FrameDimension.Time, this.currentFrame);
            }

            internal int currentFrame;
            internal long currentFrameExpiresAt;
            internal Image image;
            internal List<int> imageDuration;
            internal int frameCount;
        }

        #region Private variables

        private System.Threading.Timer tickler; // timer used to tickle the animations
        private Stopwatch stopwatch; // clock used to time the animation frame changes

        #endregion
    }

    /// <summary>
    /// Render our Aspect as a progress bar
    /// </summary>
    public class BarRenderer : BaseRenderer
    {
        #region Constructors

        /// <summary>
        /// Make a BarRenderer
        /// </summary>
        public BarRenderer()
            : base()
        {
            this.Pen = new Pen(Color.Blue, 1);
            this.Brush = Brushes.Aquamarine;
            this.BackgroundBrush = Brushes.White;
            this.StartColor = Color.Empty;
        }

        /// <summary>
        /// Make a BarRenderer for the given range of data values
        /// </summary>
        public BarRenderer(int minimum, int maximum)
            : this()
        {
            this.MinimumValue = minimum;
            this.MaximumValue = maximum;
        }

        /// <summary>
        /// Make a BarRenderer using a custom bar scheme
        /// </summary>
        public BarRenderer(Pen pen, Brush brush)
            : this()
        {
            this.Pen = pen;
            this.Brush = brush;
            this.UseStandardBar = false;
        }

        /// <summary>
        /// Make a BarRenderer using a custom bar scheme
        /// </summary>
        public BarRenderer(int minimum, int maximum, Pen pen, Brush brush)
            : this(minimum, maximum)
        {
            this.Pen = pen;
            this.Brush = brush;
            this.UseStandardBar = false;
        }

        /// <summary>
        /// Make a BarRenderer that uses a horizontal gradient
        /// </summary>
        public BarRenderer(Pen pen, Color start, Color end)
            : this()
        {
            this.Pen = pen;
            this.SetGradient(start, end);
        }

        /// <summary>
        /// Make a BarRenderer that uses a horizontal gradient
        /// </summary>
        public BarRenderer(int minimum, int maximum, Pen pen, Color start, Color end)
            : this(minimum, maximum)
        {
            this.Pen = pen;
            this.SetGradient(start, end);
        }

        #endregion

        #region Public variables

        /// <summary>
        /// Should this bar be drawn in the system style
        /// </summary>
        public bool UseStandardBar = true;

        /// <summary>
        /// How many pixels in from our cell border will this bar be drawn
        /// </summary>
        public int Padding = 2;

        /// <summary>
        /// The Pen that will draw the frame surrounding this bar
        /// </summary>
        public Pen Pen;

        /// <summary>
        /// The brush that will be used to fill the bar
        /// </summary>
        public Brush Brush;

        /// <summary>
        /// The brush that will be used to fill the background of the bar
        /// </summary>
        public Brush BackgroundBrush;

        /// <summary>
        /// The first color when a gradient is used to fill the bar
        /// </summary>
        public Color StartColor;

        /// <summary>
        /// The end color when a gradient is used to fill the bar
        /// </summary>
        public Color EndColor;

        /// <summary>
        /// Regardless of how wide the column become the progress bar will never be wider than this
        /// </summary>
        public int MaximumWidth = 100;

        /// <summary>
        /// Regardless of how high the cell is  the progress bar will never be taller than this
        /// </summary>
        public int MaximumHeight = 16;

        /// <summary>
        /// The minimum data value expected. Values less than this will given an empty bar
        /// </summary>
        public int MinimumValue = 0;

        /// <summary>
        /// The maximum value for the range. Values greater than this will give a full bar
        /// </summary>
        public int MaximumValue = 100;

        #endregion

        /// <summary>
        /// Draw this progress bar using a gradient
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetGradient(Color start, Color end)
        {
            this.StartColor = start;
            this.EndColor = end;
            this.UseStandardBar = false;
        }

        /// <summary>
        /// Draw our aspect
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            this.DrawBackground(g, r);

            Rectangle frameRect = Rectangle.Inflate(r, 0 - this.Padding, 0 - this.Padding);
            frameRect.Width = Math.Min(frameRect.Width, this.MaximumWidth);
            frameRect.Height = Math.Min(frameRect.Height, this.MaximumHeight);
            frameRect = this.AlignRectangle(r, frameRect);

            // Convert our aspect to a numeric value
            // CONSIDER: Is this the best way to do this?
            if (!(this.Aspect is IConvertible))
                return;
            double aspectValue = ((IConvertible)this.Aspect).ToDouble(NumberFormatInfo.InvariantInfo);

            Rectangle fillRect = Rectangle.Inflate(frameRect, -1, -1);
            if (aspectValue <= this.MinimumValue)
                fillRect.Width = 0;
            else if (aspectValue < this.MaximumValue)
                fillRect.Width = (int)(fillRect.Width * (aspectValue - this.MinimumValue) / this.MaximumValue);

            if (this.UseStandardBar && ProgressBarRenderer.IsSupported) {
                ProgressBarRenderer.DrawHorizontalBar(g, frameRect);
                ProgressBarRenderer.DrawHorizontalChunks(g, fillRect);
            } else {
                g.FillRectangle(this.BackgroundBrush, frameRect);
                if (fillRect.Width > 0) {
                    // FillRectangle fills inside the given rectangle, so expand it a little
                    fillRect.Width++;
                    fillRect.Height++;
                    if (this.StartColor == Color.Empty)
                        g.FillRectangle(this.Brush, fillRect);
                    else {
                        using (LinearGradientBrush gradient = new LinearGradientBrush(frameRect, this.StartColor, this.EndColor, LinearGradientMode.Horizontal)) {
                            g.FillRectangle(gradient, fillRect);
                        }
                    }
                }
                g.DrawRectangle(this.Pen, frameRect);
            }
        }
    }

    /// <summary>
    /// An ImagesRenderer draws zero or more images depending on the data returned by its Aspect.
    /// </summary>
    /// <remarks><para>This renderer's Aspect must return a ICollection of ints, strings or Images,
    /// each of which will be drawn horizontally one after the other.</para></remarks>
    public class ImagesRenderer : BaseRenderer
    {

        /// <summary>
        /// Draw our data value
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            this.DrawBackground(g, r);

            ICollection imageSelectors = this.Aspect as ICollection;
            if (imageSelectors == null)
                return;

            Point pt = r.Location;
            foreach (Object selector in imageSelectors) {
                Image image = this.GetImage(selector);
                if (image != null) {
                    g.DrawImage(image, pt);
                    pt.X += (image.Width + this.Spacing);
                }
            }
        }
    }

    /// <summary>
    /// A MultiImageRenderer draws the same image a number of times based on our data value
    /// </summary>
    /// <remarks><para>The stars in the Rating column of iTunes is a good example of this type of renderer.</para></remarks>
    public class MultiImageRenderer : BaseRenderer
    {
        /// <summary>
        /// Make a quiet rendererer
        /// </summary>
        public MultiImageRenderer()
            : base()
        {
        }

        /// <summary>
        /// Make an image renderer that will draw the indicated image, at most maxImages times.
        /// </summary>
        /// <param name="imageSelector"></param>
        /// <param name="maxImages"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public MultiImageRenderer(Object imageSelector, int maxImages, int minValue, int maxValue)
            : this()
        {
            this.ImageSelector = imageSelector;
            this.MaxNumberImages = maxImages;
            this.MinimumValue = minValue;
            this.MaximumValue = maxValue;
        }

        /// <summary>
        /// The image selector that will give the image to be drawn
        /// </summary>
        public Object ImageSelector;

        /// <summary>
        /// What is the maximum number of images that this renderer should draw?
        /// </summary>
        public int MaxNumberImages = 10;

        /// <summary>
        /// Values less than or equal to this will have 0 images drawn
        /// </summary>
        public int MinimumValue = 0;

        /// <summary>
        /// Values greater than or equal to this will have MaxNumberImages images drawn
        /// </summary>
        public int MaximumValue = 100;

        /// <summary>
        /// Draw our data value
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            this.DrawBackground(g, r);

            Image image = this.GetImage(this.ImageSelector);
            if (image == null)
                return;

            // Convert our aspect to a numeric value
            // CONSIDER: Is this the best way to do this?
            if (!(this.Aspect is IConvertible))
                return;
            double aspectValue = ((IConvertible)this.Aspect).ToDouble(NumberFormatInfo.InvariantInfo);

            // Calculate how many images we need to draw to represent our aspect value
            int numberOfImages;
            if (aspectValue <= this.MinimumValue)
                numberOfImages = 0;
            else if (aspectValue < this.MaximumValue)
                numberOfImages = 1 + (int)(this.MaxNumberImages * (aspectValue - this.MinimumValue) / this.MaximumValue);
            else
                numberOfImages = this.MaxNumberImages;

            // If we need to shrink the image, what will its on-screen dimensions be?
            int imageScaledWidth = image.Width;
            int imageScaledHeight = image.Height;
            if (r.Height < image.Height) {
                imageScaledWidth = (int)((float)image.Width * (float)r.Height / (float)image.Height);
                imageScaledHeight = r.Height;
            }
            // Calculate where the images should be drawn
            Rectangle imageBounds = r;
            imageBounds.Width = (this.MaxNumberImages * (imageScaledWidth + this.Spacing)) - this.Spacing;
            imageBounds.Height = imageScaledHeight;
            imageBounds = this.AlignRectangle(r, imageBounds);

            // Finally, draw the images
            for (int i = 0; i < numberOfImages; i++) {
                g.DrawImage(image, imageBounds.X, imageBounds.Y, imageScaledWidth, imageScaledHeight);
                imageBounds.X += (imageScaledWidth + this.Spacing);
            }
        }
    }


    /// <summary>
    /// A class to render a value that contains a bitwise-OR'ed collection of values.
    /// </summary>
    /// <typeparam name="T">The type of value that holds the bit-OR'ed flag</typeparam>
    public class FlagRenderer<T> : BaseRenderer //where T : IConvertible
    {
        /// <summary>
        /// Register the given image to the given value
        /// </summary>
        /// <param name="key">When this flag is present...</param>
        /// <param name="imageSelector">...draw this image</param>
        public void Add(T key, Object imageSelector)
        {
            Int32 k2 = ((IConvertible)key).ToInt32(NumberFormatInfo.InvariantInfo);

            this.imageMap[k2] = imageSelector;
            this.keysInOrder.Remove(k2);
            this.keysInOrder.Add(k2);
        }

        /// <summary>
        /// Draw the flags
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            this.DrawBackground(g, r);

            Int32 v2 = ((IConvertible)this.Aspect).ToInt32(NumberFormatInfo.InvariantInfo);

            Point pt = r.Location;
            foreach (Int32 key in this.keysInOrder) {
                if ((v2 & key) == key) {
                    Image image = this.GetImage(this.imageMap[key]);
                    if (image != null) {
                        g.DrawImage(image, pt);
                        pt.X += (image.Width + this.Spacing);
                    }
                }
            }
        }

        private List<Int32> keysInOrder = new List<Int32>();
        private Dictionary<Int32, Object> imageMap = new Dictionary<Int32, object>();
    }
}
