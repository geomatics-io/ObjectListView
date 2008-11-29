/*
 * Comparers - Various Comparer classes used within ObjectListView
 *
 * Author: Phillip Piper
 * Date: 25/11/2008 17:15 
 *
 * Change log:
 * 2008-11-25  JPP  Initial version
 *
 * TO DO:
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
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// ColumnComparer is the workhorse for all comparison between two values of a particular column.
    /// If the column has a specific comparer, use that to compare the values. Otherwise, do
    /// a case insensitive string compare of the string representations of the values.
    /// </summary>
    /// <remarks><para>This class inherits from both IComparer and its generic counterpart
    /// so that it can be used on untyped and typed collections.</para></remarks>
    public class ColumnComparer : IComparer, IComparer<OLVListItem>
    {
        public ColumnComparer(OLVColumn col, SortOrder order)
        {
            this.column = col;
            this.sortOrder = order;
        }

        public ColumnComparer(OLVColumn col, SortOrder order, OLVColumn col2, SortOrder order2)
            : this(col, order)
        {
            // There is no point in secondary sorting on the same column
            if (col != col2)
                this.secondComparer = new ColumnComparer(col2, order2);
        }

        public int Compare(object x, object y)
        {
            return this.Compare((OLVListItem)x, (OLVListItem)y);
        }

        public int Compare(OLVListItem x, OLVListItem y)
        {
            int result = 0;
            object x1 = this.column.GetValue(x.RowObject);
            object y1 = this.column.GetValue(y.RowObject);

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
            String xAsString = x as String;
            if (xAsString != null)
                return String.Compare(xAsString, (String)y, StringComparison.CurrentCultureIgnoreCase);
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
        private ColumnComparer secondComparer;
    }

    /// <summary>
    /// This comparer sort list view groups.
    /// It does this on the basis of the values in the Tags, if we can figure out how to compare
    /// objects of that type. Failing that, it uses a case insensitive compare on the group header.
    /// </summary>
    public class ListViewGroupComparer : IComparer<ListViewGroup>
    {
        public ListViewGroupComparer(SortOrder order)
        {
            this.sortOrder = order;
        }

        public int Compare(ListViewGroup x, ListViewGroup y)
        {
            // If we know how to compare the tags, do that.
            // Otherwise do a case insensitive compare on the group header.
            // We have explicitly catch the "almost-null" value of DBNull.Value,
            // since comparing to that value always produces a type exception.
            int result;
            IComparable comparable = x.Tag as IComparable;
            if (comparable != null && y.Tag != System.DBNull.Value)
                result = comparable.CompareTo(y.Tag);
            else
                result = String.Compare(x.Header, y.Header, StringComparison.CurrentCultureIgnoreCase);

            if (this.sortOrder == SortOrder.Descending)
                result = 0 - result;

            return result;
        }

        private SortOrder sortOrder;
    }

    /// <summary>
    /// This comparer can be used to sort a collection of model objects by a given column
    /// </summary>
    public class ModelObjectComparer : IComparer
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
                return String.Compare(xStr, (String)y, StringComparison.CurrentCultureIgnoreCase);
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