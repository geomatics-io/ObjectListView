.. -*- coding: UTF-8 -*-

:Subtitle: How I learned to stop worrying and love .NET ListView

==============
ObjectListView
==============

.. rubric:: ObjectListView is a C# wrapper around a .NET ListView. It makes the ListView
            much easier to use and provides some neat extra functionality.

Larry Wall, the author of Perl, once wrote that the three essential character flaws of any
good programmer were sloth, impatience and hubris. Good programmers want to do the minimum
amount of work (sloth). They want their programs to run quickly (impatience). They take
inordinate pride in what they have written (hubris).

ObjectListView encourages the vices of sloth and hubris, by allowing programmers to do far
less work but still produce great looking results.

I'm too impatient! Just show me what it can do!
-----------------------------------------------

Here is an example of what your ListView can look like with a few callbacks in place:

.. image:: images/fancy-screenshot.png

The TreeListView similarly make a tree structure look beautiful:

.. image:: images/treelistview.png

And this is the report that can be effortlessly produced from the ObjectListView:

.. image:: images/printpreview.png

Without wasting my time, just tell me what it does!
---------------------------------------------------

OK, here's the bullet point feature list:

* Automatically transforms a list of model objects into a fully functional ListView, including automatically sorting and grouping rows.
* Supports :ref:`owner drawing <owner-draw-label>`, including rendering animated graphics and images stored in a database.
* Easily :ref:`edit the cell values <cell-editing-label>`.
* Supports all ListView views (report, list, large and small icons).
* Supports automatic grouping.
* Columns can be fixed-width, have a minimum and/or maximum width, or be space-filling (:ref:`Column Widths <column-widths>`)
* Displays a :ref:`"list is empty" message <recipe-emptymsg>` when the list is empty (obviously).
* Supports :ref:`tooltips <recipe-tooltips>` for cells and for headers
* Supports :ref:`checkboxes in any column <recipe-checkbox>` as well as tri-state checkboxes
* Supports :ref:`alternate rows background colors <alternate-row-backgrounds>`.
* Supports :ref:`custom formatting of rows <recipe-formatter>`.
* Supports :ref:`searching (by typing) on any column <search-by-typing>`
* The `DataListView` version supports data binding.
* The `FastObjectListView` version can build a list of 10,000 objects in less than 0.1 seconds.
* The `VirtualObjectListView` version supports millions of rows through ListView's virtual mode.
* The `TreeListView` version combines an expandable tree structure with the columns of a ListView.

Seriously, after using an `ObjectListView`, you will never go back to using a plain `ListView`.

OK, I'm interested. What do I do next?
--------------------------------------

You can download_ a demonstration of the `ObjectListView` in
action. This demo includes ObjectListView project which you need to include in
your project.

After that, you might want to look at the :ref:`Getting Started` and the :ref:`Cookbook` sections. Please make
sure you have read and understood these sections before asking questions in the Forum_.
There is also an article describing the `ObjectListView at CodeProject`_.

At some point, you will want to do something with an ObjectListView and it won't be
immediately obvious how to make it happen. After dutifully scouring the :ref:`Getting
Started` and the :ref:`Cookbook` sections, you
decide that is is still not obvious. The Forum_ section is the place to find
all your as-yet-unasked questions.

It may even be possible that you might find some undocumented features in the code (also
known as bugs). These "features" can be `reported here`_ and can be tracked on the project's `Issue Tracker`_.

Finally, after you realise just how great the ObjectListView is, and how you
really have come to love .NET's ListView, you will be moved with gratitude to
`give a donation`_ to ensure the continued development of this code.

If you would like to ask me a question or suggest an improvement, you can contact me here:
phillip_piper@bigfoot.com.

.. _download: https://sourceforge.net/project/platformdownload.php?group_id=225207

.. _ObjectListView at CodeProject: http://www.codeproject.com/KB/list/ObjectListView.aspx

.. _Forum: https://sourceforge.net/project/platformdownload.php?group_id=225207

.. _reported here: https://sourceforge.net/tracker/?func=add&group_id=225207&atid=1064157

