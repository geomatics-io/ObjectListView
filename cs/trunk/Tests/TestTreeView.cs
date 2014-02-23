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
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestTreeView
    {
        [SetUp]
        public void InitEachTest() {
            PersonDb.Reset();

            mainForm = new MainForm();
            mainForm.Size = new Size();
            mainForm.Show();
            this.olv = mainForm.treeListView1;

            this.olv.CanExpandGetter = delegate(Object x) {
                return ((Person)x).Children.Count > 0;
            };
            this.olv.ChildrenGetter = delegate(Object x) {
                return ((Person)x).Children;
            };
            // this is only used when HierarchicalCheckboxes is true
            this.olv.ParentGetter = delegate(object child) {
                return ((Person)child).Parent;
            };

            this.olv.UseFiltering = false;
            this.olv.ModelFilter = null;
            this.olv.HierarchicalCheckboxes = false;
            this.olv.Roots = PersonDb.All.GetRange(0, NumberOfRoots);
            this.olv.DiscardAllState();
        }
        private const int NumberOfRoots = 2;
        protected TreeListView olv;
        private MainForm mainForm;

        [TearDown]
        public void TearDownEachTest() {
            mainForm.Close();
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
            Assert.AreEqual(expectedCount2, this.olv.GetItemCount()-1);
        }

        [Test]
        public void TestClearAll() {
            this.olv.ClearObjects();
            Assert.AreEqual(0, this.olv.GetItemCount());
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
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant());

            // After filtering the list should contain the one item that matched the filter and its ancestors
            Assert.AreEqual(4, this.olv.GetItemCount());
        }

        [Test]
        public void Test_ModelFilter_HandlesColumnChanges() {
            this.olv.ExpandAll();

            OLVColumn commentColumn = new OLVColumn("Comment field", "Comments");
            this.olv.AllColumns.Add(commentColumn);
            this.olv.RebuildColumns();

            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastComment);

            // After filtering the list should contain the one item that matched the filter and its ancestors
            Assert.AreEqual(4, this.olv.GetItemCount());

            commentColumn.IsVisible = false;
            this.olv.RebuildColumns();
            Assert.AreEqual(0, this.olv.VirtualListSize);

            commentColumn.IsVisible = true;
            this.olv.RebuildColumns();
            Assert.AreEqual(4, this.olv.GetItemCount());
            Assert.AreEqual(PersonDb.LastComment, ((Person)this.olv.GetItem(3).RowObject).Comments);
        }

        [Test]
        public void TestRefreshWhenModelChanges() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();
            this.olv.Expand(firstRoot);
            Assert.AreEqual(2, ObjectListView.EnumerableToArray(this.olv.GetChildren(firstRoot), false).Count);

            IList<Person> originalChildren = firstRoot.Children;
            firstRoot.Children = new List<Person>();
            firstRoot.Children.Add(originalChildren[0]);
            Assert.AreEqual(2, ObjectListView.EnumerableToArray(this.olv.GetChildren(firstRoot), false).Count);
            this.olv.RefreshObject(firstRoot);
            Assert.AreEqual(1, ObjectListView.EnumerableToArray(this.olv.GetChildren(firstRoot), false).Count);

            firstRoot.Children = originalChildren;
        }

        [Test]
        public void TestRefreshObject_ExpansionUnchanged() {
            this.olv.CollapseAll();
            this.olv.ExpandAll();
            int count = this.olv.GetItemCount();
            ArrayList expanded = ObjectListView.EnumerableToArray(this.olv.ExpandedObjects, false);
            Person lastExpanded = (Person) expanded[expanded.Count - 1];
            object parentOfLastExpanded = this.olv.GetParent(lastExpanded);
            this.olv.RefreshObject(parentOfLastExpanded);
            Assert.IsTrue(this.olv.IsExpanded(lastExpanded));
            Assert.AreEqual(count, this.olv.GetItemCount());
        }

        [Test]
        public void TestCheckedObjects_CheckingVisibleObjects() {
            this.olv.CheckBoxes = true;
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();
            this.olv.Expand(firstRoot);
            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.CheckedObjects = ObjectListView.EnumerableToArray(firstRoot.Children, true);

            ArrayList checkedObjects = new ArrayList(this.olv.CheckedObjects);
            Assert.AreEqual(firstRoot.Children.Count, checkedObjects.Count);
        }
        
        [Test]
        public void TestCheckedObjects_CheckingHiddenObjects() {
            this.olv.CheckBoxes = true;
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.CheckedObjects = ObjectListView.EnumerableToArray(firstRoot.Children, true);

            ArrayList checkedObjects = new ArrayList(this.olv.CheckedObjects);
            Assert.AreEqual(firstRoot.Children.Count, checkedObjects.Count);
        }

        [Test]
        public void TestHierarchicalCheckBoxes_Unrolled_CheckingParent_ChecksChildren() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();
            this.olv.Expand(firstRoot);

            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.HierarchicalCheckboxes = true;
            this.olv.CheckObject(firstRoot);

            foreach (Person child in firstRoot.Children)
                Assert.IsTrue(this.olv.IsChecked(child));

            this.olv.UncheckObject(firstRoot);

            foreach (Person child in firstRoot.Children)
                Assert.IsFalse(this.olv.IsChecked(child));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_Rolled_CheckingParent_ChecksChildren() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            this.olv.HierarchicalCheckboxes = true;
            this.olv.CheckObject(firstRoot);
            this.olv.Expand(firstRoot);

            foreach (Person child in firstRoot.Children)
                Assert.IsTrue(this.olv.IsChecked(child));

            this.olv.UncheckObject(firstRoot);

            foreach (Person child in firstRoot.Children)
                Assert.IsFalse(this.olv.IsChecked(child));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_CheckAllChildren_ParentIsChecked() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();
            this.olv.Expand(firstRoot);

            this.olv.HierarchicalCheckboxes = true;

            Assert.IsFalse(this.olv.IsChecked(firstRoot));

            foreach (Person child in firstRoot.Children)
                this.olv.CheckObject(child);

            Assert.IsTrue(this.olv.IsChecked(firstRoot));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_CheckSomeChildren_ParentIsIndeterminate() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();
            this.olv.Expand(firstRoot);

            this.olv.HierarchicalCheckboxes = true;

            Assert.IsFalse(this.olv.IsChecked(firstRoot));

            foreach (Person child in firstRoot.Children)
                this.olv.CheckObject(child);
            this.olv.UncheckObject(firstRoot.Children[0]);

            Assert.IsTrue(this.olv.IsCheckedIndeterminate(firstRoot));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_UncheckAllChildren_ParentIsUnchecked() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();
            this.olv.Expand(firstRoot);

            this.olv.HierarchicalCheckboxes = true;

            this.olv.CheckObject(firstRoot);

            foreach (Person child in firstRoot.Children)
                this.olv.UncheckObject(child);

            Assert.IsFalse(this.olv.IsChecked(firstRoot));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_ChildrenInheritCheckedness() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            this.olv.HierarchicalCheckboxes = true;

            this.olv.CheckObject(firstRoot);
            Assert.IsTrue(this.olv.IsChecked(firstRoot));

            this.olv.Expand(firstRoot);
            Assert.IsTrue(this.olv.IsChecked(firstRoot));

            foreach (Person child in firstRoot.Children)
                Assert.IsTrue(this.olv.IsChecked(child));
        }

        [Test]
        public void TestHierarchicalCheckBoxes() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.HierarchicalCheckboxes = true;
            this.olv.CheckObject(firstRoot);

            foreach (Person child in this.olv.GetChildren(firstRoot))
                Assert.IsTrue(this.olv.IsChecked(child));

            this.olv.UncheckObject(firstRoot);

            foreach (Person child in firstRoot.Children)
                Assert.IsFalse(this.olv.IsChecked(child));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_CheckedObjects_Get() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.HierarchicalCheckboxes = true;
            this.olv.CheckObject(firstRoot);

            ArrayList checkedObjects = new ArrayList(this.olv.CheckedObjects);
            Assert.AreEqual(1, checkedObjects.Count);
            Assert.IsTrue(checkedObjects.Contains(firstRoot));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_CheckedObjects_Get_IncludesExpandedChildren() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.HierarchicalCheckboxes = true;
            this.olv.Expand(firstRoot);
            this.olv.CheckObject(firstRoot);

            ArrayList checkedObjects = new ArrayList(this.olv.CheckedObjects);
            Assert.AreEqual(1 + firstRoot.Children.Count, checkedObjects.Count);
            Assert.IsTrue(checkedObjects.Contains(firstRoot));
            foreach (Person child in firstRoot.Children)
                Assert.IsTrue(checkedObjects.Contains(child));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_CheckedObjects_Get_IncludesCheckedObjectsNotInControl() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.HierarchicalCheckboxes = true;
            this.olv.Expand(firstRoot);

            Person newGuy = new Person("someone new");
            this.olv.CheckObject(newGuy);

            ArrayList checkedObjects = new ArrayList(this.olv.CheckedObjects);
            Assert.AreEqual(1, checkedObjects.Count);
            Assert.AreEqual(((Person)checkedObjects[0]).Name,  newGuy.Name);
        }

        [Test]
        public void TestHierarchicalCheckBoxes_CheckedObjects_Set_RecalculatesParent() {
            Person firstRoot = PersonDb.All[0];
            this.olv.CollapseAll();

            Assert.IsEmpty(this.olv.CheckedObjects);

            this.olv.HierarchicalCheckboxes = true;
 
            this.olv.CheckedObjects = ObjectListView.EnumerableToArray(firstRoot.Children, true);

            ArrayList checkedObjects = new ArrayList(this.olv.CheckedObjects);
            Assert.AreEqual(1 + firstRoot.Children.Count, checkedObjects.Count);
            Assert.IsTrue(checkedObjects.Contains(firstRoot));
            foreach (Person child in firstRoot.Children)
                Assert.IsTrue(checkedObjects.Contains(child));
        }

        [Test]
        public void TestHierarchicalCheckBoxes_CheckedObjects_Set_ClearsPreviousChecks() {
            Person firstRoot = PersonDb.All[0];
            Person secondRoot = PersonDb.All[1];

            this.olv.HierarchicalCheckboxes = true;
            this.olv.CheckObject(secondRoot);

            this.olv.CheckedObjects = ObjectListView.EnumerableToArray(firstRoot.Children, true);

            Assert.IsFalse(this.olv.IsChecked(secondRoot));
        }
    }
}
