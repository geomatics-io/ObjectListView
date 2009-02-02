.. -*- coding: UTF-8 -*-

:Subtitle: Recent improvements in loving the ListView

What's New?
===========

For the (mostly) complete change log, :ref:`see here <changelog>`.

3 February 2009 - Version 2.1
-----------------------------

Complete overhaul of owner drawing
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

In the same way that 2.0 overhauled the virtual list processing, this version
completely reworks the owner drawn rendering process. However, this overhaul
was done to be transparently backwards compatible.

The only breaking change is for owner drawn non-details views (which I doubt
that anyone except me ever used). Previously, the renderer on column 0 was
double tasked for both rendering cell 0 and for rendering the entire item in
non-detail view. This second responsibility now belongs explicitly to the
`ItemRenderer` property.

* Renderers are now based on `IRenderer` interface.
* Renderers are now Components and can be created, configured, and assigned within the IDE.
* Renderers can now also do hit testing.
* Owner draw text now looks like native ListView
* The text AND bitmaps now follow the alignment of the column. Previously only the
  text was aligned.
* Added `ItemRenderer` to handle non-details owner drawing
* Images are now drawn directly from the image list if possible. 30% faster than previous versions.

Other significant changes
^^^^^^^^^^^^^^^^^^^^^^^^^

* Added hot tracking
* Added checkboxes to subitems
* AspectNames can now be used as indexes onto the model objects -- effectively
  something like this: `modelObject[this.AspectName]`.   This is particularly
  helpful for `DataListView` since `DataRows` and `DataRowViews` support this type of
  indexing.
* Added `EditorRegistry` to make it easier to change or add cell editors

Minor Changes
^^^^^^^^^^^^^

* Added `TriStateCheckBoxes`, `UseCustomSelectionColors` and `UseHotItem` properties
* Added `TreeListView.RevealAfterExpand` property
* Enums are now edited by a ComboBox that shows all the possible values.
* Changed model comparisons to use `Equals()` rather than `==`. This allows the model objects to
  implement their own idea of equality.
* `ImageRenderer` can now handle multiple images. This makes `ImagesRenderer` defunct.
* `FlagsRenderer<T>` is no longer generic. It is simply `FlagsRenderer`.

Bug fixes
^^^^^^^^^

* `RefreshItem()` now correctly recalculates the background color
* Fixed bug with simple checkboxes which meant that `CheckedObjects` always returned empty.
* `TreeListView` now works when visual styles are disabled
* `DataListView` now handles boolean types better. It also now longer crashes when the data source
  is reseated.
* Fixed bug with `AlwaysGroupByColumn` where column header clicks would not resort groups.

10 January 2009 - Version 2.0.1
-------------------------------

This version adds some small features and fixes some bugs in 2.0 release.

New or changed features
^^^^^^^^^^^^^^^^^^^^^^^

* Added `ObjectListView.EnsureGroupVisible()`
* Added `TreeView.UseWaitCursorWhenExpanding` property
* Made all public and protected methods virtual so they can be overridden in subclasses. Within `TreeListView`, some classes were changed from internal to protected so that they can be accessed by subclasses
* Made `TreeRenderer` public so that it can be subclassed
* `ObjectListView.FinishCellEditing()`, `ObjectListView.PossibleFinishCellEditing()` and `ObjectListView.CancelCellEditing()` are now public
* Added `TreeRenderer.LinePen` property to allow the connection drawing pen to be changed

Bug fixes
^^^^^^^^^

* Fixed long-standing "multiple columns generated" problem. Thanks to pinkjones for his help with solving this one!
* Fixed connection line problem when there is only a single root on a `TreeListView`
* Owner drawn text is now rendered correctly when `HideSelection` is true.
* Fixed some rendering issues where the text highlight rect was miscalculated
* Fixed bug with group comparisons when a group key was null
* Fixed bug with space filling columns and layout events
* Fixed `RowHeight` so that it only changes the row height, not the width of the images.
* `TreeListView` now works even when it doesn't have a `SmallImageList`

30 November 2008 - Version 2.0
------------------------------

Version 2.0 is a major change to ObjectListView.

