using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    public partial class GlassPanelForm : Form
    {
        public GlassPanelForm() {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            FormBorderStyle = FormBorderStyle.None;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            this.Opacity = 0.5f;
            this.BackColor = Color.FromArgb(255, 254, 254, 254);
            this.TransparencyKey = this.BackColor;
            this.Bounds = new Rectangle(-10000, -10000, 1, 1);
            //this.DoubleBuffered = true;
            NativeMethods.ShowWithoutActivate(this);
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        /// <summary>
        /// Position the curtain over this control and listen to a position
        /// change in order to follow it if needed.
        /// </summary>        
        public void Bind(ObjectListView olv) {
            if (this.objectListView != null) {
                this.Unbind();
            }

            this.objectListView = olv;
            this.Owner = this.objectListView.TopLevelControl as Form;
            //this.objectListView.ClientSizeChanged += new EventHandler(objectListView_ClientSizeChanged);
            //this.objectListView.TopLevelControl.LocationChanged += new EventHandler(bindedControl_LocationChanged);
        }

        public void ShowGlass() {
            this.Bounds = ComputeControlScreenBounds(this.objectListView);
            this.Refresh();
        }

        //void objectListView_ClientSizeChanged(object sender, EventArgs e) {
        //    this.ClientSize = this.objectListView.ContentRectangle.Size;
        //    this.Invalidate();
        //}

        private Rectangle ComputeControlScreenBounds(ObjectListView olv) {
            Rectangle rect = olv.ClientRectangle;
            rect.X = 0;
            rect.Y = 0;
            return olv.RectangleToScreen(rect);
        }

        public void Unbind() {
            if (this.objectListView == null) 
                return;

            //this.objectListView.ClientSizeChanged -= new EventHandler(objectListView_ClientSizeChanged);
            //this.objectListView.TopLevelControl.LocationChanged -= new EventHandler(bindedControl_LocationChanged);
            this.objectListView = null;
        }

        public void HideGlass() {
            this.Bounds = new Rectangle(-10000, -10000, 1, 1);
        }

        //private void bindedControl_LocationChanged(object sender, EventArgs e) {
        //    Rectangle bounds = ComputeControlScreenBounds(this.objectListView);
        //    this.Left = bounds.X;
        //    this.Top = bounds.Y;
        //    this.Invalidate();
        //}

        protected override void OnPaint(PaintEventArgs e) {
            if (this.objectListView != null) {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.DrawRectangle(new Pen(Color.Green, 4.0f), this.ClientRectangle);
                this.objectListView.DrawBackgroundOverlays(g);
            }
        }

        protected override void WndProc(ref Message m) {
            const int WM_NCHITTEST = 132;
            const int HTTRANSPARENT = -1;
            switch (m.Msg) {
                // Ignore all mouse interactions
                case WM_NCHITTEST:
                    m.Result = (IntPtr)HTTRANSPARENT;
                    break;
            }
            base.WndProc(ref m);
        }

        ObjectListView objectListView;
    }
}
