.. -*- coding: UTF-8 -*-

:Subtitle: Detailed records of what changed when

.. _changelog:

Change Log
==========

2008-09-04 23:12 (#250) - setup.py
  - v1.2


2008-09-04 22:30 (#249) - ObjectListView/ObjectListView.py
  - Correct an incomplete comment


2008-09-04 22:30 (#248) - Examples/SqlExample.py
  - Correctly locate primary key when there is a WHERE clause


2008-09-04 22:30 (#247) - Examples/BatchedUpdateExample.py
  - Nicely format file size column


2008-09-04 22:29 (#246) - docs/whatsnew.rst, docs/index.rst, docs/recipes.rst
  - Final changes for v1.2


2008-09-02 23:25 (#245) - docs/features.rst, docs/.static/icon.ico, docs/whatsnew.rst, docs/listCtrlPrinter.rst, docs/conf.py, docs/majorClasses.rst, docs/index.rst
  - Updated in preparation for v1.2 release


2008-09-02 23:22 (#244) - Examples/UsingVirtualListExample.py
  - Simplified initial insertions (removed executemany)


2008-09-02 23:21 (#243) - ObjectListView/__init__.py
  - Added BatchedUpdate


2008-09-02 23:20 (#242) - Examples/BatchedUpdateExample.py
  - First version


2008-09-02 23:20 (#241) - ObjectListView/ObjectListView.py
  - Added BatchedUpdate adaptor
  - Improved speed of selecting and refreshing by keeping a map of objects to indicies
  - Added GetIndexOf()
  - Removed flicker from FastObjectListView.AddObjects() and RefreshObjects()


2008-08-31 23:09 (#240) - Examples/BatchedUpdateExample.py
  - Scanning now works


2008-08-31 20:58 (#239) - Examples/BatchedUpdateExample.py
  - First hand done layout


2008-08-31 18:07 (#238) - Examples/BatchedUpdateExample.py
  - Initial checkin


2008-08-28 22:50 (#237) - CHANGELOG.txt, docs/changelog.rst
  - Rebuilt change log


2008-08-28 22:41 (#235) - test/test_ObjectListView.py
  - Added filtering tests


2008-08-28 22:41 (#234) - ObjectListView/ObjectListView.py
  - Added GetObjects() and GetFilteredObjects()
  - Added resortNow parameter to SetSortColumn()


2008-08-28 22:39 (#233) - ObjectListView/Filter.py
  - Added Filter.Chain
  - Added text constructor parameter to TextSearch


2008-08-28 01:22 (#232) - ObjectListView/ObjectListView.py
  - Correct AddObjects() when a filter is in effect
  - Made RebuildGroups() public


2008-08-28 01:21 (#231) - Examples/Demo.py
  - Implement search controls on several tabs


2008-08-28 01:20 (#230) - ObjectListView/Filter.py
  - Make text search handle non-report views better


2008-08-27 23:59 (#229) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/Filter.py
  - Filters work


2008-08-25 10:51 (#225) - Examples/Demo.py
  - Use AddObjects() for "Add 1000" commands


2008-08-25 10:50 (#224) - ObjectListView/ObjectListView.py
  - Added AddObjects()/RemoveObjects() and friends
  - Removed duplicate code when building/refreshing/adding objects
  - One step closer to secondary sort column support


2008-08-22 19:38 (#220) - docs/listCtrlPrinter.rst
  - Added formatting picture


2008-08-20 22:21 (#219) - Examples/Demo.py
  - Changed to use new properties on ListViewPrinter


2008-08-20 22:20 (#218) - ObjectListView/ListCtrlPrinter.py
  - Consistently use properties on ListCtrlPrinter (ReportFormat, PageFooter, PageHeader, Watermark and PrintData are now all properties)
  - Removed ListCtrlPrinter.PageHeader(), ListCtrlPrinter.PageFooter(), ListCtrlPrinter.Watermark(), since they are now replaced with properties (and make more sense that way)


2008-08-20 00:28 (#217) - docs/.static/icon.ico, docs/images/listctrlprinter-example2.png, docs/images/listctrlprinter-structure.png, docs/listCtrlPrinter.rst, docs/.templates/layout.html, Examples/Demo.py
  - Added lots of documentation about ListCtrlPrinter


2008-08-20 00:27 (#216) - ObjectListView/ListCtrlPrinter.py
  - Moved AlwaysCenter and CanWrap to BlockFormat
  - Improved docs


2008-08-18 10:04 (#214) - THANKS.txt
  - Added Werner Bruhin to THANKS


2008-08-18 10:03 (#213) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/OLVEvent.py
  - Handle model objects that cannot be hashed
  - Added editing started and finished events


2008-08-18 10:02 (#212) - Examples/SqlExample.py
  - Reorganized code slightly


2008-08-18 00:37 (#211) - Examples/SqlExample.py
  - Initial checkin


2008-08-17 21:47 (#210) - ObjectListView/WordWrapRenderer.py
  - Second attempt at avoid bug in wordwrap module


2008-08-16 23:31 (#209) - ObjectListView/WordWrapRenderer.py
  - Allow truncated text to be vertically aligned


2008-08-16 23:24 (#208) - ObjectListView/ListCtrlPrinter.py
  - Use RunningBlockPusher to simplify code
  - Allow truncated strings to be vertically aligned


2008-08-16 22:58 (#207) - ObjectListView/ListCtrlPrinter.py
  - Centralize cell width calculation (again)
  - Gracefully handle substitutions that fail


2008-08-16 22:55 (#206) - Examples/Demo.wxg, Examples/Demo.py
  - All control changes on ListCtrlPrinting now update the preview


2008-08-16 10:23 (#205) - ObjectListView/WordWrapRenderer.py
  - Avoid bug in wordwrap module
  - use DCClipper
  - Simplified some code


2008-08-16 09:47 (#204) - ObjectListView/ListCtrlPrinter.py
  - Column width is now calculated by the column headers only
  - Added ListCtrlPrinter.GetPrintData()
  - Make sure print data is destroyed after printing
  - Remove print statements


2008-08-16 09:38 (#203) - ObjectListView/ObjectListView.py
  - Added ensureVisible parameter to SelectObject()


2008-08-13 00:09 (#199) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/OLVPrinter.py, ObjectListView/ListCtrlPrinter.py
  - Allow text to be vertically aligned in cells
  - Improved some docs
  - Renamed OLVPrinter to be ListCtrlPrinter


2008-08-13 00:07 (#198) - Examples/Demo.wxg, Examples/Demo.py
  - Changed OLVPrinter to be ListCtrlPrinter


2008-08-13 00:06 (#197) - docs/images/listctrlprinter-example1.png, docs/images/grouplist-example1.png, docs/listCtrlPrinter.rst, docs/.templates/layout.html, docs/faq.rst, docs/index.rst, docs/.static/listCtrlPrinter-icon.png, docs/.static/majorClasses-icon.png
  - Began documenting ListCtrlPrinter


2008-08-12 19:40 (#195) - ObjectListView/OLVPrinter.py
  - Added TooMuch() formatting
  - Create instance variables normally in ReportFormat -- rather than using setattr()
  - Simplified scaling of rows
  - Changed some method names to better reflect their more generic role
  - Changed variable to refer to a listview rather than an objectlistview


2008-08-08 11:36 (#194) - Examples/Demo.wxg, Examples/Demo.py
  - Completely reworked ListCtrl printing tab


2008-08-08 11:35 (#193) - test/test_ObjectListView.py
  - Make adjustments for GroupListView now being virtual


2008-08-08 11:34 (#192) - ObjectListView/OLVPrinter.py
  - Added ImageDecoration
  - Removed report title and footer
  - Corrected (and optimized) counting pages and skipping pages


2008-08-06 20:44 (#191) - ObjectListView/OLVPrinter.py
  - Row height is now calculate for the whole row, not just the current slice
  - Separated water format from the watermark text
  - Allow blocks to decide not to print themselves
  - Use a dummy DC when counting total pages


2008-08-06 10:14 (#190) - Examples/Demo.wxg, Examples/Demo.py
  - Rearranged Printing panel
  - Inline print preview has water mark


2008-08-05 22:43 (#189) - ObjectListView/ObjectListView.py
  - GroupListView is now implemented as a virtual list
  - Moved putBlankLineBetweenGroups into GroupListView (and out of ObjectListView)


2008-08-05 22:40 (#188) - ObjectListView/__init__.py
  - Added ListGroup into classes exported from the module


2008-08-05 22:39 (#187) - Examples/Demo.wxg, Examples/Demo.py
  - ListCtrl print previewing now works more or less completely


2008-08-04 16:43 (#186) - Examples/Demo.wxg, Examples/Demo.py
  - Added List printing example tab (not yet complete)


2008-08-04 16:43 (#185) - ObjectListView/OLVPrinter.py
  - Header and footers are now ThreeCellBlock
  - Added substitutions on text strings
  - Print garbage pages to a MemoryDC
  - Added IncludeImages and UseListCtrlTextFormat into ReportFormat
  - Added ReportFormat.Minimal()
  - Column headers can now be repeated on each page


2008-08-04 16:37 (#184) - ObjectListView/__init__.py
  - Added list printing stuff


2008-08-02 10:26 (#183) - ObjectListView/OLVPrinter.py
  - Now includes images
  - Cells can now be truncated
  - Decorations can now be either over or under their block


2008-08-02 10:24 (#182) - ObjectListView/ObjectListView.py
  - Added putBlankLineBetweenGroups to GroupListView
  - Handle None as aspect values


2008-08-02 10:23 (#181) - ObjectListView/WordWrapRenderer.py
  - Changed to use wx.lib.wordwrap
  - Added DrawTruncatedString()


2008-08-02 10:22 (#180) - Examples/GroupExample.py, Examples/ExampleModel.py, Examples/Demo.py
  - Remove locale dependence from date parsing


2008-07-31 23:50 (#179) - ObjectListView/OLVPrinter.py
  - Watermarks now work


2008-07-31 21:38 (#178) - ObjectListView/OLVPrinter.py
  - AlwaysCenterColumnHeader and IsShrinkToFit now work


2008-07-31 11:51 (#177) - ObjectListView/OLVPrinter.py
  - Margins, scaling and printer boundries all now work


2008-07-31 10:49 (#176) - test/test_OLVPrinter.py
  - Added TextBlock tests


2008-07-31 10:48 (#175) - ObjectListView/OLVPrinter.py
  - Made work with plain ListCtrls
  - Cell decorations and grids now work
  - Added gradient lines and backgrounds


2008-07-30 17:06 (#174) - ObjectListView/ObjectListView.py
  - Removed reference to testing variable '__rows'


2008-07-30 17:05 (#173) - docs/groupListView.rst, docs/index.rst, docs/gettingStarted.rst
  - COrrected some small mistakes in docs


2008-07-30 11:46 (#172) - CHANGELOG.txt, docs/changelog.rst, setup.py
  - V1.1 release


2008-07-28 22:10 (#170) - ObjectListView/OLVPrinter.py
  - Move grid drawing into CellBlock. Removed GridDecoration
  - Added Bucket and use them instead of dictionaries
  - Correctly handle GroupListView
  - Made compatible with plain ListCtrls


2008-07-28 22:04 (#169) - ObjectListView/WordWrapRenderer.py
  - Made all methods static


2008-07-27 00:22 (#168) - ObjectListView/OLVPrinter.py
  - Added GridDecoration, FrameDecoration
  - Changed technique of page header/footers


2008-07-26 00:30 (#167) - docs/features.rst, docs/whatsnew.rst, docs/groupListView.rst, docs/.templates/layout.html, docs/conf.py, docs/majorClasses.rst, docs/changelog.rst, docs/index.rst, docs/gettingStarted.rst, docs/.static/groupListView-icon.png, docs/recipes.rst
  - Added documentation about GroupListView


2008-07-26 00:28 (#166) - Examples/GroupExample.py, Examples/Demo.py, Examples/SimpleExample1.py, Examples/SimpleExample2.py
  - Minor corrections to examples


2008-07-26 00:27 (#165) - ObjectListView/ObjectListView.py
  - Correctly trigger and handle group related events
  - Made EmptyListMsg work under Linux
  - Correct location of expand/collapse images under Linux
  - Removed some isinstance() and callable() tests


2008-07-26 00:23 (#164) - ObjectListView/__init__.py
  - Export group related events


2008-07-26 00:23 (#163) - ObjectListView/OLVEvent.py
  - Complete implementation of group related events


2008-07-26 00:21 (#162) - ObjectListView/WordWrapRenderer.py
  - Factored out _CalculateLineHeight()
  - Set up a nicer font under Linux


2008-07-26 00:20 (#161) - test/test_OLVPrinter.py
  - Initial checkin


2008-07-25 15:52 (#160) - ObjectListView/WordWrapRenderer.py
  - Initial checkin


2008-07-25 13:31 (#159) - ObjectListView/OLVPrinter.py
  - Pagination now works correctly
  - Correctly calculates total number of pages


2008-07-24 21:07 (#158) - ObjectListView/OLVPrinter.py
  - Before changing to use ReportEngine


2008-07-24 10:39 (#157) - docs/groupListView.rst
  - Initial checkin


2008-07-23 11:26 (#154) - docs/features.rst
  - Included GroupListView in features


2008-07-23 11:25 (#153) - ObjectListView/OLVPrinter.py
  - More WIP


2008-07-23 11:24 (#152) - ObjectListView/ObjectListView.py
  - Consistently use GetSortColumn()
  - Updated some docs


2008-07-19 15:57 (#151) - ObjectListView/OLVPrinter.py
  - Work in progress


2008-07-17 20:40 (#150) - ObjectListView/ObjectListView.py
  - Added ability to turn off groups in GroupListView
  - Added ability to lock the group by column
  - Changed ObjectListView to use 'innerList'
  - SetColumns() can now retain the current model objects
  - Optimized sort key getter and munging. 30% faster!


2008-07-17 20:34 (#147) - Examples/Demo.wxg, Examples/Demo.py
  - Added Group tab to demo


2008-07-17 20:34 (#146) - Examples/GroupExample.py, Examples/ExampleModel.py, Examples/SimpleExample1.py, Examples/SimpleExample2.py
  - Changed to use ExampleModel.py


2008-07-17 15:03 (#145) - ObjectListView/ObjectListView.py
  - Refactored VirtualObjectListView and FastObjectListView to have common base class (AbstractVirtualObjectListView). This made FastObjectListView much simpler
  - Added GetPrimaryColumn()


2008-07-17 13:19 (#144) - Examples/GroupExample.py, Examples/Demo.py
  - In Demo.py, give the simple list a separate column for the checkbox
  - In GroupExample.py, give the list a checkbox and make the control editable.


2008-07-17 13:17 (#143) - test/test_ObjectListView.py
  - Fixed all problems with tests
  - GroupListView now passes all general ObjectListView tests


2008-07-17 13:15 (#142) - ObjectListView/OLVEvent.py
  - Added new group events


2008-07-17 13:15 (#141) - ObjectListView/ObjectListView.py
  - Allow GroupListView to have checkboxes too
  - GroupListView now copy objects to clipboard correctly
  - Use native renderer for expand/collapse images
  - Added "handleStandardKeys"
  - GetSelectedObject() now processes at most 2 rows
  - Correctly calculate primary column instead of just assuming column 0
  - Correctly handle column images
  - Search-by-typing now works in GroupListView
  - Don't allow editing of groups and empty rows
  - Added groupTitleSingleItem and groupTitlePluralItems to ColumnDefn


2008-07-15 15:39 (#140) - Examples/GroupExample.py
  - Example showing capabilites of GroupListView


2008-07-15 15:38 (#139) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/OLVEvent.py
  - First take at groupable ListCtrl


2008-07-14 20:46 (#138) - ObjectListView/ObjectListView.py
  - Added CopySelectionToClipboard and CopyObjectsToClipboard


2008-07-08 20:37 (#135) - ObjectListView/ObjectListView.py
  - Headers can have now have images
  - Fixed Linux specific issues
  - Fixed cell editor bug when double clicking out of list bounds


2008-06-27 22:13 (#134) - ObjectListView/ObjectListView.py
  - Updated docs to match v1.0.1


2008-06-23 19:50 (#132) - Examples/UsingVirtualListExample.py
  - Replace hardcoded path with wx.StandardPaths


2008-06-22 22:35 (#128) - ObjectListView/ObjectListView.py
  - Fixed bug where an imageGetter that returned 0 was treated as if it returned -1 (i.e. no image)


2008-06-20 00:16 (#126) - TODO.txt, setup.py, README.txt
  - Changed feature list
  - Changed download location


2008-06-20 00:15 (#125) - docs/features.rst, docs/whatsnew.rst, docs/.templates/layout.html, docs/conf.py, docs/majorClasses.rst
  - Update to version 1.0.1
  - Added "Class Docs" section to menu
  - Added new sections to Features and What's New


2008-06-20 00:12 (#124) - Examples/SimpleExample1.py
  - Enable logging


2008-06-20 00:11 (#123) - Examples/Demo.py
  - Added more checkboxes
  - Corrected some typing errors


2008-06-20 00:09 (#122) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/OLVEvent.py
  - Allowed for custom sorting, even on virtual lists
  - Factored out test for binary search
  - Added OLVColumn.useBinarySearch
  - Added EVT_SORT and its friends


2008-06-20 00:05 (#121) - test/test_ObjectListView.py
  - Added tests for virtual lists


2008-06-18 09:48 (#118) - setup.py
  - Change download location
  - Change feature list


2008-06-17 20:44 (#117) - ObjectListView/ObjectListView.py
  - Made binary searching work when column is sorted descending


2008-06-17 00:53 (#116) - ObjectListView/ObjectListView.py
  - use binary searches when searching on sorted columns
  - use MAX_ROWS_FOR_UNSORTED_SEARCH to limit linear searches when typing


2008-06-17 00:47 (#115) - docs/.templates/layout.html, docs/faq.rst, docs/index.rst, docs/gettingStarted.rst, docs/recipes.rst
  - Changed download location of source distribution
  - Added recipe about referencing columnDefns inside a valueGetter
  - Rearranged slightly the getting started section.
  - Added FAQ about the indent of text when there is no icon


2008-06-16 22:43 (#114) - ObjectListView/ObjectListView.py
  - Typing searches sort column complete


2008-06-15 21:15 (#113) - ObjectListView/ObjectListView.py
  - Added 'sortable' parameter. VirtualObjectListView are now not sortable by default
  - Improved management of image lists


2008-06-15 21:13 (#112) - setup.py, MANIFEST.in
  - Include bmp files in MANIFEST.in
  - Correct some details in setup.py


2008-06-14 22:31 (#111) - ObjectListView/CellEditor.py
  - Changed use to utf-8 encoding


2008-06-14 22:29 (#110) - ObjectListView/ObjectListView.py
  - Renamed sortColumn to be sortColumnIndex to make it clear
  - Allow returns in multiline cell editors
  - Only use alternate backcolors in report view, not in the other views


2008-06-08 21:30 (#109) - ObjectListView/ObjectListView.py
  - Clear the DC before drawing a checkbox. Needed for Linux


2008-05-30 14:13 (#108) - ObjectListView/ObjectListView.py, test/test_ObjectListView.py
  - Make ImageList.GetSize(0) work to empty image lists under Linux
  - Added more tests, especially for FastObjectListView


2008-05-29 14:22 (#107) - CHANGELOG.txt, docs/changelog.rst
  - v1.0 Release!


2008-05-29 14:17 (#106) - docs/features.rst, docs/whatsnew.rst, docs/cellEditing.rst, docs/.static/features-icon.png, docs/.templates/layout.html, docs/index.rst, docs/gettingStarted.rst, docs/recipes.rst
  - Finally clean up of documentation before v1.0 release


2008-05-29 14:16 (#105) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/CellEditor.py
  - Used named images internally
  - Better handling of missing image lists
  - Cleaned up some more documentation


2008-05-29 00:25 (#104) - ObjectListView/ObjectListView.py, ObjectListView/CellEditor.py
  - Changed to use "isinstance(x, basestring)" rather than "isinstance(x, (str, unicode)"


2008-05-28 00:22 (#102) - docs/.static/changelog-icon.png, docs/whatsnew.rst, ObjectListView/ObjectListView.py, docs/.static/global.css, docs/.static/structure.css, docs/.templates/layout.html, CHANGELOG.txt, docs/faq.rst, docs/index.rst, docs/gettingStarted.rst, setup.py, Examples/Demo.py
  - Better documentation in Demo.py
  - Tidied up docs for v1.0 release
  - Allow sorting by column created by CreateCheckStateColumn()


2008-05-27 13:38 (#101) - test/test_CellEditors.py, test/test_ObjectListView.py, test/test_OLVColumn.py
  - Added ".." to python path so that ObjectListView will be found even if it hasn't been installed


2008-05-27 13:37 (#100) - ObjectListView/ObjectListView.py, CHANGELOG.txt, FAQ.txt, COPYING.txt, ObjectListView/OLVEvent.py, THANKS.txt, setup.py, Examples/Demo.py, ObjectListView/CellEditor.py, ObjectListView.kpf
  - Prepare for v1.0 release


2008-05-27 13:30 (#99) - docs/.static/faq-icon.png, docs/.static/index-icon.png, docs/.static/initial.css, docs/.static/gettingStarted-icon.png, docs/whatsnew.rst, docs/.static/recipes-icon.png, docs/cellEditing.rst, docs/.templates/layout.html, docs/conf.py, docs/.static/whatsnew-icon.png, docs/index.rst, docs/gettingStarted.rst, docs/.static/cellEditing-icon.png, docs/recipes.rst, docs/.static/search-icon.png
  - Added images to generated html
  - Prepare documentation for v1.0 release


2008-05-26 17:37 (#98) - Examples/Demo.wxg, Examples/Demo.py
  - Remove "dummy" tab


2008-05-26 00:39 (#95) - setup.cfg, pylint.rc, AUTHORS.txt, TODO.txt, INSTALL.txt, CHANGELOG.txt, FAQ.txt, COPYING.txt, THANKS.txt, setup.py, COPYING, NEWS.txt, MANIFEST.in, ObjectListView.kpf
  - Did all work to create proper package with distutils (setup.py)


2008-05-26 00:35 (#93) - Examples/example-images/convertImages.bat, Examples/Demo.py, Examples/example-images/convertImages.py, Examples/SimpleExample2.py, Examples/UsingDictionaryExample.py
  - Corrected for new directory structure


2008-05-26 00:35 (#92) - ObjectListView/ObjectListView.py
  - Fixed pyLint annoyances


2008-05-26 00:34 (#91) - ObjectListView/OLVEvent.py
  - Fixed pyLint annoyances


2008-05-26 00:34 (#90) - ObjectListView/CellEditor.py
  - Fixed pyLint annoyances


2008-05-26 00:33 (#89) - ObjectListView/__init__.py
  - Cleaned up a litte


2008-05-24 01:57 (#67) - docs/source/.static/orange-800x1600.png, docs/source/images/coffee.jpg, docs/source/conf.py, docs/source/.static/reset.css, docs/source/faq.rst, docs/source/index.rst, docs/source/images/icecream3.jpg, docs/source/.static/initial.css, docs/source/.static/sphinx-default.css, docs/source/.static/master.css, docs/source/.static/light-blue-800x1600.png, docs/source/images/Thumbs.db, docs/source/.static/dialog.css, docs/source/.templates/layout.html, docs/source/.static/structure.css, docs/source/.static/global.css, docs/source/gettingStarted.rst, docs/source/recipes.rst, docs/source/.static/dialog2-blue-800x1600.png, docs/source/.static/dark-blue-800x1600.png, docs/source/images/cookbook-checkbox1.png, docs/source/images/cookbook-checkbox2.png
  - Documentation near completion


2008-05-24 01:55 (#65) - ObjectListView/ObjectListView.py
  - Added ability to name images
  - Used _ to hide "private" methods
  - Improved docs
  - Correctly calculate subitem rect when in ICON view
  - Implemented HitTestSubItem for all platforms
  - Make sure empty list msg is shown on virtual lists


2008-05-24 01:51 (#64) - ObjectListView/CellEditor.py
  - Change editor style when listctrl is in ICON view


2008-05-24 01:51 (#63) - ObjectListViewDemo/ObjectListViewDemo.py
  - Made sure all buttons worked
  - Uses named images


2008-05-24 01:49 (#62) - Tests/test_ObjectListView.py
  - Added tests for checkboxes, SelectAll, DeselectAll, Refresh


2008-05-19 21:34 (#61) - ObjectListView/ObjectListView.py
  - Added support for checkboxes
  - Used "modelObject(s)" name instead of "object(s)"
  - Made sure all public methods have docstrings


2008-05-19 21:32 (#60) - Tests/test_CellEditors.py, Tests/test_ObjectListView.py, Tests/test_OLVColumn.py, ObjectListView/CellEditor.py, ObjectListViewDemo/ObjectListViewDemo.py
  - Added ".." to sys.path to demo and tests
  - Added demo for checkboxes
  - Added tests for check boxes


2008-05-19 21:30 (#59) - docs/source/images, docs/source/.static, Examples/images/music16.png, Examples/images/convertImages.bat, docs/source/images/coffee.jpg, docs/source/conf.py, docs/source/.templates, docs/source/images/redbull.jpg, docs/source/index.rst, Examples/Images.py, ObjectListView.kpf, Examples/images/convertImages.py, docs/source/images/ModelToScreenProcess.png, Examples/images, docs/source/majorClasses.rst, docs/source/gettingStarted.rst, docs, docs/source, docs/source/recipes.rst, Examples/SimpleExample2.py, Examples/images/Group32.bmp, Examples/images/Group16.bmp, docs/source/faq.rst, docs/source/images/icecream3.jpg, Examples, docs/source/images/gettingstarted-example1.png, docs/source/images/gettingstarted-example2.png, docs/source/images/Thumbs.db, Examples/images/user32.png, Examples/SimpleExample1.py, Examples/images/music32.png, Examples/images/user16.png
  - Added Sphinx based documentation (in progress)


2008-05-12 11:29 (#44) - OwnerDrawnEditor.py, ObjectListViewDemo.py
  - Minor changes and add svn property


2008-05-12 11:28 (#43) - test_CellEditors.py, test_ObjectListView.py, test_OLVColumn.py
  - Add some svn property


2008-05-12 11:26 (#41) - ObjectListView/ObjectListView.py
  - Massively improved documentation. Generates reasonable docs using epydoc now.


2008-04-23 20:13 (#40) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/OLVEvent.py, ObjectListView/CellEditor.py
  - Added $Id$


2008-04-18 22:57 (#39) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListView/OLVEvent.py, ObjectListView/CellEditor.py
  - Updated documentation


2008-04-18 00:00 (#38) - ObjectListView/ObjectListView.py
  - Added List Empty msg
  - Cleaned up code


2008-04-17 23:59 (#36) - ObjectListViewDemo.py
  - Added "Clear List" buttons
  - Set cell edit mode
  - Made more columns non-auto sizing


2008-04-16 22:54 (#35) - ObjectListView/ObjectListView.py, ObjectListView/__init__.py, ObjectListViewDemo.py, ObjectListView/CellEditor.py
  - Modularized ObjectListView
  - Reorganised code within ObjectListView.py


2008-04-14 16:29 (#29) - test_ObjectListView.py
  - Added test for cell editing


2008-04-14 16:28 (#27) - ObjectListViewDemo.py
  - Added Complex tab
  - Made Simple tab to show what is possible with only ColumnDefns
  - Give colour and font to model objects


2008-04-14 16:26 (#26) - ObjectListView.py
  - Allow columns to have a cell editor creator function
  - Handle horizontal scrolling when cell editing
  - Added cell edit modes
  - Handle edit during non-report views
  - Correctly update slots with a previous value of None
  - First cleanup of cell editing code


2008-04-08 00:24 (#25) - ObjectListView.py
  - Cell editing finished, including model updating
  - Changed manner of rebuilding list to use ListItems
  - Unified rowFormatter to use ListItems. Now virtual lists use the same logic
  - Improved documentation on ColumnDefn
  - Lists can now be used a model objects.
  - Removed sortable parameter to ObjectListView


2008-04-08 00:18 (#24) - test_OLVColumn.py
  - Added tests for value setting
  - Added tests of list accessing
  - Reorganized tests


2008-04-08 00:17 (#23) - ObjectListViewDemo.py
  - Changed to handle new unified rowFormatter
  - Allow dateLastPlayed to be updated


2008-04-08 00:15 (#22) - OLVEvent.py
  - Allow cell value to be changed in FinishingCellEdit event


2008-04-08 00:15 (#21) - CellEditor.py
  - Validate keys in the numeric editors


2008-04-07 11:13 (#20) - ObjectListView.py, ObjectListViewDemo.py
  - Made to work under Linux (still needs work)


2008-04-07 11:12 (#19) - OLVEvent.py
  - Added the source listview as a parameter


2008-04-07 11:12 (#18) - CellEditor.py
  - Make work under Linux
  - Autocomplete no longer choke on large lists


2008-04-06 01:02 (#17) - ObjectListView.py, ObjectListViewDemo.py
  - Cell editing in progress: F2 triggers, Tabbing works
  - Improved docs in ObjectListView.py
  - Added example of cell editing events to demo


2008-04-06 00:59 (#16) - OLVEvent.py
  - Initial check in


2008-04-06 00:59 (#15) - test_CellEditors.py, test_ObjectListView.py, test_OLVColumn.py
  - Separated column tests from list tests
  - Added sorting tests and space filling tests
  - Added basic tests for all editors


2008-04-06 00:57 (#14) - CellEditor.py
  - Initial checkin.
  - Editors for all basic types working
  - Autocomplete textbox and combobox working
  - Editor registry working


2008-04-02 00:42 (#13) - ObjectListView.py, ObjectListViewDemo.py
  - Added free space filling columns


2008-03-29 22:44 (#12) - test_ObjectListView.py, ObjectListView.py, Demo.wxg, ObjectListViewDemo.py
  - Added minimum, maximum and fixed widths for columns
  - unified 'stringFormat' and 'stringConverter'
  - Added/update unit tests


2008-03-28 23:54 (#11) - ObjectListView.py, Demo.wxg, ObjectListViewDemo.py
  - Added VirtualObjectListView and FastObjectListView
  - Changed sort indicator icons
  - Changed demo to use track information, and to show new classes


2008-03-06 12:20 (#10) - ObjectListViewDemo.py
  - Call SetObjects() after assigning a rowFormatter


2008-03-06 12:19 (#9) - ObjectListView.py
  - Improved docs
  - Removed some duplicate code


2008-03-02 11:02 (#8) - ObjectListView.py, ObjectListViewDemo.py
  - Added alternate row colors
  - Added rowFormatter


2008-03-02 09:33 (#6) - ObjectListViewDemo.py
  - Added Update Selected
  - Added examples of lowercase and Unicode


2008-03-02 09:31 (#5) - test_ObjectListView.py
  - Test selections
  - Use PySimpleApp


2008-03-02 09:30 (#4) - ObjectListView.py
  - Added RefreshObject() and friends
  - Do sorting within python when possible, rather than using SortItems(). 5-10x faster!
  - Optimized RepopulateList()


2008-02-29 10:34 (#2) - images/BoxesThree32.bmp, images/BoxesThree16.bmp, images/Group32.bmp, test_ObjectListView.py, ObjectListView.py, images, images/DeliveryHand32.bmp, images/Group16.bmp, images/User32.bmp, images/DeliveryHand16.bmp, images/User16.bmp, Demo.wxg, ObjectListViewDemo.py
  - Unit tests in progress
  - Demo complete


