using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrightIdeasSoftware.SubControls
{
    /// <summary>
    ///   <para>
    ///     <strong>Why System.Windows.Forms.ListView sucks and how to fix it</strong>
    ///   </para>
    ///   <para>It seems that a perpetual affliction of .NET WinForms-based applications is slow and flickery repainting. Part of the problem is .NET's insistence on using GDI+, which is not hardware accelerated to any useful extent. That still doesn't explain why so many controls flicker all of the time, even though they're based on Win32 controls that don't have the same problem. Today I hit this problem yet again in a tool, this time with ListView. It drives me absolutely nuts to see a system with a 3GHz Core 2 and a GeForce 8800 take four seconds to redraw a list view that has three columns and a hundred entries when I drag a column, and even worse, flicker the entire time.</para>
    ///   <para>Therefore, I had to sit down tonight and figure out how you could make a standard Win32 ListView update so slowly that a 1541 drive could almost keep up with it.</para>
    ///   <para>(Caveat: As usual, I do my primary work in XP. I'm too lazy to reboot into Windows 7 right now.)</para>
    ///   <para>The way I ended up debugging this involved parallel C++ and C# apps. Both were fairly vanilla apps made using the built-in app wizards, the C++ one containing a dialog with a list view, and the C# one being the same but with a WinForm. Okay, I'll admit that the C++ one was more annoying to write, because programming a Win32 list view directly is a lot of gruntwork. However, out of the box, the C++ app updated <strong>much</strong> more smoothly and didn't flicker madly. I'll spare you the debugging details -- which include ILDASM, WinDbg, Spy++, two instances of Visual Studio, and tracepoints in x86 assembly while debugging in mixed mode -- but I managed to figure out what was going on. The WinForms ListView is indeed a Win32 ListView with heavy subclassing, but it turns out the poor performance is caused by two bad design decisions on the part of the WinForms team:</para>
    ///   <list type="number">
    ///     <item>The Win32 list view is always in owner draw mode. Always. Even if you don't have OwnerDraw set in the control. Specifically, the WinForms ListView intercepts WM_NOTIFY + NM_CUSTOMDRAW and handles the item painting itself. In doing so, it ends up creating and destroying a lot of GDI+ contexts, and that kills redraw performance, just like we've seen with DataGridView.<br /></item>
    ///     <item>In its OnHandleCreated handler, ListView sets the text background color to transparent (ListView_SetTextBkColor(hwnd, CLR_NONE)). As it turns out, this kills the fast path in the Win32 list view code and switches it from incremental painting in opaque mode to a full erase + redraw over the entire control. You can spot the difference if you set a breakpoint on {,,user32}_NtUserRedrawWindow@16.</item>
    ///   </list>
    ///   <para>Both of these are fixable -- the first problem can be fixed by intercepting NM_CUSTOMDRAW and forcing it to return 0, thus restoring the built-in redraw code, and the second one by sending another LVM_SETTEXTBKCOLOR message to restore an opaque background color. With these two fixes, the C# app runs as smoothly as the C++ app. I don't know why the WinForms team chose such poor defaults.</para>
    ///   <para>
    ///     <a href="http://www.virtualdub.org/blog/pivot/entry.php?id=273">http://www.virtualdub.org/blog/pivot/entry.php?id=273</a>
    ///   </para>
    /// </summary>
    public class FastListView : ListView
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr hwndFrom;
            public uint idFrom;
            public uint code;
        }

        private const uint NM_CUSTOMDRAW = unchecked((uint)-12);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x204E)
            {
                NMHDR hdr = (NMHDR)m.GetLParam(typeof(NMHDR));
                if (hdr.code == NM_CUSTOMDRAW)
                {
                    m.Result = (IntPtr)0;
                    return;
                }
            }

            base.WndProc(ref m);
        }
    }
}