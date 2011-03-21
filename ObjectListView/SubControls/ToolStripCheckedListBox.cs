/*
 * ToolStripCheckedListBox - Puts a CheckedListBox into a tool strip menu item
 *
 * Author: Phillip Piper
 * Date: 4-March-2011 11:59 pm
 *
 * Change log:
 * 2011-03-04  JPP  - First version
 * 
 * Copyright (C) 2011 Phillip Piper
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
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BrightIdeasSoftware {

    /// <summary>
    /// Instances of this class put a CheckedListBox into a tool strip menu item.
    /// </summary>
    public class ToolStripCheckedListBox : ToolStripControlHost {

        public ToolStripCheckedListBox()
            : base(new CheckedListBox()) {
            this.CheckedListBoxControl.MaximumSize = new Size(400, 700);
            this.CheckedListBoxControl.ThreeDCheckBoxes = true;
            this.CheckedListBoxControl.CheckOnClick = true;
            this.CheckedListBoxControl.SelectionMode = SelectionMode.One;
        }

        public CheckedListBox CheckedListBoxControl {
            get {
                return Control as CheckedListBox;
            }
        }

        public CheckedListBox.ObjectCollection Items {
            get {
                return this.CheckedListBoxControl.Items;
            }
        }

        public bool CheckedOnClick {
            get {
                return this.CheckedListBoxControl.CheckOnClick;
            }
            set {
                this.CheckedListBoxControl.CheckOnClick = value;
            }
        }

        public CheckedListBox.CheckedItemCollection CheckedItems {
            get {
                return this.CheckedListBoxControl.CheckedItems;
            }
        }

        public void AddItem(object item, bool isChecked) {
            this.Items.Add(item);
            if (isChecked)
                this.CheckedListBoxControl.SetItemChecked(this.Items.Count - 1, true);
        }

        public void AddItem(object item, CheckState state) {
            this.Items.Add(item);
            this.CheckedListBoxControl.SetItemCheckState(this.Items.Count - 1, state);
        }

        public CheckState GetItemCheckState(int i) {
            return this.CheckedListBoxControl.GetItemCheckState(i);
        }

        public void SetItemState(int i, CheckState checkState) {
            if (i >= 0 && i < this.Items.Count)
                this.CheckedListBoxControl.SetItemCheckState(i, checkState);
        }

        public void CheckAll() {
            for (int i = 0; i < this.Items.Count; i++)
                this.CheckedListBoxControl.SetItemChecked(i, true);
        }

        public void UncheckAll() {
            for (int i = 0; i < this.Items.Count; i++)
                this.CheckedListBoxControl.SetItemChecked(i, false);
        }

        protected override void OnSubscribeControlEvents(Control c) {
            base.OnSubscribeControlEvents(c);

            CheckedListBox control = (CheckedListBox)c;
            control.ItemCheck += new ItemCheckEventHandler(OnItemCheck);
        }

        protected override void OnUnsubscribeControlEvents(Control c) {
            base.OnUnsubscribeControlEvents(c);

            CheckedListBox control = (CheckedListBox)c;
            control.ItemCheck -= new ItemCheckEventHandler(OnItemCheck);
        }

        public event ItemCheckEventHandler ItemCheck;

        private void OnItemCheck(object sender, ItemCheckEventArgs e) {
            if (ItemCheck != null) {
                ItemCheck(this, e);
            }
        }
    }
}
