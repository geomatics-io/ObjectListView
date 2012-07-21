/*
 * Generator - Utility methods that generate columns or methods
 *
 * Author: Phillip Piper
 * Date: 15/08/2009 22:37
 *
 * Change log:
 * 2012-06-14  JPP  - Allow columns to be generated even if they are not marked with [OLVColumn]
 *                  - Converted class from static to instance to allow it to be subclassed.
 *                    Also, added IGenerator to allow it to be completely reimplemented.
 * v2.5.1
 * 2010-11-01  JPP  - DisplayIndex is now set correctly for columns that lack that attribute
 * v2.4.1
 * 2010-08-25  JPP  - Generator now also resets sort columns
 * v2.4
 * 2010-04-14  JPP  - Allow Name property to be set
 *                  - Don't double set the Text property
 * v2.3
 * 2009-08-15  JPP  - Initial version
 *
 * To do:
 * 
 * Copyright (C) 2009-2012 Phillip Piper
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
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// An object that implements the IGenerator interface provides the ability 
    /// to dynamically create columns
    /// for an ObjectListView based on the characteristics of a given collection
    /// of model objects.
    /// </summary>
    public interface IGenerator {
        /// <summary>
        /// Generate columns into the given ObjectListView that come from the given 
        /// model object type. 
        /// </summary>
        /// <param name="olv">The ObjectListView to modify</param>
        /// <param name="type">The model type whose attributes will be considered.</param>
        /// <param name="allProperties">Will columns be generated for properties that are not marked with [OLVColumn].</param>
        void GenerateAndReplaceColumns(ObjectListView olv, Type type, bool allProperties);

        /// <summary>
        /// Generate a list of OLVColumns based on the attributes of the given type
        /// If allProperties to true, all public properties will have a matching column generated.
        /// If allProperties is false, only properties that have a OLVColumn attribute will have a column generated.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="allProperties">Will columns be generated for properties that are not marked with [OLVColumn].</param>
        /// <returns>A collection of OLVColumns matching the attributes of Type that have OLVColumnAttributes.</returns>
        IList<OLVColumn> GenerateColumns(Type type, bool allProperties);
    }

    /// <summary>
    /// The Generator class provides methods to dynamically create columns
    /// for an ObjectListView based on the characteristics of a given collection
    /// of model objects.
    /// </summary>
    /// <remarks>
    /// <para>For a given type, a Generator can create columns to match the public properties
    /// of that type. The generator can consider all public properties or only those marked with
    /// [OLVColumn] attribute.</para>
    /// </remarks>
    public class Generator : IGenerator {
        #region Static convenience methods

        /// <summary>
        /// Gets or sets the actual generator used by the static convinence methods.
        /// </summary>
        /// <remarks>If you subclass the standard generator, you should install an instance of
        /// your subclass here.</remarks>
        public static IGenerator Instance {
            get { return Generator.instance ?? (Generator.instance = new Generator()); }
            set { Generator.instance = value; }
        }
        private static IGenerator instance;

        /// <summary>
        /// Replace all columns of the given ObjectListView with columns generated
        /// from the first member of the given enumerable. If the enumerable is 
        /// empty or null, the ObjectListView will be cleared.
        /// </summary>
        /// <param name="olv">The ObjectListView to modify</param>
        /// <param name="enumerable">The collection whose first element will be used to generate columns.</param>
        static public void GenerateColumns(ObjectListView olv, IEnumerable enumerable) {
            Generator.GenerateColumns(olv, enumerable, false);
        }

        /// <summary>
        /// Replace all columns of the given ObjectListView with columns generated
        /// from the first member of the given enumerable. If the enumerable is 
        /// empty or null, the ObjectListView will be cleared.
        /// </summary>
        /// <param name="olv">The ObjectListView to modify</param>
        /// <param name="enumerable">The collection whose first element will be used to generate columns.</param>
        /// <param name="allProperties">Will columns be generated for properties that are not marked with [OLVColumn].</param>
        static public void GenerateColumns(ObjectListView olv, IEnumerable enumerable, bool allProperties) {
            // Generate columns based on the type of the first model in the collection and then quit
            if (enumerable != null) {
                foreach (object model in enumerable) {
                    Generator.Instance.GenerateAndReplaceColumns(olv, model.GetType(), allProperties);
                    return;
                }
            }

            // If we reach here, the collection was empty, so we clear the list
            Generator.Instance.GenerateAndReplaceColumns(olv, null, allProperties);
        }

        /// <summary>
        /// Generate columns into the given ObjectListView that come from the given 
        /// model object type. 
        /// </summary>
        /// <param name="olv">The ObjectListView to modify</param>
        /// <param name="type">The model type whose attributes will be considered.</param>
        static public void GenerateColumns(ObjectListView olv, Type type) {
            Generator.Instance.GenerateAndReplaceColumns(olv, type, false);
        }

        /// <summary>
        /// Generate columns into the given ObjectListView that come from the given 
        /// model object type. 
        /// </summary>
        /// <param name="olv">The ObjectListView to modify</param>
        /// <param name="type">The model type whose attributes will be considered.</param>
        /// <param name="allProperties">Will columns be generated for properties that are not marked with [OLVColumn].</param>
        static public void GenerateColumns(ObjectListView olv, Type type, bool allProperties) {
            Generator.Instance.GenerateAndReplaceColumns(olv, type, allProperties);
        }

        /// <summary>
        /// Generate a list of OLVColumns based on the attributes of the given type
        /// that have a OLVColumn attribute.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A collection of OLVColumns matching the attributes of Type that have OLVColumnAttributes.</returns>
        static public IList<OLVColumn> GenerateColumns(Type type) {
            return Generator.Instance.GenerateColumns(type, false);
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Generate columns into the given ObjectListView that come from the given 
        /// model object type. 
        /// </summary>
        /// <param name="olv">The ObjectListView to modify</param>
        /// <param name="type">The model type whose attributes will be considered.</param>
        /// <param name="allProperties">Will columns be generated for properties that are not marked with [OLVColumn].</param>
        public virtual void GenerateAndReplaceColumns(ObjectListView olv, Type type, bool allProperties) {
            IList<OLVColumn> columns = this.GenerateColumns(type, allProperties);
            this.ReplaceColumns(olv, columns);
        }

        /// <summary>
        /// Generate a list of OLVColumns based on the attributes of the given type
        /// If allProperties to true, all public properties will have a matching column generated.
        /// If allProperties is false, only properties that have a OLVColumn attribute will have a column generated.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="allProperties">Will columns be generated for properties that are not marked with [OLVColumn].</param>
        /// <returns>A collection of OLVColumns matching the attributes of Type that have OLVColumnAttributes.</returns>
        public virtual IList<OLVColumn> GenerateColumns(Type type, bool allProperties) {
            List<OLVColumn> columns = new List<OLVColumn>();
            
            // Sanity
            if (type == null)
                return columns;

            // Iterate all public properties in the class and build columns from those that have
            // an OLVColumn attribute.
            foreach (PropertyInfo pinfo in type.GetProperties()) {
                OLVColumnAttribute attr = Attribute.GetCustomAttribute(pinfo, typeof(OLVColumnAttribute)) as OLVColumnAttribute;
                if (attr == null) {
                    if (allProperties)
                        columns.Add(this.MakeColumnFromPropertyInfo(pinfo));
                } else {
                    columns.Add(this.MakeColumnFromAttribute(pinfo, attr));
                }
            }

            // How many columns have DisplayIndex specifically set?
            int countPositiveDisplayIndex = 0;
            foreach (OLVColumn col in columns) {
                if (col.DisplayIndex >= 0)
                    countPositiveDisplayIndex += 1;
            }

            // Give columns that don't have a DisplayIndex an incremental index
            int columnIndex = countPositiveDisplayIndex;
            foreach (OLVColumn col in columns)
                if (col.DisplayIndex < 0)
                    col.DisplayIndex = (columnIndex++);

            columns.Sort(delegate(OLVColumn x, OLVColumn y) {
                return x.DisplayIndex.CompareTo(y.DisplayIndex);
            });

            return columns;
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Replace all the columns in the given listview with the given list of columns.
        /// </summary>
        /// <param name="olv"></param>
        /// <param name="columns"></param>
        protected virtual void ReplaceColumns(ObjectListView olv, IList<OLVColumn> columns) {
            olv.Reset();

            // Are there new columns to add?
            if (columns == null || columns.Count == 0) 
                return;

            // Setup the columns
            olv.AllColumns.AddRange(columns);
            if (olv.AllColumns.Exists(delegate(OLVColumn x) { return x.CheckBoxes; }))
                olv.SetupSubItemCheckBoxes();
            olv.RebuildColumns();
        }

        /// <summary>
        /// Create a column from the given PropertyInfo and OLVColumn attribute
        /// </summary>
        /// <param name="pinfo"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        protected virtual OLVColumn MakeColumnFromAttribute(PropertyInfo pinfo, OLVColumnAttribute attr) {
            string aspectName = pinfo.Name;
            bool editable = pinfo.CanWrite;

            string title = String.IsNullOrEmpty(attr.Title) ? aspectName : attr.Title;
            OLVColumn column = new OLVColumn(title, aspectName);
            this.ConfigurePossibleBooleanColumn(column, pinfo.PropertyType);

            column.AspectToStringFormat = attr.AspectToStringFormat;
            if (attr.IsCheckBoxesSet)
                column.CheckBoxes = attr.CheckBoxes;
            column.DisplayIndex = attr.DisplayIndex;
            column.FillsFreeSpace = attr.FillsFreeSpace;
            if (attr.IsFreeSpaceProportionSet)
                column.FreeSpaceProportion = attr.FreeSpaceProportion;
            column.GroupWithItemCountFormat = attr.GroupWithItemCountFormat;
            column.GroupWithItemCountSingularFormat = attr.GroupWithItemCountSingularFormat;
            column.Hyperlink = attr.Hyperlink;
            column.ImageAspectName = attr.ImageAspectName;
            column.IsEditable = attr.IsEditableSet ? attr.IsEditable : editable;
            column.IsTileViewColumn = attr.IsTileViewColumn;
            column.IsVisible = attr.IsVisible;
            column.MaximumWidth = attr.MaximumWidth;
            column.MinimumWidth = attr.MinimumWidth;
            column.Name = String.IsNullOrEmpty(attr.Name) ? aspectName : attr.Name;
            column.Tag = attr.Tag;
            if (attr.IsTextAlignSet)
                column.TextAlign = attr.TextAlign;
            column.ToolTipText = attr.ToolTipText;
            if (attr.IsTriStateCheckBoxesSet)
                column.TriStateCheckBoxes = attr.TriStateCheckBoxes;
            column.UseInitialLetterForGroup = attr.UseInitialLetterForGroup;
            column.Width = attr.Width;
            if (attr.GroupCutoffs != null && attr.GroupDescriptions != null)
                column.MakeGroupies(attr.GroupCutoffs, attr.GroupDescriptions);
            return column;
        }

        /// <summary>
        /// Make a column from the given PropertyInfo
        /// </summary>
        /// <param name="pinfo"></param>
        /// <returns></returns>
        protected virtual OLVColumn MakeColumnFromPropertyInfo(PropertyInfo pinfo) {
            OLVColumn column = new OLVColumn(pinfo.Name, pinfo.Name);
            column.IsEditable = pinfo.CanWrite;
            column.Name = pinfo.Name;
            this.ConfigurePossibleBooleanColumn(column, pinfo.PropertyType);
            return column;
        }

        protected virtual void ConfigurePossibleBooleanColumn(OLVColumn column, Type propertyType) {
            if (propertyType != typeof(bool) && propertyType != typeof(bool?) && propertyType != typeof(CheckState)) 
                return;

            column.CheckBoxes = true;
            column.TextAlign = HorizontalAlignment.Center;
            column.Width = 32;
            if (propertyType == typeof(bool?) || propertyType == typeof(CheckState))
                column.TriStateCheckBoxes = true;
        }

        #endregion

        /*
        #region Dynamic methods

        /// <summary>
        /// Generate methods so that reflection is not needed.
        /// </summary>
        /// <param name="olv"></param>
        /// <param name="type"></param>
        public static void GenerateMethods(ObjectListView olv, Type type) {
            foreach (OLVColumn column in olv.Columns) {
                GenerateColumnMethods(column, type);
            }
        }

        public static void GenerateColumnMethods(OLVColumn column, Type type) {
            if (column.AspectGetter == null && !String.IsNullOrEmpty(column.AspectName))
                column.AspectGetter = Generator.GenerateAspectGetter(type, column.AspectName);
        }

        /// <summary>
        /// Generates an aspect getter method dynamically. The method will execute
        /// the given dotted chain of selectors against a model object given at runtime.
        /// </summary>
        /// <param name="type">The type of model object to be passed to the generated method</param>
        /// <param name="path">A dotted chain of selectors. Each selector can be the name of a 
        /// field, property or parameter-less method.</param>
        /// <returns>A typed delegate</returns>
        /// <remarks>
        /// <para>
        /// If you have an AspectName of "Owner.Address.Postcode", this will generate
        /// the equivilent of: <code>this.AspectGetter = delegate (object x) {
        ///     return x.Owner.Address.Postcode;
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        private static AspectGetterDelegate GenerateAspectGetter(Type type, string path) {
            DynamicMethod getter = new DynamicMethod(String.Empty, typeof(Object), new Type[] { type }, type, true);
            Generator.GenerateIL(type, path, getter.GetILGenerator());
            return (AspectGetterDelegate)getter.CreateDelegate(typeof(AspectGetterDelegate));
        }

        /// <summary>
        /// This method generates the actual IL for the method.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <param name="il"></param>
        private static void GenerateIL(Type modelType, string path, ILGenerator il) {
            // Push our model object onto the stack
            il.Emit(OpCodes.Ldarg_0);
            OpCodes.Castclass
            // Generate the IL to access each part of the dotted chain
            Type type = modelType;
            string[] parts = path.Split('.');
            for (int i = 0; i < parts.Length; i++) {
                type = Generator.GeneratePart(il, type, parts[i], (i == parts.Length - 1));
                if (type == null)
                    break;
            }

            // If the object to be returned is a value type (e.g. int, bool), it
            // must be boxed, since the delegate returns an Object
            if (type != null && type.IsValueType && !modelType.IsValueType)
                il.Emit(OpCodes.Box, type);

            il.Emit(OpCodes.Ret);
        }

        private static Type GeneratePart(ILGenerator il, Type type, string pathPart, bool isLastPart) {
            // TODO: Generate check for null

            // Find the first member with the given nam that is a field, property, or parameter-less method
            List<MemberInfo> infos = new List<MemberInfo>(type.GetMember(pathPart));
            MemberInfo info = infos.Find(delegate(MemberInfo x) {
                if (x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property)
                    return true;
                if (x.MemberType == MemberTypes.Method)
                    return ((MethodInfo)x).GetParameters().Length == 0;
                else
                    return false;
            });

            // If we couldn't find anything with that name, pop the current result and return an error
            if (info == null) {
                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ldstr, String.Format("'{0}' is not a parameter-less method, property or field of type '{1}'", pathPart, type.FullName));
                return null;
            }

            // Generate the correct IL to access the member. We remember the type of object that is going to be returned
            // so that we can do a method lookup on it at the next iteration
            Type resultType = null;
            switch (info.MemberType) {
                case MemberTypes.Method:
                    MethodInfo mi = (MethodInfo)info;
                    if (mi.IsVirtual)
                        il.Emit(OpCodes.Callvirt, mi);
                    else
                        il.Emit(OpCodes.Call, mi);
                    resultType = mi.ReturnType;
                    break;
                case MemberTypes.Property:
                    PropertyInfo pi = (PropertyInfo)info;
                    il.Emit(OpCodes.Call, pi.GetGetMethod());
                    resultType = pi.PropertyType;
                    break;
                case MemberTypes.Field:
                    FieldInfo fi = (FieldInfo)info;
                    il.Emit(OpCodes.Ldfld, fi);
                    resultType = fi.FieldType;
                    break;
            }

            // If the method returned a value type, and something is going to call a method on that value,
            // we need to load its address onto the stack, rather than the object itself.
            if (resultType.IsValueType && !isLastPart) {
                LocalBuilder lb = il.DeclareLocal(resultType);
                il.Emit(OpCodes.Stloc, lb);
                il.Emit(OpCodes.Ldloca, lb);
            }

            return resultType;
        }

        #endregion
         */ 
    }
}
