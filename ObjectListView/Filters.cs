/*
 * Filters - Filtering on ObjectListViews
 *
 * Author: Phillip Piper
 * Date: 03/03/2010 17:00 
 *
 * Change log:
 * 2010-03-03  JPP  Initial version
 *
 * TO DO:
 *
 * Copyright (C) 2010 Phillip Piper
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
using System.Data;
using System.Reflection;

namespace BrightIdeasSoftware
{
    public interface IModelFilter
    {
        bool Filter(object modelObject);
    }

    public interface IListFilter
    {
        IEnumerable Filter(IEnumerable modelObjects);
    }

    public class AbstractModelFilter : IModelFilter
    {
        virtual public bool Filter(object modelObject) {
            return true;
        }
    }

    public class ModelFilter : IModelFilter
    {
        public ModelFilter(Predicate<object> predicate) {
            this.Predicate = predicate;
        }

        protected Predicate<object> Predicate;

        virtual public bool Filter(object modelObject) {
            return this.Predicate(modelObject);
        }
    }

    public class AbstractListFilter : IListFilter
    {
        virtual public IEnumerable Filter(IEnumerable modelObjects) {
            return modelObjects;
        }

        /// <summary>
        /// Return the given enumerable as a list, creating a new
        /// collection only if necessary.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        protected IList EnumerableToList(IEnumerable enumerable) {
            IList list = enumerable as IList;
            if (list == null) {
                list = new ArrayList();
                foreach (object x in enumerable)
                    list.Add(x);
            }
            return list;
        }
    }

    public class ListFilter : AbstractListFilter
    {
        public delegate IEnumerable ListFilterDelegate(IEnumerable rowObjects);

        public ListFilter(ListFilterDelegate function) {
            this.Function = function;
        }

        public ListFilterDelegate Function;

        public override IEnumerable Filter(IEnumerable modelObjects) {
            return this.Function(modelObjects);
        }
    }

    public class TailFilter : AbstractListFilter
    {
        public TailFilter() {

        }

        public TailFilter(int numberOfObjects) {
            this.Count = numberOfObjects;
        }

        public int Count;

        public override IEnumerable Filter(IEnumerable modelObjects) {
            if (this.Count <= 0)
                return modelObjects;

            ArrayList list = ArrayList.Adapter(this.EnumerableToList(modelObjects));

            if (this.Count > list.Count)
                return list;

            object[] tail = new object[this.Count];
            list.CopyTo(list.Count - this.Count, tail, 0, this.Count);
            return new ArrayList(tail);
        }
    }

    public class TextMatchFilter : AbstractModelFilter
    {
        public TextMatchFilter() {

        }

        public TextMatchFilter(ObjectListView olv) {
            this.ListView = olv;
        }

        public TextMatchFilter(ObjectListView olv, string text) {
            this.ListView = olv;
            this.Text = text;
        }

        public TextMatchFilter(ObjectListView olv, string text, StringComparison comparison) {
            this.ListView = olv;
            this.Text = text;
            this.StringComparison = comparison;
        }

        public string Text;
        public StringComparison StringComparison = StringComparison.InvariantCultureIgnoreCase;

        protected ObjectListView ListView;

        public override bool Filter(object modelObject) {
            if (this.ListView == null || String.IsNullOrEmpty(this.Text))
                return true;

            foreach (OLVColumn column in this.ListView.Columns) {
                if (column.IsVisible) {
                    string cellText = column.GetStringValue(modelObject);
                    if (cellText.IndexOf(this.Text, this.StringComparison) != -1)
                        return true;
                }
            }

            return false;
        }
    }
}