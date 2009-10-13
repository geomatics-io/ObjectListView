/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/25/2008 JPP  Initial Version
 */

using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestOlvBasics
    {
        [Test]
        public void TestSetObjects()
        {
            this.olv.SetObjects(PersonDb.All);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
            this.olv.SetObjects(null);
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public void TestGetModelObject()
        {
            this.olv.SetObjects(PersonDb.All);
            for (int i = 0; i < PersonDb.All.Count; i++)
                Assert.AreEqual(PersonDb.All[i], this.olv.GetModelObject(i));
        }

        [TestFixtureSetUp]
        public void Init()
        {
            this.olv = MyGlobals.mainForm.objectListView1;
        }
        protected ObjectListView olv;
    }

    [TestFixture]
    public class TestFastOlvBasics : TestOlvBasics
    {
        [TestFixtureSetUp]
        new public void Init()
        {
            this.olv = MyGlobals.mainForm.fastObjectListView1;
        }
    }

    [TestFixture]
    public class TestTreeListViewBasics : TestOlvBasics
    {
        [TestFixtureSetUp]
        new public void Init()
        {
            this.olv = MyGlobals.mainForm.treeListView1;
        }
    }
}