Major changes
^^^^^^^^^^^^^

* Added `TreeListView` which combines a tree structure with the columns on a `ListView`.
* Added `TypedObjectListView` which is a type-safe wrapper around an `ObjectListView`.
* Major overhaul of `VirtualObjectListView` to now use `IVirtualListDataSource`. The new version of `FastObjectListView` and the new `TreeListView` both make use of this new structure.
* `ObjectListView` builds to a DLL, which can then be incorporated into your .NET project. This makes it much easier to use from other .NET languages (including VB).
* Large improvement in `ListViewPrinter's` interaction with the IDE. All `Pens` and `Brushes` can now be specified through the IDE.
* Support for tri-state checkboxes, even for virtual lists.
* Support for dynamic tool tips for cells and column headers, via the `CellToolTipGetter` and `HeaderToolTipGetter` delegates respectively.
* Fissioned ObjectListView.cs into several files, which will hopefully makes the code easier to approach.
* Added many new events, including `BeforeSorting` and `AfterSorting`.
* Generate dynamic methods from AspectNames using `TypedObjectListView.GenerateAspectGetters()`. The speed of hand-written AspectGetters without the hand-written-ness. This is the most experimental part of the release. Thanks to Craig Neuwirt for his initial implementation.

Minor changes
^^^^^^^^^^^^^

* Added `CheckedAspectName` to allow check boxes to be gotten and set without requiring any code.
* Typing into a list now searches values in the sort column by default, even on plain vanilla `ObjectListViews`. The behavior was previously on available on virtual lists, and was turned off by default. Set `IsSearchOnSortColumn` to false to revert to v1.x behavior.
* Owner drawn primary columns now render checkboxes correctly (previously checkboxes were not drawn, even when `CheckBoxes` property was true).

Breaking changes
^^^^^^^^^^^^^^^^

* `CheckStateGetter` and `CheckStatePutter` now use only `CheckState`, rather than using both `CheckState` and `booleans`. Use `BooleanCheckStateGetter` and `BooleanCheckStatePutter` for behavior that is compatible with v1.x.
* `FastObjectListViews` can no longer have a `CustomSorter`. In v1.x it was possible, if tricky, to get a `CustomSorter` to work with a `FastObjectListView`, but that is no longer possible in v2.0 In v2.0, if you want to custom sort a FastObjectListView, you will have to subclass FastObjectListDataSource and override the SortObjects() method. See here for an example.

24 July 2008 - Version 1.13
---------------------------

Major changes
^^^^^^^^^^^^^

* Allow check boxes on `FastObjectListViews`. .NET's ListView cannot support
  checkboxes on virtual lists. We cannot get around this limit for plain
  `VirtualObjectListViews`, but we can for `FastObjectListViews`. This is a
  significant piece of work and there may well be bugs that I have missed. This
  implementation does not modify the traditional `CheckedIndicies`/`CheckedItems`
  properties, which will still fail. It uses the new `CheckedObjects` property as
  the way to access the checked rows. Once `CheckBoxes` is set on a
  `FastObjectListView`, trying to turn it off again will throw an exception.

* There is now a `CellEditValidating` event, which allows a cell editor to be
  validated before it loses focus. If validation fails, the cell editor will
  remain. Previous versions could not prevent the cell editor from losing focus.
  Thanks to Artiom Chilaru for the idea and the initial implementation.

* Allow selection foreground and background colors to be changed. Windows does
  not allow these colours to be customised, so we can only do these when the
  `ObjectListView` is owner drawn. To see this in action, set the
  `HighlightForegroundColor` and `HighlightBackgroundColor` properties and then set
  `UseCustomSelectionColors` to true.

* Added `AlwaysGroupByColumn` and `AlwaysGroupBySortOrder` properties, which
  force the list view to always be grouped by a particular column.

Minor improvements
^^^^^^^^^^^^^^^^^^

