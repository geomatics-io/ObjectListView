
using System.Collections;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestFilters
    {
        [SetUp]
        public void SetupTest() {
        }

        [TearDown]
        public void TearDownTest() {
            this.olv.UseFiltering = false;
            this.olv.ModelFilter = null;
            this.olv.ListFilter = null;
        }

        [Test]
        public void Test_Filter_UseFiltering() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return false; });

            this.olv.UseFiltering = true;
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ModelFilter_None() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            Assert.AreNotEqual(0, this.olv.GetItemCount());

            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return false; });
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ModelFilter_All() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            int originalCount = this.olv.GetItemCount();

            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return true; });
            Assert.AreEqual(originalCount, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_None() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            Assert.AreNotEqual(0, this.olv.GetItemCount());

            this.olv.ListFilter = new ListFilter(delegate(IEnumerable x) { return new ArrayList(); });
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_All() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            int originalCount = this.olv.GetItemCount();

            this.olv.ListFilter = new ListFilter(delegate(IEnumerable x) { return x; });
            Assert.AreEqual(originalCount, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_Tail() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ListFilter = new TailFilter(2);
            Assert.AreEqual(2, this.olv.GetItemCount());
            Assert.AreEqual(PersonDb.All[PersonDb.All.Count - 2], this.olv.GetModelObject(0));
            Assert.AreEqual(PersonDb.All[PersonDb.All.Count - 1], this.olv.GetModelObject(1));
        }

        [Test]
        public virtual void Test_ListFilter_Tail_Zero() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ListFilter = new TailFilter(0);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_Tail_Large() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ListFilter = new TailFilter(1000000);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [TestFixtureSetUp]
        public void Init() {
            this.olv = MyGlobals.mainForm.objectListView1;
        }

        protected ObjectListView olv;
    }

    [TestFixture]
    public class TestFastFilters : TestFilters
    {
        [TestFixtureSetUp]
        new public void Init() {
            this.olv = MyGlobals.mainForm.fastObjectListView1;
        }
    }

    [TestFixture]
    public class TestTreeListFilters : TestFilters
    {

        [Test]
        override public void Test_ListFilter_None() {
        }

        [Test]
        override public void Test_ListFilter_All() {
        }

        [Test]
        override public void Test_ListFilter_Tail() {
        }

        [Test]
        override public void Test_ListFilter_Tail_Zero() {
        }

        [Test]
        override public void Test_ListFilter_Tail_Large() {
        }

        [TestFixtureSetUp]
        new public void Init() {
            this.olv = MyGlobals.mainForm.treeListView1;
        }
    }
}
