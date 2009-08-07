/*
 * Overlays - Decorations that can be rendered over the top of a ListView
 *
 * Author: Phillip Piper
 * Date: 14/04/2009 4:36 PM
 *
 * Change log:
 * 200-07-24    JPP  - TintedColumnDecoration now works when last item is a member of a collapsed
 *                     group (well, it no longer crashes).
 * v2.2
 * 2009-06-01   JPP  - Make sure that TintedColumnDecoration reaches to the last item in group view
 * 2009-05-05   JPP  - Unified BillboardOverlay text rendering with that of TextOverlay
 * 2009-04-30   JPP  - Added TintedColumnDecoration
 * 2009-04-14   JPP  - Initial version
 *
 * To do:
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// The interface for an object which can draw itself over the top of
    /// an ObjectListView.
    /// </summary>
    public interface IOverlay
    {
        /// <summary>
        /// Draw this overlay
        /// </summary>
        /// <param name="olv">The ObjectListView that is being overlaid</param>
        /// <param name="g">The Graphics onto the given OLV</param>
        /// <param name="r">The content area of the OLV</param>
        void Draw(ObjectListView olv, Graphics g, Rectangle r);
    }

    /// <summary>
    /// A null implementation of the IOverlay interface
    /// </summary>
    public class AbstractOverlay : IOverlay
    {
        #region IOverlay Members

        /// <summary>
        /// Draw this overlay
        /// </summary>
        /// <param name="olv">The ObjectListView that is being overlaid</param>
        /// <param name="g">The Graphics onto the given OLV</param>
        /// <param name="r">The content area of the OLV</param>
        public virtual void Draw(ObjectListView olv, Graphics g, Rectangle r) {
        }

        #endregion
    }

    /// <summary>
    /// An overlay that can be positioned within the ObjectListView.
    /// </summary>
    [TypeConverter(typeof(OverlayConverter))]
    public class GraphicOverlay : AbstractOverlay
    {
        #region Public properties

        /// <summary>
        /// Get or set where within the content rectangle of the listview this overlay should be drawn
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("Where within the content rectangle of the listview the overlay will be drawn"),
         DefaultValue(System.Drawing.ContentAlignment.BottomRight),
         RefreshProperties(RefreshProperties.Repaint)]
        public System.Drawing.ContentAlignment Alignment {
            get { return this.overlayImageAlignment; }
            set { this.overlayImageAlignment = value; }
        }
        private System.Drawing.ContentAlignment overlayImageAlignment = System.Drawing.ContentAlignment.BottomRight;

        /// <summary>
        /// Gets or sets the number of pixels that this overlay will be inset of the horizontal edges of the
        /// ListViews content rectangle
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The number of pixels that the overlay will be inset from the horizontal edges of the ListViews content rectangle"),
         DefaultValue(20),
         NotifyParentProperty(true)]
        public int InsetX {
            get { return this.insetX; }
            set { this.insetX = Math.Max(0, value); }
        }
        private int insetX = 20;

        /// <summary>
        /// Gets or sets the number of pixels that this overlay will be inset from the vertical edges of the
        /// ListViews content rectangle
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The number of pixels that the overlay will be inset from the vertical edges of the ListViews content rectangle"),
         DefaultValue(20),
         NotifyParentProperty(true)]
        public int InsetY {
            get { return this.insetY; }
            set { this.insetY = Math.Max(0, value); }
        }
        private int insetY = 20;

        /// <summary>
        /// Gets or sets the degree of rotation by which the graphic will be transformed.
        /// The centre of rotation will be the center point of the graphic.
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The degree of rotation that will be applied to the graphic."),
         DefaultValue(0),
         NotifyParentProperty(true)]
        public int Rotation {
            get { return this.rotation; }
            set { this.rotation = value; }
        }
        private int rotation;

        #endregion

        #region Calculations

        /// <summary>
        /// Align a rectangle of the given size within the given bounds,
        /// obeying alignment and inset.
        /// </summary>
        /// <param name="bounds">The outer bounds</param>
        /// <param name="size">How big the rectangle should be</param>
        /// <returns>A rectangle</returns>
        protected Point CalculateAlignedLocation(Rectangle bounds, Size size) {
            Rectangle r = bounds;
            r.Inflate(-this.InsetX, -this.InsetY);

            Point pt = r.Location;
            switch (this.Alignment) {
                case System.Drawing.ContentAlignment.TopLeft:
                    return new Point(r.X, r.Top);
                case System.Drawing.ContentAlignment.TopCenter:
                    return new Point(r.X + ((r.Width - size.Width) / 2), r.Top);
                case System.Drawing.ContentAlignment.TopRight:
                    return new Point(r.Right - size.Width, r.Top);
                case System.Drawing.ContentAlignment.MiddleLeft:
                    return new Point(r.X, r.Y + ((r.Height - size.Height) / 2));
                case System.Drawing.ContentAlignment.MiddleCenter:
                    return new Point(r.X + ((r.Width - size.Width) / 2), r.Y + ((r.Height - size.Height) / 2));
                case System.Drawing.ContentAlignment.MiddleRight:
                    return new Point(r.Right - size.Width, r.Y + ((r.Height - size.Height) / 2));
                case System.Drawing.ContentAlignment.BottomLeft:
                    return new Point(r.X, r.Bottom - size.Height);
                case System.Drawing.ContentAlignment.BottomCenter:
                    return new Point(r.X + ((r.Width - size.Width) / 2), r.Bottom - size.Height);
                case System.Drawing.ContentAlignment.BottomRight:
                    return new Point(r.Right - size.Width, r.Bottom - size.Height);
            }

            // Should never reach here
            return bounds.Location;
        }

        /// <summary>
        /// Apply any specified rotation to the Graphic content.
        /// </summary>
        /// <param name="g">The Graphics to be transformed</param>
        /// <param name="r">The rotation will be around the centre of this rect</param>
        protected void ApplyRotation(Graphics g, Rectangle r) {
            if (this.Rotation == 0)
                return;

            // THINK: Do we want to reset the transform? I think we want to push a new transform
            g.ResetTransform();
            Matrix m = new Matrix();
            m.RotateAt(this.Rotation, new Point(r.Left + r.Width / 2, r.Top + r.Height / 2));
            g.Transform = m;
        }

        /// <summary>
        /// Reverse the rotation created by ApplyRotation()
        /// </summary>
        /// <param name="g"></param>
        protected void UnapplyRotation(Graphics g) {
            if (this.Rotation != 0)
                g.ResetTransform();
        }

        #endregion

        /// <summary>
        /// Control how the overlay is presented in the IDE
        /// </summary>
        protected class OverlayConverter : ExpandableObjectConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
                if (destinationType == typeof(string)) {
                    ImageOverlay imageOverlay = value as ImageOverlay;
                    if (imageOverlay != null) {
                        if (imageOverlay.Image == null)
                            return "(none)";
                        else
                            return "(set)";
                    }
                    TextOverlay textOverlay = value as TextOverlay;
                    if (textOverlay != null) {
                        if (String.IsNullOrEmpty(textOverlay.Text))
                            return "(none)";
                        else
                            return "(set)";
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }

    /// <summary>
    /// An overlay that will draw an image over the top of the ObjectListView
    /// </summary>
    public class ImageOverlay : GraphicOverlay
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the image that will be drawn over the top of the ListView
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The image that will be drawn over the top of the ListView"),
         DefaultValue(null),
         NotifyParentProperty(true)]
        public Image Image {
            get { return this.image; }
            set { this.image = value; }
        }
        private Image image;

        #endregion

        #region Commands

        /// <summary>
        /// Draw this overlay
        /// </summary>
        /// <param name="olv">The ObjectListView being decorated</param>
        /// <param name="g">The Graphics used for drawing</param>
        /// <param name="r">The bounds of the rendering</param>
        public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
            if (this.Image == null)
                return;

            Point pt = this.CalculateAlignedLocation(r, this.Image.Size);
            try {
                this.ApplyRotation(g, new Rectangle(pt, this.Image.Size));
                this.DrawTransparentBitmap(g, pt, this.Image, 255);
            }
            finally {
                this.UnapplyRotation(g);
            }
        }

        private void DrawTransparentBitmap(Graphics g, Point pt, Image image, int transparency) {
            ImageAttributes imageAttributes = new ImageAttributes();
            if (transparency != 255) {
                float a = (float)transparency / 255.0f;
                float[][] colorMatrixElements = {
                    new float[] {1,  0,  0,  0, 0},
                    new float[] {0,  1,  0,  0, 0},
                    new float[] {0,  0,  1,  0, 0},
                    new float[] {0,  0,  0,  a, 0},
                    new float[] {0,  0,  0,  0, 1}};

                imageAttributes.SetColorMatrix(new ColorMatrix(colorMatrixElements));
            }

            g.DrawImage(image,
               new Rectangle(pt, image.Size),              // destination rectangle
               0, 0, image.Size.Width, image.Size.Height,  // source rectangle
               GraphicsUnit.Pixel,
               imageAttributes);
        }

        #endregion
    }

    /// <summary>
    /// An overlay that will draw text over the top of the ObjectListView
    /// </summary>
    public class TextOverlay : GraphicOverlay
    {
        public TextOverlay() {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the font that will be used to draw the text over the top of the ListView
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The font that will be used to draw the text"),
         DefaultValue(null),
         NotifyParentProperty(true)]
        public Font Font {
            get { return this.font; }
            set { this.font = value; }
        }
        private Font font;

        /// <summary>
        /// Gets or sets the font that will be used to draw the text or a reasonable default
        /// </summary>
        [Browsable(false)]
        public Font FontOrDefault {
            get {
                return this.Font ?? new Font("Tahoma", 16);
            }
        }

        /// <summary>
        /// Gets or sets whether the border will be drawn with rounded corners
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("Will the border be drawn with rounded corners"),
         DefaultValue(true)]
        public bool RoundCorneredBorder {
            get { return this.roundCorneredBorder; }
            set { this.roundCorneredBorder = value; }
        }
        private bool roundCorneredBorder = true;

        /// <summary>
        /// Gets or sets the color of the text
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The color of the text"),
         DefaultValue(typeof(Color), "DarkBlue"),
         NotifyParentProperty(true)]
        public Color TextColor {
            get { return this.textColor; }
            set { this.textColor = value; }
        }
        private Color textColor = Color.DarkBlue;

        /// <summary>
        /// Gets the brush that will be used to paint the text
        /// </summary>
        [Browsable(false)]
        public Brush TextBrush {
            get {
                return new SolidBrush(Color.FromArgb(this.transparency, this.TextColor));
            }
        }

        /// <summary>
        /// Gets or sets the text that will be drawn over the top of the ListView
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The text that will be drawn over the top of the ListView"),
         DefaultValue(null),
         NotifyParentProperty(true),
         Localizable(true)]
        public string Text {
            get { return this.text; }
            set { this.text = value; }
        }
        private string text;

        /// <summary>
        /// Gets or sets the background color of the text
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("the background color of the billboard text"),
         DefaultValue(typeof(Color), "")]
        public Color BackColor {
            get { return this.backColor; }
            set { this.backColor = value; }
        }
        private Color backColor = Color.Empty;

        /// <summary>
        /// Does this overlay have a border?
        /// </summary>
        [Browsable(false)]
        public bool HasBackground {
            get {
                return this.BackColor != Color.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the color of the border around the billboard.
        /// Set this to Color.Empty to remove the border
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The color of the border around the text"),
         DefaultValue(typeof(Color), "")]
        public Color BorderColor {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }
        private Color borderColor = Color.Empty;

        /// <summary>
        /// Gets or sets the width of the border around the text
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The width of the border around the text"),
         DefaultValue(0.0f)]
        public float BorderWidth {
            get { return this.borderWidth; }
            set { this.borderWidth = value; }
        }
        private float borderWidth;

        /// <summary>
        /// Gets the brush that will be used to paint the text
        /// </summary>
        [Browsable(false)]
        public Pen BorderPen {
            get {
                return new Pen(Color.FromArgb(this.transparency, this.BorderColor), this.BorderWidth);
            }
        }

        /// <summary>
        /// Does this overlay have a border?
        /// </summary>
        [Browsable(false)]
        public bool HasBorder {
            get {
                return this.BorderColor != Color.Empty && this.BorderWidth > 0;
            }
        }

        /// <summary>
        /// Gets the brush that will be used to paint the text
        /// </summary>
        [Browsable(false)]
        public Brush BackgroundBrush {
            get {
                return new SolidBrush(Color.FromArgb(this.transparency, this.BackColor));
            }
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Draw this overlay
        /// </summary>
        /// <param name="olv">The ObjectListView being decorated</param>
        /// <param name="g">The Graphics used for drawing</param>
        /// <param name="r">The bounds of the rendering</param>
        public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
            if (String.IsNullOrEmpty(this.Text))
                return;

            this.transparency = 255;

            Rectangle textRect = this.CalculateTextBounds(g, r);
            this.DrawBorderedText(g, textRect);
        }

        /// <summary>
        /// Draw the text with a border
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        protected void DrawBorderedText(Graphics g, Rectangle textRect) {
            Rectangle borderRect = textRect;
            borderRect.Inflate((int)this.BorderWidth / 2, (int)this.BorderWidth / 2);
            borderRect.Y -= 1; // Looker better a little higher

            StringFormat sf;
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;

            try {
                this.ApplyRotation(g, textRect);
                if (this.RoundCorneredBorder) {
                    float diameter = borderRect.Height / 3; // this should be a property
                    using (GraphicsPath path = this.GetRoundedRect(borderRect, diameter)) {
                        if (this.HasBackground)
                            g.FillPath(this.BackgroundBrush, path);

                        g.DrawString(this.Text, this.FontOrDefault, this.TextBrush, textRect, sf);

                        if (this.HasBorder)
                            g.DrawPath(this.BorderPen, path);
                    }
                } else {
                    if (this.HasBackground)
                        g.FillRectangle(this.BackgroundBrush, textRect);

                    g.DrawString(this.Text, this.FontOrDefault, this.TextBrush, textRect, sf);

                    if (this.HasBorder)
                        g.DrawRectangle(this.BorderPen, borderRect);
                }
            }
            finally {
                this.UnapplyRotation(g);
            }
        }

        /// <summary>
        /// Return the rectangle that will be the precise bounds of the displayed text
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <returns>The bounds of the text</returns>
        protected Rectangle CalculateTextBounds(Graphics g, Rectangle r) {
            Rectangle insetRect = r;
            insetRect.Inflate(-this.InsetX, -this.InsetY);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            SizeF sizeF = g.MeasureString(this.Text, this.FontOrDefault, insetRect.Width, sf);
            Size size = new Size(1 + (int)sizeF.Width, 1 + (int)sizeF.Height);
            Point location = this.CalculateAlignedLocation(r, size);
            return new Rectangle(location, size);
        }

        /// <summary>
        /// Return a GraphicPath that is a round cornered rectangle
        /// </summary>
        /// <param name="rect">The rectangle</param>
        /// <param name="diameter">The diameter of the corners</param>
        /// <returns>A round cornered rectagle path</returns>
        /// <remarks>If I could rely on people using C# 3.0+, this should be
        /// an extension method of GraphicsPath.</remarks>
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

        #endregion

        #region Private variables

        protected int transparency;

        #endregion

        /// <summary>
        /// Draw simple text without a border or background.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        //protected void DrawSimpleText(Graphics g, Rectangle r) {
        //    StringFormat sf = new StringFormat();
        //    sf.Trimming = StringTrimming.EllipsisCharacter;
        //    switch (this.Alignment) {
        //        case ContentAlignment.TopLeft:
        //            sf.Alignment = StringAlignment.Near;
        //            sf.LineAlignment = StringAlignment.Near;
        //            break;
        //        case ContentAlignment.TopCenter:
        //            sf.Alignment = StringAlignment.Center;
        //            sf.LineAlignment = StringAlignment.Near;
        //            break;
        //        case ContentAlignment.TopRight:
        //            sf.Alignment = StringAlignment.Far;
        //            sf.LineAlignment = StringAlignment.Near;
        //            break;
        //        case ContentAlignment.MiddleLeft:
        //            sf.Alignment = StringAlignment.Near;
        //            sf.LineAlignment = StringAlignment.Center;
        //            break;
        //        case ContentAlignment.MiddleCenter:
        //            sf.Alignment = StringAlignment.Center;
        //            sf.LineAlignment = StringAlignment.Center;
        //            break;
        //        case ContentAlignment.MiddleRight:
        //            sf.Alignment = StringAlignment.Far;
        //            sf.LineAlignment = StringAlignment.Center;
        //            break;
        //        case ContentAlignment.BottomLeft:
        //            sf.Alignment = StringAlignment.Near;
        //            sf.LineAlignment = StringAlignment.Far;
        //            break;
        //        case ContentAlignment.BottomCenter:
        //            sf.Alignment = StringAlignment.Center;
        //            sf.LineAlignment = StringAlignment.Far;
        //            break;
        //        case ContentAlignment.BottomRight:
        //            sf.Alignment = StringAlignment.Far;
        //            sf.LineAlignment = StringAlignment.Far;
        //            break;
        //    }
        //    Rectangle r2 = r;
        //    r2.Inflate(-this.InsetX, -this.InsetY);
        //    g.DrawString(this.Text, this.FontOrDefault, this.TextBrush, r2, sf);
        //}
    }

    /// <summary>
    /// A Billboard overlay is positioned at an absolute point
    /// </summary>
    public class BillboardOverylay : TextOverlay
    {
        public BillboardOverylay() {
            this.BackColor = Color.PeachPuff;
            this.TextColor = Color.Black;
            this.BorderColor = Color.Empty;
            this.Font = new Font("Tahoma", 10);
        }

        /// <summary>
        /// Gets or sets where should the top left of the billboard be placed
        /// </summary>
        public Point Location {
            get { return this.location; }
            set { this.location = value; }
        }
        private Point location;

        /// <summary>
        /// Draw this overlay
        /// </summary>
        /// <param name="olv">The ObjectListView being decorated</param>
        /// <param name="g">The Graphics used for drawing</param>
        /// <param name="r">The bounds of the rendering</param>
        public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
            if (String.IsNullOrEmpty(this.Text))
                return;

            this.transparency = 255;

            // Calculate the bounds of the text, and then move it to where it should be
            Rectangle textRect = this.CalculateTextBounds(g, r);
            textRect.Location = this.Location;

            // Make sure the billboard is within the bounds of the List, as far as is possible
            if (textRect.Right > r.Width)
                textRect.X = Math.Max(r.Left, r.Width - textRect.Width);
            if (textRect.Bottom > r.Height)
                textRect.Y = Math.Max(r.Top, r.Height - textRect.Height);

            this.DrawBorderedText(g, textRect);
        }
    }

    /// <summary>
    /// This decoration draws a slight tint over a column of the
    /// owning listview. If no column is explicitly set, the selected
    /// column in the listview will be used.
    /// The selected column is normally the sort column,
    /// but does not have to be.
    /// </summary>
    public class TintedColumnDecoration : IOverlay
    {
        public TintedColumnDecoration() {
            this.Tint = Color.FromArgb(15, Color.Blue);
        }

        public TintedColumnDecoration(OLVColumn column) : this() {
            this.ColumnToTint = column;
        }

		#region Properties

        /// <summary>
        /// Gets or sets the column that will be tinted
        /// </summary>
		public OLVColumn ColumnToTint {
			get { return this.columnToTint; }
			set { this.columnToTint = value; }
		}
		private OLVColumn columnToTint;

        /// <summary>
        /// Gets or sets the color that will be 'tinted' over the selected column
        /// </summary>
        public Color Tint {
            get { return this.tint; }
            set {
                if (this.tint == value)
                    return;

                if (this.tintBrush != null) {
                    this.tintBrush.Dispose();
                    this.tintBrush = null;
                }

                this.tint = value;
                this.tintBrush = new SolidBrush(this.tint);
            }
        }
        private Color tint;
        private SolidBrush tintBrush;

		#endregion

        #region IOverlay Members

        public void Draw(ObjectListView olv, Graphics g, Rectangle r) {

            // This overlay only works when:
            // - the list is in Details view
            // - there is at least one row
            // - there is a selected column
            if (olv.View != System.Windows.Forms.View.Details)
                return;

            if (olv.GetItemCount() == 0)
                return;

            OLVColumn column = this.ColumnToTint ?? olv.SelectedColumn;
            if (column == null)
                return;

            Point sides = NativeMethods.GetColumnSides(olv, column.Index);
            if (sides.X == -1)
                return;

            Rectangle columnBounds = new Rectangle(sides.X, r.Top, sides.Y - sides.X, r.Bottom);

            // Find the bottom of the last item
            OLVListItem lastItem = olv.GetLastItemInDisplayOrder();
            if (lastItem != null) {
                Rectangle lastItemBounds = lastItem.Bounds;
                if (!lastItemBounds.IsEmpty && lastItemBounds.Bottom < columnBounds.Bottom)
                    columnBounds.Height = lastItemBounds.Bottom - columnBounds.Top;
            }
            g.FillRectangle(this.tintBrush, columnBounds);
        }

        #endregion
    }
}
