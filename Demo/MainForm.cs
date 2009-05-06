/*
 * ObjectListViewDemo - A simple demo to show the ObjectListView control
 *
 * User: Phillip Piper
 * Date: 15/10/2006 11:15 AM
 *
 * Change log:
 * 2006-10-20  JPP  Added DataSet tab page
 * 2006-10-15  JPP  Initial version
 */

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing.Imaging;

using BrightIdeasSoftware;

namespace ObjectListViewDemo
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
	{
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
		[STAThread]
        public static void Main(string[] args)
		{
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

        /// <summary>
        ///
        /// </summary>
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			InitializeExamples();
		}

		List<Person> masterList;
		void InitializeExamples()
		{
			masterList = new List<Person>();
            masterList.Add(new Person("Wilhelm Frat", "Gymnast", 19, new DateTime(1984, 9, 23), 45.67, false, "ak", "Aggressive, belligerent "));
            masterList.Add(new Person("Alana Roderick", "Gymnast", 21, new DateTime(1974, 9, 23), 245.67, false, "gp", "Beautiful, exquisite"));
            masterList.Add(new Person("Frank Price", "Dancer", 30, new DateTime(1965, 11, 1), 75.5, false, "ns", "Competitive, spirited"));
            masterList.Add(new Person("Eric", "Half-a-bee", 1, new DateTime(1966, 10, 12), 12.25, true, "cp", "Diminutive, vertically challenged"));
            masterList.Add(new Person("Madalene Alright", "School Teacher", 21, new DateTime(1964, 9, 23), 145.67, false, "jr", "Extensive, dimensionally challenged"));
            masterList.Add(new Person("Ned Peirce", "School Teacher", 21, new DateTime(1960, 1, 23), 145.67, false, "gab", "Fulsome, effusive"));
            masterList.Add(new Person("Felicity Brown", "Economist", 30, new DateTime(1975, 1, 12), 175.5, false, "sp", "Gifted, exceptional"));
            masterList.Add(new Person("Urny Unmin", "Economist", 41, new DateTime(1956, 9, 24), 212.25, true, "cr", "Heinous, aesthetically challenged"));
            masterList.Add(new Person("Terrance Darby", "Singer", 35, new DateTime(1970, 9, 29), 1145, false, "mb", "Introverted, relationally challenged"));
            masterList.Add(new Person("Phillip Nottingham", "Programmer", 27, new DateTime(1974, 8, 28), 245.7, false, "sj", "Jocular, gregarious"));
			masterList.Add(new Person("Mister Null"));

            List<Person> list = new List<Person>();
            foreach (Person p in masterList)
                list.Add(new Person(p));

            // Change this value to see the performance on bigger lists.
            // Each list builds about 1000 rows per second.
            while (list.Count < 200) {
                foreach (Person p in masterList)
                    list.Add(new Person(p));
            }

			InitializeSimpleExample(list);
			InitializeComplexExample(list);
			InitializeDataSetExample();
			InitializeVirtualListExample();
            InitializeExplorerExample();
            InitializeTreeListExample();
            InitializeListPrinting();
            InitializeFastListExample(list);
            InitializeDragDropExample(list);
#if MONO
            // As of 2008-03-23, grid lines on virtual lists on Windows Mono crashes the program
            this.listViewVirtual.GridLines = false;
            this.olvFastList.GridLines = false;
#endif
        }

		void TimedRebuildList (ObjectListView olv)
		{
			Stopwatch stopWatch = new Stopwatch();

			try {
				this.Cursor = Cursors.WaitCursor;
				stopWatch.Start();
				olv.BuildList();
			} finally {
				stopWatch.Stop();
				this.Cursor = Cursors.Default;
			}

			this.toolStripStatusLabel1.Text =
				String.Format("Build time: {0} items in {1}ms, average per item: {2:F}ms",
				              olv.Items.Count,
				              stopWatch.ElapsedMilliseconds,
				              (float)stopWatch.ElapsedMilliseconds / olv.Items.Count);
		}

		void InitializeSimpleExample(List<Person> list)
		{
            this.comboBox6.SelectedIndex = 0;

            // Give this column an aspect putter, since it fetches its value using a method rather than a property
            TypedColumn<Person> tcol = new TypedColumn<Person>(this.columnHeader16);
            tcol.AspectPutter = delegate(Person x, object newValue) { x.SetRate((double)newValue); };

			// Just one line of code make everything happen.
			this.listViewSimple.SetObjects(list);
		}

		void InitializeComplexExample(List<Person> list)
		{
            this.listViewComplex.OverlayText.BackColor = Color.LightBlue;
            this.listViewComplex.OverlayText.BorderWidth = 3.0f;
            this.listViewComplex.OverlayText.BorderColor = Color.Black;

            // The following line makes getting aspect about 10x faster. Since getting the aspect is
            // the slowest part of building the ListView, it is worthwhile BUT NOT NECESSARY to do.
            TypedObjectListView<Person> tlist = new TypedObjectListView<Person>(this.listViewComplex);
            tlist.GenerateAspectGetters();
            /* The line above the equivilent to typing the following:
            tlist.GetColumn(0).AspectGetter = delegate(Person x) { return x.Name; };
            tlist.GetColumn(1).AspectGetter = delegate(Person x) { return x.Occupation; };
            tlist.GetColumn(2).AspectGetter = delegate(Person x) { return x.CulinaryRating; };
            tlist.GetColumn(3).AspectGetter = delegate(Person x) { return x.YearOfBirth; };
            tlist.GetColumn(4).AspectGetter = delegate(Person x) { return x.BirthDate; };
            tlist.GetColumn(5).AspectGetter = delegate(Person x) { return x.GetRate(); };
            tlist.GetColumn(6).AspectGetter = delegate(Person x) { return x.Comments; };
            */

            this.personColumn.AspectToStringConverter = delegate(object cellValue) {
                return ((String)cellValue).ToUpperInvariant();
            };
            this.personColumn.ImageGetter = delegate(object row) {
				// People whose names start with a vowel get a star,
				// otherwise the first half of the alphabet gets hearts
				// and the second half gets music
                string name = ((Person)row).Name;
                if (name.Length > 0 && "AEIOU".Contains(name.Substring(0, 1)))
                    return 0; // star
                else if (name.CompareTo("N") < 0)
                    return 1; // heart
                else
                    return 2; // music
			};

            // Cooking skill columns
            this.columnCookingSkill.MakeGroupies(
                new Int32[]{10, 20, 30, 40},
                new String[] {"Pay to eat out", "Suggest take-away", "Passable", "Seek dinner invitation", "Hire as chef"});

            // Hourly rate column
            this.hourlyRateColumn.MakeGroupies(
                new Double[] { 100, 1000 },
                new string[] { "Less than $100", "$100-$1000", "Megabucks" });
            this.hourlyRateColumn.AspectPutter = delegate(object x, object newValue) { ((Person)x).SetRate((double)newValue); };

            // Salary indicator column
            this.moneyImageColumn.AspectGetter = delegate(object row) {
                if (((Person)row).GetRate() < 100) return "Little";
                if (((Person)row).GetRate() > 1000) return "Lots";
                return "Medium";
            };
            this.moneyImageColumn.Renderer = new MappedImageRenderer(new Object[] { "Little", Resource1.down16, "Medium", Resource1.tick16, "Lots", Resource1.star16 });

            // Birthday column
            this.birthdayColumn.GroupKeyGetter = delegate(object row) {
				return ((Person)row).BirthDate.Month;
			};
			this.birthdayColumn.GroupKeyToTitleConverter = delegate (object key) {
				return (new DateTime(1, (int)key, 1)).ToString("MMMM");
			};
			this.birthdayColumn.ImageGetter = delegate (object row) {
				Person p = (Person)row;
				if (p.BirthDate != null && (p.BirthDate.Year % 10) == 4)
					return 3;
				else
					return -1; // no image
			};

			// Use this column to test sorting and group on TimeSpan objects
			this.daysSinceBirthColumn.AspectGetter = delegate (object row) {
				return DateTime.Now - ((Person)row).BirthDate;
			};
			this.daysSinceBirthColumn.AspectToStringConverter = delegate (object aspect) {
				return ((TimeSpan)aspect).Days.ToString();
			};

            // Show a long tooltip over cells when the control key is down
            this.listViewComplex.CellToolTipGetter = delegate(OLVColumn col, Object x) {
                if (Control.ModifierKeys == Keys.Control) {
                    return String.Format("Tool tip for '{0}', column '{1}'\r\nValue shown: '{2}'", 
                        ((Person)x).Name, col.Text, col.GetStringValue(x));
                } else
                    return null;
            };

            // Install a custom renderer that draws the Tile view in a special way
            this.listViewComplex.ItemRenderer = new BusinessCardRenderer();

            // Drag and drop support
            this.listViewComplex.DragSource = new SimpleDragSource();
            SimpleDropSink dropSink = new SimpleDropSink();
            this.listViewComplex.DropSink = dropSink;
            dropSink.CanDropOnItem = true;
            dropSink.FeedbackColor = Color.IndianRed; // just to be different

            dropSink.ModelCanDrop += new EventHandler<ModelDropEventArgs>(delegate(object sender, ModelDropEventArgs e) {
                Person person = e.TargetModel as Person;
                if (person == null) {
                    e.Effect = DragDropEffects.None;
                } else {
                    if (person.MaritalStatus == MaritalStatus.Married) {
                        e.Effect = DragDropEffects.None;
                        e.InfoMessage = "Can't drop on someone who is already married";
                    } else {
                        e.Effect = DragDropEffects.Move;
                    }
                }
            });

            dropSink.ModelDropped += new EventHandler<ModelDropEventArgs>(delegate(object sender, ModelDropEventArgs e) {
                if (e.TargetModel == null)
                    return;

                // Change the dropped people plus the target person to be married
                ((Person)e.TargetModel).MaritalStatus = MaritalStatus.Married;
                foreach (Person p in e.SourceModels)
                    p.MaritalStatus = MaritalStatus.Married;

                // Force them to refresh
                e.ListView.RefreshObject(e.TargetModel);
                e.ListView.RefreshObjects(e.SourceModels);
            });

            comboBox1.SelectedIndex = 4;
            comboBox5.SelectedIndex = 0;
            listViewComplex.SetObjects(list);
        }

        /// <summary>
        /// Hackish renderer that draw a fancy version of a person for a Tile view.
        /// </summary>
        /// <remarks>This is not the way to write a professional level renderer.
        /// It is hideously inefficient (we should at least cache the images),
        /// but it is obvious</remarks>
        internal class BusinessCardRenderer : AbstractRenderer
        {
            public override bool RenderItem(DrawListViewItemEventArgs e, Graphics g, Rectangle itemBounds, object rowObject)
            {
                // If we're in any other view than Tile, return false to say that we haven't done 
                // the rendereing and the default process should do it's stuff
                ObjectListView olv = e.Item.ListView as ObjectListView;
                if (olv == null || olv.View != View.Tile)
                    return false;

                const int spacing = 8;

                // Use buffered graphics to kill flickers
                BufferedGraphics buffered = BufferedGraphicsManager.Current.Allocate(g, itemBounds);
                g = buffered.Graphics;
                g.Clear(olv.BackColor);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                Pen grey13Pen = new Pen(Color.FromArgb(0x33, 0x33, 0x33));
                SolidBrush grey13Brush = new SolidBrush(Color.FromArgb(0x33, 0x33, 0x33));

                // Allow a border around the card
                itemBounds.Inflate(-2, -2);

                // Draw card background
                const int rounding = 20;
                GraphicsPath path = this.GetRoundedRect(itemBounds, rounding);
                g.FillPath(Brushes.LemonChiffon, path);
                if (e.Item.Selected)
                    g.DrawPath(Pens.Blue, path);
                else
                    g.DrawPath(grey13Pen, path);

                g.Clip = new Region(itemBounds);

                // Draw the photo
                Rectangle photoRect = itemBounds;
                photoRect.Inflate(-spacing, -spacing);
                Person person = rowObject as Person;
                if (person != null) {
                    photoRect.Width = 75;
                    string photoFile = String.Format(@".\Photos\{0}.png", person.Photo);
                    if (File.Exists(photoFile)) {
                        Image photo = Image.FromFile(photoFile);
                        if (photo.Width > photoRect.Width)
                            photoRect.Height = (int)(photo.Height * ((float)photoRect.Width / photo.Width));
                        else
                            photoRect.Height = photo.Height;
                        g.DrawImage(photo, photoRect);
                    } else {
                        g.DrawRectangle(Pens.DarkGray, photoRect);
                    }
                }

                // Now draw the text portion
                RectangleF textBoxRect = photoRect;
                textBoxRect.X += (photoRect.Width + spacing);
                textBoxRect.Width = itemBounds.Right - textBoxRect.X - spacing;

                // Measure the height of the title
                StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
                fmt.Trimming = StringTrimming.EllipsisCharacter;
                fmt.Alignment = StringAlignment.Center;
                fmt.LineAlignment = StringAlignment.Near;
                Font font = new Font("Tahoma", 11);
                String txt = e.Item.Text;
                SizeF size = g.MeasureString(txt, font, (int)textBoxRect.Width, fmt);

                // Draw the title
                RectangleF r3 = textBoxRect;
                r3.Height = size.Height;
                path = this.GetRoundedRect(r3, 15);
                if (e.Item.Selected)
                    g.FillPath(new SolidBrush(olv.HighlightBackgroundColorOrDefault), path);
                else
                    g.FillPath(grey13Brush, path);
                g.DrawString(txt, font, Brushes.AliceBlue, textBoxRect, fmt);
                textBoxRect.Y += size.Height + spacing;

                // Draw the other bits of information
                font = new Font("Tahoma", 8);
                size = g.MeasureString("Wj", font, itemBounds.Width, fmt);
                textBoxRect.Height = size.Height;
                fmt.Alignment = StringAlignment.Near;
                for (int i = 0; i < olv.Columns.Count; i++) {
                    OLVColumn column = olv.GetColumn(i);
                    if (column.IsTileViewColumn) {
                        txt = column.GetStringValue(rowObject);
                        g.DrawString(txt, font, grey13Brush, textBoxRect, fmt);
                        textBoxRect.Y += size.Height;
                    }
                }

                // Finally render the buffered graphics
                buffered.Render();
                buffered.Dispose();

                // Return true to say that we've handled the drawing
                return true;
            }

            private GraphicsPath GetRoundedRect(RectangleF rect, float diameter)
            {
                GraphicsPath path = new GraphicsPath();

                RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);
                path.AddArc(arc, 180, 90);
                arc.X = rect.Right - diameter;
                path.AddArc(arc, 270, 90);
                arc.Y = rect.Bottom - diameter;
                path.AddArc(arc, 0, 90);
                arc.X = rect.Left;
                path.AddArc(arc, 90, 90);
                path.CloseFigure();

                return path;
            }
        }

		void InitializeDataSetExample ()
		{
			this.olvColumn1.ImageGetter  = delegate (object row) { return "user"; };

            // Long values in the first column will wrap
            BaseRenderer renderer = new BaseRenderer();
            renderer.CanWrap = true;
            this.listViewDataSet.GetColumn(0).Renderer = renderer;

            this.salaryColumn.MakeGroupies(
                new UInt32[] { 20000, 100000 },
                new string[] { "Lowly worker", "Middle management", "Rarified elevation" });

            this.heightColumn.MakeGroupies(
                new Double[] { 1.50, 1.70, 1.85 },
                new string[] { "Shortie",  "Normal", "Tall", "Really tall" });

            this.listViewDataSet.RowFormatter = delegate(OLVListItem olvi) {
                string[] colorNames = new string[] { "red", "green", "blue", "yellow" };
                // For some reason, changes to the background of column 0 don't take place
                // immediately. The list has to be rebuild before the background color changes.
                foreach (ListViewItem.ListViewSubItem subItem in olvi.SubItems) {
                    foreach (string name in colorNames) {
                        if (subItem.Text.ToLowerInvariant().Contains(name)) {
                            olvi.UseItemStyleForSubItems = false;
                            if (subItem.Text.ToLowerInvariant().Contains("bk-" + name))
                                subItem.BackColor = Color.FromName(name);
                            else
                                subItem.ForeColor = Color.FromName(name);
                        }
                    }
                    FontStyle style = FontStyle.Regular;
                    if (subItem.Text.ToLowerInvariant().Contains("bold"))
                        style |= FontStyle.Bold;
                    if (subItem.Text.ToLowerInvariant().Contains("italic"))
                        style |= FontStyle.Italic;
                    if (subItem.Text.ToLowerInvariant().Contains("underline"))
                        style |= FontStyle.Underline;
                    if (subItem.Text.ToLowerInvariant().Contains("strikeout"))
                        style |= FontStyle.Strikeout;

                    if (style != FontStyle.Regular) {
                        olvi.UseItemStyleForSubItems = false;
                        subItem.Font = new Font(subItem.Font, style);
                    }
                }
            };

            this.comboBox3.SelectedIndex = 4;
            this.comboBox7.SelectedIndex = 0;
            this.rowHeightUpDown.Value = 32;

            LoadXmlIntoList();
		}

        // A sorter to order Person objects according to a given column in a list view
        internal class MasterListSorter : Comparer<Person>
        {
            public MasterListSorter(OLVColumn col, SortOrder order)
            {
                this.column = col;
                this.sortOrder = order;
            }
            private OLVColumn column;
            private SortOrder sortOrder;

            public override int Compare(Person x, Person y)
            {
                IComparable xValue = this.column.GetValue(x) as IComparable;
                IComparable yValue = this.column.GetValue(y) as IComparable;

                int result = 0;
                if (xValue == null || yValue == null) {
                    if (xValue == null && yValue == null)
                        result = 0;
                    else
                        if (xValue != null)
                            result = 1;
                        else
                            result = -1;
                } else
                    result = xValue.CompareTo(yValue);

                if (this.sortOrder == SortOrder.Ascending)
                    return result;
                else
                    return 0 - result;
            }
        }

		void InitializeVirtualListExample ()
		{
            this.listViewVirtual.BooleanCheckStateGetter = delegate(object x) {
                return ((Person)x).IsActive;
            };
            this.listViewVirtual.BooleanCheckStatePutter = delegate(object x, bool newValue) {
                ((Person)x).IsActive = newValue;
                return newValue;
            };
            this.listViewVirtual.VirtualListSize = 10000000;
			this.listViewVirtual.RowGetter = delegate (int i) {
				Person p = masterList[i % masterList.Count];
				p.serialNumber = i;
				return p;
			};

            // Install a custom sorter, just to show how it could be done. We don't
            // have a backing store that can sort all 10 million items, so we have to be
            // content with sorting the master list and showing that sorted. It gives the idea.
            this.listViewVirtual.CustomSorter = delegate(OLVColumn col, SortOrder order) {
                masterList.Sort(new MasterListSorter(col, order));
                this.listViewVirtual.BuildList();
            };

			// Install aspect getters to optimize performance
			this.olvColumn4.AspectGetter = delegate (object x) {return ((Person)x).Name;};
			this.olvColumn5.AspectGetter = delegate (object x) {return ((Person)x).Occupation;};
			this.olvColumn7.AspectGetter = delegate (object x) {return ((Person)x).CulinaryRating;};
			this.olvColumn8.AspectGetter = delegate (object x) {return ((Person)x).YearOfBirth;};
			this.olvColumn9.AspectGetter = delegate (object x) {return ((Person)x).BirthDate;};
			this.olvColumn10.AspectGetter = delegate (object x) {return ((Person)x).GetRate();};
            this.olvColumn10.AspectPutter = delegate(object x, object newValue) { ((Person)x).SetRate((double)newValue); };

            // Install a RowFormatter to setup a tooltip on the item
            this.listViewVirtual.RowFormatter = delegate(OLVListItem lvi) {
                lvi.ToolTipText = "This is a long tool tip for '" + lvi.Text + "' that does nothing except waste space.";
            };

			this.olvColumn4.ImageGetter = delegate (object row) {
				// People whose names start with a vowel get a star,
				// otherwise the first half of the alphabet gets hearts
				// and the second half gets music
				if ("AEIOU".Contains(((Person)row).Name.Substring(0, 1)))
					return 0; // star
				else if (((Person)row).Name.CompareTo("N") < 0)
					return 1; // heart
				else
					return 2; // music
			};
			this.olvColumn5.ImageGetter  = delegate (object row) { return "user"; }; // user icon

            this.olvColumn7.Renderer = new MultiImageRenderer("star", 5, 0, 50);


            this.comboBox2.SelectedIndex = 4;
            this.comboBox8.SelectedIndex = 0;
            this.comboBoxNagLevel.SelectedIndex = 0;
		}

        void InitializeExplorerExample()
        {
            // Draw the system icon next to the name
#if !MONO
            SysImageListHelper helper = new SysImageListHelper(this.listViewFiles);
            this.olvColumnFileName.ImageGetter = delegate(object x) {
                return helper.GetImageIndex(((FileSystemInfo)x).FullName);
            };
#endif
            // Show the size of files as GB, MB and KBs. Also, group them by
            // some meaningless divisions
            this.olvColumnSize.AspectGetter = delegate(object x) {
                if (x is DirectoryInfo)
                    return (long)-1;

                try {
                    return ((FileInfo)x).Length;
                }
                catch (System.IO.FileNotFoundException) {
                    // Mono 1.2.6 throws this for hidden files
                    return (long)-2;
                }
            };
            this.olvColumnSize.AspectToStringConverter = delegate(object x) {
                if ((long)x == -1) // folder
                    return "";
                else
                    return this.FormatFileSize((long)x);
            };
            this.olvColumnSize.MakeGroupies(new long[] { 0, 1024 * 1024, 512 * 1024 * 1024 },
                new string[] { "Folders", "Small", "Big", "Disk space chewer" });

            // Group by month-year, rather than date
            // This code is duplicated for FileCreated and FileModified, so we really should
            // create named methods rather than using anonymous delegates.
            this.olvColumnFileCreated.GroupKeyGetter = delegate(object x) {
                DateTime dt = ((FileSystemInfo)x).CreationTime;
                return new DateTime(dt.Year, dt.Month, 1);
            };
            this.olvColumnFileCreated.GroupKeyToTitleConverter = delegate(object x) {
                return ((DateTime)x).ToString("MMMM yyyy");
            };

            // Group by month-year, rather than date
            this.olvColumnFileModified.GroupKeyGetter = delegate(object x) {
                DateTime dt = ((FileSystemInfo)x).LastWriteTime;
                return new DateTime(dt.Year, dt.Month, 1);
            };
            this.olvColumnFileModified.GroupKeyToTitleConverter = delegate(object x) {
                return ((DateTime)x).ToString("MMMM yyyy");
            };

            // Show the system description for this object
            this.olvColumnFileType.AspectGetter = delegate(object x) {
                return ShellUtilities.GetFileType(((FileSystemInfo)x).FullName);
            };

            // Show the file attributes for this object
            this.olvColumnAttributes.AspectGetter = delegate(object x) {
                return ((FileSystemInfo)x).Attributes;
            };
            FlagRenderer<FileAttributes> attributesRenderer = new FlagRenderer<FileAttributes>();
            attributesRenderer.Add(FileAttributes.Archive, "archive");
            attributesRenderer.Add(FileAttributes.ReadOnly, "readonly");
            attributesRenderer.Add(FileAttributes.System, "system");
            attributesRenderer.Add(FileAttributes.Hidden, "hidden");
            attributesRenderer.Add(FileAttributes.Temporary, "temporary");
            this.olvColumnAttributes.Renderer = attributesRenderer;

            this.comboBox4.SelectedIndex = 4;
            this.textBoxFolderPath.Text = @"c:\";
            this.PopulateListFromPath(this.textBoxFolderPath.Text);
        }


        void InitializeTreeListExample()
        {
            this.treeListView.CanExpandGetter = delegate(object x) {
                return (x is DirectoryInfo);
            };
            this.treeListView.ChildrenGetter = delegate(object x) {
                DirectoryInfo dir = (DirectoryInfo)x;
                try {
                    return new ArrayList(dir.GetFileSystemInfos());
                }
                catch (UnauthorizedAccessException ex) {
                    MessageBox.Show(this, ex.Message, "ObjectListViewDemo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return new ArrayList();
                }
            };

            this.treeListView.CheckBoxes = true;

            // You can change the way the connection lines are drawn by changing the pen
            //((TreeListView.TreeRenderer)this.treeListView.TreeColumnRenderer).LinePen = Pens.Firebrick;

            //-------------------------------------------------------------------
            // Eveything after this is the same as the Explorer example tab --
            // nothing specific to the TreeListView. It doesn't have the grouping
            // delegates, since TreeListViews can't show groups.

            // Draw the system icon next to the name
#if !MONO
            SysImageListHelper helper = new SysImageListHelper(this.treeListView);
            this.treeColumnName.ImageGetter = delegate(object x) {
                return helper.GetImageIndex(((FileSystemInfo)x).FullName);
            };
#endif
            // Show the size of files as GB, MB and KBs. Also, group them by
            // some meaningless divisions
            this.treeColumnSize.AspectGetter = delegate(object x) {
                if (x is DirectoryInfo)
                    return (long)-1;

                try {
                    return ((FileInfo)x).Length;
                }
                catch (System.IO.FileNotFoundException) {
                    // Mono 1.2.6 throws this for hidden files
                    return (long)-2;
                }
            };
            this.treeColumnSize.AspectToStringConverter = delegate(object x) {
                if ((long)x == -1) // folder
                    return "";
                else
                    return this.FormatFileSize((long)x);
            };

            // Show the system description for this object
            this.treeColumnFileType.AspectGetter = delegate(object x) {
                return ShellUtilities.GetFileType(((FileSystemInfo)x).FullName);
            };

            // Show the file attributes for this object
            this.treeColumnAttributes.AspectGetter = delegate(object x) {
                return ((FileSystemInfo)x).Attributes;
            };
            FlagRenderer<FileAttributes> attributesRenderer = new FlagRenderer<FileAttributes>();
            attributesRenderer.Add(FileAttributes.Archive, "archive");
            attributesRenderer.Add(FileAttributes.ReadOnly, "readonly");
            attributesRenderer.Add(FileAttributes.System, "system");
            attributesRenderer.Add(FileAttributes.Hidden, "hidden");
            attributesRenderer.Add(FileAttributes.Temporary, "temporary");
            this.treeColumnAttributes.Renderer = attributesRenderer;

            // List all drives as the roots of the tree
            ArrayList roots = new ArrayList();
            foreach (DriveInfo di in DriveInfo.GetDrives()) {
                if (di.IsReady)
                    roots.Add(new DirectoryInfo(di.Name));
            }
            this.treeListView.Roots = roots;
            this.treeListView.CellEditActivation = ObjectListView.CellEditActivateMode.F2Only;
        }

        void InitializeListPrinting()
        {
            // For some reason the Form Designer loses these settings
            this.printPreviewControl1.Zoom = 1;
            this.printPreviewControl1.AutoZoom = true;

            this.UpdatePrintPreview();
        }

        string FormatFileSize(long size)
        {
            int[] limits = new int[] { 1024 * 1024 * 1024, 1024 * 1024, 1024 };
            string[] units = new string[] { "GB", "MB", "KB" };

            for (int i = 0; i < limits.Length; i++) {
                if (size >= limits[i])
                    return String.Format("{0:#,##0.##} " + units[i], ((double)size / limits[i]));
            }

            return String.Format("{0} bytes", size); ;
        }

		void LoadXmlIntoList()
		{
			DataSet ds = LoadDatasetFromXml("Persons.xml");

			if (ds.Tables.Count > 0)
			{
#if !MONO
				this.dataGridView1.DataSource = ds;
				this.dataGridView1.DataMember = "Person";
#endif
                // Install this data source
#if MONO
                DataTable personTable = ds.Tables["Person"];
                this.listViewDataSet.DataSource = personTable;
#else
                this.listViewDataSet.DataSource = new BindingSource(ds, "Person");
#endif
                // Test with BindingSource
                //this.listViewDataSet.DataSource = new BindingSource(ds, "Person");

                // Test with DataTable
                //DataTable personTable = ds.Tables["Person"];
                //this.listViewDataSet.DataSource = personTable;

                // Test with DataView
                //DataTable personTable = ds.Tables["Person"];
                //this.listViewDataSet.DataSource = new DataView(personTable);

                // Test with DataSet
                //this.listViewDataSet.DataMember = "Person";
                //this.listViewDataSet.DataSource = ds;

                // Test with DataViewManager
                //this.listViewDataSet.DataMember = "Person";
                //this.listViewDataSet.DataSource = new DataViewManager(ds);

				// Test with nulls
                //this.listViewDataSet.DataMember = null;
                //this.listViewDataSet.DataSource = null;
			}
		}

		DataSet LoadDatasetFromXml(string fileName)
		{
			DataSet ds = new DataSet();
			FileStream fs = null;

			try {
				fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				StreamReader reader = new StreamReader(fs);
	      		ds.ReadXml(reader);
			} catch (Exception e) {
				MessageBox.Show(e.ToString());
			} finally {
				if (fs != null)
					fs.Close();
			}

			return ds;
		}

		void TimedReloadXml ()
		{
			Stopwatch stopWatch = new Stopwatch();

			try {
				this.Cursor = Cursors.WaitCursor;
				stopWatch.Start();
				this.LoadXmlIntoList();
			} finally {
				stopWatch.Stop();
				this.Cursor = Cursors.Default;
			}

			this.toolStripStatusLabel1.Text =
				String.Format("XML Load: {0} items in {1}ms, average per item: {2:F}ms",
				              listViewDataSet.Items.Count,
				              stopWatch.ElapsedMilliseconds,
				              stopWatch.ElapsedMilliseconds / listViewDataSet.Items.Count);
        }

        #region Form event handlers

        private void MainForm_Load(object sender, EventArgs e) {
            //this.BeginInvoke(new MethodInvoker(delegate() {
            //    this.testForm = new Form1();
            //    this.testForm.Attach(this.listViewSimple);
            //    this.testForm.Show(this.listViewSimple.TopLevelControl);
            //}));
        }
        //Form1 testForm;

        #endregion

        #region Utilities

        void ShowGroupsChecked(ObjectListView olv, CheckBox cb)
		{
			olv.ShowGroups = cb.Checked;
			olv.BuildList();
		}

		void ShowLabelsOnGroupsChecked(ObjectListView olv, CheckBox cb)
		{
			olv.ShowItemCountOnGroups = cb.Checked;
			olv.BuildGroups();
        }

        void HandleSelectionChanged(ObjectListView listView)
        {
            string msg;
            Person p = (Person)listView.GetSelectedObject();
            if (p == null)
                msg = listView.SelectedIndices.Count.ToString();
            else
                msg = "'" + p.Name + "'";
            this.toolStripStatusLabel1.Text = String.Format("Selected {0} of {1} items", msg, listView.GetItemCount());
        }

        void ListViewSelectedIndexChanged(object sender, System.EventArgs e)
        {
            HandleSelectionChanged((ObjectListView)sender);
        }

        private void ChangeView(ObjectListView listview, ComboBox comboBox)
        {
            // Handle restrictions on Tile view
            if (comboBox.SelectedIndex == 3) {
                if (listview.VirtualMode) {
                    MessageBox.Show("Sorry, Microsoft says that virtual lists can't use Tile view.", "Object List View Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (listview.CheckBoxes) {
                    MessageBox.Show("Microsoft says that Tile view can't have checkboxes, so CheckBoxes have been turned off on this list.", "Object List View Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listview.CheckBoxes = false;
                }
            }
            
            switch (comboBox.SelectedIndex) {
                case 0: listview.View = View.SmallIcon; break;
                case 1: listview.View = View.LargeIcon; break;
                case 2: listview.View = View.List; break;
                case 3: listview.View = View.Tile; break;
                case 4: listview.View = View.Details; break;
            }
        }

        void ChangeOwnerDrawn(ObjectListView listview, CheckBox cb)
        {
            listview.OwnerDraw = cb.Checked;
            listview.BuildList();
        }

        #endregion

        #region Simple Tab Event Handlers

		void CheckBox3CheckedChanged(object sender, System.EventArgs e)
		{
			ShowGroupsChecked(this.listViewSimple, (CheckBox)sender);
		}

		void CheckBox4CheckedChanged(object sender, System.EventArgs e)
		{
			ShowLabelsOnGroupsChecked(this.listViewSimple, (CheckBox)sender);
		}

		void Button1Click(object sender, System.EventArgs e)
		{
            this.TimedRebuildList(this.listViewSimple);
		}

		void Button4Click(object sender, System.EventArgs e)
		{
			// Silly example just to make sure that object selection works

            listViewSimple.SelectedObjects = listViewComplex.SelectedObjects;
            listViewSimple.Select();

            listViewSimple.CopyObjectsToClipboard(listViewSimple.CheckedObjects);
		}

        private void button7_Click(object sender, EventArgs e)
        {
            Person person = new Person("Some One Else " + System.Environment.TickCount);
            this.listViewSimple.AddObject(person);
            this.listViewSimple.EnsureModelVisible(person);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.listViewSimple.RemoveObject(this.listViewSimple.SelectedObject);
        }

        #endregion

        #region Complex Tab Event Handlers

        void CheckBox1CheckedChanged(object sender, System.EventArgs e)
		{
			ShowGroupsChecked(this.listViewComplex, (CheckBox)sender);
		}

		void CheckBox2CheckedChanged(object sender, System.EventArgs e)
		{
			ShowLabelsOnGroupsChecked(this.listViewComplex, (CheckBox)sender);
		}

		void Button2Click(object sender, System.EventArgs e)
		{
            this.TimedRebuildList(this.listViewComplex);
        }

		void Button5Click(object sender, System.EventArgs e)
		{
            this.listViewComplex.CopySelectionToClipboard();
            listViewComplex.SelectedObjects = listViewSimple.SelectedObjects;
            listViewComplex.Select();
		}

		void CheckBox6CheckedChanged(object sender, EventArgs e)
		{
            if (comboBox1.SelectedIndex == 3 && this.checkBox6.Checked)
                this.listViewComplex.TileSize = new Size(250, 120);

            ChangeOwnerDrawn(this.listViewComplex, (CheckBox)sender);
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 3 && this.listViewComplex.OwnerDraw)
                this.listViewComplex.TileSize = new Size(250, 120);

            this.ChangeView(this.listViewComplex, (ComboBox)sender);
        }
        #endregion

        #region Dataset Tab Event Handlers

		void CheckBox7CheckedChanged(object sender, System.EventArgs e)
		{
			ShowGroupsChecked(this.listViewDataSet, (CheckBox)sender);
		}

		void CheckBox8CheckedChanged(object sender, System.EventArgs e)
		{
			ShowLabelsOnGroupsChecked(this.listViewDataSet, (CheckBox)sender);
		}

		void Button3Click(object sender, System.EventArgs e)
		{
			this.TimedReloadXml();
        }

        void CheckBox5CheckedChanged(object sender, EventArgs e)
        {
            ChangeOwnerDrawn(this.listViewDataSet, (CheckBox)sender);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeView(this.listViewDataSet, (ComboBox)sender);
        }

        void ListViewDataSetSelectedIndexChanged(object sender, System.EventArgs e)
        {
            ObjectListView listView = (ObjectListView)sender;
            DataRowView row = (DataRowView)listView.GetSelectedObject();
            string msg;
            if (row == null)
                msg = listView.SelectedIndices.Count.ToString();
            else
                msg = "'" + row["Name"] + "'";
            this.toolStripStatusLabel1.Text = String.Format("Selected {0} of {1} items", msg, listView.Items.Count);

        }

        private void rowHeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.listViewDataSet.RowHeight = Decimal.ToInt32(this.rowHeightUpDown.Value);
        }

        private void checkBoxPause_CheckedChanged(object sender, EventArgs e)
        {
            this.listViewDataSet.PauseAnimations(((CheckBox)sender).Checked);
        }

        #endregion

        #region Virtual Tab Event Handlers

		void CheckBox9CheckedChanged(object sender, EventArgs e)
		{
            this.ChangeOwnerDrawn(this.listViewVirtual, (CheckBox)sender);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeView(this.listViewVirtual, (ComboBox)sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.listViewVirtual.SelectAll();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.listViewVirtual.DeselectAll();
        }

		#endregion

        #region Explorer Tab event handlers

        void TextBoxFolderPathTextChanged(object sender, EventArgs e)
		{
            if (Directory.Exists(this.textBoxFolderPath.Text)) {
                this.textBoxFolderPath.ForeColor = Color.Black;
                this.buttonGo.Enabled = true;
                this.buttonUp.Enabled = true;
            } else {
                this.textBoxFolderPath.ForeColor = Color.Red;
                this.buttonGo.Enabled = false;
                this.buttonUp.Enabled = false;
            }
        }

		void ButtonGoClick(object sender, EventArgs e)
		{
			string path = this.textBoxFolderPath.Text;
			this.PopulateListFromPath(path);
		}

		void PopulateListFromPath(string path)
		{
			DirectoryInfo pathInfo = new DirectoryInfo(path);
			if (!pathInfo.Exists)
				return;

            Stopwatch sw = new Stopwatch();

            Cursor.Current = Cursors.WaitCursor;
            sw.Start();
			this.listViewFiles.SetObjects(pathInfo.GetFileSystemInfos());
            sw.Stop();
            Cursor.Current = Cursors.Default;

            float msPerItem = (listViewFiles.Items.Count == 0 ? 0 : (float)sw.ElapsedMilliseconds / listViewFiles.Items.Count);
            this.toolStripStatusLabel1.Text = String.Format("Timed build: {0} items in {1}ms ({2:F}ms per item)",
                listViewFiles.Items.Count, sw.ElapsedMilliseconds, msPerItem);
		}

		void CheckBox12CheckedChanged(object sender, EventArgs e)
		{
			ShowGroupsChecked(this.listViewFiles, (CheckBox)sender);
		}

		void CheckBox11CheckedChanged(object sender, EventArgs e)
		{
            this.ShowLabelsOnGroupsChecked(this.listViewFiles, (CheckBox)sender);
		}

		void CheckBox10CheckedChanged(object sender, EventArgs e)
		{
            this.ChangeOwnerDrawn(this.listViewFiles, (CheckBox)sender);
		}

		void ComboBox4SelectedIndexChanged(object sender, EventArgs e)
		{
           this.ChangeView(this.listViewFiles, (ComboBox)sender);
           this.button13.Enabled = (this.listViewFiles.View == View.Details || this.listViewFiles.View == View.Tile);
       }

        private void listViewFiles_ItemActivate(object sender, EventArgs e)
        {
            Object rowObject = this.listViewFiles.SelectedObject;
            if (rowObject == null)
                return;

            if (rowObject is DirectoryInfo) {
                this.textBoxFolderPath.Text = ((DirectoryInfo)rowObject).FullName;
                this.buttonGo.PerformClick();
            } else {
                ShellUtilities.Execute(((FileInfo)rowObject).FullName);
            }
        }

        private void textBoxFolderPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
                this.buttonGo.PerformClick();
                e.Handled = true;
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = Directory.GetParent(this.textBoxFolderPath.Text);
            if (di == null)
                System.Media.SystemSounds.Asterisk.Play();
            else {
                this.textBoxFolderPath.Text = di.FullName;
                this.buttonGo.PerformClick();
            }
        }

        byte[] fileListViewState;

        private void buttonSaveState_Click(object sender, EventArgs e)
        {
            this.fileListViewState = this.listViewFiles.SaveState();
            this.buttonRestoreState.Enabled = true;
        }

        private void buttonRestoreState_Click(object sender, EventArgs e)
        {
            this.listViewFiles.RestoreState(this.fileListViewState);
        }

        #endregion

        #region ListView printing

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.listViewPrinter1.PageSetup();
            this.UpdatePrintPreview();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintPreview();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (rbShowVirtual.Checked == true) {
                string msg = "Be careful when printing the virtual list.\n\nIt contains 10 million rows. If you select to print all pages, it will try to print 250,000 pages! That seems little excessive for a demo.\n\nYou have been warned.";
                MessageBox.Show(msg, "Be careful", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.listViewPrinter1.PrintWithDialog();
        }

        void Button13Click(object sender, EventArgs e)
        {
            this.UpdatePrintPreview();
        }

        void UpdatePreview(object sender, EventArgs e)
        {
            this.UpdatePrintPreview();
        }

        void UpdatePrintPreview()
        {
            if (this.rbShowSimple.Checked == true)
				this.listViewPrinter1.ListView = this.listViewSimple;
			else if (this.rbShowComplex.Checked == true)
				this.listViewPrinter1.ListView = this.listViewComplex;
			else if (this.rbShowDataset.Checked == true)
				this.listViewPrinter1.ListView = this.listViewDataSet;
            else if (this.rbShowVirtual.Checked == true)
                this.listViewPrinter1.ListView = this.listViewVirtual;
            else if (this.rbShowFileExplorer.Checked == true)
                this.listViewPrinter1.ListView = this.listViewFiles;

			this.listViewPrinter1.DocumentName = this.tbTitle.Text;
			this.listViewPrinter1.Header = this.tbHeader.Text.Replace("\\t", "\t");
            this.listViewPrinter1.Footer = this.tbFooter.Text.Replace("\\t", "\t");
			this.listViewPrinter1.Watermark = this.tbWatermark.Text;

			this.listViewPrinter1.IsShrinkToFit = this.cbShrinkToFit.Checked;
			this.listViewPrinter1.IsTextOnly = !this.cbIncludeImages.Checked;
			this.listViewPrinter1.IsPrintSelectionOnly = this.cbPrintOnlySelection.Checked;

			if (this.rbStyleMinimal.Checked == true)
				this.ApplyMinimalFormatting();
			else if (this.rbStyleModern.Checked == true)
				this.ApplyModernFormatting();
			else if (this.rbStyleTooMuch.Checked == true)
				this.ApplyOverTheTopFormatting();

            if (this.cbCellGridLines.Checked == false)
                this.listViewPrinter1.ListGridPen = null;

            this.listViewPrinter1.FirstPage = (int)this.numericUpDown1.Value;
            this.listViewPrinter1.LastPage = (int)this.numericUpDown2.Value;

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
            Brush brush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.Gray, Color.White);
            this.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 2, brush);

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
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.DarkBlue, Color.White);

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer();
            this.listViewPrinter1.FooterFormat.BackgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.White, Color.Blue);

            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(new Font("Verdana", 12));

            this.listViewPrinter1.WatermarkFont = null;
            this.listViewPrinter1.WatermarkColor = Color.Empty;
        }

        /// <summary>
        /// Give the report a minimal set of default formatting values.
        /// </summary>
        public void ApplyOverTheTopFormatting()
        {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Ms Sans Serif", 9);
            this.listViewPrinter1.ListGridPen = new Pen(Color.Blue, 0.5f);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header(new Font("Comic Sans MS", 36));
            this.listViewPrinter1.HeaderFormat.TextBrush = new LinearGradientBrush(new Point(0, 0), new Point(900, 0), Color.Black, Color.Blue);
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = new TextureBrush(Resource1.star16, WrapMode.Tile);
            this.listViewPrinter1.HeaderFormat.SetBorder(Sides.All, 10, new LinearGradientBrush(new Point(0, 0), new Point(300, 0), Color.Purple, Color.Pink));

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer(new Font("Comic Sans MS", 12));
            this.listViewPrinter1.FooterFormat.TextBrush = Brushes.Blue;
            this.listViewPrinter1.FooterFormat.BackgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.Gold, Color.Green);
            this.listViewPrinter1.FooterFormat.SetBorderPen(Sides.All, new Pen(Color.FromArgb(128, Color.Green), 5));

            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            Brush brush = new HatchBrush(HatchStyle.LargeConfetti, Color.Blue, Color.Empty);
            this.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 5, brush);

            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(new Font("Comic Sans MS", 12));
            this.listViewPrinter1.ListHeaderFormat.BackgroundBrush = Brushes.PowderBlue;
            this.listViewPrinter1.ListHeaderFormat.TextBrush = Brushes.Black;

            this.listViewPrinter1.WatermarkFont = new Font("Comic Sans MS", 72);
            this.listViewPrinter1.WatermarkColor = Color.Red;
        }

        private void listViewPrinter1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            this.toolStripStatusLabel1.Text = String.Format("Printing page #{0}...", this.listViewPrinter1.PageNumber);
            this.Update();
        }

        private void listViewPrinter1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.toolStripStatusLabel1.Text = "Printing done";
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 5)
                this.UpdatePrintPreview();
        }

        #endregion

        #region Cell editing example

        private void listViewComplex_CellEditStarting(object sender, CellEditEventArgs e)
        {
            // We only want to mess with the Cooking Skill column
            if (e.Column.Text != "Cooking skill")
                return;

            ComboBox cb = new ComboBox();
            cb.Bounds = e.CellBounds;
            cb.Font = ((ObjectListView)sender).Font;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Items.AddRange(new String[] { "Pay to eat out", "Suggest take-away", "Passable", "Seek dinner invitation", "Hire as chef" });
            cb.SelectedIndex = Math.Max(0, Math.Min(cb.Items.Count-1, ((int)e.Value) / 10));
            cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
            cb.Tag = e.RowObject; // remember which person we are editing
            e.Control = cb;
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ((Person)cb.Tag).CulinaryRating = cb.SelectedIndex * 10;
        }

        private void listViewComplex_CellEditValidating(object sender, CellEditEventArgs e)
        {
            // Disallow professions from starting with "a" or "z" -- just to be arbitrary
            if (e.Column.Text == "Occupation") {
                string newValue = ((TextBox)e.Control).Text;
                if (newValue.ToLowerInvariant().StartsWith("a") || newValue.ToLowerInvariant().StartsWith("z")) {
                    e.Cancel = true;
                    MessageBox.Show(this, "Occupations cannot begin with 'a' or 'z' (just to show cell edit validation at work).", "ObjectListViewDemo",
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

            // Disallow birthdays from being on the 29th -- just to be arbitrary
            if (e.Column.Text == "Birthday") {
                DateTime newValue = ((DateTimePicker)e.Control).Value;
                if (newValue != null && newValue.Day == 29) {
                    e.Cancel = true;
                    MessageBox.Show(this, "Sorry. Birthdays cannot be on 29th of any month (just to show cell edit validation at work).", "ObjectListViewDemo",
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

        }

        private void listViewComplex_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            // We only want to mess with the Cooking Skill column
            if (e.Column.Text != "Cooking skill")
                return;

            // Stop listening for change events
            ((ComboBox)e.Control).SelectedIndexChanged -= new EventHandler(cb_SelectedIndexChanged);

            // Any updating will have been down in the SelectedIndexChanged event handler
            // Here we simply make the list redraw the involved ListViewItem
            ((ObjectListView)sender).RefreshItem(e.ListViewItem);

            // We have updated the model object, so we cancel the auto update
            e.Cancel = true;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeEditable(this.listViewComplex, (ComboBox)sender);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeEditable(this.listViewSimple, (ComboBox)sender);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeEditable(this.listViewDataSet, (ComboBox)sender);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeEditable(this.listViewVirtual, (ComboBox)sender);
        }

        private void ChangeEditable(ObjectListView objectListView, ComboBox comboBox)
        {
            if (comboBox.Text == "No")
                objectListView.CellEditActivation = ObjectListView.CellEditActivateMode.None;
            else if (comboBox.Text == "Single Click")
                objectListView.CellEditActivation = ObjectListView.CellEditActivateMode.SingleClick;
            else if (comboBox.Text == "Double Click")
                objectListView.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
            else
                objectListView.CellEditActivation = ObjectListView.CellEditActivateMode.F2Only;
        }

        #endregion

        private void InitializeFastListExample(List<Person> list)
        {
            this.olvFastList.BooleanCheckStateGetter = delegate(object x) {
                return ((Person)x).IsActive;
            };
            this.olvFastList.BooleanCheckStatePutter = delegate(object x, bool newValue) {
                ((Person)x).IsActive = newValue;
                return newValue;
            };
            this.olvColumn18.AspectGetter = delegate(object x) { return ((Person)x).Name; };

            this.olvColumn18.ImageGetter = delegate(object row) {
                // People whose names start with a vowel get a star,
                // otherwise the first half of the alphabet gets hearts
                // and the second half gets music
                if ("AEIOU".Contains(((Person)row).Name.Substring(0, 1)))
                    return 0; // star
                else if (((Person)row).Name.CompareTo("N") < 0)
                    return 1; // heart
                else
                    return 2; // music
            };

            this.olvColumn19.AspectGetter = delegate(object x) { return ((Person)x).Occupation; };
            this.olvColumn26.AspectGetter = delegate(object x) { return ((Person)x).CulinaryRating; };
            this.olvColumn26.Renderer = new MultiImageRenderer(Resource1.star16, 5, 0, 40);

            this.olvColumn27.AspectGetter = delegate(object x) { return ((Person)x).YearOfBirth; };

            this.olvColumn28.AspectGetter = delegate(object x) { return ((Person)x).BirthDate; };
            this.olvColumn28.ImageGetter = delegate(object row) {
                Person p = (Person)row;
                if (p.BirthDate != null && (p.BirthDate.Year % 10) == 4)
                    return 3;
                else
                    return -1; // no image
            };

            this.olvColumn29.AspectGetter = delegate(object x) { return ((Person)x).GetRate(); };
            this.olvColumn29.AspectPutter = delegate(object x, object newValue) { ((Person)x).SetRate((double)newValue); };

            this.olvColumn31.AspectGetter = delegate(object row) {
                if (((Person)row).GetRate() < 100) return "Little";
                if (((Person)row).GetRate() > 1000) return "Lots";
                return "Medium";
            };
            this.olvColumn31.Renderer = new MappedImageRenderer(new Object[] { "Little", Resource1.down16, "Medium", Resource1.tick16, "Lots", Resource1.star16 });
            this.olvColumn32.AspectGetter = delegate(object row) {
                return DateTime.Now - ((Person)row).BirthDate;
            };
            this.olvColumn32.AspectToStringConverter = delegate(object aspect) {
                return ((TimeSpan)aspect).Days.ToString();
            };
            this.olvColumn33.AspectGetter = delegate(object row) {
                return ((Person)row).CanTellJokes;
            };

            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 4;

            this.olvFastList.SetObjects(list);
        }

        private void InitializeDragDropExample(List<Person> list) {
            //this.olvGeeks.AutoArrange = true;
            //this.olvFroods.AutoArrange = true;

            this.olvGeeks.DragSource = new SimpleDragSource();
            this.olvFroods.DragSource = new SimpleDragSource();

            this.olvGeeks.DropSink = new RearrangingDropSink(true);
            this.olvFroods.DropSink = new RearrangingDropSink(true);

            this.olvGeeks.GetColumn(0).ImageGetter = delegate(object x) { return "user"; };
            this.olvFroods.GetColumn(0).ImageGetter = delegate(object x) { return "user"; };

            this.olvGeeks.GetColumn(2).Renderer = new MultiImageRenderer(Resource1.star16, 5, 0, 40);
            this.olvFroods.GetColumn(2).Renderer = new MultiImageRenderer(Resource1.star16, 5, 0, 40);

            this.comboBox12.SelectedIndex = 4;
            this.comboBox13.SelectedIndex = 4;

            this.olvGeeks.SetObjects(list);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ArrayList l = new ArrayList();
            while (l.Count < 1000)
                foreach (Person x in this.masterList)
                    l.Add(new Person(x));

            Stopwatch stopWatch = new Stopwatch();
            try {
                this.Cursor = Cursors.WaitCursor;
                stopWatch.Start();
                this.olvFastList.AddObjects(l);
            } finally {
                stopWatch.Stop();
                this.Cursor = Cursors.Default;
            }

            this.takeNoticeOfSelectionEvent = false;
            this.toolStripStatusLabel1.Text =
                String.Format("Build time: {0} items in {1}ms, average per item: {2:F}ms",
                              this.olvFastList.Items.Count,
                              stopWatch.ElapsedMilliseconds,
                              (float)stopWatch.ElapsedMilliseconds / this.olvFastList.Items.Count);
        }
        private bool takeNoticeOfSelectionEvent = true;

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            ChangeOwnerDrawn(this.olvFastList, (CheckBox)sender);
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeEditable(this.olvFastList, (ComboBox)sender);
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeView(this.olvFastList, (ComboBox)sender);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.olvFastList.SetObjects(null);
            this.HandleSelectionChanged(this.olvFastList);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ColumnSelectionForm form = new ColumnSelectionForm();
            form.OpenOn(this.listViewFiles);
        }

        private void olvFastList_SelectionChanged(object sender, EventArgs e)
        {
            if (this.takeNoticeOfSelectionEvent)
                this.HandleSelectionChanged((ObjectListView)sender);

            this.takeNoticeOfSelectionEvent = true;
        }

        private void listViewVirtual_SelectionChanged(object sender, EventArgs e)
        {
            this.HandleSelectionChanged((ObjectListView)sender);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();
            list.Add(new Person("A New Person"));
            list.Add(new Person("Brave New Person"));
            list.Add(new Person("someone like e e cummings"));
            list.Add(new Person("Luis Nova Pessoa"));

            // Give him a birthday that will display an image to make sure the image appears.
            list[list.Count - 1].BirthDate = new DateTime(1984, 12, 25);

            this.listViewComplex.AddObjects(list);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.listViewComplex.RemoveObjects(this.listViewComplex.SelectedObjects);
        }

		void Button18Click(object sender, EventArgs e)
		{
            this.olvFastList.RemoveObjects(this.olvFastList.SelectedObjects);
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) {
                if (this.listViewSimple.ShowGroups) {
                    this.listViewSimple.AlwaysGroupByColumn = this.listViewSimple.LastSortColumn;
                    this.listViewSimple.AlwaysGroupBySortOrder = this.listViewSimple.LastSortOrder;
                }
            } else {
                this.listViewSimple.AlwaysGroupByColumn = null;
                this.listViewSimple.AlwaysGroupBySortOrder = SortOrder.None;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.olvFastList.CopyObjectsToClipboard(this.olvFastList.CheckedObjects);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.olvFastList.EditSubItem(this.olvFastList.GetItem(5), 1);
        }

        private void treeListView_ItemActivate(object sender, EventArgs e)
        {
            Object model = this.treeListView.SelectedObject;
            if (model != null)
                this.treeListView.ToggleExpansion(model);
        }

        byte[] treeListViewState;

        private void button25_Click(object sender, EventArgs e)
        {
            this.treeListViewState = this.treeListView.SaveState();
            this.button26.Enabled = true;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            this.treeListView.RestoreState(this.treeListViewState);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            ColumnSelectionForm form = new ColumnSelectionForm();
            form.OpenOn(this.treeListView);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            this.treeListView.RefreshObjects(this.treeListView.SelectedObjects);
        }

        private void listViewComplex_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) 
                return;

            ContextMenuStrip ms = new ContextMenuStrip();
            ms.ItemClicked += new ToolStripItemClickedEventHandler(ms_ItemClicked);

            ObjectListView olv = (ObjectListView)sender;
            if (olv.ShowGroups) {
                foreach (ListViewGroup lvg in olv.Groups) {
                    ToolStripMenuItem mi = new ToolStripMenuItem(String.Format("Jump to group '{0}'", lvg.Header));
                    mi.Tag = lvg;
                    ms.Items.Add(mi);
                }
            } else {
                ToolStripMenuItem mi = new ToolStripMenuItem("Turn on 'Show Groups' to see this context menu in action");
                mi.Enabled = false;
                ms.Items.Add(mi);
            }

            ms.Show((Control)sender, e.X, e.Y);
        }

        void ms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)e.ClickedItem;
            ListViewGroup lvg = (ListViewGroup)mi.Tag;
            ObjectListView olv = (ObjectListView)lvg.ListView;
            olv.EnsureGroupVisible(lvg);
        }
        /*
        private static void BlendBitmaps(Graphics g, Bitmap b1, Bitmap b2, float transition)
        {
            float[][] colorMatrixElements = { 
   new float[] {1,  0,  0,  0, 0},        // red scaling factor of 2
   new float[] {0,  1,  0,  0, 0},        // green scaling factor of 1
   new float[] {0,  0,  1,  0, 0},        // blue scaling factor of 1
   new float[] {0,  0,  0,  transition, 0},        // alpha scaling factor of 1
   new float[] {0,  0,  0,  0, 1}};    // three translations of 0.2

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);

            g.DrawImage(
               b1,
               new Rectangle(0, 0, b1.Size.Width, b1.Size.Height),  // destination rectangle 
               0, 0,        // upper-left corner of source rectangle 
               b1.Size.Width,       // width of source rectangle
               b1.Size.Height,      // height of source rectangle
               GraphicsUnit.Pixel,
               imageAttributes);

            colorMatrix.Matrix33 = 1.0f - transition;
            imageAttributes.SetColorMatrix(colorMatrix);

            g.DrawImage(
               b2,
               new Rectangle(0, 0, b2.Size.Width, b2.Size.Height),  // destination rectangle 
               0, 0,        // upper-left corner of source rectangle 
               b2.Size.Width,       // width of source rectangle
               b2.Size.Height,      // height of source rectangle
               GraphicsUnit.Pixel,
               imageAttributes);
        }
        */
        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            this.listViewSimple.UseHotItem = ((CheckBox)sender).Checked;
        }

        private void treeListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("tree checked");
        }

        private void treeListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("tree check");
        }

        private void listViewSimple_ItemChecked(object sender, ItemCheckedEventArgs e) {
            System.Diagnostics.Debug.WriteLine("simple checked");
        }

        private void olvFastList_ItemChecked(object sender, ItemCheckedEventArgs e) {
            System.Diagnostics.Debug.WriteLine("fast checked");
        }

        private void listViewSimple_ItemCheck(object sender, ItemCheckEventArgs e) {
            System.Diagnostics.Debug.WriteLine("simple check");
        }

        private void olvFastList_ItemCheck(object sender, ItemCheckEventArgs e) {
            System.Diagnostics.Debug.WriteLine("fast check");
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e) {
            ShowGroupsChecked(this.olvGeeks, (CheckBox)sender);
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e) {
            this.ChangeView(this.olvGeeks, (ComboBox)sender);
        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e) {
            this.ChangeView(this.olvFroods, (ComboBox)sender);
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e) {
            this.ChangeOwnerDrawn(this.olvGeeks, (CheckBox)sender);
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e) {
            this.ChangeOwnerDrawn(this.olvFroods, (CheckBox)sender);
        }

        private void comboBoxNagLevel_SelectedIndexChanged(object sender, EventArgs e) {
            this.listViewVirtual.RemoveOverlay(this.nagOverlay);

            this.nagOverlay = new TextOverlay();
            switch (comboBoxNagLevel.SelectedIndex) {
                case 0:
                    this.nagOverlay.Alignment = ContentAlignment.BottomRight;
                    this.nagOverlay.Text = "Trial version";
                    this.nagOverlay.BorderWidth = 2.0f;
                    this.nagOverlay.BorderColor = Color.DarkGray;
                    this.nagOverlay.TextColor = Color.Black;
                    this.listViewVirtual.OverlayTransparency = 128;
                    break;
                case 1:
                    this.nagOverlay.Alignment = ContentAlignment.TopRight;
                    this.nagOverlay.Text = "TRIAL VERSION EXPIRED";
                    this.nagOverlay.TextColor = Color.Red;
                    this.nagOverlay.BorderWidth = 2.0f;
                    this.nagOverlay.BorderColor = Color.DarkGray;
                    this.nagOverlay.Rotation = 20;
                    this.nagOverlay.InsetX = 5;
                    this.nagOverlay.InsetY = 50;
                    this.listViewVirtual.OverlayTransparency = 255;
                    break;
                case 2:
                    this.nagOverlay.Alignment = ContentAlignment.MiddleCenter;
                    this.nagOverlay.Text = "TRIAL EXPIRED! BUY NOW!";
                    this.nagOverlay.TextColor = Color.Red;
                    this.nagOverlay.BorderWidth = 4.0f;
                    this.nagOverlay.BorderColor = Color.Red;
                    this.nagOverlay.Rotation = -30;
                    this.nagOverlay.Font = new Font("Stencil", 36);
                    this.listViewVirtual.OverlayTransparency = 192;
                    break;
            }
            this.listViewVirtual.AddOverlay(this.nagOverlay);
        }
        private TextOverlay nagOverlay;
    }
        
    enum MaritalStatus
    {
        Single,
        Married,
        Divorced,
        Partnered
    }

	class Person
	{
        public bool IsActive = true;

		public Person(string name)
		{
			this.name = name;
		}

		public Person(string name, string occupation, int culinaryRating, DateTime birthDate, double hourlyRate, bool canTellJokes, string photo, string comments)
		{
			this.name = name;
			this.Occupation = occupation;
			this.culinaryRating = culinaryRating;
			this.birthDate = birthDate;
			this.hourlyRate = hourlyRate;
            this.CanTellJokes = canTellJokes;
            this.Comments = comments;
            this.Photo = photo;
		}

        public Person(Person other)
        {
            this.name = other.Name;
            this.Occupation = other.Occupation;
            this.culinaryRating = other.CulinaryRating;
            this.birthDate = other.BirthDate;
            this.hourlyRate = other.GetRate();
            this.CanTellJokes = other.CanTellJokes;
            this.Photo = other.Photo;
            this.Comments = other.Comments;
            this.MaritalStatus = other.MaritalStatus;
        }

        // Allows tests for properties.
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string name;

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
        private string occupation;

		public int CulinaryRating {
			get { return culinaryRating; }
            set { culinaryRating = value; }
        }
        private int culinaryRating;

		public DateTime BirthDate {
			get { return birthDate; }
            set { birthDate = value; }
        }
        private DateTime birthDate;

        public int YearOfBirth
        {
            get { return this.BirthDate.Year; }
            set { this.BirthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
        }

        // Allow tests for methods
        public double GetRate()
        {
            return hourlyRate;
        }
        private double hourlyRate;

        public void SetRate(double value)
        {
            hourlyRate = value;
        }

		// Allows tests for fields.
        public string Photo;
        public string Comments;
		public int serialNumber;
        public bool? CanTellJokes;

        // Allow tests for enums
        public MaritalStatus MaritalStatus = MaritalStatus.Single;
	}

}
