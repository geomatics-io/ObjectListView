/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 11/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 11/25/2008 JPP  Initial Version
 */

using System;
using System.Collections;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestSorting
    {
        [SetUp]
        public void InitEachTest()
        {
            this.olv.LastSortColumn = null;
            this.olv.LastSortOrder = SortOrder.None;
            this.olv.SetObjects(PersonDb.All);
        }

        [TearDown]
        public void TearDownEachTest()
        {
        }

        [Test]
        public void TestInitialConditions()
        {
        }

        [Test]
        public void TestSecondarySorting()
        {
            this.olv.SecondarySortColumn = this.olv.GetColumn(0);
            this.olv.SecondarySortOrder = SortOrder.Descending;
            this.olv.Sort(this.olv.GetColumn(3), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.LastAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);

            this.olv.SecondarySortColumn = this.olv.GetColumn(0);
            this.olv.SecondarySortOrder = SortOrder.Ascending;
            this.olv.Sort(this.olv.GetColumn(3), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
        }

        [Test]
        public void TestSortingByStringColumn()
        {
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Descending);
            Assert.AreEqual(PersonDb.LastAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
        }

        [Test]
        public void TestSortingByIntColumn()
        {
            this.olv.Sort(this.olv.GetColumn(2), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.All[PersonDb.All.Count - 1], this.olv.GetModelObject(0));
            this.olv.Sort(this.olv.GetColumn(2), SortOrder.Descending);
            Assert.AreEqual(PersonDb.All[0], this.olv.GetModelObject(0));
        }

        [Test]
        public void TestNoSorting()
        {
            ArrayList beforeContents = GetContents();

            this.olv.Sort();

            Assert.AreEqual(beforeContents, GetContents());

            this.olv.LastSortColumn = this.olv.GetColumn(0);
            this.olv.LastSortOrder = SortOrder.Descending;
            this.olv.Sort();

            Assert.AreNotEqual(beforeContents, GetContents());
        }

        private ArrayList GetContents()
        {
            ArrayList contents = new ArrayList();
            for (int i = 0; i < this.olv.GetItemCount(); i++)
                contents.Add(this.olv.GetModelObject(i));
            return contents;
        }

        [Test]
        virtual public void TestCustomSorting()
        {
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);

            try {
                this.olv.CustomSorter = delegate(OLVColumn column, SortOrder order) {
                    this.olv.ListViewItemSorter = new ColumnComparer(new OLVColumn("dummy", "BirthDate"), SortOrder.Descending);
                };
                this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
                Assert.AreNotEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
            }
            finally {
                this.olv.CustomSorter = null;
            }
        }

        [Test]
        public void TestAfterSortingEvent()
        {
        }

        [Test]
        public void TestBeforeSortingEvent()
        {
        }

        [Test]
        public void TestCancelSorting()
        {
        }

        [Test]
        public void TestPreserveSelection()
        {
        }

        [TestFixtureSetUp]
        public void Init()
        {
            this.olv = MyGlobals.mainForm.objectListView1;
        }
        protected ObjectListView olv;
    }

    [TestFixture]
    public class TestFastOlvSorting : TestSorting
    {
        [TestFixtureSetUp]
        new public void Init()
        {
            this.olv = MyGlobals.mainForm.fastObjectListView1;
        }
    }
}