.. _Issue Tracker: https://sourceforge.net/tracker/?group_id=225207&atid=1064157

.. _give a donation: https://sourceforge.net/donate/index.php?group_id=225207

Bleeding-edge source
--------------------

If you are a very keen developer, you can access the SVN repository directly for this
project. The following SVN command will fetch the most recent version from the repository::

 svn co https://objectlistview.svn.sourceforge.net/svnroot/objectlistview/cs/trunk objectlistview

There are details on `how to use Subversion here <https://sourceforge.net/docs/E09>`_ on SourceForge.

Please remember that code within the SVN is bleeding edge. It has not been well-tested and
is almost certainly full of bugs. If you just want to play with the ObjectListView, it's
better to stay with the official releases, where the bugs are (hopefully) less obvious.

What people have said about ObjectListView
------------------------------------------

When thinking about using some new code, it's always interesting to hear what others have said about it.

.. pull-quote:: I wanted to say that your control, your code, and your support on the forums, has been one of the best experiences I have had with working with someone elses' product. Great job man, and very nice programming.

   -- Mike Coffey (in personal email)

.. pull-quote:: Thanks for this control, which I now use everywhere I can! It works great.

   -- `William Sauron <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2713269>`_

.. pull-quote:: Intelligent and Intuitive. Thanks.

   -- `Mike Hankey (4th Marines HQ) <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2733497>`_

.. pull-quote:: One of the guys over at the MSDN magazine is known as the Datagrid (ASP.Net) whisperer. You must be the Listview whisperer. DAMN! This is a sexy bunch of controls!

   -- `Jonathan C Dickinson <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2594655>`_

.. pull-quote:: What can I say? I have a file verification program, and I just replaced the listview with yours, and 150 lines of code was made obsolete, and very few lines were added, it also improved performance massively. The light shine upon you, MS should send you some sort of gift for removing the largest headache .NET ever contrived.

   -- `Member 3791472 <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2536977>`_

.. pull-quote:: I've got to say, your code is amazing. It's the only time in my life that I've looked at code and thought it was better than mine. Thanks for your great work (on many levels) and for sharing it.

   -- Brian Perrin (in personal email)

.. pull-quote:: I think you should rename the control description to: "A ListView on Steroids". Keep up the excellent work.

   -- `Michael (mpgjunky) <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2734381>`_

.. pull-quote:: Nice features added, especially the fast build speed and hidden columns, keep up the good work on this control! well done.

   -- `cinamon <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2416400>`_

.. pull-quote:: A VERY BIG "Thank you" for this wonderful control - it's easy to handle once you got the idea, very good structured coding, just a jewel. It became soon one of my favourites.

   -- `Metze <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2357723>`_

.. pull-quote:: Great control. This solves a lot of problems. Thank you very much

   -- `merlin981 <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2290090>`_

.. pull-quote:: Thanks for a GREAT control, and also for your help

   -- `doncp <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2178944>`_

.. pull-quote:: I use this control in almost every project now.. amazing job. Thanks!

   -- `Chris Micali <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2253750>`_

.. pull-quote:: I've never posted before but this is so good, that I must say something... This is awesome!! Keep the good work!

   -- `OverlordHammer <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=1996170>`_

.. pull-quote:: Thank you for opening your hard work to the community, it saved me hours or coding plus I learnt a few new techniques from your code.

   -- `cliftonarms <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=1938806>`_

.. pull-quote:: Those years of experience and innate talent certainly show in this code; it's a pleasure to read. Thanks much for a great example.

   -- `Steve Shaffer <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=1717218>`_

.. pull-quote:: Very nice article and worthy of bookmarking

   -- `Paul Conrad <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=2653103>`_

.. pull-quote:: That's exactly what I have been finding for days. Thanks!!!

   -- `vcleak <http://www.codeproject.com/script/Forums/View.aspx?fid=350107&msg=1716837>`_

Site contents
-------------

.. toctree::
   :maxdepth: 1

   whatsnew
   features
   gettingStarted
   recipes
   Recipe - Cell Editing <cellEditing>
   Recipe - Owner Drawn <ownerDraw>
   faq
   majorClasses
   changelog
