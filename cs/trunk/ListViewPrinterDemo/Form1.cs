using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using BrightIdeasSoftware;


namespace ListViewPrinterDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadXmlDataIntoList();
            this.Rebuild();
            this.UpdatePrintPreview(null, null);
        }

        private void LoadXmlDataIntoList()
        {
            DataSet ds = new DataSet();
            try {
                ds.ReadXml("Persons.xml");
                LoadTableIntoList(this.listView1, ds.Tables["Person"]);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTableIntoList(ListView lv, DataTable table)
        {
            lv.BeginUpdate();

            lv.Items.Clear();
            foreach (DataRow row in table.Rows) {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = row[0].ToString();
                lvi.ImageIndex = Int32.Parse(row[1].ToString());

                for (int i = 2; i < table.Columns.Count; i++) {
                    lvi.SubItems.Add(row[i].ToString());
                }
                lv.Items.Add(lvi);
            }

            lv.EndUpdate();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1)
                this.UpdatePrintPreview(null, null);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 2.0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 1.0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 0.5;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 1.0;
            this.printPreviewControl1.AutoZoom = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PageSetup();
            this.UpdatePrintPreview(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintPreview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintWithDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int pages = (int)this.numericUpDown1.Value;

            switch (pages) {
                case 1:
                case 2:
                case 3:
                    this.printPreviewControl1.Rows = 1;
                    this.printPreviewControl1.Columns = pages;
                    break;
                default:
                    this.printPreviewControl1.Rows = 2;
                    this.printPreviewControl1.Columns = ((pages-1) / 2) + 1;
                    break;
            }
        }

        private void UpdatePrintPreview(object sender, EventArgs e)
        {
            this.listViewPrinter1.Header = this.tbHeader.Text.Replace("\\t", "\t");
            this.listViewPrinter1.Footer = this.tbFooter.Text.Replace("\\t", "\t");
            this.listViewPrinter1.Watermark = this.tbWatermark.Text;

            this.listViewPrinter1.IsShrinkToFit = this.cbShrinkToFit.Checked;
            //this.listViewPrinter1.IsTextOnly = !this.cbListHeaderOnEveryPage.Checked;
            this.listViewPrinter1.IsListHeaderOnEachPage = this.cbListHeaderOnEveryPage.Checked;
            this.listViewPrinter1.IsPrintSelectionOnly = this.cbPrintSelection.Checked;

            if (this.rbStyleMinimal.Checked == true)
                this.ApplyMinimalFormatting();
            else if (this.rbStyleModern.Checked == true)
                this.ApplyModernFormatting();
            else if (this.rbStyleOTT.Checked == true)
                this.ApplyOverTheTopFormatting();
            else if (this.rbStyleCustom.Checked == true)
                this.ApplyCustomFormatting();

            if (this.cbUseGridLines.Checked == false)
                this.listViewPrinter1.ListGridPen = null;

            //this.listViewPrinter1.FirstPage = (int)this.numericUpDown1.Value;
            //this.listViewPrinter1.LastPage = (int)this.numericUpDown2.Value;

            this.printPreviewControl1.InvalidatePreview();
        }

        /// <summary>
        /// Give the report a minimal set of default formatting values.
        /// </summary>
        public void ApplyMinimalFormatting()
        {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Tahoma", 9);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header();
            this.listViewPrinter1.HeaderFormat.TextBrush = Brushes.Black;
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = null;
            this.listViewPrinter1.HeaderFormat.SetBorderPen(Sides.Bottom, new Pen(Color.Black, 0.5f));

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer();
            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            this.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 2, new LinearGradientBrush(new Rectangle(0,0,1,1), Color.Gray, Color.White, LinearGradientMode.Horizontal));

            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader();
            this.listViewPrinter1.ListHeaderFormat.BackgroundBrush = null;

            this.listViewPrinter1.WatermarkFont = null;
            this.listViewPrinter1.WatermarkColor = Color.Empty;
        }

        /// <summary>
        /// Give the report a minimal set of default formatting values.
        /// </summary>
        public void ApplyModernFormatting()
        {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Ms Sans Serif", 9);
            this.listViewPrinter1.ListGridPen = new Pen(Color.DarkGray, 0.5f);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header(new Font("Verdana", 24, FontStyle.Bold));
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = new LinearGradientBrush(new Rectangle(0,0,1,1), Color.DarkBlue, Color.White, LinearGradientMode.Horizontal);

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer();
            this.listViewPrinter1.FooterFormat.BackgroundBrush = new LinearGradientBrush(new Rectangle(0, 0, 1, 1), Color.White, Color.DarkBlue, LinearGradientMode.Horizontal);

            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(new Font("Verdana", 12));

            this.listViewPrinter1.WatermarkFont = null;
            this.listViewPrinter1.WatermarkColor = Color.Empty;
        }

        /// <summary>
        /// Give the report a some outrageous formatting values.
        /// </summary>
        public void ApplyOverTheTopFormatting()
        {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Ms Sans Serif", 9);
            this.listViewPrinter1.ListGridPen = new Pen(Color.Blue, 0.5f);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header(new Font("Comic Sans MS", 36));
            this.listViewPrinter1.HeaderFormat.TextBrush = new LinearGradientBrush(new Rectangle(0, 0, 1, 1), Color.Black, Color.Blue, LinearGradientMode.Horizontal);

            this.listViewPrinter1.HeaderFormat.BackgroundBrush = new TextureBrush(this.listView1.SmallImageList.Images["music"], WrapMode.Tile);
            this.listViewPrinter1.HeaderFormat.SetBorder(Sides.All, 10, new LinearGradientBrush(new Rectangle(0, 0, 1, 1), Color.Purple, Color.Pink, LinearGradientMode.Horizontal));

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer(new Font("Comic Sans MS", 12));
            this.listViewPrinter1.FooterFormat.TextBrush = Brushes.Blue;
            this.listViewPrinter1.FooterFormat.BackgroundBrush = new LinearGradientBrush(new Rectangle(0, 0, 1, 1), Color.Gold, Color.Green, LinearGradientMode.Horizontal);
            this.listViewPrinter1.FooterFormat.SetBorder(Sides.All, 5, new SolidBrush(Color.FromArgb(128, Color.Green)));

            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            this.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 5, new HatchBrush(HatchStyle.LargeConfetti, Color.Blue, Color.Empty));

            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(new Font("Comic Sans MS", 12));
            this.listViewPrinter1.ListHeaderFormat.BackgroundBrush = Brushes.PowderBlue;
            this.listViewPrinter1.ListHeaderFormat.TextBrush = Brushes.Black;

            this.listViewPrinter1.WatermarkFont = new Font("Comic Sans MS", 72);
            this.listViewPrinter1.WatermarkColor = Color.Red;
        }


        /// <summary>
        /// Copy the formatting from the formatting that the user has setup on the custom format panel.
        /// </summary>
        public void ApplyCustomFormatting()
        {
        	this.listViewPrinter1.ListFont = this.listViewPrinter2.ListFont;
            this.listViewPrinter1.CellFormat = this.listViewPrinter2.CellFormat;
        	this.listViewPrinter1.HeaderFormat = this.listViewPrinter2.HeaderFormat;
        	this.listViewPrinter1.FooterFormat = this.listViewPrinter2.FooterFormat;
        	this.listViewPrinter1.GroupHeaderFormat = this.listViewPrinter2.GroupHeaderFormat;
        	this.listViewPrinter1.ListHeaderFormat = this.listViewPrinter2.ListHeaderFormat;

            this.ApplyPenData(this.listViewPrinter1.CellFormat);
            this.ApplyPenData(this.listViewPrinter1.HeaderFormat);
            this.ApplyPenData(this.listViewPrinter1.FooterFormat);
            this.ApplyPenData(this.listViewPrinter1.GroupHeaderFormat);
            this.ApplyPenData(this.listViewPrinter1.ListHeaderFormat);

        	this.listViewPrinter1.WatermarkFont = this.listViewPrinter2.WatermarkFont;
        	this.listViewPrinter1.WatermarkTransparency = this.listViewPrinter2.WatermarkTransparency;
        	this.listViewPrinter1.WatermarkColor = this.listViewPrinter2.WatermarkColor;
        }

        private void ApplyPenData(BlockFormat blockFormat)
        {
            blockFormat.BackgroundBrushData = blockFormat.BackgroundBrushData;
            blockFormat.BottomBorderPenData = blockFormat.BottomBorderPenData;
            blockFormat.LeftBorderPenData = blockFormat.LeftBorderPenData;
            blockFormat.RightBorderPenData = blockFormat.RightBorderPenData;
            blockFormat.TextBrushData = blockFormat.TextBrushData;
            blockFormat.TopBorderPenData = blockFormat.TopBorderPenData;
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.listView1.ShowGroups = this.checkBox1.Checked;
            this.Rebuild();
        }

        //-----------------------------------------------------------------------------
        // Include all the stupid normal stuff that a ListView needs to be useful
        // Better to use an ObjectListView!

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == this.lastSortColumn) {
                if (this.lastSortOrder == SortOrder.Ascending)
                    this.lastSortOrder = SortOrder.Descending;
                else
                    this.lastSortOrder = SortOrder.Ascending;
            }  else {
                this.lastSortOrder = SortOrder.Ascending;
                this.lastSortColumn = e.Column;
            }

            this.Rebuild();
        }
        private int lastSortColumn = 0;
        private SortOrder lastSortOrder = SortOrder.Ascending;

        private void Rebuild() 
        {
            if (this.listView1.ShowGroups)
                this.BuildGroups(this.lastSortColumn);
            else
                this.listView1.ListViewItemSorter = new ColumnComparer(this.lastSortColumn, this.lastSortOrder);
        }

        private void BuildGroups(int column)
        {
            this.listView1.Groups.Clear();

            // Getting the Count forces any internal cache of the ListView to be flushed. Without
            // this, iterating over the Items will not work correctly if the ListView handle
            // has not yet been created.
            int dummy = this.listView1.Items.Count;

            // Separate the list view items into groups, using the group key as the descrimanent
            Dictionary<String, List<ListViewItem>> map = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem lvi in this.listView1.Items) {
                String key = lvi.SubItems[column].Text;
                if (column == 0 && key.Length > 0)
                    key = key.Substring(0, 1);
                if (!map.ContainsKey(key))
                    map[key] = new List<ListViewItem>();
                map[key].Add(lvi);
            }

            // Make a list of the required groups
            List<ListViewGroup> groups = new List<ListViewGroup>();
            foreach (String key in map.Keys) {
                groups.Add(new ListViewGroup(key));
            }

            // Sort the groups
            groups.Sort(new ListViewGroupComparer(this.lastSortOrder));

            // Put each group into the list view, and give each group its member items.
            // The order of statements is important here:
            // - the header must be calculate before the group is added to the list view,
            //   otherwise changing the header causes a nasty redraw (even in the middle of a BeginUpdate...EndUpdate pair)
            // - the group must be added before it is given items, otherwise an exception is thrown (is this documented?)
            ColumnComparer itemSorter = new ColumnComparer(column,  this.lastSortOrder);
            foreach (ListViewGroup group in groups) {
                this.listView1.Groups.Add(group);
                map[group.Header].Sort(itemSorter);
                group.Items.AddRange(map[group.Header].ToArray());
            }
        }

        internal class ListViewGroupComparer : IComparer<ListViewGroup>
        {
            public ListViewGroupComparer(SortOrder order)
            {
                this.sortOrder = order;
            }

            public int Compare(ListViewGroup x, ListViewGroup y)
            {
                int result = String.Compare(x.Header, y.Header, true);

                if (this.sortOrder == SortOrder.Descending)
                    result = 0 - result;

                return result;
            }

            private SortOrder sortOrder;
        }

        internal class ColumnComparer : IComparer, IComparer<ListViewItem>
        {
            public ColumnComparer(int col, SortOrder order)
            {
                this.column = col;
                this.sortOrder = order;
            }

            public int Compare(object x, object y)
            {
                return this.Compare((ListViewItem)x, (ListViewItem)y);
            }

            public int Compare(ListViewItem x, ListViewItem y)
            {
                int result = String.Compare(x.SubItems[this.column].Text, y.SubItems[this.column].Text, true);

                if (this.sortOrder == SortOrder.Descending)
                    result = 0 - result;

                return result;

            }

            private int column;
            private SortOrder sortOrder;
        }
        
        void CheckBox1CheckedChanged(object sender, EventArgs e)
        {
        	this.listView1.ShowGroups = ((CheckBox)sender).Checked;
        	this.Rebuild();
        }
    }
}
