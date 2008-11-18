/*
 * TreeListView - A listview that can show a tree of objects in a column
 *
 * Author: Phillip Piper
 * Date: 23/09/2008 11:15 AM
 *
 * Change log:
 * 2008-11-05  JPP  - Added ExpandAll() and CollapseAll() commands
 *                  - CanExpand is no longer cached
 *                  - Renamed InitialBranches to RootModels since it deals with model objects
 * 2008-09-23  JPP  Initial version
 *
 * TO DO:
 * 2008-10-19  Think if we can remove the need to ownerdraw the tree view. 
 *             If tree does not have checkboxes, we could use the state image
 *             to show the expand/collapse icon. If the tree has check boxes,
 *             it has to be owner drawn.
 * 
 * Copyright (C) 2006-2008 Phillip Piper
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * If you wish to use this code in a closed source application, please contact phillip_piper@bigfoot.com.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// A TreeListView combines an expandable tree structure with list view columns.
    /// </summary>
    /// <remarks>
    /// <para>To support tree operations, two delegates must be provided:</para>
    /// <list>
    /// <item>CanExpandGetter. This delegate must accept a model object and return a boolean indicating
    /// if that model should be expandable. </item>
    /// <item>ChildrenGetter. This delegate must accept a model object and return an IEnumerable of model
    /// objects that will be displayed as children of the parent model. This delegate will only be called
    /// for a model object if the CanExpandGetter has already returned true for that model.</item>
    /// </list>
    /// <para>
    /// The top level branches of the tree are set via the Roots property. 
    /// </para>
    /// <para>
    /// Do not use SetObjects() method on a TreeListView.
    /// </para>
    /// <para>The tree must be a directed acyclic graph -- no cycles are allowed.</para>
    /// <para>More generally, each model object must appear only once in the tree. The same model object that appears in two
    /// places in the tree will confuse the control.</para>
    /// </remarks>
    public class TreeListView : VirtualObjectListView
    {
        /// <summary>
        /// Make a default TreeListView
        /// </summary>
        public TreeListView()
        {
            this.TreeModel = new Tree(this);
            this.OwnerDraw = true;
            this.View = View.Details;

            this.DataSource = this.TreeModel;
            this.TreeColumnRenderer = new TreeRenderer();
        }

        //------------------------------------------------------------------------------------------
        // Properties

        /// <summary>
        /// This is the delegate that will be used to decide if a model object can be expanded.
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CanExpandGetterDelegate CanExpandGetter
        {
            get { return this.TreeModel.CanExpandGetter; }
            set { this.TreeModel.CanExpandGetter = value; }
        }

        /// <summary>
        /// This is the delegate that will be used to fetch the children of a model object
        /// </summary>
        /// <remarks>This delegate will only be called if the CanExpand delegate has 
        /// returned true for the model object.</remarks>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ChildrenGetterDelegate ChildrenGetter
        {
            get { return this.TreeModel.ChildrenGetter; }
            set { this.TreeModel.ChildrenGetter = value; }
        }

        /// <summary>
        /// The model objects that form the top level branches of the tree.
        /// </summary>
        /// <remarks>Setting this does <b>NOT</b> reset the state of the control.
        /// In particular, it does not collapse branches.</remarks>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable Roots
        {
            get { return this.TreeModel.RootObjects; }
            set {
                // Make sure that column 0 is showing a tree
                if (this.GetColumn(0).Renderer == null)
                    this.GetColumn(0).Renderer = this.TreeColumnRenderer;
                if (value == null)
                    this.TreeModel.RootObjects = new ArrayList();
                else
                    this.TreeModel.RootObjects = value;
                this.UpdateVirtualListSize();
            }
        }

        /// <summary>
        /// The renderer that will be used to draw the tree structure
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BaseRenderer TreeColumnRenderer
        {
            get { return treeRenderer; }
            set { 
                treeRenderer = value;
                if (this.Columns.Count > 0)
                    this.GetColumn(0).Renderer = value;
            }
        }
        private BaseRenderer treeRenderer;
	
        /// <summary>
        /// The model that is used to manage the tree structure
        /// </summary>
        internal Tree TreeModel;

        //------------------------------------------------------------------------------------------
        // Accessing

        /// <summary>
        /// Return true if the branch at the given model is expanded
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsExpanded(Object model)
        {
            Branch br = this.TreeModel.GetBranch(model);
            return (br != null && br.IsExpanded);
        }

        //------------------------------------------------------------------------------------------
        // Commands

        /// <summary>
        /// Collapse the subtree underneath the given model
        /// </summary>
        /// <param name="model"></param>
        public void Collapse(Object model)
        {
            int idx = this.TreeModel.Collapse(model);
            if (idx >= 0) {
                this.UpdateVirtualListSize();
                this.RedrawItems(idx, this.GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Collapse all subtrees within this control
        /// </summary>
        public void CollapseAll()
        {
            int idx = this.TreeModel.CollapseAll();
            if (idx >= 0) {
                this.UpdateVirtualListSize();
                this.RedrawItems(idx, this.GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Expand the subtree underneath the given model object
        /// </summary>
        /// <param name="model"></param>
        public void Expand(Object model)
        {
            int idx = this.TreeModel.Expand(model);
            if (idx >= 0) {
                this.UpdateVirtualListSize();
                this.RedrawItems(idx, this.GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Expand all the branches within this tree recursively.
        /// </summary>
        /// <remarks>Be careful: this method could take a long time for large trees.</remarks>
        public void ExpandAll()
        {
            int idx = this.TreeModel.ExpandAll();
            if (idx >= 0) {
                this.UpdateVirtualListSize();
                this.RedrawItems(idx, this.GetItemCount() - 1, false);
            }
        }

        /// <summary>
        /// Toggle the expanded state of the branch at the given model object
        /// </summary>
        /// <param name="model"></param>
        public void ToggleExpansion(Object model)
        {
            if (this.IsExpanded(model))
                this.Collapse(model);
            else
                this.Expand(model);
        }

        //------------------------------------------------------------------------------------------
        // Delegates

        /// <summary>
        /// Delegates of this type are use to decide if the given model object can be expanded
        /// </summary>
        /// <param name="model">The model under consideration</param>
        /// <returns>Can the given model be expanded?</returns>
        public delegate bool CanExpandGetterDelegate(Object model);

        /// <summary>
        /// Delegates of this type are used to fetch the children of the given model object
        /// </summary>
        /// <param name="model">The parent whose children should be fetched</param>
        /// <returns>An enumerable over the children</returns>
        public delegate IEnumerable ChildrenGetterDelegate(Object model);

        //------------------------------------------------------------------------------------------
        // Implementation

        /// <summary>
        /// Intercept the basic message pump to customise the hit testing.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg) {
                case 0x1012: // LVM_HITTEST = (LVM_FIRST + 18)
                    this.HandleHitTest(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        unsafe private void HandleHitTest(ref Message m)
        {
            // We want to change our base behavior by taking the indentation of tree into account
            // when performing a hit test. So we figure out which row is at the test point,
            // then calculate the indentation for that row, and modify the hit test *inplace*
            // so that the normal hittest is done, but indented by the correct amount.

            this.DefWndProc(ref m);
            NativeMethods.LVHITTESTINFO* hittest = (NativeMethods.LVHITTESTINFO*)m.LParam;
            // Find which row was hit...
            int row = hittest->iItem;
            if (row < 0) 
                return;

            // ...from that decide the model object...
            Object model = this.TreeModel.GetNthObject(row);
            if (model == null)
                return;

            // ...and from that, the branch of the tree showing that model...
            Branch br = this.TreeModel.GetBranch(model);
            if (br == null)
                return;

            // ...use the indentation on that branch to modify the hittest
            hittest->pt_x += (br.Level * TreeRenderer.PIXELS_PER_LEVEL);
            this.DefWndProc(ref m);
        }

        /// <summary>
        /// Create a OLVListItem for given row index
        /// </summary>
        /// <param name="itemIndex">The index of the row that is needed</param>
        /// <returns>An OLVListItem</returns>
        /// <remarks>This differs from the base method by also setting up the IndentCount property.</remarks>
        public override OLVListItem MakeListViewItem(int itemIndex)
        {
            OLVListItem olvItem = base.MakeListViewItem(itemIndex);
            Branch br = this.TreeModel.GetBranch(olvItem.RowObject);
            if (br != null)
                olvItem.IndentCount = br.Level;
            return olvItem;
        }

        #region Event handlers

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // This horrible sequence finds what item is under the mouse position.
            // We want to find the item under the mouse, even if the mouse is not
            // actually over the icon or label. GetItemAt() will only do that
            // when FullRowSelect is true. 
            ListViewItem lvi = null;
            if (this.FullRowSelect)
                lvi = this.GetItemAt(e.X, e.Y);
            else {
                this.FullRowSelect = true;
                lvi = this.GetItemAt(e.X, e.Y);
                this.FullRowSelect = false;
            }

            // Are they trying to expand/collapse a row?
            if (lvi != null && this.HandlePossibleExpandClick((OLVListItem)lvi, e))
                return;

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Handle the given mouse down event as a possible attempt to expand/collapse
        /// a row. Return true if the event was handled.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual bool HandlePossibleExpandClick(OLVListItem olvItem, MouseEventArgs e)
        {
            Branch br = this.TreeModel.GetBranch(olvItem.RowObject);
            if (br == null || !br.CanExpand)
                return false;

            // Calculate if they clicked on the expand/collapse icon
            Rectangle r = this.GetItemRect(olvItem.Index, ItemBoundsPortion.Icon);
            r.X = this.CalculateExpanderIndentation(br);
            if (!r.Contains(e.Location))
                return false;

            this.ToggleExpansion(olvItem.RowObject);
            return true;
        }

        private int CalculateExpanderIndentation(Branch br)
        {
            return ((br.Level - 1) * TreeRenderer.PIXELS_PER_LEVEL) - 2;
        }

        /// <summary>
        /// Decide if the given key event should be handled as a normal key input to the control?
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            // We want to handle Left and Right keys within the control
            if (((keyData & Keys.KeyCode) == Keys.Left) || ((keyData & Keys.KeyCode) == Keys.Right)) {
                return true;
            } else
                return base.IsInputKey(keyData);
        }
        
        /// <summary>
        /// Handle the keyboard input to mimic a TreeView.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns>Was the key press handled?</returns>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            OLVListItem focused = this.FocusedItem as OLVListItem;
            if (focused == null) {
                base.OnKeyDown(e);
                return;
            }

            Object modelObject = focused.RowObject;
            Branch br = this.TreeModel.GetBranch(modelObject);

            switch (e.KeyCode) {
                case Keys.Left:
                    // If the branch is expanded, collapse it. If it's collapsed,
                    // select the parent of the branch.
                    if (br.IsExpanded)
                        this.Collapse(modelObject);
                    else {
                        if (br.ParentBranch != null && br.ParentBranch.Model != null)
                            this.SelectObject(br.ParentBranch.Model, true);
                    }
                    e.Handled = true;
                    break;

                case Keys.Right:
                    // If the branch is expanded, select the first child.
                    // If it isn't expanded and can be, expand it.
                    if (br.IsExpanded) {
                        if (br.ChildBranches.Count > 0)
                            this.SelectObject(br.ChildBranches[0].Model, true);
                    } else {
                        if (br.CanExpand)
                            this.Expand(modelObject);
                    }
                    e.Handled = true;
                    break;
            }

            base.OnKeyDown(e);
        }

        #endregion

        //------------------------------------------------------------------------------------------
        // Support classes

        /// <summary>
        /// A Tree object represents a tree structure data model that supports both 
        /// tree and flat list operations as well as fast access to branches.
        /// </summary>
        internal class Tree : IVirtualListDataSource
        {
            public Tree(TreeListView treeView)
            {
                this.treeView = treeView;
                this.trunk = new Branch(null, this, null);
                this.trunk.IsExpanded = true;
            }

            //------------------------------------------------------------------------------------------
            // Properties

            /// <summary>
            /// Get or return the top level model objects in the tree
            /// </summary>
            public IEnumerable RootObjects
            {
                get { return this.trunk.Children; }
                set { 
                    this.trunk.Children = value;
                    this.RebuildList();
                }
            }
	
            /// <summary>
            /// This is the delegate that will be used to decide if a model object can be expanded.
            /// </summary>
            public CanExpandGetterDelegate CanExpandGetter
            {
                get { return canExpandGetter; }
                set { canExpandGetter = value; }
            }
            private CanExpandGetterDelegate canExpandGetter;
	
            /// <summary>
            /// This is the delegate that will be used to fetch the children of a model object
            /// </summary>
            /// <remarks>This delegate will only be called if the CanExpand delegate has 
            /// returned true for the model object.</remarks>
            public ChildrenGetterDelegate ChildrenGetter
            {
                get { return childrenGetter; }
                set { childrenGetter = value; }
            }
            private ChildrenGetterDelegate childrenGetter;
	
            //------------------------------------------------------------------------------------------
            // Commands

            /// <summary>
            /// Collapse the subtree underneath the given model
            /// </summary>
            /// <param name="model">The model to be collapsed. If the model isn't in the tree,
            /// or if it is already collapsed, the command does nothing.</param>
            /// <returns>The index of the model in flat list version of the tree</returns>
            public int Collapse(Object model)
            {
                Branch br = this.GetBranch(model);
                if (br == null || !br.IsExpanded)
                    return -1;

                int count = br.NumberVisibleDescendents;
                br.Collapse();

                // Remove the visible descendents from after the branch itself
                int idx = this.GetObjectIndex(model);
                this.objectList.RemoveRange(idx + 1, count);
                this.RebuildObjectMap(idx + 1);
                return idx;
            }

            /// <summary>
            /// Collapse all branches in this tree
            /// </summary>
            /// <returns>Return the index of the first root that was not collapsed</returns>
            public int CollapseAll()
            {
                int idx = 0;
                foreach (Branch br in this.trunk.ChildBranches) {
                    if (br.IsExpanded)
                        br.Collapse();
                }
                this.RebuildList();
                return 0;
            }

            /// <summary>
            /// Expand the subtree underneath the given model object
            /// </summary>
            /// <param name="model">The model to be expanded. If the model isn't in the tree,
            /// or if it cannot be expanded, the command does nothing.</param>
            /// <returns>The index of the model in flat list version of the tree</returns>
            public int Expand(Object model)
            {
                Branch br = this.GetBranch(model);
                if (br == null || !br.CanExpand)
                    return -1;

                // Expand the branch
                br.Expand();
                br.Sort(this.GetBranchComparer());

                // Insert the branch's visible descendents after the branch itself
                int idx = this.GetObjectIndex(model);
                this.objectList.InsertRange(idx + 1, br.Flatten());
                this.RebuildObjectMap(idx + 1);
                return idx;
            }

            /// <summary>
            /// Expand all branches in this tree
            /// </summary>
            /// <returns>Return the index of the first branch that was expanded</returns>
            public int ExpandAll()
            {
                this.trunk.ExpandAll();
                this.Sort(this.lastSortColumn, this.lastSortOrder);
                return 0;
            }

            /// <summary>
            /// Return the Branch object that represents the given model in the tree
            /// </summary>
            /// <param name="model">The model whose branches is to be returned</param>
            /// <returns>The branch that represents the given model, or null if the model
            /// isn't in the tree.</returns>
            public Branch GetBranch(object model)
            {
                Branch br;

                if (this.mapObjectToBranch.TryGetValue(model, out br))
                    return br;
                else
                    return null;
            }

            //------------------------------------------------------------------------------------------
            // Implementation

            /// <summary>
            /// Remember that the given branch is part of this tree.
            /// </summary>
            /// <param name="br"></param>
            internal void RegisterBranch(Branch br)
            {
                this.mapObjectToBranch[br.Model] = br;
            }

            /// <summary>
            /// Rebuild our flat internal list of objects.
            /// </summary>
            internal void RebuildList()
            {
                this.objectList = ArrayList.Adapter(this.trunk.Flatten());
                if (this.trunk.ChildBranches.Count > 0)
                    this.trunk.ChildBranches[0].IsFirstBranch = true;
                this.RebuildObjectMap(0);
            }

            /// <summary>
            /// Rebuild our reverse index that maps an object to its location
            /// in the objectList array.
            /// </summary>
            /// <param name="startIndex"></param>
            internal void RebuildObjectMap(int startIndex)
            {
                for (int i = startIndex; i < this.objectList.Count; i++)
                    this.mapObjectToIndex[this.objectList[i]] = i;
            }

            //------------------------------------------------------------------------------------------

            #region IVirtualListDataSource Members

            public object GetNthObject(int n)
            {
                return this.objectList[n];
            }

            public int GetObjectCount()
            {
                return this.trunk.NumberVisibleDescendents;
            }

            public int GetObjectIndex(object model)
            {
                int idx;

                if (model != null && this.mapObjectToIndex.TryGetValue(model, out idx))
                    return idx;
                else
                    return -1;
            }

            public void PrepareCache(int first, int last)
            {
            }

            public int SearchText(string value, int first, int last, OLVColumn column)
            {
                return -1;
            }

            public void Sort(OLVColumn column, SortOrder order)
            {
                this.lastSortColumn = column;
                this.lastSortOrder = order;

                // Sorting is going to change the order of the branches so clear
                // the "first branch" flag
                if (this.trunk.ChildBranches.Count > 0)
                    this.trunk.ChildBranches[0].IsFirstBranch = false;

                this.trunk.Sort(this.GetBranchComparer());
                this.RebuildList();
            }

            private BranchComparer GetBranchComparer()
            {
                if (this.lastSortColumn == null)
                    return null;
                else
                    return new BranchComparer(new ModelObjectComparer(this.lastSortColumn, this.lastSortOrder, 
                        this.treeView.GetColumn(0), this.lastSortOrder));
            }

            public void AddObjects(ICollection modelObjects)
            {
                throw new InvalidOperationException("Objects cannot be added to a Tree via this method.");
            }

            public void RemoveObjects(ICollection modelObjects)
            {
                throw new InvalidOperationException("Objects cannot be removed to a Tree via this method.");
            }

            public void SetObjects(IEnumerable collection)
            {
                // We interpret a SetObjects() call as setting the roots of the tree
                this.treeView.Roots = collection;
            }

            #endregion

            //------------------------------------------------------------------------------------------
            // Private instance variables

            private OLVColumn lastSortColumn;
            private SortOrder lastSortOrder;
            private Dictionary<Object, Branch> mapObjectToBranch = new Dictionary<object, Branch>();
            private Dictionary<Object, int> mapObjectToIndex = new Dictionary<object,int>();
            private ArrayList objectList = new ArrayList();
            private TreeListView treeView;
            private Branch trunk;
        }

        /// <summary>
        /// A Branch represents a sub-tree within a tree
        /// </summary>
        internal class Branch
        {
            [Flags]
            public enum BranchFlags {
                FirstBranch = 1,
                LastChild
            }

            public Branch(Branch parent, Tree tree, Object model)
            {
                this.ParentBranch = parent;
                this.Tree = tree;
                this.Model = model;

                if (parent != null)
                    this.Level = parent.Level + 1;
            }

            //------------------------------------------------------------------------------------------
            // Properties

            /// <summary>
            /// Get the ancestor branches of this branch, with the 'oldest' ancestor first.
            /// </summary>
            public IList<Branch> Ancestors
            {
                get {
                    List<Branch> ancestors = new List<Branch>();
                    if (this.ParentBranch != null)
                        this.ParentBranch.PushAncestors(ancestors);
                    return ancestors;
                }
            }

            private void PushAncestors(IList<Branch> list)
            {
                // This is designed to ignore the trunk (which has no parent)
                if (this.ParentBranch != null) {
                    this.ParentBranch.PushAncestors(list);
                    list.Add(this);
                }
            }

            /// <summary>
            /// Can this branch be expanded?
            /// </summary>
            public bool CanExpand
            {
                get {
                    if (this.Tree.CanExpandGetter == null || this.Model == null)
                        return false;
                    else
                        return this.Tree.CanExpandGetter(this.Model);
                }
            }
            /// <summary>
            /// Get/set the model objects that are beneath this branch
            /// </summary>
            public IEnumerable Children
            {
                get {
                    ArrayList children = new ArrayList();
                    foreach (Branch x in this.ChildBranches)
                        children.Add(x.Model);
                    return children; 
                }
                set {
                    this.ChildBranches.Clear();
                    foreach (Object x in value)
                        this.AddChild(x);
                    if (this.ChildBranches.Count > 0) 
                        this.ChildBranches[this.ChildBranches.Count - 1].IsLastChild = true;
                }
            }

            private void AddChild(object model)
            {
                Branch br = this.Tree.GetBranch(model);
                if (br == null)
                    br = this.MakeBranch(model);
                this.ChildBranches.Add(br);
            }

            private Branch MakeBranch(object model)
            {
                Branch br = new Branch(this, this.Tree, model);
                this.Tree.RegisterBranch(br);
                return br;
            }

            /// <summary>
            /// Return the number of descendents of this branch that are currently visible
            /// </summary>
            /// <returns></returns>
            public int NumberVisibleDescendents
            {
                get {
                    if (!this.IsExpanded)
                        return 0;

                    int count = this.ChildBranches.Count;
                    foreach (Branch br in this.ChildBranches)
                        count += br.NumberVisibleDescendents;
                    return count;
                }
            }


            /// <summary>
            /// Return true if this branch is the first branch of the entire tree
            /// </summary>
            public bool IsFirstBranch
            {
                get { 
                    return ((this.flags & Branch.BranchFlags.FirstBranch) != 0); 
                }
                set {
                    if (value)
                        this.flags |= Branch.BranchFlags.FirstBranch;
                    else
                        this.flags &= ~Branch.BranchFlags.FirstBranch;
                }
            }

            /// <summary>
            /// Return true if this branch is the last child of its parent
            /// </summary>
            public bool IsLastChild
            {
                get { 
                    return ((this.flags & Branch.BranchFlags.LastChild) != 0); 
                }
                set { 
                    if (value)
                        this.flags |= Branch.BranchFlags.LastChild;
                    else
                        this.flags &= ~Branch.BranchFlags.LastChild; 
                }
            }
	
            //------------------------------------------------------------------------------------------
            // Commands

            /// <summary>
            /// Collapse this branch
            /// </summary>
            public void Collapse()
            {
                this.IsExpanded = false;
            }

            /// <summary>
            /// Expand this branch
            /// </summary>
            public void Expand()
            {
                if (!this.CanExpand)
                    return;

                // THINK: Should we cache the children or fetch them each time? If we cache, we need a "DiscardCache" ability

                this.IsExpanded = true;
                if (this.alreadyHasChildren)
                    return;

                if (this.Tree.ChildrenGetter != null)
                    this.Children = this.Tree.ChildrenGetter(this.Model);

                this.alreadyHasChildren = true;
            }

            /// <summary>
            /// Expand this branch recursively
            /// </summary>
            public void ExpandAll()
            {
                this.Expand();
                foreach (Branch br in this.ChildBranches)
                    br.ExpandAll();
            }

            /// <summary>
            /// Collapse the visible descendents of this branch into list of model objects
            /// </summary>
            /// <returns></returns>
            public IList Flatten()
            {
                ArrayList flatList = new ArrayList();
                if (this.IsExpanded)
                    this.FlattenOnto(flatList);
                return flatList;
            }  

            /// <summary>
            /// Flatten this branch's visible descendents onto the given list.
            /// </summary>
            /// <param name="flatList"></param>
            /// <remarks>The branch itself is <b>not</b> included in the list.</remarks>
            public void FlattenOnto(IList flatList)
            {
                foreach (Branch br in this.ChildBranches) {
                    flatList.Add(br.Model);
                    if (br.IsExpanded)
                        br.FlattenOnto(flatList);
                }
            }

            /// <summary>
            /// Sort the sub-branches and their descendents so they are ordered according
            /// to the given comparer.
            /// </summary>
            /// <param name="comparer">The comparer that orders the branches</param>
            public void Sort(BranchComparer comparer)
            {
                if (comparer == null || this.ChildBranches.Count == 0)
                    return;
                
                // We're about to sort the children, so clear the last child flag
                this.ChildBranches[this.ChildBranches.Count - 1].IsLastChild = false;

                this.ChildBranches.Sort(comparer);
                this.ChildBranches[this.ChildBranches.Count - 1].IsLastChild = true;

                foreach (Branch br in this.ChildBranches)
                    br.Sort(comparer);
            }

            //------------------------------------------------------------------------------------------
            // Public instance variables

            public Object Model;
            public Tree Tree;
            public Branch ParentBranch;
            public List<Branch> ChildBranches = new List<Branch>();
            //public bool CanExpand = false;
            public bool IsExpanded = false;
            public int Level = 0;

            //------------------------------------------------------------------------------------------
            // Private instance variables

            private bool alreadyHasChildren = false;
            private BranchFlags flags;          
        }

        /// <summary>
        /// This class sorts branches according to how their respective model objects are sorted
        /// </summary>
        internal class BranchComparer : IComparer<Branch>
        {
            public BranchComparer(IComparer actualComparer)
            {
                this.actualComparer = actualComparer;
            }

            public int Compare(Branch x, Branch y)
            {
                return this.actualComparer.Compare(x.Model, y.Model);
            }

            private IComparer actualComparer;
        }

        /// <summary>
        /// This class handles drawing the tree structure of the primary column.
        /// </summary>
        internal class TreeRenderer : BaseRenderer
        {
            /// <summary>
            /// Return the branch that the renderer is currently drawing.
            /// </summary>
            protected Branch Branch
            {
                get {
                    return this.TreeListView.TreeModel.GetBranch(this.RowObject);
                }
            }

            /// <summary>
            /// Return the TreeListView for which the renderer is being used.
            /// </summary>
            public TreeListView TreeListView
            {
                get {
                    return (TreeListView)this.ListView;
                }
            }

            /// <summary>
            /// Should the renderer draw lines connecting siblings?
            /// </summary>
            public bool IsShowLines = true;

            /// <summary>
            /// How many pixels will be reserved for each level of indentation?
            /// </summary>
            public static int PIXELS_PER_LEVEL = 16;

            /// <summary>
            /// The real work of drawing the tree is done in this method
            /// </summary>
            /// <param name="g"></param>
            /// <param name="r"></param>
            public override void Render(System.Drawing.Graphics g, System.Drawing.Rectangle r)
            {
                this.DrawBackground(g, r);

                Branch br = this.Branch;

                if (this.IsShowLines)
                    using (Pen p = this.GetLinePen()) 
                        this.DrawLines(g, r, p, br);

                if (br.CanExpand) {
                    Rectangle r2 = r;
                    r2.Offset((br.Level - 1) * PIXELS_PER_LEVEL, 0);
                    r2.Width = PIXELS_PER_LEVEL;

                    VisualStyleElement element = VisualStyleElement.TreeView.Glyph.Closed;
                    if (br.IsExpanded)
                        element = VisualStyleElement.TreeView.Glyph.Opened;
                    VisualStyleRenderer renderer = new VisualStyleRenderer(element);
                    renderer.DrawBackground(g, r2);
                }

                int indent = br.Level * PIXELS_PER_LEVEL;
                r.Offset(indent, 0);
                r.Width -= indent;

                this.DrawImageAndText(g, r);
            }

            protected Pen GetLinePen()
            {
                Pen pen =  new Pen(Color.Blue, 1.0f);
                pen.DashStyle = DashStyle.Dot;
                return pen;
            }

            protected void DrawLines(Graphics g, Rectangle r, Pen p, Branch br)
            {
                Rectangle r2 = r;
                r2.Width = PIXELS_PER_LEVEL;
                
                // Draw lines for ancestors
                int midX;
                IList<Branch> ancestors = br.Ancestors;
                foreach (Branch ancestor in ancestors) {
                    if (!ancestor.IsLastChild) {
                        midX = r2.Left + r2.Width / 2;
                        g.DrawLine(p, midX, r2.Top, midX, r2.Bottom);
                    }
                    r2.Offset(PIXELS_PER_LEVEL, 0);
                }

                // Draw lines for this branch
                midX = r2.Left + r2.Width / 2;
                int midY = r2.Top + r2.Height / 2;
                if (br.IsFirstBranch) 
                    g.DrawLine(p, midX, midY, midX, r2.Bottom);
                else {
                    if (br.IsLastChild) 
                        g.DrawLine(p, midX, r2.Top, midX, midY);
                    else
                        g.DrawLine(p, midX, r2.Top, midX, r2.Bottom);
                }
                g.DrawLine(p, midX, midY, r2.Right, midY);
            }
        }
    }
}
