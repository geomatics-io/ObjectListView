/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/21/2008 11:01 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/21/2008 JPP  Initial Version
 */

using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware.Tests
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
//			Application.EnableVisualStyles();
//			Application.SetCompatibleTextRenderingDefault(false);
//			Application.Run(new MainForm());
            MyGlobals g = new MyGlobals();
            g.RunBeforeAnyTests();

            TestTreeListViewSelection t2 = new TestTreeListViewSelection();
            t2.Init();
            t2.InitEachTest();

            TestTreeView t = new TestTreeView();
            t.Init();
            t.InitEachTest();
            t.TestExpandAll();

            g.RunAfterAnyTests();
		}
		
	}
}
