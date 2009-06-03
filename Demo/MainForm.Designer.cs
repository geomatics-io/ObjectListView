/*
 * Created by SharpDevelop.
 * User: Viney
 * Date: 9/10/2006
 * Time: 2:14 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using BrightIdeasSoftware;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ObjectListViewDemo
{
	partial class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.command1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.command2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.command3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appearOnTheColumnHeadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox18 = new System.Windows.Forms.CheckBox();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewSimple = new BrightIdeasSoftware.ObjectListView();
            this.columnHeader11 = new BrightIdeasSoftware.OLVColumn();
            this.columnHeader12 = new BrightIdeasSoftware.OLVColumn();
            this.columnHeader13 = new BrightIdeasSoftware.OLVColumn();
            this.columnHeader14 = new BrightIdeasSoftware.OLVColumn();
            this.columnHeader15 = new BrightIdeasSoftware.OLVColumn();
            this.columnHeader16 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn34 = new BrightIdeasSoftware.OLVColumn();
            this.hotItemStyle1 = new BrightIdeasSoftware.HotItemStyle();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listViewComplex = new BrightIdeasSoftware.ObjectListView();
            this.personColumn = new BrightIdeasSoftware.OLVColumn();
            this.occupationColumn = new BrightIdeasSoftware.OLVColumn();
            this.columnCookingSkill = new BrightIdeasSoftware.OLVColumn();
            this.cookingSkillRenderer = new BrightIdeasSoftware.MultiImageRenderer();
            this.yearOfBirthColumn = new BrightIdeasSoftware.OLVColumn();
            this.birthdayColumn = new BrightIdeasSoftware.OLVColumn();
            this.hourlyRateColumn = new BrightIdeasSoftware.OLVColumn();
            this.moneyImageColumn = new BrightIdeasSoftware.OLVColumn();
            this.daysSinceBirthColumn = new BrightIdeasSoftware.OLVColumn();
            this.olvJokeColumn = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn41 = new BrightIdeasSoftware.OLVColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.checkBoxPause = new System.Windows.Forms.CheckBox();
            this.rowHeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.listViewDataSet = new BrightIdeasSoftware.DataListView();
            this.olvColumn1 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn2 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn3 = new BrightIdeasSoftware.OLVColumn();
            this.salaryColumn = new BrightIdeasSoftware.OLVColumn();
            this.salaryRenderer = new BrightIdeasSoftware.MultiImageRenderer();
            this.heightColumn = new BrightIdeasSoftware.OLVColumn();
            this.heightRenderer = new BrightIdeasSoftware.BarRenderer();
            this.olvColumn42 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnGif = new BrightIdeasSoftware.OLVColumn();
            this.imageRenderer1 = new BrightIdeasSoftware.ImageRenderer();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.comboBoxNagLevel = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewVirtual = new BrightIdeasSoftware.VirtualObjectListView();
            this.olvColumn4 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn12 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn5 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn7 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn8 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn9 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn10 = new BrightIdeasSoftware.OLVColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.buttonSaveState = new System.Windows.Forms.Button();
            this.buttonRestoreState = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listViewFiles = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnFileName = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnFileCreated = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnFileModified = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnSize = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnFileType = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnAttributes = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnFileExtension = new BrightIdeasSoftware.OLVColumn();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.cbCellGridLines = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.rbStyleTooMuch = new System.Windows.Forms.RadioButton();
            this.cbPrintOnlySelection = new System.Windows.Forms.CheckBox();
            this.rbStyleModern = new System.Windows.Forms.RadioButton();
            this.cbShrinkToFit = new System.Windows.Forms.CheckBox();
            this.rbStyleMinimal = new System.Windows.Forms.RadioButton();
            this.cbIncludeImages = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbFooter = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbHeader = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbWatermark = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbShowVirtual = new System.Windows.Forms.RadioButton();
            this.rbShowFileExplorer = new System.Windows.Forms.RadioButton();
            this.rbShowDataset = new System.Windows.Forms.RadioButton();
            this.rbShowComplex = new System.Windows.Forms.RadioButton();
            this.rbShowSimple = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.listViewPrinter1 = new BrightIdeasSoftware.ListViewPrinter();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.button19 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.olvFastList = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn18 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn19 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn26 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn27 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn28 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn29 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn31 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn32 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn33 = new BrightIdeasSoftware.OLVColumn();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.button28 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.treeColumnName = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnCreated = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnModified = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnSize = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnFileType = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnAttributes = new BrightIdeasSoftware.OLVColumn();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.checkBox22 = new System.Windows.Forms.CheckBox();
            this.checkBox21 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.olvGeeks = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn43 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn44 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn45 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn46 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn47 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn48 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn49 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn50 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn51 = new BrightIdeasSoftware.OLVColumn();
            this.olvFroods = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn52 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn53 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn54 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn55 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn56 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn57 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn58 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn59 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn60 = new BrightIdeasSoftware.OLVColumn();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.checkBox16 = new System.Windows.Forms.CheckBox();
            this.checkBox17 = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn35 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn36 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn37 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn38 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn39 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn40 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn21 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn22 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn23 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn30 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn24 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn25 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn20 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn17 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn13 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn14 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn15 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn6 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn11 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn16 = new BrightIdeasSoftware.OLVColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewSimple)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewComplex)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowHeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listViewDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewVirtual)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewFiles)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvFastList)).BeginInit();
            this.tabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.tabPage8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvGeeks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.olvFroods)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.command1ToolStripMenuItem,
            this.command2ToolStripMenuItem,
            this.command3ToolStripMenuItem,
            this.appearOnTheColumnHeadersToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(270, 92);
            // 
            // command1ToolStripMenuItem
            // 
            this.command1ToolStripMenuItem.Name = "command1ToolStripMenuItem";
            this.command1ToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.command1ToolStripMenuItem.Text = "Dummy commands...";
            // 
            // command2ToolStripMenuItem
            // 
            this.command2ToolStripMenuItem.Name = "command2ToolStripMenuItem";
            this.command2ToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.command2ToolStripMenuItem.Text = "...to test that a context menu...";
            // 
            // command3ToolStripMenuItem
            // 
            this.command3ToolStripMenuItem.Name = "command3ToolStripMenuItem";
            this.command3ToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.command3ToolStripMenuItem.Text = "...appears here and a different one...";
            // 
            // appearOnTheColumnHeadersToolStripMenuItem
            // 
            this.appearOnTheColumnHeadersToolStripMenuItem.Name = "appearOnTheColumnHeadersToolStripMenuItem";
            this.appearOnTheColumnHeadersToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.appearOnTheColumnHeadersToolStripMenuItem.Text = "...appear on the column headers.";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "compass");
            this.imageList1.Images.SetKeyName(1, "down");
            this.imageList1.Images.SetKeyName(2, "user");
            this.imageList1.Images.SetKeyName(3, "find");
            this.imageList1.Images.SetKeyName(4, "folder");
            this.imageList1.Images.SetKeyName(5, "movie");
            this.imageList1.Images.SetKeyName(6, "music");
            this.imageList1.Images.SetKeyName(7, "no");
            this.imageList1.Images.SetKeyName(8, "readonly");
            this.imageList1.Images.SetKeyName(9, "public");
            this.imageList1.Images.SetKeyName(10, "recycle");
            this.imageList1.Images.SetKeyName(11, "spanner");
            this.imageList1.Images.SetKeyName(12, "star");
            this.imageList1.Images.SetKeyName(13, "tick");
            this.imageList1.Images.SetKeyName(14, "archive");
            this.imageList1.Images.SetKeyName(15, "system");
            this.imageList1.Images.SetKeyName(16, "hidden");
            this.imageList1.Images.SetKeyName(17, "temporary");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "user");
            this.imageList2.Images.SetKeyName(1, "compass");
            this.imageList2.Images.SetKeyName(2, "down");
            this.imageList2.Images.SetKeyName(3, "find");
            this.imageList2.Images.SetKeyName(4, "folder");
            this.imageList2.Images.SetKeyName(5, "movie");
            this.imageList2.Images.SetKeyName(6, "music");
            this.imageList2.Images.SetKeyName(7, "no");
            this.imageList2.Images.SetKeyName(8, "readonly");
            this.imageList2.Images.SetKeyName(9, "public");
            this.imageList2.Images.SetKeyName(10, "recycle");
            this.imageList2.Images.SetKeyName(11, "spanner");
            this.imageList2.Images.SetKeyName(12, "star");
            this.imageList2.Images.SetKeyName(13, "tick");
            this.imageList2.Images.SetKeyName(14, "archive");
            this.imageList2.Images.SetKeyName(15, "system");
            this.imageList2.Images.SetKeyName(16, "hidden");
            this.imageList2.Images.SetKeyName(17, "temporary");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 529);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox18);
            this.tabPage1.Controls.Add(this.checkBox15);
            this.tabPage1.Controls.Add(this.comboBox6);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.button6);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.checkBox4);
            this.tabPage1.Controls.Add(this.checkBox3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.listViewSimple);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(760, 503);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Simple Example";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox18
            // 
            this.checkBox18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox18.Checked = true;
            this.checkBox18.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox18.Location = new System.Drawing.Point(260, 473);
            this.checkBox18.Name = "checkBox18";
            this.checkBox18.Size = new System.Drawing.Size(69, 24);
            this.checkBox18.TabIndex = 10;
            this.checkBox18.Text = "Hot &Item";
            this.checkBox18.UseVisualStyleBackColor = true;
            this.checkBox18.CheckedChanged += new System.EventHandler(this.checkBox18_CheckedChanged);
            // 
            // checkBox15
            // 
            this.checkBox15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox15.Location = new System.Drawing.Point(177, 473);
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size(95, 24);
            this.checkBox15.TabIndex = 9;
            this.checkBox15.Text = "&Lock Group";
            this.checkBox15.UseVisualStyleBackColor = true;
            this.checkBox15.CheckedChanged += new System.EventHandler(this.checkBox15_CheckedChanged);
            // 
            // comboBox6
            // 
            this.comboBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBox6.Location = new System.Drawing.Point(381, 473);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(83, 21);
            this.comboBox6.TabIndex = 4;
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(330, 478);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(48, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "&Editable:";
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(470, 473);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(46, 23);
            this.button7.TabIndex = 5;
            this.button7.Text = "&Add";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(522, 473);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(57, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "Re&move";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(585, 473);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Copy &Selection";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(677, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "&Rebuild List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // checkBox4
            // 
            this.checkBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox4.Location = new System.Drawing.Point(99, 473);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(83, 24);
            this.checkBox4.TabIndex = 2;
            this.checkBox4.Text = "Item &Count";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox3.Location = new System.Drawing.Point(6, 473);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(104, 24);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "Show &Groups";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.CheckBox3CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(748, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // listViewSimple
            // 
            this.listViewSimple.AllColumns.Add(this.columnHeader11);
            this.listViewSimple.AllColumns.Add(this.columnHeader12);
            this.listViewSimple.AllColumns.Add(this.columnHeader13);
            this.listViewSimple.AllColumns.Add(this.columnHeader14);
            this.listViewSimple.AllColumns.Add(this.columnHeader15);
            this.listViewSimple.AllColumns.Add(this.columnHeader16);
            this.listViewSimple.AllColumns.Add(this.olvColumn34);
            this.listViewSimple.AllowDrop = true;
            this.listViewSimple.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSimple.CheckBoxes = true;
            this.listViewSimple.CheckedAspectName = "CanTellJokes";
            this.listViewSimple.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.olvColumn34});
            this.listViewSimple.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewSimple.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSimple.FullRowSelect = true;
            this.listViewSimple.HideSelection = false;
            this.listViewSimple.HotItemStyle = this.hotItemStyle1;
            this.listViewSimple.IsSimpleDragSource = true;
            this.listViewSimple.IsSimpleDropSink = true;
            this.listViewSimple.Location = new System.Drawing.Point(6, 57);
            this.listViewSimple.Name = "listViewSimple";
            this.listViewSimple.OverlayImage.Image = global::ObjectListViewDemo.Resource1.limeleaf;
            this.listViewSimple.ShowGroups = false;
            this.listViewSimple.ShowImagesOnSubItems = true;
            this.listViewSimple.ShowItemToolTips = true;
            this.listViewSimple.Size = new System.Drawing.Size(748, 409);
            this.listViewSimple.SortGroupItemsByPrimaryColumn = false;
            this.listViewSimple.TabIndex = 0;
            this.listViewSimple.TintSortColumn = true;
            this.listViewSimple.TriStateCheckBoxes = true;
            this.listViewSimple.UseAlternatingBackColors = true;
            this.listViewSimple.UseCompatibleStateImageBehavior = false;
            this.listViewSimple.UseHotItem = true;
            this.listViewSimple.View = System.Windows.Forms.View.Details;
            this.listViewSimple.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.listViewSimple_Scroll);
            this.listViewSimple.SelectedIndexChanged += new System.EventHandler(this.ListViewSelectedIndexChanged);
            // 
            // columnHeader11
            // 
            this.columnHeader11.AspectName = "Name";
            this.columnHeader11.MaximumWidth = 200;
            this.columnHeader11.MinimumWidth = 100;
            this.columnHeader11.Text = "Person";
            this.columnHeader11.ToolTipText = "This is a long tooltip text that should appear when the mouse is over this column" +
                " header but contains absolutely no useful information :)";
            this.columnHeader11.UseInitialLetterForGroup = true;
            this.columnHeader11.Width = 140;
            // 
            // columnHeader12
            // 
            this.columnHeader12.AspectName = "Occupation";
            this.columnHeader12.MaximumWidth = 180;
            this.columnHeader12.MinimumWidth = 50;
            this.columnHeader12.Text = "Occupation";
            this.columnHeader12.Width = 112;
            // 
            // columnHeader13
            // 
            this.columnHeader13.AspectName = "CulinaryRating";
            this.columnHeader13.Text = "Cooking Skill";
            this.columnHeader13.Width = 74;
            // 
            // columnHeader14
            // 
            this.columnHeader14.AspectName = "YearOfBirth";
            this.columnHeader14.MaximumWidth = 81;
            this.columnHeader14.MinimumWidth = 81;
            this.columnHeader14.Text = "Year of birth";
            this.columnHeader14.Width = 81;
            // 
            // columnHeader15
            // 
            this.columnHeader15.AspectName = "BirthDate";
            this.columnHeader15.AspectToStringFormat = "{0:d}";
            this.columnHeader15.Text = "Birthday";
            this.columnHeader15.Width = 121;
            // 
            // columnHeader16
            // 
            this.columnHeader16.AspectName = "GetRate";
            this.columnHeader16.AspectToStringFormat = "{0:C}";
            this.columnHeader16.Text = "Hourly Rate";
            this.columnHeader16.Width = 93;
            // 
            // olvColumn34
            // 
            this.olvColumn34.AspectName = "Comments";
            this.olvColumn34.FillsFreeSpace = true;
            this.olvColumn34.IsTileViewColumn = true;
            this.olvColumn34.MinimumWidth = 30;
            this.olvColumn34.Text = "Comments";
            this.olvColumn34.ToolTipText = "This is the tool tip for the Comments column. This is configured through the Tool" +
                "TipText property.";
            this.olvColumn34.UseInitialLetterForGroup = true;
            // 
            // hotItemStyle1
            // 
            this.hotItemStyle1.BackColor = System.Drawing.Color.PeachPuff;
            this.hotItemStyle1.ForeColor = System.Drawing.Color.MediumBlue;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button16);
            this.tabPage2.Controls.Add(this.button17);
            this.tabPage2.Controls.Add(this.comboBox5);
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Controls.Add(this.checkBox6);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.listViewComplex);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 503);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Complex Example";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            this.button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button16.Location = new System.Drawing.Point(501, 473);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(46, 23);
            this.button16.TabIndex = 8;
            this.button16.Text = "Add";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button17.Location = new System.Drawing.Point(549, 473);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(57, 23);
            this.button17.TabIndex = 9;
            this.button17.Text = "Remove";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // comboBox5
            // 
            this.comboBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBox5.Location = new System.Drawing.Point(321, 473);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(71, 21);
            this.comboBox5.TabIndex = 5;
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(271, 477);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "Editable:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(397, 477);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "View:";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox1.Location = new System.Drawing.Point(431, 473);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(63, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox6.Checked = true;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox6.Location = new System.Drawing.Point(185, 473);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(86, 21);
            this.checkBox6.TabIndex = 3;
            this.checkBox6.Text = "Owner &Draw";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.CheckBox6CheckedChanged);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(608, 473);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "Copy &Selection";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(698, 473);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "&Rebuild";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(748, 48);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox2.Location = new System.Drawing.Point(101, 473);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(78, 21);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Item &Count";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox2CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.Location = new System.Drawing.Point(6, 473);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(92, 21);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Show &Groups";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
            // 
            // listViewComplex
            // 
            this.listViewComplex.AllColumns.Add(this.personColumn);
            this.listViewComplex.AllColumns.Add(this.occupationColumn);
            this.listViewComplex.AllColumns.Add(this.columnCookingSkill);
            this.listViewComplex.AllColumns.Add(this.yearOfBirthColumn);
            this.listViewComplex.AllColumns.Add(this.birthdayColumn);
            this.listViewComplex.AllColumns.Add(this.hourlyRateColumn);
            this.listViewComplex.AllColumns.Add(this.moneyImageColumn);
            this.listViewComplex.AllColumns.Add(this.daysSinceBirthColumn);
            this.listViewComplex.AllColumns.Add(this.olvJokeColumn);
            this.listViewComplex.AllColumns.Add(this.olvColumn41);
            this.listViewComplex.AllowColumnReorder = true;
            this.listViewComplex.AllowDrop = true;
            this.listViewComplex.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.listViewComplex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewComplex.BackColor = System.Drawing.SystemColors.Window;
            this.listViewComplex.CheckBoxes = true;
            this.listViewComplex.CheckedAspectName = "IsActive";
            this.listViewComplex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.personColumn,
            this.occupationColumn,
            this.columnCookingSkill,
            this.birthdayColumn,
            this.hourlyRateColumn,
            this.moneyImageColumn,
            this.daysSinceBirthColumn,
            this.olvJokeColumn,
            this.olvColumn41});
            this.listViewComplex.EmptyListMsg = "This list is empty. Press \"Add\" to create some items";
            this.listViewComplex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewComplex.FullRowSelect = true;
            this.listViewComplex.GroupWithItemCountFormat = "{0} ({1} people)";
            this.listViewComplex.GroupWithItemCountSingularFormat = "{0} ({1} person)";
            this.listViewComplex.HideSelection = false;
            this.listViewComplex.LargeImageList = this.imageList2;
            this.listViewComplex.Location = new System.Drawing.Point(6, 57);
            this.listViewComplex.Name = "listViewComplex";
            this.listViewComplex.OverlayImage.Image = global::ObjectListViewDemo.Resource1.redbull;
            this.listViewComplex.OverlayText.Alignment = System.Drawing.ContentAlignment.TopRight;
            this.listViewComplex.OverlayText.BorderColor = System.Drawing.Color.DarkRed;
            this.listViewComplex.OverlayText.BorderWidth = 4F;
            this.listViewComplex.OverlayText.InsetX = 10;
            this.listViewComplex.OverlayText.InsetY = 35;
            this.listViewComplex.OverlayText.Rotation = 20;
            this.listViewComplex.OverlayText.Text = "";
            this.listViewComplex.OverlayText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listViewComplex.OwnerDraw = true;
            this.listViewComplex.ShowGroups = false;
            this.listViewComplex.ShowImagesOnSubItems = true;
            this.listViewComplex.ShowItemToolTips = true;
            this.listViewComplex.Size = new System.Drawing.Size(748, 409);
            this.listViewComplex.SmallImageList = this.imageList1;
            this.listViewComplex.TabIndex = 0;
            this.listViewComplex.UseAlternatingBackColors = true;
            this.listViewComplex.UseCompatibleStateImageBehavior = false;
            this.listViewComplex.UseHotItem = true;
            this.listViewComplex.UseSubItemCheckBoxes = true;
            this.listViewComplex.View = System.Windows.Forms.View.Details;
            this.listViewComplex.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.listViewComplex_CellEditFinishing);
            this.listViewComplex.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.listViewComplex_CellEditStarting);
            this.listViewComplex.CellEditValidating += new BrightIdeasSoftware.CellEditEventHandler(this.listViewComplex_CellEditValidating);
            this.listViewComplex.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.listViewComplex_CellToolTip);
            this.listViewComplex.HeaderToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.listViewComplex_HeaderToolTipShowing);
            this.listViewComplex.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewComplex_MouseClick);
            this.listViewComplex.SelectedIndexChanged += new System.EventHandler(this.ListViewSelectedIndexChanged);
            // 
            // personColumn
            // 
            this.personColumn.AspectName = "Name";
            this.personColumn.Text = "Person";
            this.personColumn.ToolTipText = "Tooltip for Person column. This was configurated in the IDE. (Hold down Control t" +
                "o see a different tooltip)";
            this.personColumn.UseInitialLetterForGroup = true;
            this.personColumn.Width = 114;
            // 
            // occupationColumn
            // 
            this.occupationColumn.AspectName = "Occupation";
            this.occupationColumn.IsTileViewColumn = true;
            this.occupationColumn.Text = "Occupation";
            this.occupationColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.occupationColumn.Width = 92;
            // 
            // columnCookingSkill
            // 
            this.columnCookingSkill.AspectName = "CulinaryRating";
            this.columnCookingSkill.GroupWithItemCountFormat = "{0} ({1} candidates)";
            this.columnCookingSkill.GroupWithItemCountSingularFormat = "{0} (only {1} candidate)";
            this.columnCookingSkill.Renderer = this.cookingSkillRenderer;
            this.columnCookingSkill.Text = "Cooking skill";
            this.columnCookingSkill.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnCookingSkill.Width = 75;
            // 
            // cookingSkillRenderer
            // 
            this.cookingSkillRenderer.ImageName = "star";
            this.cookingSkillRenderer.MaximumValue = 40;
            this.cookingSkillRenderer.MaxNumberImages = 5;
            // 
            // yearOfBirthColumn
            // 
            this.yearOfBirthColumn.AspectName = "YearOfBirth";
            this.yearOfBirthColumn.DisplayIndex = 3;
            this.yearOfBirthColumn.IsVisible = false;
            this.yearOfBirthColumn.Text = "Year Of Birth";
            this.yearOfBirthColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.yearOfBirthColumn.Width = 80;
            // 
            // birthdayColumn
            // 
            this.birthdayColumn.AspectName = "BirthDate";
            this.birthdayColumn.AspectToStringFormat = "{0:D}";
            this.birthdayColumn.GroupWithItemCountFormat = "{0} has {1} birthdays";
            this.birthdayColumn.GroupWithItemCountSingularFormat = "{0} has only {1} birthday";
            this.birthdayColumn.IsTileViewColumn = true;
            this.birthdayColumn.Text = "Birthday";
            this.birthdayColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.birthdayColumn.Width = 111;
            // 
            // hourlyRateColumn
            // 
            this.hourlyRateColumn.AspectName = "GetRate";
            this.hourlyRateColumn.AspectToStringFormat = "{0:C}";
            this.hourlyRateColumn.IsTileViewColumn = true;
            this.hourlyRateColumn.Text = "Hourly Rate";
            this.hourlyRateColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hourlyRateColumn.Width = 71;
            // 
            // moneyImageColumn
            // 
            this.moneyImageColumn.IsEditable = false;
            this.moneyImageColumn.Text = "Salary";
            this.moneyImageColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.moneyImageColumn.Width = 55;
            // 
            // daysSinceBirthColumn
            // 
            this.daysSinceBirthColumn.IsEditable = false;
            this.daysSinceBirthColumn.Text = "Days Since Birth";
            this.daysSinceBirthColumn.Width = 81;
            // 
            // olvJokeColumn
            // 
            this.olvJokeColumn.AspectName = "CanTellJokes";
            this.olvJokeColumn.CheckBoxes = true;
            this.olvJokeColumn.Text = "Tells Jokes?";
            this.olvJokeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvJokeColumn.Width = 74;
            // 
            // olvColumn41
            // 
            this.olvColumn41.AspectName = "MaritalStatus";
            this.olvColumn41.IsTileViewColumn = true;
            this.olvColumn41.Text = "Married?";
            this.olvColumn41.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn41.ToolTipText = "Married column info with a much longer value";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(760, 503);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DataSet Example";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.comboBox7);
            this.groupBox3.Controls.Add(this.checkBoxPause);
            this.groupBox3.Controls.Add(this.rowHeightUpDown);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comboBox3);
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Controls.Add(this.listViewDataSet);
            this.groupBox3.Controls.Add(this.checkBox7);
            this.groupBox3.Controls.Add(this.checkBox8);
            this.groupBox3.Location = new System.Drawing.Point(6, 263);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(748, 234);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data List View";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(638, 133);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 13);
            this.label22.TabIndex = 7;
            this.label22.Text = "&Editable:";
            // 
            // comboBox7
            // 
            this.comboBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBox7.Location = new System.Drawing.Point(638, 148);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(104, 21);
            this.comboBox7.TabIndex = 8;
            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.comboBox7_SelectedIndexChanged);
            // 
            // checkBoxPause
            // 
            this.checkBoxPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPause.Checked = true;
            this.checkBoxPause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPause.Location = new System.Drawing.Point(638, 73);
            this.checkBoxPause.Name = "checkBoxPause";
            this.checkBoxPause.Size = new System.Drawing.Size(113, 19);
            this.checkBoxPause.TabIndex = 4;
            this.checkBoxPause.Text = "Pause &Animations";
            this.checkBoxPause.UseVisualStyleBackColor = true;
            this.checkBoxPause.CheckedChanged += new System.EventHandler(this.checkBoxPause_CheckedChanged);
            // 
            // rowHeightUpDown
            // 
            this.rowHeightUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rowHeightUpDown.Location = new System.Drawing.Point(638, 187);
            this.rowHeightUpDown.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.rowHeightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.rowHeightUpDown.Name = "rowHeightUpDown";
            this.rowHeightUpDown.Size = new System.Drawing.Size(101, 20);
            this.rowHeightUpDown.TabIndex = 10;
            this.rowHeightUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.rowHeightUpDown.ValueChanged += new System.EventHandler(this.rowHeightUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(638, 172);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Row &Height:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(638, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "&View:";
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox3.Location = new System.Drawing.Point(638, 108);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(104, 21);
            this.comboBox3.TabIndex = 6;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(638, 55);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(113, 19);
            this.checkBox5.TabIndex = 3;
            this.checkBox5.Text = "Owner &Draw";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.CheckBox5CheckedChanged);
            // 
            // listViewDataSet
            // 
            this.listViewDataSet.AllColumns.Add(this.olvColumn1);
            this.listViewDataSet.AllColumns.Add(this.olvColumn2);
            this.listViewDataSet.AllColumns.Add(this.olvColumn3);
            this.listViewDataSet.AllColumns.Add(this.salaryColumn);
            this.listViewDataSet.AllColumns.Add(this.heightColumn);
            this.listViewDataSet.AllColumns.Add(this.olvColumn42);
            this.listViewDataSet.AllColumns.Add(this.olvColumnGif);
            this.listViewDataSet.AllowColumnReorder = true;
            this.listViewDataSet.AllowDrop = true;
            this.listViewDataSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDataSet.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.listViewDataSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.salaryColumn,
            this.heightColumn,
            this.olvColumn42,
            this.olvColumnGif});
            this.listViewDataSet.DataSource = null;
            this.listViewDataSet.EmptyListMsg = "Add rows to the above table to see them here";
            this.listViewDataSet.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewDataSet.GridLines = true;
            this.listViewDataSet.GroupWithItemCountFormat = "{0} ({1} people)";
            this.listViewDataSet.GroupWithItemCountSingularFormat = "{0} (1 person)";
            this.listViewDataSet.HideSelection = false;
            this.listViewDataSet.HighlightBackgroundColor = System.Drawing.Color.Crimson;
            this.listViewDataSet.HighlightForegroundColor = System.Drawing.Color.DarkGreen;
            this.listViewDataSet.LargeImageList = this.imageList2;
            this.listViewDataSet.Location = new System.Drawing.Point(6, 19);
            this.listViewDataSet.Name = "listViewDataSet";
            this.listViewDataSet.OwnerDraw = true;
            this.listViewDataSet.ShowGroups = false;
            this.listViewDataSet.ShowImagesOnSubItems = true;
            this.listViewDataSet.ShowItemToolTips = true;
            this.listViewDataSet.Size = new System.Drawing.Size(626, 209);
            this.listViewDataSet.SmallImageList = this.imageList1;
            this.listViewDataSet.TabIndex = 0;
            this.listViewDataSet.UseCompatibleStateImageBehavior = false;
            this.listViewDataSet.UseHotItem = true;
            this.listViewDataSet.View = System.Windows.Forms.View.Details;
            this.listViewDataSet.SelectedIndexChanged += new System.EventHandler(this.ListViewDataSetSelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.UseInitialLetterForGroup = true;
            this.olvColumn1.Width = 112;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Company";
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "Company";
            this.olvColumn2.Width = 104;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Occupation";
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.Text = "Occupation";
            this.olvColumn3.Width = 109;
            // 
            // salaryColumn
            // 
            this.salaryColumn.AspectName = "Salary";
            this.salaryColumn.AspectToStringFormat = "{0:C}";
            this.salaryColumn.Renderer = this.salaryRenderer;
            this.salaryColumn.Text = "Salary";
            this.salaryColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.salaryColumn.Width = 84;
            // 
            // salaryRenderer
            // 
            this.salaryRenderer.ImageName = "tick";
            this.salaryRenderer.MaximumValue = 500000;
            this.salaryRenderer.MaxNumberImages = 5;
            this.salaryRenderer.MinimumValue = 10000;
            // 
            // heightColumn
            // 
            this.heightColumn.AspectName = "Height";
            this.heightColumn.Renderer = this.heightRenderer;
            this.heightColumn.Text = "Height (m)";
            this.heightColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.heightColumn.Width = 72;
            // 
            // heightRenderer
            // 
            this.heightRenderer.BackgroundColor = System.Drawing.Color.Green;
            this.heightRenderer.MaximumValue = 2;
            this.heightRenderer.UseStandardBar = false;
            // 
            // olvColumn42
            // 
            this.olvColumn42.AspectName = "TellsJokes";
            this.olvColumn42.CheckBoxes = true;
            this.olvColumn42.Text = "Joker?";
            this.olvColumn42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn42.Width = 48;
            // 
            // olvColumnGif
            // 
            this.olvColumnGif.AspectName = "GifFileName";
            this.olvColumnGif.Renderer = this.imageRenderer1;
            this.olvColumnGif.Text = "Animated GIF";
            this.olvColumnGif.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnGif.Width = 96;
            // 
            // checkBox7
            // 
            this.checkBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox7.Location = new System.Drawing.Point(638, 18);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(104, 20);
            this.checkBox7.TabIndex = 1;
            this.checkBox7.Text = "Show &Groups";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.CheckBox7CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox8.Location = new System.Drawing.Point(638, 37);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(113, 19);
            this.checkBox8.TabIndex = 2;
            this.checkBox8.Text = "Show Item &Counts";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.CheckBox8CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(6, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 201);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Data Table";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(626, 176);
            this.dataGridView1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(638, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "&Reset Data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(748, 46);
            this.label3.TabIndex = 9;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.comboBoxNagLevel);
            this.tabPage4.Controls.Add(this.label36);
            this.tabPage4.Controls.Add(this.comboBox8);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.button9);
            this.tabPage4.Controls.Add(this.button8);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.comboBox2);
            this.tabPage4.Controls.Add(this.checkBox9);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.listViewVirtual);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(760, 503);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Virtual List";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // comboBoxNagLevel
            // 
            this.comboBoxNagLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxNagLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNagLevel.FormattingEnabled = true;
            this.comboBoxNagLevel.Items.AddRange(new object[] {
            "Slight",
            "Expired",
            "Extreme"});
            this.comboBoxNagLevel.Location = new System.Drawing.Point(158, 472);
            this.comboBoxNagLevel.Name = "comboBoxNagLevel";
            this.comboBoxNagLevel.Size = new System.Drawing.Size(83, 21);
            this.comboBoxNagLevel.TabIndex = 9;
            this.comboBoxNagLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxNagLevel_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(104, 476);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(55, 13);
            this.label36.TabIndex = 8;
            this.label36.Text = "Nag level:";
            // 
            // comboBox8
            // 
            this.comboBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBox8.Location = new System.Drawing.Point(360, 472);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(83, 21);
            this.comboBox8.TabIndex = 3;
            this.comboBox8.SelectedIndexChanged += new System.EventHandler(this.comboBox8_SelectedIndexChanged);
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(306, 476);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(48, 13);
            this.label23.TabIndex = 2;
            this.label23.Text = "Editable:";
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(598, 471);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 6;
            this.button9.Text = "Deselect All";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(679, 471);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = "Select All";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(453, 476);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "View:";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox2.Location = new System.Drawing.Point(493, 472);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(99, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // checkBox9
            // 
            this.checkBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox9.Checked = true;
            this.checkBox9.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox9.Location = new System.Drawing.Point(6, 472);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(113, 21);
            this.checkBox9.TabIndex = 1;
            this.checkBox9.Text = "Owner &Draw";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.CheckBox9CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(6, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(748, 48);
            this.label4.TabIndex = 5;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // listViewVirtual
            // 
            this.listViewVirtual.AllColumns.Add(this.olvColumn4);
            this.listViewVirtual.AllColumns.Add(this.olvColumn12);
            this.listViewVirtual.AllColumns.Add(this.olvColumn5);
            this.listViewVirtual.AllColumns.Add(this.olvColumn7);
            this.listViewVirtual.AllColumns.Add(this.olvColumn8);
            this.listViewVirtual.AllColumns.Add(this.olvColumn9);
            this.listViewVirtual.AllColumns.Add(this.olvColumn10);
            this.listViewVirtual.AllowColumnReorder = true;
            this.listViewVirtual.AllowDrop = true;
            this.listViewVirtual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewVirtual.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.listViewVirtual.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4,
            this.olvColumn12,
            this.olvColumn5,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9,
            this.olvColumn10});
            this.listViewVirtual.EmptyListMsg = "Will this work?";
            this.listViewVirtual.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewVirtual.GridLines = true;
            this.listViewVirtual.HideSelection = false;
            this.listViewVirtual.LargeImageList = this.imageList2;
            this.listViewVirtual.Location = new System.Drawing.Point(6, 57);
            this.listViewVirtual.Name = "listViewVirtual";
            this.listViewVirtual.OverlayTransparency = 254;
            this.listViewVirtual.OwnerDraw = true;
            this.listViewVirtual.ShowGroups = false;
            this.listViewVirtual.ShowImagesOnSubItems = true;
            this.listViewVirtual.ShowItemToolTips = true;
            this.listViewVirtual.Size = new System.Drawing.Size(748, 409);
            this.listViewVirtual.SmallImageList = this.imageList1;
            this.listViewVirtual.TabIndex = 0;
            this.listViewVirtual.TintSortColumn = true;
            this.listViewVirtual.UseAlternatingBackColors = true;
            this.listViewVirtual.UseCompatibleStateImageBehavior = false;
            this.listViewVirtual.View = System.Windows.Forms.View.Details;
            this.listViewVirtual.VirtualListSize = 10000000;
            this.listViewVirtual.VirtualMode = true;
            this.listViewVirtual.SelectionChanged += new System.EventHandler(this.listViewVirtual_SelectionChanged);
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Name";
            this.olvColumn4.Text = "Person";
            this.olvColumn4.UseInitialLetterForGroup = true;
            this.olvColumn4.Width = 130;
            // 
            // olvColumn12
            // 
            this.olvColumn12.AspectName = "serialNumber";
            this.olvColumn12.IsEditable = false;
            this.olvColumn12.Text = "Serial #";
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Occupation";
            this.olvColumn5.Text = "Occupation";
            this.olvColumn5.Width = 100;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "CulinaryRating";
            this.olvColumn7.Text = "Cooking skill";
            this.olvColumn7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn7.Width = 80;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "YearOfBirth";
            this.olvColumn8.Text = "Year Of Birth";
            this.olvColumn8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn8.Width = 80;
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "BirthDate";
            this.olvColumn9.AspectToStringFormat = "{0:D}";
            this.olvColumn9.Text = "Birthday";
            this.olvColumn9.Width = 120;
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "GetRate";
            this.olvColumn10.AspectToStringFormat = "{0:C}";
            this.olvColumn10.Text = "Hourly Rate";
            this.olvColumn10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn10.Width = 80;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.buttonSaveState);
            this.tabPage5.Controls.Add(this.buttonRestoreState);
            this.tabPage5.Controls.Add(this.button13);
            this.tabPage5.Controls.Add(this.buttonUp);
            this.tabPage5.Controls.Add(this.buttonGo);
            this.tabPage5.Controls.Add(this.textBoxFolderPath);
            this.tabPage5.Controls.Add(this.label10);
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Controls.Add(this.comboBox4);
            this.tabPage5.Controls.Add(this.checkBox10);
            this.tabPage5.Controls.Add(this.checkBox11);
            this.tabPage5.Controls.Add(this.checkBox12);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.listViewFiles);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(760, 503);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "File Explorer";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // buttonSaveState
            // 
            this.buttonSaveState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveState.Location = new System.Drawing.Point(457, 474);
            this.buttonSaveState.Name = "buttonSaveState";
            this.buttonSaveState.Size = new System.Drawing.Size(87, 23);
            this.buttonSaveState.TabIndex = 10;
            this.buttonSaveState.Text = "Save State";
            this.buttonSaveState.UseVisualStyleBackColor = true;
            this.buttonSaveState.Click += new System.EventHandler(this.buttonSaveState_Click);
            // 
            // buttonRestoreState
            // 
            this.buttonRestoreState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestoreState.Enabled = false;
            this.buttonRestoreState.Location = new System.Drawing.Point(550, 474);
            this.buttonRestoreState.Name = "buttonRestoreState";
            this.buttonRestoreState.Size = new System.Drawing.Size(83, 23);
            this.buttonRestoreState.TabIndex = 11;
            this.buttonRestoreState.Text = "Restore State";
            this.buttonRestoreState.UseVisualStyleBackColor = true;
            this.buttonRestoreState.Click += new System.EventHandler(this.buttonRestoreState_Click);
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.Location = new System.Drawing.Point(639, 474);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(115, 23);
            this.button13.TabIndex = 12;
            this.button13.Text = "&Choose Columns...";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUp.Location = new System.Drawing.Point(679, 55);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(75, 23);
            this.buttonUp.TabIndex = 3;
            this.buttonUp.Text = "&Up";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(598, 55);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 2;
            this.buttonGo.Text = "&Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.ButtonGoClick);
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolderPath.Location = new System.Drawing.Point(56, 57);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.Size = new System.Drawing.Size(536, 20);
            this.textBoxFolderPath.TabIndex = 1;
            this.textBoxFolderPath.TextChanged += new System.EventHandler(this.TextBoxFolderPathTextChanged);
            this.textBoxFolderPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFolderPath_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "&Folder:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(303, 479);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "View:";
            // 
            // comboBox4
            // 
            this.comboBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox4.Location = new System.Drawing.Point(337, 474);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(114, 21);
            this.comboBox4.TabIndex = 9;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.ComboBox4SelectedIndexChanged);
            // 
            // checkBox10
            // 
            this.checkBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox10.Checked = true;
            this.checkBox10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox10.Location = new System.Drawing.Point(218, 477);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(90, 19);
            this.checkBox10.TabIndex = 7;
            this.checkBox10.Text = "Owner &Draw";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.CheckedChanged += new System.EventHandler(this.CheckBox10CheckedChanged);
            // 
            // checkBox11
            // 
            this.checkBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox11.Location = new System.Drawing.Point(101, 474);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(111, 24);
            this.checkBox11.TabIndex = 6;
            this.checkBox11.Text = "Show Item &Count";
            this.checkBox11.UseVisualStyleBackColor = true;
            this.checkBox11.CheckedChanged += new System.EventHandler(this.CheckBox11CheckedChanged);
            // 
            // checkBox12
            // 
            this.checkBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox12.Location = new System.Drawing.Point(6, 474);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(104, 24);
            this.checkBox12.TabIndex = 5;
            this.checkBox12.Text = "Show &Groups";
            this.checkBox12.UseVisualStyleBackColor = true;
            this.checkBox12.CheckedChanged += new System.EventHandler(this.CheckBox12CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(748, 46);
            this.label5.TabIndex = 6;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // listViewFiles
            // 
            this.listViewFiles.AllColumns.Add(this.olvColumnFileName);
            this.listViewFiles.AllColumns.Add(this.olvColumnFileCreated);
            this.listViewFiles.AllColumns.Add(this.olvColumnFileModified);
            this.listViewFiles.AllColumns.Add(this.olvColumnSize);
            this.listViewFiles.AllColumns.Add(this.olvColumnFileType);
            this.listViewFiles.AllColumns.Add(this.olvColumnAttributes);
            this.listViewFiles.AllColumns.Add(this.treeColumnFileExtension);
            this.listViewFiles.AllowColumnReorder = true;
            this.listViewFiles.AllowDrop = true;
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnFileName,
            this.olvColumnFileCreated,
            this.olvColumnFileModified,
            this.olvColumnSize,
            this.olvColumnFileType,
            this.olvColumnAttributes});
            this.listViewFiles.EmptyListMsg = "This folder is completely empty!";
            this.listViewFiles.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.LargeImageList = this.imageList2;
            this.listViewFiles.Location = new System.Drawing.Point(6, 83);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.OwnerDraw = true;
            this.listViewFiles.RowHeight = 20;
            this.listViewFiles.ShowGroups = false;
            this.listViewFiles.Size = new System.Drawing.Size(748, 385);
            this.listViewFiles.SmallImageList = this.imageList1;
            this.listViewFiles.TabIndex = 13;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.ItemActivate += new System.EventHandler(this.listViewFiles_ItemActivate);
            // 
            // olvColumnFileName
            // 
            this.olvColumnFileName.AspectName = "Name";
            this.olvColumnFileName.IsTileViewColumn = true;
            this.olvColumnFileName.Text = "Name";
            this.olvColumnFileName.UseInitialLetterForGroup = true;
            this.olvColumnFileName.Width = 180;
            // 
            // olvColumnFileCreated
            // 
            this.olvColumnFileCreated.AspectName = "CreationTime";
            this.olvColumnFileCreated.DisplayIndex = 4;
            this.olvColumnFileCreated.Text = "Created";
            this.olvColumnFileCreated.Width = 131;
            // 
            // olvColumnFileModified
            // 
            this.olvColumnFileModified.AspectName = "LastWriteTime";
            this.olvColumnFileModified.DisplayIndex = 1;
            this.olvColumnFileModified.IsTileViewColumn = true;
            this.olvColumnFileModified.Text = "Modified";
            this.olvColumnFileModified.Width = 145;
            // 
            // olvColumnSize
            // 
            this.olvColumnSize.AspectName = "Extension";
            this.olvColumnSize.DisplayIndex = 2;
            this.olvColumnSize.Text = "Size";
            this.olvColumnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnSize.Width = 80;
            // 
            // olvColumnFileType
            // 
            this.olvColumnFileType.DisplayIndex = 3;
            this.olvColumnFileType.IsTileViewColumn = true;
            this.olvColumnFileType.Text = "File Type";
            this.olvColumnFileType.Width = 148;
            // 
            // olvColumnAttributes
            // 
            this.olvColumnAttributes.FillsFreeSpace = true;
            this.olvColumnAttributes.IsEditable = false;
            this.olvColumnAttributes.MinimumWidth = 20;
            this.olvColumnAttributes.Text = "Attributes";
            // 
            // treeColumnFileExtension
            // 
            this.treeColumnFileExtension.AspectName = "Extension";
            this.treeColumnFileExtension.IsVisible = false;
            this.treeColumnFileExtension.Text = "Extension";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox5);
            this.tabPage6.Controls.Add(this.groupBox4);
            this.tabPage6.Controls.Add(this.groupBox2);
            this.tabPage6.Controls.Add(this.label12);
            this.tabPage6.Controls.Add(this.button12);
            this.tabPage6.Controls.Add(this.button11);
            this.tabPage6.Controls.Add(this.button10);
            this.tabPage6.Controls.Add(this.printPreviewControl1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(760, 503);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Printing ListViews";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numericUpDown2);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.numericUpDown1);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.cbCellGridLines);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.rbStyleTooMuch);
            this.groupBox5.Controls.Add(this.cbPrintOnlySelection);
            this.groupBox5.Controls.Add(this.rbStyleModern);
            this.groupBox5.Controls.Add(this.cbShrinkToFit);
            this.groupBox5.Controls.Add(this.rbStyleMinimal);
            this.groupBox5.Controls.Add(this.cbIncludeImages);
            this.groupBox5.Location = new System.Drawing.Point(144, 39);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(301, 109);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Format";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(200, 84);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown2.TabIndex = 11;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.Location = new System.Drawing.Point(163, 85);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 19);
            this.label19.TabIndex = 10;
            this.label19.Text = "to:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(99, 83);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.Location = new System.Drawing.Point(18, 86);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 19);
            this.label18.TabIndex = 8;
            this.label18.Text = "Page range:";
            // 
            // cbCellGridLines
            // 
            this.cbCellGridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCellGridLines.AutoSize = true;
            this.cbCellGridLines.Checked = true;
            this.cbCellGridLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCellGridLines.Location = new System.Drawing.Point(200, 62);
            this.cbCellGridLines.Name = "cbCellGridLines";
            this.cbCellGridLines.Size = new System.Drawing.Size(87, 17);
            this.cbCellGridLines.TabIndex = 7;
            this.cbCellGridLines.Text = "Cell grid lines";
            this.cbCellGridLines.UseVisualStyleBackColor = true;
            this.cbCellGridLines.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.Location = new System.Drawing.Point(19, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 19);
            this.label17.TabIndex = 0;
            this.label17.Text = "Style:";
            // 
            // rbStyleTooMuch
            // 
            this.rbStyleTooMuch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbStyleTooMuch.Location = new System.Drawing.Point(200, 15);
            this.rbStyleTooMuch.Name = "rbStyleTooMuch";
            this.rbStyleTooMuch.Size = new System.Drawing.Size(78, 24);
            this.rbStyleTooMuch.TabIndex = 3;
            this.rbStyleTooMuch.Text = "Too much!";
            this.rbStyleTooMuch.UseVisualStyleBackColor = true;
            this.rbStyleTooMuch.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // cbPrintOnlySelection
            // 
            this.cbPrintOnlySelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPrintOnlySelection.AutoSize = true;
            this.cbPrintOnlySelection.Location = new System.Drawing.Point(21, 62);
            this.cbPrintOnlySelection.Name = "cbPrintOnlySelection";
            this.cbPrintOnlySelection.Size = new System.Drawing.Size(146, 17);
            this.cbPrintOnlySelection.TabIndex = 6;
            this.cbPrintOnlySelection.Text = "Print Only Selected Rows";
            this.cbPrintOnlySelection.UseVisualStyleBackColor = true;
            this.cbPrintOnlySelection.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // rbStyleModern
            // 
            this.rbStyleModern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbStyleModern.Checked = true;
            this.rbStyleModern.Location = new System.Drawing.Point(133, 15);
            this.rbStyleModern.Name = "rbStyleModern";
            this.rbStyleModern.Size = new System.Drawing.Size(61, 24);
            this.rbStyleModern.TabIndex = 2;
            this.rbStyleModern.TabStop = true;
            this.rbStyleModern.Text = "Modern";
            this.rbStyleModern.UseVisualStyleBackColor = true;
            this.rbStyleModern.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // cbShrinkToFit
            // 
            this.cbShrinkToFit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShrinkToFit.AutoSize = true;
            this.cbShrinkToFit.Location = new System.Drawing.Point(200, 41);
            this.cbShrinkToFit.Name = "cbShrinkToFit";
            this.cbShrinkToFit.Size = new System.Drawing.Size(86, 17);
            this.cbShrinkToFit.TabIndex = 5;
            this.cbShrinkToFit.Text = "Shrink To Fit";
            this.cbShrinkToFit.UseVisualStyleBackColor = true;
            this.cbShrinkToFit.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // rbStyleMinimal
            // 
            this.rbStyleMinimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbStyleMinimal.Location = new System.Drawing.Point(66, 15);
            this.rbStyleMinimal.Name = "rbStyleMinimal";
            this.rbStyleMinimal.Size = new System.Drawing.Size(61, 24);
            this.rbStyleMinimal.TabIndex = 1;
            this.rbStyleMinimal.Text = "Minimal";
            this.rbStyleMinimal.UseVisualStyleBackColor = true;
            this.rbStyleMinimal.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // cbIncludeImages
            // 
            this.cbIncludeImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIncludeImages.AutoSize = true;
            this.cbIncludeImages.Checked = true;
            this.cbIncludeImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeImages.Location = new System.Drawing.Point(22, 41);
            this.cbIncludeImages.Name = "cbIncludeImages";
            this.cbIncludeImages.Size = new System.Drawing.Size(145, 17);
            this.cbIncludeImages.TabIndex = 4;
            this.cbIncludeImages.Text = "Include Images In Report";
            this.cbIncludeImages.UseVisualStyleBackColor = true;
            this.cbIncludeImages.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.tbFooter);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.tbHeader);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.tbWatermark);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.tbTitle);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Location = new System.Drawing.Point(451, 39);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(303, 109);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Print Settings";
            // 
            // tbFooter
            // 
            this.tbFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFooter.Location = new System.Drawing.Point(73, 61);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.Size = new System.Drawing.Size(224, 20);
            this.tbFooter.TabIndex = 5;
            this.tbFooter.Text = "{1:F}\\t\\tPage: {0}";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 19);
            this.label15.TabIndex = 4;
            this.label15.Text = "Footer:";
            // 
            // tbHeader
            // 
            this.tbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHeader.Location = new System.Drawing.Point(73, 39);
            this.tbHeader.Name = "tbHeader";
            this.tbHeader.Size = new System.Drawing.Size(224, 20);
            this.tbHeader.TabIndex = 3;
            this.tbHeader.Text = "Easy Printing ListView";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(7, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 19);
            this.label16.TabIndex = 2;
            this.label16.Text = "Header:";
            // 
            // tbWatermark
            // 
            this.tbWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWatermark.Location = new System.Drawing.Point(73, 83);
            this.tbWatermark.Name = "tbWatermark";
            this.tbWatermark.Size = new System.Drawing.Size(224, 20);
            this.tbWatermark.TabIndex = 7;
            this.tbWatermark.Text = "SLOTHFUL!";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(6, 86);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 19);
            this.label14.TabIndex = 6;
            this.label14.Text = "Watermark:";
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(73, 17);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(224, 20);
            this.tbTitle.TabIndex = 1;
            this.tbTitle.Text = "List View printer demo";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(7, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 19);
            this.label13.TabIndex = 0;
            this.label13.Text = "Job title:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbShowVirtual);
            this.groupBox2.Controls.Add(this.rbShowFileExplorer);
            this.groupBox2.Controls.Add(this.rbShowDataset);
            this.groupBox2.Controls.Add(this.rbShowComplex);
            this.groupBox2.Controls.Add(this.rbShowSimple);
            this.groupBox2.Location = new System.Drawing.Point(7, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 108);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List view to print";
            // 
            // rbShowVirtual
            // 
            this.rbShowVirtual.Location = new System.Drawing.Point(6, 67);
            this.rbShowVirtual.Name = "rbShowVirtual";
            this.rbShowVirtual.Size = new System.Drawing.Size(104, 23);
            this.rbShowVirtual.TabIndex = 3;
            this.rbShowVirtual.Text = "Virtual list";
            this.rbShowVirtual.UseVisualStyleBackColor = true;
            // 
            // rbShowFileExplorer
            // 
            this.rbShowFileExplorer.Location = new System.Drawing.Point(6, 86);
            this.rbShowFileExplorer.Name = "rbShowFileExplorer";
            this.rbShowFileExplorer.Size = new System.Drawing.Size(104, 23);
            this.rbShowFileExplorer.TabIndex = 4;
            this.rbShowFileExplorer.Text = "File explorer list";
            this.rbShowFileExplorer.UseVisualStyleBackColor = true;
            this.rbShowFileExplorer.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // rbShowDataset
            // 
            this.rbShowDataset.Location = new System.Drawing.Point(6, 48);
            this.rbShowDataset.Name = "rbShowDataset";
            this.rbShowDataset.Size = new System.Drawing.Size(104, 23);
            this.rbShowDataset.TabIndex = 2;
            this.rbShowDataset.Text = "Dataset list";
            this.rbShowDataset.UseVisualStyleBackColor = true;
            this.rbShowDataset.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // rbShowComplex
            // 
            this.rbShowComplex.Checked = true;
            this.rbShowComplex.Location = new System.Drawing.Point(6, 29);
            this.rbShowComplex.Name = "rbShowComplex";
            this.rbShowComplex.Size = new System.Drawing.Size(104, 23);
            this.rbShowComplex.TabIndex = 1;
            this.rbShowComplex.TabStop = true;
            this.rbShowComplex.Text = "Complex list";
            this.rbShowComplex.UseVisualStyleBackColor = true;
            this.rbShowComplex.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // rbShowSimple
            // 
            this.rbShowSimple.Location = new System.Drawing.Point(6, 10);
            this.rbShowSimple.Name = "rbShowSimple";
            this.rbShowSimple.Size = new System.Drawing.Size(104, 23);
            this.rbShowSimple.TabIndex = 0;
            this.rbShowSimple.Text = "Simple list";
            this.rbShowSimple.UseVisualStyleBackColor = true;
            this.rbShowSimple.CheckedChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(6, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(748, 30);
            this.label12.TabIndex = 12;
            this.label12.Text = resources.GetString("label12.Text");
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Location = new System.Drawing.Point(659, 224);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(93, 23);
            this.button12.TabIndex = 5;
            this.button12.Text = "Print...";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Location = new System.Drawing.Point(659, 195);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(93, 23);
            this.button11.TabIndex = 4;
            this.button11.Text = "Print Preview...";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Location = new System.Drawing.Point(659, 166);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(95, 23);
            this.button10.TabIndex = 3;
            this.button10.Text = "Page Setup...";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.printPreviewControl1.AutoZoom = false;
            this.printPreviewControl1.Columns = 2;
            this.printPreviewControl1.Document = this.listViewPrinter1;
            this.printPreviewControl1.Location = new System.Drawing.Point(7, 154);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(638, 328);
            this.printPreviewControl1.TabIndex = 6;
            this.printPreviewControl1.UseAntiAlias = true;
            this.printPreviewControl1.Zoom = 0.25834046193327631;
            // 
            // listViewPrinter1
            // 
            // 
            // 
            // 
            this.listViewPrinter1.CellFormat.CanWrap = true;
            this.listViewPrinter1.CellFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPrinter1.Footer = "This is the footers";
            // 
            // 
            // 
            this.listViewPrinter1.FooterFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Italic);
            // 
            // 
            // 
            this.listViewPrinter1.GroupHeaderFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.listViewPrinter1.Header = "This is the header\t\tRight";
            // 
            // 
            // 
            this.listViewPrinter1.HeaderFormat.Font = new System.Drawing.Font("Verdana", 24F);
            this.listViewPrinter1.IsListHeaderOnEachPage = false;
            // 
            // 
            // 
            this.listViewPrinter1.ListHeaderFormat.CanWrap = true;
            this.listViewPrinter1.ListHeaderFormat.Font = new System.Drawing.Font("Verdana", 12F);
            this.listViewPrinter1.ListView = this.listViewComplex;
            this.listViewPrinter1.Watermark = "TOP SECRET!";
            this.listViewPrinter1.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPrinter1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.listViewPrinter1_PrintPage);
            this.listViewPrinter1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.listViewPrinter1_EndPrint);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.button19);
            this.tabPage7.Controls.Add(this.button18);
            this.tabPage7.Controls.Add(this.label26);
            this.tabPage7.Controls.Add(this.comboBox9);
            this.tabPage7.Controls.Add(this.label24);
            this.tabPage7.Controls.Add(this.label25);
            this.tabPage7.Controls.Add(this.comboBox10);
            this.tabPage7.Controls.Add(this.checkBox13);
            this.tabPage7.Controls.Add(this.button15);
            this.tabPage7.Controls.Add(this.button14);
            this.tabPage7.Controls.Add(this.olvFastList);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(760, 503);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Fast List";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // button19
            // 
            this.button19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button19.Location = new System.Drawing.Point(400, 472);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(90, 23);
            this.button19.TabIndex = 17;
            this.button19.Text = "&Copy Checked";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button18
            // 
            this.button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button18.Location = new System.Drawing.Point(571, 472);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(101, 23);
            this.button18.TabIndex = 7;
            this.button18.Text = "Remove Selected";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.Button18Click);
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Location = new System.Drawing.Point(6, 5);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(748, 38);
            this.label26.TabIndex = 16;
            this.label26.Text = resources.GetString("label26.Text");
            // 
            // comboBox9
            // 
            this.comboBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBox9.Location = new System.Drawing.Point(189, 474);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(83, 21);
            this.comboBox9.TabIndex = 3;
            this.comboBox9.SelectedIndexChanged += new System.EventHandler(this.comboBox9_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(140, 478);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 2;
            this.label24.Text = "Editable:";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(277, 478);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(33, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "View:";
            // 
            // comboBox10
            // 
            this.comboBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox10.Location = new System.Drawing.Point(311, 474);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(83, 21);
            this.comboBox10.TabIndex = 5;
            this.comboBox10.SelectedIndexChanged += new System.EventHandler(this.comboBox10_SelectedIndexChanged);
            // 
            // checkBox13
            // 
            this.checkBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox13.Checked = true;
            this.checkBox13.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox13.Location = new System.Drawing.Point(6, 474);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(113, 21);
            this.checkBox13.TabIndex = 1;
            this.checkBox13.Text = "Owner &Draw";
            this.checkBox13.UseVisualStyleBackColor = true;
            this.checkBox13.CheckedChanged += new System.EventHandler(this.checkBox13_CheckedChanged);
            // 
            // button15
            // 
            this.button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button15.Location = new System.Drawing.Point(496, 472);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(69, 23);
            this.button15.TabIndex = 6;
            this.button15.Text = "&Clear List";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button14.Location = new System.Drawing.Point(678, 472);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(77, 23);
            this.button14.TabIndex = 8;
            this.button14.Text = "&Add 1000";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // olvFastList
            // 
            this.olvFastList.AllColumns.Add(this.olvColumn18);
            this.olvFastList.AllColumns.Add(this.olvColumn19);
            this.olvFastList.AllColumns.Add(this.olvColumn26);
            this.olvFastList.AllColumns.Add(this.olvColumn27);
            this.olvFastList.AllColumns.Add(this.olvColumn28);
            this.olvFastList.AllColumns.Add(this.olvColumn29);
            this.olvFastList.AllColumns.Add(this.olvColumn31);
            this.olvFastList.AllColumns.Add(this.olvColumn32);
            this.olvFastList.AllColumns.Add(this.olvColumn33);
            this.olvFastList.AllowColumnReorder = true;
            this.olvFastList.AllowDrop = true;
            this.olvFastList.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.olvFastList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.olvFastList.BackgroundImageTiled = true;
            this.olvFastList.CheckBoxes = true;
            this.olvFastList.CheckedAspectName = "";
            this.olvFastList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn18,
            this.olvColumn19,
            this.olvColumn26,
            this.olvColumn27,
            this.olvColumn28,
            this.olvColumn29,
            this.olvColumn31,
            this.olvColumn32,
            this.olvColumn33});
            this.olvFastList.EmptyListMsg = "This fast list is empty";
            this.olvFastList.FullRowSelect = true;
            this.olvFastList.GridLines = true;
            this.olvFastList.HideSelection = false;
            this.olvFastList.LargeImageList = this.imageList2;
            this.olvFastList.Location = new System.Drawing.Point(6, 47);
            this.olvFastList.Name = "olvFastList";
            this.olvFastList.OwnerDraw = true;
            this.olvFastList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.olvFastList.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.olvFastList.ShowGroups = false;
            this.olvFastList.ShowImagesOnSubItems = true;
            this.olvFastList.ShowItemToolTips = true;
            this.olvFastList.Size = new System.Drawing.Size(748, 419);
            this.olvFastList.SmallImageList = this.imageList1;
            this.olvFastList.TabIndex = 0;
            this.olvFastList.TintSortColumn = true;
            this.olvFastList.UseAlternatingBackColors = true;
            this.olvFastList.UseCompatibleStateImageBehavior = false;
            this.olvFastList.View = System.Windows.Forms.View.Details;
            this.olvFastList.VirtualMode = true;
            this.olvFastList.SelectionChanged += new System.EventHandler(this.olvFastList_SelectionChanged);
            this.olvFastList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.olvFastList_ItemChecked);
            this.olvFastList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.olvFastList_ItemCheck);
            // 
            // olvColumn18
            // 
            this.olvColumn18.AspectName = "Name";
            this.olvColumn18.Text = "Person";
            this.olvColumn18.UseInitialLetterForGroup = true;
            this.olvColumn18.Width = 114;
            // 
            // olvColumn19
            // 
            this.olvColumn19.AspectName = "Occupation";
            this.olvColumn19.IsTileViewColumn = true;
            this.olvColumn19.Text = "Occupation";
            this.olvColumn19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn19.Width = 92;
            // 
            // olvColumn26
            // 
            this.olvColumn26.AspectName = "CulinaryRating";
            this.olvColumn26.GroupWithItemCountFormat = "{0} ({1} candidates)";
            this.olvColumn26.GroupWithItemCountSingularFormat = "{0} (only {1} candidate)";
            this.olvColumn26.Text = "Cooking skill";
            this.olvColumn26.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn26.Width = 75;
            // 
            // olvColumn27
            // 
            this.olvColumn27.AspectName = "YearOfBirth";
            this.olvColumn27.Text = "Year Of Birth";
            this.olvColumn27.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn27.Width = 80;
            // 
            // olvColumn28
            // 
            this.olvColumn28.AspectName = "BirthDate";
            this.olvColumn28.AspectToStringFormat = "{0:D}";
            this.olvColumn28.FillsFreeSpace = true;
            this.olvColumn28.GroupWithItemCountFormat = "{0} has {1} birthdays";
            this.olvColumn28.GroupWithItemCountSingularFormat = "{0} has only {1} birthday";
            this.olvColumn28.IsTileViewColumn = true;
            this.olvColumn28.Text = "Birthday";
            this.olvColumn28.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn28.Width = 111;
            // 
            // olvColumn29
            // 
            this.olvColumn29.AspectName = "GetRate";
            this.olvColumn29.AspectToStringFormat = "{0:C}";
            this.olvColumn29.IsTileViewColumn = true;
            this.olvColumn29.Text = "Hourly Rate";
            this.olvColumn29.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn29.Width = 71;
            // 
            // olvColumn31
            // 
            this.olvColumn31.IsEditable = false;
            this.olvColumn31.Text = "Salary";
            this.olvColumn31.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn31.Width = 55;
            // 
            // olvColumn32
            // 
            this.olvColumn32.IsEditable = false;
            this.olvColumn32.Text = "Days Since Birth";
            this.olvColumn32.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn32.Width = 81;
            // 
            // olvColumn33
            // 
            this.olvColumn33.AspectName = "CanTellJokes";
            this.olvColumn33.CheckBoxes = true;
            this.olvColumn33.Text = "Tells Jokes?";
            this.olvColumn33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn33.Width = 74;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.button28);
            this.tabPage9.Controls.Add(this.button25);
            this.tabPage9.Controls.Add(this.button26);
            this.tabPage9.Controls.Add(this.button27);
            this.tabPage9.Controls.Add(this.label32);
            this.tabPage9.Controls.Add(this.treeListView);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(760, 503);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "TreeListView";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // button28
            // 
            this.button28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button28.Location = new System.Drawing.Point(6, 474);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(105, 23);
            this.button28.TabIndex = 14;
            this.button28.Text = "Refresh Selected";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click_1);
            // 
            // button25
            // 
            this.button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button25.Location = new System.Drawing.Point(457, 474);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(87, 23);
            this.button25.TabIndex = 10;
            this.button25.Text = "Save State";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button26
            // 
            this.button26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button26.Enabled = false;
            this.button26.Location = new System.Drawing.Point(550, 474);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(83, 23);
            this.button26.TabIndex = 11;
            this.button26.Text = "Restore State";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // button27
            // 
            this.button27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button27.Location = new System.Drawing.Point(639, 474);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(115, 23);
            this.button27.TabIndex = 12;
            this.button27.Text = "&Choose Columns...";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Location = new System.Drawing.Point(6, 6);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(748, 46);
            this.label32.TabIndex = 6;
            this.label32.Text = "This is like the File Explorer tab, except that it shows the directory structure," +
                " rooted on the available disks.";
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.treeColumnName);
            this.treeListView.AllColumns.Add(this.treeColumnCreated);
            this.treeListView.AllColumns.Add(this.treeColumnModified);
            this.treeListView.AllColumns.Add(this.treeColumnSize);
            this.treeListView.AllColumns.Add(this.treeColumnFileType);
            this.treeListView.AllColumns.Add(this.treeColumnAttributes);
            this.treeListView.AllColumns.Add(this.treeColumnFileExtension);
            this.treeListView.AllowColumnReorder = true;
            this.treeListView.AllowDrop = true;
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView.CheckBoxes = true;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.treeColumnName,
            this.treeColumnCreated,
            this.treeColumnModified,
            this.treeColumnSize,
            this.treeColumnFileType,
            this.treeColumnAttributes});
            this.treeListView.EmptyListMsg = "This folder is completely empty!";
            this.treeListView.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.HideSelection = false;
            this.treeListView.Location = new System.Drawing.Point(6, 55);
            this.treeListView.Name = "treeListView";
            this.treeListView.OverlayImage.Image = global::ObjectListViewDemo.Resource1.limeleaf;
            this.treeListView.OwnerDraw = true;
            this.treeListView.ShowGroups = false;
            this.treeListView.ShowImagesOnSubItems = true;
            this.treeListView.Size = new System.Drawing.Size(748, 413);
            this.treeListView.SmallImageList = this.imageList1;
            this.treeListView.TabIndex = 13;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.UseHotItem = true;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            this.treeListView.ItemActivate += new System.EventHandler(this.treeListView_ItemActivate);
            this.treeListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.treeListView_ItemChecked);
            this.treeListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.treeListView_ItemCheck);
            // 
            // treeColumnName
            // 
            this.treeColumnName.AspectName = "Name";
            this.treeColumnName.IsTileViewColumn = true;
            this.treeColumnName.Text = "Name";
            this.treeColumnName.UseInitialLetterForGroup = true;
            this.treeColumnName.Width = 180;
            // 
            // treeColumnCreated
            // 
            this.treeColumnCreated.AspectName = "CreationTime";
            this.treeColumnCreated.DisplayIndex = 4;
            this.treeColumnCreated.Text = "Created";
            this.treeColumnCreated.Width = 131;
            // 
            // treeColumnModified
            // 
            this.treeColumnModified.AspectName = "LastWriteTime";
            this.treeColumnModified.DisplayIndex = 1;
            this.treeColumnModified.IsTileViewColumn = true;
            this.treeColumnModified.Text = "Modified";
            this.treeColumnModified.Width = 145;
            // 
            // treeColumnSize
            // 
            this.treeColumnSize.AspectName = "Extension";
            this.treeColumnSize.DisplayIndex = 2;
            this.treeColumnSize.Text = "Size";
            this.treeColumnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.treeColumnSize.Width = 80;
            // 
            // treeColumnFileType
            // 
            this.treeColumnFileType.DisplayIndex = 3;
            this.treeColumnFileType.IsTileViewColumn = true;
            this.treeColumnFileType.Text = "File Type";
            this.treeColumnFileType.Width = 148;
            // 
            // treeColumnAttributes
            // 
            this.treeColumnAttributes.FillsFreeSpace = true;
            this.treeColumnAttributes.IsEditable = false;
            this.treeColumnAttributes.MinimumWidth = 20;
            this.treeColumnAttributes.Text = "Attributes";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.checkBox22);
            this.tabPage8.Controls.Add(this.checkBox21);
            this.tabPage8.Controls.Add(this.tableLayoutPanel1);
            this.tabPage8.Controls.Add(this.label33);
            this.tabPage8.Controls.Add(this.comboBox13);
            this.tabPage8.Controls.Add(this.label31);
            this.tabPage8.Controls.Add(this.comboBox12);
            this.tabPage8.Controls.Add(this.label30);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(760, 503);
            this.tabPage8.TabIndex = 9;
            this.tabPage8.Text = "Drag and drop";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // checkBox22
            // 
            this.checkBox22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox22.Location = new System.Drawing.Point(531, 474);
            this.checkBox22.Name = "checkBox22";
            this.checkBox22.Size = new System.Drawing.Size(86, 21);
            this.checkBox22.TabIndex = 21;
            this.checkBox22.Text = "Owner &Draw";
            this.checkBox22.UseVisualStyleBackColor = true;
            this.checkBox22.CheckedChanged += new System.EventHandler(this.checkBox22_CheckedChanged);
            // 
            // checkBox21
            // 
            this.checkBox21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox21.Location = new System.Drawing.Point(6, 474);
            this.checkBox21.Name = "checkBox21";
            this.checkBox21.Size = new System.Drawing.Size(86, 21);
            this.checkBox21.TabIndex = 20;
            this.checkBox21.Text = "Owner &Draw";
            this.checkBox21.UseVisualStyleBackColor = true;
            this.checkBox21.CheckedChanged += new System.EventHandler(this.checkBox21_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label34, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label35, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.olvGeeks, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.olvFroods, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 58);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 410);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label34.Location = new System.Drawing.Point(3, 3);
            this.label34.Margin = new System.Windows.Forms.Padding(3);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(119, 14);
            this.label34.TabIndex = 17;
            this.label34.Text = "Geeks and Tweebs";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label35.Location = new System.Drawing.Point(377, 3);
            this.label35.Margin = new System.Windows.Forms.Padding(3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(78, 14);
            this.label35.TabIndex = 18;
            this.label35.Text = "Cool froods";
            // 
            // olvGeeks
            // 
            this.olvGeeks.AllColumns.Add(this.olvColumn43);
            this.olvGeeks.AllColumns.Add(this.olvColumn44);
            this.olvGeeks.AllColumns.Add(this.olvColumn45);
            this.olvGeeks.AllColumns.Add(this.yearOfBirthColumn);
            this.olvGeeks.AllColumns.Add(this.olvColumn46);
            this.olvGeeks.AllColumns.Add(this.olvColumn47);
            this.olvGeeks.AllColumns.Add(this.olvColumn48);
            this.olvGeeks.AllColumns.Add(this.olvColumn49);
            this.olvGeeks.AllColumns.Add(this.olvColumn50);
            this.olvGeeks.AllColumns.Add(this.olvColumn51);
            this.olvGeeks.AllowColumnReorder = true;
            this.olvGeeks.AllowDrop = true;
            this.olvGeeks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.olvGeeks.CheckedAspectName = "";
            this.olvGeeks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn43,
            this.olvColumn44,
            this.olvColumn45,
            this.olvColumn46,
            this.olvColumn47,
            this.olvColumn48,
            this.olvColumn49,
            this.olvColumn50,
            this.olvColumn51});
            this.olvGeeks.EmptyListMsg = "Drag some cool froods here";
            this.olvGeeks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvGeeks.FullRowSelect = true;
            this.olvGeeks.GroupWithItemCountFormat = "{0} ({1} people)";
            this.olvGeeks.GroupWithItemCountSingularFormat = "{0} ({1} person)";
            this.olvGeeks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvGeeks.HideSelection = false;
            this.olvGeeks.LargeImageList = this.imageList2;
            this.olvGeeks.Location = new System.Drawing.Point(3, 23);
            this.olvGeeks.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.olvGeeks.Name = "olvGeeks";
            this.olvGeeks.OverlayImage.Image = global::ObjectListViewDemo.Resource1.redbull;
            this.olvGeeks.OverlayText.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.olvGeeks.OverlayText.Text = "";
            this.olvGeeks.ShowGroups = false;
            this.olvGeeks.ShowImagesOnSubItems = true;
            this.olvGeeks.ShowItemToolTips = true;
            this.olvGeeks.Size = new System.Drawing.Size(365, 384);
            this.olvGeeks.SmallImageList = this.imageList1;
            this.olvGeeks.TabIndex = 8;
            this.olvGeeks.UseAlternatingBackColors = true;
            this.olvGeeks.UseCompatibleStateImageBehavior = false;
            this.olvGeeks.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn43
            // 
            this.olvColumn43.AspectName = "Name";
            this.olvColumn43.Text = "Person";
            this.olvColumn43.UseInitialLetterForGroup = true;
            this.olvColumn43.Width = 114;
            // 
            // olvColumn44
            // 
            this.olvColumn44.AspectName = "Occupation";
            this.olvColumn44.IsTileViewColumn = true;
            this.olvColumn44.Text = "Occupation";
            this.olvColumn44.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn44.Width = 92;
            // 
            // olvColumn45
            // 
            this.olvColumn45.AspectName = "CulinaryRating";
            this.olvColumn45.GroupWithItemCountFormat = "{0} ({1} candidates)";
            this.olvColumn45.GroupWithItemCountSingularFormat = "{0} (only {1} candidate)";
            this.olvColumn45.Renderer = this.cookingSkillRenderer;
            this.olvColumn45.Text = "Cooking skill";
            this.olvColumn45.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn45.Width = 75;
            // 
            // olvColumn46
            // 
            this.olvColumn46.AspectName = "BirthDate";
            this.olvColumn46.AspectToStringFormat = "{0:D}";
            this.olvColumn46.GroupWithItemCountFormat = "{0} has {1} birthdays";
            this.olvColumn46.GroupWithItemCountSingularFormat = "{0} has only {1} birthday";
            this.olvColumn46.IsTileViewColumn = true;
            this.olvColumn46.Text = "Birthday";
            this.olvColumn46.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn46.Width = 111;
            // 
            // olvColumn47
            // 
            this.olvColumn47.AspectName = "GetRate";
            this.olvColumn47.AspectToStringFormat = "{0:C}";
            this.olvColumn47.IsTileViewColumn = true;
            this.olvColumn47.Text = "Hourly Rate";
            this.olvColumn47.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn47.Width = 71;
            // 
            // olvColumn48
            // 
            this.olvColumn48.IsEditable = false;
            this.olvColumn48.Text = "Salary";
            this.olvColumn48.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn48.Width = 55;
            // 
            // olvColumn49
            // 
            this.olvColumn49.IsEditable = false;
            this.olvColumn49.Text = "Days Since Birth";
            this.olvColumn49.Width = 81;
            // 
            // olvColumn50
            // 
            this.olvColumn50.AspectName = "CanTellJokes";
            this.olvColumn50.CheckBoxes = true;
            this.olvColumn50.Text = "Tells Jokes?";
            this.olvColumn50.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn50.Width = 74;
            // 
            // olvColumn51
            // 
            this.olvColumn51.AspectName = "MaritalStatus";
            this.olvColumn51.Text = "Married?";
            this.olvColumn51.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvFroods
            // 
            this.olvFroods.AllColumns.Add(this.olvColumn52);
            this.olvFroods.AllColumns.Add(this.olvColumn53);
            this.olvFroods.AllColumns.Add(this.olvColumn54);
            this.olvFroods.AllColumns.Add(this.yearOfBirthColumn);
            this.olvFroods.AllColumns.Add(this.olvColumn55);
            this.olvFroods.AllColumns.Add(this.olvColumn56);
            this.olvFroods.AllColumns.Add(this.olvColumn57);
            this.olvFroods.AllColumns.Add(this.olvColumn58);
            this.olvFroods.AllColumns.Add(this.olvColumn59);
            this.olvFroods.AllColumns.Add(this.olvColumn60);
            this.olvFroods.AllowColumnReorder = true;
            this.olvFroods.AllowDrop = true;
            this.olvFroods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.olvFroods.CheckedAspectName = "";
            this.olvFroods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn52,
            this.olvColumn53,
            this.olvColumn54,
            this.olvColumn55,
            this.olvColumn56,
            this.olvColumn57,
            this.olvColumn58,
            this.olvColumn59,
            this.olvColumn60});
            this.olvFroods.EmptyListMsg = "Drag some geeks here";
            this.olvFroods.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvFroods.FullRowSelect = true;
            this.olvFroods.GroupWithItemCountFormat = "{0} ({1} people)";
            this.olvFroods.GroupWithItemCountSingularFormat = "{0} ({1} person)";
            this.olvFroods.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvFroods.HideSelection = false;
            this.olvFroods.LargeImageList = this.imageList2;
            this.olvFroods.Location = new System.Drawing.Point(380, 23);
            this.olvFroods.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.olvFroods.Name = "olvFroods";
            this.olvFroods.OverlayImage.Image = global::ObjectListViewDemo.Resource1.redback1;
            this.olvFroods.OverlayText.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.olvFroods.OverlayText.Text = "";
            this.olvFroods.ShowGroups = false;
            this.olvFroods.ShowImagesOnSubItems = true;
            this.olvFroods.ShowItemToolTips = true;
            this.olvFroods.Size = new System.Drawing.Size(365, 384);
            this.olvFroods.SmallImageList = this.imageList1;
            this.olvFroods.TabIndex = 13;
            this.olvFroods.UseAlternatingBackColors = true;
            this.olvFroods.UseCompatibleStateImageBehavior = false;
            this.olvFroods.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn52
            // 
            this.olvColumn52.AspectName = "Name";
            this.olvColumn52.Text = "Person";
            this.olvColumn52.UseInitialLetterForGroup = true;
            this.olvColumn52.Width = 114;
            // 
            // olvColumn53
            // 
            this.olvColumn53.AspectName = "Occupation";
            this.olvColumn53.IsTileViewColumn = true;
            this.olvColumn53.Text = "Occupation";
            this.olvColumn53.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn53.Width = 92;
            // 
            // olvColumn54
            // 
            this.olvColumn54.AspectName = "CulinaryRating";
            this.olvColumn54.GroupWithItemCountFormat = "{0} ({1} candidates)";
            this.olvColumn54.GroupWithItemCountSingularFormat = "{0} (only {1} candidate)";
            this.olvColumn54.Renderer = this.cookingSkillRenderer;
            this.olvColumn54.Text = "Cooking skill";
            this.olvColumn54.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn54.Width = 75;
            // 
            // olvColumn55
            // 
            this.olvColumn55.AspectName = "BirthDate";
            this.olvColumn55.AspectToStringFormat = "{0:D}";
            this.olvColumn55.GroupWithItemCountFormat = "{0} has {1} birthdays";
            this.olvColumn55.GroupWithItemCountSingularFormat = "{0} has only {1} birthday";
            this.olvColumn55.IsTileViewColumn = true;
            this.olvColumn55.Text = "Birthday";
            this.olvColumn55.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn55.Width = 111;
            // 
            // olvColumn56
            // 
            this.olvColumn56.AspectName = "GetRate";
            this.olvColumn56.AspectToStringFormat = "{0:C}";
            this.olvColumn56.IsTileViewColumn = true;
            this.olvColumn56.Text = "Hourly Rate";
            this.olvColumn56.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn56.Width = 71;
            // 
            // olvColumn57
            // 
            this.olvColumn57.IsEditable = false;
            this.olvColumn57.Text = "Salary";
            this.olvColumn57.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn57.Width = 55;
            // 
            // olvColumn58
            // 
            this.olvColumn58.IsEditable = false;
            this.olvColumn58.Text = "Days Since Birth";
            this.olvColumn58.Width = 81;
            // 
            // olvColumn59
            // 
            this.olvColumn59.AspectName = "CanTellJokes";
            this.olvColumn59.CheckBoxes = true;
            this.olvColumn59.Text = "Tells Jokes?";
            this.olvColumn59.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn59.Width = 74;
            // 
            // olvColumn60
            // 
            this.olvColumn60.AspectName = "MaritalStatus";
            this.olvColumn60.Text = "Married?";
            this.olvColumn60.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(623, 477);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(33, 13);
            this.label33.TabIndex = 16;
            this.label33.Text = "View:";
            // 
            // comboBox13
            // 
            this.comboBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox13.Location = new System.Drawing.Point(662, 474);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.Size = new System.Drawing.Size(89, 21);
            this.comboBox13.TabIndex = 15;
            this.comboBox13.SelectedIndexChanged += new System.EventHandler(this.comboBox13_SelectedIndexChanged);
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(95, 477);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(33, 13);
            this.label31.TabIndex = 12;
            this.label31.Text = "View:";
            // 
            // comboBox12
            // 
            this.comboBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox12.Location = new System.Drawing.Point(134, 474);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(97, 21);
            this.comboBox12.TabIndex = 11;
            this.comboBox12.SelectedIndexChanged += new System.EventHandler(this.comboBox12_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label30.Location = new System.Drawing.Point(6, 7);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(748, 48);
            this.label30.TabIndex = 10;
            this.label30.Text = resources.GetString("label30.Text");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(793, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // button20
            // 
            this.button20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button20.Location = new System.Drawing.Point(483, 456);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(87, 23);
            this.button20.TabIndex = 10;
            this.button20.Text = "Save State";
            this.button20.UseVisualStyleBackColor = true;
            // 
            // button21
            // 
            this.button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button21.Enabled = false;
            this.button21.Location = new System.Drawing.Point(576, 456);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(83, 23);
            this.button21.TabIndex = 11;
            this.button21.Text = "Restore State";
            this.button21.UseVisualStyleBackColor = true;
            // 
            // button22
            // 
            this.button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button22.Location = new System.Drawing.Point(665, 456);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(115, 23);
            this.button22.TabIndex = 12;
            this.button22.Text = "&Choose Columns...";
            this.button22.UseVisualStyleBackColor = true;
            // 
            // button23
            // 
            this.button23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button23.Location = new System.Drawing.Point(705, 55);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(75, 23);
            this.button23.TabIndex = 3;
            this.button23.Text = "&Up";
            this.button23.UseVisualStyleBackColor = true;
            // 
            // button24
            // 
            this.button24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button24.Location = new System.Drawing.Point(624, 55);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(75, 23);
            this.button24.TabIndex = 2;
            this.button24.Text = "&Go";
            this.button24.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(56, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(562, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 60);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(39, 13);
            this.label27.TabIndex = 0;
            this.label27.Text = "&Folder:";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(303, 461);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(33, 13);
            this.label28.TabIndex = 8;
            this.label28.Text = "View:";
            // 
            // comboBox11
            // 
            this.comboBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBox11.Location = new System.Drawing.Point(337, 456);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(121, 21);
            this.comboBox11.TabIndex = 9;
            // 
            // checkBox14
            // 
            this.checkBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox14.Location = new System.Drawing.Point(218, 459);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(90, 19);
            this.checkBox14.TabIndex = 7;
            this.checkBox14.Text = "Owner &Draw";
            this.checkBox14.UseVisualStyleBackColor = true;
            // 
            // checkBox16
            // 
            this.checkBox16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox16.Location = new System.Drawing.Point(101, 456);
            this.checkBox16.Name = "checkBox16";
            this.checkBox16.Size = new System.Drawing.Size(111, 24);
            this.checkBox16.TabIndex = 6;
            this.checkBox16.Text = "Show Item &Count";
            this.checkBox16.UseVisualStyleBackColor = true;
            // 
            // checkBox17
            // 
            this.checkBox17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox17.Location = new System.Drawing.Point(6, 456);
            this.checkBox17.Name = "checkBox17";
            this.checkBox17.Size = new System.Drawing.Size(104, 24);
            this.checkBox17.TabIndex = 5;
            this.checkBox17.Text = "Show &Groups";
            this.checkBox17.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label29.Location = new System.Drawing.Point(6, 6);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(774, 46);
            this.label29.TabIndex = 6;
            this.label29.Text = resources.GetString("label29.Text");
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvColumn35);
            this.objectListView1.AllColumns.Add(this.olvColumn36);
            this.objectListView1.AllColumns.Add(this.olvColumn37);
            this.objectListView1.AllColumns.Add(this.olvColumn38);
            this.objectListView1.AllColumns.Add(this.olvColumn39);
            this.objectListView1.AllColumns.Add(this.olvColumn40);
            this.objectListView1.AllColumns.Add(this.treeColumnFileExtension);
            this.objectListView1.AllowColumnReorder = true;
            this.objectListView1.AllowDrop = true;
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn35,
            this.olvColumn36,
            this.olvColumn37,
            this.olvColumn38,
            this.olvColumn39,
            this.olvColumn40});
            this.objectListView1.EmptyListMsg = "This folder is completely empty!";
            this.objectListView1.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectListView1.HideSelection = false;
            this.objectListView1.LargeImageList = this.imageList2;
            this.objectListView1.Location = new System.Drawing.Point(6, 83);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.OwnerDraw = true;
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(774, 367);
            this.objectListView1.SmallImageList = this.imageList1;
            this.objectListView1.TabIndex = 13;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.UseHotItem = true;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn35
            // 
            this.olvColumn35.AspectName = "Name";
            this.olvColumn35.IsTileViewColumn = true;
            this.olvColumn35.Text = "Name";
            this.olvColumn35.UseInitialLetterForGroup = true;
            this.olvColumn35.Width = 180;
            // 
            // olvColumn36
            // 
            this.olvColumn36.AspectName = "CreationTime";
            this.olvColumn36.DisplayIndex = 4;
            this.olvColumn36.Text = "Created";
            this.olvColumn36.Width = 131;
            // 
            // olvColumn37
            // 
            this.olvColumn37.AspectName = "LastWriteTime";
            this.olvColumn37.DisplayIndex = 1;
            this.olvColumn37.IsTileViewColumn = true;
            this.olvColumn37.Text = "Modified";
            this.olvColumn37.Width = 145;
            // 
            // olvColumn38
            // 
            this.olvColumn38.AspectName = "Extension";
            this.olvColumn38.DisplayIndex = 2;
            this.olvColumn38.Text = "Size";
            this.olvColumn38.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn38.Width = 80;
            // 
            // olvColumn39
            // 
            this.olvColumn39.DisplayIndex = 3;
            this.olvColumn39.IsTileViewColumn = true;
            this.olvColumn39.Text = "File Type";
            this.olvColumn39.Width = 148;
            // 
            // olvColumn40
            // 
            this.olvColumn40.FillsFreeSpace = true;
            this.olvColumn40.IsEditable = false;
            this.olvColumn40.MinimumWidth = 20;
            this.olvColumn40.Text = "Attributes";
            // 
            // olvColumn21
            // 
            this.olvColumn21.AspectName = "StartTime";
            this.olvColumn21.DisplayIndex = 2;
            this.olvColumn21.IsVisible = false;
            this.olvColumn21.Text = "Start Time";
            // 
            // olvColumn22
            // 
            this.olvColumn22.AspectName = "Threads.Count";
            this.olvColumn22.DisplayIndex = 3;
            this.olvColumn22.IsVisible = false;
            this.olvColumn22.Text = "Thread Count";
            // 
            // olvColumn23
            // 
            this.olvColumn23.AspectName = "TotalProcessorTime";
            this.olvColumn23.DisplayIndex = 4;
            this.olvColumn23.IsVisible = false;
            this.olvColumn23.Text = "Processor Time";
            // 
            // olvColumn30
            // 
            this.olvColumn30.AspectName = "PriorityClass";
            this.olvColumn30.DisplayIndex = 9;
            this.olvColumn30.IsVisible = false;
            this.olvColumn30.Text = "Priority Class";
            // 
            // olvColumn24
            // 
            this.olvColumn24.DisplayIndex = 5;
            this.olvColumn24.IsVisible = false;
            // 
            // olvColumn25
            // 
            this.olvColumn25.DisplayIndex = 6;
            this.olvColumn25.IsVisible = false;
            // 
            // olvColumn20
            // 
            this.olvColumn20.DisplayIndex = 2;
            this.olvColumn20.IsVisible = false;
            // 
            // olvColumn17
            // 
            this.olvColumn17.DisplayIndex = 0;
            this.olvColumn17.Text = "Zero";
            // 
            // olvColumn13
            // 
            this.olvColumn13.DisplayIndex = 1;
            this.olvColumn13.Text = "Two";
            // 
            // olvColumn14
            // 
            this.olvColumn14.DisplayIndex = 2;
            this.olvColumn14.Text = "Three";
            // 
            // olvColumn15
            // 
            this.olvColumn15.DisplayIndex = 3;
            this.olvColumn15.Text = "Four";
            // 
            // olvColumn6
            // 
            this.olvColumn6.DisplayIndex = 0;
            // 
            // olvColumn11
            // 
            this.olvColumn11.DisplayIndex = 1;
            this.olvColumn11.IsVisible = false;
            this.olvColumn11.Text = "One";
            // 
            // olvColumn16
            // 
            this.olvColumn16.DisplayIndex = 4;
            this.olvColumn16.IsVisible = false;
            this.olvColumn16.Text = "Five";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 566);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "ObjectListView Demo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewSimple)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewComplex)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowHeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listViewDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewVirtual)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewFiles)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvFastList)).EndInit();
            this.tabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvGeeks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.olvFroods)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		private System.Windows.Forms.Button button18;
		private System.Windows.Forms.RadioButton rbShowSimple;
		private System.Windows.Forms.RadioButton rbShowComplex;
		private System.Windows.Forms.RadioButton rbShowDataset;
		private System.Windows.Forms.RadioButton rbShowFileExplorer;
		private System.Windows.Forms.TextBox tbTitle;
		private System.Windows.Forms.TextBox tbWatermark;
		private System.Windows.Forms.TextBox tbHeader;
		private System.Windows.Forms.TextBox tbFooter;
		private System.Windows.Forms.CheckBox cbIncludeImages;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.RadioButton rbStyleMinimal;
		private System.Windows.Forms.RadioButton rbStyleModern;
        private System.Windows.Forms.RadioButton rbStyleTooMuch;
		private System.Windows.Forms.CheckBox cbPrintOnlySelection;
		private System.Windows.Forms.CheckBox cbShrinkToFit;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
        private BrightIdeasSoftware.ListViewPrinter listViewPrinter1;
		private BrightIdeasSoftware.OLVColumn olvColumnFileName;
		private BrightIdeasSoftware.OLVColumn olvColumnFileModified;
		private BrightIdeasSoftware.OLVColumn olvColumnFileCreated;
		private BrightIdeasSoftware.OLVColumn olvColumnSize;
		private BrightIdeasSoftware.ObjectListView listViewFiles;
		private System.Windows.Forms.Button buttonGo;
		private System.Windows.Forms.TextBox textBoxFolderPath;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox12;
		private System.Windows.Forms.CheckBox checkBox11;
		private System.Windows.Forms.CheckBox checkBox10;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.CheckBox checkBox9;
		private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dataGridView1;
		private BrightIdeasSoftware.OLVColumn olvColumn12;
		private BrightIdeasSoftware.ObjectListView listViewSimple;
		private BrightIdeasSoftware.OLVColumn olvColumn10;
		private BrightIdeasSoftware.OLVColumn olvColumn9;
		private BrightIdeasSoftware.OLVColumn olvColumn8;
		private BrightIdeasSoftware.OLVColumn olvColumn7;
		private BrightIdeasSoftware.OLVColumn olvColumn5;
		private BrightIdeasSoftware.OLVColumn olvColumn4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabPage tabPage4;
		private BrightIdeasSoftware.VirtualObjectListView listViewVirtual;
		private BrightIdeasSoftware.OLVColumn yearOfBirthColumn;
		private BrightIdeasSoftware.OLVColumn occupationColumn;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private BrightIdeasSoftware.OLVColumn daysSinceBirthColumn;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.CheckBox checkBox8;
		private GroupBox groupBox1;
		private BrightIdeasSoftware.DataListView listViewDataSet;
		private BrightIdeasSoftware.OLVColumn salaryColumn;
		private BrightIdeasSoftware.OLVColumn olvColumn2;
		private BrightIdeasSoftware.OLVColumn olvColumn3;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ImageList imageList1;
		private BrightIdeasSoftware.OLVColumn personColumn;
		private BrightIdeasSoftware.OLVColumn columnCookingSkill;
		private BrightIdeasSoftware.OLVColumn birthdayColumn;
		private BrightIdeasSoftware.OLVColumn hourlyRateColumn;
		private BrightIdeasSoftware.OLVColumn columnHeader12;
		private BrightIdeasSoftware.OLVColumn columnHeader11;
		private BrightIdeasSoftware.OLVColumn columnHeader13;
		private BrightIdeasSoftware.OLVColumn columnHeader14;
		private BrightIdeasSoftware.OLVColumn columnHeader15;
		private BrightIdeasSoftware.OLVColumn columnHeader16;
        private ObjectListView listViewComplex;
		private BrightIdeasSoftware.OLVColumn moneyImageColumn;
        private ImageList imageList2;
		private BrightIdeasSoftware.OLVColumn heightColumn;
        private Label label6;
        private ComboBox comboBox1;
        private Label label7;
        private ComboBox comboBox2;
        private Label label8;
        private ComboBox comboBox3;
        private OLVColumn olvColumnFileType;
        private Button buttonUp;
        private OLVColumn olvColumnGif;
        private NumericUpDown rowHeightUpDown;
        private Label label11;
        private CheckBox checkBoxPause;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private TabPage tabPage6;
        private Button button12;
        private Button button11;
        private Button button10;
        private CheckBox cbCellGridLines;
        private GroupBox groupBox5;
        private NumericUpDown numericUpDown2;
        private Label label19;
        private NumericUpDown numericUpDown1;
        private Label label18;
        private RadioButton rbShowVirtual;
        private OLVColumn olvJokeColumn;
        private ComboBox comboBox5;
        private Label label20;
        private ComboBox comboBox6;
        private Label label21;
        private Label label22;
        private ComboBox comboBox7;
        private ComboBox comboBox8;
        private Label label23;
        private OLVColumn olvColumn6;
        private OLVColumn olvColumn17;
        private OLVColumn olvColumn13;
        private OLVColumn olvColumn14;
        private OLVColumn olvColumn15;
        private OLVColumn olvColumn11;
        private OLVColumn olvColumn16;
        private OLVColumn olvColumn20;
        private OLVColumn olvColumn24;
        private OLVColumn olvColumn25;
        private Button button13;
        private OLVColumn treeColumnFileExtension;
        private OLVColumn olvColumnAttributes;
        private OLVColumn olvColumn21;
        private OLVColumn olvColumn22;
        private OLVColumn olvColumn23;
        private OLVColumn olvColumn30;
        private TabPage tabPage7;
        private FastObjectListView olvFastList;
        private OLVColumn olvColumn18;
        private OLVColumn olvColumn19;
        private OLVColumn olvColumn26;
        private OLVColumn olvColumn27;
        private OLVColumn olvColumn28;
        private OLVColumn olvColumn29;
        private OLVColumn olvColumn31;
        private OLVColumn olvColumn32;
        private OLVColumn olvColumn33;
        private Button button14;
        private ComboBox comboBox9;
        private Label label24;
        private Label label25;
        private ComboBox comboBox10;
        private CheckBox checkBox13;
        private Button button15;
        private Label label26;
        private OLVColumn olvColumn34;
        private Button buttonSaveState;
        private Button buttonRestoreState;
        private Button button16;
        private Button button17;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem command1ToolStripMenuItem;
        private ToolStripMenuItem command2ToolStripMenuItem;
        private ToolStripMenuItem command3ToolStripMenuItem;
        private ToolStripMenuItem appearOnTheColumnHeadersToolStripMenuItem;
        private CheckBox checkBox15;
        private Button button19;
        private TabPage tabPage9;
        private Button button25;
        private Button button26;
        private Button button27;
        private Label label32;
        private TreeListView treeListView;
        private OLVColumn treeColumnName;
        private OLVColumn treeColumnCreated;
        private OLVColumn treeColumnModified;
        private OLVColumn treeColumnSize;
        private OLVColumn treeColumnFileType;
        private OLVColumn treeColumnAttributes;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private Button button24;
        private TextBox textBox1;
        private Label label27;
        private Label label28;
        private ComboBox comboBox11;
        private CheckBox checkBox14;
        private CheckBox checkBox16;
        private CheckBox checkBox17;
        private Label label29;
        private ObjectListView objectListView1;
        private OLVColumn olvColumn35;
        private OLVColumn olvColumn36;
        private OLVColumn olvColumn37;
        private OLVColumn olvColumn38;
        private OLVColumn olvColumn39;
        private OLVColumn olvColumn40;
        private OLVColumn olvColumn41;
        private CheckBox checkBox18;
        private HotItemStyle hotItemStyle1;
        private BarRenderer heightRenderer;
        private ImageRenderer imageRenderer1;
        private MultiImageRenderer salaryRenderer;
        private OLVColumn olvColumn42;
        private MultiImageRenderer cookingSkillRenderer;
        private TabPage tabPage8;
        private Label label34;
        private Label label33;
        private ComboBox comboBox13;
        private ObjectListView olvFroods;
        private OLVColumn olvColumn52;
        private OLVColumn olvColumn53;
        private OLVColumn olvColumn54;
        private OLVColumn olvColumn55;
        private OLVColumn olvColumn56;
        private OLVColumn olvColumn57;
        private OLVColumn olvColumn58;
        private OLVColumn olvColumn59;
        private OLVColumn olvColumn60;
        private Label label31;
        private ComboBox comboBox12;
        private Label label30;
        private ObjectListView olvGeeks;
        private OLVColumn olvColumn43;
        private OLVColumn olvColumn44;
        private OLVColumn olvColumn45;
        private OLVColumn olvColumn46;
        private OLVColumn olvColumn47;
        private OLVColumn olvColumn48;
        private OLVColumn olvColumn49;
        private OLVColumn olvColumn50;
        private OLVColumn olvColumn51;
        private Label label35;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox checkBox22;
        private CheckBox checkBox21;
        private ComboBox comboBoxNagLevel;
        private Label label36;
        private Button button28;

	}
}
