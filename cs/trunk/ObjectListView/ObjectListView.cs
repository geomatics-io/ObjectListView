/*
 * ObjectListView - A listview to show various aspects of a collection of objects
 *
 * Author: Phillip Piper
 * Date: 9/10/2006 11:15 AM
 *
 * Change log
 * 2009-08-03  JPP  - Subitem edit rectangles always allowed for an image in the cell, even if there was none.
 *                    Now they only allow for an image when there actually is one.
 *                  - Added Bounds property to OLVListItem which handles items being part of collapsed groups.
 * 2009-07-26  JPP  - Avoided bug in .NET framework involving column 0 of owner drawn listviews not being
 *                    redrawn when the listview was scrolled horizontally (this was a LOT of work to track
 *                    down and fix!)
 *                  - The cell edit rectangle is now correctly calculated when the listview is scrolled 
 *                    horizontally.
 * 2009-07-14  JPP  - If the user clicks/double clicks on a tree list cell, an edit operation will no longer begin
 *                    if the click was to the left of the expander. This is implemented in such a way that
 *                    other renderers can have similar "dead" zones.
 * 2009-07-11  JPP  - CalculateCellBounds() messed with the FullRowSelect property, which confused the
 *                    tooltip handling on the underlying control. It no longer does this.
 *                  - The cell edit rectangle is now correctly calculated for owner-drawn, non-Details views.
 * 2009-07-08  JPP  - Added Cell events (CellClicked, CellOver, CellRightClicked)
 *                  - Made BuildList(), AddObject() and RemoveObject() thread-safe
 * 2009-07-04  JPP  - Space bar now properly toggles checkedness of selected rows
 * 2009-07-02  JPP  - Fixed bug with tooltips when the underlying Windows control was destroyed.
 *                  - CellToolTipShowing events are now triggered in all views.
 * v2.2
 * 2009-06-02  JPP  - BeforeSortingEventArgs now has a Handled property to let event handlers do
 *                    the item sorting themselves.
 *                  - AlwaysGroupByColumn works again, as does SortGroupItemsByPrimaryColumn and all their
 *                    various permutations.
 *                  - SecondarySortOrder and SecondarySortColumn are now "null" by default
 * 2009-05-15  JPP  - Fixed bug so that KeyPress events are again triggered
 * 2009-05-10  JPP  - Removed all unsafe code
 * 2009-05-07  JPP  - Don't use glass panel for overlays when in design mode. It's too confusing.
 * 2009-05-05  JPP  - Added Scroll event (thanks to Christophe Hosten for the complete patch to implement this)
 *                  - Added Unfocused foreground and background colors (also thanks to Christophe Hosten)
 * 2009-04-29  JPP  - Added SelectedColumn property, which puts a slight tint on that column. Combine
 *                    this with TintSortColumn property and the sort column is automatically tinted.
 *                  - Use an overlay to implement "empty list" msg. Default empty list msg is now prettier.
 * 2009-04-28  JPP  - Fixed bug where DoubleClick events were not triggered when CheckBoxes was true
 * 2009-04-23  JPP  - Fixed various bugs under Vista.
 *                  - Made groups collapsible - Vista only. Thanks to Crustyapplesniffer.
 *                  - Forward events from DropSink to the control itself. This allows handlers to be defined
 *                    within the IDE for drop events
 * 2009-04-16  JPP  - Made several properties localizable.
 * 2009-04-11  JPP  - Correctly renderer checkboxes when RowHeight is non-standard
 * 2009-04-11  JPP  - Implemented overlay architecture, based on CustomDraw scheme.
 *                    This unified drag drop feedback, empty list msgs and overlay images.
 *                  - Added OverlayImage and friends, which allows an image to be drawn
 *                    transparently over the listview
 * 2009-04-10  JPP  - Fixed long-standing annoying flicker on owner drawn virtual lists!
 *                    This means, amongst other things, that grid lines no longer get confused,
 *                    and drag-select no longer flickers.
 * 2009-04-07  JPP  - Calculate edit rectangles more accurately
 * 2009-04-06  JPP  - Double-clicking no longer toggles the checkbox
 *                  - Double-clicking on a checkbox no longer confuses the checkbox
 * 2009-03-16  JPP  - Optimized the build of autocomplete lists
 * v2.1
 * 2009-02-24  JPP  - Fix bug where double-clicking VERY quickly on two different cells
 *                    could give two editors
 *                  - Maintain focused item when rebuilding list (SF #2547060)
 * 2009-02-22  JPP  - Reworked checkboxes so that events are triggered for virtual lists
 * 2009-02-15  JPP  - Added ObjectListView.ConfigureAutoComplete utility method
 * 2009-02-02  JPP  - Fixed bug with AlwaysGroupByColumn where column header clicks would not resort groups.
 * 2009-02-01  JPP  - OLVColumn.CheckBoxes and TriStateCheckBoxes now work.
 * 2009-01-28  JPP  - Complete overhaul of renderers!
 *                       - Use IRenderer
 *                       - Added ObjectListView.ItemRenderer to draw whole items
 * 2009-01-23  JPP  - Simple Checkboxes now work properly
 *                  - Added TriStateCheckBoxes property to control whether the user can
 *                    set the row checkbox to have the Indeterminate value
 *                  - CheckState property is now just a wrapper around the StateImageIndex property
 * 2009-01-20  JPP  - Changed to always draw columns when owner drawn, rather than falling back on DrawDefault.
 *                    This simplified several owner drawn problems
 *                  - Added DefaultRenderer property to help with the above
 *                  - HotItem background color is applied to all cells even when FullRowSelect is false
 *                  - Allow grouping by CheckedAspectName columns
 *                  - Commented out experimental animations. Still needs work.
 * 2009-01-17  JPP  - Added HotItemStyle and UseHotItem to highlight the row under the cursor
 *                  - Added UseCustomSelectionColors property
 *                  - Owner draw mode now honors ForeColor and BackColor settings on the list
 * 2009-01-16  JPP  - Changed to use EditorRegistry rather than hard coding cell editors
 * 2009-01-10  JPP  - Changed to use Equals() method rather than == to compare model objects.
 * v2.0.1
 * 2009-01-08  JPP  - Fixed long-standing "multiple columns generated" problem.
 *                    Thanks to pinkjones for his help with solving this one!
 *                  - Added EnsureGroupVisible()
 * 2009-01-07  JPP  - Made all public and protected methods virtual
 *                  - FinishCellEditing, PossibleFinishCellEditing and CancelCellEditing are now public
 * 2008-12-20  JPP  - Fixed bug with group comparisons when a group key was null (SF#2445761)
 * 2008-12-19  JPP  - Fixed bug with space filling columns and layout events
 *                  - Fixed RowHeight so that it only changes the row height, not the width of the images.
 * v2.0
 * 2008-12-10  JPP  - Handle Backspace key. Resets the seach-by-typing state without delay
 *                  - Made some changes to the column collection editor to try and avoid
 *                    the multiple column generation problem.
 *                  - Updated some documentation
 * 2008-12-07  JPP  - Search-by-typing now works when showing groups
 *                  - Added BeforeSearching and AfterSearching events which are triggered when the user types
 *                    into the list.
 *                  - Added secondary sort information to Before/AfterSorting events
 *                  - Reorganized group sorting code. Now triggers Sorting events.
 *                  - Added GetItemIndexInDisplayOrder()
 *                  - Tweaked in the interaction of the column editor with the IDE so that we (normally)
 *                    don't rely on a hack to find the owning ObjectListView
 *                  - Changed all 'DefaultValue(typeof(Color), "Empty")' to 'DefaultValue(typeof(Color), "")'
 *                    since the first does not given Color.Empty as I thought, but the second does.
 * 2008-11-28  JPP  - Fixed long standing bug with horizontal scrollbar when shrinking the window.
 *                    (thanks to Bartosz Borowik)
 * 2008-11-25  JPP  - Added support for dynamic tooltips
 *                  - Split out comparers and header controls stuff into their own files
 * 2008-11-21  JPP  - Fixed bug where enabling grouping when there was not a sort column would not
 *                    produce a grouped list. Grouping column now defaults to column 0.
 *                  - Preserve selection on virtual lists when sorting
 * 2008-11-20  JPP  - Added ability to search by sort column to ObjectListView. Unified this with
 *                    ability that was already in VirtualObjectListView
 * 2008-11-19  JPP  - Fixed bug in ChangeToFilteredColumns() where DisplayOrder was not always restored correctly.
 * 2008-10-29  JPP  - Event argument blocks moved to directly within the namespace, rather than being
 *                    nested inside ObjectListView class.
 *                  - Removed OLVColumn.CellEditor since it was never used.
 *                  - Marked OLVColumn.AspectGetterAutoGenerated as obsolete (it has not been used for
 *                    several versions now).
 * 2008-10-28  JPP  - SelectedObjects is now an IList, rather than an ArrayList. This allows
 *                    it to accept generic list (eg List<File>).
 * 2008-10-09  JPP  - Support indeterminate checkbox values.
 *                    [BREAKING CHANGE] CheckStateGetter/CheckStatePutter now use CheckState types only.
 *                    BooleanCheckStateGetter and BooleanCheckStatePutter added to ease transition.
 * 2008-10-08  JPP  - Added setFocus parameter to SelectObject(), which allows focus to be set
 *                    at the same time as selecting.
 * 2008-09-27  JPP  - BIG CHANGE: Fissioned this file into separate files for each component
 * 2008-09-24  JPP  - Corrected bug with owner drawn lists where a column 0 with a renderer
 *                    would draw at column 0 even if column 0 was dragged to another position.
 *                  - Correctly handle space filling columns when columns are added/removed
 * 2008-09-16  JPP  - Consistently use try..finally for BeginUpdate()/EndUpdate() pairs
 * 2008-08-24  JPP  - If LastSortOrder is None when adding objects, don't force a resort.
 * 2008-08-22  JPP  - Catch and ignore some problems with setting TopIndex on FastObjectListViews.
 * 2008-08-05  JPP  - In the right-click column select menu, columns are now sorted by display order, rather than alphabetically
 * v1.13
 * 2008-07-23  JPP  - Consistently use copy-on-write semantics with Add/RemoveObject methods
 * 2008-07-10  JPP  - Enable validation on cell editors through a CellEditValidating event.
 *                    (thanks to Artiom Chilaru for the initial suggestion and implementation).
 * 2008-07-09  JPP  - Added HeaderControl.Handle to allow OLV to be used within UserControls.
 *                    (thanks to Michael Coffey for tracking this down).
 * 2008-06-23  JPP  - Split the more generally useful CopyObjectsToClipboard() method
 *                    out of CopySelectionToClipboard()
 * 2008-06-22  JPP  - Added AlwaysGroupByColumn and AlwaysGroupBySortOrder, which
 *                    force the list view to always be grouped by a particular column.
 * 2008-05-31  JPP  - Allow check boxes on FastObjectListViews
 *                  - Added CheckedObject and CheckedObjects properties
 * 2008-05-11  JPP  - Allow selection foreground and background colors to be changed.
 *                    Windows doesn't allow this, so we can only make it happen when owner
 *                    drawing. Set the HighlightForegroundColor and  HighlightBackgroundColor
 *                    properties and then call EnableCustomSelectionColors().
 * v1.12
 * 2008-05-08  JPP  - Fixed bug where the column select menu would not appear if the
 *                    ObjectListView has a context menu installed.
 * 2008-05-05  JPP  - Non detail views can now be owner drawn. The renderer installed for
 *                    primary column is given the chance to render the whole item.
 *                    See BusinessCardRenderer in the demo for an example.
 *                  - BREAKING CHANGE: RenderDelegate now returns a bool to indicate if default
 *                    rendering should be done. Previously returned void. Only important if your
 *                    code used RendererDelegate directly. Renderers derived from BaseRenderer
 *                    are unchanged.
 * 2008-05-03  JPP  - Changed cell editing to use values directly when the values are Strings.
 *                    Previously, values were always handed to the AspectToStringConverter.
 *                  - When editing a cell, tabbing no longer tries to edit the next subitem
 *                    when not in details view!
 * 2008-05-02  JPP  - MappedImageRenderer can now handle a Aspects that return a collection
 *                    of values. Each value will be drawn as its own image.
 *                  - Made AddObjects() and RemoveObjects() work for all flavours (or at least not crash)
 *                  - Fixed bug with clearing virtual lists that has been scrolled vertically
 *                  - Made TopItemIndex work with virtual lists.
 * 2008-05-01  JPP  - Added AddObjects() and RemoveObjects() to allow faster mods to the list
 *                  - Reorganised public properties. Now alphabetical.
 *                  - Made the class ObjectListViewState internal, as it always should have been.
 * v1.11
 * 2008-04-29  JPP  - Preserve scroll position when building the list or changing columns.
 *                  - Added TopItemIndex property. Due to problems with the underlying control, this
 *                    property is not always reliable. See property docs for info.
 * 2008-04-27  JPP  - Added SelectedIndex property.
 *                  - Use a different, more general strategy to handle Invoke(). Removed all delegates
 *                    that were only declared to support Invoke().
 *                  - Check all native structures for 64-bit correctness.
 * 2008-04-25  JPP  - Released on SourceForge.
 * 2008-04-13  JPP  - Added ColumnRightClick event.
 *                  - Made the assembly CLS-compliant. To do this, our cell editors were made internal, and
 *                    the constraint on FlagRenderer template parameter was removed (the type must still
 *                    be an IConvertible, but if it isn't, the error will be caught at runtime, not compile time).
 * 2008-04-12  JPP  - Changed HandleHeaderRightClick() to have a columnIndex parameter, which tells
 *                    exactly which column was right-clicked.
 * 2008-03-31  JPP  - Added SaveState() and RestoreState()
 *                  - When cell editing, scrolling with a mouse wheel now ends the edit operation.
 * v1.10
 * 2008-03-25  JPP  - Added space filling columns. See OLVColumn.FreeSpaceProportion property for details.
 *                    A space filling columns fills all (or a portion) of the width unoccupied by other columns.
 * 2008-03-23  JPP  - Finished tinkering with support for Mono. Compile with conditional compilation symbol 'MONO'
 *                    to enable. On Windows, current problems with Mono:
 *                    - grid lines on virtual lists crashes
 *                    - when grouped, items sometimes are not drawn when any item is scrolled out of view
 *                    - i can't seem to get owner drawing to work
 *                    - when editing cell values, the editing controls always appear behind the listview,
 *                      where they function fine -- the user just can't see them :-)
 * 2008-03-16  JPP  - Added some methods suggested by Chris Marlowe (thanks for the suggestions Chris)
 *                    - ClearObjects()
 *                    - GetCheckedObject(), GetCheckedObjects()
 *                    - GetItemAt() variation that gets both the item and the column under a point
 * 2008-02-28  JPP  - Fixed bug with subitem colors when using OwnerDrawn lists and a RowFormatter.
 * v1.9.1
 * 2008-01-29  JPP  - Fixed bug that caused owner-drawn virtual lists to use 100% CPU
 *                  - Added FlagRenderer to help draw bitwise-OR'ed flag values
 * 2008-01-23  JPP  - Fixed bug (introduced in v1.9) that made alternate row colour with groups not quite right
 *                  - Ensure that DesignerSerializationVisibility.Hidden is set on all non-browsable properties
 *                  - Make sure that sort indicators are shown after changing which columns are visible
 * 2008-01-21  JPP  - Added FastObjectListView
 * v1.9
 * 2008-01-18  JPP  - Added IncrementalUpdate()
 * 2008-01-16  JPP  - Right clicking on column header will allow the user to choose which columns are visible.
 *                    Set SelectColumnsOnRightClick to false to prevent this behaviour.
 *                  - Added ImagesRenderer to draw more than one images in a column
 *                  - Changed the positioning of the empty list m to use all the client area. Thanks to Matze.
 * 2007-12-13  JPP  - Added CopySelectionToClipboard(). Ctrl-C invokes this method. Supports text
 *                    and HTML formats.
 * 2007-12-12  JPP  - Added support for checkboxes via CheckStateGetter and CheckStatePutter properties.
 *                  - Made ObjectListView and OLVColumn into partial classes so that others can extend them.
 * 2007-12-09  JPP  - Added ability to have hidden columns, i.e. columns that the ObjectListView knows
 *                    about but that are not visible to the user. Controlled by OLVColumn.IsVisible.
 *                    Added ColumnSelectionForm to the project to show how it could be used in an application.
 *
 * v1.8
 * 2007-11-26  JPP  - Cell editing fully functional
 * 2007-11-21  JPP  - Added SelectionChanged event. This event is triggered once when the
 *                    selection changes, no matter how many items are selected or deselected (in
 *                    contrast to SelectedIndexChanged which is called once for every row that
 *                    is selected or deselected). Thanks to lupokehl42 (Daniel) for his suggestions and
 *                    improvements on this idea.
 * 2007-11-19  JPP  - First take at cell editing
 * 2007-11-17  JPP  - Changed so that items within a group are not sorted if lastSortOrder == None
 *                  - Only call MakeSortIndicatorImages() if we haven't already made the sort indicators
 *                    (Corrected misspelling in the name of the method too)
 * 2007-11-06  JPP  - Added ability to have secondary sort criteria when sorting
 *                    (SecondarySortColumn and SecondarySortOrder properties)
 *                  - Added SortGroupItemsByPrimaryColumn to allow group items to be sorted by the
 *                    primary column. Previous default was to sort by the grouping column.
 * v1.7
 * No big changes to this version but made to work with ListViewPrinter and released with it.
 *
 * 2007-11-05  JPP  - Changed BaseRenderer to use DrawString() rather than TextRenderer, since TextRenderer
 *                    does not work when printing.
 * v1.6
 * 2007-11-03  JPP  - Fixed some bugs in the rebuilding of DataListView.
 * 2007-10-31  JPP  - Changed to use builtin sort indicators on XP and later. This also avoids alignment
 *                    problems on Vista. (thanks to gravybod for the suggestion and example implementation)
 * 2007-10-21  JPP  - Added MinimumWidth and MaximumWidth properties to OLVColumn.
 *                  - Added ability for BuildList() to preserve selection. Calling BuildList() directly
 *                    tries to preserve selection; calling SetObjects() does not.
 *                  - Added SelectAll() and DeselectAll() methods. Useful for working with large lists.
 * 2007-10-08  JPP  - Added GetNextItem() and GetPreviousItem(), which walk sequentially through the
 *                    listview items, even when the view is grouped.
 *                  - Added SelectedItem property
 * 2007-09-28  JPP  - Optimized aspect-to-string conversion. BuildList() 15% faster.
 *                  - Added empty implementation of RefreshObjects() to VirtualObjectListView since
 *                    RefreshObjects() cannot work on virtual lists.
 * 2007-09-13  JPP  - Corrected bug with custom sorter in VirtualObjectListView (thanks for mpgjunky)
 * 2007-09-07  JPP  - Corrected image scaling bug in DrawAlignedImage() (thanks to krita970)
 * 2007-08-29  JPP  - Allow item count labels on groups to be set per column (thanks to cmarlow for idea)
 * 2007-08-14  JPP  - Major rework of DataListView based on Ian Griffiths's great work
 * 2007-08-11  JPP  - When empty, the control can now draw a "List Empty" m
 *                  - Added GetColumn() and GetItem() methods
 * v1.5
 * 2007-08-03  JPP  - Support animated GIFs in ImageRenderer
 *                  - Allow height of rows to be specified - EXPERIMENTAL!
 * 2007-07-26  JPP  - Optimised redrawing of owner-drawn lists by remembering the update rect
 *                  - Allow sort indicators to be turned off
 * 2007-06-30  JPP  - Added RowFormatter delegate
 *                  - Allow a different label when there is only one item in a group (thanks to cmarlow)
 * v1.4
 * 2007-04-12  JPP  - Allow owner drawn on steriods!
 *                  - Column headers now display sort indicators
 *                  - ImageGetter delegates can now return ints, strings or Images
 *                    (Images are only visible if the list is owner drawn)
 *                  - Added OLVColumn.MakeGroupies to help with group partitioning
 *                  - All normal listview views are now supported
 *                  - Allow dotted aspect names, e.g. Owner.Workgroup.Name (thanks to OlafD)
 *                  - Added SelectedObject and SelectedObjects properties
 * v1.3
 * 2007-03-01  JPP  - Added DataListView
 *                  - Added VirtualObjectListView
 * 					- Added Freeze/Unfreeze capabilities
 *                  - Allowed sort handler to be installed
 *                  - Simplified sort comparisons: handles 95% of cases with only 6 lines of code!
 *                  - Fixed bug with alternative line colors on unsorted lists (thanks to cmarlow)
 * 2007-01-13  JPP  - Fixed bug with lastSortOrder (thanks to Kwan Fu Sit)
 *                  - Non-OLVColumns are no longer allowed
 * 2007-01-04  JPP  - Clear sorter before rebuilding list. 10x faster! (thanks to aaberg)
 *                  - Include GetField in GetAspectByName() so field values can be Invoked too.
 * 					- Fixed subtle bug in RefreshItem() that erased background colors.
 * 2006-11-01  JPP  - Added alternate line colouring
 * 2006-10-20  JPP  - Refactored all sorting comparisons and made it extendable. See ComparerManager.
 *                  - Improved IDE integration
 *                  - Made control DoubleBuffered
 *                  - Added object selection methods
 * 2006-10-13  JPP  Implemented grouping and column sorting
 * 2006-10-09  JPP  Initial version
 *
 * TO DO:
 * - Drag and drop support
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// An object list displays 'aspects' of a collection of objects in a listview control.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The intelligence for this control is in the columns. OLVColumns are
    /// extended so they understand how to fetch an 'aspect' from each row
    /// object. They also understand how to sort by their aspect, and
    /// how to group them.
    /// </para>
    /// <para>
    /// Aspects are extracted by giving the name of a method to be called or a
    /// property to be fetched. These names can be simple names or they can be dotted
    /// to chain property access e.g. "Owner.Address.Postcode".
    /// Aspects can also be extracted by installing a delegate.
    /// </para>
    /// <para>
    /// Sorting by column clicking and grouping by column are handled automatically.
    /// </para>
    /// <para>
    /// Right clicking on the column header should present a popup menu that allows the user to
    /// choose which columns will be visible in the list. This behaviour can be disabled by
    /// setting SelectColumnsOnRightClick to false.
    /// </para>
    /// <para>
    /// This list puts sort indicators in the column headers to show the column sorting direction.
    /// On Windows XP and later, the system standard images are used.
    /// If you wish to replace the standard images with your own images, put entries in the small image list
    /// with the key values "sort-indicator-up" and "sort-indicator-down".
    /// </para>
    /// <para>
    /// For these classes to build correctly, the project must have references to these assemblies:
    /// <list>
    /// <item>System</item>
    /// <item>System.Data</item>
    /// <item>System.Design</item>
    /// <item>System.Drawing</item>
    /// <item>System.Windows.Forms (obviously)</item>
    /// </list>
    /// </para>
    /// </remarks>
    public partial class ObjectListView : ListView, ISupportInitialize
    {
        /// <summary>
        /// Create an ObjectListView
        /// </summary>
        public ObjectListView()
            : base() {
            this.ColumnClick += new ColumnClickEventHandler(this.HandleColumnClick);
            this.Layout += new LayoutEventHandler(this.HandleLayout);
            this.ColumnWidthChanging += new ColumnWidthChangingEventHandler(this.HandleColumnWidthChanging);
            this.ColumnWidthChanged += new ColumnWidthChangedEventHandler(this.HandleColumnWidthChanged);

            base.View = View.Details;
            this.DoubleBuffered = true; // kill nasty flickers. hiss... me hates 'em
            this.ShowSortIndicators = true;

            // Setup the overlays that will be controlled by the IDE settings
            this.InitializeStandardOverlays();
            this.InitializeEmptyListMsgOverlay();
        }

        #region Public properties

        /// <summary>
        /// Get or set all the columns that this control knows about.
        /// Only those columns where IsVisible is true will be seen by the user.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you want to add new columns programmatically, add them to
        /// AllColumns and then call RebuildColumns(). Normally, you do not have to
        /// deal with this property directly. Just use the IDE.
        /// </para>
        /// <para>If you do add or remove columns from the AllColumns collection,
        /// you have to call RebuildColumns() to make those changes take effect.</para>
        /// </remarks>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual List<OLVColumn> AllColumns {
            get {
                return this.allColumns;
            }
            set {
                if (value == null)
                    this.allColumns = new List<OLVColumn>();
                else
                    this.allColumns = value;
            }
        }
        private List<OLVColumn> allColumns = new List<OLVColumn>();

        /// <summary>
        /// If every second row has a background different to the control, what color should it be?
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("If using alternate colors, what foregroundColor should alterate rows be?"),
         DefaultValue(typeof(Color), "")]
        public Color AlternateRowBackColor {
            get { return alternateRowBackColor; }
            set { alternateRowBackColor = value; }
        }
        private Color alternateRowBackColor = Color.Empty;

        /// <summary>
        /// Return the alternate row background color that has been set, or the default color
        /// </summary>
        [Browsable(false)]
        public virtual Color AlternateRowBackColorOrDefault {
            get {
                if (alternateRowBackColor == Color.Empty)
                    return Color.LemonChiffon;
                else
                    return alternateRowBackColor;
            }
        }

        /// <summary>
        /// This property forces the ObjectListView to always group items by the given column.
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual OLVColumn AlwaysGroupByColumn {
            get { return alwaysGroupByColumn; }
            set { alwaysGroupByColumn = value; }
        }
        private OLVColumn alwaysGroupByColumn;

        /// <summary>
        /// If AlwaysGroupByColumn is not null, this property will be used to decide how
        /// those groups are sorted. If this property has the value SortOrder.None, then
        /// the sort order will toggle according to the users last header click.
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortOrder AlwaysGroupBySortOrder {
            get { return alwaysGroupBySortOrder; }
            set { alwaysGroupBySortOrder = value; }
        }
        private SortOrder alwaysGroupBySortOrder = SortOrder.None;

        /// <summary>
        /// Give access to the image list that is actually being used by the control
        /// </summary>
        /// <remarks>
        /// Normally, it is preferable to use SmallImageList. Only use this property
        /// if you know exactly what you are doing.
        /// </remarks>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ImageList BaseSmallImageList {
            get { return base.SmallImageList; }
            set { base.SmallImageList = value; }
        }

        /// <summary>
        /// How does a user indicate that they want to edit cells?
        /// </summary>
        public enum CellEditActivateMode
        {
            /// <summary>
            /// This list cannot be edited. F2 does nothing.
            /// </summary>
            None = 0,

            /// <summary>
            /// A single click on  a <strong>subitem</strong> will edit the value. Single clicking the primary column,
            /// selects the row just like normal. The user must press F2 to edit the primary column.
            /// </summary>
            SingleClick = 1,

            /// <summary>
            /// Double clicking a subitem or the primary column will edit that cell.
            /// F2 will edit the primary column.
            /// </summary>
            DoubleClick = 2,

            /// <summary>
            /// Pressing F2 is the only way to edit the cells. Once the primary column is being edited,
            /// the other cells in the row can be edited by pressing Tab.
            /// </summary>
            F2Only = 3
        }

        /// <summary>
        /// How does the user indicate that they want to edit a cell?
        /// None means that the listview cannot be edited.
        /// </summary>
        /// <remarks>Columns can also be marked as editable.</remarks>
        [Category("Behavior - ObjectListView"),
        Description("How does the user indicate that they want to edit a cell?"),
        DefaultValue(CellEditActivateMode.None)]
        public virtual CellEditActivateMode CellEditActivation {
            get { return cellEditActivation; }
            set { cellEditActivation = value; }
        }
        private CellEditActivateMode cellEditActivation = CellEditActivateMode.None;

        /// <summary>
        /// Gets the tool tip that shows tips for the cells
        /// </summary>
        public ToolTipControl CellToolTip {
            get {
                if (this.cellToolTip == null) {
                    this.CreateCellToolTip();
                }
                return this.cellToolTip;
            }
        }
        private ToolTipControl cellToolTip;

        /// <summary>
        /// Should this list show checkboxes?
        /// </summary>
        public new bool CheckBoxes {
            get {
                return base.CheckBoxes;
            }
            set {
                base.CheckBoxes = value;

                // Initialize the state image list so we can display indetermined values.
                this.InitializeStateImageList();
            }
        }

        /// <summary>
        /// Return the model object of the row that is checked or null if no row is checked
        /// or more than one row is checked
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Object CheckedObject {
            get {
                IList checkedObjects = this.CheckedObjects;
                if (checkedObjects.Count == 1)
                    return checkedObjects[0];
                else
                    return null;
            }
            set {
                this.CheckedObjects = new ArrayList(new Object[] { value });
            }
        }

        /// <summary>
        /// Get or set the collection of model objects that are checked.
        /// When setting this property, any row whose model object isn't
        /// in the given collection will be unchecked. Setting to null is
        /// equivilent to unchecking all.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property returns a simple collection. Changes made to the returned
        /// collection do NOT affect the list. This is different to the behaviour of
        /// CheckedIndicies collection.
        /// </para>
        /// <para>
        /// .NET's CheckedItems property is not helpful. It is just a short-hand for
        /// iterating through the list looking for items that are checked.
        /// </para>
        /// <para>
        /// The performance of the get method is O(n), where n is the number of items
        /// in the control. The performance of the set method is
        /// O(n*m) where m is the number of objects being checked. Be careful on long lists.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IList CheckedObjects {
            get {
                ArrayList objects = new ArrayList();
                if (this.CheckBoxes) {
                    for (int i = 0; i < this.GetItemCount(); i++) {
                        OLVListItem olvi = this.GetItem(i);
                        if (olvi.CheckState == CheckState.Checked)
                            objects.Add(olvi.RowObject);
                    }
                }
                return objects;
            }
            set {
                if (!this.CheckBoxes)
                    return;

                if (value == null)
                    value = new ArrayList();

                foreach (Object x in this.Objects) {
                    if (value.Contains(x))
                        this.SetObjectCheckedness(x, CheckState.Checked);
                    else
                        this.SetObjectCheckedness(x, CheckState.Unchecked);
                }
            }
        }

        /// <summary>
        /// Get/set the list of columns that should be used when the list switches to tile view.
        /// </summary>
        [Browsable(false),
        Obsolete("Use GetFilteredColumns() and OLVColumn.IsTileViewColumn instead"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<OLVColumn> ColumnsForTileView {
            get { return this.GetFilteredColumns(View.Tile); }
        }

        /// <summary>
        /// Return the visible columns in the order they are displayed to the user
        /// </summary>
        [Browsable(false)]
        public virtual List<OLVColumn> ColumnsInDisplayOrder {
            get {
                List<OLVColumn> columnsInDisplayOrder = new List<OLVColumn>(this.Columns.Count);
                for (int i = 0; i < this.Columns.Count; i++)
                    columnsInDisplayOrder.Add(null);
                for (int i = 0; i < this.Columns.Count; i++) {
                    OLVColumn col = this.GetColumn(i);
                    columnsInDisplayOrder[col.DisplayIndex] = col;
                }
                return columnsInDisplayOrder;
            }
        }

        /// <summary>
        /// Get the area of the control that shows the list, minus any header control
        /// </summary>
        [Browsable(false)]
        public Rectangle  ContentRectangle {
            get {
                Rectangle r = this.ClientRectangle;

                // If the listview has a header control, remove the header from the control area
                if (this.View == View.Details && this.HeaderControl != null) {
                    Rectangle hdrBounds = new Rectangle();
                    NativeMethods.GetClientRect(this.HeaderControl.Handle, ref hdrBounds);
                    r.Y = hdrBounds.Height;
                    r.Height = r.Height - hdrBounds.Height;
                }

                return r;
            }
        }

        /// <summary>
        /// When owner drawing, this renderer will draw columns that do not have specific renderer
        /// given to them
        /// </summary>
        /// <remarks>If you try to set this to null, it will revert to a BaseRenderer</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IRenderer DefaultRenderer {
            get { return defaultRenderer; }
            set {
                if (value == null)
                    defaultRenderer = new BaseRenderer();
                else
                    defaultRenderer = value;
            }
        }
        private IRenderer defaultRenderer = new BaseRenderer();

        /// <summary>
        /// Gets or sets the object that controls how drags start from this control
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDragSource DragSource {
            get { return this.dragSource; }
            set { this.dragSource = value; }
        }
        private IDragSource dragSource;

        /// <summary>
        /// Gets or sets the object that controls how drops are accepted and processed
        /// by this ListView.
        /// </summary>
        /// <remarks>
        /// If the given sink is an instance of SimpleDropSink, then events from the drop sink
        /// will be automatically forwarded to the ObjectListView (which means that handlers
        /// for those event can be configured within the IDE).
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDropSink DropSink {
            get { return this.dropSink; }
            set {
                if (this.dropSink == value)
                    return;

                // Stop listening for events on the old sink
                SimpleDropSink oldSink = this.dropSink as SimpleDropSink;
                if (oldSink != null) {
                    oldSink.CanDrop -= new EventHandler<OlvDropEventArgs>(dropSink_CanDrop);
                    oldSink.Dropped -= new EventHandler<OlvDropEventArgs>(dropSink_Dropped);
                    oldSink.ModelCanDrop -= new EventHandler<ModelDropEventArgs>(dropSink_ModelCanDrop);
                    oldSink.ModelDropped -= new EventHandler<ModelDropEventArgs>(dropSink_ModelDropped);
                }

                this.dropSink = value;
                this.AllowDrop = (value != null);
                if (this.dropSink != null)
                    this.dropSink.ListView = this;

                // Start listening for events on the new sink
                SimpleDropSink newSink = value as SimpleDropSink;
                if (newSink != null) {
                    newSink.CanDrop += new EventHandler<OlvDropEventArgs>(dropSink_CanDrop);
                    newSink.Dropped += new EventHandler<OlvDropEventArgs>(dropSink_Dropped);
                    newSink.ModelCanDrop += new EventHandler<ModelDropEventArgs>(dropSink_ModelCanDrop);
                    newSink.ModelDropped += new EventHandler<ModelDropEventArgs>(dropSink_ModelDropped);
                }
            }
        }
        private IDropSink dropSink;

        // Forward events from the drop sink to the control itself
        void dropSink_CanDrop(object sender, OlvDropEventArgs e) { this.OnCanDrop(e); }
        void dropSink_Dropped(object sender, OlvDropEventArgs e) { this.OnDropped(e); }
        void dropSink_ModelCanDrop(object sender, ModelDropEventArgs e) { this.OnModelCanDrop(e); }
        void dropSink_ModelDropped(object sender, ModelDropEventArgs e) { this.OnModelDropped(e); }

        /// <summary>
        /// This registry decides what control should be used to edit what cells, based
        /// on the type of the value in the cell.
        /// </summary>
        /// <see cref="EditorRegistry"/>
        /// <remarks>All instances of ObjectListView share the same editor registry.</remarks>
        static public EditorRegistry EditorRegistry = new EditorRegistry();

        /// <summary>
        /// Gets or sets the text that should be shown when there are no items in this list view.
        /// </summary>
        /// <remarks>If the EmptyListMsgOverlay has been changed to something other than a TextOverlay,
        /// this property does nothing</remarks>
        [Category("Appearance - ObjectListView"),
         Description("When the list has no items, show this m in the control"),
         DefaultValue(null),
         Localizable(true)]
        public virtual String EmptyListMsg {
            get {
                TextOverlay overlay = this.EmptyListMsgOverlay as TextOverlay;
                if (overlay == null)
                    return null;
                else
                    return overlay.Text;
            }
            set {
                TextOverlay overlay = this.EmptyListMsgOverlay as TextOverlay;
                if (overlay != null)
                    overlay.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the font in which the List Empty message should be drawn
        /// </summary>
        /// <remarks>If the EmptyListMsgOverlay has been changed to something other than a TextOverlay,
        /// this property does nothing</remarks>
        [Category("Appearance - ObjectListView"),
        Description("What font should the 'list empty' message be drawn in?"),
        DefaultValue(null)]
        public virtual Font EmptyListMsgFont {
            get {
                TextOverlay overlay = this.EmptyListMsgOverlay as TextOverlay;
                if (overlay == null)
                    return null;
                else
                    return overlay.Font;
            }
            set {
                TextOverlay overlay = this.EmptyListMsgOverlay as TextOverlay;
                if (overlay != null)
                    overlay.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets the overlay responsible for drawing the List Empty msg.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IOverlay EmptyListMsgOverlay {
            get { return this.emptyListMsgOverlay; }
            set {
                if (this.emptyListMsgOverlay != value) {
                    this.emptyListMsgOverlay = value;
                    this.Invalidate();
                }
            }
        }
        private IOverlay emptyListMsgOverlay;

        /// <summary>
        /// Return the font for the 'list empty' m or a default
        /// </summary>
        [Browsable(false)]
        public virtual Font EmptyListMsgFontOrDefault {
            get {
                if (this.EmptyListMsgFont == null)
                    return new Font("Tahoma", 14);
                else
                    return this.EmptyListMsgFont;
            }
        }

        /// <summary>
        /// Get or set whether or not the listview is frozen. When the listview is
        /// frozen, it will not update itself.
        /// </summary>
        /// <remarks><para>The Frozen property is similar to the methods Freeze()/Unfreeze()
        /// except that changes to the Frozen property do not nest.</para></remarks>
        /// <example>objectListView1.Frozen = false; // unfreeze the control regardless of the number of Freeze() calls
        /// </example>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Frozen {
            get { return freezeCount > 0; }
            set {
                if (value)
                    Freeze();
                else if (freezeCount > 0) {
                    freezeCount = 1;
                    Unfreeze();
                }
            }
        }
        private int freezeCount = 0;

        /// <summary>
        /// When a group title has an item count, how should the lable be formatted?
        /// </summary>
        /// <remarks>
        /// The given format string can/should have two placeholders:
        /// <list type="bullet">
        /// <item>{0} - the original group title</item>
        /// <item>{1} - the number of items in the group</item>
        /// </list>
        /// </remarks>
        /// <example>"{0} [{1} items]"</example>
        [Category("Behavior - ObjectListView"),
         Description("The format to use when suffixing item counts to group titles"),
         DefaultValue(null),
         Localizable(true)]
        public virtual string GroupWithItemCountFormat {
            get { return groupWithItemCountFormat; }
            set { groupWithItemCountFormat = value; }
        }
        private string groupWithItemCountFormat;

        /// <summary>
        /// Return this.GroupWithItemCountFormat or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual string GroupWithItemCountFormatOrDefault {
            get {
                if (String.IsNullOrEmpty(this.GroupWithItemCountFormat))
                    return "{0} [{1} items]";
                else
                    return this.GroupWithItemCountFormat;
            }
        }

        /// <summary>
        /// When a group title has an item count, how should the lable be formatted if
        /// there is only one item in the group?
        /// </summary>
        /// <remarks>
        /// The given format string can/should have two placeholders:
        /// <list type="bullet">
        /// <item>{0} - the original group title</item>
        /// <item>{1} - the number of items in the group (always 1)</item>
        /// </list>
        /// </remarks>
        /// <example>"{0} [{1} item]"</example>
        [Category("Behavior - ObjectListView"),
         Description("The format to use when suffixing item counts to group titles"),
         DefaultValue(null),
         Localizable(true)]
        public virtual string GroupWithItemCountSingularFormat {
            get { return groupWithItemCountSingularFormat; }
            set { groupWithItemCountSingularFormat = value; }
        }
        private string groupWithItemCountSingularFormat;

        /// <summary>
        /// Return this.GroupWithItemCountSingularFormat or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual string GroupWithItemCountSingularFormatOrDefault {
            get {
                if (String.IsNullOrEmpty(this.GroupWithItemCountSingularFormat))
                    return "{0} [{1} item]";
                else
                    return this.GroupWithItemCountSingularFormat;
            }
        }

        /// <summary>
        /// Gets or sets whether or now the groups in this ObjectListView should be collapsible.
        /// </summary>
        /// <remarks>
        /// This feature only works under Vista and later.
        /// </remarks>
        [Browsable(true),
         Category("Appearance - ObjectListView"),
         Description("Should the groups in this control be collapsible (Vista only)."),
         DefaultValue(true)]
        public bool HasCollapsibleGroups {
            get { return hasCollapsibleGroup; }
            set { hasCollapsibleGroup = value; }
        }
        private bool hasCollapsibleGroup = true;

        /// <summary>
        /// Does this listview have a m that should be drawn when the list is empty?
        /// </summary>
        [Browsable(false)]
        public virtual bool HasEmptyListMsg {
            get { return !String.IsNullOrEmpty(this.EmptyListMsg); }
        }

        /// <summary>
        /// Get whether there are any overlays to be drawn
        /// </summary>
        [Browsable(false)]
        public bool HasOverlays {
            get {
                return (this.Overlays.Count > 2 ||
                    this.imageOverlay.Image != null ||
                    !String.IsNullOrEmpty(this.textOverlay.Text));
            }
        }

        /// <summary>
        /// Gets the tool tip that shows tips for the column headers
        /// </summary>
        public ToolTipControl HeaderToolTip {
            get {
                return this.HeaderControl.ToolTip;
            }
        }

        /// <summary>
        /// The index of the item that is 'hot', i.e. under the cursor. -1 means no item.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int HotItemIndex {
            get { return hotItemIndex; }
            set { hotItemIndex = value; }
        }
        private int hotItemIndex = -1;

        /// <summary>
        /// What sort of formatting should be applied to the row under the cursor?
        /// </summary>
        /// <remarks>This only takes effect when UseHotItem is true.</remarks>
        [Category("Appearance - ObjectListView"),
         Description("How should the row under the cursor be highlighted"),
         DefaultValue(null)]
        public virtual HotItemStyle HotItemStyle {
            get { return this.hotItemStyle; }
            set { this.hotItemStyle = value; }
        }
        private HotItemStyle hotItemStyle;

        /// <summary>
        /// What color should be used for the background of selected rows?
        /// </summary>
        /// <remarks>Windows does not give the option of changing the selection background.
        /// So the control has to be owner drawn to see the result of this setting.
        /// Setting UseCustomSelectionColors = true will do this for you.</remarks>
        [Category("Appearance - ObjectListView"),
         Description("The background foregroundColor of selected rows when the control is owner drawn"),
         DefaultValue(typeof(Color), "")]
        public virtual Color HighlightBackgroundColor {
            get { return highlightBackgroundColor; }
            set { highlightBackgroundColor = value; }
        }
        private Color highlightBackgroundColor = Color.Empty;

        /// <summary>
        /// Return the color should be used for the background of selected rows or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual Color HighlightBackgroundColorOrDefault {
            get {
                if (this.HighlightBackgroundColor.IsEmpty)
                    return SystemColors.Highlight;
                else
                    return this.HighlightBackgroundColor;
            }
        }

        /// <summary>
        /// What color should be used for the foreground of selected rows?
        /// </summary>
        /// <remarks>Windows does not give the option of changing the selection foreground (text color).
        /// So the control has to be owner drawn to see the result of this setting.
        /// Setting UseCustomSelectionColors = true will do this for you.</remarks>
        [Category("Appearance - ObjectListView"),
         Description("The foreground foregroundColor of selected rows when the control is owner drawn"),
         DefaultValue(typeof(Color), "")]
        public virtual Color HighlightForegroundColor {
            get { return highlightForegroundColor; }
            set { highlightForegroundColor = value; }
        }
        private Color highlightForegroundColor = Color.Empty;

        /// <summary>
        /// Return the color should be used for the foreground of selected rows or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual Color HighlightForegroundColorOrDefault {
            get {
                if (this.HighlightForegroundColor.IsEmpty)
                    return SystemColors.HighlightText;
                else
                    return this.HighlightForegroundColor;
            }
        }

        /// <summary>
        /// Return true if a cell edit operation is currently happening
        /// </summary>
        [Browsable(false)]
        public virtual bool IsCellEditing {
            get { return this.cellEditor != null; }
        }

        /// <summary>
        /// When the user types into a list, should the values in the current sort column be searched to find a match?
        /// If this is false, the primary column will always be used regardless of the sort column.
        /// </summary>
        /// <remarks>When this is true, the behavior is like that of ITunes.</remarks>
        [Category("Behavior - ObjectListView"),
        Description("When the user types into a list, should the values in the current sort column be searched to find a match?"),
        DefaultValue(true)]
        public virtual bool IsSearchOnSortColumn {
            get { return isSearchOnSortColumn; }
            set { isSearchOnSortColumn = value; }
        }
        private bool isSearchOnSortColumn = true;

        /// <summary>
        /// Gets or sets if this control will use a SimpleDropSink to receive drops
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this replaces any previous DropSink.
        /// </para>
        /// <para>
        /// After setting this to true, the SimpleDropSink will still need to be configured
        /// to say when it can accept drops and what should happen when something is dropped.
        /// The need to do these things makes this property mostly useless :(
        /// </para>
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("Should this control will use a SimpleDropSink to receive drops."),
        DefaultValue(false)]
        public virtual bool IsSimpleDropSink {
            get { return this.DropSink != null; }
            set {
                if (value)
                    this.DropSink = new SimpleDropSink();
                else
                    this.DropSink = null;
            }
        }

        /// <summary>
        /// Gets or sets if this control will use a SimpleDragSource to initiate drags
        /// </summary>
        /// <remarks>Setting this replaces any previous DragSource</remarks>
        [Category("Behavior - ObjectListView"),
        Description("Should this control use a SimpleDragSource to initiate drags out from this control"),
        DefaultValue(false)]
        public virtual bool IsSimpleDragSource {
            get { return this.DragSource != null; }
            set {
                if (value)
                    this.DragSource = new SimpleDragSource();
                else
                    this.DragSource = null;
            }
        }

        /// <summary>
        /// This renderer draws the items when in the list is in non-details view.
        /// In details view, the renderers for the individuals columns are responsible.
        /// </summary>
        [Category("Appearance - ObjectListView"),
        Description("The owner drawn renderer that draws items when the list is in non-Details view."),
        DefaultValue(null)]
        public IRenderer ItemRenderer {
            get { return itemRenderer; }
            set { itemRenderer = value; }
        }
        private IRenderer itemRenderer;

        /// <summary>
        /// Which column did we last sort by
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual OLVColumn LastSortColumn {
            get { return this.lastSortColumn; }
            set {
                this.lastSortColumn = value;
                if (this.TintSortColumn) {
                    this.SelectedColumn = value;
                }
            }
        }
        private OLVColumn lastSortColumn;

        /// <summary>
        /// Which direction did we last sort
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortOrder LastSortOrder {
            get { return lastSortOrder; }
            set { lastSortOrder = value; }
        }
        private SortOrder lastSortOrder;

        /// <summary>
        /// Get/set the collection of objects that this list will show
        /// </summary>
        /// <remarks>
        /// <para>
        /// The contents of the control will be updated immediately after setting this property.
        /// </para>
        /// <para>This method preserves selection, if possible. Use SetObjects() if
        /// you do not want to preserve the selection. Preserving selection is the slowest part of this
        /// code and performance is O(n) where n is the number of selected rows.</para>
        /// <para>This method is not thread safe.</para>
        /// <para>The property DOES work on virtual lists: setting is problem-free, but if you try to get it
        /// and the list has 10 million objects, it may take some time to return.</para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IEnumerable Objects {
            get {
                if (this.VirtualMode) {
                    ArrayList contents = new ArrayList(this.GetItemCount());
                    for (int i = 0; i < this.GetItemCount(); i++)
                        contents.Add(this.GetModelObject(i));
                    return contents;
                } else
                    return this.objects;
            }
            set {
                this.BeginUpdate();
                try {
                    IList previousSelection = this.SelectedObjects;
                    this.SetObjects(value);
                    this.SelectedObjects = previousSelection;
                }
                finally {
                    this.EndUpdate();
                }
            }
        }
        private IEnumerable objects;

        /// <summary>
        /// Gets or sets the image that will be drawn over the top of the ListView
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The image that will be drawn over the top of the ListView"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         RefreshProperties(RefreshProperties.Repaint)]
        public ImageOverlay OverlayImage {
            get { return this.imageOverlay; }
            set { this.imageOverlay = value; }
        }

        /// <summary>
        /// Gets or sets the text that will be drawn over the top of the ListView
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("The text that will be drawn over the top of the ListView"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextOverlay OverlayText {
            get { return this.textOverlay; }
            set { 
                this.textOverlay = value;
                
                if (this.Created)
                    this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the transparency of all the overlays. 
        /// 0 is completely transparent, 255 is completely opaque.
        /// </summary>
        /// <remarks>
        /// This is the default value, but individual overlays can ignore it if they wish.
        /// </remarks>
        [Category("Appearance - ObjectListView"),
        Description("How transparent should overlays be? 0 is completely transparent, 255 is completely opaque"),
         DefaultValue(128)]
        public int OverlayTransparency {
            get { return this.overlayTransparency; }
            set { 
                this.overlayTransparency = Math.Min(255, Math.Max(0, value));
                if (this.glassPanel != null)
                    this.glassPanel.UpdateTransparency();
            }
        }
        private int overlayTransparency = 128;

        /// <summary>
        /// Gets the list of overlays that will be drawn on top of the ListView
        /// </summary>
        /// <remarks>
        /// You can add new overlays and remove overlays that you have added, but 
        /// don't mess with the overlays that you didn't create.
        /// </remarks>
        [Browsable(false)]
        protected IList<IOverlay> Overlays {
            get { return this.overlays; }
        }
        private List<IOverlay> overlays = new List<IOverlay>();

        /// <summary>
        /// Gets the list of decorations that will be drawn the ListView
        /// </summary>
        /// <remarks>
        /// A decoration scrolls with the list contents. An overlay is fixed in place.
        /// </remarks>
        [Browsable(false)]
        protected IList<IOverlay> Decorations {
            get { return this.decorations; }
        }
        private List<IOverlay> decorations = new List<IOverlay>();

        /// <summary>
        /// Specify the height of each row in the control in pixels.
        /// </summary>
        /// <remarks><para>The row height in a listview is normally determined by the font size and the small image list size.
        /// This setting allows that calculation to be overridden (within reason: you still cannot set the line height to be
        /// less than the line height of the font used in the control). </para>
        /// <para>Setting it to -1 means use the normal calculation method.</para>
        /// <para><bold>This feature is experiemental!</bold> Strange things may happen to your program,
        /// your spouse or your pet if you use it.</para>
        /// </remarks>
        [Category("Appearance - ObjectListView"),
         Description("Specify the height of each row in pixels. -1 indicates default height"),
         DefaultValue(-1)]
        public virtual int RowHeight {
            get { return rowHeight; }
            set {
                if (value < 1)
                    rowHeight = -1;
                else
                    rowHeight = value;
                this.SetupBaseImageList();
                if (this.CheckBoxes)
                    this.InitializeStateImageList();
            }
        }
        private int rowHeight = -1;

        /// <summary>
        /// How many pixels high is each row?
        /// </summary>
        [Browsable(false)]
        public virtual int RowHeightEffective {
            get {
                switch (this.View) {
                    case View.List:
                    case View.SmallIcon:
                    case View.Details:
                        return Math.Max(this.SmallImageSize.Height, this.Font.Height);

                    case View.Tile:
                        return this.TileSize.Height;

                    case View.LargeIcon:
                        if (this.LargeImageList == null)
                            return this.Font.Height;
                        else
                            return Math.Max(this.LargeImageList.ImageSize.Height, this.Font.Height);

                    default:
                        // This should never happen
                        return 0;
                }
            }
        }

        /// <summary>
        /// How many rows appear on each page of this control
        /// </summary>
        [Browsable(false)]
        public virtual int RowsPerPage {
            get {
                return NativeMethods.GetCountPerPage(this);
            }
        }

        /// <summary>
        /// Get/set the column that will be used to resolve comparisons that are equal when sorting.
        /// </summary>
        /// <remarks>There is no user interface for this setting. It must be set programmatically.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual OLVColumn SecondarySortColumn {
            get { return this.secondarySortColumn; }
            set { this.secondarySortColumn = value; }
        }
        private OLVColumn secondarySortColumn;

        /// <summary>
        /// When the SecondarySortColumn is used, in what order will it compare results?
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortOrder SecondarySortOrder {
            get { return this.secondarySortOrder; }
            set { this.secondarySortOrder = value; }
        }
        private SortOrder secondarySortOrder = SortOrder.None;

        /// <summary>
        /// When the user right clicks on the column headers, should a menu be presented which will allow
        /// them to choose which columns will be shown in the view?
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("When the user right clicks on the column headers, should a menu be presented which will allow them to choose which columns will be shown in the view?"),
        DefaultValue(true)]
        public virtual bool SelectColumnsOnRightClick {
            get { return selectColumnsOnRightClick; }
            set { selectColumnsOnRightClick = value; }
        }
        private bool selectColumnsOnRightClick = true;

        /// <summary>
        /// When the column select menu is open, should it stay open after an item is selected?
        /// Staying open allows the user to turn more than one column on or off at a time.
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("When the column select menu is open, should it stay open after an item is selected?"),
        DefaultValue(true)]
        public virtual bool SelectColumnsMenuStaysOpen {
            get { return selectColumnsMenuStaysOpen; }
            set { selectColumnsMenuStaysOpen = value; }
        }
        private bool selectColumnsMenuStaysOpen = true;

        /// <summary>
        /// Gets or sets the column that is drawn with a slight tint. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If TintSortColumn is true, the sort column will automatically
        /// be made the selected column.
        /// </para>
        /// <para>
        /// The colour of the tint is controlled by SelectedColumnTint.
        /// </para>
        /// </remarks>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OLVColumn SelectedColumn {
            get { return this.selectedColumn; }
            set {
                this.selectedColumn = value;
                if (value == null) {
                    this.RemoveDecoration(this.selectedColumnOverlay);
                } else {
                    if (!this.HasDecoration(this.selectedColumnOverlay)) 
                        this.AddDecoration(this.selectedColumnOverlay);
                }
            }
        }
        private OLVColumn selectedColumn;
        private TintedColumnDecoration selectedColumnOverlay = new TintedColumnDecoration();

        /// <summary>
        /// What color should be used to tint the selected column?
        /// </summary>
        /// <remarks>
        /// The tint color must be alpha-blendable, so if the given color is solid
        /// (i.e. alpha = 255), it will be changed to have a reasonable alpha value.
        /// </remarks>
        [Category("Appearance - ObjectListView"),
         Description("The color that will be used to tint the selected column"),
         DefaultValue(typeof(Color), "")]
        public virtual Color SelectedColumnTint {
            get { return selectedColumnTint; }
            set {
                this.selectedColumnTint = value;
                if (this.selectedColumnTint.A == 255)
                    this.selectedColumnTint = Color.FromArgb(15, value);
                this.selectedColumnOverlay.Tint = this.selectedColumnTint;
            }
        }
        private Color selectedColumnTint = Color.Empty;

        /// <summary>
        /// Return the index of the row that is currently selected. If no row is selected,
        /// or more than one is selected, return -1.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectedIndex {
            get {
                if (this.SelectedIndices.Count == 1)
                    return this.SelectedIndices[0];
                else
                    return -1;
            }
            set {
                this.SelectedIndices.Clear();
                if (value >= 0 && value < this.Items.Count)
                    this.SelectedIndices.Add(value);
            }
        }

        /// <summary>
        /// Get the ListViewItem that is currently selected . If no row is selected, or more than one is selected, return null.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ListViewItem SelectedItem {
            get {
                if (this.SelectedIndices.Count == 1)
                    return this.GetItem(this.SelectedIndices[0]);
                else
                    return null;
            }
            set {
                this.SelectedIndices.Clear();
                if (value != null)
                    this.SelectedIndices.Add(value.Index);
            }
        }

        /// <summary>
        /// Get the model object from the currently selected row. If no row is selected, or more than one is selected, return null.
        /// Select the row that is displaying the given model object. All other rows are deselected.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Object SelectedObject {
            get { return this.GetSelectedObject(); }
            set { this.SelectObject(value); }
        }

        /// <summary>
        /// Get the model objects from the currently selected rows. If no row is selected, the returned List will be empty.
        /// When setting this value, select the rows that is displaying the given model objects. All other rows are deselected.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IList SelectedObjects {
            get { return this.GetSelectedObjects(); }
            set { this.SelectObjects(value); }
        }

        /// <summary>
        /// Should the list view show a bitmap in the column header to show the sort direction?
        /// </summary>
        /// <remarks>
        /// The only reason for not wanting to have sort indicators is that, on pre-XP versions of
        /// Windows, having sort indicators required the ListView to have a small image list, and
        /// as soon as you give a ListView a SmallImageList, the text of column 0 is bumped 16
        /// pixels to the right, even if you never used an image.
        /// </remarks>
        [Category("Appearance - ObjectListView"),
         Description("Should the list view show sort indicators in the column headers?"),
         DefaultValue(true)]
        public virtual bool ShowSortIndicators {
            get { return showSortIndicators; }
            set { showSortIndicators = value; }
        }
        private bool showSortIndicators;

        /// <summary>
        /// Should the list view show images on subitems?
        /// </summary>
        /// <remarks>
        /// <para>Virtual lists have to be owner drawn in order to show images on subitems?</para>
        /// </remarks>
        [Category("Appearance - ObjectListView"),
         Description("Should the list view show images on subitems?"),
         DefaultValue(false)]
        public virtual bool ShowImagesOnSubItems {
            get {
#if MONO
                return false;
#else
                return showImagesOnSubItems;
#endif
            }
            set {
                showImagesOnSubItems = value;
                if (value && this.VirtualMode)
                    this.OwnerDraw = true;
            }
        }
        private bool showImagesOnSubItems;

        /// <summary>
        /// This property controls whether group labels will be suffixed with a count of items.
        /// </summary>
        /// <remarks>
        /// The format of the suffix is controlled by GroupWithItemCountFormat/GroupWithItemCountSingularFormat properties
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("Will group titles be suffixed with a count of the items in the group?"),
         DefaultValue(false)]
        public virtual bool ShowItemCountOnGroups {
            get { return showItemCountOnGroups; }
            set { showItemCountOnGroups = value; }
        }
        private bool showItemCountOnGroups;

        /// <summary>
        /// Override the SmallImageList property so we can correctly shadow its operations.
        /// </summary>
        /// <remarks><para>If you use the RowHeight property to specify the row height, the SmallImageList
        /// must be fully initialised before setting/changing the RowHeight. If you add new images to the image
        /// list after setting the RowHeight, you must assign the imagelist to the control again. Something as simple
        /// as this will work:
        /// <code>listView1.SmallImageList = listView1.SmallImageList;</code></para>
        /// </remarks>
        new public ImageList SmallImageList {
            get { return this.shadowedImageList; }
            set {
                this.shadowedImageList = value;
                if (this.UseSubItemCheckBoxes)
                    this.SetupSubItemCheckBoxes();
                this.SetupBaseImageList();
            }
        }
        private ImageList shadowedImageList = null;

        /// <summary>
        /// Return the size of the images in the small image list or a reasonable default
        /// </summary>
        public virtual Size SmallImageSize {
            get {
                if (this.SmallImageList == null)
                    return new Size(16, 16);
                else
                    return this.SmallImageList.ImageSize;
            }
        }

        /// <summary>
        /// When the listview is grouped, should the items be sorted by the primary column?
        /// If this is false, the items will be sorted by the same column as they are grouped.
        /// </summary>
        [Category("Behavior - ObjectListView"),
         Description("When the listview is grouped, should the items be sorted by the primary column? If this is false, the items will be sorted by the same column as they are grouped."),
         DefaultValue(true)]
        public virtual bool SortGroupItemsByPrimaryColumn {
            get { return this.sortGroupItemsByPrimaryColumn; }
            set { this.sortGroupItemsByPrimaryColumn = value; }
        }
        private bool sortGroupItemsByPrimaryColumn = true;

        /// <summary>
        /// Should the sort column show a slight tinge?
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("Should the sort column show a slight tinting?"),
         DefaultValue(false)]
        public virtual bool TintSortColumn {
            get { return this.tintSortColumn; }
            set { 
                this.tintSortColumn = value;
                if (value && this.LastSortColumn != null)
                    this.SelectedColumn = this.LastSortColumn;
                else
                    this.SelectedColumn = null;
            }
        }
        private bool tintSortColumn;

        /// <summary>
        /// Should each row have a tri-state checkbox?
        /// </summary>
        /// <remarks>
        /// If this is true, the user can choose the third state (normally Indeterminate). Otherwise, user clicks
        /// alternate between checked and unchecked. CheckStateGetter can still return Indeterminate when this
        /// setting is false.
        /// </remarks>
        [Category("Appearance - ObjectListView"),
         Description("Should the primary column have a checkbox that behaves as a tri-state checkbox?"),
         DefaultValue(false)]
        public virtual bool TriStateCheckBoxes {
            get { return triStateCheckBoxes; }
            set {
                triStateCheckBoxes = value;
                if (value && !this.CheckBoxes)
                    this.CheckBoxes = true;
                this.InitializeStateImageList();
            }
        }
        private bool triStateCheckBoxes;

        /// <summary>
        /// Get or set the index of the top item of this listview
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property only works when the listview is in Details view and not showing groups.
        /// </para>
        /// <para>
        /// The reason that it does not work when showing groups is that, when groups are enabled,
        /// the Windows msg LVM_GETTOPINDEX always returns 0, regardless of the
        /// scroll position.
        /// </para>
        /// </remarks>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int TopItemIndex {
            get {
                if (this.View == View.Details && this.TopItem != null)
                    return this.TopItem.Index;
                else
                    return -1;
            }
            set {
                int newTopIndex = Math.Min(value, this.GetItemCount() - 1);
                if (this.View == View.Details && newTopIndex >= 0) {
                    this.TopItem = this.Items[newTopIndex];

                    // Setting the TopItem sometimes gives off by one errors,
                    // that (bizarrely) are correct on a second attempt
                    if (this.TopItem != null && this.TopItem.Index != newTopIndex)
                        this.TopItem = this.GetItem(newTopIndex);
                }
            }
        }

        /// <summary>
        /// When resizing a column by dragging its divider, should any space filling columns be
        /// resized at each mouse move? If this is false, the filling columns will be
        /// updated when the mouse is released.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In previous versions, setting this to true produced ugly behaviour, because every
        /// column to the right of the divider being dragged was updated twice: once when
        /// the column be resized changes size (this moves all the columns slightly to the right);
        /// then again when the filling columns are updated - they are shrunk
        /// so that the combined width is not more than the control, so everything jumps slightly back to the left again.
        /// </para>
        /// <para>
        /// But, as of v2.0, the change the Windows messages in place, so there is now only one update,
        /// and everything looks nice and smooth.
        /// </para>
        /// <para>
        /// However, it still looks odd when the space filling column
        /// is in the left of the column that is being resized: the right edge of the column is dragged, but
        /// its <b>left</b> edge moves, since the space filling column is shrinking.
        /// </para>
        /// <para>Given the above behavior is probably best to turn this property off if your space filling
        /// columns aren't the right-most columns.</para>
        /// </remarks>
        [Category("Behavior - ObjectListView"),
        Description("When resizing a column by dragging its divider, should any space filling columns be resized at each mouse move?"),
        DefaultValue(true)]
        public virtual bool UpdateSpaceFillingColumnsWhenDraggingColumnDivider {
            get { return updateSpaceFillingColumnsWhenDraggingColumnDivider; }
            set { updateSpaceFillingColumnsWhenDraggingColumnDivider = value; }
        }
        private bool updateSpaceFillingColumnsWhenDraggingColumnDivider = true;

        /// <summary>
        /// What color should be used for the background of selected rows when the control doesn't have the focus?
        /// </summary>
        /// <remarks>Windows does not give the option of changing the selection background.
        /// So the control has to be owner drawn to see the result of this setting.
        /// Setting UseCustomSelectionColors = true will do this for you.</remarks>
        [Category("Appearance - ObjectListView"),
         Description("The background color of selected rows when the control is owner drawn and doesn't have the focus"),
         DefaultValue(typeof(Color), "")]
        public virtual Color UnfocusedHighlightBackgroundColor {
            get { return unfocusedHighlightBackgroundColor; }
            set { unfocusedHighlightBackgroundColor = value; }
        }
        private Color unfocusedHighlightBackgroundColor = Color.Empty;

        /// <summary>
        /// Return the color should be used for the background of selected rows when the control doesn't have the focus or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual Color UnfocusedHighlightBackgroundColorOrDefault {
            get {
                if (this.UnfocusedHighlightBackgroundColor.IsEmpty)
                    return SystemColors.Control;
                else
                    return this.UnfocusedHighlightBackgroundColor;
            }
        }
 
        /// <summary>
        /// What color should be used for the foreground of selected rows when the control doesn't have the focus?
        /// </summary>
        /// <remarks>Windows does not give the option of changing the selection foreground (text color).
        /// So the control has to be owner drawn to see the result of this setting.
        /// Setting UseCustomSelectionColors = true will do this for you.</remarks>
        [Category("Appearance - ObjectListView"),
         Description("The foreground color of selected rows when the control is owner drawn and doesn't have the focus"),
         DefaultValue(typeof(Color), "")]
        public virtual Color UnfocusedHighlightForegroundColor {
            get { return unfocusedHighlightForegroundColor; }
            set { unfocusedHighlightForegroundColor = value; }
        }
        private Color unfocusedHighlightForegroundColor = Color.Empty;

        /// <summary>
        /// Return the color should be used for the foreground of selected rows when the control doesn't have the focus or a reasonable default
        /// </summary>
        [Browsable(false)]
        public virtual Color UnfocusedHighlightForegroundColorOrDefault {
            get {
                if (this.UnfocusedHighlightForegroundColor.IsEmpty)
                    return SystemColors.ControlText;
                else
                    return this.UnfocusedHighlightForegroundColor;
            }
        }

        /// <summary>
        /// Should the list give a different background color to every second row?
        /// </summary>
        /// <remarks><para>The color of the alternate rows is given by AlternateRowBackColor.</para>
        /// <para>There is a "feature" in .NET for listviews in non-full-row-select mode, where
        /// selected rows are not drawn with their correct background color.</para></remarks>
        [Category("Appearance - ObjectListView"),
         Description("Should the list view use a different backcolor to alternate rows?"),
         DefaultValue(false)]
        public virtual bool UseAlternatingBackColors {
            get { return useAlternatingBackColors; }
            set { useAlternatingBackColors = value; }
        }
        private bool useAlternatingBackColors;

        /// <summary>
        /// Should the selected row be drawn with non-standard foreground and background colors?
        /// </summary>
        /// <remarks>
        /// When this is enabled, the control becomes owner drawn.
        /// </remarks>
        [Category("Appearance - ObjectListView"),
         Description("Should the selected row be drawn with non-standard foreground and background colors?"),
         DefaultValue(false)]
        public bool UseCustomSelectionColors {
            get { return this.useCustomSelectionColors; }
            set {
                this.useCustomSelectionColors = value;

                if (!this.DesignMode && value)
                    this.OwnerDraw = true;
            }
        }
        private bool useCustomSelectionColors;

        /// <summary>
        /// Should the item under the cursor be formatted in a special way?
        /// </summary>
        [Category("Appearance - ObjectListView"),
         Description("Should HotTracking be used? Hot tracking applies special formatting to the row under the cursor"),
         DefaultValue(false)]
        public bool UseHotItem {
            get { return this.useHotItem; }
            set { this.useHotItem = value; }
        }
        private bool useHotItem;

        /// <summary>
        /// Should this control be configured to show check boxes on subitems?
        /// </summary>
        /// <remarks>If this is set to True, the control will be given a SmallImageList if it
        /// doesn't already have one. Also, if it is a virtual list, it will be set to owner
        /// drawn, since virtual lists can't draw check boxes without being owner drawn.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("Should this control be configured to show check boxes on subitems."),
         DefaultValue(false)]
        public bool UseSubItemCheckBoxes {
            get { return this.useSubItemCheckBoxes; }
            set {
                this.useSubItemCheckBoxes = value;
                if (value)
                    this.SetupSubItemCheckBoxes();
            }
        }
        private bool useSubItemCheckBoxes;

        /// <summary>
        /// Get/set the style of view that this listview is using
        /// </summary>
        /// <remarks>Switching to tile or details view installs the columns appropriate to that view.
        /// Confusingly, in tile view, every column is shown as a row of information.</remarks>
        new public View View {
            get { return base.View; }
            set {
                if (base.View == value)
                    return;

                if (this.Frozen) {
                    base.View = value;
                    this.SetupBaseImageList();
                } else {
                this.Freeze();

                // If we are switching to a Detail or Tile view, setup the columns needed for that view
                if (value == View.Details || value == View.Tile) {
                    this.ChangeToFilteredColumns(value);

                    if (value == View.Tile)
                        this.CalculateReasonableTileSize();
                }

                base.View = value;
                this.SetupBaseImageList();
                this.Unfreeze();
            }
        }
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// This delegate fetches the checkedness of an object as a boolean only.
        /// </summary>
        /// <remarks>Use this if you never want to worry about the
        /// Indeterminate state (which is fairly common).
        /// <para>
        /// This is a convenience wrapper around the CheckStateGetter property.
        /// </para>
        /// </remarks>
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual BooleanCheckStateGetterDelegate BooleanCheckStateGetter {
            set {
                if (value == null)
                    this.CheckStateGetter = null;
                else
                    this.CheckStateGetter = delegate(Object x) {
                        return value(x) ? CheckState.Checked : CheckState.Unchecked;
                    };
            }
        }

        /// <summary>
        /// This delegate sets the checkedness of an object as a boolean only. It must return
        /// true or false indicating if the object was checked or not.
        /// </summary>
        /// <remarks>Use this if you never want to worry about the
        /// Indeterminate state (which is fairly common).
        /// <para>
        /// This is a convenience wrapper around the CheckStatePutter property.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual BooleanCheckStatePutterDelegate BooleanCheckStatePutter {
            set {
                if (value == null)
                    this.CheckStatePutter = null;
                else
                    this.CheckStatePutter = delegate(Object x, CheckState state) {
                        bool isChecked = (state == CheckState.Checked);
                        return value(x, isChecked) ? CheckState.Checked : CheckState.Unchecked;
                    };
            }
        }

        /// <summary>
        /// This delegate is called when the list wants to show a tooltip for a particular cell.
        /// The delegate should return the text to display, or null to use the default behavior
        /// (which is to show the full text of truncated cell values).
        /// </summary>
        /// <remarks>
        /// Displaying the full text of truncated cell values only work for FullRowSelect listviews.
        /// This is MS's behavior, not mine. Don't complain to me :)
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CellToolTipGetterDelegate CellToolTipGetter {
            get { return cellToolTipGetter; }
            set { cellToolTipGetter = value; }
        }
        private CellToolTipGetterDelegate cellToolTipGetter;

        /// <summary>
        /// The name of the property (or field) that holds whether or not a model is checked.
        /// </summary>
        /// <remarks>
        /// <para>The property be modifiable. It must have a return type of bool or of bool? if
        /// TriStateCheckBoxes is true.</para>
        /// <para>Setting this property replaces any CheckStateGetter or CheckStatePutter that have been installed.
        /// Conversely, later setting the CheckStateGetter or CheckStatePutter properties will take precedence
        /// over the behavior of this property.</para>
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("The name of the property or field that holds the 'checkedness' of the model"),
         DefaultValue(null)]
        public virtual string CheckedAspectName {
            get { return checkedAspectName; }
            set {
                checkedAspectName = value;
                if (String.IsNullOrEmpty(checkedAspectName)) {
                    this.checkedAspectMunger = null;
                    this.CheckStateGetter = null;
                    this.CheckStatePutter = null;
                } else {
                    this.checkedAspectMunger = new Munger(checkedAspectName);
                    this.CheckStateGetter = delegate(Object modelObject) {
                        bool? result = this.checkedAspectMunger.GetValue(modelObject) as bool?;
                        if (result.HasValue)
                            if (result.Value)
                                return CheckState.Checked;
                            else
                                return CheckState.Unchecked;
                        else
                            if (this.TriStateCheckBoxes)
                                return CheckState.Indeterminate;
                            else
                                return CheckState.Unchecked;
                    };
                    this.CheckStatePutter = delegate(Object modelObject, CheckState newValue) {
                        if (this.TriStateCheckBoxes && newValue == CheckState.Indeterminate)
                            this.checkedAspectMunger.PutValue(modelObject, null);
                        else
                            this.checkedAspectMunger.PutValue(modelObject, newValue == CheckState.Checked);
                        return this.CheckStateGetter(modelObject);
                    };
                }
            }
        }
        private string checkedAspectName;
        private Munger checkedAspectMunger;

        /// <summary>
        /// This delegate will be called whenever the ObjectListView needs to know the check state
        /// of the row associated with a given model object.
        /// </summary>
        /// <remarks>
        /// <para>.NET has no support for indeterminate values, but as of v2.0, this class allows
        /// indeterminate values.</para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CheckStateGetterDelegate CheckStateGetter {
            get { return checkStateGetter; }
            set { checkStateGetter = value; }
        }
        private CheckStateGetterDelegate checkStateGetter;

        /// <summary>
        /// This delegate will be called whenever the user tries to change the check state of a row.
        /// The delegate should return the state that was actually set, which may be different
        /// to the state given.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CheckStatePutterDelegate CheckStatePutter {
            get { return checkStatePutter; }
            set { checkStatePutter = value; }
        }
        private CheckStatePutterDelegate checkStatePutter;

        /// <summary>
        /// This delegate can be used to sort the table in a custom fasion.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The delegate must install a ListViewItemSorter on the ObjectListView.
        /// Installing the ItemSorter does the actual work of sorting the ListViewItems.
        /// See ColumnComparer in the code for an example of what an ItemSorter has to do.
        /// </para>
        /// <para>
        /// Do not install a CustomSorter on a VirtualObjectListView. Override the SortObjects()
        /// method of the IVirtualListDataSource instead.
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SortDelegate CustomSorter {
            get { return customSorter; }
            set { customSorter = value; }
        }
        private SortDelegate customSorter;

        /// <summary>
        /// This delegate is called when the list wants to show a tooltip for a particular header.
        /// The delegate should return the text to display, or null to use the default behavior
        /// (which is to not show any tooltip).
        /// </summary>
        /// <remarks>
        /// Installing a HeaderToolTipGetter takes precedence over any text in OLVColumn.ToolTipText.
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HeaderToolTipGetterDelegate HeaderToolTipGetter {
            get { return headerToolTipGetter; }
            set { headerToolTipGetter = value; }
        }
        private HeaderToolTipGetterDelegate headerToolTipGetter;

        /// <summary>
        /// This delegate can be used to format a OLVListItem before it is added to the control.
        /// </summary>
        /// <remarks>
        /// <para>The model object for the row can be found through the RowObject property of the OLVListItem object.</para>
        /// <para>All subitems normally have the same style as list item, so setting the forecolor on one
        /// subitem changes the forecolor of all subitems.
        /// To allow subitems to have different attributes, do this:
        /// <code>myListViewItem.UseItemStyleForSubItems = false;</code>.
        /// </para>
        /// <para>If UseAlternatingBackColors is true, the backcolor of the listitem will be calculated
        /// by the control and cannot be controlled by the RowFormatter delegate. 
        /// In general, trying to use a RowFormatter
        /// when UseAlternatingBackColors is true does not work well.</para>
        /// <para>As it says in the summary, this is called <b>before</b> the item is added to the control.
        /// Many properties of the OLVListItem itself are not available at that point, including:
        /// Index, Selected, Focused, Bounds, Checked, DisplayIndex.</para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual RowFormatterDelegate RowFormatter {
            get { return rowFormatter; }
            set { rowFormatter = value; }
        }
        private RowFormatterDelegate rowFormatter;

        #endregion

        #region List commands

        /// <summary>
        /// Add the given model object to this control.
        /// </summary>
        /// <param name="modelObject">The model object to be displayed</param>
        /// <remarks>See AddObjects() for more details</remarks>
        public virtual void AddObject(object modelObject) {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate() { this.AddObject(modelObject); });
            else 
                this.AddObjects(new object[] { modelObject });
        }

        /// <summary>
        /// Add the given collection of model objects to this control.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        /// <remarks>
        /// <para>The added objects will appear in their correct sort position, if sorting
        /// is active (i.e. if LastSortColumn is not null). Otherwise, they will appear at the end of the list.</para>
        /// <para>No check is performed to see if any of the objects are already in the ListView.</para>
        /// <para>Null objects are silently ignored.</para>
        /// </remarks>
        public virtual void AddObjects(ICollection modelObjects) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate() { this.AddObjects(modelObjects); });
                return;
            }
            this.InsertObjects(this.GetItemCount(), modelObjects);
            this.Sort(this.LastSortColumn, this.LastSortOrder);

        }

        /// <summary>
        /// Organise the view items into groups, based on the last sort column or the first column
        /// if there is no last sort column
        /// </summary>
        public virtual void BuildGroups() {
            this.BuildGroups(this.LastSortColumn, this.LastSortOrder == SortOrder.None ? SortOrder.Ascending : this.LastSortOrder);
        }

        /// <summary>
        /// Organise the view items into groups, based on the given column
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the AlwaysGroupByColumn property is not null,
        /// the list view items will be organisd by that column,
        /// and the 'column' parameter will be ignored.
        /// </para>
        /// <para>This method triggers sorting events: BeforeSorting and AfterSorting.</para>
        /// </remarks>
        /// <param name="column">The column whose values should be used for sorting.</param>
        public virtual void BuildGroups(OLVColumn column, SortOrder order) {
            BeforeSortingEventArgs args = this.BuildBeforeSortingEventArgs(column, order);
            this.OnBeforeSorting(args);
            if (args.Canceled)
                return;

            this.BuildGroups(args.ColumnToGroupBy, args.GroupByOrder, 
                args.ColumnToSort, args.SortOrder, args.SecondaryColumnToSort, args.SecondarySortOrder);

            this.OnAfterSorting(new AfterSortingEventArgs(args));
        }

        private BeforeSortingEventArgs BuildBeforeSortingEventArgs(OLVColumn column, SortOrder order) {
            OLVColumn groupBy = this.AlwaysGroupByColumn ?? column ?? this.GetColumn(0);
            SortOrder groupByOrder = this.AlwaysGroupBySortOrder;
            if (order == SortOrder.None) {
                order = this.Sorting;
                if (order == SortOrder.None)
                    order = SortOrder.Ascending;
            }
            if (groupByOrder == SortOrder.None)
                groupByOrder = order;

            BeforeSortingEventArgs args = new BeforeSortingEventArgs(
                groupBy, groupByOrder,
                column, order,
                this.SecondarySortColumn ?? this.GetColumn(0),
                this.SecondarySortOrder == SortOrder.None ? order : this.SecondarySortOrder);
            return args;
        }

        /// <summary>
        /// Organise the view items into groups, based on the given columns
        /// </summary>
        /// <param name="column">The column whose values should be used for sorting. Cannot be null</param>
        /// <param name="order">The order in which the values from column will be sorted</param>
        /// <param name="secondaryColumn">When the values from 'column' are equal, use the values provided by this column</param>
        /// <param name="order">How will the secondary values be sorted</param>
        /// <remarks>This method does not trigger sorting events. Use BuildGroups() to do that</remarks>
        public virtual void BuildGroups(OLVColumn groupByColumn, SortOrder groupByOrder, 
            OLVColumn column, SortOrder order, OLVColumn secondaryColumn, SortOrder secondaryOrder) {
            // Sanity checks
            if (groupByColumn == null)
                return;

            this.Groups.Clear();

            // Getting the Count forces any internal cache of the ListView to be flushed. Without
            // this, iterating over the Items will not work correctly if the ListView handle
            // has not yet been created.
            int dummy = this.Items.Count;

            // Separate the list view items into groups, using the group key as the descrimanent
            NullableDictionary<object, List<OLVListItem>> map = new NullableDictionary<object, List<OLVListItem>>();
            foreach (OLVListItem olvi in this.Items) {
                object key = groupByColumn.GetGroupKey(olvi.RowObject);
                if (!map.ContainsKey(key))
                    map[key] = new List<OLVListItem>();
                map[key].Add(olvi);
            }

            // Make a list of the required groups
            List<ListViewGroup> groups = new List<ListViewGroup>();
            foreach (object key in map.Keys) {
                ListViewGroup lvg = new ListViewGroup(groupByColumn.ConvertGroupKeyToTitle(key));
                lvg.Tag = key;
                groups.Add(lvg);
            }

            // Sort the groups
            groups.Sort(new ListViewGroupComparer(groupByOrder));

            // Put each group into the list view, and give each group its member items.
            // The order of statements is important here:
            // - the header must be calculate before the group is added to the list view,
            //   otherwise changing the header causes a nasty redraw (even in the middle of a BeginUpdate...EndUpdate pair)
            // - the group must be added before it is given items, otherwise an exception is thrown (is this documented?)
            string fmt = groupByColumn.GroupWithItemCountFormatOrDefault;
            string singularFmt = groupByColumn.GroupWithItemCountSingularFormatOrDefault;
            ColumnComparer itemSorter = new ColumnComparer(
                (this.SortGroupItemsByPrimaryColumn ? this.GetColumn(0) : column), order, secondaryColumn, secondaryOrder);
            foreach (ListViewGroup group in groups) {
                if (this.ShowItemCountOnGroups) {
                    int count = map[group.Tag].Count;
                    group.Header = String.Format((count == 1 ? singularFmt : fmt), group.Header, count);
                }
                this.Groups.Add(group);
                // If there is no sort order, don't sort since the sort isn't stable
                // THINK: Can order ever be None here?
                if (order != SortOrder.None)
                    map[group.Tag].Sort(itemSorter);
                group.Items.AddRange(map[group.Tag].ToArray());
            }
        }

        /// <summary>
        /// Build/rebuild all the list view items in the list
        /// </summary>
        public virtual void BuildList() {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(this.BuildList));
            else
                this.BuildList(true);
        }

        /// <summary>
        /// Build/rebuild all the list view items in the list
        /// </summary>
        /// <param name="shouldPreserveState">If this is true, the control will try to preserve the selection,
        /// focused item, and the scroll position (see Remarks)
        /// </param>
        /// <remarks>
        /// <para>
        /// Use this method in situations were the contents of the list is basically the same
        /// as previously.
        /// </para>
        /// <para>
        /// Due to limitations in .NET's ListView, the scroll position is only preserved if
        /// the control is in Details view AND it is not showing groups.
        /// </para>
        /// </remarks>
        public virtual void BuildList(bool shouldPreserveState) {
            if (this.Frozen)
                return;

            this.ClearHotItem();
            int previousTopIndex = this.TopItemIndex;
            IList previousSelection = new ArrayList();
            Object previousFocus = null;
            if (shouldPreserveState && this.objects != null) {
                previousSelection = this.SelectedObjects;
                OLVListItem focusedItem = this.FocusedItem as OLVListItem;
                if (focusedItem != null)
                    previousFocus = focusedItem.RowObject;
            }

            this.BeginUpdate();
            try {
                this.Items.Clear();
                this.ListViewItemSorter = null;

                if (this.objects != null) {
                    // Build a list of all our items and then display them. (Building
                    // a list and then doing one AddRange is about 10-15% faster than individual adds)
                    List<OLVListItem> itemList = new List<OLVListItem>();
                    foreach (object rowObject in this.objects) {
                        OLVListItem lvi = new OLVListItem(rowObject);
                        this.FillInValues(lvi, rowObject);
                        itemList.Add(lvi);
                    }
                    this.Items.AddRange(itemList.ToArray());
                    this.SetAllSubItemImages();
                    this.Sort();

                    // If the list isn't sorted, we might need to setup the background colors here
                    if (this.LastSortColumn == null && this.UseAlternatingBackColors && this.View == View.Details)
                        this.PrepareAlternateBackColors();

                    if (shouldPreserveState) {
                        this.SelectedObjects = previousSelection;
                        this.FocusedItem = this.ModelToItem(previousFocus);
                    }

                    this.RefreshHotItem();
                }
            }
            finally {
                this.EndUpdate();
            }

            // We can only restore the scroll position after the EndUpdate() because
            // of caching that the ListView does internally during a BeginUpdate/EndUpdate pair.
            if (shouldPreserveState) {
                this.TopItemIndex = previousTopIndex;
                this.RefreshHotItem();
            }
        }

        /// <summary>
        /// Give the listview a reasonable size of its tiles, based on the number of lines of
        /// information that each tile is going to display.
        /// </summary>
        public virtual void CalculateReasonableTileSize() {
            if (this.Columns.Count <= 0)
                return;

            int imageHeight = (this.LargeImageList == null ? 16 : this.LargeImageList.ImageSize.Height);
            int dataHeight = (this.Font.Height + 1) * this.Columns.Count;
            int tileWidth = (this.TileSize.Width == 0 ? 200 : this.TileSize.Width);
            int tileHeight = Math.Max(this.TileSize.Height, Math.Max(imageHeight, dataHeight));
            this.TileSize = new Size(tileWidth, tileHeight);
        }

        /// <summary>
        /// Rebuild this list for the given view
        /// </summary>
        /// <param name="view"></param>
        public virtual void ChangeToFilteredColumns(View view) {
            // Store the state
            IList previousSelection = this.SelectedObjects;
            int previousTopIndex = this.TopItemIndex;

            this.Freeze();
            this.Clear();
            List<OLVColumn> cols = this.GetFilteredColumns(view);
            if (view == View.Details) {
                // Where should each column be shown? We try to put it back where it last was,
                // but if that's not possible, it appears at the end of the columns
                foreach (OLVColumn x in cols) {
                    if (x.LastDisplayIndex == -1 || x.LastDisplayIndex > cols.Count - 1)
                        x.DisplayIndex = cols.Count - 1;
                    else
                        x.DisplayIndex = x.LastDisplayIndex;
                }
            }
            this.Columns.AddRange(cols.ToArray());
            if (view == View.Details)
                this.ShowSortIndicator();
            this.BuildList();
            this.Unfreeze();

            // Restore the state
            this.SelectedObjects = previousSelection;
            this.TopItemIndex = previousTopIndex;
        }

        /// <summary>
        /// Remove all items from this list
        /// </summary>
        /// <remark>This method can safely be called from background threads.</remark>
        public virtual void ClearObjects() {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(this.ClearObjects));
            else
                this.SetObjects(null);
        }

        /// <summary>
        /// Copy a text and html representation of the selected rows onto the clipboard.
        /// </summary>
        /// <remarks>Be careful when using this with virtual lists. If the user has selected
        /// 10,000,000 rows, this method will faithfully try to copy all of them to the clipboard.
        /// From the user's point of view, your program will appear to have hung.</remarks>
        public virtual void CopySelectionToClipboard() {
            //THINK: Do we want to include something like this?
            //if (this.SelectedIndices.Count > 10000)
            //    return;

            this.CopyObjectsToClipboard(this.SelectedObjects);
        }

        /// <summary>
        /// Copy a text and html representation of the given objects onto the clipboard.
        /// </summary>
        public virtual void CopyObjectsToClipboard(IList objectsToCopy) {
            if (objectsToCopy.Count == 0)
                return;

            OLVDataObject dataObject = new OLVDataObject(this, objectsToCopy);
            dataObject.CreateTextFormats();
            Clipboard.SetDataObject(dataObject);
        }

        /// <summary>
        /// Deselect all rows in the listview
        /// </summary>
        public virtual void DeselectAll() {
            NativeMethods.DeselectAllItems(this);
        }

        /// <summary>
        /// Setup the list so it will draw selected rows using custom colours.
        /// </summary>
        /// <remarks>
        /// This method makes the list owner drawn, and ensures that all columns have at
        /// least a BaseRender installed.
        /// </remarks>
        public virtual void EnableCustomSelectionColors() {
            this.UseCustomSelectionColors = true;
        }

        /// <summary>
        /// Return the ListViewItem that appears immediately after the given item.
        /// If the given item is null, the first item in the list will be returned.
        /// Return null if the given item is the last item.
        /// </summary>
        /// <param name="itemToFind">The item that is before the item that is returned, or null</param>
        /// <returns>A ListViewItem</returns>
        public virtual ListViewItem GetNextItem(ListViewItem itemToFind) {
            if (this.ShowGroups) {
                bool isFound = (itemToFind == null);
                foreach (ListViewGroup group in this.Groups) {
                    foreach (ListViewItem lvi in group.Items) {
                        if (isFound)
                            return lvi;
                        isFound = (lvi == itemToFind);
                    }
                }
                return null;
            } else {
                if (this.GetItemCount() == 0)
                    return null;
                if (itemToFind == null)
                    return this.GetItem(0);
                if (itemToFind.Index == this.GetItemCount() - 1)
                    return null;
                return this.GetItem(itemToFind.Index + 1);
            }
        }

        /// <summary>
        /// Return the last item in the order they are shown to the user.
        /// If the control is not grouped, the display order is the same as the
        /// sorted list order. But if the list is grouped, the display order is different.
        /// </summary>
        /// <returns></returns>
        public virtual OLVListItem GetLastItemInDisplayOrder() {
            if (!this.ShowGroups)
                return this.GetItem(this.GetItemCount() - 1);

            if (this.Groups.Count > 0) {
                ListViewGroup lastGroup = this.Groups[this.Groups.Count - 1];
                if (lastGroup.Items.Count > 0)
                    return (OLVListItem)lastGroup.Items[lastGroup.Items.Count - 1];
            }

            return null;
        }

        /// <summary>
        /// Return the n'th item (0-based) in the order they are shown to the user.
        /// If the control is not grouped, the display order is the same as the
        /// sorted list order. But if the list is grouped, the display order is different.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public virtual OLVListItem GetNthItemInDisplayOrder(int n) {
            if (!this.ShowGroups)
                return this.GetItem(n);

            foreach (ListViewGroup lgv in this.Groups) {
                if (n < lgv.Items.Count)
                    return (OLVListItem)lgv.Items[n];

                n -= lgv.Items.Count;
            }

            return null;
        }

        /// <summary>
        /// Return the index of the given ListViewItem as it currently shown to the user.
        /// If the control is not grouped, the display order is the same as the
        /// sorted list order. But if the list is grouped, the display order is different.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int GetItemIndexInDisplayOrder(ListViewItem value) {
            if (!this.ShowGroups)
                return value.Index;

            // TODO: This could be optimized
            int i = 0;
            foreach (ListViewGroup lvg in this.Groups) {
                foreach (ListViewItem lvi in lvg.Items) {
                    if (lvi == value)
                        return i;
                    i++;
                }
            }

            return -1;
        }

        /// <summary>
        /// Return the ListViewItem that appears immediately before the given item.
        /// If the given item is null, the last item in the list will be returned.
        /// Return null if the given item is the first item.
        /// </summary>
        /// <param name="itemToFind">The item that is before the item that is returned</param>
        /// <returns>A ListViewItem</returns>
        public virtual ListViewItem GetPreviousItem(ListViewItem itemToFind) {
            if (this.ShowGroups) {
                ListViewItem previousItem = null;
                foreach (ListViewGroup group in this.Groups) {
                    foreach (ListViewItem lvi in group.Items) {
                        if (lvi == itemToFind)
                            return previousItem;
                        else
                            previousItem = lvi;
                    }
                }
                if (itemToFind == null)
                    return previousItem;
                else
                    return null;
            } else {
                if (this.GetItemCount() == 0)
                    return null;
                if (itemToFind == null)
                    return this.GetItem(this.GetItemCount() - 1);
                if (itemToFind.Index == 0)
                    return null;
                return this.GetItem(itemToFind.Index - 1);
            }
        }

        /// <summary>
        /// Update the list to reflect the contents of the given collection, without affecting
        /// the scrolling position, selection or sort order.
        /// </summary>
        /// <param name="collection">The objects to be displayed</param>
        /// <remarks>
        /// <para>This method is about twice as slow as SetObjects().</para>
        /// <para>This method is experimental -- it may disappear in later versions of the code.</para>
        /// <para>There has to be a better way to do this! JPP 15/1/2008</para>
        /// <para>In most situations, if you need this functionality, use a FastObjectListView instead. JPP 2/2/2008</para>
        /// </remarks>
        [Obsolete("Use a FastObjectListView instead of this method.", false)]
        public virtual void IncrementalUpdate(IEnumerable collection) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate { this.IncrementalUpdate(collection); });
                return;
            }

            this.BeginUpdate();

            this.ListViewItemSorter = null;
            IList previousSelection = this.SelectedObjects;

            // Replace existing rows, creating new listviewitems if we get to the end of the list
            List<OLVListItem> newItems = new List<OLVListItem>();
            int rowIndex = 0;
            int itemCount = this.Items.Count;
            foreach (object model in collection) {
                if (rowIndex < itemCount) {
                    OLVListItem lvi = this.GetItem(rowIndex);
                    lvi.RowObject = model;
                    this.RefreshItem(lvi);
                } else {
                    OLVListItem lvi = new OLVListItem(model);
                    this.FillInValues(lvi, model);
                    newItems.Add(lvi);
                }
                rowIndex++;
            }

            // Delete any excess rows
            int numRowsToDelete = itemCount - rowIndex;
            for (int i = 0; i < numRowsToDelete; i++)
                this.Items.RemoveAt(rowIndex);

            this.Items.AddRange(newItems.ToArray());
            this.Sort(this.LastSortColumn);

            SetAllSubItemImages();

            this.SelectedObjects = previousSelection;

            this.EndUpdate();

            this.objects = collection;
        }

        /// <summary>
        /// Insert the given collection of objects before the given position
        /// </summary>
        /// <param name="index">Where to insert the objects</param>
        /// <param name="modelObjects">The objects to be inserted</param>
        /// <remarks>
        /// <para>
        /// This operation only makes sense of non-sorted, non-grouped
        /// lists, since any subsequent sort/group operation will rearrange
        /// the list.
        /// </para> 
        /// <para>This method only works on ObjectListViews and FastObjectListViews.</para>
        ///</remarks>
        public virtual void InsertObjects(int index, ICollection modelObjects) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate() {
                    this.InsertObjects(index, modelObjects);
                });
                return;
            }
            if (modelObjects == null)
                return;

            this.BeginUpdate();
            try {
                // Give the world a chance to cancel or change the added objects
                ItemsAddingEventArgs args = new ItemsAddingEventArgs(modelObjects);
                this.OnItemsAdding(args);
                if (args.Canceled)
                    return;
                modelObjects = args.ObjectsToAdd;

                this.ListViewItemSorter = null;
                this.TakeOwnershipOfObjects();
                ArrayList ourObjects = (ArrayList)this.Objects;
                index = Math.Max(0, Math.Min(index, this.GetItemCount()));
                int i = index;
                foreach (object modelObject in modelObjects) {
                    if (modelObject != null) {
                        ourObjects.Insert(i, modelObject);
                        OLVListItem lvi = new OLVListItem(modelObject);
                        this.FillInValues(lvi, modelObject);
                        this.Items.Insert(i, lvi);
                        i++;
                    }
                }

                for (i = index; i < this.GetItemCount(); i++) {
                    OLVListItem lvi = this.GetItem(i);
                    this.SetSubItemImages(lvi.Index, lvi);
                }

                if (this.LastSortColumn == null && this.UseAlternatingBackColors && this.View == View.Details)
                    this.PrepareAlternateBackColors();

                // Tell the world that the list has changed
                this.OnItemsChanged(new ItemsChangedEventArgs());
            }
            finally {
                this.EndUpdate();
            }
        }

        /// <summary>
        /// Return true if the row representing the given model is selected
        /// </summary>
        /// <param name="model">The model object to look for</param>
        /// <returns>Is the row selected</returns>
        public bool IsSelected(object model) {
            OLVListItem item = this.ModelToItem(model);
            if (item == null)
                return false;
            else
                return item.Selected;
        }

        /// <summary>
        /// Scroll the ListView by the given deltas.
        /// </summary>
        /// <param name="dx">Horizontal delta</param>
        /// <param name="dy">Vertical delta</param>
        internal void LowLevelScroll(int dx, int dy) {
            NativeMethods.Scroll(this, dx, dy);
        }

        /// <summary>
        /// Move the given collection of objects to the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="modelObjects"></param>
        public virtual void MoveObjects(int index, ICollection modelObjects) {

            // We are going to remove all the given objects from our list
            // and then insert them at the given location
            this.TakeOwnershipOfObjects();
            ArrayList ourObjects = (ArrayList)this.Objects;

            List<int> indicesToRemove = new List<int>();
            foreach (object modelObject in modelObjects) {
                if (modelObject != null) {
                    int i = this.IndexOf(modelObject);
                    if (i >= 0) {
                        indicesToRemove.Add(i);
                        ourObjects.Remove(modelObject);
                        if (i <= index)
                            index--;
                    }
                }
            }

            // Remove the objects in reverse order so earlier
            // deletes don't change the index of later ones
            indicesToRemove.Sort();
            indicesToRemove.Reverse();
            try {
                this.BeginUpdate();
                foreach (int i in indicesToRemove) {
                    this.Items.RemoveAt(i);
                }
                this.InsertObjects(index, modelObjects);
            }
            finally {
                this.EndUpdate();
            }
        }

        /// <summary>
        /// What is under the given point? This takes the various parts of a cell into accout, including
        /// any custom parts that a custom renderer might use
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>An information block about what is under the point</returns>
        public virtual OlvListViewHitTestInfo OlvHitTest(int x, int y) {
            return this.OlvHitTest(x, y, false);
        }

        /// <summary>
        /// What is under the given point? This takes the various parts of a cell into accout, including
        /// any custom parts that a custom renderer might use
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="useFullRowSelect">Force the hit test to be done as if full row select is true</param>
        /// <returns>An information block about what is under the point</returns>
        public virtual OlvListViewHitTestInfo OlvHitTest(int x, int y, bool useFullRowSelect) {
            // This horrible sequence finds what item is under the given position.
            // We want to find the item under the point, even if the point is not
            // actually over the icon or label. HitTest() will only do that
            // when FullRowSelect is true.
            ListViewHitTestInfo hitTestInfo = null;
            if (this.FullRowSelect || !useFullRowSelect)
                hitTestInfo = this.HitTest(x, y);
            else {
                this.FullRowSelect = true;
                hitTestInfo = this.HitTest(x, y);
                this.FullRowSelect = false;
            }

            OlvListViewHitTestInfo hti = new OlvListViewHitTestInfo(hitTestInfo);

            if (this.OwnerDraw)
                this.CalculateOwnerDrawnHitTest(hti, x, y);
            else
                this.CalculateStandardHitTest(hti, x, y);

            return hti;
        }

        /// <summary>
        /// Perform a hit test when the control is not owner drawn
        /// </summary>
        /// <param name="hti"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected virtual void CalculateStandardHitTest(OlvListViewHitTestInfo hti, int x, int y) {

            // Check if the point is over a sub item checkbox

            // Subitem checkboxes are only visible in details mode
            if (this.View != View.Details)
                return;

            // Does the subitem have a checkbox?
            if (hti.Column == null || !hti.Column.CheckBoxes)
                return;

            // Figure out if they clicked in the checkbox.
            Rectangle r = hti.SubItem.Bounds;
            r.Width = this.SmallImageSize.Width;
            if (r.Contains(x, y))
                hti.HitTestLocation = HitTestLocation.CheckBox;
        }

        /// <summary>
        /// Perform a hit test when the control is owner drawn. This hands off responsibility
        /// to the renderer.
        /// </summary>
        /// <param name="hti"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected virtual void CalculateOwnerDrawnHitTest(OlvListViewHitTestInfo hti, int x, int y) {
            // If the click wasn't on an item, give up
            if (hti.Item == null)
                return;

            // If the list is showing column, but they clicked outside the columns, also give up
            if (this.View == View.Details && hti.Column == null)
                return;

            // Which renderer was responsible for drawing that point
            IRenderer renderer = null;
            if (this.View == View.Details) {
                renderer = hti.Column.Renderer ?? this.DefaultRenderer;
            } else {
                renderer = this.ItemRenderer;
            }

            // We can't decide who was responsible. Give up
            if (renderer == null)
                return;

            // Ask the responsible renderer what is at that point
            renderer.HitTest(hti, x, y);
        }

        /// <summary>
        /// Pause (or unpause) all animations in the list
        /// </summary>
        /// <param name="isPause">true to pause, false to unpause</param>
        public virtual void PauseAnimations(bool isPause) {
            for (int i = 0; i < this.Columns.Count; i++) {
                OLVColumn col = this.GetColumn(i);
                if (col.Renderer is ImageRenderer)
                    ((ImageRenderer)col.Renderer).Paused = isPause;
            }
        }

        /// <summary>
        /// Rebuild the columns based upon its current view and column visibility settings
        /// </summary>
        public virtual void RebuildColumns() {
            // THINK: Do we need this here? 2009/06/12
            //if (this.UseCustomSelectionColors)
            //    this.EnableCustomSelectionColors();

            this.ChangeToFilteredColumns(this.View);
        }

        /// <summary>
        /// Remove the given model object from the ListView
        /// </summary>
        /// <param name="modelObject">The model to be removed</param>
        /// <remarks>See RemoveObjects() for more details</remarks>
        public virtual void RemoveObject(object modelObject) {
            if (this.InvokeRequired) 
                this.Invoke((MethodInvoker)delegate() { this.RemoveObject(modelObject); });
            else
                this.RemoveObjects(new object[] { modelObject });
        }

        /// <summary>
        /// Remove all of the given objects from the control
        /// </summary>
        /// <param name="modelObjects">Collection of objects to be removed</param>
        /// <remarks>
        /// <para>Nulls and model objects that are not in the ListView are silently ignored.</para>
        /// </remarks>
        public virtual void RemoveObjects(ICollection modelObjects) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate() { this.RemoveObjects(modelObjects); });
                return;
            }
            if (modelObjects == null)
                return;

            this.BeginUpdate();
            try {
                // Give the world a chance to cancel or change the added objects
                ItemsRemovingEventArgs args = new ItemsRemovingEventArgs(modelObjects);
                this.OnItemsRemoving(args);
                if (args.Canceled)
                    return;
                modelObjects = args.ObjectsToRemove;

                this.TakeOwnershipOfObjects();
                ArrayList ourObjects = (ArrayList)this.Objects;
                foreach (object modelObject in modelObjects) {
                    if (modelObject != null) {
                        ourObjects.Remove(modelObject);
                        int i = this.IndexOf(modelObject);
                        if (i >= 0)
                            this.Items.RemoveAt(i);
                    }
                }

                if (this.LastSortColumn == null && this.UseAlternatingBackColors && this.View == View.Details)
                    this.PrepareAlternateBackColors();

                // Tell the world that the list has changed
                this.OnItemsChanged(new ItemsChangedEventArgs());
            }
            finally {
                this.EndUpdate();
            }
        }

        /// <summary>
        /// Select all rows in the listview
        /// </summary>
        public virtual void SelectAll() {
            NativeMethods.SelectAllItems(this);
        }

        /// <summary>
        /// Set the collection of objects that will be shown in this list view.
        /// </summary>
        /// <remark>This method can safely be called from background threads.</remark>
        /// <remarks>The list is updated immediately</remarks>
        /// <param name="collection">The objects to be displayed</param>
        public virtual void SetObjects(IEnumerable collection) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate { this.SetObjects(collection); });
                return;
            }

            // Give the world a chance to cancel or change the assigned collection
            ItemsChangingEventArgs args = new ItemsChangingEventArgs(this.objects, collection);
            this.OnItemsChanging(args);
            if (args.Canceled)
                return;
            collection = args.NewObjects;

            // If we own the current list and they change to another list, we don't own it anymore
            if (this.isOwnerOfObjects && this.objects != collection)
                this.isOwnerOfObjects = false;
            this.objects = collection;
            this.BuildList(false);

            // Tell the world that the list has changed
            this.OnItemsChanged(new ItemsChangedEventArgs());
        }

        #endregion

        #region Save/Restore State

        /// <summary>
        /// Return a byte array that represents the current state of the ObjectListView, such
        /// that the state can be restored by RestoreState()
        /// </summary>
        /// <remarks>
        /// <para>The state of an ObjectListView includes the attributes that the user can modify:
        /// <list>
        /// <item>current view (i.e. Details, Tile, Large Icon...)</item>
        /// <item>sort column and direction</item>
        /// <item>column order</item>
        /// <item>column widths</item>
        /// <item>column visibility</item>
        /// </list>
        /// </para>
        /// <para>
        /// It does not include selection or the scroll position.
        /// </para>
        /// </remarks>
        /// <returns>A byte array representing the state of the ObjectListView</returns>
        public virtual byte[] SaveState() {
            ObjectListViewState olvState = new ObjectListViewState();
            olvState.VersionNumber = 1;
            olvState.NumberOfColumns = this.AllColumns.Count;
            olvState.CurrentView = this.View;

            // If we have a sort column, it is possible that it is not currently being shown, in which
            // case, it's Index will be -1. So we calculate its index directly. Technically, the sort
            // column does not even have to a member of AllColumns, in which case IndexOf will return -1,
            // which is works fine since we have no way of restoring such a column anyway.
            if (this.LastSortColumn != null)
                olvState.SortColumn = this.AllColumns.IndexOf(this.LastSortColumn);
            olvState.LastSortOrder = this.LastSortOrder;
            olvState.IsShowingGroups = this.ShowGroups;

            if (this.AllColumns.Count > 0 && this.AllColumns[0].LastDisplayIndex == -1)
                this.RememberDisplayIndicies();

            foreach (OLVColumn column in this.AllColumns) {
                olvState.ColumnIsVisible.Add(column.IsVisible);
                olvState.ColumnDisplayIndicies.Add(column.LastDisplayIndex);
                olvState.ColumnWidths.Add(column.Width);
            }

            // Now that we have stored our state, convert it to a byte array
            MemoryStream ms = new MemoryStream();
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(ms, olvState);

            return ms.ToArray();
        }

        /// <summary>
        /// Restore the state of the control from the given string, which must have been
        /// produced by SaveState()
        /// </summary>
        /// <param name="state">A byte array returned from SaveState()</param>
        /// <returns>Returns true if the state was restored</returns>
        public virtual bool RestoreState(byte[] state) {
            MemoryStream ms = new MemoryStream(state);
            BinaryFormatter deserializer = new BinaryFormatter();
            ObjectListViewState olvState;
            try {
                olvState = deserializer.Deserialize(ms) as ObjectListViewState;
            }
            catch (System.Runtime.Serialization.SerializationException) {
                return false;
            }

            // The number of columns has changed. We have no way to match old
            // columns to the new ones, so we just give up.
            if (olvState.NumberOfColumns != this.AllColumns.Count)
                return false;

            if (olvState.SortColumn == -1) {
                this.LastSortColumn = null;
                this.LastSortOrder = SortOrder.None;
            } else {
                this.LastSortColumn = this.AllColumns[olvState.SortColumn];
                this.LastSortOrder = olvState.LastSortOrder;
            }

            for (int i = 0; i < olvState.NumberOfColumns; i++) {
                OLVColumn column = this.AllColumns[i];
                column.Width = (int)olvState.ColumnWidths[i];
                column.IsVisible = (bool)olvState.ColumnIsVisible[i];
                column.LastDisplayIndex = (int)olvState.ColumnDisplayIndicies[i];
            }

            if (olvState.IsShowingGroups != this.ShowGroups)
                this.ShowGroups = olvState.IsShowingGroups;

            if (this.View == olvState.CurrentView)
                this.RebuildColumns();
            else
                this.View = olvState.CurrentView;

            return true;
        }

        /// <summary>
        /// Instances of this class are used to store the state of an ObjectListView.
        /// </summary>
        [Serializable]
        internal class ObjectListViewState
        {
            public int VersionNumber = 1;
            public int NumberOfColumns = 1;
            public View CurrentView;
            public int SortColumn = -1;
            public bool IsShowingGroups;
            public SortOrder LastSortOrder = SortOrder.None;
            public ArrayList ColumnIsVisible = new ArrayList();
            public ArrayList ColumnDisplayIndicies = new ArrayList();
            public ArrayList ColumnWidths = new ArrayList();
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// The application is idle. Trigger a SelectionChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleApplicationIdle(object sender, EventArgs e) {
            // Remove the handler before triggering the event
            Application.Idle -= new EventHandler(HandleApplicationIdle);
            this.hasIdleHandler = false;

            this.OnSelectionChanged(new EventArgs());
        }

        /// <summary>
        /// The cell tooltip control wants information about the tool tip that it should show.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleCellToolTipShowing(object sender, ToolTipShowingEventArgs e) {
            this.BuildCellEvent(e, this.PointToClient(Cursor.Position));
            if (e.Item != null) {
                e.Text = this.GetCellToolTip(e.ColumnIndex, e.RowIndex);
                this.OnCellToolTip(e);
            }
        }

        /// <summary>
        /// Allow the HeaderControl to call back into HandleHeaderToolTipShowing without making that method public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void headerToolTip_Showing(object sender, ToolTipShowingEventArgs e) {
            this.HandleHeaderToolTipShowing(sender, e);
        }

        /// <summary>
        /// The header tooltip control wants information about the tool tip that it should show.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleHeaderToolTipShowing(object sender, ToolTipShowingEventArgs e) {
            e.ColumnIndex = this.HeaderControl.ColumnIndexUnderCursor;
            if (e.ColumnIndex < 0)
                return;

            e.RowIndex = -1;
            e.Model = null;
            e.Column = this.GetColumn(e.ColumnIndex);
            e.Text = this.GetHeaderToolTip(e.ColumnIndex);
            e.ListView = this;
            this.OnHeaderToolTip(e);
        }

        /// <summary>
        /// Event handler for the column click event
        /// </summary>
        protected virtual void HandleColumnClick(object sender, ColumnClickEventArgs e) {
            if (!this.PossibleFinishCellEditing())
                return;

            // Toggle the sorting direction on successive clicks on the same column
            if (this.LastSortColumn != null && e.Column == this.LastSortColumn.Index)
                this.LastSortOrder = (this.LastSortOrder == SortOrder.Descending ? SortOrder.Ascending : SortOrder.Descending);
            else
                this.LastSortOrder = SortOrder.Ascending;

            this.BeginUpdate();
            try {
                this.Sort(e.Column);
            }
            finally {
                this.EndUpdate();
            }
        }

        #endregion

        #region Low level Windows Message handling

        /// <summary>
        /// Override the basic m pump for this control
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                //case 0x14: // WM_ERASEBKGND
                //    Can't do anything here since, when the control is double buffered, anything
                //    done here is immediately over-drawn
                //    break;
                case 0x0F: // WM_PAINT
                    if (!this.HandlePaint(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x46: // WM_WINDOWPOSCHANGING
                    if (!this.HandleWindowPosChanging(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x4E: // WM_NOTIFY
                    if (!this.HandleNotify(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x0100: // WM_KEY_DOWN
                    if (!this.HandleKeyDown(ref m)) 
                        base.WndProc(ref m);
                    break;
                case 0x0102: // WM_CHAR
                    if (!this.HandleChar(ref m)) 
                        base.WndProc(ref m);
                    break;
                case 0x0201: // WM_LBUTTONDOWN
                    if (this.PossibleFinishCellEditing() && !this.HandleLButtonDown(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x0203: // WM_LBUTTONDBLCLK
                    if (this.PossibleFinishCellEditing() && !this.HandleLButtonDoubleClick(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x204E: // WM_REFLECT_NOTIFY
                    if (!this.HandleReflectNotify(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x114: // WM_HSCROLL:
                case 0x115: // WM_VSCROLL:
                    if (this.PossibleFinishCellEditing()) 
                        base.WndProc(ref m);
                    break;
                case 0x20A: // WM_MOUSEWHEEL:
                case 0x20E: // WM_MOUSEHWHEEL:
                    if (this.PossibleFinishCellEditing()) 
                        base.WndProc(ref m);
                    break;
                case 0x7B: // WM_CONTEXTMENU
                    if (!this.HandleContextMenu(ref m))
                        base.WndProc(ref m);
                    break;
                case 0x1000 + 145: // LVM_INSERTGROUP     = LVM_FIRST + 145;
                    // Add the collapsible feature if needed
                    if (!this.HandleInsertGroup(ref m)) 
                        base.WndProc(ref m);
                    break;
                case 0x202:  /*WM_LBUTTONUP*/
                    if (ObjectListView.IsVista && this.HasCollapsibleGroups) 
                        base.DefWndProc(ref m);
                    base.WndProc(ref m);
                    break;

                case 2: // WM_DESTROY
                    if (!this.HandleDestroy(ref m))
                        base.WndProc(ref m);
                    break;
                //case 0x1000 + 20:
                //    System.Diagnostics.Debug.WriteLine("LVM_SCROLL");
                //    base.WndProc(ref m);
                //    break;

                // This doesn't seem to be called when i expected
                //case 0x1053: // LVM_FINDITEM = (LVM_FIRST + 83)
                //    if (!this.HandleFindItem(ref m))
                //        base.WndProc(ref m);
                //    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// Is the program running on Vista or later?
        /// </summary>
        static public bool IsVista {
            get {
                if (!ObjectListView.isVista.HasValue)
                    ObjectListView.isVista = Environment.OSVersion.Version.Major >= 6;
                return ObjectListView.isVista.Value;
            }
        }
        static private bool? isVista;

        /// <summary>
        /// Handle the search for item m if possible.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleChar(ref Message m) {

            // Trigger a normal KeyPress event, which listeners can handle if they want.
            // Handling the event stops ObjectListView's fancy search-by-typing.
            if (this.ProcessKeyEventArgs(ref m))
                return true;

            const int MILLISECONDS_BETWEEN_KEYPRESSES = 1000;

            // What character did the user type and was it part of a longer string?
            char character = (char)m.WParam.ToInt32(); //TODO: Will this work on 64 bit or MBCS?
            if (character == (char)Keys.Back) {
                // Backspace forces the next key to be considered the start of a new search
                this.timeLastCharEvent = 0;
                return true;
            }
            
            if (System.Environment.TickCount < (this.timeLastCharEvent + MILLISECONDS_BETWEEN_KEYPRESSES))
                this.lastSearchString += character;
            else
                this.lastSearchString = character.ToString();

            // If this control is showing checkboxes, we want to ignore single space presses,
            // since they are used to toggle the selected checkboxes.
            if (this.CheckBoxes && this.lastSearchString == " ") {
                this.timeLastCharEvent = 0;
                return true;
            }

            // Where should the search start?
            int start = 0;
            ListViewItem focused = this.FocusedItem;
            if (focused != null) {
                start = this.GetItemIndexInDisplayOrder(focused);

                // If the user presses a single key, we search from after the focused item,
                // being careful not to march past the end of the list
                if (this.lastSearchString.Length == 1) {
                    start += 1;
                    if (start == this.GetItemCount())
                        start = 0;
                }
            }

            // Give the world a chance to fiddle with or completely avoid the searching process
            BeforeSearchingEventArgs args = new BeforeSearchingEventArgs(this.lastSearchString, start);
            this.OnBeforeSearching(args);
            if (args.Canceled)
                return true;

            // The parameters of the search may have been changed
            string searchString = args.StringToFind;
            start = args.StartSearchFrom;

            // Do the actual search
            int found = this.FindMatchingRow(searchString, start, SearchDirectionHint.Down);
            if (found >= 0) {
                // Select and focus on the found item
                this.BeginUpdate();
                try {
                    this.SelectedIndices.Clear();
                    ListViewItem lvi = this.GetNthItemInDisplayOrder(found);
                    lvi.Selected = true;
                    lvi.Focused = true;
                    this.EnsureVisible(lvi.Index);
                }
                finally {
                    this.EndUpdate();
                }
            }

            // Tell the world that a search has occurred
            AfterSearchingEventArgs args2 = new AfterSearchingEventArgs(searchString, found);
            this.OnAfterSearching(args2);
            if (!args2.Handled) {
                if (found < 0)
                    System.Media.SystemSounds.Beep.Play();
            }

            // When did this event occur?
            this.timeLastCharEvent = System.Environment.TickCount;
            return true;
        }
        private int timeLastCharEvent;
        private string lastSearchString;

        /// <summary>
        /// The user wants to see the context menu.
        /// </summary>
        /// <param name="m">The windows m</param>
        /// <returns>A bool indicating if this m has been handled</returns>
        /// <remarks>
        /// We want to ignore context menu requests that are triggered by right clicks on the header
        /// </remarks>
        protected virtual bool HandleContextMenu(ref Message m) {
            // Don't try to handle context menu commands at design time.
            if (this.DesignMode)
                return false;

            // If the context menu command was generated by the keyboard, LParam will be -1.
            // We don't want to process these.
            if (((int)m.LParam) == -1)
                return false;

            // If the context menu came from somewhere other than the header control,
            // we also don't want to ignore it
            if (m.WParam != this.HeaderControl.Handle)
                return false;

            // OK. Looks like a right click in the header
            if (!this.PossibleFinishCellEditing())
                return true;

            int columnIndex = this.HeaderControl.ColumnIndexUnderCursor;
            return this.HandleHeaderRightClick(columnIndex);
        }

        /// <summary>
        /// Handle the Custom draw series of notifications
        /// </summary>
        /// <param name="m">The message</param>
        /// <returns>True if the message has been handled</returns>
        protected virtual bool HandleCustomDraw(ref Message m) {
            const int CDDS_PREPAINT = 1;
            const int CDDS_POSTPAINT = 2;
            const int CDDS_PREERASE = 3;
            const int CDDS_POSTERASE = 4;
            const int CDDS_ITEM = 0x00010000;
            const int CDDS_SUBITEM = 0x00020000;
            const int CDDS_ITEMPREPAINT = (CDDS_ITEM | CDDS_PREPAINT);
            const int CDDS_ITEMPOSTPAINT = (CDDS_ITEM | CDDS_POSTPAINT);
            const int CDDS_ITEMPREERASE = (CDDS_ITEM | CDDS_PREERASE);
            const int CDDS_ITEMPOSTERASE = (CDDS_ITEM | CDDS_POSTERASE);
            const int CDDS_SUBITEMPREPAINT = (CDDS_SUBITEM | CDDS_ITEMPREPAINT);
            const int CDDS_SUBITEMPOSTPAINT = (CDDS_SUBITEM | CDDS_ITEMPOSTPAINT);
            const int CDRF_NOTIFYPOSTPAINT = 0x10;
            //const int CDRF_NOTIFYITEMDRAW = 0x20;
            //const int CDRF_NOTIFYSUBITEMDRAW = 0x20; // same value as above!
            const int CDRF_NOTIFYPOSTERASE = 0x40; 

            NativeMethods.NMLVCUSTOMDRAW nmcustomdraw = (NativeMethods.NMLVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMLVCUSTOMDRAW));
            //System.Diagnostics.Debug.WriteLine(String.Format("cd: {0:x}", nmcustomdraw.nmcd.dwDrawStage));

            // There is a bug in owner drawn virtual lists which causes lots of custom draw messages
            // to be sent to the control *outside* of a WmPaint event. AFAIK, these custom draw events
            // are spurious and only serve to make the control flicker annoyingly.
            // So, we ignore messages that are outside of a paint event.
            if (!this.isInWmPaintEvent)
                return true;

            // One more complication! Sometimes with owner drawn virtual lists, the act of drawing
            // the overlays triggers a second attempt to paint the control -- which makes an annoying
            // flicker. So, we only do the custom drawing once per WmPaint event.
            if (!this.shouldDoCustomDrawing)
                return true;

            switch (nmcustomdraw.nmcd.dwDrawStage) {
                case CDDS_PREPAINT:
                    //System.Diagnostics.Debug.WriteLine("CDDS_PREPAINT");

                    // If there are any items, we have to wait until at least one has been painted 
                    // before we draw the overlays. If there aren't any items, there will never be any
                    // item paint events, so we can draw the overlays whenever
                    this.isAfterItemPaint = (this.GetItemCount() == 0);
                    this.prePaintLevel++;
                    base.WndProc(ref m);
                    // Make sure that we get postpaint notifications
                    m.Result = (IntPtr)((int)m.Result | CDRF_NOTIFYPOSTPAINT | CDRF_NOTIFYPOSTERASE);
                    return true;

                case CDDS_POSTPAINT:
                    //System.Diagnostics.Debug.WriteLine("CDDS_POSTPAINT");
                    this.prePaintLevel--;

                    // When in group view, we have two problems. On XP, the control sends 
                    // a whole heap of PREPAINT/POSTPAINT messages before drawing any items.
                    // We have to wait until after the first item paint before we draw overlays.
                    // On Vista, we have a different problem. On Vista, the control nests calls
                    // to PREPAINT and POSTPAINT. We only want to draw overlays on the outermost
                    // POSTPAINT.
                    if (this.prePaintLevel == 0 && (this.isMarqueSelecting || this.isAfterItemPaint)) {
                        this.shouldDoCustomDrawing = false;

                        // Draw our overlays after everything has been drawn
                        using (Graphics g = Graphics.FromHdc(nmcustomdraw.nmcd.hdc)) {
                            this.DrawAllDecorations(g);
                        }
                    }
                    break;

                case CDDS_ITEMPREPAINT:
                    //System.Diagnostics.Debug.WriteLine("CDDS_ITEMPREPAINT");

                    // When in group view on XP, the control send a whole heap of PREPAINT/POSTPAINT
                    // messages before drawing any items.
                    // We have to wait until after the first item paint before we draw overlays
                    this.isAfterItemPaint = true;

                    // This scheme of catching custom draw msgs works fine, except
                    // for Tile view. Something in .NET's handling of Tile view causes lots
                    // of invalidates and erases. So, we just ignore completely
                    // .NET's handling of Tile view and let the underlying control
                    // do its stuff. Strangely, if the Tile view is
                    // completely owner drawn, those erasures don't happen.
                    if (this.View == View.Tile) {
                        if (this.OwnerDraw && this.ItemRenderer != null)
                            base.WndProc(ref m);
                    } else {
                        base.WndProc(ref m);
                    }

                    //NativeMethods.NMLVCUSTOMDRAW nmcustomdraw2 = (NativeMethods.NMLVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMLVCUSTOMDRAW));
                    //nmcustomdraw2.clrText = ColorTranslator.ToWin32(Color.Red);
                    //Marshal.StructureToPtr(nmcustomdraw2, m.LParam, false);
                    //m.Result = (IntPtr)CDRF_NOTIFYSUBITEMDRAW;

                    m.Result = (IntPtr)((int)m.Result | CDRF_NOTIFYPOSTPAINT | CDRF_NOTIFYPOSTERASE);
                    return true;

                case CDDS_ITEMPOSTPAINT:
                    //System.Diagnostics.Debug.WriteLine("CDDS_ITEMPOSTPAINT");
                    break;

                case CDDS_SUBITEMPREPAINT:
                    //System.Diagnostics.Debug.WriteLine(String.Format("CDDS_SUBITEMPREPAINT ({0},{1})", (int)nmcustomdraw.nmcd.dwItemSpec, nmcustomdraw.iSubItem));

                    // There is a bug in the .NET framework which appears when column 0 of an owner drawn listview
                    // is dragged to another column position.
                    // The bounds calculation always returns the left edge of column 0 as being 0.
                    // The effects of this bug become apparent 
                    // when the listview is scrolled horizontally: the control can think that column 0
                    // is no longer visible (the horizontal scroll position is subtracted from the bounds, giving a
                    // rectangle that is offscreen). In those circumstances, column 0 is not redraw because 
                    // the control thinks it is not visible and so does not trigger a DrawSubItem event.
                    
                    // To fix this problem, we have to detected the situation -- owner drawing column 0 in any column except 0 --
                    // trigger our own DrawSubItem, and then prevent the default processing from occuring.

                    // Are we owner drawing column 0 when it's in any column except 0?
                    if (!this.OwnerDraw)
                        return false;

                    int columnIndex = nmcustomdraw.iSubItem;
                    if (columnIndex != 0)
                        return false;

                    int displayIndex = this.Columns[0].DisplayIndex;
                    if (displayIndex == 0)
                        return false;

                    int rowIndex = (int)nmcustomdraw.nmcd.dwItemSpec;
                    OLVListItem item = this.GetItem(rowIndex);
                    if (item == null)
                        return false;

                    // OK. We have the error condition, so lets do what the .NET framework should do.
                    // Trigger an event to draw column 0 when it is not at display index 0
                    using (Graphics g = Graphics.FromHdc(nmcustomdraw.nmcd.hdc)) {

                        // Correctly calculate the bounds of cell 0
                        Rectangle r = GetCellZeroBounds(rowIndex);

                        // We can hardcode "0" here since we know we are only doing this for column 0
                        DrawListViewSubItemEventArgs args = new DrawListViewSubItemEventArgs(g, r, item, item.SubItems[0], rowIndex, 0,
                            this.Columns[0], (ListViewItemStates)nmcustomdraw.nmcd.uItemState);
                        this.OnDrawSubItem(args);

                        // If the event handler wants to do the default processing (i.e. DrawDefault = true), we are stuck.
                        // There is no way we can force the default drawing because of the bug in .NET we are trying to get around.
                        System.Diagnostics.Trace.Assert(!args.DrawDefault, "Default drawing is impossible in this situation");
                    }
                    m.Result = (IntPtr)4;

                    return true;

                case CDDS_SUBITEMPOSTPAINT:
                    //System.Diagnostics.Debug.WriteLine("CDDS_SUBITEMPOSTPAINT");
                    break;

                // I have included these stages, but it doesn't seem that they are sent for ListViews.
                // http://www.tech-archive.net/Archive/VC/microsoft.public.vc.mfc/2006-08/msg00220.html

                case CDDS_PREERASE:
                    //System.Diagnostics.Debug.WriteLine("CDDS_PREERASE");
                    break;

                case CDDS_POSTERASE:
                    //System.Diagnostics.Debug.WriteLine("CDDS_POSTERASE");
                    break;

                case CDDS_ITEMPREERASE:
                    //System.Diagnostics.Debug.WriteLine("CDDS_ITEMPREERASE");
                    break;

                case CDDS_ITEMPOSTERASE:
                    //System.Diagnostics.Debug.WriteLine("CDDS_ITEMPOSTERASE");
                    break;
            }

            return false;
        }
        bool isAfterItemPaint;

        private Rectangle GetCellZeroBounds(int rowIndex) {
            Rectangle r = this.GetItemRect(rowIndex, ItemBoundsPortion.Entire);
            Point sides = NativeMethods.GetScrolledColumnSides(this, 0);
            r.X = sides.X + 1;
            r.Width = sides.Y - sides.X;
            return r;
        }

        /// <summary>
        /// Handle the underlying control being destroyed
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected virtual bool HandleDestroy(ref Message m) {
            //System.Diagnostics.Debug.WriteLine("WM_DESTROY");
            if (this.cellToolTip == null)
                return false;

            // When the underlying control is destroyed, we need to recreate
            // and reconfigure its tooltip
            this.cellToolTip.PushSettings();
            base.WndProc(ref m);
            this.BeginInvoke((MethodInvoker)delegate {
                this.UpdateCellToolTipHandle();
                this.cellToolTip.PopSettings();
            });
            return true;
        }

        /// <summary>
        /// Handle the search for item m if possible.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleFindItem(ref Message m) {
            // NOTE: As far as I can see, this message is never actually sent to the control, making this
            // method redundant!

            const int LVFI_STRING = 0x0002;

            NativeMethods.LVFINDINFO findInfo = (NativeMethods.LVFINDINFO)m.GetLParam(typeof(NativeMethods.LVFINDINFO));

            // We can only handle string searches
            if ((findInfo.flags & LVFI_STRING) != LVFI_STRING)
                return false;

            int start = m.WParam.ToInt32();
            m.Result = (IntPtr)this.FindMatchingRow(findInfo.psz, start, SearchDirectionHint.Down);
            return true;
        }

        /// <summary>
        /// Find the first row after the given start in which the text value in the
        /// comparison column begins with the given text. The comparison column is column 0,
        /// unless IsSearchOnSortColumn is true, in which case the current sort column is used.
        /// </summary>
        /// <param name="text">The text to be prefix matched</param>
        /// <param name="start">The index of the first row to consider</param>
        /// <param name="direction">Which direction should be searched?</param>
        /// <returns>The index of the first row that matched, or -1</returns>
        /// <remarks>The text comparison is a case-insensitive, prefix match. The search will
        /// search the every row until a match is found, wrapping at the end if needed.</remarks>
        public virtual int FindMatchingRow(string text, int start, SearchDirectionHint direction) {
            // We also can't do anything if we don't have data
            int rowCount = this.GetItemCount();
            if (rowCount == 0)
                return -1;

            // Which column are we going to use for our comparing?
            OLVColumn column = this.GetColumn(0);
            if (this.IsSearchOnSortColumn && this.View == View.Details && this.LastSortColumn != null)
                column = this.LastSortColumn;

            // Do two searches if necessary to find a match. The second search is the wrap-around part of searching
            int i;
            if (direction == SearchDirectionHint.Down) {
                i = this.FindMatchInRange(text, start, rowCount - 1, column);
                if (i == -1 && start > 0)
                    i = this.FindMatchInRange(text, 0, start - 1, column);
            } else {
                i = this.FindMatchInRange(text, start, 0, column);
                if (i == -1 && start != rowCount)
                    i = this.FindMatchInRange(text, rowCount - 1, start + 1, column);
            }

            return i;
        }

        /// <summary>
        /// Find the first row in the given range of rows that prefix matches the string value of the given column.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="column"></param>
        /// <returns>The index of the matched row, or -1</returns>
        protected virtual int FindMatchInRange(string text, int first, int last, OLVColumn column) {
            if (first <= last) {
                for (int i = first; i <= last; i++) {
                    string data = column.GetStringValue(this.GetNthItemInDisplayOrder(i).RowObject);
                    if (data.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            } else {
                for (int i = first; i >= last; i--) {
                    string data = column.GetStringValue(this.GetNthItemInDisplayOrder(i).RowObject);
                    if (data.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Handle an InsertGroup message
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected virtual bool HandleInsertGroup(ref Message m) {
            if (ObjectListView.IsVista && this.HasCollapsibleGroups) {
                NativeMethods.LVGROUP group = (NativeMethods.LVGROUP)Marshal.PtrToStructure(
                    m.LParam, typeof(NativeMethods.LVGROUP));
                group.state = (int)NativeMethods.GroupState.COLLAPSIBLE;
                group.mask = group.mask ^ 4; // LVGF_STATE 
                Marshal.StructureToPtr(group, m.LParam, true);
            }
            return false;
        }

        /// <summary>
        /// Handle a key down message
        /// </summary>
        /// <param name="m"></param>
        /// <returns>True if the msg has been handled</returns>
        protected virtual bool HandleKeyDown(ref Message m) {

            // If this is a checkbox list, toggle the selected rows when the user presses Space
            if (this.CheckBoxes && m.WParam.ToInt32() == (int)Keys.Space && this.SelectedIndices.Count > 0) {
                this.ToggleSelectedRows();
                return true;
            }

            // Remember the scroll position so we can decide if the listview has scrolled in the 
            // handling of the event.
            int scrollPositionH = NativeMethods.GetScrollPosition(this, true);
            int scrollPositionV = NativeMethods.GetScrollPosition(this, false);

            base.WndProc(ref m);

            // If the keydown processing changed the scroll position, trigger a Scroll event    
            int newScrollPositionH = NativeMethods.GetScrollPosition(this, true);
            int newScrollPositionV = NativeMethods.GetScrollPosition(this, false);

            if (scrollPositionH != newScrollPositionH) {
                ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, 
                    scrollPositionH, newScrollPositionH, ScrollOrientation.HorizontalScroll);
                this.OnScroll(args);
            } else if (scrollPositionV != newScrollPositionV) {
                ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, 
                    scrollPositionV, newScrollPositionV, ScrollOrientation.VerticalScroll);
                this.OnScroll(args);
            }

            return true;
        }

        private void ToggleSelectedRows() {
            // This doesn't actually toggle all rows. It toggles the first row, and
            // all other rows get the check state of that first row.
            Object primaryModel = this.GetItem(this.SelectedIndices[0]).RowObject;
            this.ToggleCheckObject(primaryModel);
            CheckState? state = this.GetCheckState(primaryModel);
            if (state.HasValue) {
                foreach (Object x in this.SelectedObjects)
                    this.SetObjectCheckedness(x, state.Value);
            }
        }

        /// <summary>
        /// Catch the Left Button down event.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleLButtonDown(ref Message m) {
            /// We have to intercept this low level message rather than the more natural
            /// overridding of OnMouseDown, since ListCtrl's internal mouse down behavior
            /// is to select (or deselect) rows when the mouse is released. We don't
            /// want the selection to change when the user checks or unchecks a checkbox, so if the
            /// mouse down event was to check/uncheck, we have to hide this mouse
            /// down event from the control.

            int x = m.LParam.ToInt32() & 0xFFFF;
            int y = (m.LParam.ToInt32() >> 16) & 0xFFFF;

            OlvListViewHitTestInfo hti = this.OlvHitTest(x, y, true);
            return this.ProcessLButtonDown(hti);
        }

        /// <summary>
        /// Handle a mouse down at the given hit test location
        /// </summary>
        /// <remarks>Subclasses can override this to do something unique</remarks>
        /// <param name="hti"></param>
        /// <returns>True if the message has been handled</returns>
        protected virtual bool ProcessLButtonDown(OlvListViewHitTestInfo hti) {
            if (hti.Item == null)
                return false;

            // If they didn't click checkbox, we can just return
            if (this.View != View.Details || hti.HitTestLocation != HitTestLocation.CheckBox)
                return false;

            // Did they click a sub item checkbox?
            if (hti.Column.Index > 0) {
                this.ToggleSubItemCheckBox(hti.RowObject, hti.Column);
                return true;
            }

            // They must have clicked the primary checkbox
            this.ToggleCheckObject(hti.RowObject);

            // If they change the checkbox of a selecte row, all the rows in the selection
            // should be given the same state
            if (hti.Item.Selected) {
                CheckState? state = this.GetCheckState(hti.RowObject);
                if (state.HasValue) {
                foreach (Object x in this.SelectedObjects)
                        this.SetObjectCheckedness(x, state.Value);
            }
            }

            return true;
        }

        /// <summary>
        /// Catch the Left Button double click event.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleLButtonDoubleClick(ref Message m) {
            int x = m.LParam.ToInt32() & 0xFFFF;
            int y = (m.LParam.ToInt32() >> 16) & 0xFFFF;

            OlvListViewHitTestInfo hti = this.OlvHitTest(x, y);
            return this.ProcessLButtonDoubleClick(hti);
        }

        /// <summary>
        /// Handle a mouse double click at the given hit test location
        /// </summary>
        /// <remarks>Subclasses can override this to do something unique</remarks>
        /// <param name="hti"></param>
        /// <returns>True if the message has been handled</returns>
        protected virtual bool ProcessLButtonDoubleClick(OlvListViewHitTestInfo hti) {

            // If the user double clicked on a checkbox, ignore it
            return (hti.HitTestLocation == HitTestLocation.CheckBox);
        }

        /// <summary>
        /// In the notification messages, we handle change of state of list items
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleReflectNotify(ref Message m) {
            const int NM_DBLCLK = -3;
            const int NM_RDBLCLK = -6;
            const int NM_CUSTOMDRAW = -12;
            const int NM_RELEASEDCAPTURE = -16;
            const int LVN_ITEMCHANGED = -101;
            const int LVN_ITEMCHANGING = -100; 
            const int LVN_MARQUEBEGIN = -156;
            const int LVN_BEGINSCROLL = -180;
            const int LVN_ENDSCROLL = -181;
            const int LVIF_STATE = 8;

            bool isMsgHandled = false;

            NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
            //System.Diagnostics.Debug.WriteLine(String.Format("rn: {0}", nmhdr->code));

            switch (nmhdr.code) {
                case LVN_BEGINSCROLL:
                    //System.Diagnostics.Debug.WriteLine("LVN_BEGINSCROLL");

                    NativeMethods.NMLVSCROLL nmlvscroll = (NativeMethods.NMLVSCROLL)m.GetLParam(typeof(NativeMethods.NMLVSCROLL));
                    if (nmlvscroll.dx != 0) {
                        int scrollPositionH = NativeMethods.GetScrollPosition(this, true);
                        ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, scrollPositionH - nmlvscroll.dx, scrollPositionH, ScrollOrientation.HorizontalScroll);
                        this.OnScroll(args);
                    }
                    else if (nmlvscroll.dy != 0)
                    {
                        int scrollPositionV = NativeMethods.GetScrollPosition(this, false);
                        ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.EndScroll, scrollPositionV - nmlvscroll.dy, scrollPositionV, ScrollOrientation.VerticalScroll);
                        this.OnScroll(args);
                    }
                    break;

                case LVN_ENDSCROLL:
                    //System.Diagnostics.Debug.WriteLine("LVN_ENDSCROLL");
                    break;

                case LVN_MARQUEBEGIN:
                    //System.Diagnostics.Debug.WriteLine("LVN_MARQUEBEGIN"); 
                    this.isMarqueSelecting = true;
                    break;

                case NM_RELEASEDCAPTURE:
                    //System.Diagnostics.Debug.WriteLine("NM_RELEASEDCAPTURE"); 
                    this.isMarqueSelecting = false;
                    this.Invalidate();
                    break;

                case NM_CUSTOMDRAW:
                    //System.Diagnostics.Debug.WriteLine("NM_CUSTOMDRAW");
                    isMsgHandled = this.HandleCustomDraw(ref m);
                    break;

                case NM_DBLCLK:
                    // The default behavior of a .NET ListView with checkboxes is to toggle the checkbox on 
                    // double-click. That's just silly, if you ask me :)
                    if (this.CheckBoxes) {
                        // How do we make ListView not do that silliness? We could just ignore the message
                        // but the last part of the base code sets up state information, and without that
                        // state, the ListView doesn't trigger MouseDoubleClick events. So we fake a 
                        // right button double click event, which sets up the same state, but without
                        // toggling the checkbox.
                        nmhdr.code = NM_RDBLCLK;
                        Marshal.StructureToPtr(nmhdr, m.LParam, false);
                    }
                    break;

                case LVN_ITEMCHANGED:
                    NativeMethods.NMLISTVIEW nmlistviewPtr2 = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
                    if ((nmlistviewPtr2.uChanged & LVIF_STATE) != 0) {
                        CheckState currentValue = this.CalculateState(nmlistviewPtr2.uOldState);
                        CheckState newCheckValue = this.CalculateState(nmlistviewPtr2.uNewState);
                        if (currentValue != newCheckValue) {
                            // Remove the state indicies so that we don't trigger the OnItemChecked method
                            // when we call our base method after exiting this method
                            nmlistviewPtr2.uOldState = (nmlistviewPtr2.uOldState & 0x0FFF);
                            nmlistviewPtr2.uNewState = (nmlistviewPtr2.uNewState & 0x0FFF);
                            Marshal.StructureToPtr(nmlistviewPtr2, m.LParam, false);
                        }
                    }
                    break;

                case LVN_ITEMCHANGING:
                    NativeMethods.NMLISTVIEW nmlistviewPtr = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
                    if ((nmlistviewPtr.uChanged & LVIF_STATE) != 0) {
                        CheckState currentValue = this.CalculateState(nmlistviewPtr.uOldState);
                        CheckState newCheckValue = this.CalculateState(nmlistviewPtr.uNewState);

                        if (currentValue != newCheckValue) {
                            // Prevent the base method from seeing the state change,
                            // since we handled it elsewhere
                            nmlistviewPtr.uChanged &= ~LVIF_STATE;
                            Marshal.StructureToPtr(nmlistviewPtr, m.LParam, false);
                        }
                    }
                    break;
            }

            return isMsgHandled;
        }

        private CheckState CalculateState(int state) {
            switch ((state & 0xf000) >> 12) {
                case 1:
                    return CheckState.Unchecked;
                case 2:
                    return CheckState.Checked;
                case 3:
                    return CheckState.Indeterminate;
                default:
                    return CheckState.Checked;
            }
        }

        /// <summary>
        /// In the notification messages, we handle attempts to change the width of our columns
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected bool HandleNotify(ref Message m) {
            bool isMsgHandled = false;

            const int HDN_FIRST = (0 - 300);
            const int HDN_ITEMCHANGINGA = (HDN_FIRST - 0);
            const int HDN_ITEMCHANGINGW = (HDN_FIRST - 20);
            const int HDN_ITEMCLICKA = (HDN_FIRST - 2);
            const int HDN_ITEMCLICKW = (HDN_FIRST - 22);
            const int HDN_DIVIDERDBLCLICKA = (HDN_FIRST - 5);
            const int HDN_DIVIDERDBLCLICKW = (HDN_FIRST - 25);
            const int HDN_BEGINTRACKA = (HDN_FIRST - 6);
            const int HDN_BEGINTRACKW = (HDN_FIRST - 26);
            //const int HDN_ENDTRACKA = (HDN_FIRST - 7);
            //const int HDN_ENDTRACKW = (HDN_FIRST - 27);
            const int HDN_TRACKA = (HDN_FIRST - 8);
            const int HDN_TRACKW = (HDN_FIRST - 28);

            // Handle the notification, remembering to handle both ANSI and Unicode versions
            NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
            //System.Diagnostics.Debug.WriteLine(String.Format("not: {0}", nmhdr->code));

            //if (nmhdr.code < HDN_FIRST)
            //    System.Diagnostics.Debug.WriteLine(nmhdr.code);

            // In KB Article #183258, MS states that when a header control has the HDS_FULLDRAG style, it will receive
            // ITEMCHANGING events rather than TRACK events. Under XP SP2 (at least) this is not always true, which may be
            // why MS has withdrawn that particular KB article. It is true that the header is always given the HDS_FULLDRAG
            // style. But even while window style set, the control doesn't always received ITEMCHANGING events.
            // The controlling setting seems to be the Explorer option "Show Window Contents While Dragging"!
            // In the category of "truly bizarre side effects", if the this option is turned on, we will receive
            // ITEMCHANGING events instead of TRACK events. But if it is turned off, we receive lots of TRACK events and
            // only one ITEMCHANGING event at the very end of the process.
            // If we receive HDN_TRACK messages, it's harder to control the resizing process. If we return a result of 1, we
            // cancel the whole drag operation, not just that particular track event, which is clearly not what we want.
            // If we are willing to compile with unsafe code enabled, we can modify the size of the column in place, using the
            // commented out code below. But without unsafe code, the best we can do is allow the user to drag the column to
            // any width, and then spring it back to within bounds once they release the mouse button. UI-wise it's very ugly.
            switch (nmheader.nhdr.code) {

                case HDN_ITEMCLICKA:
                case HDN_ITEMCLICKW:
                    if (!this.PossibleFinishCellEditing()) {
                        m.Result = (IntPtr)1; // prevent the change from happening
                        isMsgHandled = true;
                    }
                    break;

                case HDN_DIVIDERDBLCLICKA:
                case HDN_DIVIDERDBLCLICKW:
                case HDN_BEGINTRACKA:
                case HDN_BEGINTRACKW:
                    if (!this.PossibleFinishCellEditing()) {
                        m.Result = (IntPtr)1; // prevent the change from happening
                        isMsgHandled = true;
                        break;
                    }
                    if (nmheader.iItem >= 0 && nmheader.iItem < this.Columns.Count) {
                        OLVColumn column = this.GetColumn(nmheader.iItem);
                        // Space filling columns can't be dragged or double-click resized
                        if (column.FillsFreeSpace) {
                            m.Result = (IntPtr)1; // prevent the change from happening
                            isMsgHandled = true;
                        }
                    }
                    break;

                case HDN_TRACKA:
                case HDN_TRACKW:
                    if (nmheader.iItem >= 0 && nmheader.iItem < this.Columns.Count) {
                        NativeMethods.HDITEM hditem = (NativeMethods.HDITEM)Marshal.PtrToStructure(nmheader.pHDITEM, typeof(NativeMethods.HDITEM));
                        OLVColumn column = this.GetColumn(nmheader.iItem);
                        if (hditem.cxy < column.MinimumWidth)
                            hditem.cxy = column.MinimumWidth;
                        else if (column.MaximumWidth != -1 && hditem.cxy > column.MaximumWidth)
                            hditem.cxy = column.MaximumWidth;
                        Marshal.StructureToPtr(hditem, nmheader.pHDITEM, false);
                    }
                    break;

                case HDN_ITEMCHANGINGA:
                case HDN_ITEMCHANGINGW:
                    nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
                    if (nmheader.iItem >= 0 && nmheader.iItem < this.Columns.Count) {
                        NativeMethods.HDITEM hditem = (NativeMethods.HDITEM)Marshal.PtrToStructure(nmheader.pHDITEM, typeof(NativeMethods.HDITEM));
                        OLVColumn column = this.GetColumn(nmheader.iItem);
                        // Check the mask to see if the width field is valid, and if it is, make sure it's within range
                        if ((hditem.mask & 1) == 1) {
                            if (hditem.cxy < column.MinimumWidth ||
                                (column.MaximumWidth != -1 && hditem.cxy > column.MaximumWidth)) {
                                m.Result = (IntPtr)1; // prevent the change from happening
                                isMsgHandled = true;
                            }
                        }
                    }
                    break;

                case ToolTipControl.TTN_SHOW:
                    //System.Diagnostics.Debug.WriteLine("olv TTN_SHOW");
                    System.Diagnostics.Trace.Assert(this.CellToolTip.Handle == nmheader.nhdr.hwndFrom);
                    isMsgHandled = this.CellToolTip.HandleShow(ref m);
                    break;

                case ToolTipControl.TTN_POP:
                    //System.Diagnostics.Debug.WriteLine("olv TTN_POP");
                    System.Diagnostics.Trace.Assert(this.CellToolTip.Handle == nmheader.nhdr.hwndFrom);
                    isMsgHandled = this.CellToolTip.HandlePop(ref m);
                    break;

                case ToolTipControl.TTN_GETDISPINFO:
                    //System.Diagnostics.Debug.WriteLine("olv TTN_GETDISPINFO");
                    System.Diagnostics.Trace.Assert(this.CellToolTip.Handle == nmheader.nhdr.hwndFrom);
                    isMsgHandled = this.CellToolTip.HandleGetDispInfo(ref m);
                    break;
            }

            return isMsgHandled;
        }

        /// <summary>
        /// Create a ToolTipControl to manage the tooltip control used by the listview control
        /// </summary>
        protected virtual void CreateCellToolTip() {
            this.cellToolTip = new ToolTipControl();
            this.cellToolTip.AssignHandle(NativeMethods.GetTooltipControl(this));
            this.cellToolTip.Showing += new EventHandler<ToolTipShowingEventArgs>(HandleCellToolTipShowing);
            this.cellToolTip.SetMaxWidth();
            NativeMethods.MakeTopMost(this.cellToolTip);
        }

        /// <summary>
        /// Update the handle used by our cell tooltip to be the tooltip used by
        /// the underlying Windows listview control.
        /// </summary>
        protected virtual void UpdateCellToolTipHandle() {
            if (this.cellToolTip != null && this.cellToolTip.Handle == IntPtr.Zero) 
                this.cellToolTip.AssignHandle(NativeMethods.GetTooltipControl(this));
        }

        /// <summary>
        /// Handle the WM_PAINT event
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Return true if the msg has been handled and nothing further should be done</returns>
        protected virtual bool HandlePaint(ref Message m) {
            //System.Diagnostics.Debug.WriteLine("> WMPAINT");

            // We only want to custom draw the control within WmPaint message and only
            // once per paint event. We use these bools to insure this.
            this.isInWmPaintEvent = true;
            this.shouldDoCustomDrawing = true;
            this.prePaintLevel = 0;

            this.ShowOverlays();

            this.HandlePrePaint();
            base.WndProc(ref m);
            this.HandlePostPaint();
            this.isInWmPaintEvent = false;
            //System.Diagnostics.Debug.WriteLine("< WMPAINT");
            return true;
        }
        private int prePaintLevel;

        /// <summary>
        /// Perform any steps needed before painting the control
        /// </summary>
        protected virtual void HandlePrePaint() {
            // When we get a WM_PAINT msg, remember the rectangle that is being updated.
            // We can't get this information later, since the BeginPaint call wipes it out.
            this.lastUpdateRectangle = NativeMethods.GetUpdateRect(this);

            //// When the list is empty, we want to handle the drawing of the control by ourselves.
            //// Unfortunately, there is no easy way to tell our superclass that we want to do this.
            //// So we resort to guile and deception. We validate the list area of the control, which
            //// effectively tells our superclass that this area does not need to be painted.
            //// Our superclass will then not paint the control, leaving us free to do so ourselves.
            //// Without doing this trickery, the superclass will draw the list as empty,
            //// and then moments later, we will draw the empty list msg, giving a nasty flicker
            //if (this.GetItemCount() == 0 && this.HasEmptyListMsg)
            //    NativeMethods.ValidateRect(this, this.ClientRectangle);
        }

        /// <summary>
        /// Perform any steps needed after painting the control
        /// </summary>
        protected virtual void HandlePostPaint() {
            // This message is no longer necessary, but we keep it for compatibility
        }
        
        /// <summary>
        /// Handle the window position changing.
        /// </summary>
        /// <param name="m">The m to be processed</param>
        /// <returns>bool to indicate if the m has been handled</returns>
        protected virtual bool HandleWindowPosChanging(ref Message m) {
            const int SWP_NOSIZE = 1;

            NativeMethods.WINDOWPOS pos = (NativeMethods.WINDOWPOS)m.GetLParam(typeof(NativeMethods.WINDOWPOS));
            if ((pos.flags & SWP_NOSIZE) == 0) {
                if (pos.cx < this.Bounds.Width) // only when shrinking
                    // pos.cx is the window width, not the client area width, so we have to subtract the border widths
                    this.ResizeFreeSpaceFillingColumns(pos.cx - (this.Bounds.Width - this.ClientSize.Width));
            }

            return false;
        }

        #endregion

        #region Column header clicking, column hiding and resizing

        protected HeaderControl HeaderControl {
            get {
                if (this.headerControl == null)
                    this.headerControl = new HeaderControl(this);
                return this.headerControl;
            }
        }
        private HeaderControl headerControl;

        /// <summary>
        /// The user has right clicked on the column headers. Do whatever is required
        /// </summary>
        /// <returns>Return true if this event has been handle</returns>
        protected virtual bool HandleHeaderRightClick(int columnIndex) {
            ColumnClickEventArgs eventArgs = new ColumnClickEventArgs(columnIndex);
            this.OnColumnRightClick(eventArgs);

            if (this.SelectColumnsOnRightClick)
                this.ShowColumnSelectMenu(Cursor.Position);

            return this.SelectColumnsOnRightClick;
        }

        /// <summary>
        /// The user has right clicked on the column headers. Do whatever is required
        /// </summary>
        /// <returns>Return true if this event has been handle</returns>
        [Obsolete("Use HandleHeaderRightClick(int) instead")]
        protected virtual bool HandleHeaderRightClick() {
            return false;
        }

        /// <summary>
        /// Show a popup menu at the given point which will allow the user to choose which columns
        /// are visible on this listview
        /// </summary>
        /// <param name="pt">Where should the menu be placed</param>
        protected virtual void ShowColumnSelectMenu(Point pt) {
            ToolStripDropDown m = this.MakeColumnSelectMenu(new ContextMenuStrip());
            m.Show(pt);
        }

        /// <summary>
        /// Append the column selection menu items to the given menu strip.
        /// </summary>
        /// <param name="strip">The menu to which the items will be added.</param>
        /// <returns>Return the menu to which the items were added</returns>
        public virtual ToolStripDropDown MakeColumnSelectMenu(ToolStripDropDown strip) {
            strip.ItemClicked += new ToolStripItemClickedEventHandler(ColumnSelectMenu_ItemClicked);
            strip.Closing += new ToolStripDropDownClosingEventHandler(ColumnSelectMenu_Closing);

            List<OLVColumn> columns = new List<OLVColumn>(this.AllColumns);
            // Sort columns alphabetically
            //columns.Sort(delegate(OLVColumn x, OLVColumn y) { return String.Compare(x.Text, y.Text, true); });

            // Sort columns by display order
            if (this.AllColumns.Count > 0 && this.AllColumns[0].LastDisplayIndex == -1)
                this.RememberDisplayIndicies();
            columns.Sort(delegate(OLVColumn x, OLVColumn y) { return (x.LastDisplayIndex - y.LastDisplayIndex); });

            // Build menu from sorted columns
            foreach (OLVColumn col in columns) {
                ToolStripMenuItem mi = new ToolStripMenuItem(col.Text);
                mi.Checked = col.IsVisible;
                mi.Tag = col;

                // The 'Index' property returns -1 when the column is not visible, so if the
                // column isn't visible we have to enable the item. Also the first column can't be turned off
                mi.Enabled = !col.IsVisible || (col.Index > 0);
                strip.Items.Add(mi);
            }

            return strip;
        }

        private void ColumnSelectMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            ToolStripMenuItem mi = (ToolStripMenuItem)e.ClickedItem;
            OLVColumn col = (OLVColumn)mi.Tag;
            mi.Checked = !mi.Checked;
            col.IsVisible = mi.Checked;
            this.BeginInvoke(new MethodInvoker(this.RebuildColumns));
        }

        private void ColumnSelectMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e) {
            e.Cancel = (this.SelectColumnsMenuStaysOpen &&
                e.CloseReason == ToolStripDropDownCloseReason.ItemClicked);
        }

        /// <summary>
        /// Override the OnColumnReordered method to do what we want
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnReordered(ColumnReorderedEventArgs e) {
            base.OnColumnReordered(e);

            // The internal logic of the .NET code behind a ENDDRAG event means that,
            // at this point, the DisplayIndex's of the columns are not yet as they are
            // going to be. So we have to invoke a method to run later that will remember
            // what the real DisplayIndex's are.
            this.BeginInvoke(new MethodInvoker(this.RememberDisplayIndicies));
        }

        private void RememberDisplayIndicies() {
            // Remember the display indexes so we can put them back at a later date
            foreach (OLVColumn x in this.AllColumns)
                x.LastDisplayIndex = x.DisplayIndex;
        }

        /// <summary>
        /// When the column widths are changing, resize the space filling columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
            if (this.UpdateSpaceFillingColumnsWhenDraggingColumnDivider && !this.GetColumn(e.ColumnIndex).FillsFreeSpace) {
                // If the width of a column is increasing, resize any space filling columns allowing the extra
                // space that the new column width is going to consume
                int oldWidth = this.GetColumn(e.ColumnIndex).Width;
                if (e.NewWidth > oldWidth)
                    this.ResizeFreeSpaceFillingColumns(this.ClientSize.Width - (e.NewWidth - oldWidth));
                else
                    this.ResizeFreeSpaceFillingColumns();
            }
        }

        /// <summary>
        /// When the column widths change, resize the space filling columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e) {
            if (!this.GetColumn(e.ColumnIndex).FillsFreeSpace)
                this.ResizeFreeSpaceFillingColumns();
        }

        /// <summary>
        /// When the size of the control changes, we have to resize our space filling columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandleLayout(object sender, LayoutEventArgs e) {
            // We have to delay executing the recalculation of the columns, since virtual lists
            // get terribly confused if we resize the column widths during this event.
            if (this.Created) {
                this.BeginInvoke(new MethodInvoker(this.ResizeFreeSpaceFillingColumns));
            }
        }

        /// <summary>
        /// Resize our space filling columns so they fill any unoccupied width in the control
        /// </summary>
        protected virtual void ResizeFreeSpaceFillingColumns() {
            this.ResizeFreeSpaceFillingColumns(this.ClientSize.Width);
        }

        /// <summary>
        /// Resize our space filling columns so they fill any unoccupied width in the control
        /// </summary>
        protected virtual void ResizeFreeSpaceFillingColumns(int freeSpace) {
            // It's too confusing to dynamically resize columns at design time.
            if (this.DesignMode)
                return;

            if (this.Frozen)
                return;

            // Calculate the free space available
            int totalProportion = 0;
            List<OLVColumn> spaceFillingColumns = new List<OLVColumn>();
            for (int i = 0; i < this.Columns.Count; i++) {
                OLVColumn col = this.GetColumn(i);
                if (col.FillsFreeSpace) {
                    spaceFillingColumns.Add(col);
                    totalProportion += col.FreeSpaceProportion;
                } else
                    freeSpace -= col.Width;
            }
            freeSpace = Math.Max(0, freeSpace);

            // Any space filling column that would hit it's Minimum or Maximum
            // width must be treated as a fixed column.
            foreach (OLVColumn col in spaceFillingColumns.ToArray()) {
                int newWidth = (freeSpace * col.FreeSpaceProportion) / totalProportion;

                if (col.MinimumWidth != -1 && newWidth < col.MinimumWidth)
                    newWidth = col.MinimumWidth;
                else if (col.MaximumWidth != -1 && newWidth > col.MaximumWidth)
                    newWidth = col.MaximumWidth;
                else
                    newWidth = 0;

                if (newWidth > 0) {
                    col.Width = newWidth;
                    freeSpace -= newWidth;
                    totalProportion -= col.FreeSpaceProportion;
                    spaceFillingColumns.Remove(col);
                }
            }

            // Distribute the free space between the columns
            foreach (OLVColumn col in spaceFillingColumns) {
                col.Width = (freeSpace * col.FreeSpaceProportion) / totalProportion;
            }
        }

        #endregion

        #region Checkboxes

        /// <summary>
        /// Mark the given object as indeterminate check state
        /// </summary>
        /// <param name="modelObject">The model object to be marked indeterminate</param>
        public virtual void CheckIndeterminateObject(object modelObject) {
            this.SetObjectCheckedness(modelObject, CheckState.Indeterminate);
        }

        /// <summary>
        /// Mark the given object as checked in the list
        /// </summary>
        /// <param name="modelObject">The model object to be checked</param>
        public virtual void CheckObject(object modelObject) {
            this.SetObjectCheckedness(modelObject, CheckState.Checked);
        }

        /// <summary>
        /// Put a check into the check box at the given cell
        /// </summary>
        /// <param name="rowObject"></param>
        /// <param name="column"></param>
        public virtual void CheckSubItem(object rowObject, OLVColumn column) {
            if (column == null || rowObject == null || !column.CheckBoxes)
                return;

            column.PutCheckState(rowObject, CheckState.Checked);
            this.RefreshObject(rowObject);
        }

        /// <summary>
        /// Put an indeterminate check into the check box at the given cell
        /// </summary>
        /// <param name="rowObject"></param>
        /// <param name="column"></param>
        public virtual void CheckIndeterminateSubItem(object rowObject, OLVColumn column) {
            if (column == null || rowObject == null || !column.CheckBoxes)
                return;

            column.PutCheckState(rowObject, CheckState.Indeterminate);
            this.RefreshObject(rowObject);
        }

        /// <summary>
        /// Return true of the given object is checked
        /// </summary>
        /// <param name="modelObject">The model object whose checkedness is returned</param>
        /// <returns>Is the given object checked?</returns>
        /// <remarks>If the given object is not in the list, this method returns false.</remarks>
        public virtual bool IsChecked(object modelObject) {
            OLVListItem olvi = this.ModelToItem(modelObject);
            if (olvi == null)
                return false;
            else
                return olvi.CheckState == CheckState.Checked;
        }

        /// <summary>
        /// Return true of the given object is indeterminately checked
        /// </summary>
        /// <param name="modelObject">The model object whose checkedness is returned</param>
        /// <returns>Is the given object indeterminately checked?</returns>
        /// <remarks>If the given object is not in the list, this method returns false.</remarks>
        public virtual bool IsCheckedIndeterminate(object modelObject) {
            OLVListItem olvi = this.ModelToItem(modelObject);
            if (olvi == null)
                return false;
            else
                return (olvi.CheckState == CheckState.Indeterminate);
        }

        /// <summary>
        /// Is there a check at the check box at the given cell
        /// </summary>
        /// <param name="rowObject"></param>
        /// <param name="column"></param>
        public virtual bool IsSubItemChecked(object rowObject, OLVColumn column) {
            if (column != null && rowObject != null && column.CheckBoxes)
                return (column.GetCheckState(rowObject) == CheckState.Checked);
            else
                return false;
        }

        /// <summary>
        /// Get the checkedness of an object from the model. Returning null means the
        /// model does not know and the value from the control will be used.
        /// </summary>
        /// <param name="modelObject"></param>
        /// <returns></returns>
        protected virtual CheckState? GetCheckState(Object modelObject) {
            if (this.CheckStateGetter == null)
                return null;
            else
                return this.CheckStateGetter(modelObject);
        }

        /// <summary>
        /// Record the change of checkstate for the given object in the model.
        /// This does not update the UI -- only the model
        /// </summary>
        /// <param name="modelObject"></param>
        /// <param name="state"></param>
        /// <returns>The check state that was recorded and that should be used to update
        /// the control.</returns>
        protected virtual CheckState PutCheckState(Object modelObject, CheckState state) {
            if (this.CheckStatePutter == null)
                return state;
            else
                return this.CheckStatePutter(modelObject, state);
        }

        /// <summary>
        /// Change the check state of the given object to be the given state.
        /// </summary>
        /// <param name="modelObject"></param>
        /// <param name="state"></param>
        protected virtual void SetObjectCheckedness(object modelObject, CheckState state) {
            OLVListItem olvi = this.ModelToItem(modelObject);
            if (olvi == null || olvi.CheckState == state)
                return;

            // Trigger checkbox changing event. We only need to do this for virtual
            // lists, since setting CheckState triggers these events for non-virtual lists
            ItemCheckEventArgs ice = new ItemCheckEventArgs(olvi.Index, state, olvi.CheckState);
            this.OnItemCheck(ice);
            if (ice.NewValue == olvi.CheckState)
                return;

            olvi.CheckState = this.PutCheckState(modelObject, state);
            this.RefreshItem(olvi);

            // Trigger check changed event
            this.OnItemChecked(new ItemCheckedEventArgs(olvi));
        }

        /// <summary>
        /// Toggle the checkedness of the given object. A checked object becomes
        /// unchecked; an unchecked or indeterminate object becomes checked.
        /// If the list has tristate checkboxes, the order is:
        ///    unchecked -> checked -> indeterminate -> unchecked ...
        /// </summary>
        /// <param name="modelObject">The model object to be checked</param>
        public virtual void ToggleCheckObject(object modelObject) {
            OLVListItem olvi = this.ModelToItem(modelObject);
            if (olvi == null)
                return;

            CheckState newState = CheckState.Checked;

            if (olvi.CheckState == CheckState.Checked) {
                if (this.TriStateCheckBoxes)
                    newState = CheckState.Indeterminate;
                else
                    newState = CheckState.Unchecked;
            } else {
                if (olvi.CheckState == CheckState.Indeterminate && this.TriStateCheckBoxes)
                    newState = CheckState.Unchecked;
            }
            this.SetObjectCheckedness(modelObject, newState);
        }

        /// <summary>
        /// Toggle the check at the check box of the given cell
        /// </summary>
        /// <param name="rowObject"></param>
        /// <param name="column"></param>
        public virtual void ToggleSubItemCheckBox(object rowObject, OLVColumn column) {
            if (column.TriStateCheckBoxes) {
                if (column.GetCheckState(rowObject) == CheckState.Checked)
                    this.CheckIndeterminateSubItem(rowObject, column);
                else if (column.GetCheckState(rowObject) == CheckState.Indeterminate)
                    this.UncheckSubItem(rowObject, column);
                else
                    this.CheckSubItem(rowObject, column);
            } else
                if (this.IsSubItemChecked(rowObject, column))
                    this.UncheckSubItem(rowObject, column);
                else
                    this.CheckSubItem(rowObject, column);
        }

        /// <summary>
        /// Mark the given object as unchecked in the list
        /// </summary>
        /// <param name="modelObject">The model object to be unchecked</param>
        public virtual void UncheckObject(object modelObject) {
            this.SetObjectCheckedness(modelObject, CheckState.Unchecked);
        }

        /// <summary>
        /// Uncheck the check at the given cell
        /// </summary>
        /// <param name="rowObject"></param>
        /// <param name="column"></param>
        public virtual void UncheckSubItem(object rowObject, OLVColumn column) {
            if (column == null || rowObject == null || !column.CheckBoxes)
                return;

            column.PutCheckState(rowObject, CheckState.Unchecked);
            this.RefreshObject(rowObject);
        }

        #endregion

        #region OLV accessing

        /// <summary>
        /// Return the column at the given index
        /// </summary>
        /// <param name="index">Index of the column to be returned</param>
        /// <returns>An OLVColumn</returns>
        public virtual OLVColumn GetColumn(int index) {
            return (OLVColumn)this.Columns[index];
        }

        /// <summary>
        /// Return the column at the given title.
        /// </summary>
        /// <param name="name">Name of the column to be returned</param>
        /// <returns>An OLVColumn</returns>
        public virtual OLVColumn GetColumn(string name) {
            foreach (ColumnHeader column in this.Columns) {
                if (column.Text == name)
                    return (OLVColumn)column;
            }
            return null;
        }

        /// <summary>
        /// Return a collection of columns that are appropriate to the given view.
        /// Only Tile and Details have columns; all other views have 0 columns.
        /// </summary>
        /// <param name="view">Which view are the columns being calculate for?</param>
        /// <returns>A list of columns</returns>
        public virtual List<OLVColumn> GetFilteredColumns(View view) {
            // For both detail and tile view, the first column must be included. Normally, we would
            // use the ColumnHeader.Index property, but if the header is not currently part of a ListView
            // that property returns -1. So, we track the index of
            // the column header, and always include the first header.

            int index = 0;
            switch (view) {
                case View.Details:
                    return this.AllColumns.FindAll(delegate(OLVColumn x) { return (index++ == 0) || x.IsVisible; });
                case View.Tile:
                    return this.AllColumns.FindAll(delegate(OLVColumn x) { return (index++ == 0) || x.IsTileViewColumn; });
                default:
                    return new List<OLVColumn>();
            }
        }

        /// <summary>
        /// Return the number of items in the list
        /// </summary>
        /// <returns>the number of items in the list</returns>
        public virtual int GetItemCount() {
            return this.Items.Count;
        }

        /// <summary>
        /// Return the item at the given index
        /// </summary>
        /// <param name="index">Index of the item to be returned</param>
        /// <returns>An OLVListItem</returns>
        public virtual OLVListItem GetItem(int index) {
            if (index >= 0 && index < this.GetItemCount())
                return (OLVListItem)this.Items[index];
            else
                return null;
        }

        /// <summary>
        /// Return the model object at the given index
        /// </summary>
        /// <param name="index">Index of the model object to be returned</param>
        /// <returns>A model object</returns>
        public virtual object GetModelObject(int index) {
            OLVListItem item = this.GetItem(index);
            if (item == null)
                return null;
            else
                return item.RowObject;
        }

        /// <summary>
        /// Find the item and column that are under the given co-ords
        /// </summary>
        /// <param name="x">X co-ord</param>
        /// <param name="y">Y co-ord</param>
        /// <param name="selectedColumn">The column under the given point</param>
        /// <returns>The item under the given point. Can be null.</returns>
        public virtual OLVListItem GetItemAt(int x, int y, out OLVColumn selectedColumn) {
            selectedColumn = null;
            ListViewHitTestInfo info = this.HitTest(x, y);
            if (info.Item == null)
                return null;

            if (info.SubItem != null) {
                int subItemIndex = info.Item.SubItems.IndexOf(info.SubItem);
                selectedColumn = this.GetColumn(subItemIndex);
            }

            return (OLVListItem)info.Item;
        }

        #endregion

        #region Object manipulation

        /// <summary>
        /// Ensure that the given model object is visible
        /// </summary>
        /// <param name="modelObject">The model object to be revealed</param>
        public virtual void EnsureModelVisible(Object modelObject) {
            int idx = this.IndexOf(modelObject);
            if (idx >= 0)
                this.EnsureVisible(idx);
        }

        /// <summary>
        /// Return the model object of the row that is selected or null if there is no selection or more than one selection
        /// </summary>
        /// <returns>Model object or null</returns>
        public virtual object GetSelectedObject() {
            if (this.SelectedIndices.Count == 1)
                return this.GetModelObject(this.SelectedIndices[0]);
            else
                return null;
        }

        /// <summary>
        /// Return the model objects of the rows that are selected or an empty collection if there is no selection
        /// </summary>
        /// <returns>ArrayList</returns>
        public virtual ArrayList GetSelectedObjects() {
            ArrayList objects = new ArrayList(this.SelectedIndices.Count);
            foreach (int index in this.SelectedIndices)
                objects.Add(this.GetModelObject(index));

            return objects;
        }

        /// <summary>
        /// Return the model object of the row that is checked or null if no row is checked
        /// or more than one row is checked
        /// </summary>
        /// <returns>Model object or null</returns>
        /// <remarks>Use CheckedObject property instead of this method</remarks>
        [Obsolete("Use CheckedObject property instead of this method")]
        public virtual object GetCheckedObject() {
            return this.CheckedObject;
        }

        /// <summary>
        /// Get the collection of model objects that are checked.
        /// </summary>
        /// <remarks>Use CheckedObjects property instead of this method</remarks>
        [Obsolete("Use CheckedObjects property instead of this method")]
        public virtual ArrayList GetCheckedObjects() {
            return (ArrayList)this.CheckedObjects;
        }

        /// <summary>
        /// Find the given model object within the listview and return its index
        /// </summary>
        /// <remarks>Technically, this method will work with virtual lists, but it will
        /// probably be very slow.</remarks>
        /// <param name="modelObject">The model object to be found</param>
        /// <returns>The index of the object. -1 means the object was not present</returns>
        public virtual int IndexOf(Object modelObject) {
            for (int i = 0; i < this.GetItemCount(); i++) {
                if (this.GetModelObject(i) == modelObject)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Update the ListViewItem with the data from its associated model.
        /// </summary>
        /// <remarks>This method does not resort or regroup the view. It simply updates
        /// the displayed data of the given item</remarks>
        public virtual void RefreshItem(OLVListItem olvi) {
            olvi.SubItems.Clear();
            this.FillInValues(olvi, olvi.RowObject);
            this.SetSubItemImages(olvi.Index, olvi, true);

            // Calculate the background color
            if (this.UseAlternatingBackColors) {
                int rowIndex = this.GetItemIndexInDisplayOrder(olvi);
                Color newBackColor = this.BackColor;
                if (rowIndex % 2 != 0)
                    newBackColor = this.AlternateRowBackColorOrDefault;
                if (olvi.BackColor != newBackColor) {
                    olvi.BackColor = newBackColor;
                    this.CorrectSubItemColors(olvi);
                }
            }
        }

        /// <summary>
        /// Update the rows that are showing the given objects
        /// </summary>
        /// <remarks>This method does not resort or regroup the view.</remarks>
        public virtual void RefreshObject(object modelObject) {
            this.RefreshObjects(new object[] { modelObject });
        }

        /// <summary>
        /// Update the rows that are showing the given objects
        /// </summary>
        /// <remarks>
        /// <para>This method does not resort or regroup the view.</para>
        /// <para>This method can safely be called from background threads.</para>
        /// </remarks>
        public virtual void RefreshObjects(IList modelObjects) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate { this.RefreshObjects(modelObjects); });
                return;
            }
            foreach (object modelObject in modelObjects) {
                OLVListItem olvi = this.ModelToItem(modelObject);
                if (olvi != null)
                    this.RefreshItem(olvi);
            }
        }

        /// <summary>
        /// Update the rows that are selected
        /// </summary>
        /// <remarks>This method does not resort or regroup the view.</remarks>
        public virtual void RefreshSelectedObjects() {
            foreach (ListViewItem lvi in this.SelectedItems)
                this.RefreshItem((OLVListItem)lvi);
        }

        /// <summary>
        /// Scroll the listview so that the given group is at the top.
        /// </summary>
        /// <param name="lvg">The group to be revealed</param>
        /// <remarks><para>
        /// If the group is already visible, the list will still be scrolled to move
        /// the group to the top, if that is possible.
        /// </para>
        /// <para>This only works when the list is showing groups (obviously)</para>
        /// </remarks>
        public virtual void EnsureGroupVisible(ListViewGroup lvg) {
            if (!this.ShowGroups || lvg == null)
                return;

            int groupIndex = this.Groups.IndexOf(lvg);
            if (groupIndex <= 0) {
                // There is no easy way to scroll back to the beginning of the list
                int delta = 0 - NativeMethods.GetScrollPosition(this, false);
                NativeMethods.Scroll(this, 0, delta);
            } else {
                // Find the display rectangle of the last item in the previous group
                ListViewGroup previousGroup = this.Groups[groupIndex - 1];
                ListViewItem lastItemInGroup = previousGroup.Items[previousGroup.Items.Count - 1];
                Rectangle r = this.GetItemRect(lastItemInGroup.Index);

                // Scroll so that the last item of the previous group is just out of sight,
                // which will make the desired group header visible.
                int delta = r.Y + r.Height / 2;
                NativeMethods.Scroll(this, 0, delta);
            }
        }

        /// <summary>
        /// Select the row that is displaying the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="modelObject">The object to be selected or null to deselect all</param>
        public virtual void SelectObject(object modelObject) {
            this.SelectObject(modelObject, false);
        }

        /// <summary>
        /// Select the row that is displaying the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="modelObject">The object to be selected or null to deselect all</param>
        /// <param name="setFocus">Should the object be focused as well?</param>
        public virtual void SelectObject(object modelObject, bool setFocus) {
            // If the given model is already selected, don't do anything else (prevents an flicker)
            if (this.SelectedItems.Count == 1 && ((OLVListItem)this.SelectedItems[0]).RowObject.Equals(modelObject))
                return;

            this.SelectedItems.Clear();

            OLVListItem olvi = this.ModelToItem(modelObject);
            if (olvi != null) {
                olvi.Selected = true;
                if (setFocus)
                    olvi.Focused = true;
            }
        }

        /// <summary>
        /// Select the rows that is displaying any of the given model object. All other rows are deselected.
        /// </summary>
        /// <param name="modelObjects">A collection of model objects</param>
        public virtual void SelectObjects(IList modelObjects) {
            this.SelectedItems.Clear();

            if (modelObjects == null)
                return;

            foreach (object modelObject in modelObjects) {
                OLVListItem olvi = this.ModelToItem(modelObject);
                if (olvi != null)
                    olvi.Selected = true;
            }
        }

        #endregion

        #region Freezing

        /// <summary>
        /// Freeze the listview so that it no longer updates itself.
        /// </summary>
        /// <remarks>Freeze()/Unfreeze() calls nest correctly</remarks>
        public virtual void Freeze() {
            freezeCount++;
        }

        /// <summary>
        /// Unfreeze the listview. If this call is the outermost Unfreeze(),
        /// the contents of the listview will be rebuilt.
        /// </summary>
        /// <remarks>Freeze()/Unfreeze() calls nest correctly</remarks>
        public virtual void Unfreeze() {
            if (freezeCount <= 0)
                return;

            freezeCount--;
            if (freezeCount == 0)
                DoUnfreeze();
        }

        /// <summary>
        /// Do the actual work required when the listview is unfrozen
        /// </summary>
        protected virtual void DoUnfreeze() {
            this.ResizeFreeSpaceFillingColumns();
            this.BuildList();
        }

        #endregion

        #region Column Sorting

        /// <summary>
        /// Sort the items by the last sort column and order
        /// </summary>
        new public void Sort() {
            this.Sort(this.LastSortColumn, this.LastSortOrder);
        }

        /// <summary>
        /// Sort the items in the list view by the values in the given column and the last sort order
        /// </summary>
        /// <param name="columnToSortName">The name of the column whose values will be used for the sorting</param>
        public virtual void Sort(string columnToSortName) {
            this.Sort(this.GetColumn(columnToSortName), this.LastSortOrder);
        }

        /// <summary>
        /// Sort the items in the list view by the values in the given column and the last sort order
        /// </summary>
        /// <param name="columnToSortIndex">The index of the column whose values will be used for the sorting</param>
        public virtual void Sort(int columnToSortIndex) {
            if (columnToSortIndex >= 0 && columnToSortIndex < this.Columns.Count)
                this.Sort(this.GetColumn(columnToSortIndex), this.LastSortOrder);
        }

        /// <summary>
        /// Sort the items in the list view by the values in the given column and the last sort order
        /// </summary>
        /// <param name="columnToSort">The column whose values will be used for the sorting</param>
        public virtual void Sort(OLVColumn columnToSort) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate { this.Sort(columnToSort); });
            } else {
                this.Sort(columnToSort, this.LastSortOrder);
            }
        }

        /// <summary>
        /// Sort the items in the list view by the values in the given column and by the given order.
        /// </summary>
        /// <param name="columnToSort">The column whose values will be used for the sorting.
        /// If null, the first column will be used.</param>
        /// <param name="order">The ordering to be used for sorting. If this is None,
        /// this.Sorting and then SortOrder.Ascending will be used</param>
        /// <remarks>If ShowGroups is true, the rows will be grouped by the given column.
        /// If AlwaysGroupsByColumn is not null, the rows will be grouped by that column,
        /// and the rows within each group will be sorted by the given column.</remarks>
        public virtual void Sort(OLVColumn columnToSort, SortOrder order) {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate { this.Sort(columnToSort, order); });
                return;
            }

            // Sanity checks
            if (this.GetItemCount() == 0 || this.Columns.Count == 0)
                return;

            // Fill in default values, if the parameters don't make sense
            if (this.ShowGroups) {
                columnToSort = columnToSort ?? this.GetColumn(0);
                if (order == SortOrder.None) {
                    order = this.Sorting;
                    if (order == SortOrder.None)
                        order = SortOrder.Ascending;
                }
            }

            // Give the world a chance to fiddle with or completely avoid the sorting process
            BeforeSortingEventArgs args = this.BuildBeforeSortingEventArgs(columnToSort, order);
            this.OnBeforeSorting(args);
            if (args.Canceled)
                return;

            // Sanity checks
            if (args.ColumnToSort == null || args.SortOrder == SortOrder.None)
                return;

            // Virtual lists don't preserve selection, so we have to do it specifically
            // THINK: Do we need to preserve focus too?
            IList selection = new ArrayList();
            if (this.VirtualMode)
                selection = this.SelectedObjects;

            this.ClearHotItem();

            // Finally, do the work of sorting, unless an event handler has already done the sorting for us
            if (!args.Handled) {
                if (this.ShowGroups)
                    this.BuildGroups(args.ColumnToGroupBy, args.GroupByOrder, args.ColumnToSort, args.SortOrder,
                        args.SecondaryColumnToSort, args.SecondarySortOrder);
                else if (this.CustomSorter != null)
                    this.CustomSorter(columnToSort, order);
                else
                    this.ListViewItemSorter = new ColumnComparer(args.ColumnToSort, args.SortOrder,
                        args.SecondaryColumnToSort, args.SecondarySortOrder);
            }

            if (this.ShowSortIndicators)
                this.ShowSortIndicator(args.ColumnToSort, args.SortOrder);

            if (this.UseAlternatingBackColors && this.View == View.Details)
                this.PrepareAlternateBackColors();

            this.LastSortColumn = args.ColumnToSort;
            this.LastSortOrder = args.SortOrder;

            if (selection.Count > 0)
                this.SelectedObjects = selection;

            this.RefreshHotItem();

            this.OnAfterSorting(new AfterSortingEventArgs(args));
        }

        /// <summary>
        /// When grouping items, there are some constraints that must always be observed.
        /// In particular, if AlwaysGroupByColumn property is set, it must be honoured.
        /// </summary>
        /// <param name="columnToSort"></param>
        /// <param name="order"></param>
        private void RationalizeColumnForGrouping(ref OLVColumn columnToSort, ref SortOrder order,
            ref OLVColumn secondaryColumn, ref SortOrder secondaryOrder) {
            if (this.AlwaysGroupByColumn != null) {
                secondaryColumn = columnToSort;
                secondaryOrder = order;
                columnToSort = this.AlwaysGroupByColumn;
            }
            if (this.AlwaysGroupBySortOrder != SortOrder.None) {
                order = this.AlwaysGroupBySortOrder;
            }

            // Groups have to have a sorting column
            if (columnToSort == null && this.Columns.Count > 0)
                columnToSort = this.GetColumn(0);
            if (order == SortOrder.None)
                order = SortOrder.Ascending;
        }

        /// <summary>
        /// Put a sort indicator next to the text of the sort column
        /// </summary>
        public virtual void ShowSortIndicator() {
            if (this.ShowSortIndicators && this.LastSortOrder != SortOrder.None)
                this.ShowSortIndicator(this.LastSortColumn, this.LastSortOrder);
        }

        /// <summary>
        /// Put a sort indicator next to the text of the given given column
        /// </summary>
        /// <param name="columnToSort">The column to be marked</param>
        /// <param name="sortOrder">The sort order in effect on that column</param>
        protected virtual void ShowSortIndicator(OLVColumn columnToSort, SortOrder sortOrder) {
            int imageIndex = -1;

            if (!NativeMethods.HasBuiltinSortIndicators()) {
                // If we can't use builtin image, we have to make and then locate the index of the
                // sort indicator we want to use. SortOrder.None doesn't show an image.
                if (this.SmallImageList == null || !this.SmallImageList.Images.ContainsKey(SORT_INDICATOR_UP_KEY))
                    MakeSortIndicatorImages();

                if (sortOrder == SortOrder.Ascending)
                    imageIndex = this.SmallImageList.Images.IndexOfKey(SORT_INDICATOR_UP_KEY);
                else if (sortOrder == SortOrder.Descending)
                    imageIndex = this.SmallImageList.Images.IndexOfKey(SORT_INDICATOR_DOWN_KEY);
            }

            // Set the image for each column
            for (int i = 0; i < this.Columns.Count; i++) {
                if (i == columnToSort.Index)
                    NativeMethods.SetColumnImage(this, i, sortOrder, imageIndex);
                else
                    NativeMethods.SetColumnImage(this, i, SortOrder.None, -1);
            }
        }

        /// <summary>
        /// The name of the image used when a column is sorted ascending
        /// </summary>
        /// <remarks>This image is only used on pre-XP systems. System images are used for XP and later</remarks>
        public const string SORT_INDICATOR_UP_KEY = "sort-indicator-up";

        /// <summary>
        /// The name of the image used when a column is sorted descending
        /// </summary>
        /// <remarks>This image is only used on pre-XP systems. System images are used for XP and later</remarks>
        public const string SORT_INDICATOR_DOWN_KEY = "sort-indicator-down";

        /// <summary>
        /// If the sort indicator images don't already exist, this method will make and install them
        /// </summary>
        protected virtual void MakeSortIndicatorImages() {
            // Don't mess with the image list in design mode
            if (this.DesignMode)
                return;

            ImageList il = this.SmallImageList;
            if (il == null) {
                il = new ImageList();
                il.ImageSize = new Size(16, 16);
            }

            // This arrangement of points works well with (16,16) images, and OK with others
            int midX = il.ImageSize.Width / 2;
            int midY = (il.ImageSize.Height / 2) - 1;
            int deltaX = midX - 2;
            int deltaY = deltaX / 2;

            if (il.Images.IndexOfKey(SORT_INDICATOR_UP_KEY) == -1) {
                Point pt1 = new Point(midX - deltaX, midY + deltaY);
                Point pt2 = new Point(midX, midY - deltaY - 1);
                Point pt3 = new Point(midX + deltaX, midY + deltaY);
                il.Images.Add(SORT_INDICATOR_UP_KEY, this.MakeTriangleBitmap(il.ImageSize, new Point[] { pt1, pt2, pt3 }));
            }

            if (il.Images.IndexOfKey(SORT_INDICATOR_DOWN_KEY) == -1) {
                Point pt1 = new Point(midX - deltaX, midY - deltaY);
                Point pt2 = new Point(midX, midY + deltaY);
                Point pt3 = new Point(midX + deltaX, midY - deltaY);
                il.Images.Add(SORT_INDICATOR_DOWN_KEY, this.MakeTriangleBitmap(il.ImageSize, new Point[] { pt1, pt2, pt3 }));
            }

            this.SmallImageList = il;
        }

        private Bitmap MakeTriangleBitmap(Size sz, Point[] pts) {
            Bitmap bm = new Bitmap(sz.Width, sz.Height);
            Graphics g = Graphics.FromImage(bm);
            g.FillPolygon(new SolidBrush(Color.Gray), pts);
            return bm;
        }


        #endregion

        #region Utilities

        /// <summary>
        /// For some reason, UseItemStyleForSubItems doesn't work for the colors
        /// when owner drawing the list, so we have to specifically give each subitem
        /// the desired colors
        /// </summary>
        /// <param name="olvi">The item whose subitems are to be corrected</param>
        /// <remarks>Cells drawn via BaseRenderer don't need this, but it is needed
        /// when an owner drawn cell uses DrawDefault=true</remarks>
        protected virtual void CorrectSubItemColors(ListViewItem olvi) {
            //if (this.OwnerDraw && olvi.UseItemStyleForSubItems)
            //    foreach (ListViewItem.ListViewSubItem si in olvi.SubItems) {
            //        si.BackColor = olvi.BackColor;
            //        si.ForeColor = olvi.ForeColor;
            //        si.Font = olvi.Font;
            //    }
        }

        /// <summary>
        /// Fill in the given OLVListItem with values of the given row
        /// </summary>
        /// <param name="lvi">the OLVListItem that is to be stuff with values</param>
        /// <param name="rowObject">the model object from which values will be taken</param>
        protected virtual void FillInValues(OLVListItem lvi, object rowObject) {
            if (this.Columns.Count == 0)
                return;

            OLVColumn column = this.GetColumn(0);
            lvi.Text = column.GetStringValue(rowObject);
            lvi.ImageSelector = column.GetImage(rowObject);

            for (int i = 1; i < this.Columns.Count; i++) {
                column = this.GetColumn(i);
                lvi.SubItems.Add(new OLVListSubItem(column.GetStringValue(rowObject),
                                                    column.GetImage(rowObject)));
            }

            // Give the item the same font/colors as the control
            lvi.Font = this.Font;
            lvi.BackColor = this.BackColor;
            lvi.ForeColor = this.ForeColor;

            // Give the row formatter a chance to mess with the item
            if (this.RowFormatter != null)
                this.RowFormatter(lvi);

            this.CorrectSubItemColors(lvi);

            // Set the check state of the row, if we are showing check boxes
            if (this.CheckBoxes) {
                CheckState? state = this.GetCheckState(lvi.RowObject);
                if (state.HasValue)
                    lvi.CheckState = (CheckState)state;
            }
        }

        /// <summary>
        /// Make sure the ListView has the extended style that says to display subitem images.
        /// </summary>
        /// <remarks>This method must be called after any .NET call that update the extended styles
        /// since they seem to erase this setting.</remarks>
        protected virtual void ForceSubItemImagesExStyle() {
            NativeMethods.ForceSubItemImagesExStyle(this);
        }

        /// <summary>
        /// Convert the given image selector to an index into our image list.
        /// Return -1 if that's not possible
        /// </summary>
        /// <param name="imageSelector"></param>
        /// <returns>Index of the image in the imageList, or -1</returns>
        protected virtual int GetActualImageIndex(Object imageSelector) {
            if (imageSelector == null)
                return -1;

            if (imageSelector is Int32)
                return (int)imageSelector;

            String imageSelectorAsString = imageSelector as String;
            if (imageSelectorAsString != null && this.SmallImageList != null)
                return this.SmallImageList.Images.IndexOfKey(imageSelectorAsString);

            return -1;
        }

        /// <summary>
        /// Return the tooltip that should be shown when the mouse is hovered over the given column
        /// </summary>
        /// <param name="columnIndex">The column index whose tool tip is to be fetched</param>
        /// <returns>A string or null if no tool tip is to be shown</returns>
        public virtual String GetHeaderToolTip(int columnIndex) {
            OLVColumn column = this.GetColumn(columnIndex);
            if (column == null)
                return null;
            String tooltip = column.ToolTipText;
            if (this.HeaderToolTipGetter != null)
                tooltip = this.HeaderToolTipGetter(column);
            return tooltip;
        }

        /// <summary>
        /// Return the tooltip that should be shown when the mouse is hovered over the given cell
        /// </summary>
        /// <param name="columnIndex">The column index whose tool tip is to be fetched</param>
        /// <returns>A string or null if no tool tip is to be shown</returns>
        public virtual String GetCellToolTip(int columnIndex, int rowIndex) {
            if (this.CellToolTipGetter == null)
                return null;
            else
                return this.CellToolTipGetter(this.GetColumn(columnIndex), this.GetModelObject(rowIndex));
        }

        /// <summary>
        /// Return the OLVListItem that displays the given model object
        /// </summary>
        /// <param name="modelObject">The modelObject whose item is to be found</param>
        /// <returns>The OLVListItem that displays the model, or null</returns>
        /// <remarks>This method has O(n) performance.</remarks>
        public virtual OLVListItem ModelToItem(object modelObject) {
            if (modelObject == null)
                return null;

            foreach (OLVListItem olvi in this.Items) {
                if (olvi.RowObject != null && olvi.RowObject.Equals(modelObject))
                    return olvi;
            }
            return null;
        }

        /// <summary>
        /// Prepare the listview to show alternate row backcolors
        /// </summary>
        /// <remarks>We cannot rely on lvi.Index in this method.
        /// In a straight list, lvi.Index is the display index, and can be used to determine
        /// whether the row should be colored. But when organised by groups, lvi.Index is not
        /// useable because it still refers to the position in the overall list, not the display order.
        ///</remarks>
        protected virtual void PrepareAlternateBackColors() {
            // If this method is called during a BeginUpdate/EndUpdate pair, changes to the
            // Items collection are cached. Getting the Count flushes that cache.
            int count = this.Items.Count;

            Color rowBackColor = this.AlternateRowBackColorOrDefault;
            int i = 0;
            if (this.ShowGroups) {
                foreach (ListViewGroup group in this.Groups) {
                    foreach (ListViewItem lvi in group.Items) {
                        if (i % 2 == 0)
                            lvi.BackColor = this.BackColor;
                        else
                            lvi.BackColor = rowBackColor;
                        CorrectSubItemColors(lvi);
                        i++;
                    }
                }
            } else {
                foreach (ListViewItem lvi in this.Items) {
                    if (i % 2 == 0)
                        lvi.BackColor = this.BackColor;
                    else
                        lvi.BackColor = rowBackColor;
                    CorrectSubItemColors(lvi);
                    i++;
                }
            }
        }

        /// <summary>
        /// Setup all subitem images on all rows
        /// </summary>
        protected virtual void SetAllSubItemImages() {
            if (!this.ShowImagesOnSubItems || this.OwnerDraw)
                return;

            this.ForceSubItemImagesExStyle();

            for (int rowIndex = 0; rowIndex < this.GetItemCount(); rowIndex++)
                SetSubItemImages(rowIndex, this.GetItem(rowIndex));
        }

        /// <summary>
        /// Tell the underlying list control which images to show against the subitems
        /// </summary>
        /// <param name="rowIndex">the index at which the item occurs</param>
        /// <param name="item">the item whose subitems are to be set</param>
        protected virtual void SetSubItemImages(int rowIndex, OLVListItem item) {
            this.SetSubItemImages(rowIndex, item, false);
        }

        /// <summary>
        /// Tell the underlying list control which images to show against the subitems
        /// </summary>
        /// <param name="rowIndex">the index at which the item occurs</param>
        /// <param name="item">the item whose subitems are to be set</param>
        /// <param name="shouldClearImages">will existing images be cleared if no new image is provided?</param>
        protected virtual void SetSubItemImages(int rowIndex, OLVListItem item, bool shouldClearImages) {
            if (!this.ShowImagesOnSubItems || this.OwnerDraw)
                return;

            for (int i = 1; i < item.SubItems.Count; i++) {
                this.SetSubItemImage(rowIndex, i, (OLVListSubItem)item.SubItems[i], shouldClearImages);
            }
        }

        public virtual void SetSubItemImage(int rowIndex, int subItemIndex, OLVListSubItem subItem, bool shouldClearImages) {
            int imageIndex = this.GetActualImageIndex(subItem.ImageSelector);
            if (shouldClearImages || imageIndex != -1)
                NativeMethods.SetSubItemImage(this, rowIndex, subItemIndex, imageIndex);
        }

        /// <summary>
        /// Take ownership of the 'objects' collection. This separats our collection from the source.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method
        /// separates the 'objects' instance variable from its source, so that any AddObject/RemoveObject
        /// calls will modify our collection and not the original colleciton.
        /// </para>
        /// <para>
        /// This method has the intentional side-effect of converting our list of objects to an ArrayList.
        /// </para>
        /// </remarks>
        protected virtual void TakeOwnershipOfObjects() {
            if (this.isOwnerOfObjects)
                return;

            this.isOwnerOfObjects = true;

            if (this.objects == null)
                this.objects = new ArrayList();
            else if (this.objects is ICollection)
                this.objects = new ArrayList((ICollection)this.objects);
            else {
                ArrayList newObjects = new ArrayList();
                foreach (object x in this.objects)
                    newObjects.Add(x);
                this.objects = newObjects;
            }
        }

        #endregion

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit() {
            this.Frozen = true;
        }

        void ISupportInitialize.EndInit() {
            if (this.RowHeight != -1) {
                this.SmallImageList = this.SmallImageList;
                if (this.CheckBoxes)
                    this.InitializeStateImageList();
            }

            if (this.UseCustomSelectionColors)
                this.EnableCustomSelectionColors();

            if (this.UseSubItemCheckBoxes || (this.VirtualMode && this.CheckBoxes))
                this.SetupSubItemCheckBoxes();

            this.Frozen = false;
        }

        #endregion

        #region Image list manipulation

        /// <summary>
        /// Update our externally visible image list so it holds the same images as our shadow list, but sized correctly
        /// </summary>
        private void SetupBaseImageList() {
            // If a row height hasn't been set, or an image list has been give which is the required size, just assign it
            if (rowHeight == -1 || 
                this.View != View.Details ||
                (this.shadowedImageList != null && this.shadowedImageList.ImageSize.Height == rowHeight))
                this.BaseSmallImageList = this.shadowedImageList;
            else {
                int width = (this.shadowedImageList == null ? 16 : this.shadowedImageList.ImageSize.Width);
                this.BaseSmallImageList = this.MakeResizedImageList(width, rowHeight, shadowedImageList);
            }
        }

        /// <summary>
        /// Return a copy of the given source image list, where each image has been resized to be height x height in size.
        /// If source is null, an empty image list of the given size is returned
        /// </summary>
        /// <param name="height">Height and width of the new images</param>
        /// <param name="source">Source of the images (can be null)</param>
        /// <returns>A new image list</returns>
        private ImageList MakeResizedImageList(int width, int height, ImageList source) {
            ImageList il = new ImageList();
            il.ImageSize = new Size(width, height);

            // If there's nothing to copy, just return the new list
            if (source == null)
                return il;

            il.TransparentColor = source.TransparentColor;
            il.ColorDepth = source.ColorDepth;

            // Fill the imagelist with resized copies from the source
            for (int i = 0; i < source.Images.Count; i++) {
                Bitmap bm = this.MakeResizedImage(width, height, source.Images[i], source.TransparentColor);
                il.Images.Add(bm);
            }

            // Give each image the same key it has in the original
            foreach (String key in source.Images.Keys) {
                il.Images.SetKeyName(source.Images.IndexOfKey(key), key);
            }

            return il;
        }

        /// <summary>
        /// Return a bitmap of the given height x height, which shows the given image, centred.
        /// </summary>
        /// <param name="height">Height and width of new bitmap</param>
        /// <param name="image">Image to be centred</param>
        /// <param name="transparent">The background color</param>
        /// <returns>A new bitmap</returns>
        private Bitmap MakeResizedImage(int width, int height, Image image, Color transparent) {
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(transparent);
            int x = Math.Max(0, (bm.Size.Width - image.Size.Width) / 2);
            int y = Math.Max(0, (bm.Size.Height - image.Size.Height) / 2);
            g.DrawImage(image, x, y, image.Size.Width, image.Size.Height);
            return bm;
        }

        /// <summary>
        /// Initialize the state image list with the required checkbox images
        /// </summary>
        protected virtual void InitializeStateImageList() {
            if (this.DesignMode)
                return;

            if (this.StateImageList == null) {
                this.StateImageList = new ImageList();
                this.StateImageList.ImageSize = new Size(16, 16);
            }

            if (this.RowHeight != -1 && 
                this.View == View.Details &&
                this.StateImageList.ImageSize.Height != this.RowHeight) {
                this.StateImageList = new ImageList();
                this.StateImageList.ImageSize = new Size(16, this.RowHeight);
            }

            if (this.StateImageList.Images.Count == 0)
                this.AddCheckStateBitmap(this.StateImageList, UNCHECKED_KEY, CheckBoxState.UncheckedNormal);
            if (this.StateImageList.Images.Count <= 1)
                this.AddCheckStateBitmap(this.StateImageList, CHECKED_KEY, CheckBoxState.CheckedNormal);
            if (this.TriStateCheckBoxes && this.StateImageList.Images.Count <= 2)
                this.AddCheckStateBitmap(this.StateImageList, INDETERMINATE_KEY, CheckBoxState.MixedNormal);
            else {
                if (this.StateImageList.Images.ContainsKey(INDETERMINATE_KEY))
                    this.StateImageList.Images.RemoveByKey(INDETERMINATE_KEY);
            }
        }

        /// <summary>
        /// The name of the image used when a check box is checked
        /// </summary>
        public const string CHECKED_KEY = "checkbox-checked";

        /// <summary>
        /// The name of the image used when a check box is unchecked
        /// </summary>
        public const string UNCHECKED_KEY = "checkbox-unchecked";

        /// <summary>
        /// The name of the image used when a check box is Indeterminate
        /// </summary>
        public const string INDETERMINATE_KEY = "checkbox-indeterminate";

        /// <summary>
        /// Setup this control so it can display check boxes on subitems
        /// (or primary checkboxes in virtual mode)
        /// </summary>
        /// <remarks>This gives the ListView a small image list, if it doesn't already have one.</remarks>
        public virtual void SetupSubItemCheckBoxes() {
            this.ShowImagesOnSubItems = true;
            if (this.SmallImageList == null || !this.SmallImageList.Images.ContainsKey(CHECKED_KEY))
                this.InitializeCheckBoxImages();
        }

        /// <summary>
        /// Make sure the small image list for this control has checkbox images
        /// </summary>
        /// <remarks>This gives the ListView a small image list, if it doesn't already have one.</remarks>
        protected virtual void InitializeCheckBoxImages() {
            // Don't mess with the image list in design mode
            if (this.DesignMode)
                return;

            ImageList il = this.SmallImageList;
            if (il == null) {
                il = new ImageList();
                il.ImageSize = new Size(16, 16);
            }

            this.AddCheckStateBitmap(il, CHECKED_KEY, CheckBoxState.CheckedNormal);
            this.AddCheckStateBitmap(il, UNCHECKED_KEY, CheckBoxState.UncheckedNormal);
            this.AddCheckStateBitmap(il, INDETERMINATE_KEY, CheckBoxState.MixedNormal);

            this.SmallImageList = il;
        }

        private void AddCheckStateBitmap(ImageList il, string key, CheckBoxState boxState) {
            Bitmap b = new Bitmap(il.ImageSize.Width, il.ImageSize.Height);
            Graphics g = Graphics.FromImage(b);
            g.Clear(il.TransparentColor);
            Point location = new Point(b.Width / 2 - 5, b.Height / 2 - 6);
            CheckBoxRenderer.DrawCheckBox(g, location, boxState);
            il.Images.Add(key, b);
        }

        #endregion

        #region Owner drawing

        /// <summary>
        /// Owner draw the column header
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e) {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        /// <summary>
        /// Owner draw the item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawListViewItemEventArgs e) {
            if (this.View == View.Details)
                e.DrawDefault = false;
            else {
                if (this.ItemRenderer == null)
                    e.DrawDefault = true;
                else {
                    Object row = ((OLVListItem)e.Item).RowObject;
                    e.DrawDefault = !this.ItemRenderer.RenderItem(e, e.Graphics, e.Bounds, row);
                }
            }

            if (e.DrawDefault)
                base.OnDrawItem(e);
        }

        /// <summary>
        /// Owner draw a single subitem
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e) {
            //System.Diagnostics.Debug.WriteLine(String.Format("OnDrawSubItem ({0}, {1})", e.ItemIndex, e.ColumnIndex));
            // Don't try to do owner drawing at design time
            if (this.DesignMode) {
                e.DrawDefault = true;
                return;
            }

            // Calculate where the subitem should be drawn
            Rectangle r = e.Bounds;

            // Optimize drawing by only redrawing subitems that touch the area that was damaged
            if (!r.IntersectsWith(this.lastUpdateRectangle)) 
                return;

            // Get the special renderer for this column. If there isn't one, use the default draw mechanism.
            OLVColumn column = this.GetColumn(e.ColumnIndex);
            IRenderer renderer = column.Renderer ?? this.DefaultRenderer;

            // Get a graphics context for the renderer to use.
            // But we have more complications. Virtual lists have a nasty habit of drawing column 0
            // whenever there is any mouse move events over a row, and doing it in an un-double-buffered manner,
            // which results in nasty flickers! There are also some unbuffered draw when a mouse is first
            // hovered over column 0 of a normal row. So, to avoid all complications,
            // we always manually double-buffer the drawing.
            // Except with Mono, which doesn't seem to handle double buffering at all :-(
            Graphics g = e.Graphics;
            BufferedGraphics buffer = null;
            bool avoidFlickerMode = true; // set this to false to see the problems with flicker
            if (avoidFlickerMode) {
                buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, r);
                g = buffer.Graphics;
            }

            // Finally, give the renderer a chance to draw something
            e.DrawDefault = !renderer.RenderSubItem(e, g, r, ((OLVListItem)e.Item).RowObject);

            if (buffer != null) {
                if (!e.DrawDefault)
                    buffer.Render();
                buffer.Dispose();
            }
        }

        #endregion

        #region OnEvent Handling

        /// <summary>
        /// We need the click count in the mouse up event, but that is always 1.
        /// So we have to remember the click count from the preceding mouse down event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            this.lastMouseDownClickCount = e.Clicks;
        }
        private int lastMouseDownClickCount = 0;

        /// <summary>
        /// When the mouse leaves the control, remove any hot item highlighting
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            this.UpdateHotItem(new Point(-1, -1));
        }

        // We could change the hot item on the mouse hover event, but it looks wrong.

        //protected override void OnMouseHover(EventArgs e)
        //{
        //    base.OnMouseHover(e);
        //    this.UpdateHotItem(this.PointToClient(Cursor.Position));
        //}

        /// <summary>
        /// When the mouse moves, we might need to change the hot item.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            this.UpdateHotItem(e.Location);

            CellOverEventArgs args = new CellOverEventArgs();
            this.BuildCellEvent(args, e.Location);
            this.OnCellOver(args);
        }

        /// <summary>
        /// Check to see if we need to start editing a cell
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Right) {
                this.OnRightMouseUp(e);
                return;
            }

            // What event should we listen for to start cell editing?
            // ------------------------------------------------------
            //
            // We can't use OnMouseClick, OnMouseDoubleClick, OnClick, or OnDoubleClick
            // since they are not triggered for clicks on subitems without Full Row Select.
            //
            // We could use OnMouseDown, but selecting rows is done in OnMouseUp. This means
            // that if we start the editing during OnMouseDown, the editor will automatically
            // lose focus when mouse up happens.
            //

            // Tell the world about a cell click. If someone handles it, don't do anything else
            CellClickEventArgs args = new CellClickEventArgs();
            this.BuildCellEvent(args, e.Location);
            args.ClickCount = this.lastMouseDownClickCount;
            this.OnCellClick(args);
            if (args.Handled)
                return;

            // No one handled it so check to see if we should start editing.
            // We only start the edit if the user clicked on something (not a dead zone).
            if (this.ShouldStartCellEdit(e) &&
                args.HitTest.HitTestLocation != HitTestLocation.Nothing) {
                // In non-details views, ColumnIndex will be -1
                int columnIndex = Math.Max(0, args.ColumnIndex);

                // We don't edit the primary column by single clicks -- only subitems.
                if (this.CellEditActivation != CellEditActivateMode.SingleClick || columnIndex > 0)
                    this.EditSubItem(args.Item, columnIndex);
            }
        }
        
        /// <summary>
        /// The user right clicked on the control
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRightMouseUp(MouseEventArgs e) {
            CellRightClickEventArgs args = new CellRightClickEventArgs();
            this.BuildCellEvent(args, e.Location);
            this.OnCellRightClick(args);
            if (!args.Handled) {
                if (args.MenuStrip != null) {
                    args.MenuStrip.Show(this, args.Location);
                }
            }
        }

        private void BuildCellEvent(CellEventArgs args, Point location) {
            OlvListViewHitTestInfo hitTest = this.OlvHitTest(location.X, location.Y);
            args.HitTest = hitTest;
            args.ListView = this;
            args.Location = location;
            args.Item = hitTest.Item;
            args.SubItem = hitTest.SubItem;
            args.Model = hitTest.RowObject;
            args.ColumnIndex = hitTest.ColumnIndex;
            args.Column = hitTest.Column;
            if (hitTest.Item != null)
                args.RowIndex = hitTest.Item.Index;
            args.ModifierKeys = Control.ModifierKeys;
        }

        /// <summary>
        /// This method is called every time a row is selected or deselected. This can be
        /// a pain if the user shift-clicks 100 rows. We override this method so we can
        /// trigger one event for any number of select/deselects that come from one user action
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedIndexChanged(EventArgs e) {
            base.OnSelectedIndexChanged(e);

            // If we haven't already scheduled an event, schedule it to be triggered
            // By using idle time, we will wait until all select events for the same
            // user action have finished before triggering the event.
            if (!this.hasIdleHandler) {
                this.hasIdleHandler = true;
                Application.Idle += new EventHandler(HandleApplicationIdle);
            }
        }

        #endregion

        #region Cell editing

        /// <summary>
        /// Should we start editing the cell in response to the given mouse button event?
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual bool ShouldStartCellEdit(MouseEventArgs e) {
            if (this.IsCellEditing)
                return false;

            if (e.Button != MouseButtons.Left)
                return false;

            if ((Control.ModifierKeys & (Keys.Shift | Keys.Control | Keys.Alt)) != 0)
                return false;

            if (this.lastMouseDownClickCount == 1 && this.CellEditActivation == CellEditActivateMode.SingleClick)
                return true;

            return (this.lastMouseDownClickCount == 2 && this.CellEditActivation == CellEditActivateMode.DoubleClick);
        }

        /// <summary>
        /// Handle a key press on this control. We specifically look for F2 which edits the primary column,
        /// or a Tab character during an edit operation, which tries to start editing on the next (or previous) cell.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData) {
            bool isSimpleTabKey = ((keyData & Keys.KeyCode) == Keys.Tab) && ((keyData & (Keys.Alt | Keys.Control)) == Keys.None);

            if (isSimpleTabKey && this.IsCellEditing) { // Tab key while editing
                // If the cell editing was cancelled, don't handle the tab
                if (!this.PossibleFinishCellEditing())
                    return true;

                // We can only Tab between columns when we are in Details view
                if (this.View != View.Details)
                    return true;

                OLVListItem olvi = this.cellEditEventArgs.ListViewItem;
                int subItemIndex = this.cellEditEventArgs.SubItemIndex;
                int displayIndex = this.GetColumn(subItemIndex).DisplayIndex;

                // Move to the next or previous editable subitem, depending on Shift key. Wrap at the edges
                List<OLVColumn> columnsInDisplayOrder = this.ColumnsInDisplayOrder;
                do {
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        displayIndex = (olvi.SubItems.Count + displayIndex - 1) % olvi.SubItems.Count;
                    else
                        displayIndex = (displayIndex + 1) % olvi.SubItems.Count;
                } while (!columnsInDisplayOrder[displayIndex].IsEditable);

                // If we found a different editable cell, start editing it
                subItemIndex = columnsInDisplayOrder[displayIndex].Index;
                if (this.cellEditEventArgs.SubItemIndex != subItemIndex) {
                    this.StartCellEdit(olvi, subItemIndex);
                    return true;
                }
            }

            // Treat F2 as a request to edit the primary column
            if (keyData == Keys.F2 && !this.IsCellEditing) {
                this.EditSubItem((OLVListItem)this.FocusedItem, 0);
                return true;
            }

            // We have to catch Return/Enter/Escape here since some types of controls
            // (e.g. ComboBox, UserControl) don't trigger key events that we can listen for.
            // Treat Return or Enter as committing the current edit operation
            if ((keyData == Keys.Return || keyData == Keys.Enter) && this.IsCellEditing) {
                this.PossibleFinishCellEditing();
                return true;
            }

            // Treat Escaoe as cancel the current edit operation
            if (keyData == Keys.Escape && this.IsCellEditing) {
                this.CancelCellEdit();
                return true;
            }

            // Treat Ctrl-C as Copy To Clipboard. We still allow the default processing
            if ((keyData & Keys.Control) == Keys.Control && (keyData & Keys.KeyCode) == Keys.C)
                this.CopySelectionToClipboard();

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Begin an edit operation on the given cell.
        /// </summary>
        /// <remarks>This performs various sanity checks and passes off the real work to StartCellEdit().</remarks>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        public virtual void EditSubItem(OLVListItem item, int subItemIndex) {
            if (item == null)
                return;

            if (subItemIndex < 0 && subItemIndex >= item.SubItems.Count)
                return;

            if (this.CellEditActivation == CellEditActivateMode.None)
                return;

            if (!this.GetColumn(subItemIndex).IsEditable)
                return;

            this.StartCellEdit(item, subItemIndex);
        }

        /// <summary>
        /// Really start an edit operation on a given cell. The parameters are assumed to be sane.
        /// </summary>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        protected virtual void StartCellEdit(OLVListItem item, int subItemIndex) {
            OLVColumn column = this.GetColumn(subItemIndex);
            Rectangle r = this.CalculateCellEditorBounds(item, subItemIndex);
            Control c = this.GetCellEditor(item, subItemIndex);
            c.Bounds = r;

            // Try to align the control as the column is aligned. Not all controls support this property
            PropertyInfo pinfo = c.GetType().GetProperty("TextAlign");
            if (pinfo != null)
                pinfo.SetValue(c, column.TextAlign, null);

            // Give the control the value from the model
            this.SetControlValue(c, column.GetValue(item.RowObject), column.GetStringValue(item.RowObject));

            // Give the outside world the chance to munge with the process
            this.cellEditEventArgs = new CellEditEventArgs(column, c, r, item, subItemIndex);
            this.OnCellEditStarting(this.cellEditEventArgs);
            if (this.cellEditEventArgs.Cancel)
                return;

            // The event handler may have completely changed the control, so we need to remember it
            this.cellEditor = this.cellEditEventArgs.Control;

            // If the control isn't the height of the cell, centre it vertically. We don't
            // need to do this when in Tile view.
            if (this.View != View.Tile && this.cellEditor.Height != r.Height)
                this.cellEditor.Top += (r.Height - this.cellEditor.Height) / 2;

            this.Controls.Add(this.cellEditor);
            this.ConfigureControl();
            this.PauseAnimations(true);
        }
        private Control cellEditor = null;
        private CellEditEventArgs cellEditEventArgs = null;

        /// <summary>
        /// Calculate the bounds of the edit control for the given item/column
        /// </summary>
        /// <param name="item"></param>
        /// <param name="subItemIndex"></param>
        /// <returns></returns>
        public Rectangle CalculateCellEditorBounds(OLVListItem item, int subItemIndex) {
            Rectangle r = this.CalculateCellBounds(item, subItemIndex);
            if (this.OwnerDraw)
                return CalculateCellEditorBoundsOwnerDrawn(item, subItemIndex, r);
            else
                return CalculateCellEditorBoundsStandard(item, subItemIndex, r);
        }

        /// <summary>
        /// Calculate the bounds of the edit control for the given item/column, when the listview
        /// is being owner drawn.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="subItemIndex"></param>
        /// <param name="cellBounds"></param>
        /// <returns>A rectangle that is the bounds of the cell editor</returns>
        protected Rectangle CalculateCellEditorBoundsOwnerDrawn(OLVListItem item, int subItemIndex, Rectangle r) {
            IRenderer renderer = null;
            if (this.View == View.Details)
                renderer = this.GetColumn(subItemIndex).Renderer ?? this.DefaultRenderer;
            else
                renderer = this.ItemRenderer;

            if (renderer == null)
                return r;
            else
                return renderer.GetEditRectangle(this.CreateGraphics(), r, item, subItemIndex);
        }

        /// <summary>
        /// Calculate the bounds of the edit control for the given item/column, when the listview
        /// is not being owner drawn.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="subItemIndex"></param>
        /// <param name="cellBounds"></param>
        /// <returns>A rectangle that is the bounds of the cell editor</returns>
        protected Rectangle CalculateCellEditorBoundsStandard(OLVListItem item, int subItemIndex, Rectangle cellBounds) {
            if (this.View != View.Details)
                return cellBounds;//            this.GetItemRect(item.Index, ItemBoundsPortion.Label);

            // Allow for image (if there is one)
            int offset = 0;
            object subItemImageSelector = item.ImageSelector;
            if (subItemIndex > 0)
                subItemImageSelector = ((OLVListSubItem)item.SubItems[subItemIndex]).ImageSelector;
            if (this.GetActualImageIndex(subItemImageSelector) != -1) {
                offset += this.SmallImageSize.Width + 2;
            }

            // Allow for checkbox
            if (this.CheckBoxes && this.StateImageList != null && subItemIndex == 0) {
                offset += this.StateImageList.ImageSize.Width + 2;
            }

            // Allow for indent (first column only)
            if (subItemIndex == 0 && item.IndentCount > 0) {
                offset += (this.SmallImageSize.Width * item.IndentCount);
            }

            // Do the adjustment
            if (offset > 0) {
                cellBounds.X += offset;
                cellBounds.Width -= offset;
            }

            return cellBounds;
        }

        /// <summary>
        /// Try to give the given value to the provided control. Fall back to assigning a string
        /// if the value assignment fails.
        /// </summary>
        /// <param name="c">A control</param>
        /// <param name="value">The value to be given to the control</param>
        /// <param name="stringValue">The string to be given if the value doesn't work</param>
        protected virtual void SetControlValue(Control control, Object value, String stringValue) {
            // Handle combobox explicitly
            if (control is ComboBox) {
                ComboBox cb = ((ComboBox)control);
                if (cb.Created)
                    cb.SelectedValue = value;
                else
                    this.BeginInvoke(new MethodInvoker(delegate {
                        cb.SelectedValue = value;
                    }));
                return;
            }

            // Look for a property called "Value". We have to look twice, the first time might get an ambiguous
            PropertyInfo pinfo = null;
            try {
                pinfo = control.GetType().GetProperty("Value");
            }
            catch (AmbiguousMatchException) {
                // The lowest level class of the control must have overridden the "Value" property.
                // We now have to specifically  look for only public instance properties declared in the lowest level class.
                pinfo = control.GetType().GetProperty("Value", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            }

            // If we found it, use it to assign a value, otherwise simply set the text
            if (pinfo != null) {
                try {
                    pinfo.SetValue(control, value, null);
                    return;
                }
                catch (TargetInvocationException) {
                    // Not a lot we can do about this one. Something went wrong in the bowels
                    // of the method. Let's take the ostrich approach and just ignore it :-)
                }
                catch (ArgumentException) {
                }
            }

            // There wasn't a Value property, or we couldn't set it, so set the text instead
            try {
                String valueAsString = value as String;
                if (valueAsString == null)
                    control.Text = stringValue;
                else
                    control.Text = valueAsString;
            }
            catch (ArgumentOutOfRangeException) {
                // The value couldn't be set via the Text property.
            }
        }

        /// <summary>
        /// Setup the given control to be a cell editor
        /// </summary>
        protected virtual void ConfigureControl() {
            this.cellEditor.Validating += new CancelEventHandler(CellEditor_Validating);
            this.cellEditor.Select();
        }

        /// <summary>
        /// Return the value that the given control is showing
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        protected virtual Object GetControlValue(Control control) {
            if (control is TextBox)
                return ((TextBox)control).Text;

            if (control is ComboBox)
                return ((ComboBox)control).SelectedValue;

            if (control is CheckBox)
                return ((CheckBox)control).Checked;

            try {
                return control.GetType().InvokeMember("Value", BindingFlags.GetProperty, null, control, null);
            }
            catch (MissingMethodException) { // Microsoft throws this
                return control.Text;
            }
            catch (MissingFieldException) { // Mono throws this
                return control.Text;
            }
        }

        /// <summary>
        /// Called when the cell editor could be about to lose focus. Time to commit the change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CellEditor_Validating(object sender, CancelEventArgs e) {
            this.cellEditEventArgs.Cancel = false;
            this.OnCellEditorValidating(this.cellEditEventArgs);

            if (this.cellEditEventArgs.Cancel) {
                this.cellEditEventArgs.Control.Select();
                e.Cancel = true;
            } else
                FinishCellEdit();
        }

        /// <summary>
        /// Return the bounds of the given cell
        /// </summary>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        /// <returns>A Rectangle</returns>
        public virtual Rectangle CalculateCellBounds(OLVListItem item, int subItemIndex) {
            // We use ItemBoundsPortion.Label rather than ItemBoundsPortion.Item
            // since Label extends to the right edge of the cell, whereas Item gives just the 
            // current text width.
            return this.CalculateCellBounds(item, subItemIndex, ItemBoundsPortion.Label);
        }

        /// <summary>
        /// Return the bounds of the given cell only until the edge of the current text
        /// </summary>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        /// <returns>A Rectangle</returns>
        public virtual Rectangle CalculateCellTextBounds(OLVListItem item, int subItemIndex) {
            return this.CalculateCellBounds(item, subItemIndex, ItemBoundsPortion.ItemOnly);
        }

        private Rectangle CalculateCellBounds(OLVListItem item, int subItemIndex, ItemBoundsPortion portion) {
            // SubItem.Bounds works for every subitem, except the first.
            if (subItemIndex > 0)
                return item.SubItems[subItemIndex].Bounds;

            // For non detail views, we just use the requested portion
            Rectangle r = this.GetItemRect(item.Index, portion);
            if (this.View != View.Details)
                return r;

            // Finding the bounds of cell 0 should not be a difficult task, but it is. Problems:
            // 1) item.SubItem[0].Bounds is always the full bounds of the entire row, not just cell 0.
            // 2) if column 0 has been dragged to some other position, the bounds always has a left edge of 0.

            // We avoid both these problems by using the position of sides the column header to calculate
            // the sides of the cell
            Point sides = NativeMethods.GetScrolledColumnSides(this, 0);
            r.X = sides.X + 4;
            r.Width = sides.Y - sides.X - 5;

            return r;
        }

        /// <summary>
        /// Return a control that can be used to edit the value of the given cell.
        /// </summary>
        /// <param name="item">The row to be edited</param>
        /// <param name="subItemIndex">The index of the cell to be edited</param>
        /// <returns>A Control to edit the given cell</returns>
        protected virtual Control GetCellEditor(OLVListItem item, int subItemIndex) {
            OLVColumn column = this.GetColumn(subItemIndex);
            Object value = column.GetValue(item.RowObject);

            // If the value of the cell is null, we can't decide what type of editor to use.
            // Run through the first 1000 rows looking for a non-null value in this column.
            for (int i = 0; value == null && i < Math.Min(this.GetItemCount(), 1000); i++)
                value = column.GetValue(this.GetModelObject(i));

            // TODO: What do we do if value is still null here?

            // Ask the registry for an instance of the appropriate editor.
            Control editor = ObjectListView.EditorRegistry.GetEditor(item.RowObject, column, value);

            // Use a default editor if the registry can't create one for us.
            if (editor == null)
                editor = this.MakeDefaultCellEditor(column);

            return editor;
        }

        /// <summary>
        /// Return a TextBox that can be used as a default cell editor.
        /// </summary>
        /// <param name="column">What column does the cell belong to?</param>
        /// <returns></returns>
        protected virtual Control MakeDefaultCellEditor(OLVColumn column) {
            TextBox tb = new TextBox();
            this.ConfigureAutoComplete(tb, column);
            return tb;
        }

        /// <summary>
        /// Configure the given text box to autocomplete unique values
        /// from the given column. At most 1000 rows will be considered.
        /// </summary>
        /// <param name="tb">The textbox to configure</param>
        /// <param name="column">The column used to calculate values</param>
        public void ConfigureAutoComplete(TextBox tb, OLVColumn column) {
            this.ConfigureAutoComplete(tb, column, 1000);
        }


        /// <summary>
        /// Configure the given text box to autocomplete unique values
        /// from the given column. At most 1000 rows will be considered.
        /// </summary>
        /// <param name="tb">The textbox to configure</param>
        /// <param name="column">The column used to calculate values</param>
        /// <param name="maxRows">Consider only this many rows</param>
        public void ConfigureAutoComplete(TextBox tb, OLVColumn column, int maxRows) {
            // Don't consider more rows than we actually have
            maxRows = Math.Min(this.GetItemCount(), maxRows);

            // Reset any existing autocomplete
            tb.AutoCompleteCustomSource.Clear();

            // Build a list of unique values, to be used as autocomplete on the editor
            Dictionary<string, bool> alreadySeen = new Dictionary<string, bool>();
            List<string> values = new List<string>();
            string valueAsString;
            for (int i = 0; i < maxRows; i++) {
                valueAsString = column.GetStringValue(this.GetModelObject(i));
                if (!String.IsNullOrEmpty(valueAsString) && !alreadySeen.ContainsKey(valueAsString)) {
                    values.Add(valueAsString);
                    alreadySeen[valueAsString] = true;
                }
            }

            tb.AutoCompleteCustomSource.AddRange(values.ToArray());
            tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tb.AutoCompleteMode = AutoCompleteMode.Append;
        }

        /// <summary>
        /// Stop editing a cell and throw away any changes.
        /// </summary>
        public virtual void CancelCellEdit() {
            if (!this.IsCellEditing)
                return;

            // Let the world know that the user has cancelled the edit operation
            this.cellEditEventArgs.Cancel = true;
            this.OnCellEditFinishing(this.cellEditEventArgs);

            // Now cleanup the editing process
            this.CleanupCellEdit();
        }

        /// <summary>
        /// If a cell edit is in progress, finish the edit.
        /// </summary>
        /// <returns>Returns false if the finishing process was cancelled
        /// (i.e. the cell editor is still on screen)</returns>
        /// <remarks>This method does not guarantee that the editing will finish. The validation
        /// process can cause the finishing to be aborted. Developers should check the return value
        /// or use IsCellEditing property after calling this method to see if the user is still
        /// editing a cell.</remarks>
        public virtual bool PossibleFinishCellEditing() {
            if (this.IsCellEditing) {
                this.cellEditEventArgs.Cancel = false;
                this.OnCellEditorValidating(this.cellEditEventArgs);

                if (this.cellEditEventArgs.Cancel)
                    return false;

                this.FinishCellEdit();
            }

            return true;
        }

        /// <summary>
        /// Finish the cell edit operation, writing changed data back to the model object
        /// </summary>
        /// <remarks>This method does not trigger a Validating event, so it always finishes
        /// the cell edit.</remarks>
        public virtual void FinishCellEdit() {
            if (!this.IsCellEditing)
                return;

            this.cellEditEventArgs.Cancel = false;
            this.OnCellEditFinishing(this.cellEditEventArgs);

            // If someone doesn't cancel the editing process, write the value back into the model
            if (!this.cellEditEventArgs.Cancel) {
                Object value = this.GetControlValue(this.cellEditor);
                this.cellEditEventArgs.Column.PutValue(this.cellEditEventArgs.RowObject, value);
                this.RefreshItem(this.cellEditEventArgs.ListViewItem);
            }

            this.CleanupCellEdit();
        }

        /// <summary>
        /// Remove all trace of any existing cell edit operation
        /// </summary>
        protected virtual void CleanupCellEdit() {
            if (this.cellEditor == null)
                return;

            this.cellEditor.Validating -= new CancelEventHandler(CellEditor_Validating);
            this.Controls.Remove(this.cellEditor);
            this.cellEditor.Dispose(); //THINK: do we need to call this?
            this.cellEditor = null;
            this.Select();
            this.PauseAnimations(false);
        }

        #endregion

        #region Hot Item handling

        /// <summary>
        /// Force the hot item to be recalculated
        /// </summary>
        public virtual void ClearHotItem() {
            this.UpdateHotItem(new Point(-1, -1));
            //this.ClearTransitions();
        }

        /// <summary>
        /// Force the hot item to be recalculated
        /// </summary>
        public virtual void RefreshHotItem() {
            this.UpdateHotItem(this.PointToClient(Cursor.Position));
        }

        /// <summary>
        /// The mouse has moved to the given pt. See if the hot item needs to be updated
        /// </summary>
        /// <param name="pt">Where is the mouse?</param>
        /// <remarks>This is the main entry point for hot item handling</remarks>
        protected virtual void UpdateHotItem(Point pt) {
            if (!this.UseHotItem || this.HotItemStyle == null)
                return;

            // Figure out which item the mouse is over
            int newHotItem = -1;
            ListViewHitTestInfo hti = this.HitTest(pt);
            if (hti.Item != null && !hti.Item.Selected) {
                // If the list is full row select or they are hovering over column 0,
                // then we pay attention to any change.
                if (this.FullRowSelect || hti.Item.SubItems.IndexOf(hti.SubItem) == 0)
                    newHotItem = hti.Item.Index;
            }

            // If the hot item hasn't changed, we don't need to do anything else
            if (this.HotItemIndex == newHotItem)
                return;

            // Invalidate the old and new hot items so they are redrawn
            int oldHotItem = this.HotItemIndex;
            this.HotItemIndex = newHotItem;
            this.BeginUpdate();
            try {
                if (oldHotItem != -1)
                    this.UnapplyHotItemStyle(oldHotItem);

                if (newHotItem != -1)
                    this.ApplyHotItemStyle(newHotItem);
            }
            finally {
                this.EndUpdate();
            }
        }

        protected virtual void ApplyHotItemStyle(int index) {
            //this.RegisterHotItemTransition(index);

            // Virtual lists apply hot item style when fetching their rows
            if (this.VirtualMode)
                this.RedrawItems(index, index, false);
            else
                this.ApplyHotItemStyle(this.GetItem(index));
        }

        protected virtual void ApplyHotItemStyle(OLVListItem olvi) {
            if (!this.FullRowSelect) {
                this.ApplyHotItemStyle(olvi, olvi.SubItems[0]);
                return;
            }

            if (this.HotItemStyle.Font != null)
                olvi.Font = this.HotItemStyle.Font;

            if (this.HotItemStyle.FontStyle != FontStyle.Regular) {
                Font f = olvi.Font;
                if (f == null)
                    f = this.Font;
                olvi.Font = new Font(f, this.HotItemStyle.FontStyle);
            }

            if (this.HotItemStyle.ForeColor != Color.Empty)
                olvi.ForeColor = this.HotItemStyle.ForeColor;

            if (this.HotItemStyle.BackColor != Color.Empty)
                olvi.BackColor = this.HotItemStyle.BackColor;

            this.CorrectSubItemColors(olvi);
        }

        protected virtual void ApplyHotItemStyle(ListViewItem olvi, ListViewItem.ListViewSubItem si) {
            olvi.UseItemStyleForSubItems = false;

            // Background color is applied to all sub items
            foreach (ListViewItem.ListViewSubItem x in olvi.SubItems) {
                if (this.HotItemStyle.BackColor == Color.Empty)
                    x.BackColor = olvi.BackColor;
                else
                    x.BackColor = this.HotItemStyle.BackColor;
                x.ForeColor = olvi.ForeColor;
            }

            if (this.HotItemStyle.Font != null)
                si.Font = this.HotItemStyle.Font;

            if (this.HotItemStyle.FontStyle != FontStyle.Regular) {
                Font f = olvi.Font;
                if (f == null)
                    f = this.Font;
                si.Font = new Font(f, this.HotItemStyle.FontStyle);
            }

            if (this.HotItemStyle.ForeColor != Color.Empty)
                si.ForeColor = this.HotItemStyle.ForeColor;
        }

        protected virtual void UnapplyHotItemStyle(int index) {
            if (this.VirtualMode)
                this.RedrawItems(index, index, false);
            else
                this.UnapplyHotItemStyle(this.GetItem(index));
        }

        protected virtual void UnapplyHotItemStyle(OLVListItem olvi) {
            if (olvi != null) {
                olvi.UseItemStyleForSubItems = true;
                this.RefreshItem(olvi);
                //this.FillInValues(olvi, olvi.RowObject);
            }
        }

        //Dictionary<int, TransitionState> transitionStateMap = new Dictionary<int, TransitionState>();

        //internal TransitionState GetHotItemTransitionState(int rowIndex)
        //{
        //    TransitionState state;

        //    if (this.transitionStateMap.TryGetValue(rowIndex, out state))
        //        return state;
        //    else
        //        return null;
        //}

        //protected virtual void RegisterHotItemTransition(int newHotItem)
        //{
        //    if (this.GetHotItemTransitionState(newHotItem) == null)
        //        this.transitionStateMap[newHotItem] = new TransitionState(newHotItem);
        //}

        //protected virtual void tickler_Tick(object sender, EventArgs e)
        //{
        //    if (this.InvokeRequired)
        //        this.Invoke(new MethodInvoker(this.tickler_TickInUIThread));
        //    else
        //        this.tickler_TickInUIThread();
        //}

        //protected virtual void tickler_TickInUIThread()
        //{
        //    List<int> toRemove = new List<int>();
        //    foreach (TransitionState state in this.transitionStateMap.Values) {
        //        state.Tick(state.RowIndex == this.HotItemIndex);
        //        if (!this.UseHotItem || state.IsDone || state.RowIndex >= this.GetItemCount())
        //            toRemove.Add(state.RowIndex);
        //        else
        //            this.RedrawItems(state.RowIndex, state.RowIndex, false);
        //    }

        //    foreach (int rowIndex in toRemove) {
        //        this.transitionStateMap.Remove(rowIndex);
        //        this.RedrawItems(rowIndex, rowIndex, false);
        //    }
        //}

        //protected virtual void ClearTransitions()
        //{
        //    foreach (TransitionState state in this.transitionStateMap.Values)
        //        state.IsDone = true;
        //}

        #endregion
        
        #region Drag and drop

        protected override void OnItemDrag(ItemDragEventArgs e) {
            base.OnItemDrag(e);

            if (this.DragSource == null)
                return;
            
            Object data = this.DragSource.StartDrag(this, e.Button, (OLVListItem)e.Item);
            if (data != null) {
                DragDropEffects effect = this.DoDragDrop(data, this.DragSource.GetAllowedEffects(data));
                this.DragSource.EndDrag(data, effect);
            }
        }

        protected override void OnDragEnter(DragEventArgs args) {
            base.OnDragEnter(args);

            if (this.DropSink != null)
                this.DropSink.Enter(args);
        }

        protected override void OnDragOver(DragEventArgs args) {
            base.OnDragOver(args);

            if (this.DropSink != null)
                this.DropSink.Over(args);
        }

        protected override void OnDragDrop(DragEventArgs args) {
            base.OnDragDrop(args);

            if (this.DropSink != null)
                this.DropSink.Drop(args);
        }

        protected override void OnDragLeave(EventArgs e) {
            base.OnDragLeave(e);

            if (this.DropSink != null)
                this.DropSink.Leave();
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs args) {
            base.OnGiveFeedback(args);

            if (this.DropSink != null)
                this.DropSink.GiveFeedback(args);
        }

        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs args) {
            base.OnQueryContinueDrag(args);

            if (this.DropSink != null)
                this.DropSink.QueryContinue(args);
        }

        #endregion

        #region Decorations and Overlays

        /// <summary>
        /// Add the given overlay to those on this list and make it appear
        /// </summary>
        /// <param name="overlay">The overlay</param>
        /// <remarks>
        /// A decoration scrolls with the listview. An overlay stays fixed in place.
        /// </remarks>
        public virtual void AddDecoration(IOverlay decoration) {
            this.Decorations.Add(decoration);
            this.Invalidate();
        }

        /// <summary>
        /// Add the given overlay to those on this list and make it appear
        /// </summary>
        /// <param name="overlay">The overlay</param>
        public virtual void AddOverlay(IOverlay overlay) {
            this.Overlays.Add(overlay);
            this.RefreshOverlays();
        }

        /// <summary>
        /// Draw all the decorations
        /// </summary>
        /// <param name="g">A Graphics</param>
        protected virtual void DrawAllDecorations(Graphics g) {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle contentRectangle = this.ContentRectangle;

            if (this.HasEmptyListMsg && this.GetItemCount() == 0) {
                this.EmptyListMsgOverlay.Draw(this, g, contentRectangle);
            }

            // Let the drop sink draw whatever feedback it likes
            if (this.DropSink != null) {
                this.DropSink.DrawFeedback(g, contentRectangle);
            }

            // Draw our decorations
            g.TranslateTransform(0 - NativeMethods.GetScrollPosition(this, true), 0);
            foreach (IOverlay decoration in this.Decorations) {
                decoration.Draw(this, g, contentRectangle);
            }
            g.ResetTransform();

            // If we are in design mode, we don't want to use the glass panel,
            // so we draw the background overlays here
            if (this.DesignMode) {
                this.DrawBackgroundOverlays(g);
            }
        }

        /// <summary>
        /// Draw the overlays
        /// </summary>
        /// <param name="g"></param>
        internal void DrawBackgroundOverlays(Graphics g) {
            Rectangle contentRectangle = this.ContentRectangle;
            foreach (IOverlay overlay in this.Overlays) {
                overlay.Draw(this, g, contentRectangle);
            }
        }

        /// <summary>
        /// Is the given overlay showne on this list
        /// </summary>
        /// <param name="overlay">The overlay</param>
        public virtual bool HasDecoration(IOverlay overlay) {
            return this.Decorations.Contains(overlay);
        }

        /// <summary>
        /// Is the given overlay shown on this list?
        /// </summary>
        /// <param name="overlay">The overlay</param>
        public virtual bool HasOverlay(IOverlay overlay) {
            return this.Overlays.Contains(overlay);
        }

        /// <summary>
        /// Hide any overlays.
        /// </summary>
        /// <remarks>
        /// This is only a temporary hiding -- the overlays will be shown
        /// the next time the ObjectListView redraws.
        /// </remarks>
        public virtual void HideOverlays() {
            if (this.glassPanel != null)
                this.glassPanel.HideGlass();
        }
        private GlassPanelForm glassPanel;

        /// <summary>
        /// Create and configure the empty list msg overlay
        /// </summary>
        protected virtual void InitializeEmptyListMsgOverlay() {
            TextOverlay textOverlay = new TextOverlay();
            textOverlay.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            textOverlay.TextColor = SystemColors.ControlDarkDark;
            textOverlay.BackColor = Color.BlanchedAlmond;
            textOverlay.BorderColor = SystemColors.ControlDark;
            textOverlay.BorderWidth = 2.0f;
            this.EmptyListMsgOverlay = textOverlay;
        }

        /// <summary>
        /// Initialize the standard image and text overlays
        /// </summary>
        protected virtual void InitializeStandardOverlays() {
            this.OverlayImage = new ImageOverlay();
            this.Overlays.Add(this.OverlayImage);
            this.OverlayText = new TextOverlay();
            this.Overlays.Add(this.OverlayText);
        }

        /// <summary>
        /// Make sure that any overlays are visible.
        /// </summary>
        public virtual void ShowOverlays() {
            // If we are in design mode or there are no overlays, don't use a glass panel
            if (this.DesignMode || !this.HasOverlays)
                return;

            if (this.glassPanel == null) {
                this.glassPanel = new GlassPanelForm();
                this.glassPanel.Bind(this);
            }

            this.glassPanel.ShowGlass();
        }

        /// <summary>
        /// Refresh the display of the overlays
        /// </summary>
        public virtual void RefreshOverlays() {
            if (this.glassPanel != null) {
                this.glassPanel.Invalidate();
            }
        }

        /// <summary>
        /// Remove the given decoration from this list
        /// </summary>
        /// <param name="overlay">The overlay</param>
        public virtual void RemoveDecoration(IOverlay overlay) {
            this.Decorations.Remove(overlay);
            this.Invalidate();
        }

        /// <summary>
        /// Remove the given overlay to those on this list
        /// </summary>
        /// <param name="overlay">The overlay</param>
        public virtual void RemoveOverlay(IOverlay overlay) {
            this.Overlays.Remove(overlay);
            this.RefreshOverlays();
        }

        #endregion

        #region Design Time

        /// <summary>
        /// This class works in conjunction with the OLVColumns property to allow OLVColumns
        /// to be added to the ObjectListView.
        /// </summary>
        internal class OLVColumnCollectionEditor : System.ComponentModel.Design.CollectionEditor
        {
            public OLVColumnCollectionEditor(Type t)
                : base(t) {
            }

            protected override Type CreateCollectionItemType() {
                return typeof(OLVColumn);
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
                // Figure out which ObjectListView we are working on. This should be the Instance of the context.
                ObjectListView olv = null;
                if (context != null)
                    olv = context.Instance as ObjectListView;

                if (olv == null) {
                    //THINK: Can this ever happen?
                    System.Diagnostics.Debug.WriteLine("context.Instance was NOT an ObjectListView");

                    // Hack to figure out which ObjectListView we are working on
                    ListView.ColumnHeaderCollection cols = (ListView.ColumnHeaderCollection)value;
                    if (cols.Count == 0) {
                        cols.Add(new OLVColumn());
                        olv = (ObjectListView)cols[0].ListView;
                        cols.Clear();
                        olv.AllColumns.Clear();
                    } else
                        olv = (ObjectListView)cols[0].ListView;
                }

                // Edit all the columns, not just the ones that are visible
                base.EditValue(context, provider, olv.AllColumns);

                // Calculate just the visible columns
                List<OLVColumn> newColumns = olv.GetFilteredColumns(View.Details);
                olv.Columns.Clear();
                olv.Columns.AddRange(newColumns.ToArray());

                return olv.Columns;
            }

            protected override string GetDisplayText(object value) {
                OLVColumn col = value as OLVColumn;
                if (col == null || String.IsNullOrEmpty(col.AspectName))
                    return base.GetDisplayText(value);

                return base.GetDisplayText(value) + " (" + col.AspectName + ")";
            }
        }

        /// <summary>
        /// Return Columns for this list. We hide the original so we can associate
        /// a specialised editor with it.
        /// </summary>
        [Editor(typeof(OLVColumnCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        new public ListView.ColumnHeaderCollection Columns {
            get {
                return base.Columns;
            }
        }

        #endregion

        #region Implementation variables

        private Rectangle lastUpdateRectangle; // remember the update rect from the last WM_PAINT message
        private bool isOwnerOfObjects; // does this ObjectListView own the Objects collection?
        private bool hasIdleHandler; // has an Idle handler already been installed?
        private bool isInWmPaintEvent; // is a WmPaint event currently being handled?
        private bool shouldDoCustomDrawing; // should the list do its custom drawing?
        private ImageOverlay imageOverlay; // the image overlay that is controlled by IDE settings
        private TextOverlay textOverlay; // the text overlay that is controlled by IDE settings
        private bool isMarqueSelecting; // Is a margque selection in progress?

        #endregion
    }

    #region Delegate declarations

    /// <summary>
    /// These delegates are used to extract an aspect from a row object
    /// </summary>
    public delegate Object AspectGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to put a changed value back into a model object
    /// </summary>
    public delegate void AspectPutterDelegate(Object rowObject, Object newValue);

    /// <summary>
    /// These delegates can be used to convert an aspect value to a display string,
    /// instead of using the default ToString()
    /// </summary>
    public delegate string AspectToStringConverterDelegate(Object value);

    /// <summary>
    /// These delegates are used to get the tooltip for a cell
    /// </summary>
    public delegate String CellToolTipGetterDelegate(OLVColumn column, Object modelObject);

    /// <summary>
    /// These delegates are used to the state of the checkbox for a row object.
    /// </summary>
    /// <remarks><para>
    /// For reasons known only to someone in Microsoft, we can only set
    /// a boolean on the ListViewItem to indicate it's "checked-ness", but when
    /// we receive update events, we have to use a tristate CheckState. So we can
    /// be told about an indeterminate state, but we can't set it ourselves.
    /// </para>
    /// <para>As of version 2.0, we can now return indeterminate state.</para>
    /// </remarks>
    public delegate CheckState CheckStateGetterDelegate(Object rowObject);
    public delegate bool BooleanCheckStateGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to put a changed check state back into a model object
    /// </summary>
    public delegate CheckState CheckStatePutterDelegate(Object rowObject, CheckState newValue);
    public delegate bool BooleanCheckStatePutterDelegate(Object rowObject, bool newValue);

    /// <summary>
    /// The callbacks for RightColumnClick events
    /// </summary>
    public delegate void ColumnRightClickEventHandler(object sender, ColumnClickEventArgs e);

    /// <summary>
    /// <summary>
    /// These delegates are used to retrieve the object that is the key of the group to which the given row belongs.
    /// </summary>
    public delegate Object GroupKeyGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to convert a group key into a title for the group
    /// </summary>
    public delegate string GroupKeyToTitleConverterDelegate(Object groupKey);

    /// <summary>
    /// These delegates are used to get the tooltip for a column header
    /// </summary>
    public delegate String HeaderToolTipGetterDelegate(OLVColumn column);

    /// <summary>
    /// These delegates are used to fetch the image selector that should be used
    /// to choose an image for this column.
    /// </summary>
    public delegate Object ImageGetterDelegate(Object rowObject);

    /// <summary>
    /// These delegates are used to draw a cell
    /// </summary>
    public delegate bool RenderDelegate(EventArgs e, Graphics g, Rectangle r, Object rowObject);

    /// <summary>
    /// These delegates are used to fetch a row object for virtual lists
    /// </summary>
    public delegate Object RowGetterDelegate(int rowIndex);

    /// <summary>
    /// These delegates are used to format a listviewitem before it is added to the control.
    /// </summary>
    public delegate void RowFormatterDelegate(OLVListItem olvItem);

    /// <summary>
    /// These delegates are used to sort the listview in some custom fashion
    /// </summary>
    public delegate void SortDelegate(OLVColumn column, SortOrder sortOrder);

    #endregion

    #region Column

    /// <summary>
    /// An OLVColumn knows which aspect of an object it should present.
    /// </summary>
    /// <remarks>
    /// The column knows how to:
    /// <list type="bullet">
    ///	<item>extract its aspect from the row object</item>
    ///	<item>convert an aspect to a string</item>
    ///	<item>calculate the image for the row object</item>
    ///	<item>extract a group "key" from the row object</item>
    ///	<item>convert a group "key" into a title for the group</item>
    /// </list>
    /// <para>For sorting to work correctly, aspects from the same column
    /// must be of the same type, that is, the same aspect cannot sometimes
    /// return strings and other times integers.</para>
    /// </remarks>
    [Browsable(false)]
    public partial class OLVColumn : ColumnHeader
    {
        /// <summary>
        /// Create an OLVColumn
        /// </summary>
        public OLVColumn()
            : base() {
        }

        /// <summary>
        /// Initialize a column to have the given title, and show the given aspect
        /// </summary>
        /// <param name="title">The title of the column</param>
        /// <param name="aspect">The aspect to be shown in the column</param>
        public OLVColumn(string title, string aspect)
            : this() {
            this.Text = title;
            this.AspectName = aspect;
        }

        #region Public Properties

        /// <summary>
        /// This delegate will be used to extract a value to be displayed in this column.
        /// </summary>
        /// <remarks>
        /// If this is set, AspectName is ignored.
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AspectGetterDelegate AspectGetter {
            get { return aspectGetter; }
            set { aspectGetter = value; }
        }
        private AspectGetterDelegate aspectGetter;

        /// <summary>
        /// Remember if this aspect getter for this column was generated internally, and can therefore
        /// be regenerated at will
        /// </summary>
        [Obsolete("This property is no longer maintained", true),
         Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AspectGetterAutoGenerated {
            get { return aspectGetterAutoGenerated; }
            set { aspectGetterAutoGenerated = value; }
        }
        private bool aspectGetterAutoGenerated;

        /// <summary>
        /// The name of the property or method that should be called to get the value to display in this column.
        /// This is only used if a ValueGetterDelegate has not been given.
        /// </summary>
        /// <remarks>This name can be dotted to chain references to properties or parameter-less methods.</remarks>
        /// <example>"DateOfBirth"</example>
        /// <example>"Owner.HomeAddress.Postcode"</example>
        [Category("Behavior - ObjectListView"),
         Description("The name of the property or method that should be called to get the aspect to display in this column"),
         DefaultValue(null)]
        public string AspectName {
            get { return aspectName; }
            set {
                aspectName = value;
                this.aspectMunger = null;
            }
        }
        private string aspectName;

        /// <summary>
        /// This delegate will be used to put an edited value back into the model object.
        /// </summary>
        /// <remarks>
        /// This does nothing if IsEditable == false.
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AspectPutterDelegate AspectPutter {
            get { return aspectPutter; }
            set { aspectPutter = value; }
        }
        private AspectPutterDelegate aspectPutter;

        /// <summary>
        /// The delegate that will be used to translate the aspect to display in this column into a string.
        /// </summary>
        /// <remarks>If this value is set, AspectToStringFormat will be ignored.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AspectToStringConverterDelegate AspectToStringConverter {
            get { return aspectToStringConverter; }
            set { aspectToStringConverter = value; }
        }
        private AspectToStringConverterDelegate aspectToStringConverter;

        /// <summary>
        /// This format string will be used to convert an aspect to its string representation.
        /// </summary>
        /// <remarks>
        /// This string is passed as the first parameter to the String.Format() method.
        /// This is only used if AspectToStringConverter has not been set.</remarks>
        /// <example>"{0:C}" to convert a number to currency</example>
        [Category("Behavior - ObjectListView"),
         Description("The format string that will be used to convert an aspect to its string representation"),
         DefaultValue(null)]
        public string AspectToStringFormat {
            get { return aspectToStringFormat; }
            set { aspectToStringFormat = value; }
        }
        private string aspectToStringFormat;

        /// <summary>
        /// Should this column show a checkbox, rather than a string?
        /// </summary>
        /// <remarks>
        /// Setting this on column 0 has no effect. Column 0 check box is controlled
        /// by the list view itself.
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("Should values in this column be treated as a checkbox, rather than a string?"),
         DefaultValue(false)]
        public virtual bool CheckBoxes {
            get { return checkBoxes; }
            set {
                this.checkBoxes = value;
                if (this.Renderer == null)
                    this.Renderer = new CheckStateRenderer();
            }
        }
        private bool checkBoxes;

        /// <summary>
        /// Should this column have a tri-state checkbox?
        /// </summary>
        /// <remarks>
        /// If this is true, the user can choose the third state (normally Indeterminate).
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("Should values in this column be treated as a tri-state checkbox?"),
         DefaultValue(false)]
        public virtual bool TriStateCheckBoxes {
            get { return triStateCheckBoxes; }
            set {
                triStateCheckBoxes = value;
                if (value && !this.CheckBoxes)
                    this.CheckBoxes = true;
            }
        }
        private bool triStateCheckBoxes;

        /// <summary>
        /// Should this column resize to fill the free space in the listview?
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you want two (or more) columns to equally share the available free space, set this property to True.
        /// If you want this column to have a larger or smaller share of the free space, you must
        /// set the FreeSpaceProportion property explicitly.
        /// </para>
        /// <para>
        /// Space filling columns are still governed by the MinimumWidth and MaximumWidth properties.
        /// </para>
        /// /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("Will this column resize to fill unoccupied horizontal space in the listview?"),
         DefaultValue(false)]
        public bool FillsFreeSpace {
            get { return this.FreeSpaceProportion > 0; }
            set {
                if (value)
                    this.freeSpaceProportion = 1;
                else
                    this.freeSpaceProportion = 0;
            }
        }

        /// <summary>
        /// What proportion of the unoccupied horizontal space in the control should be given to this column?
        /// </summary>
        /// <remarks>
        /// <para>
        /// There are situations where it would be nice if a column (normally the rightmost one) would expand as
        /// the list view expands, so that as much of the column was visible as possible without having to scroll
        /// horizontally (you should never, ever make your users have to scroll anything horizontally!).
        /// </para>
        /// <para>
        /// A space filling column is resized to occupy a proportion of the unoccupied width of the listview (the
        /// unoccupied width is the width left over once all the the non-filling columns have been given their space).
        /// This property indicates the relative proportion of that unoccupied space that will be given to this column.
        /// The actual value of this property is not important -- only its value relative to the value in other columns.
        /// For example:
        /// <list type="bullet">
        /// <item>
        /// If there is only one space filling column, it will be given all the free space, regardless of the value in FreeSpaceProportion.
        /// </item>
        /// <item>
        /// If there are two or more space filling columns and they all have the same value for FreeSpaceProportion,
        /// they will share the free space equally.
        /// </item>
        /// <item>
        /// If there are three space filling columns with values of 3, 2, and 1
        /// for FreeSpaceProportion, then the first column with occupy half the free space, the second will
        /// occupy one-third of the free space, and the third column one-sixth of the free space.
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FreeSpaceProportion {
            get { return freeSpaceProportion; }
            set { freeSpaceProportion = Math.Max(0, value); }
        }
        private int freeSpaceProportion = 0;

        /// <summary>
        /// This delegate is called to get the object that is the key for the group
        /// to which the given row belongs.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GroupKeyGetterDelegate GroupKeyGetter {
            get { return groupKeyGetter; }
            set { groupKeyGetter = value; }
        }
        private GroupKeyGetterDelegate groupKeyGetter;

        /// <summary>
        /// This delegate is called to convert a group key into a title for that group.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GroupKeyToTitleConverterDelegate GroupKeyToTitleConverter {
            get { return groupKeyToTitleConverter; }
            set { groupKeyToTitleConverter = value; }
        }
        private GroupKeyToTitleConverterDelegate groupKeyToTitleConverter;

        /// <summary>
        /// When the listview is grouped by this column and group title has an item count,
        /// how should the lable be formatted?
        /// </summary>
        /// <remarks>
        /// The given format string can/should have two placeholders:
        /// <list type="bullet">
        /// <item>{0} - the original group title</item>
        /// <item>{1} - the number of items in the group</item>
        /// </list>
        /// <para>If this value is not set, the values from the list view will be used</para>
        /// </remarks>
        /// <example>"{0} [{1} items]"</example>
        [Category("Behavior - ObjectListView"),
         Description("The format to use when suffixing item counts to group titles"),
         DefaultValue(null),
         Localizable(true)]
        public string GroupWithItemCountFormat {
            get { return groupWithItemCountFormat; }
            set { groupWithItemCountFormat = value; }
        }
        private string groupWithItemCountFormat;

        /// <summary>
        /// Return this.GroupWithItemCountFormat or a reasonable default
        /// </summary>
        [Browsable(false)]
        public string GroupWithItemCountFormatOrDefault {
            get {
                if (String.IsNullOrEmpty(this.GroupWithItemCountFormat))
                    // There is one rare but pathelogically possible case where the ListView can
                    // be null, so we have to provide a workable default for that rare case.
                    if (this.ListView == null)
                        return "{0} [{1} items]";
                    else
                        return ((ObjectListView)this.ListView).GroupWithItemCountFormatOrDefault;
                else
                    return this.GroupWithItemCountFormat;
            }
        }

        /// <summary>
        /// When the listview is grouped by this column and a group title has an item count,
        /// how should the lable be formatted if there is only one item in the group?
        /// </summary>
        /// <remarks>
        /// The given format string can/should have two placeholders:
        /// <list type="bullet">
        /// <item>{0} - the original group title</item>
        /// <item>{1} - the number of items in the group (always 1)</item>
        /// </list>
        /// <para>If this value is not set, the values from the list view will be used</para>
        /// </remarks>
        /// <example>"{0} [{1} item]"</example>
        [Category("Behavior - ObjectListView"),
         Description("The format to use when suffixing item counts to group titles"),
         DefaultValue(null),
         Localizable(true)]
        public string GroupWithItemCountSingularFormat {
            get { return groupWithItemCountSingularFormat; }
            set { groupWithItemCountSingularFormat = value; }
        }
        private string groupWithItemCountSingularFormat;

        /// <summary>
        /// Return this.GroupWithItemCountSingularFormat or a reasonable default
        /// </summary>
        [Browsable(false)]
        public string GroupWithItemCountSingularFormatOrDefault {
            get {
                if (String.IsNullOrEmpty(this.GroupWithItemCountSingularFormat))
                    // There is one pathelogically rare but still possible case where the ListView can
                    // be null, so we have to provide a workable default for that rare case.
                    if (this.ListView == null)
                        return "{0} [{1} item]";
                    else
                        return ((ObjectListView)this.ListView).GroupWithItemCountSingularFormatOrDefault;
                else
                    return this.GroupWithItemCountSingularFormat;
            }
        }

        /// <summary>
        /// This delegate is called to get the image selector of the image that should be shown in this column.
        /// It can return an int, string, Image or null.
        /// </summary>
        /// <remarks><para>This delegate can use these return value to identify the image:</para>
        /// <list>
        /// <item>null or -1 -- indicates no image</item>
        /// <item>an int -- the int value will be used as an index into the image list</item>
        /// <item>a String -- the string value will be used as a key into the image list</item>
        /// <item>an Image -- the Image will be drawn directly (only in OwnerDrawn mode)</item>
        /// </list>
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImageGetterDelegate ImageGetter {
            get { return imageGetter; }
            set { imageGetter = value; }
        }
        private ImageGetterDelegate imageGetter;

        /// <summary>
        /// Can the values shown in this column be edited?
        /// </summary>
        /// <remarks>This defaults to true, since the primary means to control the editability of a listview
        /// is on the listview itself. Once a listview is editable, all the columns are too, unless the
        /// programmer explicitly marks them as not editable</remarks>
        [Category("Behavior - ObjectListView"),
         Description("Can the value in this column be edited?"),
         DefaultValue(true)]
        public bool IsEditable {
            get { return isEditable; }
            set { isEditable = value; }
        }
        private bool isEditable = true;

        /// <summary>
        /// Is this column a fixed width column?
        /// </summary>
        [Browsable(false)]
        public bool IsFixedWidth {
            get {
                return (this.MinimumWidth != -1 && this.MaximumWidth != -1 && this.MinimumWidth >= this.MaximumWidth);
            }
        }

        /// <summary>
        /// Get/set whether this column should be used when the view is switched to tile view.
        /// </summary>
        /// <remarks>Column 0 is always included in tileview regardless of this setting.
        /// Tile views do not work well with many "columns" of information, 2 or 3 works best.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("Will this column be used when the view is switched to tile view"),
         DefaultValue(false)]
        public bool IsTileViewColumn {
            get { return isTileViewColumn; }
            set { isTileViewColumn = value; }
        }
        private bool isTileViewColumn = false;

        /// <summary>
        /// Can this column be seen by the user?
        /// </summary>
        /// <remarks>After changing this value, you must call RebuildColumns() before the changes will be effected.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("Can this column be seen by the user?"),
         DefaultValue(true)]
        public bool IsVisible {
            get { return isVisible; }
            set { isVisible = value; }
        }
        private bool isVisible = true;

        /// <summary>
        /// Where was this column last positioned within the Detail view columns
        /// </summary>
        /// <remarks>DisplayIndex is volatile. Once a column is removed from the control,
        /// there is no way to discover where it was in the display order. This property
        /// guards that information even when the column is not in the listview's active columns.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LastDisplayIndex = -1;

        /// <summary>
        /// What is the maximum width that the user can give to this column?
        /// </summary>
        /// <remarks>-1 means there is no maximum width. Give this the same value as MinimumWidth to make a fixed width column.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("What is the maximum width to which the user can resize this column?"),
         DefaultValue(-1)]
        public int MaximumWidth {
            get { return maxWidth; }
            set {
                maxWidth = value;
                if (maxWidth != -1 && this.Width > maxWidth)
                    this.Width = maxWidth;
            }
        }
        private int maxWidth = -1;

        /// <summary>
        /// What is the minimum width that the user can give to this column?
        /// </summary>
        /// <remarks>-1 means there is no minimum width. Give this the same value as MaximumWidth to make a fixed width column.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("What is the minimum width to which the user can resize this column?"),
         DefaultValue(-1)]
        public int MinimumWidth {
            get { return minWidth; }
            set {
                minWidth = value;
                if (this.Width < minWidth)
                    this.Width = minWidth;
            }
        }
        private int minWidth = -1;

        /// <summary>
        /// Get/set the renderer that will be invoked when a cell needs to be redrawn
        /// </summary>
        [Category("Behavior - ObjectListView"),
        Description("The renderer will draw this column when the ListView is owner drawn"),
        DefaultValue(null)]
        public IRenderer Renderer {
            get { return renderer; }
            set { renderer = value; }
        }
        private IRenderer renderer;

        /// <summary>
        /// This delegate is called when a cell needs to be drawn in OwnerDrawn mode.
        /// </summary>
        /// <remarks>This method is kept primarily for backwards compatibility.
        /// New code should implement an IRenderer, though this property will be maintained.</remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RenderDelegate RendererDelegate {
            get {
                if (this.Renderer is Version1Renderer)
                    return ((Version1Renderer)this.Renderer).RenderDelegate;
                else
                    return null;
            }
            set {
                if (value == null)
                    this.Renderer = null;
                else
                    this.Renderer = new Version1Renderer(value);
            }
        }

        /// <summary>
        /// What string should be displayed when the mouse is hovered over the header of this column?
        /// </summary>
        /// <remarks>If a HeaderToolTipGetter is installed on the owning ObjectListView, this
        /// value will be ignored.</remarks>
        [Category("Behavior - ObjectListView"),
         Description("The tooltip to show when the mouse is hovered over the header of this column"),
         DefaultValue((String)null),
         Localizable(true)]
        public String ToolTipText {
            get { return toolTipText; }
            set { toolTipText = value; }
        }
        private String toolTipText;

        /// <summary>
        /// Group objects by the initial letter of the aspect of the column
        /// </summary>
        /// <remarks>
        /// One common pattern is to group column by the initial letter of the value for that group.
        /// The aspect must be a string (obviously).
        /// </remarks>
        [Category("Behavior - ObjectListView"),
         Description("The name of the property or method that should be called to get the aspect to display in this column"),
         DefaultValue(false)]
        public bool UseInitialLetterForGroup {
            get { return useInitialLetterForGroup; }
            set { useInitialLetterForGroup = value; }
        }
        private bool useInitialLetterForGroup;

        #endregion

        #region Object commands

        /// <summary>
        /// For a given group value, return the string that should be used as the groups title.
        /// </summary>
        /// <param name="value">The group key that is being converted to a title</param>
        /// <returns>string</returns>
        public string ConvertGroupKeyToTitle(object value) {
            if (this.groupKeyToTitleConverter == null)
                if (value == null)
                    return "{null}";
                else
                    return this.ValueToString(value);
            else
                return this.groupKeyToTitleConverter(value);
        }

        /// <summary>
        /// Get the checkedness of the given object for this column
        /// </summary>
        /// <param name="rowObject">The row object that is being displayed</param>
        /// <returns>The checkedness of the object</returns>
        public CheckState GetCheckState(object rowObject) {
            if (!this.CheckBoxes)
                return CheckState.Unchecked;

            Object aspect = this.GetValue(rowObject);
            bool? aspectAsBool = aspect as bool?;
            if (aspectAsBool.HasValue) {
                if ((bool)aspectAsBool)
                    return CheckState.Checked;
                else
                    return CheckState.Unchecked;
            } else
                return CheckState.Indeterminate;
        }

        /// <summary>
        /// Put the checkedness of the given object for this column
        /// </summary>
        /// <param name="rowObject">The row object that is being displayed</param>
        /// <returns>The checkedness of the object</returns>
        public void PutCheckState(object rowObject, CheckState newState) {
            if (newState == CheckState.Checked)
                this.PutValue(rowObject, true);
            else
                if (newState == CheckState.Unchecked)
                    this.PutValue(rowObject, false);
                else
                    this.PutValue(rowObject, null);
        }

        /// <summary>
        /// For a given row object, extract the value indicated by the AspectName property of this column.
        /// </summary>
        /// <param name="rowObject">The row object that is being displayed</param>
        /// <returns>An object, which is the aspect named by AspectName</returns>
        public object GetAspectByName(object rowObject) {
            if (this.aspectMunger == null)
                this.aspectMunger = new Munger(this.AspectName);

            return this.aspectMunger.GetValue(rowObject);
        }
        private Munger aspectMunger;

        /// <summary>
        /// For a given row object, return the object that is the key of the group that this row belongs to.
        /// </summary>
        /// <param name="rowObject">The row object that is being displayed</param>
        /// <returns>Group key object</returns>
        public object GetGroupKey(object rowObject) {
            if (this.groupKeyGetter == null) {
                object key = this.GetValue(rowObject);
                String keyAsString = key as String;
                if (keyAsString != null && this.UseInitialLetterForGroup) {
                    if (keyAsString.Length > 0)
                        key = keyAsString.Substring(0, 1).ToUpper();
                }
                return key;
            } else
                return this.groupKeyGetter(rowObject);
        }

        /// <summary>
        /// For a given row object, return the image selector of the image that should displayed in this column.
        /// </summary>
        /// <param name="rowObject">The row object that is being displayed</param>
        /// <returns>int or string or Image. int or string will be used as index into image list. null or -1 means no image</returns>
        public Object GetImage(object rowObject) {
            if (this.CheckBoxes)
                return this.GetCheckStateImage(rowObject);

            if (this.imageGetter != null)
                return this.imageGetter(rowObject);

            if (!String.IsNullOrEmpty(this.ImageKey))
                return this.ImageKey;

            return this.ImageIndex;
        }

        /// <summary>
        /// Return the image that represents the check box for the given model
        /// </summary>
        /// <param name="rowObject"></param>
        /// <returns></returns>
        public string GetCheckStateImage(Object rowObject) {
            CheckState checkState = this.GetCheckState(rowObject);

            if (checkState == CheckState.Checked)
                return ObjectListView.CHECKED_KEY;

            if (checkState == CheckState.Unchecked)
                return ObjectListView.UNCHECKED_KEY;

            return ObjectListView.INDETERMINATE_KEY;
        }

        /// <summary>
        /// For a given row object, return the string representation of the value shown in this column.
        /// </summary>
        /// <remarks>
        /// For aspects that are string (e.g. aPerson.Name), the aspect and its string representation are the same.
        /// For non-strings (e.g. aPerson.DateOfBirth), the string representation is very different.
        /// </remarks>
        /// <param name="rowObject"></param>
        /// <returns></returns>
        public string GetStringValue(object rowObject) {
            return this.ValueToString(this.GetValue(rowObject));
        }

        /// <summary>
        /// For a given row object, return the object that is to be displayed in this column.
        /// </summary>
        /// <param name="rowObject">The row object that is being displayed</param>
        /// <returns>An object, which is the aspect to be displayed</returns>
        public object GetValue(object rowObject) {
            if (this.AspectGetter == null)
                return this.GetAspectByName(rowObject);
            else
                return this.AspectGetter(rowObject);
        }

        /// <summary>
        /// Update the given model object with the given value using the column's
        /// AspectName.
        /// </summary>
        /// <param name="rowObject">The model object to be updated</param>
        /// <param name="newValue">The value to be put into the model</param>
        public void PutAspectByName(Object rowObject, Object newValue) {
            if (this.aspectMunger == null)
                this.aspectMunger = new Munger(this.AspectName);

            this.aspectMunger.PutValue(rowObject, newValue);
        }

        /// <summary>
        /// Update the given model object with the given value
        /// </summary>
        /// <param name="rowObject">The model object to be updated</param>
        /// <param name="newValue">The value to be put into the model</param>
        public void PutValue(Object rowObject, Object newValue) {
            if (this.aspectPutter == null)
                this.PutAspectByName(rowObject, newValue);
            else
                this.aspectPutter(rowObject, newValue);
        }

        /// <summary>
        /// Convert the aspect object to its string representation.
        /// </summary>
        /// <remarks>
        /// If the column has been given a ToStringDelegate, that will be used to do
        /// the conversion, otherwise just use ToString(). Nulls are always converted
        /// to empty strings.
        /// </remarks>
        /// <param name="value">The value of the aspect that should be displayed</param>
        /// <returns>A string representation of the aspect</returns>
        public string ValueToString(object value) {
            // CONSIDER: Should we give aspect-to-string converters a chance to work on a null value?
            if (value == null)
                return "";

            if (this.AspectToStringConverter != null)
                return this.AspectToStringConverter(value);

            string fmt = this.AspectToStringFormat;
            if (String.IsNullOrEmpty(fmt))
                return value.ToString();
            else
                return String.Format(fmt, value);
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Install delegates that will group the columns aspects into progressive partitions.
        /// If an aspect is less than value[n], it will be grouped with description[n].
        /// If an aspect has a value greater than the last element in "values", it will be grouped
        /// with the last element in "descriptions".
        /// </summary>
        /// <param name="values">Array of values. Values must be able to be
        /// compared to the aspect (using IComparable)</param>
        /// <param name="descriptions">The description for the matching value. The last element is the default description.
        /// If there are n values, there must be n+1 descriptions.</param>
        /// <example>
        /// this.salaryColumn.MakeGroupies(
        ///     new UInt32[] { 20000, 100000 },
        ///     new string[] { "Lowly worker",  "Middle management", "Rarified elevation"});
        /// </example>
        public void MakeGroupies<T>(T[] values, string[] descriptions) {
            if (values.Length + 1 != descriptions.Length)
                throw new ArgumentException("descriptions must have one more element than values.");

            // Install a delegate that returns the index of the description to be shown
            this.GroupKeyGetter = delegate(object row) {
                Object aspect = this.GetValue(row);
                if (aspect == null || aspect == System.DBNull.Value)
                    return -1;
                IComparable comparable = (IComparable)aspect;
                for (int i = 0; i < values.Length; i++) {
                    if (comparable.CompareTo(values[i]) < 0)
                        return i;
                }

                // Display the last element in the array
                return descriptions.Length - 1;
            };

            // Install a delegate that simply looks up the given index in the descriptions.
            this.GroupKeyToTitleConverter = delegate(object key) {
                if ((int)key < 0)
                    return "";

                return descriptions[(int)key];
            };
        }

        #endregion
    }

    #endregion

    #region OLVListItem and OLVListSubItem

    /// <summary>
    /// OLVListItems are specialized ListViewItems that know which row object they came from,
    /// and the row index at which they are displayed, even when in group view mode. They
    /// also know the image they should draw against themselves
    /// </summary>
    public class OLVListItem : ListViewItem
    {
        /// <summary>
        /// Create a OLVListItem for the given row object
        /// </summary>
        public OLVListItem(object rowObject)
            : base() {
            this.rowObject = rowObject;
        }

        /// <summary>
        /// Create a OLVListItem for the given row object, represented by the given string and image
        /// </summary>
        public OLVListItem(object rowObject, string text, Object image)
            : base(text, -1) {
            this.rowObject = rowObject;
            this.imageSelector = image;
        }

        /// <summary>
        /// RowObject is the model object that is source of the data for this list item.
        /// </summary>
        public object RowObject {
            get { return rowObject; }
            set { rowObject = value; }
        }
        private object rowObject;

        /// <summary>
        /// DisplayIndex is the index of the row where this item is displayed. For flat lists,
        /// this is the same as ListViewItem.Index, but for grouped views, it is different.
        /// </summary>
        [Obsolete("This property is no longer maintained", true)]
        public int DisplayIndex {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// Get or set the image that should be shown against this item
        /// </summary>
        /// <remarks><para>This can be an Image, a string or an int. A string or an int will
        /// be used as an index into the small image list.</para></remarks>
        public Object ImageSelector {
            get { return imageSelector; }
            set {
                imageSelector = value;
                if (value is Int32)
                    this.ImageIndex = (Int32)value;
                else if (value is String)
                    this.ImageKey = (String)value;
                else
                    this.ImageIndex = -1;
            }
        }
        private Object imageSelector;

        /// <summary>
        /// Enable tri-state checkbox.
        /// </summary>
        /// <remarks>.NET's Checked property was not built to handle tri-state checkboxes,
        /// and will return True for both Checked and Indeterminate states.</remarks>
        public CheckState CheckState {
            get {
                switch (this.StateImageIndex) {
                    case 0:
                        return System.Windows.Forms.CheckState.Unchecked;
                    case 1:
                        return System.Windows.Forms.CheckState.Checked;
                    case 2:
                        return System.Windows.Forms.CheckState.Indeterminate;
                    default:
                        return System.Windows.Forms.CheckState.Unchecked;
                }
            }
            set {
                if (this.checkState == value)
                    return;

                this.checkState = value;

                //THINK: I don't think we need this, since the Checked property just uses StateImageIndex, which we are about to set.
                //this.Checked = (checkState == CheckState.Checked);

                // We have to specifically set the state image
                switch (value) {
                    case System.Windows.Forms.CheckState.Unchecked:
                        this.StateImageIndex = 0;
                        break;
                    case System.Windows.Forms.CheckState.Checked:
                        this.StateImageIndex = 1;
                        break;
                    case System.Windows.Forms.CheckState.Indeterminate:
                        this.StateImageIndex = 2;
                        break;
                }
            }
        }
        private CheckState checkState;

        /// <summary>
        /// Gets the bounding rectangle of the item, including all subitems
        /// </summary>
        new public Rectangle Bounds {
            get {
                try {
                    return base.Bounds;
                }
                catch (System.ArgumentException) {
                    // If the item is part of a collapsed group, Bounds will throw an exception
                    return Rectangle.Empty;
                }
            }
        }
    }

    /// <summary>
    /// A ListViewSubItem that knows which image should be drawn against it.
    /// </summary>
    [Browsable(false)]
    public class OLVListSubItem : ListViewItem.ListViewSubItem
    {
        /// <summary>
        /// Create a OLVListSubItem
        /// </summary>
        public OLVListSubItem()
            : base() {
        }

        /// <summary>
        /// Create a OLVListSubItem that shows the given string and image
        /// </summary>
        public OLVListSubItem(string text, Object image)
            : base() {
            this.Text = text;
            this.ImageSelector = image;
        }

        /// <summary>
        /// Get or set the image that should be shown against this item
        /// </summary>
        /// <remarks><para>This can be an Image, a string or an int. A string or an int will
        /// be used as an index into the small image list.</para></remarks>
        public Object ImageSelector {
            get { return imageSelector; }
            set { imageSelector = value; }
        }
        private Object imageSelector;

        /// <summary>
        /// Return the state of the animatation of the image on this subitem.
        /// Null means there is either no image, or it is not an animation
        /// </summary>
        internal ImageRenderer.AnimationState AnimationState {
            get { return animationState; }
            set { animationState = value; }
        }
        private ImageRenderer.AnimationState animationState;

    }

    #endregion

    /// <summary>
    /// Instances of this class specify how should "hot items" (non-selected
    /// rows under the cursor) be renderered.
    /// </summary>
    public class HotItemStyle : System.ComponentModel.Component
    {
        [DefaultValue(null)]
        public Font Font {
            get { return this.font; }
            set { this.font = value; }
        }
        private Font font;

        [DefaultValue(FontStyle.Regular)]
        public FontStyle FontStyle {
            get { return this.fontStyle; }
            set { this.fontStyle = value; }
        }
        private FontStyle fontStyle;

        [DefaultValue(typeof(Color), "")]
        public Color ForeColor {
            get { return this.foreColor; }
            set { this.foreColor = value; }
        }
        private Color foreColor;

        [DefaultValue(typeof(Color), "")]
        public Color BackColor {
            get { return this.backColor; }
            set { this.backColor = value; }
        }
        private Color backColor;
    }

    /// <summary>
    /// Instances of this class encapsulate the information gathered during a OlvHitTest()
    /// operation.
    /// </summary>
    /// <remarks>Custom renderers can use HitTestLocation.UserDefined and the UserData
    /// object to store more specific locations for use during event handlers.</remarks>
    public class OlvListViewHitTestInfo
    {
        public OlvListViewHitTestInfo(ListViewHitTestInfo hti) {
            this.item = (OLVListItem)hti.Item;
            this.subItem = hti.SubItem;
            this.location = hti.Location;

            switch (hti.Location) {
                case ListViewHitTestLocations.StateImage:
                    this.HitTestLocation = HitTestLocation.CheckBox;
                    break;
                case ListViewHitTestLocations.Image:
                    this.HitTestLocation = HitTestLocation.Image;
                    break;
                case ListViewHitTestLocations.Label:
                    this.HitTestLocation = HitTestLocation.Text;
                    break;
                default:
                    this.HitTestLocation = HitTestLocation.Nothing;
                    break;
            }
        }

        #region Public fields

        /// <summary>
        /// Where is the hit location?
        /// </summary>
        public HitTestLocation HitTestLocation;

        /// <summary>
        /// Custom renderers can use this information to supply more details about the hit location
        /// </summary>
        public Object UserData;

        #endregion

        #region Public read-only properties

        /// <summary>
        /// Gets the item that was hit
        /// </summary>
        public OLVListItem Item {
            get { return item; }
        }
        private OLVListItem item;

        /// <summary>
        /// Gets the subitem that was hit
        /// </summary>
        public ListViewItem.ListViewSubItem SubItem {
            get { return subItem; }
        }
        private ListViewItem.ListViewSubItem subItem;

        /// <summary>
        /// Gets the part of the subitem that was hit
        /// </summary>
        public ListViewHitTestLocations Location {
            get { return location; }
        }
        private ListViewHitTestLocations location;

        /// <summary>
        /// Gets the ObjectListView that was tested
        /// </summary>
        public ObjectListView ListView {
            get {
                if (this.Item == null)
                    return null;
                else
                    return (ObjectListView)this.Item.ListView;
            }
        }

        /// <summary>
        /// Gets the model object that was hit
        /// </summary>
        public Object RowObject {
            get {
                if (this.Item == null)
                    return null;
                else
                    return this.Item.RowObject;
            }
        }

        /// <summary>
        /// Gets the index of the column under the hit point
        /// </summary>
        public int ColumnIndex {
            get {
                if (this.Item == null || this.SubItem == null)
                    return -1;
                else
                    return this.Item.SubItems.IndexOf(this.SubItem);
            }
        }

        /// <summary>
        /// Gets the column that was hit
        /// </summary>
        public OLVColumn Column {
            get {
                int index = this.ColumnIndex;
                if (index < 0)
                    return null;
                else
                    return this.ListView.GetColumn(index);
            }
        }

        #endregion
    }

    public enum HitTestLocation
    {
        Nothing,
        Text,
        Image,
        CheckBox,
        ExpandButton,
        InCell, // in the cell but not in any more specific location
        UserDefined
    }

    //internal class TransitionState
    //{
    //    public TransitionState(int rowIndex)
    //    {
    //        this.RowIndex = rowIndex;
    //    }
    //    public int RowIndex;
    //    public float Progress = 0.5f;
    //    public float Step = 0.1f;

    //    public bool IsDone
    //    {
    //        get { return (this.Progress < 0.0f); }
    //        set {
    //            if (value)
    //                this.Progress = -1.0f;
    //            else
    //                this.Progress = 0.0f;
    //        }
    //    }

    //    public void Tick(bool forward)
    //    {
    //        if (forward)
    //            this.Progress = Math.Min(1.0f, this.Progress + this.Step);
    //        else
    //            this.Progress = Math.Max(-this.Step, this.Progress - this.Step);
    //    }
    //}

    // public class TransitionRenderer : BaseRenderer
    // {
    //     public TransitionRenderer()
    //     {
    //     }

    //     internal TransitionState GetTransitionState()
    //     {
    //         if (this.ListView == null)
    //             return null;
    //         else
    //             return this.ListView.GetHotItemTransitionState(this.ListItem.Index);
    //     }

    //     //public override bool OptionalRender(Graphics g, Rectangle r)
    //     //{
    //     //    TransitionState state = this.GetHotItemTransitionState();
    //     //    if (state == null || this.IsItemSelected)
    //     //        return base.OptionalRender(g, r);

    //     //    if (!state.IsInitialized)
    //     //        this.InitializeState(g, r, state);

    //     //    this.DrawBackground(g, r);
    //     //    this.RenderBitmaps(g, r, state);

    //     //    return true;
    //     //}

    //     protected override void DrawBackground(Graphics g, Rectangle r)
    //     {
    //         base.DrawBackground(g, r);

    //         TransitionState state = this.GetTransitionState();
    //         if (state == null || this.IsItemSelected)
    //             return;

    //         Color c = Color.Plum;
    //         g.SmoothingMode = SmoothingMode.AntiAlias;
    //         const int rounding = 16;
    //         GraphicsPath path = this.GetRoundedRect(r, rounding);
    //         //g.FillPath(new SolidBrush(Color.FromArgb((int)(196 * state.Progress), c)), path);

    //         PathGradientBrush pthGrBrush = new PathGradientBrush(path);
    //         pthGrBrush.CenterColor = Color.FromArgb((int)(255 * state.Progress), c);
    //         Color[] colors = { Color.FromArgb((int)(96 * state.Progress), c) };
    //         pthGrBrush.SurroundColors = colors;
    //         g.FillPath(pthGrBrush, path);
    //     }

    //     private GraphicsPath GetRoundedRect(RectangleF rect, float diameter)
    //     {
    //         GraphicsPath path = new GraphicsPath();

    //         RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);
    //         path.AddArc(arc, 180, 90);
    //         arc.X = rect.Right - diameter;
    //         path.AddArc(arc, 270, 90);
    //         arc.Y = rect.Bottom - diameter;
    //         path.AddArc(arc, 0, 90);
    //         arc.X = rect.Left;
    //         path.AddArc(arc, 90, 90);
    //         path.CloseFigure();

    //         return path;
    //     }

    //     /*
    //     private void InitializeState(Graphics g, Rectangle r, TransitionState state)
    //     {
    //         state.FromBitmap = new Bitmap(r.Width, r.Height, g);
    //         Graphics bitmapGraphics = Graphics.FromImage(state.FromBitmap);
    //         System.Diagnostics.Debug.WriteLine("from");
    //         System.Diagnostics.Debug.WriteLine(this.GetText());

    //         base.OptionalRender(bitmapGraphics, new Rectangle(0, 0, r.Width, r.Height));

    //         state.ToBitmap = new Bitmap(r.Width, r.Height, g);
    //         Graphics bitmapGraphics2 = Graphics.FromImage(state.ToBitmap);
    //         bitmapGraphics2.FillEllipse(Brushes.Gold, new Rectangle(0, 0, r.Width, r.Height));
    //         this.IsDrawBackground = false;
    //         System.Diagnostics.Debug.WriteLine("to");
    //         System.Diagnostics.Debug.WriteLine(this.GetText());
    //         base.OptionalRender(bitmapGraphics2, new Rectangle(0, 0, r.Width, r.Height));
    //         this.IsDrawBackground = true;
    //     }

    //     private void RenderBitmaps(Graphics g, Rectangle r, TransitionState state)
    //     {
    //         if (state.Progress >= 1.0f)
    //             g.DrawImage(state.ToBitmap, r.X, r.Y);
    //         else if (state.Progress <= 0.0f)
    //             g.DrawImage(state.FromBitmap, r.X, r.Y);
    //         else
    //         this.BlendBitmaps(g, r, state.FromBitmap, state.ToBitmap, state.Progress);
    //     }

    //     private void BlendBitmaps(Graphics g, Rectangle r, Bitmap fromBitmap, Bitmap toBitmap, float transition)
    //     {
    //         float[][] colorMatrixElements = {
    //new float[] {1,  0,  0,  0, 0},
    //new float[] {0,  1,  0,  0, 0},
    //new float[] {0,  0,  1,  0, 0},
    //new float[] {0,  0,  0,  transition, 0},
    //new float[] {0,  0,  0,  0, 1}};

    //         ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
    //         ImageAttributes imageAttributes = new ImageAttributes();
    //         imageAttributes.SetColorMatrix(colorMatrix);

    //         g.DrawImage(
    //            toBitmap,
    //            new Rectangle(r.X, r.Y, toBitmap.Size.Width, toBitmap.Size.Height),  // destination rectangle
    //            0, 0,        // upper-left corner of source rectangle
    //            toBitmap.Size.Width,       // width of source rectangle
    //            toBitmap.Size.Height,      // height of source rectangle
    //            GraphicsUnit.Pixel,
    //            imageAttributes);

    //         colorMatrix.Matrix33 = 1.0f - transition;
    //         imageAttributes.SetColorMatrix(colorMatrix);

    //         g.DrawImage(
    //            fromBitmap,
    //            new Rectangle(r.X, r.Y, fromBitmap.Size.Width, fromBitmap.Size.Height),  // destination rectangle
    //            0, 0,        // upper-left corner of source rectangle
    //            fromBitmap.Size.Width,       // width of source rectangle
    //            fromBitmap.Size.Height,      // height of source rectangle
    //            GraphicsUnit.Pixel,
    //            imageAttributes);
    //     }
    //     */
    // }

    /// <summary>
    /// A simple-minded implementation of a Dictionary that can handle null as a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key</typeparam>
    /// <typeparam name="TValue">The type of the values to be stored</typeparam>
    /// <remarks>This is not a full implementation and is only meant to handle
    /// collecting groups by their keys, since groups can have null as a key value.</remarks>
    internal class NullableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private bool hasNullKey;
        private TValue nullValue;

        new public TValue this[TKey key] {
            get {
                if (key == null) {
                    if (hasNullKey)
                        return nullValue;
                    else
                        throw new KeyNotFoundException();
                } else
                    return base[key];
            }
            set {
                if (key == null) {
                    this.hasNullKey = true;
                    this.nullValue = value;
                } else
                    base[key] = value;
            }
        }

        new public bool ContainsKey(TKey key) {
            if (key == null)
                return this.hasNullKey;
            else
                return base.ContainsKey(key);
        }

        new public IList Keys {
            get {
                ArrayList list = new ArrayList(base.Keys);
                if (this.hasNullKey)
                    list.Add(null);
                return list;
            }
        }
    }
}
