/*
 * Events - All the events that can be triggered within an ObjectListView.
 *
 * Author: Phillip Piper
 * Date: 17/10/2008 9:15 PM
 *
 * Change log:
 * 2009-06-13   JPP  - Added Cell events
 *                   - Moved all event parameter blocks to this file.
 *                   - Added Handled property to AfterSearchEventArgs
 * v2.2
 * 2009-06-01   JPP  - Added ColumnToGroupBy and GroupByOrder to sorting events
                     - Gave all event descriptions
 * 2009-04-23   JPP  - Added drag drop events
 * v2.1
 * 2009-01-18   JPP  - Moved SelectionChanged event to this file
 * v2.0
 * 2008-12-06   JPP  - Added searching events
 * 2008-12-01   JPP  - Added secondary sort information to Before/AfterSorting events
 * 2008-10-17   JPP  - Separated from ObjectListView.cs
 * 
 * Copyright (C) 2006-2009 Phillip Piper
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// The callbacks for CellEditing events
    /// </summary>
    /// <remarks>
    /// We could replace this with EventHandler<CellEditEventArgs> but that would break all
    /// cell editing event code from v1.x.
    /// </remarks>
    public delegate void CellEditEventHandler(object sender, CellEditEventArgs e);

    partial class ObjectListView
    {
        //-----------------------------------------------------------------------------------
        #region Events

        /// <summary>
        /// Triggered after a ObjectListView has been searched by the user typing into the list
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered after the control has done a search-by-typing action.")]
        public event EventHandler<AfterSearchingEventArgs> AfterSearching;

        /// <summary>
        /// Triggered after a ObjectListView has been sorted
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered after the items in the list have been sorted.")]
        public event EventHandler<AfterSortingEventArgs> AfterSorting;

        /// <summary>
        /// Triggered before a ObjectListView is searched by the user typing into the list
        /// </summary>
        /// <remarks>
        /// Set Cancelled to true to prevent the searching from taking place.
        /// Changing StringToFind or StartSearchFrom will change the subsequent search.
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered before the control does a search-by-typing action.")]
        public event EventHandler<BeforeSearchingEventArgs> BeforeSearching;

        /// <summary>
        /// Triggered before a ObjectListView is sorted
        /// </summary>
        /// <remarks>
        /// Set Cancelled to true to prevent the sort from taking place.
        /// Changing ColumnToSort or SortOrder will change the subsequent sort.
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered before the items in the list are sorted.")]
        public event EventHandler<BeforeSortingEventArgs> BeforeSorting;

        /// <summary>
        /// This event is triggered when the user moves a drag over an ObjectListView that
        /// has a SimpleDropSink installed as the drop handler.
        /// </summary>
        /// <remarks>
        /// Handlers for this event should set the Effect argument and optionally the
        /// InfoMsg property. They can also change any of the DropTarget* setttings to change
        /// the target of the drop.
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("Can the user drop the currently dragged items at the current mouse location?")]
        public event EventHandler<OlvDropEventArgs> CanDrop;

        /// <summary>
        /// Triggered when a cell is about to finish being edited.
        /// </summary>
        /// <remarks>If Cancel is already true, the user is cancelling the edit operation.
        /// Set Cancel to true to prevent the value from the cell being written into the model.
        /// You cannot prevent the editing from finishing within this event -- you need
        /// the CellEditValidating event for that.</remarks>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered cell edit operation is finishing.")]
        public event CellEditEventHandler CellEditFinishing;

        /// <summary>
        /// Triggered when a cell is about to be edited.
        /// </summary>
        /// <remarks>Set Cancel to true to prevent the cell being edited.
        /// You can change the the Control to be something completely different.</remarks>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when cell edit is about to begin.")]
        public event CellEditEventHandler CellEditStarting;

        /// <summary>
        /// Triggered when a cell editor needs to be validated
        /// </summary>
        /// <remarks>
        /// If this event is cancelled, focus will remain on the cell editor.
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when a cell editor is about to lose focus and its new contents need to be validated.")]
        public event CellEditEventHandler CellEditValidating;

        /// <summary>
        /// Triggered when a cell is left clicked.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the user left clicks a cell.")]
        public event EventHandler<CellClickEventArgs> CellClick;

        /// <summary>
        /// Triggered when the mouse is above a cell.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the mouse is over a cell.")]
        public event EventHandler<CellOverEventArgs> CellOver;

        /// <summary>
        /// Triggered when a cell is right clicked.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the user right clicks a cell.")]
        public event EventHandler<CellRightClickEventArgs> CellRightClick;

        /// <summary>
        /// This event is triggered when a cell needs a tool tip.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when a cell needs a tool tip.")]
        public event EventHandler<ToolTipShowingEventArgs> CellToolTipShowing;

        /// <summary>
        /// Triggered when a column header is right clicked.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the user right clicks a column header.")]
        public event ColumnRightClickEventHandler ColumnRightClick;

        /// <summary>
        /// This event is triggered when the user releases a drag over an ObjectListView that
        /// has a SimpleDropSink installed as the drop handler.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the user dropped items onto the control.")]
        public event EventHandler<OlvDropEventArgs> Dropped;

        /// <summary>
        /// This event is triggered when a header needs a tool tip.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when a header needs a tool tip.")]
        public event EventHandler<ToolTipShowingEventArgs> HeaderToolTipShowing;

        /// <summary>
        /// Some new objects are about to be added to an ObjectListView.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when objects are about to be added to the control")]
        public event EventHandler<ItemsAddingEventArgs> ItemsAdding;

        /// <summary>
        /// The contents of the ObjectListView has changed.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the contents of the control have changed.")]
        public event EventHandler<ItemsChangedEventArgs> ItemsChanged;

        /// <summary>
        /// The contents of the ObjectListView is about to change via a SetObjects call
        /// </summary>
        /// <remarks>
        /// <para>Set Cancelled to true to prevent the contents of the list changing. This does not work with virtual lists.</para>
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the contents of the control changes.")]
        public event EventHandler<ItemsChangingEventArgs> ItemsChanging;

        /// <summary>
        /// Some objects are about to be removed from an ObjectListView.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when objects are removed from the control.")]
        public event EventHandler<ItemsRemovingEventArgs> ItemsRemoving;

        /// <summary>
        /// This event is triggered when the user moves a drag over an ObjectListView that
        /// has a SimpleDropSink installed as the drop handler, and when the source control
        /// for the drag was an ObjectListView.
        /// </summary>
        /// <remarks>
        /// Handlers for this event should set the Effect argument and optionally the
        /// InfoMsg property. They can also change any of the DropTarget* setttings to change
        /// the target of the drop.
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("Can the dragged collection of model objects be dropped at the current mouse location")]
        public event EventHandler<ModelDropEventArgs> ModelCanDrop;

        /// <summary>
        /// This event is triggered when the user releases a drag over an ObjectListView that
        /// has a SimpleDropSink installed as the drop handler and when the source control
        /// for the drag was an ObjectListView.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("A collection of model objects from a ObjectListView has been dropped on this control")]
        public event EventHandler<ModelDropEventArgs> ModelDropped;

        /// <summary>
        /// This event is triggered once per user action that changes the selection state
        /// of one or more rows.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered once per user action that changes the selection state of one or more rows.")]
        public event EventHandler SelectionChanged;

        /// <summary>
        /// This event is triggered when the contents of the ObjectListView has scrolled.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("This event is triggered when the contents of the ObjectListView has scrolled.")]
        public event EventHandler<ScrollEventArgs> Scroll;

        #endregion

        //-----------------------------------------------------------------------------------
        #region OnEvents

        protected virtual void OnAfterSearching(AfterSearchingEventArgs e) {
            if (this.AfterSearching != null)
                this.AfterSearching(this, e);
        }

        protected virtual void OnAfterSorting(AfterSortingEventArgs e) {
            if (this.AfterSorting != null)
                this.AfterSorting(this, e);
        }

        protected virtual void OnBeforeSearching(BeforeSearchingEventArgs e) {
            if (this.BeforeSearching != null)
                this.BeforeSearching(this, e);
        }

        protected virtual void OnBeforeSorting(BeforeSortingEventArgs e) {
            if (this.BeforeSorting != null)
                this.BeforeSorting(this, e);
        }

        protected virtual void OnCanDrop(OlvDropEventArgs args) {
            if (this.CanDrop != null)
                this.CanDrop(this, args);
        }

        protected virtual void OnCellClick(CellClickEventArgs args) {
            if (this.CellClick != null)
                this.CellClick(this, args);
        }

        protected virtual void OnCellOver(CellOverEventArgs args) {
            if (this.CellOver != null)
                this.CellOver(this, args);
        }

        protected virtual void OnCellRightClick(CellRightClickEventArgs args) {
            if (this.CellRightClick != null)
                this.CellRightClick(this, args);
        }

        protected virtual void OnCellToolTip(ToolTipShowingEventArgs args) {
            if (this.CellToolTipShowing != null)
                this.CellToolTipShowing(this, args);
        }

        protected virtual void OnColumnRightClick(ColumnClickEventArgs e) {
            if (this.ColumnRightClick != null)
                this.ColumnRightClick(this, e);
        }

        protected virtual void OnDropped(OlvDropEventArgs args) {
            if (this.Dropped != null)
                this.Dropped(this, args);
        }

        protected virtual void OnHeaderToolTip(ToolTipShowingEventArgs args) {
            if (this.HeaderToolTipShowing != null)
                this.HeaderToolTipShowing(this, args);
        }

        protected virtual void OnItemsAdding(ItemsAddingEventArgs e) {
            if (this.ItemsAdding != null)
                this.ItemsAdding(this, e);
        }

        protected virtual void OnItemsChanged(ItemsChangedEventArgs e) {
            if (this.ItemsChanged != null)
                this.ItemsChanged(this, e);
        }

        protected virtual void OnItemsChanging(ItemsChangingEventArgs e) {
            if (this.ItemsChanging != null)
                this.ItemsChanging(this, e);
        }

        protected virtual void OnItemsRemoving(ItemsRemovingEventArgs e) {
            if (this.ItemsRemoving != null)
                this.ItemsRemoving(this, e);
        }

        protected virtual void OnModelCanDrop(ModelDropEventArgs args) {
            if (this.ModelCanDrop != null)
                this.ModelCanDrop(this, args);
        }

        protected virtual void OnModelDropped(ModelDropEventArgs args) {
            if (this.ModelDropped != null)
                this.ModelDropped(this, args);
        }

        protected virtual void OnSelectionChanged(EventArgs e) {
            if (this.SelectionChanged != null)
                this.SelectionChanged(this, e);
        }

        protected virtual void OnScroll(ScrollEventArgs e) {
            if (this.Scroll != null)
                this.Scroll(this, e);
        }

        /// <summary>
        /// Tell the world when a cell is about to be edited.
        /// </summary>
        protected virtual void OnCellEditStarting(CellEditEventArgs e) {
            if (this.CellEditStarting != null)
                this.CellEditStarting(this, e);
        }

        /// <summary>
        /// Tell the world when a cell is about to finish being edited.
        /// </summary>
        protected virtual void OnCellEditorValidating(CellEditEventArgs e) {
            // Hack. ListView is an imperfect control container. It does not manage validation
            // perfectly. If the ListView is part of a TabControl, and the cell editor loses
            // focus by the user clicking on another tab, the TabControl processes the click
            // and switches tabs, even if this Validating event cancels. This results in the
            // strange situation where the cell editor is active, but isn't visible. When the
            // user switches back to the tab with the ListView, composite controls like spin
            // controls, DateTimePicker and ComboBoxes do not work properly. Specifically,
            // keyboard input still works fine, but the controls do not respond to mouse
            // input. SO, if the validation fails, we have to specifically give focus back to
            // the cell editor. (this is the Select() call in the code below). 
            // But (there is always a 'but'), doing that changes the focus so the cell editor
            // triggers another Validating event -- which fails again. From the user's point
            // of view, they click away from the cell editor, and the validating code
            // complains twice. So we only trigger a Validating event if more than 0.1 seconds
            // has elapsed since the last validate event.
            // I know it's a hack. I'm very open to hear a neater solution.

            // Also, this timed response stops us from sending a series of validation events
            // if the user clicks and holds on the OLV scroll bar.
            if ((Environment.TickCount - lastValidatingEvent) < 500) {
                e.Cancel = true;
            } else {
                lastValidatingEvent = Environment.TickCount;
                if (this.CellEditValidating != null)
                    this.CellEditValidating(this, e);
            }
            lastValidatingEvent = Environment.TickCount;
        }
        private int lastValidatingEvent = 0;

        /// <summary>
        /// Tell the world when a cell is about to finish being edited.
        /// </summary>
        protected virtual void OnCellEditFinishing(CellEditEventArgs e) {
            if (this.CellEditFinishing != null)
                this.CellEditFinishing(this, e);
        }

        #endregion
    }

    //-----------------------------------------------------------------------------------
    #region Event Parameter Blocks

    /// <summary>
    /// Let the world know that a cell edit operation is beginning or ending
    /// </summary>
    public class CellEditEventArgs : EventArgs
    {
        /// <summary>
        /// Create an event args
        /// </summary>
        /// <param name="column"></param>
        /// <param name="control"></param>
        /// <param name="r"></param>
        /// <param name="item"></param>
        /// <param name="subItemIndex"></param>
        public CellEditEventArgs(OLVColumn column, Control control, Rectangle r, OLVListItem item, int subItemIndex) {
            this.Control = control;
            this.column = column;
            this.cellBounds = r;
            this.listViewItem = item;
            this.rowObject = item.RowObject;
            this.subItemIndex = subItemIndex;
            this.value = column.GetValue(item.RowObject);
        }

        /// <summary>
        /// Change this to true to cancel the cell editing operation.
        /// </summary>
        /// <remarks>
        /// <para>During the CellEditStarting event, setting this to true will prevent the cell from being edited.</para>
        /// <para>During the CellEditFinishing event, if this value is already true, this indicates that the user has
        /// cancelled the edit operation and that the handler should perform cleanup only. Setting this to true,
        /// will prevent the ObjectListView from trying to write the new value into the model object.</para>
        /// </remarks>
        public bool Cancel;

        /// <summary>
        /// During the CellEditStarting event, this can be modified to be the control that you want
        /// to edit the value. You must fully configure the control before returning from the event,
        /// including its bounds and the value it is showing.
        /// During the CellEditFinishing event, you can use this to get the value that the user
        /// entered and commit that value to the model. Changing the control during the finishing
        /// event has no effect.
        /// </summary>
        public Control Control;

        /// <summary>
        /// The column of the cell that is going to be or has been edited.
        /// </summary>
        public OLVColumn Column {
            get { return this.column; }
        }
        private OLVColumn column;

        /// <summary>
        /// The model object of the row of the cell that is going to be or has been edited.
        /// </summary>
        public Object RowObject {
            get { return this.rowObject; }
        }
        private Object rowObject;

        /// <summary>
        /// The listview item of the cell that is going to be or has been edited.
        /// </summary>
        public OLVListItem ListViewItem {
            get { return this.listViewItem; }
        }
        private OLVListItem listViewItem;

        /// <summary>
        /// The index of the cell that is going to be or has been edited.
        /// </summary>
        public int SubItemIndex {
            get { return this.subItemIndex; }
        }
        private int subItemIndex;

        /// <summary>
        /// The data value of the cell before the edit operation began.
        /// </summary>
        public Object Value {
            get { return this.value; }
        }
        private Object value;

        /// <summary>
        /// The bounds of the cell that is going to be or has been edited.
        /// </summary>
        public Rectangle CellBounds {
            get { return this.cellBounds; }
        }
        private Rectangle cellBounds;
    }

    public class CancellableEventArgs : EventArgs
    {
        /// <summary>
        /// Has this event been cancelled by the event handler?
        /// </summary>
        public bool Canceled;
    }

    public class BeforeSortingEventArgs : CancellableEventArgs
    {
        public BeforeSortingEventArgs(OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2) {
            this.ColumnToGroupBy = column;
            this.GroupByOrder = order;
            this.ColumnToSort = column;
            this.SortOrder = order;
            this.SecondaryColumnToSort = column2;
            this.SecondarySortOrder = order2;
        }

        public BeforeSortingEventArgs(OLVColumn groupColumn, SortOrder groupOrder, OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2) {
            this.ColumnToGroupBy = groupColumn;
            this.GroupByOrder = groupOrder;
            this.ColumnToSort = column;
            this.SortOrder = order;
            this.SecondaryColumnToSort = column2;
            this.SecondarySortOrder = order2;
        }

        /// <summary>
        /// Did the event handler already do the sorting for us?
        /// </summary>
        public bool Handled;

        public OLVColumn ColumnToGroupBy;
        public SortOrder GroupByOrder;
        public OLVColumn ColumnToSort;
        public SortOrder SortOrder;
        public OLVColumn SecondaryColumnToSort;
        public SortOrder SecondarySortOrder;
    }

    public class AfterSortingEventArgs : EventArgs
    {
        public AfterSortingEventArgs(OLVColumn groupColumn, SortOrder groupOrder, OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2) {
            this.columnToGroupBy = groupColumn;
            this.groupByOrder = groupOrder;
            this.columnToSort = column;
            this.sortOrder = order;
            this.secondaryColumnToSort = column2;
            this.secondarySortOrder = order2;
        }

        public AfterSortingEventArgs(BeforeSortingEventArgs args) {
            this.columnToGroupBy = args.ColumnToGroupBy;
            this.groupByOrder = args.GroupByOrder;
            this.columnToSort = args.ColumnToSort;
            this.sortOrder = args.SortOrder;
            this.secondaryColumnToSort = args.SecondaryColumnToSort;
            this.secondarySortOrder = args.SecondarySortOrder;
        }

        public OLVColumn ColumnToGroupBy {
            get { return columnToGroupBy; }
        }
        private OLVColumn columnToGroupBy;

        public SortOrder GroupByOrder {
            get { return groupByOrder; }
        }
        private SortOrder groupByOrder;

        public OLVColumn ColumnToSort {
            get { return columnToSort; }
        }
        private OLVColumn columnToSort;

        public SortOrder SortOrder {
            get { return sortOrder; }
        }
        private SortOrder sortOrder;

        public OLVColumn SecondaryColumnToSort {
            get { return secondaryColumnToSort; }
        }
        private OLVColumn secondaryColumnToSort;

        public SortOrder SecondarySortOrder {
            get { return secondarySortOrder; }
        }
        private SortOrder secondarySortOrder;
    }

    /// <summary>
    /// This event is triggered after the items in the list have been changed,
    /// either through SetObjects, AddObjects or RemoveObjects.
    /// </summary>
    public class ItemsChangedEventArgs : EventArgs
    {
        public ItemsChangedEventArgs() {
        }

        /// <summary>
        /// Constructor for this event when used by a virtual list
        /// </summary>
        /// <param name="oldObjectCount"></param>
        /// <param name="newObjectCount"></param>
        public ItemsChangedEventArgs(int oldObjectCount, int newObjectCount) {
            this.oldObjectCount = oldObjectCount;
            this.newObjectCount = newObjectCount;
        }

        public int OldObjectCount {
            get { return oldObjectCount; }
        }
        private int oldObjectCount;

        public int NewObjectCount {
            get { return newObjectCount; }
        }
        private int newObjectCount;
    }

    /// <summary>
    /// This event is triggered by AddObjects before any change has been made to the list.
    /// </summary>
    public class ItemsAddingEventArgs : CancellableEventArgs
    {
        public ItemsAddingEventArgs(ICollection objectsToAdd) {
            this.ObjectsToAdd = objectsToAdd;
        }

        public ICollection ObjectsToAdd;
    }

    /// <summary>
    /// This event is triggered by SetObjects before any change has been made to the list.
    /// </summary>
    /// <remarks>
    /// When used with a virtual list, OldObjects will always be null.
    /// </remarks>
    public class ItemsChangingEventArgs : CancellableEventArgs
    {
        public ItemsChangingEventArgs(IEnumerable oldObjects, IEnumerable newObjects) {
            this.oldObjects = oldObjects;
            this.NewObjects = newObjects;
        }

        public IEnumerable OldObjects {
            get { return oldObjects; }
        }
        private IEnumerable oldObjects;

        public IEnumerable NewObjects;
    }

    /// <summary>
    /// This event is triggered by RemoveObjects before any change has been made to the list.
    /// </summary>
    public class ItemsRemovingEventArgs : CancellableEventArgs
    {
        public ItemsRemovingEventArgs(ICollection objectsToRemove) {
            this.ObjectsToRemove = objectsToRemove;
        }

        public ICollection ObjectsToRemove;
    }

    /// <summary>
    /// Triggered after the user types into a list
    /// </summary>
    public class AfterSearchingEventArgs : EventArgs
    {
        public AfterSearchingEventArgs(string stringToFind, int indexSelected) {
            this.stringToFind = stringToFind;
            this.indexSelected = indexSelected;
        }

        /// <summary>
        /// Gets the string that was actually searched for
        /// </summary>
        public string StringToFind {
            get { return this.stringToFind; }
        }
        private string stringToFind;

        /// <summary>
        /// Gets or sets whether an the event handler already handled this event
        /// </summary>
        public bool Handled;

        /// <summary>
        /// Gets the index of the row that was selected by the search.
        /// -1 means that no row was matched
        /// </summary>
        public int IndexSelected {
            get { return this.indexSelected; }
        }
        private int indexSelected;
    }

    /// <summary>
    /// Triggered when the user types into a list
    /// </summary>
    public class BeforeSearchingEventArgs : CancellableEventArgs
    {
        public BeforeSearchingEventArgs(string stringToFind, int startSearchFrom) {
            this.StringToFind = stringToFind;
            this.StartSearchFrom = startSearchFrom;
        }

        /// <summary>
        /// Gets or sets the string that will be found by the search routine
        /// </summary>
        /// <remarks>Modifying this value does not modify the memory of what the user has typed. 
        /// When the user next presses a character, the search string will revert to what 
        /// the user has actually typed.</remarks>
        public string StringToFind;

        /// <summary>
        /// Gets or sets the index of the first row that will be considered to matching.
        /// </summary>
        public int StartSearchFrom;
    }

    /// <summary>
    /// The parameter block when telling the world about a cell based event
    /// </summary>
    public class CellEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the ObjectListView that is the source of the event
        /// </summary>
        public ObjectListView ListView {
            get { return this.listView; }
            internal set { this.listView = value; }
        }
        private ObjectListView listView;

        /// <summary>
        /// Gets the model object under the cell
        /// </summary>
        /// <remarks>This is null for events triggered by the header.</remarks>
        public object Model {
            get { return this.model; }
            internal set { this.model = value; }
        }
        private object model;

        /// <summary>
        /// Gets the row index of the cell
        /// </summary>
        /// <remarks>This is -1 for events triggered by the header.</remarks>
        public int RowIndex {
            get { return this.rowIndex; }
            internal set { this.rowIndex = value; }
        }
        private int rowIndex = -1;

        /// <summary>
        /// Gets the column index of the cell
        /// </summary>
        /// <remarks>This is -1 when the view is not in details view.</remarks>
        public int ColumnIndex {
            get { return this.columnIndex; }
            internal set { this.columnIndex = value; }
        }
        private int columnIndex = -1;

        /// <summary>
        /// Gets the column of the cell 
        /// </summary>
        /// <remarks>This is null when the view is not in details view.</remarks>
        public OLVColumn Column {
            get { return this.column; }
            internal set { this.column = value; }
        }
        private OLVColumn column;

        /// <summary>
        /// Gets the location of the mouse at the time of the event
        /// </summary>
        public Point Location {
            get { return this.location; }
            internal set { this.location = value; }
        }
        private Point location;

        /// <summary>
        /// Gets the state of the modifier keys at the time of the event
        /// </summary>
        public Keys ModifierKeys {
            get { return this.modifierKeys; }
            internal set { this.modifierKeys = value; }
        }
        private Keys modifierKeys;

        /// <summary>
        /// Gets the item of the cell
        /// </summary>
        public OLVListItem Item {
            get { return item; }
            internal set { this.item = value; }
        }
        private OLVListItem item;

        /// <summary>
        /// Gets the subitem of the cell
        /// </summary>
        /// <remarks>This is null when the view is not in details view and 
        /// for event triggered by the header</remarks>
        public ListViewItem.ListViewSubItem SubItem {
            get { return subItem; }
            internal set { this.subItem = value; }
        }
        private ListViewItem.ListViewSubItem subItem;

        /// <summary>
        /// Gets the HitTest object that determined which cell was hit
        /// </summary>
        public OlvListViewHitTestInfo HitTest {
            get { return hitTest; }
            internal set { hitTest = value;  }
        }
        private OlvListViewHitTestInfo hitTest;
    }

    /// <summary>
    /// Tells the world that a cell was clicked
    /// </summary>
    public class CellClickEventArgs : CellEventArgs
    {
        /// <summary>
        /// Gets or set if this event completelely handled. If it was, no further processing
        /// will be done for it.
        /// </summary>
        public bool Handled;

        /// <summary>
        /// Gets or sets the number of clicks associated with this event
        /// </summary>
        public int ClickCount {
            get { return this.clickCount; }
            set { this.clickCount = value; }
        }
        private int clickCount;
    }

    /// <summary>
    /// Tells the world that a cell was right clicked
    /// </summary>
    public class CellRightClickEventArgs : CellEventArgs
    {
        /// <summary>
        /// Gets or set if this event completelely handled. If it was, no further processing
        /// will be done for it.
        /// </summary>
        public bool Handled;

        /// <summary>
        /// Gets or sets the menu that should be displayed as a result of this event.
        /// </summary>
        /// <remarks>The menu will be positioned at Location, so changing that property changes
        /// where the menu will be displayed.</remarks>
        public ContextMenuStrip MenuStrip;
    }

    public class CellHoverEventArgs : CellEventArgs
    {
    }

    public class CellOverEventArgs : CellEventArgs
    {
    }

    /// <summary>
    /// The parameter block when telling the world that a tool tip is about to be shown.
    /// </summary>
    public class ToolTipShowingEventArgs : CellEventArgs
    {
        /// <summary>
        /// Gets the tooltip control that is triggering the tooltip event
        /// </summary>
        public ToolTipControl ToolTipControl {
            get { return this.toolTipControl; }
            internal set { this.toolTipControl = value; }
        }
        private ToolTipControl toolTipControl;

        /// <summary>
        /// Gets or sets the text should be shown on the tooltip for this event
        /// </summary>
        /// <remarks>Setting this to empty or null prevents any tooltip from showing</remarks>
        public string Text;

        /// <summary>
        /// In what direction should the text for this tooltip be drawn?
        /// </summary>
        public RightToLeft RightToLeft;

        /// <summary>
        /// Should the tooltip for this event been shown in bubble style?
        /// </summary>
        /// <remarks>This doesn't work reliable under Vista</remarks>
        public bool? IsBalloon;

        /// <summary>
        /// What color should be used for the background of the tooltip
        /// </summary>
        /// <remarks>Setting this does nothing under Vista</remarks>
        public Color? BackColor;

        /// <summary>
        /// What color should be used for the foreground of the tooltip
        /// </summary>
        /// <remarks>Setting this does nothing under Vista</remarks>
        public Color? ForeColor;

        /// <summary>
        /// What string should be used as the title for the tooltip for this event?
        /// </summary>
        public string Title;

        /// <summary>
        /// Which standard icon should be used for the tooltip for this event
        /// </summary>
        public ToolTipControl.StandardIcons? StandardIcon;

        /// <summary>
        /// How many milliseconds should the tooltip remain before it automatically
        /// disappears.
        /// </summary>
        public int? AutoPopDelay;

        /// <summary>
        /// What font should be used to draw the text of the tooltip?
        /// </summary>
        public Font Font;
    }

    #endregion
}