* Added `CheckObject()` and all its friends, as well as `CheckedObject` and `CheckedObjects` properties
* Added `LastSortColumn` and `LastSortOrder` properties.
* Made `SORT_INDICATOR_UP_KEY` and `SORT_INDICATOR_DOWN_KEY` public so they can be used to specify the image used on column headers when sorting.
* Broke the more generally useful `CopyObjectsToClipboard()` method out of `CopySelectionToClipboard()`. `CopyObjectsToClipboard()` could now be used, for example, to copy all checked objects to the clipboard.
* Similarly, building the column selection context menu was separated from showing that context menu. This is so external code can use the menu building method, and then make any modification desired before showing the menu. The building of the context menu is now handled by `MakeColumnSelectMenu()`.
* Added `RefreshItem()` to `VirtualObjectListView` so that refreshing an object actually does something.
* Consistently use copy-on-write semantics with `AddObject(s)/RemoveObject(s)` methods. Previously, if `SetObjects()` was given an `ArrayList` that list was modified directly by the Add/RemoveObject(s) methods. Now, a copy is always taken and modifying, leaving the original collection intact.

Bug fixes (not a complete list)
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

* Fixed a bug with `GetItem()` on virtual lists where the item returned was not always complete .
* Fixed a bug/limitation that prevented `ObjectListView` from responding to right clicks when it was used within a `UserControl` (thanks to Michael Coffey).
* Corrected bug where the last object in a list could not be selected via `SelectedObject`.
* Fixed bug in `GetAspectByName()` where chained aspects would crash if one of the middle aspects returned null (thanks to philippe dykmans).

10 May 2008 - Version 1.12
--------------------------

* Added `AddObject/AddObjects/RemoveObject/RemoveObjects` methods. These methods allow the programmer to add and remove specific model objects from the `ObjectListView`. These methods work on `ObjectListView` and `FastObjectListView`. They have no effect on `DataListView` and `VirtualObjectListView` since the data source of both of these is outside the control of the ObjectListView.
* Non detail views can now be owner drawn. The renderer installed for primary column is given the chance to render the whole item. See BusinessCardRenderer in the demo for an example. In the demo, go to the Complex tab, turn on Owner Drawn, and switch to Tile view to see this in action.
* BREAKING CHANGE. The signature of `RenderDelegate` has changed. It now returns a `boolean` to indicate if default rendering should be done. This delegate previously returned `void`. This is only important if your code used `RendererDelegate` directly. Renderers derived from `BaseRenderer` are unchanged.
* The `TopItemIndex` property now works with virtual lists
* `MappedImageRenderer` will now render a collection of values
* Fixed the required number of bugs:
* The column select menu will now appear when the header is right clicked even when a context menu is installed on the `ObjectListView`
* Tabbing while editing the primary column in a non-details view no longer tries to edit the new column's value
* When a virtual list that is scrolled vertically is cleared, the underlying
  `ListView` becomes confused about the scroll position, and incorrectly renders
  items after that. ObjectListView now avoids this problem.

1 May 2008 - Version 1.11
-------------------------

* Added `SaveState()` and `RestoreState()`. These methods save and restore the user modifiable state of an `ObjectListView`. They are useful for saving and restoring the state of your ObjectListView between application runs. See the demo for examples of how to use them.
* Added `ColumnRightClick` event
* Added `SelectedIndex` property
* Added `TopItemIndex` property. Due to problems with the underlying `ListView` control, this property has several quirks and limitations. See the documentation on the property itself.
* Calling `BuildList(true)` will now try to preserve scroll position as well as the selection (unfortunately, the scroll position cannot be preserved while showing groups).
* ObjectListView is now CLS-compliant
* Various bug fixes. In particular, ObjectListView should now be fully functional on 64-bit versions of Windows.

18 March 2008 - Version 1.10
----------------------------

