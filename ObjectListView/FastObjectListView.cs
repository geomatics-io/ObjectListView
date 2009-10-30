/*
 * FastObjectListView - A listview that behaves like an ObjectListView but has the speed of a virtual list
 *
 * Author: Phillip Piper
 * Date: 27/09/2008 9:15 AM
 *
 * Change log:
 * v2.3
 * 2009-08-27   JPP  - Added GroupingStrategy
 *                   - Added optimized Objects property
 * v2.2.1
 * 2009-01-07   JPP  - Made all public and protected methods virtual
 * 2008-09-27   JPP  - Separated from ObjectListView.cs
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
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// A FastObjectListView trades function for speed.
    /// </summary>
    /// <remarks>
    /// <para>On my mid-range laptop, this view builds a list of 10,000 objects in 0.1 seconds,
    /// as opposed to a normal ObjectListView which takes 10-15 seconds. Lists of up to 50,000 items should be
    /// able to be handled with sub-second response times even on low end machines.</para>
    /// <para>
    /// A FastObjectListView is implemented as a virtual list with some of the virtual modes limits (e.g. no sorting)
    /// fixed through coding. There are some functions that simply cannot be provided. Specifically, a FastObjectListView cannot:
    /// <list>
    /// <item>shows groups</item>
    /// <item>use Tile view</item>
    /// <item>display images on subitems (though you can easily circumvent this limit by making the list owner drawn)</item>
    /// </list>
    /// </para>
    /// </remarks>
    public class FastObjectListView : VirtualObjectListView
    {
        /// <summary>
        /// Make a FastObjectListView
        /// </summary>
        public FastObjectListView()
        {
            this.DataSource = new FastObjectListDataSource(this);
            this.GroupingStrategy = new FastListGroupingStrategy();
        }

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
        /// </remarks>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IEnumerable Objects {
            get {
                // This is much faster than the base method
                return ((FastObjectListDataSource)this.DataSource).ObjectList; 
            }
            set { base.Objects = value; }
        }
    }

    /// <summary>
    /// Provide a data source for a FastObjectListView
    /// </summary>
    public class FastObjectListDataSource : AbstractVirtualListDataSource
    {
        public FastObjectListDataSource(FastObjectListView listView)
            : base(listView)
        {
        }

        internal ArrayList ObjectList {
            get { return objectList; }
        }

        #region IVirtualListDataSource Members

        public override object GetNthObject(int n)
        {
            return this.objectList[n];
        }

        public override int GetObjectCount()
        {
            return this.objectList.Count;
        }

        public override int GetObjectIndex(object model)
        {
            int index;

            if (model != null && this.objectsToIndexMap.TryGetValue(model, out index))
                return index;
            else
                return -1;
        }

        public override int SearchText(string value, int first, int last, OLVColumn column)
        {
            return DefaultSearchText(value, first, last, column, this);
        }

        public override void Sort(OLVColumn column, SortOrder sortOrder)
        {
            if (sortOrder != SortOrder.None)
                this.objectList.Sort(new ModelObjectComparer(column, sortOrder, this.listView.SecondarySortColumn, this.listView.SecondarySortOrder));
            this.RebuildIndexMap();
        }

        public override void AddObjects(ICollection modelObjects)
        {
            foreach (object modelObject in modelObjects) {
                if (modelObject != null)
                    this.objectList.Add(modelObject);
            }
            this.RebuildIndexMap();
        }

        public override void RemoveObjects(ICollection modelObjects)
        {
            List<int> indicesToRemove = new List<int>();
            foreach (object modelObject in modelObjects) {
                int i = this.GetObjectIndex(modelObject);
                if (i >= 0)
                    indicesToRemove.Add(i);
            }
            // Sort the indices from highest to lowest so that we
            // remove latter ones before earlier ones. In this way, the
            // indices of the rows doesn't change after the deletes.
            indicesToRemove.Sort();
            indicesToRemove.Reverse();

            foreach (int i in indicesToRemove)
                this.listView.SelectedIndices.Remove(i);

            foreach (int i in indicesToRemove)
                this.objectList.RemoveAt(i);

            this.RebuildIndexMap();
        }

        public override void SetObjects(IEnumerable collection)
        {
            ArrayList newObjects = new ArrayList();
            if (collection != null) {
                if (collection is ICollection)
                    newObjects = new ArrayList((ICollection)collection);
                else {
                    foreach (object x in collection)
                        newObjects.Add(x);
                }
            }

            this.objectList = newObjects;
            this.RebuildIndexMap();
        }

        private ArrayList objectList = new ArrayList();

        #endregion

        #region Implementation

        /// <summary>
        /// Rebuild the map that remembers which model object is displayed at which line
        /// </summary>
        protected void RebuildIndexMap()
        {
            this.objectsToIndexMap.Clear();
            for (int i = 0; i < this.objectList.Count; i++)
                this.objectsToIndexMap[this.objectList[i]] = i;
        }
        Dictionary<Object, int> objectsToIndexMap = new Dictionary<Object, int>();

        #endregion
    }

}
