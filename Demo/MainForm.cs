/*
 * ObjectListViewDemo - A simple demo to show the ObjectListView control
 *
 * User: Phillip Piper
 * Date: 15/10/2006 11:15 AM
 *
 * Change log:
 * 2009-07-04  JPP  Added ExampleVirtualDataSource for virtual list demo
 * [lots of stuff]
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
            // Use different font under Vista
            if (ObjectListView.IsVistaOrLater)
                this.Font = new Font("Segoe UI", 9);

			masterList = new List<Person>();
            masterList.Add(new Person("Wilhelm Frat", "Gymnast", 21, new DateTime(1984, 9, 23), 45.67, false, "ak", "Aggressive, belligerent "));
            masterList.Add(new Person("Alana Roderick", "Gymnast", 17, new DateTime(1974, 9, 23), 245.67, false, "gp", "Beautiful, exquisite"));
            masterList.Add(new Person("Frank Price", "Dancer", 30, new DateTime(1965, 11, 1), 75.5, false, "ns", "Competitive, spirited"));
            masterList.Add(new Person("Eric", "Half-a-bee", 1, new DateTime(1966, 10, 12), 12.25, true, "cp", "Diminutive, vertically challenged"));
            masterList.Add(new Person("Nicola Scotts", "Nurse", 42, new DateTime(1965, 10, 29), 1245.7, false, "np", "Wise, fun, lovely"));
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
            //while (list.Count < 5000) {
            //    foreach (Person p in masterList)
            //        list.Add(new Person(p));
            //}

			InitializeSimpleExample(list);
			InitializeComplexExample(list);
			InitializeDataSetExample();
			InitializeVirtualListExample();
            InitializeExplorerExample();
            InitializeTreeListExample();
            InitializeListPrinting();
            InitializeFastListExample(list);
            InitializeDragDropExample(list);
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

            // Uncomment this to see a fancy cell highlighting while editing
            //this.olvSimple.AddDecoration(new EditingCellBorderDecoration { UseLightbox = true });

			// Just one line of code make everything happen.
			this.olvSimple.SetObjects(list);
		}

		void InitializeComplexExample(List<Person> list)
		{
            this.olvComplex.AddDecoration(new EditingCellBorderDecoration { UseLightbox = true });

            // The following line makes getting aspect about 10x faster. Since getting the aspect is
            // the slowest part of building the ListView, it is worthwhile BUT NOT NECESSARY to do.
            TypedObjectListView<Person> tlist = new TypedObjectListView<Person>(this.olvComplex);
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
                new object[]{10, 20, 30, 40},
                new string[] {"Pay to eat out", "Suggest take-away", "Passable", "Seek dinner invitation", "Hire as chef"},
                new string[] { "not", "toast", "hamburger", "beef", "chef" },
                new string[] {
                    "Pay good money -- or flee the house -- rather than eat their homecooked food",
                    "Offer to buy takeaway rather than risk what may appear on your plate",
                    "Neither spectacular nor dangerous",
                    "Try to visit at dinner time to wrangle an invitation to dinner",
                    "Do whatever is necessary to procure their services" },
                new string[] { "Call 911", "Phone PizzaHut", "", "Open calendar", "Check bank balance" }
            );

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

            // Install a custom renderer that draws the Tile view in a special way
            this.olvComplex.ItemRenderer = new BusinessCardRenderer();

            // Drag and drop support
            // You can set up drag and drop explicitly (like this) or, in the IDE, you can set
            // IsSimpleDropSource and IsSimpleDragSource and respond to CanDrop and Dropped events

            this.olvComplex.DragSource = new SimpleDragSource();
            SimpleDropSink dropSink = new SimpleDropSink();
            this.olvComplex.DropSink = dropSink;
            dropSink.CanDropOnItem = true;
            //dropSink.CanDropOnSubItem = true;
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

            // Which hot item style to use?
            if (ObjectListView.IsVistaOrLater)
                this.comboBox15.Items.Add("Vista");
            this.comboBox15.SelectedIndex = 3;

            this.comboBox1.SelectedIndex = 4;
            this.comboBox5.SelectedIndex = 0;

            this.olvComplex.SetObjects(list);
        }

        /// <summary>
        /// Move the given item to the given index in the given group
        /// </summary>
        /// <remarks>The item and group must belong to the same ListView</remarks>
        public void MoveToGroup(ListViewItem lvi, ListViewGroup group, int indexInGroup) {
            group.ListView.BeginUpdate();
            lvi.Group = null;
            ListViewItem[] items = new ListViewItem[group.Items.Count + 1];
            group.Items.CopyTo(items, 0);
            Array.Copy(items, indexInGroup, items, indexInGroup + 1, group.Items.Count - indexInGroup);
            items[indexInGroup] = lvi;
            for (int i = 0; i < items.Length; i++)
                items[i].Group = null;
            for (int i = 0; i < items.Length; i++)
                group.Items.Add(items[i]);
            group.ListView.EndUpdate();
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

                // Use buffered graphics to kill flickers
                BufferedGraphics buffered = BufferedGraphicsManager.Current.Allocate(g, itemBounds);
                g = buffered.Graphics;
                g.Clear(olv.BackColor);
                g.SmoothingMode = ObjectListView.SmoothingMode;
                g.TextRenderingHint = ObjectListView.TextRenderingHint;

                if (e.Item.Selected) {
                    this.BorderPen = Pens.Blue;
                    this.HeaderBackBrush = new SolidBrush(olv.HighlightBackgroundColorOrDefault);
                } else {
                    this.BorderPen = new Pen(Color.FromArgb(0x33, 0x33, 0x33));
                    this.HeaderBackBrush = new SolidBrush(Color.FromArgb(0x33, 0x33, 0x33));
                }
                DrawBusinessCard(g, itemBounds, rowObject, olv, (OLVListItem)e.Item);

                // Finally render the buffered graphics
                buffered.Render();
                buffered.Dispose();

                // Return true to say that we've handled the drawing
                return true;
            }

            internal Pen BorderPen = new Pen(Color.FromArgb(0x33, 0x33, 0x33));
            internal Brush TextBrush = new SolidBrush(Color.FromArgb(0x22, 0x22, 0x22));
            internal Brush HeaderTextBrush = Brushes.AliceBlue;
            internal Brush HeaderBackBrush = new SolidBrush(Color.FromArgb(0x33, 0x33, 0x33));
            internal Brush BackBrush = Brushes.LemonChiffon;

            public void DrawBusinessCard(Graphics g, Rectangle itemBounds, object rowObject, ObjectListView olv, OLVListItem item) {
                const int spacing = 8;

                // Allow a border around the card
                itemBounds.Inflate(-2, -2);

                // Draw card background
                const int rounding = 20;
                GraphicsPath path = this.GetRoundedRect(itemBounds, rounding);
                g.FillPath(this.BackBrush, path);
                g.DrawPath(this.BorderPen, path);

                g.Clip = new Region(itemBounds);

                // Draw the photo
                Rectangle photoRect = itemBounds;
                photoRect.Inflate(-spacing, -spacing);
                Person person = rowObject as Person;
                if (person != null) {
                    photoRect.Width = 80;
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

                StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
                fmt.Trimming = StringTrimming.EllipsisCharacter;
                fmt.Alignment = StringAlignment.Center;
                fmt.LineAlignment = StringAlignment.Near;
                String txt = item.Text;

                using (Font font = new Font("Tahoma", 11)) {
                    // Measure the height of the title
                    SizeF size = g.MeasureString(txt, font, (int)textBoxRect.Width, fmt);
                    // Draw the title
                    RectangleF r3 = textBoxRect;
                    r3.Height = size.Height;
                    path = this.GetRoundedRect(r3, 15);
                    g.FillPath(this.HeaderBackBrush, path);
                    g.DrawString(txt, font, this.HeaderTextBrush, textBoxRect, fmt);
                    textBoxRect.Y += size.Height + spacing;
                }

                // Draw the other bits of information
                using (Font font = new Font("Tahoma", 8)) {
                    SizeF size = g.MeasureString("Wj", font, itemBounds.Width, fmt);
                    textBoxRect.Height = size.Height;
                    fmt.Alignment = StringAlignment.Near;
                    for (int i = 0; i < olv.Columns.Count; i++) {
                        OLVColumn column = olv.GetColumn(i);
                        if (column.IsTileViewColumn) {
                            txt = column.GetStringValue(rowObject);
                            g.DrawString(txt, font, this.TextBrush, textBoxRect, fmt);
                            textBoxRect.Y += size.Height;
                        }
                    }
                }
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

        /// <summary>
        /// This simple class just shows how an overlay can be drawn when the hot item changes.
        /// </summary>
        internal class BusinessCardOverlay : AbstractOverlay
        {
            public BusinessCardOverlay() {
                businessCardRenderer.HeaderBackBrush = Brushes.DarkBlue;
                businessCardRenderer.BorderPen = new Pen(Color.DarkBlue, 2);
                this.Transparency = 255;
            }
            #region IOverlay Members

            public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
                if (olv.HotRowIndex < 0)
                    return;

                if (olv.View == View.Tile)
                    return;

                OLVListItem item = olv.GetItem(olv.HotRowIndex);
                if (item == null)
                    return;

                Size cardSize = new Size(250, 120);
                Rectangle cardBounds = new Rectangle(
                    r.Right - cardSize.Width - 8, r.Bottom - cardSize.Height - 8, cardSize.Width, cardSize.Height);
                businessCardRenderer.DrawBusinessCard(g, cardBounds, item.RowObject, olv, item);
            }

            #endregion

            private BusinessCardRenderer businessCardRenderer = new BusinessCardRenderer();
        }

		void InitializeDataSetExample ()
		{
            this.olvColumn1.ImageGetter  = delegate (object row) { return "user"; };

            this.salaryColumn.MakeGroupies(
                new UInt32[] { 20000, 100000 },
                new string[] { "Lowly worker", "Middle management", "Rarified elevation" });

            this.heightColumn.MakeGroupies(
                new Double[] { 1.50, 1.70, 1.85 },
                new string[] { "Shortie",  "Normal", "Tall", "Really tall" });

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

        /// <summary>
        /// This hacked data source makes a small list of model objects act like a much bigger list
        /// of model objects. This forces some comprises (like sorting doesn't sort the whole list,
        /// only the given list), but it is just to show what sort of thing needs to happen.
        /// </summary>
        class ExampleVirtualDataSource : AbstractVirtualListDataSource
        {
            public ExampleVirtualDataSource(VirtualObjectListView listView, List<Person> objectList) :
                base(listView) {
                this.Objects = objectList;
            }

            public override int GetObjectIndex(object model) {
                return this.Objects.IndexOf((Person)model);
            }

            public override object GetNthObject(int n) {
                Person p = (Person)this.Objects[n % this.Objects.Count];
                p.serialNumber = n;
                return p;
            }

            public override int GetObjectCount() {
                return 500 * 1000;
            }

            public override void Sort(OLVColumn column, SortOrder order) {
                this.Objects.Sort (new MasterListSorter(column, order));
            }

            public override int SearchText(string value, int first, int last, OLVColumn column) {
                return DefaultSearchText(value, first, last, column, this);
            }

            List<Person> Objects;
        }

		void InitializeVirtualListExample ()
		{
            this.olvVirtual.BooleanCheckStateGetter = delegate(object x) {
                return ((Person)x).IsActive;
            };
            this.olvVirtual.BooleanCheckStatePutter = delegate(object x, bool newValue) {
                ((Person)x).IsActive = newValue;
                return newValue;
            };
            this.olvVirtual.DataSource = new ExampleVirtualDataSource(this.olvVirtual, this.masterList);

            // Install a custom sorter, just to show how it could be done. We don't
            // have a backing store that can sort all 10 million items, so we have to be
            // content with sorting the master list and showing that sorted. It gives the idea.
            this.olvVirtual.CustomSorter = delegate(OLVColumn col, SortOrder order) {
                masterList.Sort(new MasterListSorter(col, order));
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
            this.olvVirtual.RowFormatter = delegate(OLVListItem lvi) {
                lvi.ToolTipText = String.Format("This is a long tool tip for '{0}' that does nothing except waste space.", lvi.Text);
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
            SysImageListHelper helper = new SysImageListHelper(this.olvFiles);
            this.olvColumnFileName.ImageGetter = delegate(object x) {
                return helper.GetImageIndex(((FileSystemInfo)x).FullName);
            };

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
            FlagRenderer attributesRenderer = new FlagRenderer();
            attributesRenderer.Add(FileAttributes.Archive, "archive");
            attributesRenderer.Add(FileAttributes.ReadOnly, "readonly");
            attributesRenderer.Add(FileAttributes.System, "system");
            attributesRenderer.Add(FileAttributes.Hidden, "hidden");
            attributesRenderer.Add(FileAttributes.Temporary, "temporary");
            this.olvColumnAttributes.Renderer = attributesRenderer;

            // Which hot item style to use?
            if (ObjectListView.IsVistaOrLater)
                this.comboBox14.Items.Add("Vista");
            this.comboBox14.SelectedIndex = 3;

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

                    // Test checking objects before they exist in the list

                    //ArrayList list = new ArrayList(dir.GetFileSystemInfos());
                    //ArrayList list2 = new ArrayList();
                    //foreach (FileSystemInfo fsi in list) {
                    //    if (fsi.Name.ToLowerInvariant().StartsWith("d"))
                    //        list2.Add(fsi);
                    //}
                    //this.treeListView.CheckedObjects = list2;
                    //return list;
                }
                catch (UnauthorizedAccessException ex) {
                    MessageBox.Show(this, ex.Message, "ObjectListViewDemo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return new ArrayList();
                }
            };

            //this.treeListView.CheckBoxes = false;

            // You can change the way the connection lines are drawn by changing the pen
            TreeListView.TreeRenderer renderer = (TreeListView.TreeRenderer)this.treeListView.TreeColumnRenderer;
            renderer.LinePen = new Pen(Color.Firebrick, 0.5f);
            renderer.LinePen.DashStyle = DashStyle.Dot;

            //-------------------------------------------------------------------
            // Eveything after this is the same as the Explorer example tab --
            // nothing specific to the TreeListView. It doesn't have the grouping
            // delegates, since TreeListViews can't show groups.

            // Draw the system icon next to the name
            SysImageListHelper helper = new SysImageListHelper(this.treeListView);
            this.treeColumnName.ImageGetter = delegate(object x) {
                return helper.GetImageIndex(((FileSystemInfo)x).FullName);
            };

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
            FlagRenderer attributesRenderer = new FlagRenderer();
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
				this.dataGridView1.DataSource = ds;
				this.dataGridView1.DataMember = "Person";
                // Install this data source
                this.olvData.DataSource = new BindingSource(ds, "Person");

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
                using (StreamReader reader = new StreamReader(fs)) {
                    ds.ReadXml(reader);
                }
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
				              olvData.Items.Count,
				              stopWatch.ElapsedMilliseconds,
				              stopWatch.ElapsedMilliseconds / olvData.Items.Count);
        }

        #region Form event handlers

        private void MainForm_Load(object sender, EventArgs e) {
            // Make the tooltips look somewhat different
            this.olvComplex.CellToolTip.BackColor = Color.Black;
            this.olvComplex.CellToolTip.ForeColor = Color.AntiqueWhite;
            this.olvComplex.HeaderToolTip.BackColor = Color.AntiqueWhite;
            this.olvComplex.HeaderToolTip.ForeColor = Color.Black;
            this.olvComplex.HeaderToolTip.IsBalloon = true;
        }

        #endregion

        #region Utilities

        void ShowGroupsChecked(ObjectListView olv, CheckBox cb)
		{
            if (cb.Checked && olv.View == View.List) {
                cb.Checked = false;
                MessageBox.Show("ListView's cannot show groups when in List view.", "Object List View Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else {
                olv.ShowGroups = cb.Checked;
                olv.BuildList();
            }
		}

		void ShowLabelsOnGroupsChecked(ObjectListView olv, CheckBox cb)
		{
			olv.ShowItemCountOnGroups = cb.Checked;
            if (olv.ShowGroups)
    			olv.BuildGroups();
        }

        void HandleSelectionChanged(ObjectListView listView)
        {
            string msg;
            Person p = (Person)listView.GetSelectedObject();
            if (p == null)
                msg = listView.SelectedIndices.Count.ToString();
            else
                msg = String.Format("'{0}'", p.Name);
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
			ShowGroupsChecked(this.olvSimple, (CheckBox)sender);
		}

		void CheckBox4CheckedChanged(object sender, System.EventArgs e)
		{
			ShowLabelsOnGroupsChecked(this.olvSimple, (CheckBox)sender);
		}

		void Button1Click(object sender, System.EventArgs e)
		{
            this.TimedRebuildList(this.olvSimple);
		}

        void Button4Click(object sender, System.EventArgs e) {
            // Silly example just to make sure that object selection works

            olvSimple.SelectedObjects = olvComplex.SelectedObjects;
            olvSimple.Select();

            olvSimple.CopyObjectsToClipboard(olvSimple.CheckedObjects);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Person person = new Person("Some One Else " + System.Environment.TickCount);
            this.olvSimple.AddObject(person);
            this.olvSimple.EnsureModelVisible(person);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.olvSimple.RemoveObjects(this.olvSimple.SelectedObjects);
        }

        #endregion

        #region Complex Tab Event Handlers

        void CheckBox1CheckedChanged(object sender, System.EventArgs e)
		{
			ShowGroupsChecked(this.olvComplex, (CheckBox)sender);
		}

		void CheckBox2CheckedChanged(object sender, System.EventArgs e)
		{
            this.olvComplex.UseTranslucentSelection = ((CheckBox)sender).Checked;
            this.olvComplex.UseTranslucentHotItem = ((CheckBox)sender).Checked;

            // Make the hot item show an overlay when it changes
            if (this.olvComplex.UseTranslucentHotItem) {
                this.olvComplex.HotItemStyle.Overlay = new BusinessCardOverlay();
                this.olvComplex.HotItemStyle = this.olvComplex.HotItemStyle;
            }

            this.olvComplex.Invalidate();
        }

		void Button2Click(object sender, System.EventArgs e)
		{
            this.TimedRebuildList(this.olvComplex);
        }

		void Button5Click(object sender, System.EventArgs e)
		{
            this.olvComplex.CopySelectionToClipboard();
            olvComplex.SelectedObjects = olvSimple.SelectedObjects;
            olvComplex.Select();
            //this.olvComplex.ShowHeaderInAllViews = !this.olvComplex.ShowHeaderInAllViews;

		}

		void CheckBox6CheckedChanged(object sender, EventArgs e)
		{
            if (comboBox1.SelectedIndex == 3 && this.checkBox6.Checked)
                this.olvComplex.TileSize = new Size(250, 120);

            ChangeOwnerDrawn(this.olvComplex, (CheckBox)sender);
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 3 && this.olvComplex.OwnerDraw)
                this.olvComplex.TileSize = new Size(250, 120);

            this.ChangeView(this.olvComplex, (ComboBox)sender);
        }
        #endregion

        #region Dataset Tab Event Handlers

		void CheckBox7CheckedChanged(object sender, System.EventArgs e)
		{
			ShowGroupsChecked(this.olvData, (CheckBox)sender);
		}

		void CheckBox8CheckedChanged(object sender, System.EventArgs e)
		{
			ShowLabelsOnGroupsChecked(this.olvData, (CheckBox)sender);
		}

		void Button3Click(object sender, System.EventArgs e)
		{
			this.TimedReloadXml();
        }

        void CheckBox5CheckedChanged(object sender, EventArgs e)
        {
            ChangeOwnerDrawn(this.olvData, (CheckBox)sender);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeView(this.olvData, (ComboBox)sender);
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
            this.olvData.RowHeight = Decimal.ToInt32(this.rowHeightUpDown.Value);
        }

        private void checkBoxPause_CheckedChanged(object sender, EventArgs e)
        {
            this.olvData.PauseAnimations(((CheckBox)sender).Checked);
        }

        #endregion

        #region Virtual Tab Event Handlers

		void CheckBox9CheckedChanged(object sender, EventArgs e)
		{
            this.ChangeOwnerDrawn(this.olvVirtual, (CheckBox)sender);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeView(this.olvVirtual, (ComboBox)sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.olvVirtual.SelectAll();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.olvVirtual.DeselectAll();
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
			this.olvFiles.SetObjects(pathInfo.GetFileSystemInfos());
            sw.Stop();
            Cursor.Current = Cursors.Default;

            float msPerItem = (olvFiles.Items.Count == 0 ? 0 : (float)sw.ElapsedMilliseconds / olvFiles.Items.Count);
            this.toolStripStatusLabel1.Text = String.Format("Timed build: {0} items in {1}ms ({2:F}ms per item)",
                olvFiles.Items.Count, sw.ElapsedMilliseconds, msPerItem);
		}

		void CheckBox12CheckedChanged(object sender, EventArgs e)
		{
			ShowGroupsChecked(this.olvFiles, (CheckBox)sender);
		}

		void CheckBox11CheckedChanged(object sender, EventArgs e)
		{
            this.ShowLabelsOnGroupsChecked(this.olvFiles, (CheckBox)sender);
		}

		void CheckBox10CheckedChanged(object sender, EventArgs e)
		{
            this.ChangeOwnerDrawn(this.olvFiles, (CheckBox)sender);
		}

		void ComboBox4SelectedIndexChanged(object sender, EventArgs e)
		{
           this.ChangeView(this.olvFiles, (ComboBox)sender);
           this.button13.Enabled = (this.olvFiles.View == View.Details || this.olvFiles.View == View.Tile);
       }

        private void listViewFiles_ItemActivate(object sender, EventArgs e)
        {
            Object rowObject = this.olvFiles.SelectedObject;
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
            this.fileListViewState = this.olvFiles.SaveState();
            this.buttonRestoreState.Enabled = true;
        }

        private void buttonRestoreState_Click(object sender, EventArgs e)
        {
            this.olvFiles.RestoreState(this.fileListViewState);
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
				this.listViewPrinter1.ListView = this.olvSimple;
			else if (this.rbShowComplex.Checked == true)
				this.listViewPrinter1.ListView = this.olvComplex;
			else if (this.rbShowDataset.Checked == true)
				this.listViewPrinter1.ListView = this.olvData;
            else if (this.rbShowVirtual.Checked == true)
                this.listViewPrinter1.ListView = this.olvVirtual;
            else if (this.rbShowFileExplorer.Checked == true)
                this.listViewPrinter1.ListView = this.olvFiles;

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
            this.ChangeEditable(this.olvComplex, (ComboBox)sender);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeEditable(this.olvSimple, (ComboBox)sender);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeEditable(this.olvData, (ComboBox)sender);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeEditable(this.olvVirtual, (ComboBox)sender);
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
            this.olvFast.BooleanCheckStateGetter = delegate(object x) {
                return ((Person)x).IsActive;
            };
            this.olvFast.BooleanCheckStatePutter = delegate(object x, bool newValue) {
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
            this.olvColumn26.MakeGroupies(
                new object[] { 10, 20, 30, 40 },
                new string[] { "Pay to eat out", "Suggest take-away", "Passable", "Seek dinner invitation", "Hire as chef" },
                new string[] { "not", "toast", "hamburger", "beef", "chef" },
                new string[] { "Pay good money -- or flee the house -- rather than eat their homecooked food",
                                "Offer to buy takeaway rather than risk what may appear on your plate",
                                "Neither spectacular nor dangerous",
                                "Try to visit at dinner time to wrangle an invitation to dinner",
                                "Do whatever is necessary to procure their services" },
                new string[] { "Call 911", "Phone PizzaHut", "", "Open calendar", "Check bank balance" }
            );

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
            comboBox16.SelectedIndex = 2;

            this.olvFast.SetObjects(list);
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
                this.olvFast.AddObjects(l);
            } finally {
                stopWatch.Stop();
                this.Cursor = Cursors.Default;
            }

            this.takeNoticeOfSelectionEvent = false;
            this.toolStripStatusLabel1.Text =
                String.Format("Build time: {0} items in {1}ms, average per item: {2:F}ms",
                              this.olvFast.Items.Count,
                              stopWatch.ElapsedMilliseconds,
                              (float)stopWatch.ElapsedMilliseconds / this.olvFast.Items.Count);
        }
        private bool takeNoticeOfSelectionEvent = true;

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            ChangeOwnerDrawn(this.olvFast, (CheckBox)sender);
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeEditable(this.olvFast, (ComboBox)sender);
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeView(this.olvFast, (ComboBox)sender);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.olvFast.ClearObjects();
            this.HandleSelectionChanged(this.olvFast);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ColumnSelectionForm form = new ColumnSelectionForm();
            form.OpenOn(this.olvFiles);
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

            this.olvComplex.AddObjects(list);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.olvComplex.RemoveObjects(this.olvComplex.SelectedObjects);
        }

		void Button18Click(object sender, EventArgs e)
		{
            this.olvFast.RemoveObjects(this.olvFast.SelectedObjects);
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) {
                if (this.olvSimple.ShowGroups) {
                    this.olvSimple.AlwaysGroupByColumn = this.olvSimple.LastSortColumn;
                    this.olvSimple.AlwaysGroupBySortOrder = this.olvSimple.LastSortOrder;
                }
            } else {
                this.olvSimple.AlwaysGroupByColumn = null;
                this.olvSimple.AlwaysGroupBySortOrder = SortOrder.None;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.olvFast.CopyObjectsToClipboard(this.olvFast.CheckedObjects);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.olvFast.EditSubItem(this.olvFast.GetItem(5), 1);
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
            //if (e.Button != MouseButtons.Right)
            //    return;

            //ContextMenuStrip ms = new ContextMenuStrip();
            //ms.ItemClicked += new ToolStripItemClickedEventHandler(ms_ItemClicked);

            //ObjectListView olv = (ObjectListView)sender;
            //if (olv.ShowGroups) {
            //    foreach (ListViewGroup lvg in olv.Groups) {
            //        ToolStripMenuItem mi = new ToolStripMenuItem(String.Format("Jump to group '{0}'", lvg.Header));
            //        mi.Tag = lvg;
            //        ms.Items.Add(mi);
            //    }
            //} else {
            //    ToolStripMenuItem mi = new ToolStripMenuItem("Turn on 'Show Groups' to see this context menu in action");
            //    mi.Enabled = false;
            //    ms.Items.Add(mi);
            //}

            //ms.Show((Control)sender, e.X, e.Y);
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
            this.olvSimple.UseHotItem = ((CheckBox)sender).Checked;
        }

        private void treeListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("tree checked");
        }

        private void treeListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("tree check");
        }

        private void listViewSimple_ItemChecked(object sender, ItemCheckedEventArgs e) {
            //System.Diagnostics.Debug.WriteLine("simple checked");
        }

        private void olvFastList_ItemChecked(object sender, ItemCheckedEventArgs e) {
            //System.Diagnostics.Debug.WriteLine("fast checked");
        }

        private void listViewSimple_ItemCheck(object sender, ItemCheckEventArgs e) {
            //System.Diagnostics.Debug.WriteLine("simple check");
        }

        private void olvFastList_ItemCheck(object sender, ItemCheckEventArgs e) {
            //System.Diagnostics.Debug.WriteLine("fast check");
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
            this.olvVirtual.RemoveOverlay(this.nagOverlay);

            this.nagOverlay = new TextOverlay();
            switch (comboBoxNagLevel.SelectedIndex) {
                case 0:
                    this.nagOverlay.Alignment = ContentAlignment.BottomRight;
                    this.nagOverlay.Text = "Trial version";
                    this.nagOverlay.BackColor = Color.White;
                    this.nagOverlay.BorderWidth = 2.0f;
                    this.nagOverlay.BorderColor = Color.RoyalBlue;
                    this.nagOverlay.TextColor = Color.DarkBlue;
                    this.olvVirtual.OverlayTransparency = 255;
                    break;
                case 1:
                    this.nagOverlay.Alignment = ContentAlignment.TopRight;
                    this.nagOverlay.Text = "TRIAL VERSION EXPIRED";
                    this.nagOverlay.TextColor = Color.Red;
                    this.nagOverlay.BackColor = Color.White;
                    this.nagOverlay.BorderWidth = 2.0f;
                    this.nagOverlay.BorderColor = Color.DarkGray;
                    this.nagOverlay.Rotation = 20;
                    this.nagOverlay.InsetX = 5;
                    this.nagOverlay.InsetY = 50;
                    this.olvVirtual.OverlayTransparency = 192;
                    break;
                case 2:
                    this.nagOverlay.Alignment = ContentAlignment.MiddleCenter;
                    this.nagOverlay.Text = "TRIAL EXPIRED! BUY NOW!";
                    this.nagOverlay.TextColor = Color.Red;
                    this.nagOverlay.BorderWidth = 4.0f;
                    this.nagOverlay.BorderColor = Color.Red;
                    this.nagOverlay.Rotation = -30;
                    this.nagOverlay.Font = new Font("Stencil", 36);
                    this.olvVirtual.OverlayTransparency = 192;
                    break;
            }
            this.olvVirtual.AddOverlay(this.nagOverlay);
        }
        private TextOverlay nagOverlay;

        private void button28_Click_1(object sender, EventArgs e) {
            // Test to make sure that RefreshObject() refreshes the object itself
            //DirectoryInfo di = this.treeListView.SelectedObject as DirectoryInfo;
            //if (di != null) {
            //    if ((di.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
            //        di.Attributes = di.Attributes & ~FileAttributes.Archive;
            //    else
            //        di.Attributes = di.Attributes | FileAttributes.Archive;
            //}
            this.treeListView.RefreshObjects(this.treeListView.SelectedObjects);
        }

        private void listViewComplex_CellToolTip(object sender, ToolTipShowingEventArgs e) {
            // Show a long tooltip over cells when the control key is down
            if (Control.ModifierKeys != Keys.Control)
                return;

            OLVColumn col = e.Column ?? e.ListView.GetColumn(0);
            string stringValue = col.GetStringValue(e.Model);
            if (stringValue.StartsWith("m", StringComparison.InvariantCultureIgnoreCase)) {
                e.IsBalloon = !ObjectListView.IsVistaOrLater; // balloons don't work reliably on vista
                e.ToolTipControl.SetMaxWidth(400);
                e.Title = "WARNING";
                e.StandardIcon = ToolTipControl.StandardIcons.InfoLarge;
                e.BackColor = Color.AliceBlue;
                e.ForeColor = Color.IndianRed;
                e.AutoPopDelay = 15000;
                e.Font = new Font("Tahoma", 12.0f);
                e.Text = "THIS VALUE BEGINS WITH A DANGEROUS LETTER!\r\n\r\n" +
                    "On no account should members of the public attempt to pronounce this word without " +
                    "the assistance of trained vocalization specialists.";
            } else {
                e.Text = String.Format("Tool tip for '{0}', column '{1}'\r\nValue shown: '{2}'",
                    ((Person)e.Model).Name, col.Text, stringValue);
            }
        }

        private void listViewComplex_HeaderToolTipShowing(object sender, ToolTipShowingEventArgs e) {
            if (Control.ModifierKeys != Keys.Control)
                return;

            e.Title = "Information";
            e.StandardIcon = ToolTipControl.StandardIcons.Info;
            e.AutoPopDelay = 10000;
            e.Text = String.Format("More details about the '{0}' column\r\n\r\nThis only shows when the control key is down.",
                e.Column.Text);
        }

        private void listViewFiles_CellToolTipShowing(object sender, ToolTipShowingEventArgs e) {
            if (!this.showToolTipsOnFiles)
                return;

            e.Text = String.Format("Tool tip for '{0}', column '{1}'\r\nValue shown: '{2}'",
                e.Model, e.Column.Text, e.SubItem.Text);
        }

        private void checkBox19_CheckedChanged_1(object sender, EventArgs e) {
            this.showToolTipsOnFiles = !this.showToolTipsOnFiles;
        }
        bool showToolTipsOnFiles = false;

        private void listViewFiles_CellClick(object sender, CellClickEventArgs e) {
            System.Diagnostics.Trace.WriteLine(String.Format("clicked ({0}, {1}). model {2}. click count: {3}",
                e.RowIndex, e.ColumnIndex, e.Model, e.ClickCount));
        }

        private void listViewFiles_CellRightClick(object sender, CellRightClickEventArgs e) {
            System.Diagnostics.Trace.WriteLine(String.Format("right clicked {0}, {1}). model {2}", e.RowIndex, e.ColumnIndex, e.Model));
            // Show a menu if the click was on first column
            if (e.ColumnIndex == 0)
                e.MenuStrip = this.contextMenuStrip2;
        }

        private void treeListView_ModelCanDrop(object sender, ModelDropEventArgs e) {
            e.Effect = DragDropEffects.None;
            if (e.TargetModel != null) {
                if (e.TargetModel is DirectoryInfo)
                    e.Effect = e.StandardDropActionFromKeys;
                else
                    e.InfoMessage = "Can only drop on directories";
            }
        }

        private void treeListView_ModelDropped(object sender, ModelDropEventArgs e) {
            String msg = String.Format("{2} items were dropped on '{1}' as a {0} operation.",
                e.Effect, ((DirectoryInfo)e.TargetModel).Name, e.SourceModels.Count);
            MessageBox.Show(msg, "OLV Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listViewSimple_CellClick(object sender, CellClickEventArgs e) {
            System.Diagnostics.Trace.WriteLine(String.Format("clicked ({0}, {1}). model {2}. click count: {3}",
                e.RowIndex, e.ColumnIndex, e.Model, e.ClickCount));
        }

        private void listViewComplex_CellRightClick(object sender, CellRightClickEventArgs e) {
            ContextMenuStrip ms = new ContextMenuStrip();
            ms.ItemClicked += new ToolStripItemClickedEventHandler(ms_ItemClicked);

            ObjectListView olv = e.ListView;
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

            e.MenuStrip = ms;
        }

        void ms_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            ToolStripMenuItem mi = (ToolStripMenuItem)e.ClickedItem;
            ListViewGroup lvg = (ListViewGroup)mi.Tag;
            ObjectListView olv = (ObjectListView)lvg.ListView;
            olv.EnsureGroupVisible(lvg);
        }

        private void listViewSimple_CellOver(object sender, CellOverEventArgs e) {
            //System.Diagnostics.Trace.WriteLine(String.Format("over ({0}, {1}). model {2}",
            //    e.RowIndex, e.ColumnIndex, e.Model));
        }

        private void listViewComplex_CellOver(object sender, CellOverEventArgs e) {
            //System.Diagnostics.Trace.WriteLine(String.Format("over ({0}, {1}). model {2}",
            //    e.RowIndex, e.ColumnIndex, e.Model));
        }

        private void listViewComplex_HotItemChanged(object sender, HotItemChangedEventArgs e) {
            //System.Diagnostics.Trace.WriteLine(String.Format("** HOT ITEM ({0}, {1}, {2})",
            //    e.HotRowIndex, e.HotColumnIndex, e.HotCellHitLocation));
        }

        private void listViewDataSet_FormatCell(object sender, FormatCellEventArgs e) {
            string[] colorNames = new string[] { "red", "green", "blue", "yellow", "black", "white" };
            string text = e.SubItem.Text.ToLowerInvariant();
            foreach (string name in colorNames) {
                if (text.Contains(name)) {
                    if (text.Contains("bk-" + name))
                        e.SubItem.BackColor = Color.FromName(name);
                    else
                        e.SubItem.ForeColor = Color.FromName(name);
                }
            }
            FontStyle style = FontStyle.Regular;
            if (text.Contains("bold"))
                style |= FontStyle.Bold;
            if (text.Contains("italic"))
                style |= FontStyle.Italic;
            if (text.Contains("underline"))
                style |= FontStyle.Underline;
            if (text.Contains("strikeout"))
                style |= FontStyle.Strikeout;

            if (style != FontStyle.Regular) {
                e.SubItem.Font = new Font(e.SubItem.Font, style);
            }
        }

        private void listViewComplex_FormatRow(object sender, FormatRowEventArgs e) {
            e.UseCellFormatEvents = true;
            if (olvComplex.View != View.Details) {
                if (e.Item.Text.ToLowerInvariant().StartsWith("nicola")) {
                    e.Item.Decoration = new ImageDecoration(Resource1.loveheart, 64);
                } else
                    e.Item.Decoration = null;
            }
        }

        private void listViewComplex_FormatCell(object sender, FormatCellEventArgs e) {
            Person p = (Person)e.Model;

            // Put a love heart next to Nicola's name :)
            if (e.ColumnIndex == 0) {
                if (e.SubItem.Text.ToLowerInvariant().StartsWith("nicola")) {
                    e.SubItem.Decoration = new ImageDecoration(Resource1.loveheart, 64);
                } else
                    e.SubItem.Decoration = null;
            }

            // If the occupation is missing a value, put a composite decoration over it
            // to draw attention to.
            if (e.ColumnIndex == 1 && e.SubItem.Text == "") {
                TextDecoration decoration = new TextDecoration("Missing!", 255);
                decoration.Alignment = ContentAlignment.MiddleCenter;
                decoration.Font = new Font(this.Font.Name, this.Font.SizeInPoints + 2);
                decoration.TextColor = Color.Firebrick;
                decoration.Rotation = -20;
                e.SubItem.Decoration = decoration;
                CellBorderDecoration cbd = new CellBorderDecoration();
                cbd.BorderPen = new Pen(Color.FromArgb(128, Color.Firebrick));
                cbd.FillBrush = null;
                cbd.CornerRounding = 4.0f;
                e.SubItem.Decorations.Add(cbd);
            }
            //if (e.ColumnIndex == 7) {
            //    if (p.CanTellJokes.HasValue && p.CanTellJokes.Value)
            //        e.SubItem.Decoration = new CellBorderDecoration();
            //    else
            //        e.SubItem.Decoration = null;
            //}
        }

        private void listViewSimple_IsHyperlink(object sender, IsHyperlinkEventArgs e) {
            if (e.Text.ToLowerInvariant().StartsWith("m"))
                e.Url = null;
            else
                e.Url = "http://objectlistview.sourceforge.net";
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e) {
            if (ObjectListView.IsVistaOrLater) {
                this.olvFast.ShowGroups = !this.olvFast.ShowGroups;
                this.olvFast.BuildList();
            } else {
                MessageBox.Show("Sorry. Groups on virtual lists only works on Vista and later",
                    "OLV Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listViewComplex_GroupTaskClicked(object sender, GroupTaskClickedEventArgs e) {
            MessageBox.Show(String.Format("group task click: {0}", e.Group), "OLV Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void olvFastList_GroupTaskClicked(object sender, GroupTaskClickedEventArgs e) {
            MessageBox.Show(String.Format("group task click: {0}", e.Group), "OLV Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e) {
            this.ChangeHotItemStyle(this.olvFiles, (ComboBox)sender);
        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e) {
            this.ChangeHotItemStyle(this.olvComplex, (ComboBox)sender);


            // Make the hot item show an overlay when it changes
            if (this.olvComplex.UseTranslucentHotItem) {
                this.olvComplex.HotItemStyle.Overlay = new BusinessCardOverlay();
                this.olvComplex.HotItemStyle = this.olvComplex.HotItemStyle;
            }

            this.olvComplex.UseTranslucentSelection = this.olvComplex.UseTranslucentHotItem;

            this.olvComplex.Invalidate();
        }

        private void ChangeHotItemStyle(ObjectListView olv, ComboBox cb) {

            olv.UseTranslucentHotItem = false;
            olv.UseHotItem = true;
            olv.FullRowSelect = olv == this.olvComplex; // olvComplex should be full row select
            olv.UseExplorerTheme = false;

            switch (cb.SelectedIndex) {
                case 0:
                    olv.UseHotItem = false;
                    break;
                case 1:
                    HotItemStyle hotItemStyle = new HotItemStyle();
                    hotItemStyle.ForeColor = Color.AliceBlue;
                    hotItemStyle.BackColor = Color.FromArgb(255, 64, 64, 64);
                    olv.HotItemStyle = hotItemStyle;
                    break;
                case 2:
                    RowBorderDecoration rbd = new RowBorderDecoration();
                    rbd.BorderPen = new Pen(Color.SeaGreen, 2);
                    rbd.FillBrush = null;
                    rbd.CornerRounding = 4.0f;
                    HotItemStyle hotItemStyle2 = new HotItemStyle();
                    hotItemStyle2.Decoration = rbd;
                    olv.HotItemStyle = hotItemStyle2;
                    break;
                case 3:
                    olv.UseTranslucentHotItem = true;
                    break;
                case 4:
                    HotItemStyle hotItemStyle3 = new HotItemStyle();
                    hotItemStyle3.Decoration = new LightBoxDecoration();
                    olv.HotItemStyle = hotItemStyle3;
                    break;
                case 5:
                    olv.FullRowSelect = true;
                    olv.UseHotItem = false;
                    olv.UseExplorerTheme = true;

                    // Using Explorer theme doesn't work in owner drawn mode
                    if (olv == this.olvFiles)
                        this.checkBox10.Checked = false;
                    if (olv == this.olvComplex)
                        this.checkBox6.Checked = false;
                    break;
            }
            olv.Invalidate();
        }

        private void olvFastList_IsHyperlink(object sender, IsHyperlinkEventArgs e) {
            if (e.Text.ToLowerInvariant().StartsWith("s")) {
                e.Url = null; // no hyperlink for cells starting with s
            } else {
                e.Url = "http://objectlistview.sourceforge.net";
            }
        }

        private void button29_Click(object sender, EventArgs e) {
            AnimatedDecoration listAnimation = new AnimatedDecoration(this.olvSimple);
            Animation animation = listAnimation.Animation;

            //Sprite image = new ImageSprite(Resource1.largestar);
            //image.FixedLocation = Locators.SpriteAligned(Corner.MiddleCenter);
            //image.Add(0, 2000, Effects.Rotate(0, 360 * 2f));
            //image.Add(1000, 1000, Effects.Fade(1.0f, 0.0f));
            //animation.Add(0, image);

            Sprite image = new ImageSprite(Resource1.largestar);
            image.Add(0, 500, Effects.Move(Corner.BottomCenter, Corner.MiddleCenter));
            image.Add(0, 500, Effects.Rotate(0, 180));
            image.Add(500, 1500, Effects.Rotate(180, 360 * 2.5f));
            image.Add(500, 1000, Effects.Scale(1.0f, 3.0f));
            image.Add(500, 1000, Effects.Goto(Corner.MiddleCenter));
            image.Add(1000, 900, Effects.Fade(1.0f, 0.0f));
            animation.Add(0, image);

            Sprite text = new TextSprite("Animations!", new Font("Tahoma", 32), Color.Blue, Color.AliceBlue, Color.Red, 3.0f);
            text.Opacity = 0.0f;
            text.FixedLocation = Locators.SpriteAligned(Corner.MiddleCenter);
            text.Add(900, 900, Effects.Fade(0.0f, 1.0f));
            text.Add(1000, 800, Effects.Rotate(180, 1440));
            text.Add(2000, 500, Effects.Scale(1.0f, 0.5f));
            text.Add(3500, 1000, Effects.Scale(0.5f, 3.0f));
            text.Add(3500, 1000, Effects.Fade(1.0f, 0.0f));
            animation.Add(0, text);

            animation.Start();
        }

        private void button30_Click(object sender, EventArgs e) {
            AnimatedDecoration animatedDecoration = new AnimatedDecoration(this.olvSimple,
                this.olvSimple.GetModelObject(5));
            Animation animation = animatedDecoration.Animation;

            // Animate the same star several times to make ghosting
            this.AddStarAnimation(animation, 250, 0.4f);
            this.AddStarAnimation(animation, 200, 0.5f);
            this.AddStarAnimation(animation, 150, 0.6f);
            this.AddStarAnimation(animation, 100, 0.7f);
            this.AddStarAnimation(animation, 50, 0.8f);
            this.AddStarAnimation(animation, 0, 1.0f);

            this.AddStarAnimation(animation, 2250, 0.4f);
            this.AddStarAnimation(animation, 2200, 0.5f);
            this.AddStarAnimation(animation, 2150, 0.6f);
            this.AddStarAnimation(animation, 2100, 0.7f);
            this.AddStarAnimation(animation, 2050, 0.8f);
            this.AddStarAnimation(animation, 2000, 1.0f);

            animation.Start();
        }

        private void AddStarAnimation(Animation animation, int start, float opacity) {
            Sprite sprite = new ImageSprite(Resource1.star32);
            sprite.Opacity = opacity;
            sprite.Add(0, 4000, Effects.Walk(Locators.AnimationBounds(), WalkDirection.Anticlockwise));
            sprite.Add(0, 4000, Effects.Rotate(0, 1480));
            animation.Add(start, sprite);
        }

        private void button31_Click(object sender, EventArgs e) {
            AnimatedDecoration animatedDecoration = new AnimatedDecoration(this.olvSimple,
                this.olvSimple.GetModelObject(5),
                this.olvSimple.GetColumn(1));
            Animation animation = animatedDecoration.Animation;

            ShapeSprite sprite = ShapeSprite.RoundedRectangle(5.0f, Color.Firebrick, Color.FromArgb(48, Color.Firebrick));
            sprite.Opacity = 0.0f;
            sprite.CornerRounding = 14;
            sprite.FixedBounds = Locators.AnimationBounds(3, 3);
            sprite.Add(0, 4500, Effects.Blink(3));
            animation.Add(0, sprite);

            animation.Start();
        }

        private void listViewSimple_MouseHover(object sender, EventArgs e) {
            OLVColumn column;
            Point pt = this.olvSimple.PointToClient(Cursor.Position);
            OLVListItem item = this.olvSimple.GetItemAt(pt.X, pt.Y, out column);
            System.Diagnostics.Debug.WriteLine(String.Format("Hover: {0}, {1}", item, column));
        }

        private void textBoxFilterSimple_TextChanged(object sender, EventArgs e) {
            this.TimedFilter(this.olvSimple, textBoxFilterSimple.Text);
        }

        private void textBoxFilterComplex_TextChanged(object sender, EventArgs e) {
            this.TimedFilter(this.olvComplex, textBoxFilterComplex.Text); 
        }

        private void textBoxFilterFast_TextChanged(object sender, EventArgs e) {
            TextMatchFilter.MatchKind matchKind = TextMatchFilter.MatchKind.Text;
            switch (comboBox16.SelectedIndex) {
                case 0: matchKind = TextMatchFilter.MatchKind.Text; break;
                case 1: matchKind = TextMatchFilter.MatchKind.StringStart; break;
                case 2: matchKind = TextMatchFilter.MatchKind.Regex; break;
            }
            this.TimedFilter(this.olvFast, textBoxFilterFast.Text, matchKind);
        }

        private void textBoxFilterData_TextChanged(object sender, EventArgs e) {
            this.TimedFilter(this.olvData, textBoxFilterData.Text);
        }

        private void textBoxTreeFilter_TextChanged(object sender, EventArgs e) {
            this.TimedFilter(this.treeListView, textBoxFilterTree.Text);
        }

        void TimedFilter(ObjectListView olv, string txt) {
            this.TimedFilter(olv, txt, TextMatchFilter.MatchKind.Text);
        }

        void TimedFilter(ObjectListView olv, string txt, TextMatchFilter.MatchKind matchKind) {
            TextMatchFilter filter = null;
            if (!String.IsNullOrEmpty(txt))
                filter = new TextMatchFilter(olv, txt, matchKind);

            // Setup a default renderer to draw the filter matches
            if (filter == null)
                olv.DefaultRenderer = null;
            else {
                olv.DefaultRenderer = new HighlightTextRenderer(filter);

                // Uncomment this line to see how the GDI+ rendering looks
                //olv.DefaultRenderer = new HighlightTextRenderer { Filter = filter, UseGdiTextRendering = false };
            }

            // Some lists have renderers already installed
            HighlightTextRenderer highlightingRenderer = olv.GetColumn(0).Renderer as HighlightTextRenderer;
            if (highlightingRenderer != null)
                highlightingRenderer.Filter = filter;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            olv.ModelFilter = filter;
            stopWatch.Stop();

            IList objects = olv.Objects as IList;
            if (objects == null)
                this.toolStripStatusLabel1.Text =
                    String.Format("Filtered in {0}ms", stopWatch.ElapsedMilliseconds);
            else
                this.toolStripStatusLabel1.Text =
                    String.Format("Filtered {0} items down to {1} items in {2}ms",
                                  objects.Count,
                                  olv.Items.Count,
                                  stopWatch.ElapsedMilliseconds);
        }

        private void listViewSimple_Scroll(object sender, ScrollEventArgs e) {

        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e) {
            // Fake a TextChanged event so the filter updates
            this.textBoxFilterFast_TextChanged(null, null);
        }

        private void olvSimple_ModelCanDrop(object sender, ModelDropEventArgs e) {
            e.Effect = DragDropEffects.None;

            // Only allow dropping if we are grouped on the cooking column
            if (!olvSimple.ShowGroups || olvSimple.PrimarySortColumn != olvSimpleCookingColumn) 
                return;

            if (e.DropTargetLocation == DropTargetLocation.Item ||
                e.DropTargetLocation == DropTargetLocation.AboveItem ||
                e.DropTargetLocation == DropTargetLocation.BelowItem) {
                e.Effect = DragDropEffects.Move;
            } 
        }

        private void olvSimple_ModelDropped(object sender, ModelDropEventArgs e) {
            // What item was dropped on?
            Person target = e.TargetModel as Person;
            if (target == null)
                return;

            foreach (Person source in e.SourceModels) {
                source.CulinaryRating = target.CulinaryRating;
            }

            olvSimple.BuildList(true);
        }

        private void olvComplex_BeforeCreatingGroups(object sender, CreateGroupsEventArgs e) {
            if (e.Parameters.PrimarySort == olvMarriedColumn && Control.ModifierKeys == Keys.Control) {
                e.Parameters.GroupComparer = new StrangeGroupComparer(e.Parameters.PrimarySortOrder);
                e.Parameters.ItemComparer = new StrangeItemComparer(this.olvComplex.GetColumn(0), e.Parameters.PrimarySortOrder);
            }
        }
    }

    /// <summary>
    /// This comparer sort groups alphabetically by their header, BUT ignoring the first letter
    /// </summary>
    public class StrangeGroupComparer : IComparer<OLVGroup>
    {
        public StrangeGroupComparer(SortOrder order) {
            this.sortOrder = order;
        }

        public int Compare(OLVGroup x, OLVGroup y) {

            string xValue = x.Header;
            string yValue = y.Header;

            if (xValue.Length >= 2)
                xValue = xValue.Substring(1);
            if (yValue.Length >= 2)
                yValue = yValue.Substring(1);

            int result = String.Compare(xValue, yValue, StringComparison.CurrentCultureIgnoreCase);

            if (this.sortOrder == SortOrder.Descending)
                result = 0 - result;

            return result;
        }

        private SortOrder sortOrder;
    }

    /// <summary>
    /// StrangeItemComparer is an example of how to customize the ordering of items
    /// within groups. It orders items by their text representation but ignoring
    /// the first letter. This admittedly a pointless way to order items, but it
    /// is simply an example.
    /// </summary>
    public class StrangeItemComparer : IComparer<OLVListItem>
    {
        public StrangeItemComparer(OLVColumn col, SortOrder order) {
            this.column = col;
            this.sortOrder = order;
        }

        public int Compare(OLVListItem x, OLVListItem y) {

            string xValue = this.column.GetStringValue(x.RowObject);
            string yValue = this.column.GetStringValue(y.RowObject);

            if (xValue.Length >= 2)
                xValue = xValue.Substring(1);
            if (yValue.Length >= 2)
                yValue = yValue.Substring(1);

            int result = String.Compare(xValue, yValue, StringComparison.CurrentCultureIgnoreCase);

            if (this.sortOrder == SortOrder.Descending)
                result = 0 - result;

            return result;
        }

        private OLVColumn column;
        private SortOrder sortOrder;
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