* Added space filling columns. A space filling column fills all (or a portion) of the width unoccupied by other columns.
* Added some methods suggested by Chris Marlowe: `ClearObjects()`, `GetCheckedObject()`, `GetCheckedObjects()`, a flavour of `GetItemAt()` that returns the item and column under a point. Thanks for the suggestions, Chris.
* Added minimal support for Mono. To create a Mono version, compile with conditional compilation symbol "MONO". The Windows.Forms support under Mono is still a work in progress -- the listview still has some serious problems (I'm looking at you, virtual mode). If you do have success with Mono, I'm happy to include any fixes you might make (especially from Linux or Mac coders). Please don't ask me Mono questions.
* Fixed bug with subitem colors when using owner drawn lists and a `RowFormatter`.

2 February 2008 - Version 1.9.1
-------------------------------

* Added `FastObjectListView` for all impatient programmers.
* Added `FlagRenderer` to help with drawing bitwise-OR'ed flags (search for `FlagRenderer` in the demo project to see an example)
* Fixed the inevitable bugs that managed to appear:
* Alternate row colouring with groups was slightly off
* In some circumstances, owner drawn virtual lists would use 100% CPU
* Made sure that sort indicators are correctly shown after changing which columns are visible

16 January 2008 - Version 1.9
-----------------------------

* Added ability to have hidden columns, i.e. columns that the ObjectListView
  knows about but that are not visible to the user. This is controlled by
  `OLVColumn.IsVisible`. I added `ColumnSelectionForm` to the demo project to show
  how it could be used in an application. Also, right clicking on the column
  header will allow the user to choose which columns are visible. Set
  `SelectColumnsOnRightClick` to false to prevent this behaviour.

* Added `CopySelectionToClipboard()` which pastes a text and HTML representation
  of the selected rows onto the Clipboard. By default, this is bound to Ctrl-C.

* Added support for checkboxes via `CheckStateGetter` and `CheckStatePutter`
  properties. See `ColumnSelectionForm` for an example of how to use.

* Added `ImagesRenderer` to draw more than one image in a column.

* Made `ObjectListView` and `OLVColumn` into partial classes so that others can
  extend them.

* Added experimental `IncrementalUpdate()` method, which operates like
  `SetObjects()` but without changing the scrolling position, the selection, or
  the sort order. And it does this without a single flicker. Good for lists that
  are updated regularly. [Better to use a `FastObjectListView` and the `Objects`
  property]

* Fixed the required quota of small bugs.

30 November 2007 - Version 1.8
------------------------------

* Added cell editing -- so easy to say, so much work to do
* Added `SelectionChanged` event, which is triggered once per user action regardless of how many items are selected or deselected. In comparison, `SelectedIndexChanged` events are triggered for every item that is selected or deselected. So, if 100 items are selected, and the user clicks a different item to select just that item, 101 SelectedIndexChanged events will be triggered, but only one SelectionChanged event. Thanks to lupokehl42 for this suggestion and improvements.
* Added the ability to have secondary sort column used when the main sort column gives the same sort value for two rows. See `SecondarySortColumn` and `SecondarySortOrder` properties for details. There is no user interface for these items -- they have to be set by the programmer.
* `ObjectListView` now handles `RightToLeftLayout` correctly in owner drawn mode, for all you users of Hebrew and Arabic (still working on getting `ListViewPrinter` to work, though). Thanks for dschilo for his help and input.

13 November 2007 - Version 1.7.1
--------------------------------

* Fixed bug in owner drawn code, where the text background color of selected items was incorrectly calculated.
* Fixed buggy interaction between `ListViewPrinter` and owner drawn mode.

7 November 2007 - Version 1.7
-----------------------------

* Added ability to print `ObjectListViews` using `ListViewPrinter`.

30 October 2007 - Version 1.6
-----------------------------

Major changes
^^^^^^^^^^^^^

* Added ability to give each column a minimum and maximum width (set the minimum
  equal to the maximum to make a fixed-width column). Thanks to Andrew Philips for
  his suggestions and input.

* Complete overhaul of `DataListView` to now be a fully functional, data-
  bindable control. This is based on Ian Griffiths' excellent example, which
  should be available here__, but unfortunately seems to have disappeared from the
  Web. Thanks to ereigo for significant help with debugging this new code.

* Added the ability for the listview to display a "this list is empty"-type
  message when the ListView is empty (obviously). This is controlled by the
  `EmptyListMsg` and `EmptyListMsgFont` properties. Have a look at the "File
  Explorer" tab in the demo to see what it looks like.

.. __: http://www.interact-sw.co.uk/utilities/bindablelistview

Minor changes
^^^^^^^^^^^^^

* Added the ability to preserve the selection when `BuildList()` is called. This is on by default.
* Added the `GetNextItem()` and `GetPreviousItem()` methods, which walk sequentially through the ListView items, even when the view is grouped (thanks to eriego for the suggestion).
* Allow item count labels on groups to be set per column (thanks to cmarlow for the idea).
* Added the `SelectedItem` property and the `GetColumn()` and `GetItem()` methods.
* Optimized aspect-to-string conversion. `BuildList()` is 15% faster.
* Corrected the bug with the custom sorter in `VirtualObjectListView` (thanks to mpgjunky).
* Corrected the image scaling bug in `DrawAlignedImage()` (thanks to krita970).
* Uses built-in sort indicators on Windows XP or later (thanks to gravybod for sample implementation).
* Plus the requisite number of small bug fixes.

3 August 2007 - Version 1.5
---------------------------

* `ObjectListViews` now have a `RowFormatter` delegate. This delegate is called whenever a `ListItem` is added or refreshed. This allows the format of the item and its sub-items to be changed to suit the data being displayed, like red colour for negative numbers in an accounting package. The DataView tab in the demo has an example of a `RowFormatter` in action. Include any of these words in the value for a cell and see what happens: red, blue, green, yellow, bold, italic, underline, bk-red, bk-green. Be aware that using RowFormatter and trying to have alternate coloured backgrounds for rows can give unexpected results. In general, `RowFormatter` and `UseAlternatingBackColors` do not play well together.
* `ObjectListView` now has a `RowHeight` property. Set this to an integer value and the rows in the `ListView` will be that height. Normal `ListViews` do not allow the height of the rows to be specified; it is calculated from the size of the small image list and the ListView font. The `RowHeight` property overrules this calculation by shadowing the small image list. This feature should be considered highly experimental. One known problem is that if you change the row height while the vertical scroll bar is not at zero, the control's rendering becomes confused.
* Animated GIF support: if you give an animated GIF as an `Image` to a column that has `ImageRenderer`, the GIF will be animated. Like all renderers, this only works in `OwnerDrawn` mode. See the DataView tab in the demo for an example.
* Sort indicators can now be disabled, so you can put your own images on column headers.
* Better handling of item counts on groups that only have one member: thanks to cmarlow for the suggestion and sample implementation.
* The obligatory small bug fixes.

30 April 2007 - Version 1.4
---------------------------

* Owner drawing and renderers.
* `ObjectListView` now supports all ListView.View modes, not just Details. The tile view has its own support built in.
* Column headers now show sort indicators.
* Aspect names can be chained using a "dot" syntax. For example, Owner.Workgroup.Name is now a valid `AspectName`. Thanks to OlafD for this suggestion and a sample implementation.
* `ImageGetter` delegates can now return ints, strings or Image objects, rather than just ints as in previous versions. ints and strings are used as indices into the image lists. Images are only shown when in OwnerDrawn mode.
* Added `OLVColumn.MakeGroupies()` to simplify group partitioning.

5 April 2007 - Version 1.3
--------------------------

* Added `DataListView`.
* Added `VirtualObjectListView`.
* Added `Freeze()`/`Unfreeze()`/`Frozen` functionality.
* Added ability to hand off sorting to a `CustomSorter` delegate.
* Fixed bug in alternate line coloring with unsorted lists: thanks to cmarlow for finding this.
* Handle null conditions better, e.g. `SetObjects(null)` or having zero columns.
* Dumbed-down the sorting comparison strategy. Previous strategy was classic overkill: user extensible, handles every possible situation and unintelligible to the uninitiated. The simpler solution handles 98% of cases, is completely obvious and is implemented in 6 lines.

5 January 2007 - Version 1.2
----------------------------

* Added alternate line colors.
* Unset sorter before building list. 10x faster! Thanks to aaberg for finding this.
* Small bug fixes.

26 October 2006 - Version 1.1
-----------------------------

* Added "Data Unaware" and "IDE Integration" article sections.
* Added model-object-level manipulation methods, e.g. `SelectObject()` and `GetSelectedObjects()`.
* Improved IDE integration.
* Refactored sorting comparisons to remove a nasty if...else cascade.

14 October 2006 - Version 1.0
-----------------------------
