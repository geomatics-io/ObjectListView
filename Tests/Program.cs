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

            TestTreeView t = new TestTreeView();
            t.Init();
            t.InitEachTest();
            t.TestExpand();

            g.RunAfterAnyTests();
        }

        //[STAThread]
        //private static void Main(string[] args)
        //{
        //    int start = Environment.TickCount;
        //    int iterations = 1000;

        //    Person person1 = new Person("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");
        //    Person2 person2 = new Person2("name", "occupation", 100, DateTime.Now, 1.0, true, "  photo  ", "comments");

        //    OLVColumn column1 = new OLVColumn("ignored", "CulinaryRating");
        //    OLVColumn column2 = new OLVColumn("ignored", "BirthDate.Year.ToString.Length");
        //    OLVColumn column3 = new OLVColumn("ignored", "Photo.ToString.Trim");

        //    for (int i = 0; i < iterations; i++) {
        //        column1.GetValue(person1);
        //        column1.GetValue(person2);
        //        column2.GetValue(person1);
        //        column2.GetValue(person2);
        //        column3.GetValue(person1);
        //        column3.GetValue(person2);
        //    }

        //    Console.WriteLine("Elapsed time: {0}ms", Environment.TickCount-start);
        //}

        // Base line: Elapsed time: 204ms
    }
}
