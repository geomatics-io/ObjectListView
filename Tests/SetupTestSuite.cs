/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/25/2008 10:31 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/25/2008 JPP  Initial Version
 */

using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace BrightIdeasSoftware.Tests
{
    [SetUpFixture]
    public class MyGlobals
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            MyGlobals.mainForm = new MainForm();
            MyGlobals.mainForm.Show();
        }
        public static MainForm mainForm;

        [TearDown]
        public void RunAfterAnyTests()
        {
            MyGlobals.mainForm.Close();
        }
    }
}
