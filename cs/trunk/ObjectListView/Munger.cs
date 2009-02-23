/*
 * Munger - An Interface pattern on getting and setting values from object through Reflection
 *
 * Author: Phillip Piper
 * Date: 28/11/2008 17:15 
 *
 * Change log:
 * 2009-02-15  JPP  - Made Munger a public class
 * 2009-01-20  JPP  - Made the Munger capable of handling indexed access.
 *                    Incidentally, this removed the ugliness that the last change introduced.
 * 2009-01-18  JPP  - Handle target objects from a DataListView (normally DataRowViews)
 * v2.0
 * 2008-11-28  JPP  Initial version
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
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// An instance of Munger gets a value from or puts a value into a target object. The property
    /// to be peeked (or poked) is determined from a string. The peeking or poking is done using reflection.
    /// </summary>
    public class Munger
    {
        public Munger()
        {
        }

        public Munger(String aspectName)
        {
            this.AspectName = aspectName;
        }

        /// <summary>
        /// The name of the aspect that is to be peeked or poked.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This name can be a field, property or parameter-less method.
        /// </para>
        /// <para>
        /// The name can be dotted, which chains references. If any link in the chain returns
        /// null, the entire chain is considered to return null.
        /// </para>
        /// </remarks>
        /// <example>"DateOfBirth"</example>
        /// <example>"Owner.HomeAddress.Postcode"</example>
        public string AspectName
        {
            get { return aspectName; }
            set { 
                aspectName = value;
                if (String.IsNullOrEmpty(aspectName))
                    this.aspectNameParts = new List<string>();
                else
                    this.aspectNameParts = new List<string>(aspectName.Split('.'));
            }
        }
        private string aspectName;
        private List<String> aspectNameParts = new List<string>();

        /// <summary>
        /// Extract the value indicated by our AspectName from the given target.
        /// </summary>
        /// <param name="target">The object that will be peeked</param>
        /// <returns>The value read from the target</returns>
        public Object GetValue(Object target)
        {
            if (this.aspectNameParts.Count == 0)
                return null;

            // TODO: refactor this code with the same code that exists in SetValue()
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.GetField;
            const BindingFlags flags2 = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;
            foreach (String property in this.aspectNameParts) {
                if (target == null)
                    break;
                try {
                    target = target.GetType().InvokeMember(property, flags, null, target, null);
                }
                catch (MissingMethodException) {
                    // If that didn't work, try to use property as an indexer. This covers things like arrays
                    // dictionaries and DataRows
                    try {
                        target = target.GetType().InvokeMember("Item", flags2, null, target, new Object[] { property });
                    }
                    catch {
                        // We could catch MissingMethodException, KeyNotFoundException, TargetInvocationException plus
                        // others, but basically if anything goes wrong here, we give up
                        return String.Format("'{0}' is not a parameter-less method, property or field of type '{1}'", property, target.GetType());
                    }
                }
            }
            return target;
        }

        /// <summary>
        /// Poke the given value into the given target indicated by our AspectName.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the AspectName is a dotted path, all the selectors bar the last
        /// are used to find the object that should be updated, and the last
        /// selector is used as the property to update on that object.
        /// </para>
        /// <para>
        /// So, if 'target' is a Person and the AspectName is "HomeAddress.Postcode",
        /// this method will first fetch "HomeAddress" property, and then try to set the
        /// "Postcode" property on the home address object.
        /// </para>
        /// </remarks>
        /// <param name="target">The object that will be poked</param>
        /// <param name="value">The value that will be poked into the target</param>
        public void PutValue(Object target, Object value)
        {
            if (this.aspectNameParts.Count == 0)
                return;

            // Get the object to be poked
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.GetField;
            for (int i = 0; i < this.aspectNameParts.Count-1; i++) {
                if (target == null)
                    break;
                try {
                    target = target.GetType().InvokeMember(this.aspectNameParts[i], flags, null, target, null);
                }
                catch (System.MissingMethodException) {
                    // If that didn't work, try to use property as an indexer. This covers things like arrays
                    // dictionaries and DataRows
                    try {
                        const BindingFlags flags2 = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;
                        target = target.GetType().InvokeMember("Item", flags2, null, target, new Object[] { this.aspectNameParts[i] });
                    }
                    catch {
                        // We could catch MissingMethodException, KeyNotFoundException, TargetInvocationException plus
                        // others, but basically if anything goes wrong here, we give up

                        System.Diagnostics.Debug.WriteLine(String.Format("Cannot invoke '{0}' on a {1}", this.aspectNameParts[i], target.GetType()));
                        return;
                    }
                }
            }

            if (target == null)
                return;

            // Now try to set the value
            String lastPart = this.aspectNameParts[this.aspectNameParts.Count - 1];
            try {
                // Try to set a property or field first, since that's the most common case
                const BindingFlags flags3 = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | 
                    BindingFlags.SetProperty | BindingFlags.SetField;
                target.GetType().InvokeMember(lastPart, flags3, null, target, new Object[] { value });
            }
            catch (System.MissingMethodException ex) {
                try {
                    // If that failed, it could be method name that we are looking for
                    const BindingFlags flags4 = BindingFlags.Public | BindingFlags.NonPublic | 
                        BindingFlags.Instance | BindingFlags.InvokeMethod;
                    target.GetType().InvokeMember(lastPart, flags4, null, target, new Object[] { value });
                }
                catch (System.MissingMethodException ex2) {
                    // If that didn't work, try to use property as an indexer. This covers things like arrays
                    // dictionaries and DataRows
                    try {
                        const BindingFlags flags5 = BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty;
                        target = target.GetType().InvokeMember("Item", flags5, null, target,
                            new Object[] { lastPart, value });
                    }
                    catch {
                        // We could catch MissingMethodException, KeyNotFoundException, TargetInvocationException plus
                        // others, but basically if anything goes wrong here, we give up
                        System.Diagnostics.Debug.WriteLine("Invoke PutAspectByName failed:");
                        System.Diagnostics.Debug.WriteLine(ex);
                        System.Diagnostics.Debug.WriteLine(ex2);
                    }
                }
            }
        }
    }
}
