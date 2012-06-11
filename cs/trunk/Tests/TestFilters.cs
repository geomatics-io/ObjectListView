﻿
using System.Collections;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Collections.Generic;

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
            this.olv.ModelFilter = TextMatchFilter.Prefix(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(0, 4));
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(0, 4));
            filter.StringComparison = StringComparison.InvariantCulture;
            this.olv.ModelFilter = filter;
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = TextMatchFilter.Prefix(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(1, 4));
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
            this.olv.ModelFilter = TextMatchFilter.Regex(this.olv, "[z]+");
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_CaseInsensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "Z+");
            filter.StringComparison = StringComparison.CurrentCultureIgnoreCase;
            this.olv.ModelFilter = filter;

            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "Z+");
            filter.StringComparison = StringComparison.CurrentCulture;
            this.olv.ModelFilter = filter;
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_Invalid() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = TextMatchFilter.Regex(this.olv, @"[\*");
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Columns() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "occup");
            filter.Columns = new OLVColumn[] { this.olv.GetColumn(1), this.olv.GetColumn(2) };
            this.olv.ModelFilter = filter;
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Columns_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "occup");
            filter.Columns = new OLVColumn[] { this.olv.GetColumn(0), this.olv.GetColumn(2) };
            this.olv.ModelFilter = filter;
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text() {
            TextMatchFilter filter = new TextMatchFilter(this.olv, "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(2, ranges.Count);
            Assert.AreEqual(2, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(7, ranges[1].First);
            Assert.AreEqual(3, ranges[1].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "abc", "DE");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCDE"));
            Assert.AreEqual(3, ranges.Count);
            Assert.AreEqual(2, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(7, ranges[1].First);
            Assert.AreEqual(3, ranges[1].Length);
            Assert.AreEqual(10, ranges[2].First);
            Assert.AreEqual(2, ranges[2].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text_NoMatch() {
            TextMatchFilter filter = new TextMatchFilter(this.olv, "xyz");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text_NoMatch_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "xyz", "jpp");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_StringStart() {
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("abcd-ABCD"));
            Assert.AreEqual(1, ranges.Count);
            Assert.AreEqual(0, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_StringStart_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, "xyz", "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("abcd-ABCD"));
            Assert.AreEqual(1, ranges.Count);
            Assert.AreEqual(0, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_StringStart_NoMatch() {
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Regex() {
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "[abcd]+");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("nada-abcd-ab-ABCD"));
            Assert.AreEqual(4, ranges.Count);
            Assert.AreEqual(1, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(5, ranges[1].First);
            Assert.AreEqual(4, ranges[1].Length);
            Assert.AreEqual(10, ranges[2].First);
            Assert.AreEqual(2, ranges[2].Length);
            Assert.AreEqual(13, ranges[3].First);
            Assert.AreEqual(4, ranges[3].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Regex_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "x.*z", "a.*c");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("rst-ABC-rst-xyz"));
            Assert.AreEqual(2, ranges.Count);
            Assert.AreEqual(12, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(4, ranges[1].First);
            Assert.AreEqual(3, ranges[1].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Regex_NoMatch() {
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "[yz]+");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
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
        // ListFilters don't work on TreeListViews

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

    [TestFixture]
    public class TestDateClusteringStrategy {

        readonly DateTime DATE1 = new DateTime(1998, 11, 30, 22, 23, 24);
        readonly DateTime DATE2 = new DateTime(1999, 12, 31, 22, 23, 24);

        [Test]
        public void Test_Construction_Empty() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy();
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE2; };
            object result = strategy.GetClusterKey(null);

            Assert.AreEqual(new DateTime(1999, 12, 1), result);
        }

        [Test]
        public void Test_Construction_WithPortions() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Hour | DateTimePortion.Minute, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            object result = strategy.GetClusterKey(null);

            Assert.AreEqual(new DateTime(1, 1, 1, 22, 23, 0), result);
        }

        [Test]
        public void Test_Extracting_FromNull() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Hour | DateTimePortion.Minute, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return null; };
            object result = strategy.GetClusterKey(null);
            Assert.IsNull(result);
        }

        [Test]
        public void Test_GetClusterDisplayLabel_Plural() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Hour | DateTimePortion.Minute, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            ICluster cluster = new Cluster(strategy.GetClusterKey(null));
            cluster.Count = 2;
            string result = strategy.GetClusterDisplayLabel(cluster);
            Assert.AreEqual("22:23 (2 items)", result);
        }

        [Test]
        public void Test_GetClusterDisplayLabel_Singular() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Year | DateTimePortion.Month, "MM-yy");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            ICluster cluster = new Cluster(strategy.GetClusterKey(null));
            cluster.Count = 1;
            string result = strategy.GetClusterDisplayLabel(cluster);
            Assert.AreEqual("11-98 (1 item)", result);
        }

        [Test]
        public void Test_GetClusterDisplayLabel_NullValue() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Year | DateTimePortion.Month, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            ICluster cluster = new Cluster(null);
            cluster.Count = 1;
            string result = strategy.GetClusterDisplayLabel(cluster);
            Assert.AreEqual(ClusteringStrategy.NULL_LABEL + " (1 item)", result);
        }
    }

    [TestFixture]
    public class TestFlagClusteringStrategy {

        [Test]
        public void Test_EnumConstruction_Values() {
            FlagClusteringStrategy strategy = new FlagClusteringStrategy(typeof(TestFlagEnum));
            Assert.AreEqual(4, strategy.Values.Length);
            Assert.Contains(2, strategy.Values);
            Assert.Contains(4, strategy.Values);
            Assert.Contains(8, strategy.Values);
            Assert.Contains(16, strategy.Values);
        }

        [Test]
        public void Test_EnumConstruction_Labels() {
            FlagClusteringStrategy strategy = new FlagClusteringStrategy(typeof(TestFlagEnum));
            Assert.AreEqual(4, strategy.Labels.Length);
            Assert.Contains("FlagValue1", strategy.Labels);
            Assert.Contains("FlagValue2", strategy.Labels);
            Assert.Contains("FlagValue3", strategy.Labels);
            Assert.Contains("FlagValue4", strategy.Labels);
        }

        [Test]
        public void Test_GetClusterKey() {
            FlagClusteringStrategy strategy = new FlagClusteringStrategy(typeof(TestFlagEnum));
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return TestFlagEnum.FlagValue1 | TestFlagEnum.FlagValue4; };
            object result = strategy.GetClusterKey(null);
            Assert.IsInstanceOf<IEnumerable>(result);
            Assert.AreEqual(2, ((ICollection)result).Count);
            Assert.Contains((ulong)TestFlagEnum.FlagValue1, result as ICollection);
            Assert.Contains((ulong)TestFlagEnum.FlagValue4, result as ICollection);
        }

        [Test]
        public void Test_GetClusterKey_ZeroValue() {
            FlagClusteringStrategy strategy = new FlagClusteringStrategy(typeof(TestFlagEnum));
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return 0; };
            object result = strategy.GetClusterKey(null);
            Assert.IsInstanceOf<IEnumerable>(result);
            Assert.AreEqual(0, ((ICollection)result).Count);
        }

        [Test]
        public void Test_GetClusterKey_NonNumericValue() {
            FlagClusteringStrategy strategy = new FlagClusteringStrategy(typeof(TestFlagEnum));
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return "not number"; };
            object result = strategy.GetClusterKey(null);
            Assert.IsInstanceOf<IEnumerable>(result);
            Assert.AreEqual(0, ((ICollection)result).Count);
        }

        [Test]
        public void Test_GetClusterKey_NonConvertibleValue() {
            FlagClusteringStrategy strategy = new FlagClusteringStrategy(typeof(TestFlagEnum));
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return new object(); };
            object result = strategy.GetClusterKey(null);
            Assert.IsInstanceOf<IEnumerable>(result);
            Assert.AreEqual(0, ((ICollection)result).Count);
        }

        [Test]
        public void Test_GetClusterDisplayLabel() {
            FlagClusteringStrategy strategy = new FlagClusteringStrategy(typeof(TestFlagEnum));
            ICluster cluster = new Cluster(TestFlagEnum.FlagValue2);
            cluster.Count = 2;
            string result = strategy.GetClusterDisplayLabel(cluster);
            Assert.AreEqual("FlagValue2 (2 items)", result);
        }

        [Flags]
        private enum TestFlagEnum {
            FlagValue1 = 2,
            FlagValue2 = 4,
            FlagValue3 = 8,
            FlagValue4 = 16
        }
    }
}
