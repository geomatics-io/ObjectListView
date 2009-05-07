/*
 * HeaderControl - A limited implementation of HeaderControl
 *
 * Author: Phillip Piper
 * Date: 25/11/2008 17:15 
 *
 * Change log:
 * 2008-11-25  JPP  Initial version
 *
 * TO DO:
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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Class used to capture window messages for the header of the list view
    /// control.
    /// </summary>
    public class HeaderControl : NativeWindow
    {
        private ObjectListView parentListView;
        private MyToolTip tooltip;

        public HeaderControl(ObjectListView olv) {
            this.parentListView = olv;
            this.AssignHandle(NativeMethods.GetHeaderControl(olv));
            this.tooltip = new MyToolTip();
            this.tooltip.AddTool(this);
        }

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
            get { return NativeMethods.GetHeaderControl(this.parentListView); }
        }
        //TODO: The Handle property may no longer be necessary. CHECK! 2008/11/28

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
                this.tooltip.PopToolTip(this);
                this.columnShowingTip = columnIndex;
            }
        }
        private int columnShowingTip = -1;

        unsafe protected bool HandleNotify(ref Message m) {
            //const int TTN_SHOW = -521;
            //const int TTN_POP = -522;
            const int TTN_GETDISPINFO = -530;

            if (m.LParam == IntPtr.Zero)
                return false;

            NativeMethods.NMHDR* lParam = (NativeMethods.NMHDR*)m.LParam;
            switch (lParam->code) {
                case TTN_GETDISPINFO:
                    return HandleGetDispInfo(ref m);
            }

            return false;
        }

        protected bool HandleGetDispInfo(ref Message m) {
            int columnIndex = this.ColumnIndexUnderCursor;
            if (columnIndex < 0)
                return false;

            string text = this.parentListView.GetHeaderToolTip(columnIndex);
            if (String.IsNullOrEmpty(text))
                return false;

            NativeMethods.TOOLTIPTEXT tooltipText = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
            tooltipText.lpszText = text;
            if (this.parentListView.RightToLeft == RightToLeft.Yes)
                tooltipText.uFlags |= 4;

            Marshal.StructureToPtr(tooltipText, m.LParam, false);
            return true;
        }

        protected bool IsCursorOverLockedDivider {
            get {
                Point pt = this.parentListView.PointToClient(Cursor.Position);
                pt.X += NativeMethods.GetScrollPosition(this.parentListView, true);
                int dividerIndex = NativeMethods.GetDividerUnderPoint(this.Handle, pt);
                if (dividerIndex >= 0 && dividerIndex < this.parentListView.Columns.Count) {
                    OLVColumn column = this.parentListView.GetColumn(dividerIndex);
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
                Point pt = this.parentListView.PointToClient(Cursor.Position);
                pt.X += NativeMethods.GetScrollPosition(this.parentListView, true);
                return NativeMethods.GetColumnUnderPoint(this.Handle, pt);
            }
        }
    }

    /// <summary>
    /// A limited wrapper around a Windows tooltip window.
    /// </summary>
    public class MyToolTip
    {
        public MyToolTip() {
            this.window = new MyToolTipNativeWindow(this);
        }
        private MyToolTipNativeWindow window;

        public IntPtr Handle {
            get {
                if (!this.IsHandleCreated) {
                    this.CreateHandle();
                }
                return this.window.Handle;
            }
        }

        public void AddTool(IWin32Window window) {
            const int TTM_ADDTOOL = 0x432;

            NativeMethods.SendMessage(this.Handle, 0x418, 0, SystemInformation.MaxWindowTrackSize.Width);

            NativeMethods.TOOLINFO lParam = this.GetTOOLINFO(window);
            IntPtr result = NativeMethods.SendMessageTOOLINFO(this.Handle, TTM_ADDTOOL, 0, lParam);
        }

        public void PopToolTip(IWin32Window window) {
            const int TTM_POP = 0x41c;
            NativeMethods.SendMessage(this.Handle, TTM_POP, 0, 0);
        }

        public void RemoveToolTip(IWin32Window window) {
            const int TTM_DELTOOL = 0x433;
            NativeMethods.TOOLINFO lParam = this.GetTOOLINFO(window);
            NativeMethods.SendMessageTOOLINFO(this.Handle, TTM_DELTOOL, 0, lParam);
        }

        internal NativeMethods.TOOLINFO GetTOOLINFO(IWin32Window window) {
            const int TTF_IDISHWND = 1;
            //const int TTF_ABSOLUTE = 0x80;
            //const int TTF_CENTERTIP = 2;
            const int TTF_SUBCLASS = 0x10;
            //const int TTF_TRACK = 0x20;
            //const int TTF_TRANSPARENT = 0x100;

            NativeMethods.TOOLINFO toolinfo_tooltip = new NativeMethods.TOOLINFO();
            toolinfo_tooltip.hwnd = window.Handle;
            toolinfo_tooltip.uFlags = TTF_IDISHWND | TTF_SUBCLASS;
            toolinfo_tooltip.uId = window.Handle;
            toolinfo_tooltip.lpszText = (IntPtr)(-1); // LPSTR_TEXTCALLBACK

            return toolinfo_tooltip;
        }

        protected void CreateHandle() {
            if (this.IsHandleCreated)
                return;

            const int TTS_BALLOON = 0x40;
            const int TTS_NOPREFIX = 2;

            CreateParams p = new CreateParams();
            p.ClassName = "tooltips_class32";
            p.Style = TTS_NOPREFIX;
            p.Style = TTS_NOPREFIX | TTS_BALLOON;
            this.window.CreateHandle(p);
        }

        protected bool IsHandleCreated {
            get {
                return (this.window != null && this.window.Handle != IntPtr.Zero);
            }
        }

        public void WndProc(ref Message msg) {
            //System.Diagnostics.Debug.WriteLine(String.Format("xx {0:x}", m.Msg));
            //switch (m.Msg) {
            //    case 0x4E: // WM_NOTIFY
            //        if (!this.HandleNotify(ref m))
            //            return;
            //        break;
            //    case 0x204E: // WM_REFLECT_NOTIFY
            //        if (!this.HandleNotify(ref m))
            //            return;
            //        break;
            //}
            this.window.DefWndProc(ref msg);
        }

        internal class MyToolTipNativeWindow : NativeWindow
        {
            public MyToolTipNativeWindow(MyToolTip control) {
                this.control = control;
            }

            protected override void WndProc(ref Message m) {
                if (this.control != null) {
                    this.control.WndProc(ref m);
                }
            }

            private MyToolTip control;
        }
    }
}