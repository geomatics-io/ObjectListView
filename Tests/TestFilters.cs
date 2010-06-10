
using System.Collections;
using NUnit.Framework;
using System;

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

        [Test]
        public virtual void Test_TextFilter_CaseInsensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant());
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant(), StringComparison.InvariantCulture);
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv,
                PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(0, 4),
                TextMatchFilter.MatchKind.StringStart);
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv,
                PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(0, 4),
                TextMatchFilter.MatchKind.StringStart, StringComparison.InvariantCulture);
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv,
                PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(1, 4),
                TextMatchFilter.MatchKind.StringStart);
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastAlphabeticalName + "WILL NOT MATCH");
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, "[z]+", TextMatchFilter.MatchKind.Regex);
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_CaseInsensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, "Z+", TextMatchFilter.MatchKind.Regex, StringComparison.CurrentCultureIgnoreCase);
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, "Z+", TextMatchFilter.MatchKind.Regex, StringComparison.CurrentCulture);
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_Invalid() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, @"[\*", TextMatchFilter.MatchKind.Regex);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Columns() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, "occup", new OLVColumn[] { this.olv.GetColumn(1), this.olv.GetColumn(2) });
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Columns_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, "occup", new OLVColumn[] { this.olv.GetColumn(0), this.olv.GetColumn(2) });
            Assert.AreEqual(0, this.olv.GetItemCount());
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
