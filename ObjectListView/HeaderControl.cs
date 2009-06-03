/*
 * HeaderControl - A limited implementation of HeaderControl
 *
 * Author: Phillip Piper
 * Date: 25/11/2008 17:15 
 *
 * Change log:
 * v2.2
 * 2009-06-01  JPP  - Use ToolTipControl
 * 2009-05-10  JPP  - Removed all unsafe code
 * 2008-11-25  JPP  - Initial version
 *
 * TO DO:
 *
 * Copyright (C) 2006-2009 Phillip Piper
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
    /// <summary>
    /// Class used to capture window messages for the header of the list view
    /// control.
    /// </summary>
    public class HeaderControl : NativeWindow
    {
        public HeaderControl(ObjectListView olv) {
            this.ListView = olv;
            this.AssignHandle(NativeMethods.GetHeaderControl(olv));
        }

        #region Properties

        protected ObjectListView ListView {
            get { return this.listView; }
            set { this.listView = value; }
        }
        private ObjectListView listView;

        /// <summary>
        /// Get or set the ToolTip that shows tips for the header
        /// </summary>
        public ToolTipControl ToolTip {
            get {
                if (this.toolTip == null) {
                    this.CreateToolTip();
                }
                return this.toolTip;
            }
            protected set { this.toolTip = value; }
        }
        private ToolTipControl toolTip;

        /// <summary>
        /// Return the Windows handle behind this control
        /// </summary>
        /// <remarks>
        /// When an ObjectListView is initialized as part of a UserControl, the
        /// GetHeaderControl() method returns 0 until the UserControl is
        /// completely initialized. So the AssignHandle() call in the constructor
        /// doesn't work. So we override the Handle property so value is always
        /// current.
        /// </remarks>
        public new IntPtr Handle {
            get { return NativeMethods.GetHeaderControl(this.ListView); }
        }
        //TODO: The Handle property may no longer be necessary. CHECK! 2008/11/28
        
        #endregion

        #region Tooltip

        protected virtual void CreateToolTip() {
            this.ToolTip = new ToolTipControl();
            this.ToolTip.Create(this.Handle);
            this.ToolTip.AddTool(this);
            this.ToolTip.Showing += new EventHandler<ToolTipShowingEventArgs>(this.ListView.headerToolTip_Showing);
        }

        #endregion

        #region Windows messaging

        protected override void WndProc(ref Message m) {
            const int WM_SETCURSOR = 0x20;
            const int WM_NOTIFY = 0x4E;
            const int WM_MOUSEMOVE = 0x200;

            switch (m.Msg) {
                case WM_SETCURSOR:
                    if (this.IsCursorOverLockedDivider) {
                        m.Result = (IntPtr)1;	// Don't change the cursor
                        return;
                    }
                    break;

                case WM_NOTIFY:
                    if (!this.HandleNotify(ref m))
                        return;
                    break;

                case WM_MOUSEMOVE:
                    this.HandleMouseMove(ref m);
                    break;
            }

            base.WndProc(ref m);
        }

        protected void HandleMouseMove(ref Message m) {
            int columnIndex = this.ColumnIndexUnderCursor;

            // If the mouse has moved to a different header, pop the current tip (if any)
            if (columnIndex != this.columnShowingTip) {
                this.ToolTip.PopToolTip(this);
                this.columnShowingTip = columnIndex;
            }
        }
        private int columnShowingTip = -1;

        protected bool HandleNotify(ref Message m) {
            // Can this ever happen? JPP 2009-05-22
            if (m.LParam == IntPtr.Zero)
                return false;

            NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
            switch (nmhdr.code) {

                case ToolTipControl.TTN_SHOW:
                    //System.Diagnostics.Debug.WriteLine("hdr TTN_SHOW");
                    System.Diagnostics.Trace.Assert(this.ToolTip.Handle == nmhdr.hwndFrom);
                    return this.ToolTip.HandleShow(ref m);

                case ToolTipControl.TTN_POP:
                    //System.Diagnostics.Debug.WriteLine("hdr TTN_POP");
                    System.Diagnostics.Trace.Assert(this.ToolTip.Handle == nmhdr.hwndFrom);
                    return this.ToolTip.HandlePop(ref m);

                case ToolTipControl.TTN_GETDISPINFO:
                    //System.Diagnostics.Debug.WriteLine("hdr TTN_GETDISPINFO");
                    System.Diagnostics.Trace.Assert(this.ToolTip.Handle == nmhdr.hwndFrom);
                    return this.ToolTip.HandleGetDispInfo(ref m);
            }

            return false;
        }
        
        #endregion

        #region Calculation properties

        protected bool IsCursorOverLockedDivider {
            get {
                Point pt = this.ListView.PointToClient(Cursor.Position);
                pt.X += NativeMethods.GetScrollPosition(this.ListView, true);
                int dividerIndex = NativeMethods.GetDividerUnderPoint(this.Handle, pt);
                if (dividerIndex >= 0 && dividerIndex < this.ListView.Columns.Count) {
                    OLVColumn column = this.ListView.GetColumn(dividerIndex);
                    return column.IsFixedWidth || column.FillsFreeSpace;
                } else
                    return false;
            }
        }

        /// <summary>
        /// Return the index of the column under the current cursor position,
        /// or -1 if the cursor is not over a column
        /// </summary>
        /// <returns>Index of the column under the cursor, or -1</returns>
        public int ColumnIndexUnderCursor {
            get {
                Point pt = this.ListView.PointToClient(Cursor.Position);
                pt.X += NativeMethods.GetScrollPosition(this.ListView, true);
                return NativeMethods.GetColumnUnderPoint(this.Handle, pt);
            }
        }

        #endregion
    }
}