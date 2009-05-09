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
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestTreeView
    {
        [SetUp]
        public void InitEachTest() {
            this.olv.Roots = PersonDb.All.GetRange(0, this.numberOfRoots);
            this.olv.CollapseAll();
        }
        int numberOfRoots = 2;

        [TearDown]
        public void TearDownEachTest() {
        }

        [Test]
        public void TestInitialConditions() {
            Assert.AreEqual(this.numberOfRoots, this.olv.GetItemCount());
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
            Assert.AreEqual(this.numberOfRoots, this.olv.GetItemCount());
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

            Assert.AreEqual(this.numberOfRoots, this.olv.GetItemCount());
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
            this.olv.RefreshObject(new Person("name3"));
        }

        [Test]
        public void TestPreserveSelection() {
            this.olv.SelectedObject = PersonDb.All[1];
            this.olv.Expand(PersonDb.All[0]);
            Assert.AreEqual(PersonDb.All[1], this.olv.SelectedObject);
            this.olv.Collapse(PersonDb.All[0]);
            Assert.AreEqual(PersonDb.All[1], this.olv.SelectedObject);
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
        }
        protected TreeListView olv;
    }
}
