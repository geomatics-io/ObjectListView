.. -*- coding: UTF-8 -*-

:Subtitle: All you wanted to know and more...

.. _changelog:

Change Log
==========

Version Index
-------------
* `v2.1 - 24 February 2009`_
* `v2.1a - 07 February 2009`_
* `v2.0.1 - 10 January 2009`_
* `v2.0 - 30 November 2008`_
* `v1.13 - 24 July 2008`_
* `v1.12 - 10 May 2008`_
* `v1.11 - 11 April 2008`_
* `v1.10 - 19 March 2008`_
* `v1.9.1 - 02 February 2008`_
* `v1.9 - 16 January 2008`_
* `v1.8 - 30 November 2007`_
* `v1.6 - 30 September 2007`_
* `v1.5 - 03 August 2007`_
* `v1.4 - 30 April 2007`_
* `Previous versions - 04 April 2007`_


v2.1 - 24 February 2009
-----------------------

2009-02-23 22:07 (#461) - Tests/Program.cs, Tests/TestCheckBoxes.cs
  - Allow tests for check events for virtual lists

2009-02-23 22:07 (#460) - docs/index.rst
  - Added some more nice references

2009-02-23 22:05 (#459) - ObjectListView/ObjectListView.cs, ObjectListView/TreeListView.cs
  - Reworked checkboxes so that events are triggered for virtual lists
  - ToggleCheckObject() now handle TriStateCheckBoxes
  - Removed some commented out code

2009-02-23 20:15 (#458) - Demo/MainForm.Designer.cs, Demo/MainForm.cs
  - ItemCheck and ItemChecked events

2009-02-23 20:14 (#457) - ObjectListView/ObjectListView.cs, ObjectListView/VirtualObjectListView.cs
  - Try to get ItemCheck and ItemChecked events to work on virtual lists

2009-02-23 15:13 (#456) - ObjectListView/ObjectListView.cs
  - Added ObjectListView.ConfigureAutoComplete utility method
  - Added RowsPerPage property
  - Optimized native windows message handling

2009-02-23 15:10 (#455) - ObjectListView/Munger.cs
  - Made Munger a public class

2009-02-23 15:08 (#453) - ObjectListView/ObjectListView.cs, ObjectListView/ObjectListView2008.csproj, ObjectListView/VirtualObjectListView.cs, ObjectListView/Properties/AssemblyInfo.cs
  - Checked items with virtual lists now works again

2009-02-09 17:15 (#452) - ObjectListView/ObjectListView.cs
  - Added IsSelected()



v2.1a - 07 February 2009
------------------------

2009-02-03 00:23 (#449) - Demo/MainForm.Designer.cs, Demo/MainForm.cs, Demo/MainForm.resx
  - Simple tab "Lock group" now locks sort order too

2009-02-03 00:22 (#448) - docs/whatsnew.rst, docs/.templates/layout.html, docs/changelog.rst, docs/faq.rst
  - Updated fixed bug descriptions
  - Generated change log
  - Fixed links to download and discussion in template

2009-02-03 00:20 (#447) - ObjectListView/ObjectListView.cs
  - Fixed bug with AlwaysGroupByColumn where column header clicks would not resort groups.

2009-02-01 23:53 (#444) - ObjectListView/ObjectListView.cs
  - Added UseSubItemCheckBoxes to initialize checkbox images
  - OLVColumn.CheckBoxes and TriStateCheckBoxes now work.

2009-02-01 23:50 (#443) - ObjectListView/Renderers.cs
  - Use slightly changed subitem checkbox scheme
  - Tweaked CheckStateRenderer

2009-02-01 23:50 (#442) - ObjectListView/DataListView.cs
  - Use slightly changed subitem checkbox scheme

2009-02-01 23:49 (#441) - Tests/TestCheckBoxes.cs
  - Changed subitem checkbox tests for new scheme

2009-02-01 23:49 (#440) - Demo/MainForm.Designer.cs, Demo/MainForm.cs, Demo/MainForm.resx
  - Polish for v2.1 alpha release

2009-02-01 23:48 (#439) - docs/recipes.rst
  - Added subitem checkbox documentation

2009-02-01 23:47 (#438) - docs/.static/structure.css
  - Remove left padding on images in cookbook

2009-02-01 08:52 (#437) - ObjectListView/ObjectListView.cs
  - Changed CalculateCellBounds to correctly calculate bounds of column 0 cells

2009-02-01 08:50 (#436) - Demo/MainForm.Designer.cs, ObjectListView/DataListView.cs, ObjectListView/Renderers.cs, Demo/MainForm.resx
  - Added CheckStateRenderer
  - Added BaseRenderer.DrawImages()
  - Reorganized methods in Renderers.cs

2009-01-31 14:01 (#435) - ObjectListView/ObjectListView.cs, ObjectListView/Renderers.cs
  - Use renderer to calculate cell editor bounds
  - Correctly calculate the bounds of cell (x, 0)

2009-01-31 14:00 (#434) - Tests/TestCheckBoxes.cs
  - Added sub item checkbox test

2009-01-31 12:32 (#433) - docs/.static/ownerDraw-icon.png, docs/.static/Thumbs.db, docs/.static/structure.css, docs/ownerDraw.rst
  - new styles docs almost complete

2009-01-31 01:12 (#431) - docs/features.rst, docs/whatsnew.rst, docs/.static/structure.css, docs/.templates/layout.html, docs/faq.rst, docs/recipes.rst
  - New style docs mostly complete

2009-01-28 19:58 (#426) - docs/images/mappedimage-renderer.png, docs/.static/gettingStarted-icon.png, docs/images/orange-800x1600.png, docs/.static/orange-800x1600.png, docs/whatsnew.rst, docs/images/coffee.jpg, docs/listCtrlPrinter.rst, docs/.static/reset.css, docs/.static/listCtrlPrinter-icon.png, docs/features.rst, docs/.static/faq-icon.png, docs/images/printpreview.png, docs/images/ownerdrawn-example1.png, docs/groupListView.rst, docs/.static/global.css, docs/gettingStarted.rst, docs/images/bar-renderer.png, docs/images/limeleaf.jpg, docs/.static/recipes-icon.png, docs/.static/whatsnew-icon.png, docs/faq.rst, docs/.static/search-icon.png, docs/.static/initial.css, docs/images/flags-renderer.png, docs/images/gettingstarted-example1.png, docs/images/gettingstarted-example2.png, docs/.static/dialog.css, docs/.static/Thumbs.db, docs/images/gettingstarted-example3.png, docs/.static/structure.css, docs/.templates/layout.html, docs/images/gettingstarted-example4.png, docs/images/gettingstarted-example5.png, docs/changelog.rst, docs/images/gettingstarted-example6.png, docs/.static/groupListView-icon.png, docs/.static/cellEditing-icon.png, docs/images/fancy-screenshot.png, docs/.static/majorClasses-icon.png, docs/images, docs/.static, docs/images/tileview-example.png, docs/.templates, docs/conf.py, docs/images/redbull.jpg, docs/images/image-renderer.png, docs/index.rst, docs/images/dialog2-blue.gif, docs/images/ReportModernExample.jpg, docs/images/ModelToScreenProcess.png, docs/cellEditing.rst, docs/images/right-arrow.png, docs/majorClasses.rst, docs, docs/images/images-renderer.png, docs/recipes.rst, docs/images/dialog2-blue-800x1600.png, docs/.static/dialog2-blue-800x1600.png, docs/images/tileview-ownerdrawn.png, docs/.static/changelog-icon.png, docs/.static/icon.ico, docs/images/right-arrow.gif, docs/images/treelistview.png, docs/images/icecream3.jpg, docs/images/ObjectListView.jpg, docs/.static/index-icon.png, docs/.static/master.css, docs/images/light-blue-800x1600.png, docs/.static/light-blue-800x1600.png, docs/images/multiimage-renderer.png, docs/.static/features-icon.png, docs/images/smoothie2.jpg, docs/images/dark-blue-800x1600.png, docs/.static/dark-blue-800x1600.png
  - New style docs

2009-01-27 23:49 (#425) - Demo/MainForm.cs
  - Use ItemRenderer on complex list view

2009-01-27 23:47 (#423) - ObjectListView/TreeListView.cs
  - Changed to use new Renderer and HitTest scheme

2009-01-27 23:47 (#422) - ObjectListView/ObjectListView.cs
  - Finished HitTest portion of new renderer scheme
  - Added ObjectListView.ItemRenderer to draw whole items (rather than double dutying the renderer of column 0)
  - Handle owner drawn of non-Details views

2009-01-27 23:44 (#421) - ObjectListView/Renderers.cs
  - Finished HitTest portion of new renderer scheme
  - Updated docs on new methods
  - Reorganized properties and methods on BaseRenderer
  - Made all methods virtual

2009-01-25 23:58 (#417) - ObjectListView/ObjectListView.cs, ObjectListView/ObjectListView2008.csproj, ObjectListView/Renderers.cs
  - First take at making Renderers into Components

2009-01-24 18:39 (#416) - ObjectListView/ObjectListView.cs, ObjectListView/Renderers.cs
  - New hit test scheme

2009-01-24 10:37 (#415) - ObjectListView/ObjectListView.cs, ObjectListView/Renderers.cs
  - Change hit test processing

2009-01-23 21:04 (#414) - ObjectListView/Renderers.cs, ObjectListView/NativeMethods.cs
  - Align image and text in accord with column alignment

2009-01-23 13:27 (#413) - ObjectListView/ObjectListView.cs
  - Simple Checkboxes now work properly
  - Added TriStateCheckBoxes property to control whether the user can set the row checkbox to have the Indeterminate value
  - CheckState property is now just a wrapper around the StateImageIndex property

2009-01-22 22:53 (#412) - ObjectListView/NativeMethods.cs
  - Added GetCountPerPage()

2009-01-22 22:52 (#411) - ObjectListView/TreeListView.cs
  - Added RevealAfterExpand property. If this is true (the default) after expanding a branch, the control scrolls to reveal as much of the expanded branch as possible.

2009-01-21 23:50 (#410) - ObjectListView/Renderers.cs
  - Changed to use TextRenderer rather than native GDI routines.
  - BaseRenderer now matches the per-pixel layout of native ListView more closely

2009-01-21 23:40 (#409) - ObjectListView/NativeMethods.cs
  - Removed GDI methods that were added in last revision

2009-01-21 00:29 (#406) - ObjectListView/Renderers.cs
  - Changed draw from image list if possible. 30% faster!
  - Tweaked some spacings to look more like native ListView
  - Text highlight for non FullRowSelect is now the right color when the control doesn't have focus.
  - Commented out experimental animations. Still needs work.

2009-01-21 00:22 (#405) - ObjectListView/ObjectListView.cs
  - Commented out experimental animations. Still needs work.

2009-01-20 20:58 (#404) - ObjectListView/ObjectListView.cs
  - Changed to always draw columns when owner drawn, rather than falling back on DrawDefault. This simplified several owner drawn problems
  - Added DefaultRenderer property to help with the above
  - HotItem background color is applied to all cells even when FullRowSelect is false
  - Allow grouping by CheckedAspectName columns

2009-01-20 20:55 (#403) - ObjectListView/Renderers.cs
  - Correctly animate hot item backgrounds

2009-01-20 12:16 (#402) - Tests/Program.cs, Tests/TestColumn.cs
  - Added tests for indexed access for column values

2009-01-20 12:15 (#401) - ObjectListView/Munger.cs
  - Made the Munger capable of handling indexed access. Incidentally, this removed the ugliness that the last change introduced.

2009-01-20 00:01 (#400) - Demo/Persons.xml
  - Added Tells Jokes field

2009-01-20 00:00 (#399) - ObjectListView/Renderers.cs
  - Changed to draw text using GDI routines. Looks more like native control this way. Set UseGdiTextRendering to false to revert to previous behavior.
  - Added IsPrinting property
  - IsDrawBackground is now calculated and cannot be set

2009-01-19 23:55 (#398) - ObjectListView/NativeMethods.cs
  - Added method need to draw text using GDI routines

2009-01-19 23:54 (#397) - ListViewPrinter/ListViewPrinter.cs
  - Use IsPrinting property on BaseRenderer

2009-01-19 18:55 (#394) - ObjectListView/CellEditors.cs
  - Added special handling for enums

2009-01-19 18:53 (#393) - ObjectListView/Events.cs
  - Moved SelectionChanged event to this file

2009-01-19 18:52 (#392) - ObjectListView/Munger.cs
  - Handle target objects from a DataListView (normally DataRowViews)

2009-01-19 18:52 (#391) - ObjectListView/DataListView.cs
  - Boolean columns are now handled as checkboxes
  - Auto-generated columns would fail if the data source was reseated, even to the same data source

2009-01-19 18:51 (#390) - ObjectListView/ObjectListView.cs
  - Added HotItemStyle and UseHotItem to highlight the row under the cursor
  - Added UseCustomSelectionColors property
  - Owner draw mode now honors ForeColor and BackColor settings on the list
  - Reorganisation all hot item handling

2009-01-19 18:48 (#389) - ObjectListView/Renderers.cs
  - Removed IsHotItem

2009-01-19 18:46 (#388) - ListViewPrinter/ListViewPrinter2008.csproj, Tests/Tests2008.csproj, Demo/ObjectListViewDemo2008.csproj, ObjectListView2008.sln, ObjectListView/ObjectListView2008.csproj, ListViewPrinterDemo/ListViewPrinterDemo2008.csproj
  - Added VS 2008 projects

2009-01-17 13:10 (#387) - ObjectListView/ObjectListView.cs, Demo/MainForm.Designer.cs, ObjectListView/VirtualObjectListView.cs, ObjectListView/CellEditors.cs, Demo/MainForm.resx
  - Improving hot tracking
  - Start enum editor

2009-01-17 11:27 (#386) - ObjectListView/ObjectListView.cs, Demo/MainForm.Designer.cs, Demo/MainForm.cs, ObjectListView/Renderers.cs
  - Polishing subitem checkboxes

2009-01-17 00:04 (#385) - ObjectListView/ObjectListView.cs, Demo/MainForm.Designer.cs, Demo/MainForm.cs, Demo/MainForm.resx
  - Checkboxes on subitems. Take I complete

2009-01-16 10:21 (#381) - ObjectListView/Renderers.cs
  - Changed the vertical position of owner drawn checkboxes a little

2009-01-16 10:20 (#380) - ObjectListView/ObjectListView.cs, ObjectListView/CellEditors.cs
  - Changed to use EditorRegistry

2009-01-15 19:57 (#379) - ObjectListView/ObjectListView.cs, ObjectListView/VirtualObjectListView.cs, ObjectListView/Renderers.cs
  - First take at animated hot tracking

2009-01-15 16:14 (#378) - ObjectListView/TreeListView.cs
  - Changed TreeRenderer to work with visual styles are disabled

2009-01-11 11:56 (#377) - ObjectListView/ObjectListView.cs
  - Changed to use Equals() method rather than == to compare model objects.



v2.0.1 - 10 January 2009
------------------------

2009-01-10 17:20 (#374) - ObjectListView/Properties/AssemblyInfo.cs
  - Updated to version 2.0.1

2009-01-10 17:07 (#373) - ObjectListView/ObjectListView.cs
  - Made FinishCellEditing public

2009-01-08 23:28 (#372) - Demo/MainForm.Designer.cs, Demo/MainForm.cs, Demo/ObjectListViewDemo.csproj
  - Handle right click on complex list to show EnsureGroupVisible() in action

2009-01-08 23:27 (#371) - ObjectListView/ObjectListView.cs
  - Added EnsureGroupVisible()

2009-01-08 23:26 (#370) - ObjectListView/NativeMethods.cs
  - Added Scroll() method

2009-01-08 18:58 (#369) - Demo/Persons.xml
  - Made one name longer to test cell wrapping

2009-01-08 18:57 (#368) - ObjectListView/ObjectListView.cs
  - Fixed long-standing "multiple columns generated" problem. Thanks to pinkjones for his help with solving this one!
  - Made all public and protected methods virtual
  - PossibleFinishCellEditing and CancelCellEditing are now public

2009-01-08 18:51 (#367) - ObjectListView/TreeListView.cs
  - Made all public and protected methods virtual
  - Changed some classes from 'internal' to 'protected' so that they can be accessed by subclasses of TreeListView.

2009-01-08 18:50 (#366) - ObjectListView/Renderers.cs
  - Made all public and protected methods virtual

2009-01-08 18:50 (#365) - ObjectListView/DataListView.cs, ObjectListView/TypedObjectListView.cs, ObjectListView/VirtualObjectListView.cs, ObjectListView/FastObjectListView.cs
  - Made all public and protected methods virtual

2008-12-29 12:17 (#364) - ObjectListView/Renderers.cs
  - Render text correctly when HideSelection is true.

2008-12-29 12:16 (#363) - ObjectListView/TreeListView.cs
  - Minor documentation change

2008-12-29 11:18 (#362) - ObjectListView/ObjectListView.cs
  - Added Description for RowHeight property

2008-12-29 11:17 (#361) - ObjectListView/Renderers.cs
  - BaseRenderer now works correctly in all Views

2008-12-23 23:10 (#360) - ObjectListView/TreeListView.cs
  - Added UseWaitCursorWhenExpanding property
  - Fixed connection line problem when there is only a single root
  - Made TreeRenderer public so that it can be subclassed

2008-12-23 23:08 (#359) - ObjectListView/Renderers.cs
  - Fixed two small bugs in BarRenderer

2008-12-23 00:41 (#358) - ObjectListView/TreeListView.cs
  - Added LinePen property to TreeRenderer to allow the connection drawing pen to be changed
  - Fixed some rendering issues where the text highlight rect was miscalculated
  - Correctly draw connections for single root object

2008-12-23 00:39 (#357) - ObjectListView/Renderers.cs
  - Fixed bug with calculating the height of a custom bar
  - Added a little more space between icon and text

2008-12-23 00:38 (#356) - Demo/MainForm.cs
  - Added comment about how to use LinePen for a TreeRenderer

2008-12-21 00:23 (#355) - ObjectListView/Comparers.cs
  - Fixed bug with group comparisons when a group key was null (SF#2445761)

2008-12-20 23:59 (#353) - ObjectListView/ObjectListView.cs
  - Fixed bug with group comparisons when a group key was null (SF#2445761)

2008-12-20 00:01 (#352) - ObjectListView/ObjectListView.cs
  - Fixed bug with space filling columns and layout events
  - Fixed RowHeight so that it only changes the row height, not the width of the images.

2008-12-19 22:14 (#351) - ListViewPrinter/BrushPenData.cs, ListViewPrinter/ListViewPrinter.cs
  - Hide all obsolete properties from the code generator
  - Correctly set the default value of colors to be Color.Empty

2008-12-10 15:17 (#346) - Demo/MainForm.Designer.cs
  - Generated code no longer includes Color.Empty, since that is the default

2008-12-10 15:17 (#345) - Demo/MainForm.cs
  - Cleaned up TreeListView initialization

2008-12-10 15:16 (#344) - ObjectListView/TreeListView.cs
  - TreeListView now works even when it doesn't have a SmallImageList

2008-12-10 15:15 (#343) - ObjectListView/ObjectListView.cs
  - Handle Backspace key. Resets the seach-by-typing state without delay
  - Made some changes to the column collection editor to try and avoid the multiple column generation problem.
  - Column collection editor now shows the aspect name as well as the column name
  - Updated some documentation

2008-12-07 21:32 (#340) - Demo/MainForm.cs
  - Simplified initialization code

2008-12-07 20:37 (#339) - ObjectListView/VirtualObjectListView.cs
  - Trigger Before/AfterSearching events

2008-12-07 20:36 (#338) - ObjectListView/TreeListView.cs
  - Search-by-typing now works

2008-12-07 20:36 (#337) - ObjectListView/ObjectListView.cs
  - Search-by-typing now works when showing groups
  - Added BeforeSearching and AfterSearching events which are triggered when the user types into the list.
  - Added secondary sort information to Before/AfterSorting events
  - Reorganized group sorting code. Now triggers Sorting events.
  - Added GetItemIndexInDisplayOrder()
  - Tweaked in the interaction of the column editor with the IDE so that we (normally) don't rely on a hack to find the owning ObjectListView
  - Changed all 'DefaultValue(typeof(Color), "Empty")' to 'DefaultValue(typeof(Color), "")' since the first does not given Color.Empty as I thought, but the second does.

2008-12-07 20:34 (#335) - ObjectListView/Events.cs
  - Added BeforeSearching and AfterSearching events



v2.0 - 30 November 2008
-----------------------

2008-11-29 09:43 (#330) - Demo/MainForm.Designer.cs, Demo/MainForm.cs
  - Simplified Simple Tab by using CheckedAspectName

2008-11-29 09:43 (#329) - Tests/TestCheckBoxes.cs
  - Added tests for CheckedAspectName

2008-11-29 09:42 (#327) - ObjectListView/ObjectListView.cs
  - Added CheckedAspectName to simplify CheckBox handling
  - In the IDE, all ObjectListView behaviours now appear in a "Behavior - ObjectListView" category,

2008-11-29 09:41 (#326) - ObjectListView/HeaderControl.cs
  - Simplified implementation

2008-11-29 09:41 (#325) - ObjectListView/Events.cs
  - In the IDE, all ObjectListView behaviours now appear in a "Behavior - ObjectListView" category,

2008-11-29 08:38 (#324) - Demo/MainForm.Designer.cs, Demo/MainForm.cs, Demo/MainForm.resx
  - Added code that shows tooltips and custom selection colors

2008-11-29 08:36 (#323) - Tests/Program.cs, Tests/TestColumn.cs, Tests/TestSorting.cs
  - Added tests for updating values via OLVColumn

2008-11-28 18:26 (#322) - ObjectListView/Munger.cs, ObjectListView/ObjectListView.cs, ObjectListView/ObjectListView.csproj
  - Broke Reflection mechanism into Munger class

2008-11-28 13:54 (#320) - ObjectListView/NativeMethods.cs
  - Added WINDOWPOS
  - Remove several unused methods and corrected some return types [FXCOP]

2008-11-28 13:53 (#319) - ObjectListView/HeaderControl.cs
  - Made HeaderControl disposable [FXCCOP]
  - Changed several GetXXX() methods to properties [FXCCOP]

2008-11-28 13:51 (#318) - ObjectListView/Comparers.cs
  - Removed some redundant casts
  - Added StringComparison.CurrentCultureIgnoreCase to several string comparisons

2008-11-28 13:50 (#317) - ObjectListView/ObjectListView.cs
  - Fixed long standing bug with horizontal scrollbar when shrinking the window (thanks to Bartosz Borowik)
  - Fixed some more redundant casts [FXCOP]

2008-11-27 15:56 (#315) - ObjectListView/TreeListView.cs
  - Corrected calculation of expand/collapse icon (SF#2338819)
  - Fixed ugliness with dotted lines in renderer (SF#2332889)
  - Fixed problem with custom selection colors (SF#2338805)
  - Don't autoexpand branches when they are refreshed

2008-11-27 15:53 (#314) - ObjectListView/TypedObjectListView.cs
  - Added tool tip getting properties

2008-11-26 23:19 (#313) - ObjectListView2008.sln
  - Added VS 2008 format solution

2008-11-25 23:42 (#312) - ObjectListView/ObjectListView.cs, ObjectListView/ObjectListView.csproj, ObjectListView/HeaderControl.cs, ObjectListView/NativeMethods.cs
  - Added support for cell and header tool tips
  - Delay making the HeaderControl until after the ObjectListView is completely created
  - Moved comparers to Comparers.cs

2008-11-25 23:39 (#311) - ObjectListView/Comparers.cs
  - Collected all Comparers

2008-11-25 23:39 (#310) - ObjectListView/FastObjectListView.cs
  - Moved ModelObjectComparer to Comparers.cs file

2008-11-25 16:47 (#309) - ObjectListView/ObjectListView.cs, ObjectListView/NativeMethods.cs
  - First take at custom tooltips for headers and cells

2008-11-23 20:21 (#308) - ObjectListView/ObjectListView.cs
  - Preserve selection on virtual lists when sorting

2008-11-23 20:20 (#307) - ObjectListView/VirtualObjectListView.cs
  - Maintain sort order after adding objects
  - Changed column header click handling since ObjectListView now preserves selection when sorting

2008-11-23 20:18 (#306) - ObjectListView/Properties/AssemblyInfo.cs
  - Changed version number to 2.0.x

2008-11-23 20:18 (#305) - Tests/TestSorting.cs
  - Added tests for sorting events
  - Added tests for preserving selection

2008-11-23 20:17 (#304) - ListViewPrinterDemo/Form1.Designer.cs, ListViewPrinterDemo/Form1.cs
  - Made compatible with ListViewPrinter v2.0

2008-11-23 20:17 (#303) - ListViewPrinter/ListViewPrinter.csproj, ListViewPrinter/Properties/AssemblyInfo.cs, ListViewPrinter/ListViewPrinter.cs
  - Added more compatibility methods/Properties
  - Changed version number

2008-11-22 14:00 (#301) - Demo/MainForm.Designer.cs
  - Added Refresh Selected button to treeListView tab

2008-11-22 13:59 (#300) - Tests/MainForm.Designer.cs, Tests/Program.cs, Tests/Tests.csproj, Tests/TestSorting.cs, Tests/Person.cs
  - Added sorting tests

2008-11-22 13:58 (#299) - ObjectListView/ObjectListView.cs
  - Fixed bug where enabling grouping when there was not a sort column would not produce a grouped list. Grouping column now defaults to column 0.
  - Added ability to search by sort column to ObjectListView. Unified this with ability that was already in VirtualObjectListView
  - Objects property now always returns the objects of a control, even in virtual mode
  - Made ColumnComparer public so it can be used elsewhere

2008-11-22 13:54 (#298) - ObjectListView/NativeMethods.cs
  - Added search-by-typing structures NMLVFINDITEM, LVFINDITEM

2008-11-22 13:53 (#297) - ObjectListView/Events.cs
  - Moved ColumnRightClick event to here

2008-11-22 13:53 (#296) - ObjectListView/VirtualObjectListView.cs
  - Moved  IsSearchOnSortColumn to base class
  - Unified search-by-typing with ObjectListView

2008-11-20 00:01 (#295) - ObjectListView.sln
  - Added tests project to solution

2008-11-20 00:01 (#294) - Demo/MainForm.Designer.cs, Demo/MainForm.cs, Demo/MainForm.resx
  - Cleaned up tree list view demo a little

2008-11-20 00:00 (#293) - Tests/TestTreeView.cs
  - Test that selection is preserved when expanding or collapsing

2008-11-19 23:59 (#292) - ObjectListView/ObjectListView.cs
  - Fixed bug in ChangeToFilteredColumns() where DisplayOrder was not always restored correctly

2008-11-19 23:58 (#291) - ObjectListView/VirtualObjectListView.cs
  - Fixed some caching issues
  - Check upper bound on item index when selecting objects

2008-11-19 23:56 (#290) - ObjectListView/TreeListView.cs
  - AddObjects() and RemoveObjects() now operate on the root collection
  - Expand/collapse now preserve the selection -- more or less :)
  - Overrode RefreshObjects() to rebuild the given objects and their children

2008-11-18 13:19 (#280) - ListViewPrinterDemo/ListViewPrinterDemo.csproj, , Tests/TestTreeView.cs, ListViewPrinter/BrushForm.resx, Tests/AssemblyInfo.cs, ObjectListView/TypedObjectListView.cs, ObjectListView/VirtualObjectListView.cs, ListViewPrinter/Properties, ListViewPrinterDemo/Form1.cs, ObjectListView/ObjectListView.FxCop, ObjectListView/NativeMethods.cs, ListViewPrinterDemo/Properties, ObjectListView/FastObjectListView.cs, Demo/MainForm.resx, Tests/Program.cs, ObjectListView.shfb, Tests/TestCheckBoxes.cs, ListViewPrinter/BrushForm.cs, Demo/ObjectListViewDemo.csproj, ObjectListView/TreeListView.cs, ListViewPrinter/ListViewPrinter.cs, ObjectListView/Properties, Tests/MainForm.resx, ListViewPrinter/BrushPen.DesignTime.cs, ObjectListView/CustomDictionary.xml, ListViewPrinter/BrushPenData.cs, ObjectListView/ObjectListView.cs, Demo/MainForm.Designer.cs, Demo/Resource1.Designer.cs, Demo/MainForm.cs, ObjectListView/DataListView.cs, Tests/TestColumn.cs, Tests/Tests.csproj, ObjectListView/Events.cs, ListViewPrinter/RuntimePropertiesObject.cs, ListViewPrinterDemo/Form1.Designer.cs, ListViewPrinterDemo/Persons.xml, ObjectListView/CellEditors.cs, Tests/TestBasics.cs, ObjectListView/ObjectListView.shfb, Tests/Person.cs, Tests/MainForm.Designer.cs, Tests/MainForm.cs, Demo/ColumnSelectionForm.cs, Tests/SetupTestSuite.cs, Tests/OLVTests.nunit, ListViewPrinter/ListViewPrinter.csproj, ListViewPrinterDemo/Form1.resx, ListViewPrinter/BrushForm.Designer.cs, ObjectListView/ObjectListView.csproj, ListViewPrinterDemo/Resources, ObjectListView/Renderers.cs, ListViewPrinterDemo/Program.cs, Tests/TestSelection.cs, Demo/ShellUtilities.cs
  - Changed project structure for v2.0

2008-09-16 22:08 (#188) - ObjectListViewDemo.csproj, TypedObjectListView.cs
  - Added first take at strongly typed wrapper for OLV

2008-09-16 22:07 (#187) - MainForm.cs
  - First attempt at using strongly typed wrapper on OLV

2008-09-16 22:07 (#186) - ObjectListView.cs
  - If LastSortOrder is None when adding objects, don't force a resort.
  - Catch and ignore some problems with setting TopIndex on FastObjectListViews.
  - Sort columns by display order, rather than alphabetically



v1.13 - 24 July 2008
--------------------

2008-07-23 17:29 (#183) - MainForm.Designer.cs, MainForm.cs
  - Corrected small bug in BusinessCardRenderer

2008-07-23 17:21 (#182) - ObjectListView.cs
  - Consistently use copy-on-write semantics with Add/RemoveObject methods

2008-07-11 08:35 (#181) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added code for cell validating on complex tab

2008-07-11 08:34 (#180) - ObjectListView.cs
  - Added LastSortColumn and LastSortOrder properties
  - Made SORT_INDICATOR_UP_KEY and SORT_INDICATOR_DOWN_KEY public

2008-07-11 01:05 (#179) - ObjectListView.cs
  - Enable validation on cell editors through a CellEditValidating event.

2008-07-09 19:48 (#178) - ObjectListView.cs
  - Added HeaderControl.Handle property

2008-06-24 00:10 (#177) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added "Lock Groups" checkbox

2008-06-24 00:08 (#176) - ObjectListView.cs
  - Broke the more generally useful CopyObjectsToClipboard() method out of CopySelectionToClipboard()

2008-06-23 22:02 (#175) - ObjectListView.cs
  - Allow check boxes on FastObjectListViews
  - Added AlwaysGroupByColumn and AlwaysGroupBySortOrder
  - Don't do our context menu processing when in design mode
  - Separate showing and building our context menu so that the building can be used externally

2008-06-07 19:43 (#174) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - First take at maing checkboxes work on virtual lists
  - Added RefreshItem() to virtua list

2008-06-02 19:01 (#173) - ObjectListView.cs
  - Corrected bug when setting SelectedIndex
  - Optimized getters for DataListView

2008-05-11 23:14 (#172) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Enable custom selection colors on data list view

2008-05-11 23:10 (#171) - ObjectListView.cs
  - Allow selection foreground and background colors to be changed.



v1.12 - 10 May 2008
-------------------

2008-05-09 11:04 (#169) - ObjectListView.cs
  - v1.12 released

2008-05-09 10:17 (#168) - ObjectListView.cs
  - Made the ObjectsAsList property protected
  - Placed UpdateSpaceFillingColumnsWhenDraggingColumnDivider into Behavior category

2008-05-09 09:26 (#167) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Changed to use OptionalRender method in BusinessCardRenderer

2008-05-08 16:04 (#166) - ObjectListView.cs
  - Changed RenderWithDefault to OptionalRender
  - Reversed sense of boolean returned from OptionalRender

2008-05-07 23:54 (#165) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added context menu to Simple list to test that it works
  - Changed BusinessCardRenderer a little

2008-05-07 23:53 (#164) - ObjectListView.cs
  - Column selection context menu now appears even when the ObjectListView has it's own context menu installed.
  - Fix bug with owner drawing of non-detaila view.

2008-05-06 00:09 (#163) - Photos/mb.png, Photos/ns.png, Photos/cp.png, Photos/sp.png, Photos/cr.png, Photos/gp.png, Photos/es.png, Photos/gab.png, Photos/jp.png, Photos/ak.png, Photos/jr.png, Photos/sj.png
  - Smaller images please

2008-05-05 23:58 (#162) - ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Non detail views can now be owner drawn. The renderer installed for primary column is given the chance to render the whole item.
  - BREAKING CHANGE: RenderDelegate now returns a bool to indicate if default rendering should be done. Previously returned void.
  - Added BusinessCardRenderer to Complex tab as an example of owner drawing in Tile view

2008-05-05 23:46 (#161) - Photos/mb.png, Photos/ns.png, Photos/cp.png, Photos/sp.png, Photos/cr.png, Photos/gp.png, Photos/es.png, Photos/gab.png, Photos/jp.png, Photos/ak.png, Photos/jr.png, Photos/sj.png, Photos
  - Photos to demonstrate BusinessCardRenderer

2008-05-04 22:08 (#160) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added code to test AddObjects()/RemoveObjects()

2008-05-04 22:08 (#159) - ObjectListView.cs
  - Made AddObjects() and RemoveObjects() work for all flavours (or at least not crash)
  - Changed cell editing to use values directly when the values are Strings. Previously, values were always handed to the AspectToStringConverter.
  - When editing a cell, tabbing no longer tries to edit the next subitem when not in details view!
  - MappedImageRenderer can now handle a Aspects that return a collection of values. Each value will be drawn as its own image.
  - Fixed bug with clearing virtual lists that has been scrolled vertically
  - Made TopItemIndex work with virtual lists.

2008-05-04 22:06 (#158) - ListViewPrinter.cs, ShellUtilities.cs, COPYING, ColumnSelectionForm.cs
  - Made sure that all public and protected methods have at least some form of comment

2008-05-02 00:19 (#157) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added examples of using AddObjects() and RemoveObjects()

2008-05-02 00:18 (#156) - ObjectListView.cs
  - Added AddObjects() and RemoveObjects() to allow faster mods to the list
  - Reorganised public properties. Now alphabetical.
  - Made the class ObjectListViewState internal, as it always should have been.

2008-05-01 14:16 (#155) - ObjectListView.cs
  - Added GPLv3 text

2008-05-01 13:48 (#153) - ObjectListView.cs
  - Updated documentation on CustomSorter property

2008-05-01 09:06 (#152) - ListViewPrinter.cs, ObjectListView.shfb, ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.cs
  - Minor formatting and documentation changes

2008-04-30 08:56 (#151) - ObjectListView.cs, MainForm.resx
  - Preserve scroll position when building the list or changing columns.
  - Added TopItemIndex property. Due to problems with the underlying control, this property is not always reliable.

2008-04-28 22:43 (#150) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs
  - Added ColumnRightClick event.
  - Made the assembly CLS-compliant.
  - Added SelectedIndex property
  - Made all NativeMethods 64-bit correct

2008-04-13 14:21 (#149) - ListViewPrinter.cs, AssemblyInfo.cs, ObjectListView.cs
  - Made CLS compliant

2008-04-12 23:04 (#148) - ObjectListView.cs
  - Remove unwanted WriteLine's

2008-04-12 23:02 (#147) - ObjectListView.cs
  - Changed HandleHeaderRightClick() to have a columnIndex parameter, which tells which column was right-clicked



v1.11 - 11 April 2008
---------------------

2008-04-10 08:50 (#146) - ObjectListView.cs
  - Minor code improvements

2008-04-01 23:05 (#145) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added code to test SaveState() and RestoreState()

2008-04-01 23:05 (#144) - ObjectListView.cs
  - Added SaveState() and RestoreState()
  - When cell editing, scrolling with a mouse wheel now ends the edit operation.

2008-03-26 23:43 (#142) - ObjectListView.shfb, ListViewPrinter.cs, ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Merged Mono changes back into trunk
  - Fixed a funny interaction between cell editing and space filling columns

2008-03-26 22:31 (#141) - ObjectListView.cs
  - Update some methods and docs that I missed last time about changing proportional to space filling

2008-03-26 00:15 (#139) - ObjectListView.shfb, ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Add space filling columns
  - Remove most <code></code> brackets from docs because that tag doesn't do what I thought.



v1.10 - 19 March 2008
---------------------

2008-03-16 21:35 (#137) - AssemblyInfo.cs
  - Changed version number

2008-03-16 21:34 (#136) - ObjectListView.cs
  - Made some more methods thread safe.
  - Added some methods suggested by Chris Marlowe (thanks for the suggestions Chris)
  - - ClearObjects()
  - - GetCheckedObject(), GetCheckedObjects()
  - - GetItemAt() variation that gets both the item and the column under a point

2008-03-12 10:40 (#135) - MainForm.resx, MainForm.Designer.cs
  - Before Mono migration

2008-03-12 10:39 (#134) - ObjectListView.cs
  - Added CorrectSubItemBackColors()

2008-02-03 10:30 (#132) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - v1.9.1

2008-02-03 10:30 (#131) - ObjectListView.cs
  - Fixed bug that caused owner-drawn virtual lists to use 100% CPU
  - Added FlagRenderer to help draw bitwise-OR'ed flag values
  - Fixed bug (introduced in v1.9) that made alternate row colour with groups not quite right
  - Ensure that DesignerSerializationVisibility.Hidden is set on all non-browsable properties
  - Make sure that sort indicators are shown after changing which columns are visible
  - Added FastObjectListView



v1.9.1 - 02 February 2008
-------------------------

2008-01-19 20:41 (#129) - ObjectListView.cs
  - v1.9.0.2 but released to CodeProject as v1.9

2008-01-19 20:37 (#128) - ObjectListView.cs
  - v1.9.0.1 but was released to CodeProject as v1.9

2008-01-19 11:24 (#127) - ObjectListView.cs, ObjectListViewDemo.csproj, ShellUtilities.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - v1.9

2008-01-18 00:04 (#125) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - First take at IncrementalUpdate()

2008-01-17 00:33 (#124) - ListViewPrinter.cs, MainForm.resx, ColumnSelectionForm.cs, MainForm.Designer.cs, MainForm.cs
  - Candidate for v1.9

2008-01-17 00:33 (#123) - ObjectListView.cs
  - Added right click on columns to choose columns
  - Added ImagesRenderer
  - Batch the adding of list items (15% faster)
  - Redraw the control EmptyListMsg changes
  - Made RefreshObject/s thread safe



v1.9 - 16 January 2008
----------------------

2007-12-13 23:57 (#121) - ColumnSelectionForm.Designer.cs, ColumnSelectionForm.cs
  - Use new check box support in ObjectListView

2007-12-13 23:57 (#120) - ObjectListView.cs
  - Support for check boxes
  - Cleanup some column hiding code

2007-12-11 23:24 (#119) - ColumnSelectionForm.resx, ColumnSelectionForm.Designer.cs, ColumnSelectionForm.cs
  - Allow user to select which columns are visible, and in which order they should be displayed

2007-12-11 23:23 (#118) - ObjectListView.cs, ObjectListViewDemo.csproj
  - Added ability to make some columns hidden
  - Made ObjectListView and OLVColumn both partial classes

2007-12-11 23:15 (#117) - MainForm.Designer.cs, MainForm.cs
  - Added column selection button



v1.8 - 30 November 2007
-----------------------

2007-11-30 19:21 (#113) - ObjectListView.cs
  - Trigger CellEditFinishing when the user cancels editing
  - Correctly calculate the background color of a cell when the listview doesn't have the focus

2007-11-30 19:19 (#112) - MainForm.Designer.cs, MainForm.cs
  - Set correct tab order on all pages

2007-11-29 21:32 (#110) - ObjectListView.cs
  - Allow renderers to wrap text (only used when printing)

2007-11-29 21:31 (#109) - ListViewPrinter.cs
  - Made list cells able to wrap
  - Handle items having less subitems than there are columns

2007-11-29 10:45 (#108) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Cell editing complete
  - Handle RTL layout

2007-11-25 14:44 (#107) - MainForm.Designer.cs, MainForm.cs
  - Allow user to control the editability of the list views

2007-11-25 14:44 (#106) - ObjectListView.cs
  - Intercept Enter and Escape rather than relying on key events, since some controls (like ComboBox) don't trigger them.
  - Refactored some code and improved some comments

2007-11-24 14:21 (#102) - MainForm.cs
  - Test out cell editing events
  - Added some AspectPutters
  - Added CanTellJokes boolean field to test handling of booleans

2007-11-24 14:19 (#101) - ObjectListView.cs
  - Added SelectionChanged event
  - Added GetItem() and GetItemCount() and used everywhere to help compatibility with virtual list
  - Added GetModelObject()
  - Documented cell editing methods
  - Use AutoCompleteCellEditor as default cell editor
  - Put cell editing events into "Behavior" category

2007-11-24 14:14 (#100) - ListViewPrinter.cs
  - Fixed bug where icon was overdrawn by background

2007-11-21 09:40 (#97) - ObjectListView.cs
  - Cell editing working. Still needs docs

2007-11-21 09:38 (#96) - ListViewPrinter.cs
  - Changed to use DefaultValue(typeof(Color), "Empty")

2007-11-17 15:14 (#94) - ObjectListView.cs
  - Don't sort group items if the lastSortOrder is None

2007-11-13 13:30 (#93) - ObjectListView.cs
  - Correctly draw background of text of selected item
  - Fixed interaction between ListViewPrinter and owner-drawn mode

2007-11-13 13:27 (#91) - ListViewPrinter.cs
  - Fixed bug with page handling
  - Fixed some problem with text formatting

2007-11-10 16:16 (#89) - MainForm.cs
  - Refresh the print preview when we switch to that tab
  - Warn when trying to print the virtual list

2007-11-10 16:15 (#88) - Persons.xml
  - Added lots more people

2007-11-10 16:14 (#87) - ObjectListView.cs
  - Handle an image selector of an empty string

2007-11-10 16:14 (#86) - ListViewPrinter.cs
  - Added ability to print list header on top of each page

2007-11-09 11:41 (#82) - ObjectListView.cs
  - Pin column width to valid value when changing min or max values

2007-11-09 11:41 (#81) - ListViewPrinter.cs, MainForm.Designer.cs, MainForm.cs
  - Added support for virtual lists

2007-11-08 21:51 (#79) - MainForm.cs
  - Changed to use Pens for BlockFOrmat

2007-11-08 21:50 (#78) - ListViewPrinter.cs
  - Changed to use Pen internally
  - Lots of other cleanups

2007-11-07 14:50 (#77) - ListViewPrinter.cs, ObjectListView.cs, MainForm.Designer.cs, MainForm.cs
  - Use BlockFormat instance for cells
  - Properly filling row background
  - Corrected miscalculations with borders and text insets

2007-11-05 21:30 (#75) - ListViewPrinter.cs, ObjectListViewDemo.csproj, ShellUtilities.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - v1.7 release

2007-11-04 22:15 (#73) - ListViewPrinter.cs, MainForm.Designer.cs, MainForm.cs
  - Refactored all formatting

2007-11-03 20:21 (#72) - ListViewPrinter.cs
  - First take a group printing

2007-11-03 14:18 (#71) - ListViewPrinter.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - ListViewPrinter and example v1.0 complete!

2007-11-03 08:52 (#70) - ListViewPrinter.cs, ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - ListViewPrinter more or less complete

2007-11-01 21:25 (#69) - ListViewPrinter.cs
  - First basic working version

2007-10-31 19:05 (#68) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs
  - Tidy up prior to v1.6 release

2007-10-31 08:36 (#67) - ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Version 1.6b almost ready

2007-10-30 22:59 (#66) - ObjectListView.cs
  - Improved DataListView

2007-10-17 22:20 (#64) - oject/ObjectListView.html, oject/ObjectListViewDemo.zip, ObjectListViewDemo.csproj, oject/ObjectListView.zip
  - v1.6 release

2007-10-17 22:08 (#63) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Get ready for 1.6 release

2007-10-06 19:27 (#62) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Give an example of a custom sorter on virtual lists
  - Optimise value getters

2007-10-06 19:18 (#61) - ObjectListView.cs
  - Optimized aspect-to-string conversion. BuildList() 15% faster.
  - Added empty implementation of RefreshObjects() to VirtualObjectListView since
  - RefreshObjects() cannot work on virtual lists.
  - Corrected bug with custom sorter in VirtualObjectListView
  - Corrected image scaling bug in DrawAlignedImage()
  - Allow item count labels on groups to be set per column



v1.6 - 30 September 2007
------------------------

2007-08-20 22:29 (#59) - ObjectListView.cs, MainForm.cs
  - Massive rework of DataListView to make it truly bindable.

2007-08-14 22:31 (#58) - ObjectListView.cs
  - Sync with changes from cmarlow

2007-08-11 11:25 (#50) - ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs
  - Added List Empty msg capability



v1.5 - 03 August 2007
---------------------

2007-08-02 23:07 (#48) - ObjectListViewDemo.sln, ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - v1.5 Release

2007-08-02 23:06 (#47) - AnimatedGifRenderer.cs
  - Last use before being replaced

2007-07-31 14:45 (#43) - AnimatedGifRenderer.cs
  - Change to use Image rather than GifDecoder

2007-07-30 21:07 (#42) - ObjectListViewDemo.sln, ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, Persons.xml, MainForm.Designer.cs, MainForm.cs
  - RowHeight now works
  - AnimatedGifs work - Mark I

2007-07-28 00:10 (#41) - AnimatedGifs/constrct.gif, AnimatedGifs/enter3.gif, GifDecoder.cs, AnimatedGifs/eye~1.gif, AnimatedGifs/free1.gif, ObjectListView.cs, ObjectListViewDemo.csproj, AnimatedGifs/eye2.gif, AnimatedGifRenderer.cs, AnimatedGifs/3dlink1.gif, AnimatedGifs/floppydisk2.gif, AnimatedGifs/cd1.gif, AnimatedGifs/email1.gif, AnimatedGifs/handright.gif, AnimatedGifs/net2.gif, AnimatedGifs/clickhere1.gif, AnimatedGifs/hot1.gif, MainForm.Designer.cs, AnimatedGifs/envelope.gif, AnimatedGifs/exclame.gif, AnimatedGifs/new5.gif, AnimatedGifs/email8.gif, AnimatedGifs/cool3.gif, AnimatedGifs/laptop1.gif, AnimatedGifs/circum.gif, AnimatedGifs/handleft.gif, MainForm.resx, Persons.xml, AnimatedGifs
  - Gif animation now works reasonably well

2007-07-27 10:46 (#40) - ObjectListView.cs, ObjectListViewDemo.csproj, AnimatedGifRenderer.cs, ShellUtilities.cs, MainForm.resx, Persons.xml, MainForm.Designer.cs, MainForm.cs
  - AnimatedGifRenderer mark I working

2007-05-24 09:46 (#38) - ShellUtilities.cs
  - Changed documentation

2007-05-24 09:45 (#37) - ObjectListView.cs
  - Changed "ListViewNative" to "NativeMethods"
  - Made OLVColumn.GetImage() pay attention to ImageIndex and ImageKey properties
  - Improved some documentation

2007-05-03 23:07 (#34) - ObjectListView.cs
  - Freeze control while switching views
  - Handle a null binding source
  - Use SystemColors rather than caching FromKnownColor results

2007-05-03 23:05 (#33) - ObjectListViewDemo.csproj
  - Added ShellUtilities.cs

2007-05-03 23:05 (#32) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added FileExplorer tab
  - Cleanup event handlers

2007-05-01 16:27 (#31) - ObjectListView.cs
  - Sort by column rather than by index (still not sure about this one!)
  - Swap columns when using tile view

2007-05-01 16:20 (#30) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Add view comboboxes and owner draw check boxes



v1.4 - 30 April 2007
--------------------

2007-04-21 10:19 (#29) - ObjectListView.cs, MainForm.resx, Persons.xml, MainForm.Designer.cs, MainForm.cs
  - Made OwnerDraw optional.
  - Added list sort indicators on columns
  - Moved all native calls to their own class

2007-04-17 23:30 (#26) - ObjectListView.cs, ObjectListViewDemo.csproj, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Owner drawing almost complete

2007-04-09 23:49 (#24) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Release 1.3

2007-04-07 09:58 (#22) - ObjectListView.cs
  - First attempt to generalise DataListView
  - Added more method comments

2007-04-06 07:00 (#21) - ObjectListView.cs
  - Added DataTableListView and VirtualObjectListView
  - Added CustomSorter property
  - Massively simplified sorting strategy
  - Separated all owner-drawing code -- until complete
  - Improved comments

2007-04-06 06:56 (#20) - Persons.xml
  - Added some more people to the list

2007-04-06 06:56 (#19) - ObjectListViewDemo.sln, ObjectListViewDemo.csproj
  - v1.3 release

2007-04-06 06:55 (#18) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Changed DataSet example to use DataTableListView
  - Removed some unwanted code



Previous versions - 04 April 2007
---------------------------------

2007-01-17 15:01 (#17) - MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added virtual list example
  - Added selection status message

2007-01-17 14:55 (#16) - ObjectListView.cs
  - Added VirtualObjectListView class
  - Big change: Owner draw list
  - Removed all Windows specific code
  - Fixed bug with Sorting variable

2007-01-06 23:13 (#12) - oject/ObjectListView.html, oject/ObjectListViewDemo.zip, ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, oject/ObjectListView.zip, MainForm.cs
  - v1.2 alternate line colouring, speed improvements

2007-01-05 22:34 (#11) - ObjectListView.cs
  - Clear the sorter before rebuilding the list. 10x faster!
  - Include fields in InvokeMember() options

2006-12-20 00:14 (#10) - ObjectListView.cs
  - Changed default AlternateRowBackColor

2006-11-09 15:35 (#9) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added alternate row back colouring

2006-11-02 00:28 (#8) - ObjectListView.cs, MainForm.resx, MainForm.Designer.cs, MainForm.cs
  - Added alternate row colouring

2006-10-26 16:38 (#6) - ObjectListView.cs
  - Added object level manipulation methods
  - Shadowed Columns property

2006-10-26 16:37 (#5) - MainForm.Designer.cs, MainForm.cs
  - Use new object level manipulation methods

