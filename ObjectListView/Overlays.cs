/*
 * Overlays - Decorations that can be rendered over the top of a ListView
 *
 * Author: Phillip Piper
 * Date: 14/04/2009 4:36 PM
 *
 * Change log:
 * 2009-04-14   JPP  - Initial version
 *
 * To do:
 * - Move ability to have border and background from billboard into text overlay
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// The interface for an object which can draw itself over the top of 
    /// an ObjectListView.
    /// </summary>
    public interface IOverlay
    {
        void Draw(ObjectListView olv, Graphics g, Rectangle r);
    }

    /// <summary>
    /// A null implementation of the IOverlay interface
    /// </summary>
    public class AbstractOverlay : IOverlay
    {
        #region IOverlay Members

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
         NotifyParentProperty(true),
         RefreshProperties(RefreshProperties.All)]
        public System.Drawing.ContentAlignment Alignment {
            get { return this.overlayImageAlignment; }
            set { this.overlayImageAlignment = value; }
        }
        private System.Drawing.ContentAlignment overlayImageAlignment = System.Drawing.ContentAlignment.BottomRight;

        /// <summary>
        /// Gets or sets the number of pixels that this overlay will be inset of the edge of the 
        /// ListViews content rectangle
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The number of pixels that the overlay will be inset of the edge of the ListViews content rectangle"),
         DefaultValue(20),
         NotifyParentProperty(true)]
        public int Inset {
            get { return this.inset; }
            set { this.inset = Math.Max(0, value); }
        }
        private int inset = 20;

        /// <summary>
        /// Gets or sets the transparency of this overlays. 
        /// 0 is completely transparent, 255 is completely opaque.
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("How transparent should the overlay be? 0 is opaque, 255 is completely transparent"),
         DefaultValue(128),
         NotifyParentProperty(true)]
        public int Transparency {
            get { return this.transparency; }
            set { this.transparency = Math.Min(255, Math.Max(0, value)); }
        }
        private int transparency = 128;

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
            r.Inflate(-this.Inset, -this.Inset);

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
            this.DrawTransparentBitmap(g, pt, this.Image, this.Transparency);
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
        /// Gets or sets the image that will be drawn over the top of the ListView
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The image that will be drawn over the top of the ListView"),
         DefaultValue(typeof(Color), "DarkGray"),
         NotifyParentProperty(true)]
        public Color TextColor {
            get { return this.textColor; }
            set { this.textColor = value; }
        }
        private Color textColor = Color.DarkGray;

        /// <summary>
        /// Gets the brush that will be used to paint the text
        /// </summary>
        [Browsable(false)]
        public Brush TextBrush {
            get {
                return new SolidBrush(Color.FromArgb(this.Transparency, this.TextColor));
            }
        }

        /// <summary>
        /// Gets or sets the image that will be drawn over the top of the ListView
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
        /// Draw this overlay
        /// </summary>
        /// <param name="olv">The ObjectListView being decorated</param>
        /// <param name="g">The Graphics used for drawing</param>
        /// <param name="r">The bounds of the rendering</param>
        public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
            if (String.IsNullOrEmpty(this.Text))
                return;

            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter;
            switch (this.Alignment) {
                case ContentAlignment.TopLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
            }

            Rectangle r2 = r;
            r2.Inflate(-this.Inset, -this.Inset);
            g.DrawString(this.Text, this.FontOrDefault, this.TextBrush, r2, sf);
        }
    }

    /// <summary>
    /// A Billboard overlay is positioned at an absolute point and 
    /// painted with a background and border
    /// </summary>
    public class BillboardOverylay : TextOverlay
    {
        public BillboardOverylay() {
            this.BackColor = Color.PeachPuff;
            this.TextColor = Color.Black;
            this.BorderWidth = 2.0f;
            this.BorderColor = Color.Empty;
            this.Transparency = 255;
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
        /// Gets or sets the background color of the billboard
        /// </summary>
        public Color BackColor {
            get { return this.backColor; }
            set { this.backColor = value; }
        }
        private Color backColor;

        /// <summary>
        /// Gets or sets the color of the border around the billboard.
        /// Set this to Color.Empty to remove the border
        /// </summary>
        public Color BorderColor {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }
        private Color borderColor;

        /// <summary>
        /// Gets or sets the width of the border around the billboard
        /// </summary>
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
                return new Pen(Color.FromArgb(this.Transparency, this.BorderColor), this.BorderWidth);
            }
        }
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
                return new SolidBrush(Color.FromArgb(this.Transparency, this.BackColor));
            }
        }

        /// <summary>
        /// Draw this overlay
        /// </summary>
        /// <param name="olv">The ObjectListView being decorated</param>
        /// <param name="g">The Graphics used for drawing</param>
        /// <param name="r">The bounds of the rendering</param>
        public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            int maximumWidth = Math.Max(200, olv.Width);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            SizeF size = g.MeasureString(this.Text, this.FontOrDefault, maximumWidth, sf);
            Rectangle r2 = new Rectangle(this.Location.X, this.Location.Y, 1+(int)size.Width, 1+(int)size.Height);

            if (this.HasBorder)
                r2.Inflate((int)this.BorderWidth / 2, (int)this.BorderWidth / 2);

            // Make sure the billboard is within the bounds of the List, as far as is possible
            if (r2.Right > olv.Width)
                r2.X = Math.Max(olv.Left, olv.Width - r2.Width);
            if (r2.Bottom > olv.Height)
                r2.Y = Math.Max(olv.Top, olv.Height - r2.Height);

            float diameter = r2.Height / 3;
            using (GraphicsPath path = this.GetRoundedRect(r2, diameter)) {
                g.FillPath(this.BackgroundBrush, path);
                if (this.HasBorder)
                    g.DrawPath(this.BorderPen, path);
                g.DrawString(this.Text, this.FontOrDefault, this.TextBrush, r2, sf);
            }
        }

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
    }
}
