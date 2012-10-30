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
using System.Collections.Generic;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestOlvBasics
    {
        [Test]
        public void Test_SetObjects_All() {
            this.olv.SetObjects(PersonDb.All);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public void Test_SetObjects_Null() {
            this.olv.SetObjects(null);
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public void Test_GetModelObject() {
            this.olv.SetObjects(PersonDb.All);
            for (int i = 0; i < PersonDb.All.Count; i++)
                Assert.AreEqual(PersonDb.All[i], this.olv.GetModelObject(i));
        }

        [Test]
        public void Test_AddObject() {
            this.olv.SetObjects(null);
            this.olv.AddObject(PersonDb.All[0]);
            Assert.AreNotEqual(-1, this.olv.IndexOf(PersonDb.All[0]));
            this.olv.AddObject(PersonDb.All[1]);
            Assert.AreNotEqual(-1, this.olv.IndexOf(PersonDb.All[1]));
            Assert.AreEqual(2, this.olv.GetItemCount());
        }

        [Test]
        public void Test_AddObjects() {
            this.olv.SetObjects(null);
            this.olv.AddObjects(PersonDb.All);
            foreach (object x in PersonDb.All)
                Assert.AreNotEqual(-1, this.olv.IndexOf(x));
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public void Test_RemoveObject() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.RemoveObject(PersonDb.All[1]);
            Assert.AreEqual(-1, this.olv.IndexOf(PersonDb.All[1]));
            Assert.AreEqual(PersonDb.All.Count - 1, this.olv.GetItemCount());
        }

        [Test]
        public void Test_RemoveObjects() {
            this.olv.SetObjects(PersonDb.All);
            List<Person> toRemove = new List<Person>();
            toRemove.Add(PersonDb.All[1]);
            toRemove.Add(PersonDb.All[2]);
            toRemove.Add(PersonDb.All[5]);
            this.olv.RemoveObjects(toRemove);
            foreach (Person x in toRemove)
                Assert.AreEqual(-1, this.olv.IndexOf(x));
            Assert.AreEqual(PersonDb.All.Count - toRemove.Count, this.olv.GetItemCount());
        }


        [Test]
        public void Test_EffectiveRowHeight() {
            this.olv.RowHeight = 32;
            Assert.AreEqual(32, this.olv.RowHeightEffective);
            this.olv.RowHeight = -1;
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

    [TestFixture]
    public class TestTypedListView {
        [Test]
        public void Test_Objects_All() {
            this.tolv.Objects = PersonDb.All;
            Assert.AreEqual(PersonDb.All.Count, this.tolv.Objects.Count);
        }

        [Test]
        public void Test_GenerateAspectGetters_ExtractsData() {
            this.tolv.GenerateAspectGetters();
            this.tolv.Objects = PersonDb.All;
            Person p = this.tolv.ListView.GetItem(0).RowObject as Person;
            Assert.AreEqual(p.Name, this.tolv.ListView.Items[0].SubItems[0].Text);
        }

        [Test]
        public void Test_GenerateAspectGetters_NullDataObject() {
            this.tolv.GenerateAspectGetters();
            List<Person> list = new List<Person>();
            list.Add(null);
            this.tolv.Objects = list;
            Assert.AreEqual(list.Count, this.tolv.Objects.Count);
        }

        [Test]
        public void Test_GenerateAspectGetters_ClearObjects() {
            this.tolv.GenerateAspectGetters();
            this.tolv.Objects = PersonDb.All;
            this.tolv.ListView.ClearObjects();
            Assert.AreEqual(0, this.tolv.Objects.Count);
            this.tolv.Objects = PersonDb.All;
            Assert.AreEqual(PersonDb.All.Count, this.tolv.Objects.Count);
        }

        [TearDown]
        public void TestTearDown() {
            // Clear any aspect getters that were generated
            for (int i = 0; i < this.tolv.ListView.Columns.Count; i++)
                this.tolv.ListView.GetColumn(i).AspectGetter = null;
        }

        [TestFixtureSetUp]
        public void Init() {
            this.tolv = new TypedObjectListView<Person>(MyGlobals.mainForm.objectListView1);
        }

        protected TypedObjectListView<Person> tolv;
    }
}
