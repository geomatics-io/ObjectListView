/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/25/2008 JPP  Initial Version
 */

using System.Windows.Forms;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestOlvCheckBoxes
    {
        [SetUp]
        public void InitEachTest() {
            if (!this.olv.CheckBoxes)
                this.olv.CheckBoxes = true;

            this.olv.SetObjects(PersonDb.All);
            this.olv.CheckedObjects = null;
            this.olv.TriStateCheckBoxes = false;
        }

        [TearDown]
        public void TearDownEachTest() {
            this.olv.CheckStateGetter = null;
            this.olv.CheckStatePutter = null;
        }

        [Test]
        public void TestCheckObject() {
            Person p = PersonDb.All[1];
            this.olv.CheckObject(p);
            Assert.AreEqual(p, this.olv.CheckedObject);
            Assert.IsTrue(this.olv.IsChecked(p));
            Assert.IsFalse(this.olv.IsCheckedIndeterminate(p));
            Assert.AreEqual(CheckState.Checked, this.olv.ModelToItem(p).CheckState);

        }

        [Test]
        public void TestCheckIndeterminateObject() {
            Person p = PersonDb.All[1];
            this.olv.CheckIndeterminateObject(p);
            Assert.IsTrue(this.olv.IsCheckedIndeterminate(p));
            Assert.IsFalse(this.olv.IsChecked(p));
            Assert.AreEqual(CheckState.Indeterminate, this.olv.ModelToItem(p).CheckState);

        }

        [Test]
        public void TestUncheckObject() {
            Person p = PersonDb.All[1];
            this.olv.CheckedObjects = PersonDb.All;
            this.olv.UncheckObject(p);
            Assert.IsFalse(this.olv.IsChecked(p));
            Assert.IsFalse(this.olv.IsCheckedIndeterminate(p));
            Assert.AreEqual(CheckState.Unchecked, this.olv.ModelToItem(p).CheckState);
        }

        [Test]
        public void TestToggleCheckObject() {
            this.olv.ToggleCheckObject(PersonDb.All[1]);
            Assert.IsTrue(this.olv.IsChecked(PersonDb.All[1]));
            this.olv.ToggleCheckObject(PersonDb.All[1]);
            Assert.IsFalse(this.olv.IsChecked(PersonDb.All[1]));
        }

        [Test]
        public void TestCheckedObject() {
            this.olv.CheckedObject = PersonDb.All[1];
            Assert.AreEqual(PersonDb.All[1], this.olv.CheckedObject);
        }

        [Test]
        public void TestCheckedObjects() {
            this.olv.CheckedObjects = PersonDb.All;
            // We really want to check that the collections contain the same members, but 
            // there is no easy way to do that
            Assert.AreEqual(PersonDb.All.Count, this.olv.CheckedObjects.Count);
        }

        [Test]
        public void TestCheckStateGetter() {
            this.olv.TriStateCheckBoxes = true;
            this.olv.CheckStateGetter = delegate(object x) {
                int idx = PersonDb.All.IndexOf((Person)x);
                switch (idx) {
                    case 0:
                        return CheckState.Checked;
                    case 1:
                        return CheckState.Unchecked;
                    default:
                        return CheckState.Indeterminate;
                }
            };
            this.olv.SetObjects(PersonDb.All);

            Assert.IsTrue(this.olv.IsChecked(PersonDb.All[0]));
            Assert.IsTrue(this.olv.ModelToItem(PersonDb.All[0]).CheckState == CheckState.Checked);
            Assert.IsFalse(this.olv.IsChecked(PersonDb.All[1]));
            Assert.IsTrue(this.olv.ModelToItem(PersonDb.All[1]).CheckState == CheckState.Unchecked);
            for (int i = 2; i < PersonDb.All.Count; i++) {
                Assert.IsTrue(this.olv.IsCheckedIndeterminate(PersonDb.All[i]));
                Assert.IsTrue(this.olv.ModelToItem(PersonDb.All[i]).CheckState == CheckState.Indeterminate);
            }
        }

        [Test]
        public void TestBooleanCheckStateGetter() {
            this.olv.BooleanCheckStateGetter = delegate(object x) { return x == PersonDb.All[1]; };
            this.olv.SetObjects(PersonDb.All);
            Assert.IsTrue(this.olv.IsChecked(PersonDb.All[1]));
            Assert.IsFalse(this.olv.IsChecked(PersonDb.All[0]));

            this.olv.BooleanCheckStateGetter = delegate(object x) { return x != PersonDb.All[0]; };
            this.olv.SetObjects(PersonDb.All);
            foreach (Person x in PersonDb.All) {
                if (x == PersonDb.All[0])
                    Assert.IsFalse(this.olv.IsChecked(x));
                else
                    Assert.IsTrue(this.olv.IsChecked(x));
            }
        }

        [Test]
        public void TestCheckStatePutter() {
            this.olv.CheckStatePutter = delegate(object x, CheckState state) {
                Person p = (Person)x;
                switch (state) {
                    case CheckState.Checked:
                        p.IsActive = true;
                        break;
                    case CheckState.Unchecked:
                        p.IsActive = false;
                        break;
                    case CheckState.Indeterminate:
                        p.IsActive = null;
                        break;
                }
                return state;
            };
            this.olv.CheckObject(PersonDb.All[0]);
            Assert.IsTrue(PersonDb.All[0].IsActive == true);
            this.olv.UncheckObject(PersonDb.All[0]);
            Assert.IsTrue(PersonDb.All[0].IsActive == false);
            this.olv.CheckIndeterminateObject(PersonDb.All[0]);
            Assert.IsFalse(PersonDb.All[0].IsActive.HasValue);
        }

        [Test]
        public void TestCheckStatePutterWithDifferentReturnValue() {
            this.olv.CheckStatePutter = delegate(object x, CheckState state) {
                Person p = (Person)x;

                // Pretend first person cannot be activated
                if (p == PersonDb.All[0]) {
                    p.IsActive = false;
                    return CheckState.Unchecked;
                }

                switch (state) {
                    case CheckState.Checked:
                        p.IsActive = true;
                        break;
                    case CheckState.Unchecked:
                        p.IsActive = false;
                        break;
                    case CheckState.Indeterminate:
                        p.IsActive = null;
                        break;
                }
                return state;
            };
            this.olv.CheckObject(PersonDb.All[0]);
            Assert.IsFalse(this.olv.IsChecked(PersonDb.All[0]));
            Assert.AreEqual(CheckState.Unchecked, this.olv.ModelToItem(PersonDb.All[1]).CheckState);
        }

        [Test]
        public void TestItemCheckEvent() {
            this.olv.TriStateCheckBoxes = true;

            try {
                this.olv.ItemCheck += new ItemCheckEventHandler(olv_ItemCheck);
                Person p = PersonDb.All[0];
                this.olv.CheckObject(p);
                Assert.AreEqual(p, this.personInEvent);
                Assert.AreEqual(CheckState.Checked, this.stateInEvent);

                p = PersonDb.All[1];
                this.olv.CheckObject(p);
                Assert.AreEqual(p, this.personInEvent);
                Assert.AreEqual(CheckState.Checked, this.stateInEvent);

                this.olv.UncheckObject(p);
                Assert.AreEqual(p, this.personInEvent);
                Assert.AreEqual(CheckState.Unchecked, this.stateInEvent);

                this.olv.CheckIndeterminateObject(p);
                Assert.AreEqual(p, this.personInEvent);
                Assert.AreEqual(CheckState.Indeterminate, this.stateInEvent);
            }
            finally {
                this.olv.ItemCheck -= new ItemCheckEventHandler(olv_ItemCheck);
            }
        }
        Person personInEvent;
        CheckState stateInEvent;

        void olv_ItemCheck(object sender, ItemCheckEventArgs e) {
            this.personInEvent = (Person)this.olv.GetItem(e.Index).RowObject;
            this.stateInEvent = e.NewValue;
        }

        [Test]
        public void TestSpaceBar() {
            // For some reason, SendKeys doesn't work here

            //this.olv.Select(); // Make the listview have the keyboard focus
            //this.olv.SelectAll();
            //SendKeys.SendWait(" ");
            //Assert.AreEqual(this.olv.GetItemCount(), this.olv.CheckedObjects.Count);
        }

        [Test]
        public void TestCheckedAspectName() {
            foreach (Person p in PersonDb.All)
                p.IsActive = false;

            this.olv.CheckedAspectName = "IsActive";
            this.olv.SetObjects(PersonDb.All);
            Assert.IsEmpty(this.olv.CheckedObjects);

            foreach (Person p in PersonDb.All)
                p.IsActive = true;
            this.olv.SetObjects(PersonDb.All);
            Assert.AreEqual(PersonDb.All.Count, this.olv.CheckedObjects.Count);

            this.olv.CheckedAspectName = null;
        }

        [Test]
        public void TestSubItemCheckBox() {
            PersonDb.All[0].IsActive = true;
            PersonDb.All[1].IsActive = false;

            OLVColumn column = this.olv.GetColumn(1);
            string oldAspectName = column.AspectName;
            column.AspectName = "IsActive";
            column.CheckBoxes = true;

            this.olv.SetObjects(PersonDb.All);
            Assert.IsTrue(this.olv.IsSubItemChecked(PersonDb.All[0], column));
            Assert.IsFalse(this.olv.IsSubItemChecked(PersonDb.All[1], column));

            this.olv.ToggleSubItemCheckBox(PersonDb.All[0], column);
            this.olv.ToggleSubItemCheckBox(PersonDb.All[1], column);

            Assert.IsFalse(this.olv.IsSubItemChecked(PersonDb.All[0], column));
            Assert.IsTrue(this.olv.IsSubItemChecked(PersonDb.All[1], column));

            this.olv.GetColumn(1).AspectName = oldAspectName;
        }

        [TestFixtureSetUp]
        public void Init() {
            this.olv = MyGlobals.mainForm.objectListView1;
        }
        protected ObjectListView olv;
    }

    [TestFixture]
    public class TestFastOlvCheckBoxes : TestOlvCheckBoxes
    {
        [TestFixtureSetUp]
        new public void Init() {
            this.olv = MyGlobals.mainForm.fastObjectListView1;
        }
    }

    [TestFixture]
    public class TestTreeListViewCheckBoxes : TestOlvCheckBoxes
    {
        [TestFixtureSetUp]
        new public void Init() {
            this.olv = MyGlobals.mainForm.treeListView1;
        }
    }
}
