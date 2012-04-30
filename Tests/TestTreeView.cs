/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/25/2008 JPP  Initial Version
 */

using System;
using System.Collections;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestTreeView
    {
        [SetUp]
        public void InitEachTest() {
            this.olv.Roots = PersonDb.All.GetRange(0, NumberOfRoots);
            this.olv.DiscardAllState();
        }
        private const int NumberOfRoots = 2;

        [TearDown]
        public void TearDownEachTest() {
        }

        [Test]
        public void TestInitialConditions() {
            Assert.AreEqual(NumberOfRoots, this.olv.GetItemCount());
            int i = 0;
            foreach (Object x in this.olv.Roots) {
                Assert.AreEqual(PersonDb.All[i], x);
                Assert.IsFalse(this.olv.IsExpanded(x));
                i++;
            }
        }

        [Test]
        public void TestCollapseAll() {
            this.olv.ExpandAll();
            this.olv.CollapseAll();
            Assert.AreEqual(NumberOfRoots, this.olv.GetItemCount());
        }

        [Test]
        public void TestExpandAll() {
            this.olv.ExpandAll();
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public void TestExpand() {
            this.olv.ExpandAll();
            this.olv.CollapseAll();

            Assert.AreEqual(NumberOfRoots, this.olv.GetItemCount());
            int expectedCount = this.olv.GetItemCount() + PersonDb.All[0].Children.Count;
            this.olv.Expand(PersonDb.All[0]);
            Assert.AreEqual(expectedCount, this.olv.GetItemCount());

            int expectedCount2 = this.olv.GetItemCount() + PersonDb.All[1].Children.Count;
            foreach (Person p in PersonDb.All[1].Children)
                expectedCount2 += p.Children.Count;
            this.olv.Expand(PersonDb.All[1]);
            Assert.AreEqual(expectedCount2, this.olv.GetItemCount());
        }

        [Test]
        public void TestCollapse() {
            int originalCount = this.olv.GetItemCount();
            this.olv.Expand(PersonDb.All[0]);
            this.olv.Expand(PersonDb.All[1]);

            this.olv.Collapse(PersonDb.All[1]);
            this.olv.Collapse(PersonDb.All[0]);
            Assert.AreEqual(originalCount, this.olv.GetItemCount());
        }

        [Test]
        public void TestRefreshOfHiddenItem() {
            this.olv.ExpandAll();
            this.olv.Collapse(PersonDb.All[1]);

            int count = this.olv.GetItemCount();

            // This should do nothing since its parent is collapsed
            this.olv.RefreshObject(PersonDb.All[1].Children[0]);
            Assert.AreEqual(count, this.olv.GetItemCount());
        }

        [Test]
        public void TestNullReferences() {
            this.olv.Expand(null);
            this.olv.Collapse(null);
            this.olv.RefreshObject(null);
        }

        [Test]
        public void TestNonExistentObjects() {
            this.olv.Expand(new Person("name1"));
            this.olv.Collapse(new Person("name2"));
            this.olv.RefreshObject(1);
        }

        [Test]
        public void TestGetParentRoot() {
            Assert.IsNull(this.olv.GetParent(PersonDb.All[0]));
        }

        [Test]
        public void TestGetParentBeforeExpand() {
            Person p = PersonDb.All[0];
            Assert.IsNull(this.olv.GetParent(p.Children[0]));
        }

        [Test]
        public void TestGetParent() {
            Person p = PersonDb.All[0];
            this.olv.Expand(p);
            Assert.AreEqual(p, this.olv.GetParent(p.Children[0]));
        }

        [Test]
        public void TestGetChildrenLeaf() {
            Person p = PersonDb.All[0];
            Assert.IsEmpty((IList)this.olv.GetChildren(p.Children[0]));
        }

        [Test]
        public void TestGetChildren() {
            Person p = PersonDb.All[0];
            IEnumerable kids = this.olv.GetChildren(p);
            int i = 0;
            foreach (Person x in kids) {
                Assert.AreEqual(x, p.Children[i]);
                i++;
            }
            Assert.AreEqual(i, p.Children.Count);
        }

        [Test]
        public void TestPreserveSelection() {
            this.olv.SelectedObject = PersonDb.All[1];
            this.olv.Expand(PersonDb.All[0]);
            Assert.AreEqual(PersonDb.All[1], this.olv.SelectedObject);
            this.olv.Collapse(PersonDb.All[0]);
            Assert.AreEqual(PersonDb.All[1], this.olv.SelectedObject);
        }

        [Test]
        public void TestExpandedObjects() {
            this.olv.ExpandedObjects = new Person[] {PersonDb.All[1]};
            Assert.Contains(PersonDb.All[1], this.olv.ExpandedObjects as ICollection);
            this.olv.ExpandedObjects = null;
            Assert.IsEmpty(this.olv.ExpandedObjects as ICollection);
        }

        [Test]
        public void TestPreserveExpansion() {
            this.olv.Expand(PersonDb.All[1]);
            Assert.Contains(PersonDb.All[1], this.olv.ExpandedObjects as ICollection);
            this.olv.Collapse(PersonDb.All[1]);
            Assert.IsEmpty(this.olv.ExpandedObjects as ICollection);
        }

        [Test]
        public void TestRebuildAllWithPreserve() {
            this.olv.CheckBoxes = true;
            this.olv.SelectedObject = PersonDb.All[0];
            this.olv.Expand(PersonDb.All[1]);
            this.olv.CheckedObjects = new Person[] { PersonDb.All[0] };
            this.olv.RebuildAll(true);
            Assert.AreEqual(PersonDb.All[0], this.olv.SelectedObject);
            Assert.Contains(PersonDb.All[1], this.olv.ExpandedObjects as ICollection);
            Assert.Contains(PersonDb.All[0], this.olv.CheckedObjects as ICollection);
            this.olv.CheckBoxes = false;
        }

        [Test]
        public void TestModelFilterNestedMatchParentsIncluded() {
            this.olv.ExpandAll();

            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.FirstAlphabeticalName.ToLowerInvariant());

            // After filtering the list should contain the one item that matched the filter and its parent
            Assert.AreEqual(2, this.olv.GetItemCount());
        }

        [TestFixtureSetUp]
        public void Init() {
            this.olv = MyGlobals.mainForm.treeListView1;
            this.olv.CanExpandGetter = delegate(Object x) {
                return ((Person)x).Children.Count > 0;
            };
            this.olv.ChildrenGetter = delegate(Object x) {
                return ((Person)x).Children;
            };
            this.olv.UseFiltering = false;
            this.olv.ModelFilter = null;
        }
        protected TreeListView olv;
    }
}
