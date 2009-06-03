/*
 * ToolTipControl - A limited wrapper around a Windows tooltip control
 *
 * For some reason, the ToolTip class in the .NET framework is implemented in a significantly
 * different manner to other controls. For our purposes, the worst of these problems
 * is that we cannot get the Handle, so we cannot send Windows level messages to the control.
 * 
 * Author: Phillip Piper
 * Date: 2009-05-17 7:22PM 
 *
 * Change log:
 * 2009-05-17  JPP  - Initial version
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// A limited wrapper around a Windows tooltip window.
    /// </summary>
    public class ToolTipControl : NativeWindow
    {
        #region Constants

        public enum StandardIcons
        {
            None = 0,
            Info = 1,
            Warning = 2,
            Error = 3,

            // These values only work on Vista and later
            InfoLarge = 4,
            WarningLarge = 5,
            ErrorLarge = 6
        }

        const int GWL_STYLE = -16;
        const int WM_GETFONT = 0x31;
        const int WM_SETFONT = 0x30;
        const int WS_BORDER = 0x800000;
        const int WS_EX_TOPMOST = 8;

        const int TTM_ADDTOOL = 0x432;
        const int TTM_DELTOOL = 0x433;
        const int TTM_GETCURRENTTOOL = 0x400 + 59;
        const int TTM_GETTIPBKCOLOR = 0x400 + 22;
        const int TTM_GETTIPTEXTCOLOR = 0x400 + 23;
        const int TTM_GETDELAYTIME = 0x400 + 21;
        const int TTM_NEWTOOLRECT = 0x400 + 52;
        const int TTM_POP = 0x41c;
        const int TTM_SETDELAYTIME = 0x400 + 3;
        const int TTM_SETMAXTIPWIDTH = 0x400 + 24;
        const int TTM_SETTIPBKCOLOR = 0x400 + 19;
        const int TTM_SETTIPTEXTCOLOR = 0x400 + 20;
        const int TTM_SETTITLE = 0x400 + 33;
        const int TTM_SETTOOLINFO = 0x400 + 54;

        const int TTF_IDISHWND = 1;
        //const int TTF_ABSOLUTE = 0x80;
        const int TTF_CENTERTIP = 2;
        const int TTF_RTLREADING = 4;
        const int TTF_SUBCLASS = 0x10;
        //const int TTF_TRACK = 0x20;
        //const int TTF_TRANSPARENT = 0x100;
        const int TTF_PARSELINKS = 0x1000;

        const int TTS_NOPREFIX = 2;
        const int TTS_BALLOON = 0x40;
        const int TTS_USEVISUALSTYLE = 0x100;

        const int TTN_FIRST = -520;
        public const int TTN_SHOW = (TTN_FIRST - 1);
        public const int TTN_POP = (TTN_FIRST - 2);
        public const int TTN_LINKCLICK = (TTN_FIRST - 3);
        public const int TTN_GETDISPINFO = (TTN_FIRST - 10);

        const int TTDT_AUTOMATIC = 0;
        const int TTDT_RESHOW = 1;
        const int TTDT_AUTOPOP = 2;
        const int TTDT_INITIAL = 3;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set if the style of the tooltip control
        /// </summary>
        internal int WindowStyle {
            get {
                return (int)NativeMethods.GetWindowLong(this.Handle, GWL_STYLE);
            }
            set {
                NativeMethods.SetWindowLong(this.Handle, GWL_STYLE, value);
            }
        }

        /// <summary>
        /// Get or set if the tooltip should be shown as a ballon
        /// </summary>
        public bool IsBalloon {
            get {
                return (this.WindowStyle & TTS_BALLOON) == TTS_BALLOON;
            }
            set {
                if (this.IsBalloon == value)
                    return;

                int windowStyle = this.WindowStyle;
                if (value) {
                    windowStyle |= (TTS_BALLOON | TTS_USEVISUALSTYLE);
                    windowStyle &= ~WS_BORDER; // makes the ballon look wrong
                } else {
                    windowStyle &= ~(TTS_BALLOON | TTS_USEVISUALSTYLE);
                    if (this.hasBorder)
                        windowStyle |= WS_BORDER;
                    else
                        windowStyle &= ~WS_BORDER;
                }
                this.WindowStyle = windowStyle;
            }
        }

        /// <summary>
        /// Get or set if the tooltip should be shown as a ballon
        /// </summary>
        public bool HasBorder {
            get {
                return this.hasBorder;
            }
            set {
                if (this.hasBorder == value)
                    return;

                if (value) {
                    this.WindowStyle |= WS_BORDER;
                } else {
                    this.WindowStyle &= ~WS_BORDER;
                }
            }
        }
        private bool hasBorder = true;

        /// <summary>
        /// Get or set the background color of the tooltip
        /// </summary>
        public Color BackColor {
            get {
                int color = (int)NativeMethods.SendMessage(this.Handle, TTM_GETTIPBKCOLOR, 0, 0);
                return ColorTranslator.FromWin32(color);
            }
            set {
                int color = ColorTranslator.ToWin32(value);
                NativeMethods.SendMessage(this.Handle, TTM_SETTIPBKCOLOR, color, 0);
            }
        }

        /// <summary>
        /// Get or set the color of the text and border on the tooltip.
        /// </summary>
        public Color ForeColor {
            get {
                int color = (int)NativeMethods.SendMessage(this.Handle, TTM_GETTIPTEXTCOLOR, 0, 0);
                return ColorTranslator.FromWin32(color);
            }
            set {
                int color = ColorTranslator.ToWin32(value);
                NativeMethods.SendMessage(this.Handle, TTM_SETTIPTEXTCOLOR, color, 0);
            }
        }

        /// <summary>
        /// Get or set the title that will be shown on the tooltip.
        /// </summary>
        public string Title {
            get {
                return this.title;
            }
            set {
                if (String.IsNullOrEmpty(value))
                    this.title = String.Empty;
                else
                    if (value.Length >= 100)
                        this.title = value.Substring(0, 99);
                    else
                        this.title = value;
                NativeMethods.SendMessageString(this.Handle, TTM_SETTITLE, (int)this.standardIcon, this.title);
            }
        }
        private string title;

        /// <summary>
        /// Get or set the icon that will be shown on the tooltip.
        /// </summary>
        public StandardIcons StandardIcon {
            get {
                return this.standardIcon;
            }
            set {
                this.standardIcon = value;
                NativeMethods.SendMessageString(this.Handle, TTM_SETTITLE, (int)this.standardIcon, this.title);
            }
        }
        private StandardIcons standardIcon;

        /// <summary>
        /// Gets or sets the font that will be used to draw this control.
        /// is still.
        /// </summary>
        /// <remarks>Setting this to null reverts to the default font.</remarks>
        public Font Font {
            get {
                IntPtr hfont = NativeMethods.SendMessage(this.Handle, WM_GETFONT, 0, 0);
                if (hfont == IntPtr.Zero)
                    return Control.DefaultFont;
                else
                    return Font.FromHfont(hfont);
            }
            set {
                Font newFont = value ?? Control.DefaultFont;
                if (newFont == this.font)
                    return;

                this.font = newFont;
                IntPtr hfont = this.font.ToHfont(); // THINK: When should we delete this hfont?
                NativeMethods.SendMessage(this.Handle, WM_SETFONT, hfont, 0);
            }
        }
        private Font font;

        /// <summary>
        /// Gets or sets how many milliseconds the tooltip will remain visible while the mouse
        /// is still.
        /// </summary>
        public int AutoPopDelay {
            get { return this.GetDelayTime(TTDT_AUTOPOP); }
            set { this.SetDelayTime(TTDT_AUTOPOP, value); }
        }

        /// <summary>
        /// Gets or sets how many milliseconds the mouse must be still before the tooltip is shown.
        /// </summary>
        public int InitialDelay {
            get { return this.GetDelayTime(TTDT_INITIAL); }
            set { this.SetDelayTime(TTDT_INITIAL, value); }
        }

        /// <summary>
        /// Gets or sets how many milliseconds the mouse must be still before the tooltip is shown again.
        /// </summary>
        public int ReshowDelay {
            get { return this.GetDelayTime(TTDT_RESHOW); }
            set { this.SetDelayTime(TTDT_RESHOW, value); }
        }

        private int GetDelayTime(int which) {
            return (int)NativeMethods.SendMessage(this.Handle, TTM_GETDELAYTIME, which, 0);
        }

        private void SetDelayTime(int which, int value) {
            NativeMethods.SendMessage(this.Handle, TTM_SETDELAYTIME, which, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Create the underlying control.
        /// </summary>
        /// <param name="parentHandle">The parent of the tooltip</param>
        /// <remarks>This does nothing if the control has already been created</remarks>
        public void Create(IntPtr parentHandle) {
            if (this.Handle != IntPtr.Zero)
                return;

            CreateParams cp = new CreateParams();
            cp.ClassName = "tooltips_class32";
            cp.Style = TTS_NOPREFIX;
            cp.ExStyle = WS_EX_TOPMOST;
            cp.Parent = parentHandle;
            this.CreateHandle(cp);
            
            // Ensure that multiline tooltips work correctly
            this.SetMaxWidth();
        }

        /// <summary>
        /// Take a copy of the current settings and restore them when the 
        /// tooltip is poppped.
        /// </summary>
        /// <remarks>
        /// This call cannot be nested. Subsequent calls to this method will be ignored
        /// until PopSettings() is called.
        /// </remarks>
        public void PushSettings() {
            // Ignore any nested calls
            if (this.settings != null)
                return;
            this.settings = new Hashtable();
            this.settings["IsBalloon"] = this.IsBalloon;
            this.settings["HasBorder"] = this.HasBorder;
            this.settings["BackColor"] = this.BackColor;
            this.settings["ForeColor"] = this.ForeColor;
            this.settings["Title"] = this.Title;
            this.settings["StandardIcon"] = this.StandardIcon;
            this.settings["AutoPopDelay"] = this.AutoPopDelay;
            this.settings["InitialDelay"] = this.InitialDelay;
            this.settings["ReshowDelay"] = this.ReshowDelay;
            this.settings["Font"] = this.Font;
        }
        private Hashtable settings;

        /// <summary>
        /// Restore the settings of the tooltip as they were when PushSettings()
        /// was last called.
        /// </summary>
        public void PopSettings() {
            if (this.settings == null)
                return;

            this.IsBalloon = (bool)this.settings["IsBalloon"];
            this.HasBorder = (bool)this.settings["HasBorder"];
            this.BackColor = (Color)this.settings["BackColor"];
            this.ForeColor = (Color)this.settings["ForeColor"];
            this.Title = (string)this.settings["Title"];
            this.StandardIcon = (StandardIcons)this.settings["StandardIcon"];
            this.AutoPopDelay = (int)this.settings["AutoPopDelay"];
            this.InitialDelay = (int)this.settings["InitialDelay"];
            this.ReshowDelay = (int)this.settings["ReshowDelay"];
            this.Font = (Font)this.settings["Font"];

            this.settings = null;
        }

        /// <summary>
        /// Add the given window to those for whom this tooltip will show tips
        /// </summary>
        /// <param name="window">The window</param>
        public void AddTool(IWin32Window window) {
            NativeMethods.TOOLINFO lParam = this.MakeToolInfoStruct(window);
            NativeMethods.SendMessageTOOLINFO(this.Handle, TTM_ADDTOOL, 0, lParam);
        }

        /// <summary>
        /// Hide any currently visible tooltip
        /// </summary>
        /// <param name="window"></param>
        public void PopToolTip(IWin32Window window) {
            NativeMethods.SendMessage(this.Handle, TTM_POP, 0, 0);
        }

        /// <summary>
        /// Remove the given window from those managed by this tooltip
        /// </summary>
        /// <param name="window"></param>
        public void RemoveToolTip(IWin32Window window) {
            NativeMethods.TOOLINFO lParam = this.MakeToolInfoStruct(window);
            NativeMethods.SendMessageTOOLINFO(this.Handle, TTM_DELTOOL, 0, lParam);
        }

        /// <summary>
        /// Remove the given window from those managed by this tooltip
        /// </summary>
        /// <param name="window"></param>
        //public void ChangeCurrentToolRect(Rectangle r) {
        //    NativeMethods.TOOLINFO tool = new NativeMethods.TOOLINFO();
        //    IntPtr result = NativeMethods.SendMessageTOOLINFO(this.Handle, TTM_GETCURRENTTOOL, 0, tool);
        //    if (result != IntPtr.Zero) {
        //        System.Diagnostics.Debug.WriteLine(String.Format("{0}, {1}, {2}, {3}",
        //        tool.rect.left,
        //        tool.rect.top,
        //        tool.rect.right,
        //        tool.rect.bottom));
        //        //result = NativeMethods.SendMessageTOOLINFO(this.Handle, TTM_NEWTOOLRECT, 0, tool);
        //    }
        //}

        /// <summary>
        /// Set the maximum width of a tooltip string.
        /// </summary>
        public void SetMaxWidth() {
            this.SetMaxWidth(SystemInformation.MaxWindowTrackSize.Width);
        }

        /// <summary>
        /// Set the maximum width of a tooltip string.
        /// </summary>
        /// <remarks>Setting this ensures that line breaks in the tooltip are honoured.</remarks>
        public void SetMaxWidth(int maxWidth) {
            NativeMethods.SendMessage(this.Handle, TTM_SETMAXTIPWIDTH, 0, maxWidth);
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Make a TOOLINFO structure for the given window
        /// </summary>
        /// <param name="window"></param>
        /// <returns>A filled in TOOLINFO</returns>
        private NativeMethods.TOOLINFO MakeToolInfoStruct(IWin32Window window) {

            NativeMethods.TOOLINFO toolinfo_tooltip = new NativeMethods.TOOLINFO();
            toolinfo_tooltip.hwnd = window.Handle;
            toolinfo_tooltip.uFlags = TTF_IDISHWND | TTF_SUBCLASS | TTF_PARSELINKS;
            toolinfo_tooltip.uId = window.Handle;
            toolinfo_tooltip.lpszText = (IntPtr)(-1); // LPSTR_TEXTCALLBACK

            return toolinfo_tooltip;
        }

        /// <summary>
        /// Handle a WmNotify message
        /// </summary>
        /// <param name="msg">The msg</param>
        /// <returns>True if the message has been handled</returns>
        protected virtual bool HandleNotify(ref Message msg) {

            NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)msg.GetLParam(typeof(NativeMethods.NMHEADER));
            switch (nmheader.nhdr.code) {
                case TTN_SHOW:
                    System.Diagnostics.Debug.WriteLine("in TTN_SHOW");
                    break;
                case TTN_POP:
                    System.Diagnostics.Debug.WriteLine("in TTN_POP");
                    break;
                case TTN_LINKCLICK:
                    System.Diagnostics.Debug.WriteLine("in TTN_LINKCLICK");
                    break;
                case TTN_GETDISPINFO:
                    System.Diagnostics.Debug.WriteLine("in TTN_GETDISPINFO");
                    break;
            }

            return false;
        }

        /// <summary>
        /// Handle a get display info message
        /// </summary>
        /// <param name="msg">The msg</param>
        /// <returns>True if the message has been handled</returns>
        public virtual bool HandleGetDispInfo(ref Message msg) {
            this.SetMaxWidth();
            ToolTipShowingEventArgs args = new ToolTipShowingEventArgs();
            args.ToolTipControl = this;
            this.OnShowing(args);
            if (String.IsNullOrEmpty(args.Text))
                return false;

            this.ApplyEventFormatting(args);
            NativeMethods.TOOLTIPTEXT ttt = (NativeMethods.TOOLTIPTEXT)msg.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
            ttt.lpszText = args.Text;
            if (args.RightToLeft == RightToLeft.Yes)
                ttt.uFlags |= TTF_RTLREADING;
            Marshal.StructureToPtr(ttt, msg.LParam, false);

            return true;
        }

        private void ApplyEventFormatting(ToolTipShowingEventArgs args) {
            if (!args.IsBalloon.HasValue &&
                !args.BackColor.HasValue &&
                !args.ForeColor.HasValue &&
                args.Title == null &&
                !args.StandardIcon.HasValue &&
                !args.AutoPopDelay.HasValue &&
                args.Font == null)
                return;

            this.PushSettings();
            if (args.IsBalloon.HasValue)
                this.IsBalloon = args.IsBalloon.Value;
            if (args.BackColor.HasValue)
                this.BackColor = args.BackColor.Value;
            if (args.ForeColor.HasValue)
                this.ForeColor = args.ForeColor.Value;
            if (args.StandardIcon.HasValue)
                this.StandardIcon = args.StandardIcon.Value;
            if (args.AutoPopDelay.HasValue)
                this.AutoPopDelay = args.AutoPopDelay.Value;
            if (args.Font != null)
                this.Font = args.Font;
            if (args.Title != null)
                this.Title = args.Title;
        }

        /// <summary>
        /// Handle a TTN_LINKCLICK message
        /// </summary>
        /// <param name="msg">The msg</param>
        /// <returns>True if the message has been handled</returns>
        public virtual bool HandleLinkClick(ref Message msg) {
            return false;
        }

        /// <summary>
        /// Handle a TTN_POP message
        /// </summary>
        /// <param name="msg">The msg</param>
        /// <returns>True if the message has been handled</returns>
        public virtual bool HandlePop(ref Message msg) {
            base.WndProc(ref msg);
            this.PopSettings();
            return true;
        }

        /// <summary>
        /// Handle a TTN_SHOW message
        /// </summary>
        /// <param name="msg">The msg</param>
        /// <returns>True if the message has been handled</returns>
        public virtual bool HandleShow(ref Message msg) {
            return false;
        }

        /// <summary>
        /// Handle a reflected notify message
        /// </summary>
        /// <param name="msg">The msg</param>
        /// <returns>True if the message has been handled</returns>
        protected virtual bool HandleReflectNotify(ref Message msg) {

            NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)msg.GetLParam(typeof(NativeMethods.NMHEADER));
            switch (nmheader.nhdr.code) {
                case TTN_SHOW:
                    //System.Diagnostics.Debug.WriteLine("reflect TTN_SHOW");
                    if (this.HandleShow(ref msg))
                        return true;
                    break;
                case TTN_POP:
                    //System.Diagnostics.Debug.WriteLine("reflect TTN_POP");
                    if (this.HandlePop(ref msg))
                        return true;
                    break;
                case TTN_LINKCLICK:
                    //System.Diagnostics.Debug.WriteLine("reflect TTN_LINKCLICK");
                    if (this.HandleLinkClick(ref msg))
                        return true;
                    break;
                case TTN_GETDISPINFO:
                    //System.Diagnostics.Debug.WriteLine("reflect TTN_GETDISPINFO");
                    if (this.HandleGetDispInfo(ref msg))
                        return true;
                    break;
            }

            return false;
        }

        /// <summary>
        /// Mess with the basic message pump of the tooltip
        /// </summary>
        /// <param name="msg"></param>
        override protected void WndProc(ref Message msg) {
            //System.Diagnostics.Debug.WriteLine(String.Format("xx {0:x}", msg.Msg));
            switch (msg.Msg) {
                case 0x4E: // WM_NOTIFY
                    if (!this.HandleNotify(ref msg))
                        return;
                    break;

                case 0x204E: // WM_REFLECT_NOTIFY
                    if (!this.HandleReflectNotify(ref msg))
                        return;
                    break;
            }

            base.WndProc(ref msg);
        }

        #endregion

        #region Events

        public event EventHandler<ToolTipShowingEventArgs> Showing;
        public event EventHandler<EventArgs> Pop;

        protected virtual void OnShowing(ToolTipShowingEventArgs e) {
            if (this.Showing != null)
                this.Showing(this, e);
        }

        protected virtual void OnPop(EventArgs e) {
            if (this.Pop != null)
                this.Pop(this, e);
        }

        #endregion
    }

    public class ToolTipShowingEventArgs : EventArgs
    {
        public ObjectListView ListView;
        public object Model;
        public int RowIndex;
        public int ColumnIndex;
        public OLVColumn Column;
        public Point Location;
        public ToolTipControl ToolTipControl;

        public string Text;
        public RightToLeft RightToLeft;

        public bool? IsBalloon;
        public Color? BackColor;
        public Color? ForeColor;
        public string Title;
        public ToolTipControl.StandardIcons? StandardIcon;
        public int? AutoPopDelay;
        public Font Font;
    }
}