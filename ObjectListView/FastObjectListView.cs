/*
 * FastObjectListView - A listview that behaves like an ObjectListView but has the speed of a virtual list
 *
 * Author: Phillip Piper
 * Date: 27/09/2008 9:15 AM
 *
 * Change log:
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
using System.Drawing;
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
    /// <item>display images on subitems</item>
    /// </list>
    /// </para>
    /// <para>You can circumvent the limit on subitem images by making the list owner drawn, and giving the column
    /// a Renderer of BaseRenderer, e.g. <code>myColumnWithImage.Renderer = new BaseRenderer();</code> </para>
    /// </remarks>
    public class FastObjectListView : VirtualObjectListView
    {
        /// <summary>
        /// Make a FastObjectListView
        /// </summary>
        public FastObjectListView()
        {
            this.DataSource = new FastObjectListDataSource(this);
        }

    }

    public class FastObjectListDataSource : AbstractVirtualListDataSource
    {
        public FastObjectListDataSource(FastObjectListView listView)
            : base(listView)
        {
        }

        #region IVirtualListDataSource Members

        override public object GetNthObject(int n)
        {
            return this.objectList[n];
        }

        override public int GetObjectCount()
        {
            return this.objectList.Count;
        }

        override public int GetObjectIndex(object model)
        {
            int index;

            if (model != null && this.objectsToIndexMap.TryGetValue(model, out index))
                return index;
            else
                return -1;
        }

        override public int SearchText(string value, int first, int last, OLVColumn column)
        {
            return DefaultSearchText(value, first, last, column, this);
        }

        override public void Sort(OLVColumn column, SortOrder sortOrder)
        {
            if (sortOrder != SortOrder.None)
                this.objectList.Sort(new ModelObjectComparer(column, sortOrder, this.listView.SecondarySortColumn, this.listView.SecondarySortOrder));
            this.RebuildIndexMap();
        }

        override public void AddObjects(ICollection modelObjects)
        {
            foreach (object modelObject in modelObjects) {
                if (modelObject != null)
                    this.objectList.Add(modelObject);
            }
            this.RebuildIndexMap();
        }

        override public void RemoveObjects(ICollection modelObjects)
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

        override public void SetObjects(IEnumerable collection)
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

    /// <summary>
    /// This comparer can be used to sort a collection of model objects by a given column
    /// </summary>
    internal class ModelObjectComparer : IComparer
    {
        public ModelObjectComparer(OLVColumn col, SortOrder order)
        {
            this.column = col;
            this.sortOrder = order;
        }

        public ModelObjectComparer(OLVColumn col, SortOrder order, OLVColumn col2, SortOrder order2)
            : this(col, order)
        {
            // There is no point in secondary sorting on the same column
            if (col != col2)
                this.secondComparer = new ModelObjectComparer(col2, order2);
        }

        public int Compare(object x, object y)
        {
            int result = 0;
            object x1 = this.column.GetValue(x);
            object y1 = this.column.GetValue(y);

            if (this.sortOrder == SortOrder.None)
                return 0;

            // Handle nulls. Null values come last
            bool xIsNull = (x1 == null || x1 == System.DBNull.Value);
            bool yIsNull = (y1 == null || y1 == System.DBNull.Value);
            if (xIsNull || yIsNull) {
                if (xIsNull && yIsNull)
                    result = 0;
                else
                    result = (xIsNull ? -1 : 1);
            } else {
                result = this.CompareValues(x1, y1);
            }

            if (this.sortOrder == SortOrder.Descending)
                result = 0 - result;

            // If the result was equality, use the secondary comparer to resolve it
            if (result == 0 && this.secondComparer != null)
                result = this.secondComparer.Compare(x, y);

            return result;
        }

        public int CompareValues(object x, object y)
        {
            // Force case insensitive compares on strings
            String xStr = x as String;
            if (xStr != null)
                return String.Compare(xStr, (String)y, true);
            else {
                IComparable comparable = x as IComparable;
                if (comparable != null)
                    return comparable.CompareTo(y);
                else
                    return 0;
            }
        }

        private OLVColumn column;
        private SortOrder sortOrder;
        private ModelObjectComparer secondComparer;
    }

}
