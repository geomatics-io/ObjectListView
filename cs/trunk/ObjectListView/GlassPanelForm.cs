﻿/*
 * GlassPanelForm - A transparent form that is placed over an ObjectListView
 * to allow flicker-free overlay images during scrollin.
 *
 * Author: Phillip Piper
 * Date: 14/04/2009 4:36 PM
 *
 * Change log:
 * 2009-06-05   JPP  - Handle when owning window is a topmost window
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
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    public partial class GlassPanelForm : Form
    {
        public GlassPanelForm() {
            this.Name = "GlassPanelForm";
            this.Text = "GlassPanelForm";

            ClientSize = new System.Drawing.Size(0, 0);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;

            SetStyle(ControlStyles.Selectable, false);
            
            this.Opacity = 0.5f;
            this.BackColor = Color.FromArgb(255, 254, 254, 254);
            this.TransparencyKey = this.BackColor;
            this.HideGlass();
            NativeMethods.ShowWithoutActivate(this);
        }

        #region Properties

        /// <summary>
        /// Get the low-level windows flag that will be given to CreateWindow.
        /// </summary>
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Attach this form to the given ObjectListView
        /// </summary>        
        public void Bind(ObjectListView olv) {
            if (this.objectListView != null)
                this.Unbind();

            this.objectListView = olv;

            this.objectListView.LocationChanged += new EventHandler(objectListView_LocationChanged);
            this.objectListView.SizeChanged += new EventHandler(objectListView_SizeChanged);
            this.objectListView.VisibleChanged += new EventHandler(objectListView_VisibleChanged);
            this.objectListView.ParentChanged += new EventHandler(objectListView_ParentChanged);

            Control parent = this.objectListView.Parent;
            while (parent != null) {
                parent.ParentChanged += new EventHandler(objectListView_ParentChanged);
                TabControl tabControl = parent as TabControl;
                if (tabControl != null) {
                    tabControl.Selected += new TabControlEventHandler(tabControl_Selected);
                }
                parent = parent.Parent;
            }
            this.Owner = this.objectListView.TopLevelControl as Form;
            if (this.Owner != null) {
                this.Owner.LocationChanged += new EventHandler(Owner_LocationChanged);
                this.Owner.ResizeBegin += new EventHandler(Owner_ResizeBegin);
                this.Owner.ResizeEnd += new EventHandler(Owner_ResizeEnd);
                if (this.Owner.TopMost) {
                    // We can't do this.TopMost = true; since that will activate the panel,
                    // taking focus away from the owner of the listview
                    NativeMethods.MakeTopMost(this);
                }
            }

            this.UpdateTransparency();
        }

        /// <summary>
        /// Made the overlay panel invisible
        /// </summary>
        public void HideGlass() {
            if (!this.isGlassShown)
                return;

            this.isGlassShown = false;
            this.Bounds = new Rectangle(-10000, -10000, 1, 1);
        }

        /// <summary>
        /// Show the overlay panel in its correctly location
        /// </summary>
        /// <remarks>
        /// If the panel is always shown, this method does nothing.
        /// If the panel is being resized, this method also does nothing.
        /// </remarks>
        public void ShowGlass() {
            if (this.isGlassShown || this.isDuringResizeSequence)
                return;

            this.isGlassShown = true;
            this.RecalculateBounds();
        }

        /// <summary>
        /// Detach this glass panel from its previous ObjectListView
        /// </summary>        
        /// <remarks>
        /// You should unbind the overlay panel before making any changes to the 
        /// widget hierarchy.
        /// </remarks>
        public void Unbind() {
            if (this.objectListView == null)
                return;

            this.objectListView.SizeChanged -= new EventHandler(objectListView_SizeChanged);
            this.objectListView.VisibleChanged -= new EventHandler(objectListView_VisibleChanged);
            this.objectListView.ParentChanged -= new EventHandler(objectListView_ParentChanged);

            Control parent = this.objectListView.Parent;
            while (parent != null) {
                parent.ParentChanged -= new EventHandler(objectListView_ParentChanged);
                TabControl tabControl = parent as TabControl;
                if (tabControl != null) {
                    tabControl.Selected -= new TabControlEventHandler(tabControl_Selected);
                }
                parent = parent.Parent;
            }

            this.Owner = this.objectListView.TopLevelControl as Form;
            if (this.Owner != null) {
                this.Owner.LocationChanged -= new EventHandler(Owner_LocationChanged);
                this.Owner.ResizeBegin -= new EventHandler(Owner_ResizeBegin);
                this.Owner.ResizeEnd -= new EventHandler(Owner_ResizeEnd);
            }

            this.objectListView = null;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle when the form that owns the ObjectListView begins to be resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Owner_ResizeBegin(object sender, EventArgs e) {
            // When the top level window is being resized, we just want to hide
            // the overlay window. When the resizing finishes, we want to show
            // the overlay window, if it was shown before the resize started.
            this.isDuringResizeSequence = true;
            this.wasGlassShownBeforeResize = this.isGlassShown;
            this.HideGlass();
        }

        /// <summary>
        /// Handle when the form that owns the ObjectListView finished to be resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Owner_ResizeEnd(object sender, EventArgs e) {
            this.isDuringResizeSequence = false;
            if (this.wasGlassShownBeforeResize)
                this.ShowGlass();
        }

        /// <summary>
        /// Handle when the bound OLV changes its location. The overlay panel must 
        /// be moved too, IFF it is currently visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objectListView_LocationChanged(object sender, EventArgs e) {
            if (this.isGlassShown) {
                this.RecalculateBounds();
            }
        }

        /// <summary>
        /// Handle when the bound OLV changes size. The overlay panel must 
        /// resize too, IFF it is currently visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objectListView_SizeChanged(object sender, EventArgs e) {
            if (this.isGlassShown) {
                this.Size = this.objectListView.ClientSize;
            }
        }

        /// <summary>
        /// Handle when the bound OLV is part of a TabControl and that
        /// TabControl changes tabs. The overlay panel is hidden. The
        /// first time the bound OLV is redrawn, the overlay panel will
        /// be shown again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tabControl_Selected(object sender, TabControlEventArgs e) {
            this.HideGlass();
        }

        /// <summary>
        /// Somewhere the parent of the bound OLV has changed. Update
        /// our events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objectListView_ParentChanged(object sender, EventArgs e) {
            ObjectListView olv = this.objectListView;
            this.Unbind();
            this.Bind(olv);
        }

        /// <summary>
        /// Handle when the bound OLV changes its visibility.
        /// The overlay panel should match the OLV's visibility.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objectListView_VisibleChanged(object sender, EventArgs e) {
            if (this.objectListView.Visible)
                this.ShowGlass();
            else
                this.HideGlass();
        }

        /// <summary>
        /// The owning form has moved. Move the overlay panel too.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Owner_LocationChanged(object sender, EventArgs e) {
            this.RecalculateBounds();
        }

        #endregion

        #region Implementation

        protected override void OnPaint(PaintEventArgs e) {
            if (this.objectListView == null)
                return;

            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.DrawRectangle(new Pen(Color.Green, 4.0f), this.ClientRectangle);
            this.objectListView.DrawBackgroundOverlays(g);
        }

        protected void RecalculateBounds() {
            if (!this.isGlassShown)
                return;

            Rectangle rect = this.objectListView.ClientRectangle;
            rect.X = 0;
            rect.Y = 0;
            this.Bounds = this.objectListView.RectangleToScreen(rect);
        }

        internal void UpdateTransparency() {
            this.Opacity = this.objectListView.OverlayTransparency / 255.0f;
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

        #endregion

        #region Private variables

        ObjectListView objectListView;
        private bool isDuringResizeSequence;
        private bool isGlassShown;
        private bool wasGlassShownBeforeResize;

        #endregion

    }
}
