using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Wrapper for all native method calls on ListView controls
    /// </summary>
    internal class NativeMethods
    {
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SCROLL = LVM_FIRST + 20;
        private const int LVM_GETHEADER = LVM_FIRST + 31;
        private const int LVM_GETCOUNTPERPAGE = LVM_FIRST + 40;
        private const int LVM_SETITEMSTATE = LVM_FIRST + 43;
        private const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        private const int LVM_SETITEM = LVM_FIRST + 76;
        private const int LVM_GETCOLUMN = LVM_FIRST + 95;
        private const int LVM_SETCOLUMN = LVM_FIRST + 96;

        private const int LVS_EX_SUBITEMIMAGES = 0x0002;

        private const int LVIF_TEXT = 0x0001;
        private const int LVIF_IMAGE = 0x0002;
        private const int LVIF_PARAM = 0x0004;
        private const int LVIF_STATE = 0x0008;
        private const int LVIF_INDENT = 0x0010;
        private const int LVIF_NORECOMPUTE = 0x0800;

        private const int LVCF_FMT = 0x0001;
        private const int LVCF_WIDTH = 0x0002;
        private const int LVCF_TEXT = 0x0004;
        private const int LVCF_SUBITEM = 0x0008;
        private const int LVCF_IMAGE = 0x0010;
        private const int LVCF_ORDER = 0x0020;
        private const int LVCFMT_LEFT = 0x0000;
        private const int LVCFMT_RIGHT = 0x0001;
        private const int LVCFMT_CENTER = 0x0002;
        private const int LVCFMT_JUSTIFYMASK = 0x0003;

        private const int LVCFMT_IMAGE = 0x0800;
        private const int LVCFMT_BITMAP_ON_RIGHT = 0x1000;
        private const int LVCFMT_COL_HAS_IMAGES = 0x8000;

        private const int HDM_FIRST = 0x1200;
        private const int HDM_HITTEST = HDM_FIRST + 6;
        private const int HDM_GETITEM = HDM_FIRST + 11;
        private const int HDM_SETITEM = HDM_FIRST + 12;

        private const int HDI_WIDTH = 0x0001;
        private const int HDI_TEXT = 0x0002;
        private const int HDI_FORMAT = 0x0004;
        private const int HDI_BITMAP = 0x0010;
        private const int HDI_IMAGE = 0x0020;

        private const int HDF_LEFT = 0x0000;
        private const int HDF_RIGHT = 0x0001;
        private const int HDF_CENTER = 0x0002;
        private const int HDF_JUSTIFYMASK = 0x0003;
        private const int HDF_RTLREADING = 0x0004;
        private const int HDF_STRING = 0x4000;
        private const int HDF_BITMAP = 0x2000;
        private const int HDF_BITMAP_ON_RIGHT = 0x1000;
        private const int HDF_IMAGE = 0x0800;
        private const int HDF_SORTUP = 0x0400;
        private const int HDF_SORTDOWN = 0x0200;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_CTL = 2;
        private const int SB_BOTH = 3;

        private const int SIF_RANGE = 0x0001;
        private const int SIF_PAGE = 0x0002;
        private const int SIF_POS = 0x0004;
        private const int SIF_DISABLENOSCROLL = 0x0008;
        private const int SIF_TRACKPOS = 0x0010;
        private const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);

        private const int ILD_NORMAL = 0x00000000;
        private const int ILD_TRANSPARENT = 0x00000001;
        private const int ILD_MASK = 0x00000010;
        private const int ILD_IMAGE = 0x00000020;
        private const int ILD_BLEND25 = 0x00000002;
        private const int ILD_BLEND50 = 0x00000004;

        [StructLayout(LayoutKind.Sequential)]
        public struct HDITEM
        {
            public int mask;
            public int cxy;
            public IntPtr pszText;
            public IntPtr hbm;
            public int cchTextMax;
            public int fmt;
            public IntPtr lParam;
            public int iImage;
            public int iOrder;
            //if (_WIN32_IE >= 0x0500)
            public int type;
            public IntPtr pvFilter;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class HDHITTESTINFO
        {
            public int pt_x;
            public int pt_y;
            public int flags;
            public int iItem;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVBKIMAGE
        {
            public int ulFlags;
            public IntPtr hBmp;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszImage;
            public int cchImageMax;
            public int xOffset;
            public int yOffset;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVCOLUMN
        {
            public int mask;
            public int fmt;
            public int cx;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public int cchTextMax;
            public int iSubItem;
            // These are available in Common Controls >= 0x0300
            public int iImage;
            public int iOrder;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVFINDINFO
        {
            public int flags;
            public string psz;
            public IntPtr lParam;
            public int ptX;
            public int ptY;
            public int vkDirection;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVHITTESTINFO
        {
            public int pt_x;
            public int pt_y;
            public int flags;
            public int iItem;
            public int iSubItem;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVITEM
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            // These are available in Common Controls >= 0x0300
            public int iIndent;
            // These are available in Common Controls >= 0x056
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;
        };

        /// <summary>
        /// Notify m header structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMCUSTOMDRAW
        {
            public NativeMethods.NMHDR nmcd;
            public int dwDrawStage;
            public IntPtr hdc;
            public NativeMethods.RECT rc;
            public IntPtr dwItemSpec;
            public int uItemState;
            public IntPtr lItemlParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHEADER
        {
            public NMHDR nhdr;
            public int iItem;
            public int iButton;
            public IntPtr pHDITEM;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMLISTVIEW
        {
            public NativeMethods.NMHDR hdr;
            public int iItem;
            public int iSubItem;
            public int uNewState;
            public int uOldState;
            public int uChanged;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMLVCUSTOMDRAW
        {
            public NativeMethods.NMCUSTOMDRAW nmcd;
            public int clrText;
            public int clrTextBk;
            public int iSubItem;
            public int dwItemType;
            public int clrFace;
            public int iIconEffect;
            public int iIconPhase;
            public int iPartId;
            public int iStateId;
            public NativeMethods.RECT rcText;
            public uint uAlign;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMLVFINDITEM
        {
            public NativeMethods.NMHDR hdr;
            public int iStart;
            public NativeMethods.LVFINDINFO lvfi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SCROLLINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO));
            public int uFlags;
            public IntPtr hwnd;
            public IntPtr uId;
            public NativeMethods.RECT rect;
            public IntPtr hinst = IntPtr.Zero;
            public IntPtr lpszText;
            public IntPtr lParam = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TOOLTIPTEXT
        {
            public NativeMethods.NMHDR hdr;
            public string lpszText;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szText;
            public IntPtr hinst;
            public int uFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        // Various flavours of SendMessage: plain vanilla, and passing references to various structures
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessageLVItem(IntPtr hWnd, int msg, int wParam, ref LVITEM lvi);
        //[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        //private static extern IntPtr SendMessageLVColumn(IntPtr hWnd, int m, int wParam, ref LVCOLUMN lvc);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessageHDItem(IntPtr hWnd, int msg, int wParam, ref HDITEM hdi);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageHDHITTESTINFO(IntPtr hWnd, int Msg, IntPtr wParam, [In, Out] HDHITTESTINFO lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTOOLINFO(IntPtr hWnd, int Msg, int wParam, NativeMethods.TOOLINFO lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageLVBKIMAGE(IntPtr hWnd, int Msg, int wParam, ref NativeMethods.LVBKIMAGE lParam);

        // Entry points used by this code
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(IntPtr hWnd, ref Rectangle r);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, SCROLLINFO si);

        [DllImport("user32.dll", EntryPoint = "GetUpdateRect", CharSet = CharSet.Auto)]
        private static extern int GetUpdateRectInternal(IntPtr hWnd, ref Rectangle r, bool eraseBackground);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        private static extern bool ImageList_Draw(IntPtr himl, int i, IntPtr hdcDst, int x, int y, int fStyle);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern bool SetScrollInfo(IntPtr hWnd, int fnBar, SCROLLINFO si, bool fRedraw);

        [DllImport("user32.dll", EntryPoint = "ValidateRect", CharSet = CharSet.Auto)]
        private static extern IntPtr ValidatedRectInternal(IntPtr hWnd, ref Rectangle r);

        /// <summary>
        /// Put an image under the ListView.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The ListView must have its handle created before calling this.
        /// </para>
        /// <para>
        /// This doesn't work very well.
        /// </para>
        /// </remarks>
        /// <param name="lv"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool SetBackgroundImage(ListView lv, Image image) {
            const int LVBKIF_SOURCE_NONE = 0x00000000;
            //const int LVBKIF_SOURCE_HBITMAP = 0x00000001;
            //const int LVBKIF_SOURCE_URL = 0x00000002;
            //const int LVBKIF_SOURCE_MASK = 0x00000003;
            //const int LVBKIF_STYLE_NORMAL = 0x00000000;
            //const int LVBKIF_STYLE_TILE = 0x00000010;
            //const int LVBKIF_STYLE_MASK = 0x00000010;
            //const int LVBKIF_FLAG_TILEOFFSET = 0x00000100;
            const int LVBKIF_TYPE_WATERMARK = 0x10000000;
            //const int LVBKIF_FLAG_ALPHABLEND = 0x20000000;

            const int LVM_SETBKIMAGE = 0x108A;

            LVBKIMAGE lvbkimage = new LVBKIMAGE();
            Bitmap bm = image as Bitmap;
            if (bm == null)
                lvbkimage.ulFlags = LVBKIF_SOURCE_NONE;
            else {
                lvbkimage.hBmp = bm.GetHbitmap();
                lvbkimage.ulFlags = LVBKIF_TYPE_WATERMARK;

                //lvbkimage.xOffset = 90;
                //lvbkimage.yOffset = 90;

                //string backgroundImageFileName = @"c:\temp\t2.bmp";
                //lvbkimage.pszImage = backgroundImageFileName;
                //lvbkimage.cchImageMax = backgroundImageFileName.Length + 1;
                //lvbkimage.ulFlags = LVBKIF_SOURCE_URL | LVBKIF_STYLE_TILE;
            }

            Application.OleRequired();
            IntPtr result = NativeMethods.SendMessageLVBKIMAGE(lv.Handle, LVM_SETBKIMAGE, 0, ref lvbkimage);
            return (result != IntPtr.Zero);
        }

        public static bool DrawImageList(Graphics g, ImageList il, int index, int x, int y, bool isSelected)
        {
            int flags = ILD_TRANSPARENT;
            if (isSelected)
                flags |= ILD_BLEND25;
            bool result = ImageList_Draw(il.Handle, index, g.GetHdc(), x, y, flags);
            g.ReleaseHdc();
            return result;
        }

        /// <summary>
        /// Make sure the ListView has the extended style that says to display subitem images.
        /// </summary>
        /// <remarks>This method must be called after any .NET call that update the extended styles
        /// since they seem to erase this setting.</remarks>
        /// <param name="list">The listview to send a m to</param>
        public static void ForceSubItemImagesExStyle(ListView list) {
            SendMessage(list.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES);
        }

        /// <summary>
        /// Calculates the number of items that can fit vertically in the visible area of a list-view (which
        /// must be in details or list view.
        /// </summary>
        /// <param name="list">The listView</param>
        /// <returns>Number of visible items per page</returns>
        public static int GetCountPerPage(ListView list)
        {
            return (int)SendMessage(list.Handle, LVM_GETCOUNTPERPAGE, 0, 0);
        }
        /// <summary>
        /// For the given item and subitem, make it display the given image
        /// </summary>
        /// <param name="list">The listview to send a m to</param>
        /// <param name="itemIndex">row number (0 based)</param>
        /// <param name="subItemIndex">subitem (0 is the item itself)</param>
        /// <param name="imageIndex">index into the image list</param>
        public static void SetSubItemImage(ListView list, int itemIndex, int subItemIndex, int imageIndex) {
            LVITEM lvItem = new LVITEM();
            lvItem.mask = LVIF_IMAGE;
            lvItem.iItem = itemIndex;
            lvItem.iSubItem = subItemIndex;
            lvItem.iImage = imageIndex;
            SendMessageLVItem(list.Handle, LVM_SETITEM, 0, ref lvItem);
        }

        /// <summary>
        /// Setup the given column of the listview to show the given image to the right of the text.
        /// If the image index is -1, any previous image is cleared
        /// </summary>
        /// <param name="list">The listview to send a m to</param>
        /// <param name="columnIndex">Index of the column to modifiy</param>
        /// <param name="order"></param>
        /// <param name="imageIndex">Index into the small image list</param>
        public static void SetColumnImage(ListView list, int columnIndex, SortOrder order, int imageIndex) {
            IntPtr hdrCntl = NativeMethods.GetHeaderControl(list);
            if (hdrCntl.ToInt32() == 0)
                return;

            HDITEM item = new HDITEM();
            item.mask = HDI_FORMAT;
            IntPtr result = SendMessageHDItem(hdrCntl, HDM_GETITEM, columnIndex, ref item);

            item.fmt &= ~(HDF_SORTUP | HDF_SORTDOWN | HDF_IMAGE | HDF_BITMAP_ON_RIGHT);

            if (NativeMethods.HasBuiltinSortIndicators()) {
                if (order == SortOrder.Ascending)
                    item.fmt |= HDF_SORTUP;
                if (order == SortOrder.Descending)
                    item.fmt |= HDF_SORTDOWN;
            }
            else {
                item.mask |= HDI_IMAGE;
                item.fmt |= (HDF_IMAGE | HDF_BITMAP_ON_RIGHT);
                item.iImage = imageIndex;
            }

            result = SendMessageHDItem(hdrCntl, HDM_SETITEM, columnIndex, ref item);
        }

        /// <summary>
        /// Does this version of the operating system have builtin sort indicators?
        /// </summary>
        /// <returns>Are there builtin sort indicators</returns>
        /// <remarks>XP and later have these</remarks>
        public static bool HasBuiltinSortIndicators( ) {
            return OSFeature.Feature.GetVersionPresent(OSFeature.Themes) != null;
        }

        /// <summary>
        /// Return the bounds of the update region on the given control.
        /// </summary>
        /// <remarks>The BeginPaint() system call validates the update region, effectively wiping out this information.
        /// So this call has to be made before the BeginPaint() call.</remarks>
        /// <param name="cntl">The control whose update region is be calculated</param>
        /// <returns>A rectangle</returns>
        public static Rectangle GetUpdateRect(Control cntl) {
            Rectangle r = new Rectangle();
            GetUpdateRectInternal(cntl.Handle, ref r, false);
            return r;
        }

        /// <summary>
        /// Validate an area of the given control. A validated area will not be repainted at the next redraw.
        /// </summary>
        /// <param name="cntl">The control to be validated</param>
        /// <param name="r">The area of the control to be validated</param>
        public static void ValidateRect(Control cntl, Rectangle r) {
            ValidatedRectInternal(cntl.Handle, ref r);
        }

        /// <summary>
        /// Select all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be selected</param>
        public static void SelectAllItems(ListView list) {
            NativeMethods.SetItemState(list, -1, 2, 2);
        }

        /// <summary>
        /// Deselect all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be deselected</param>
        public static void DeselectAllItems(ListView list) {
            NativeMethods.SetItemState(list, -1, 2, 0);
        }

        /// <summary>
        /// Set the item state on the given item
        /// </summary>
        /// <param name="list">The listview whose item's state is to be changed</param>
        /// <param name="itemIndex">The index of the item to be changed</param>
        /// <param name="mask">Which bits of the value are to be set?</param>
        /// <param name="value">The value to be set</param>
        public static void SetItemState(ListView list, int itemIndex, int mask, int value) {
            LVITEM lvItem = new LVITEM();
            lvItem.stateMask = mask;
            lvItem.state = value;
            SendMessageLVItem(list.Handle, LVM_SETITEMSTATE, itemIndex, ref lvItem);
        }

        /// <summary>
        /// Scroll the given listview by the given deltas
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns>true if the scroll succeeded</returns>
        public static bool Scroll(ListView list, int dx, int dy) {
            return SendMessage(list.Handle, LVM_SCROLL, dx, dy) != IntPtr.Zero;
        }

        /// <summary>
        /// Return the handle to the header control on the given list
        /// </summary>
        /// <param name="list">The listview whose header control is to be returned</param>
        /// <returns>The handle to the header control</returns>
        public static IntPtr GetHeaderControl(ListView list) {
            return SendMessage(list.Handle, LVM_GETHEADER, 0, 0);
        }

        /// <summary>
        /// Return the index of the divider under the given point. Return -1 if no divider is under the pt
        /// </summary>
        /// <param name="handle">The list we are interested in</param>
        /// <param name="pt">The client co-ords</param>
        /// <returns>The index of the divider under the point, or -1 if no divider is under that point</returns>
        public static int GetDividerUnderPoint(IntPtr handle, Point pt) {
            const int HHT_ONDIVIDER = 4;
            return NativeMethods.HeaderControlHitTest(handle, pt, HHT_ONDIVIDER);
        }

        /// <summary>
        /// Return the index of the column of the header that is under the given point.
        /// Return -1 if no column is under the pt
        /// </summary>
        /// <param name="handle">The list we are interested in</param>
        /// <param name="pt">The client co-ords</param>
        /// <returns>The index of the column under the point, or -1 if no column header is under that point</returns>
        public static int GetColumnUnderPoint(IntPtr handle, Point pt) {
            const int HHT_ONHEADER = 2;
            return NativeMethods.HeaderControlHitTest(handle, pt, HHT_ONHEADER);
        }

        private static int HeaderControlHitTest(IntPtr handle, Point pt, int flag) {
            HDHITTESTINFO testInfo = new HDHITTESTINFO();
            testInfo.pt_x = pt.X;
            testInfo.pt_y = pt.Y;
            IntPtr result = NativeMethods.SendMessageHDHITTESTINFO(handle, HDM_HITTEST, IntPtr.Zero, testInfo);
            if ((testInfo.flags & flag) != 0)
                return result.ToInt32();
            else
                return -1;
        }

        /// <summary>
        /// Get the scroll position of the given scroll bar
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="horizontalBar"></param>
        /// <returns></returns>
        public static int GetScrollPosition(IntPtr handle, bool horizontalBar) {
            int fnBar = (horizontalBar ? SB_HORZ : SB_VERT);

            SCROLLINFO si = new SCROLLINFO();
            si.fMask = SIF_POS;
            if (GetScrollInfo(handle, fnBar, si))
                return si.nPos;
            else
                return -1;
        }
    }
}
