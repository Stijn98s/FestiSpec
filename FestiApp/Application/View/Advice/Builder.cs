using iTextSharp.text;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Windows.Input;
using Cursor = System.Windows.Forms.Cursor;
using Cursors = System.Windows.Forms.Cursors;
using Image = iTextSharp.text.Image;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace FestiApp.View.Advice
{
    public partial class Builder : Control
    {
        private const int DRAG_HANDLE_SIZE = 7;
        private int mouseX, mouseY;
        public Control SelectedControl { get; set; }
        private Direction direction;
        private Point newLocation;
        private Size newSize;
        private int totalpages = 0;

        public int CurrentPage { get; set; } = 0;

        public Builder()
        {
            InitializeComponent();
        }
        private enum Direction
        {
            NW,
            N,
            NE,
            W,
            E,
            SW,
            S,
            SE,
            None
        }

        public string Content { get; set; }

        public void GotoPage(int i)
        {
            CurrentPage = i;
            SelectedControl = null;

            foreach (Control control in pnControls.Controls)
            {
                control.Visible = (int)control.Tag == CurrentPage;
            }

            Refresh();
        }

        public void NextPage()
        {
            CurrentPage++;
            SelectedControl = null;

            if (CurrentPage > totalpages)
            {
                totalpages = CurrentPage;
            }

            foreach (Control control in pnControls.Controls)
            {
                control.Visible = (int)control.Tag == CurrentPage;
            }

            Refresh();
        }

        public void PrevPage()
        {
            CurrentPage--;
            SelectedControl = null;

            foreach (Control control in pnControls.Controls)
            {
                control.Visible = (int)control.Tag == CurrentPage;
            }

            Refresh();
        }

        private void control_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            Cursor = Cursors.SizeAll;
        }

        private void control_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Start();
        }

        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            Refresh();

            if (e.Button == MouseButtons.Left)
            {
                SelectedControl = (Control)sender;
                mouseX = -e.X;
                mouseY = -e.Y;
            }
        }

        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var control = (Control)sender;
                var nextPosition = new Point();
                nextPosition = pnControls.PointToClient(MousePosition);
                nextPosition.Offset(mouseX, mouseY);
                control.Location = nextPosition;
            }
        }

        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Refresh();
                var control = (Control)sender;
                Cursor.Clip = Rectangle.Empty;
                DrawControlBorder(control);
                OnContentChanged(new EventArgs());
            }
        }

        private void DrawControlBorder(object sender)
        {
            var control = (Control)sender;

            if (control == null) return;

            var border = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE / 2),
                new Size(control.Size.Width + DRAG_HANDLE_SIZE,
                    control.Size.Height + DRAG_HANDLE_SIZE));
            var NW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            var N = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            var NE = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            var W = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            var E = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            var SW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            var S = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            var SE = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));

            //get the form graphic
            var g = pnControls.CreateGraphics();
            //draw the border and drag handles
            ControlPaint.DrawBorder(g, border, Color.Gray, ButtonBorderStyle.Dotted);
            ControlPaint.DrawGrabHandle(g, NW, true, true);
            ControlPaint.DrawGrabHandle(g, N, true, true);
            ControlPaint.DrawGrabHandle(g, NE, true, true);
            ControlPaint.DrawGrabHandle(g, W, true, true);
            ControlPaint.DrawGrabHandle(g, E, true, true);
            ControlPaint.DrawGrabHandle(g, SW, true, true);
            ControlPaint.DrawGrabHandle(g, S, true, true);
            ControlPaint.DrawGrabHandle(g, SE, true, true);
            g.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                var pos = pnControls.PointToClient(MousePosition);
                //check if the mouse cursor is within the drag handle
                if (pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X && pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y)
                {
                    direction = Direction.NW;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                         pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                         pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                         pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE)
                {
                    direction = Direction.SE;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2 &&
                         pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                         pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                         pos.Y <= SelectedControl.Location.Y)
                {
                    direction = Direction.N;
                    Cursor = Cursors.SizeNS;
                }
                else if (pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2 &&
                         pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                         pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                         pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE)
                {
                    direction = Direction.S;
                    Cursor = Cursors.SizeNS;
                }
                else if (pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                         pos.X <= SelectedControl.Location.X &&
                         pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                         pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2)
                {
                    direction = Direction.W;
                    Cursor = Cursors.SizeWE;
                }
                else if (pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                         pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                         pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                         pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2)
                {
                    direction = Direction.E;
                    Cursor = Cursors.SizeWE;
                }
                else if (pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                         pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                         pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE && pos.Y <= SelectedControl.Location.Y)
                {
                    direction = Direction.NE;
                    Cursor = Cursors.SizeNESW;
                }
                else if (pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                         pos.X <= SelectedControl.Location.X + DRAG_HANDLE_SIZE &&
                         pos.Y >= SelectedControl.Location.Y + SelectedControl.Height - DRAG_HANDLE_SIZE &&
                         pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE)
                {
                    direction = Direction.SW;
                    Cursor = Cursors.SizeNESW;
                }
                else
                {
                    Cursor = Cursors.Default;
                    direction = Direction.None;
                }
            }
            else
            {
                direction = Direction.None;
                Cursor = Cursors.Default;
            }
        }

        private void pnControls_MouseUp(object sender, MouseEventArgs e)
        {
            SelectedControl = null;
            pnControls.Focus();
            Refresh();
            DrawControlBorder(SelectedControl);
        }

        private void pnControls_MouseDown(object sender, MouseEventArgs e)
        {
            pnControls.Focus();
            Refresh();
            DrawControlBorder(SelectedControl);
            timer1.Start();
        }

        private void pnControls_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedControl == null || e.Button != MouseButtons.Left) return;

            timer1.Stop();
            pnControls.Invalidate(false);

            var pos = pnControls.PointToClient(MousePosition);

            if (direction == Direction.NW)
            {
                newLocation = new Point(pos.X, pos.Y);
                newSize = new Size(SelectedControl.Size.Width - (newLocation.X - SelectedControl.Location.X),
                    SelectedControl.Size.Height - (newLocation.Y - SelectedControl.Location.Y));
                SelectedControl.Location = newLocation;
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
            else if (direction == Direction.SE)
            {
                newLocation = new Point(pos.X, pos.Y);
                newSize = new Size(
                    SelectedControl.Size.Width +
                    (newLocation.X - SelectedControl.Size.Width - SelectedControl.Location.X),
                    SelectedControl.Height + (newLocation.Y - SelectedControl.Height - SelectedControl.Location.Y));
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
            else if (direction == Direction.N)
            {
                newLocation = new Point(SelectedControl.Location.X, pos.Y);
                newSize = new Size(SelectedControl.Width,
                    SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                SelectedControl.Location = newLocation;
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
            else if (direction == Direction.S)
            {
                newLocation = new Point(pos.X, SelectedControl.Location.Y);
                newSize = new Size(SelectedControl.Width,
                    pos.Y - SelectedControl.Location.Y);
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
            else if (direction == Direction.W)
            {
                newLocation = new Point(pos.X, SelectedControl.Location.Y);
                newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                    SelectedControl.Height);
                SelectedControl.Location = newLocation;
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
            else if (direction == Direction.E)
            {
                newLocation = new Point(pos.X, pos.Y);
                newSize = new Size(pos.X - SelectedControl.Location.X,
                    SelectedControl.Height);
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
            else if (direction == Direction.SW)
            {
                newLocation = new Point(pos.X, SelectedControl.Location.Y);
                newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                    pos.Y - SelectedControl.Location.Y);
                SelectedControl.Location = newLocation;
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
            else if (direction == Direction.NE)
            {
                newLocation = new Point(SelectedControl.Location.X, pos.Y);
                newSize = new Size(pos.X - SelectedControl.Location.X,
                    SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                SelectedControl.Location = newLocation;
                SelectedControl.Size = newSize;
                DrawControlBorder(SelectedControl);
            }
        }

        public void AddLabel(string label, bool editable = false)
        {
            var ctrl = new EditableLabel
            {
                Location = new Point(30, 130),
                ClientSize = new Size(100, 30)
            };

            ctrl.CanEdit = editable;
            ctrl.Text = label;

            ctrl.MouseEnter += control_MouseEnter;
            ctrl.MouseLeave += control_MouseLeave;
            ctrl.MouseDown += control_MouseDown;
            ctrl.MouseMove += control_MouseMove;
            ctrl.MouseUp += control_MouseUp;
            ctrl.Tag = CurrentPage;
            ctrl.BringToFront();
            pnControls.Controls.Add(ctrl);
            OnContentChanged(new EventArgs());
        }

        public void AddPanel()
        {
            var ctrl = new Panel
            {
                Location = new Point(80, 130),
                BackColor = Color.Transparent,
                Tag = CurrentPage
            };

            ctrl.MouseEnter += control_MouseEnter;
            ctrl.MouseLeave += control_MouseLeave;
            ctrl.MouseDown += control_MouseDown;
            ctrl.MouseMove += control_MouseMove;
            ctrl.MouseUp += control_MouseUp;

            ctrl.BringToFront();
            pnControls.Controls.Add(ctrl);
            OnContentChanged(new EventArgs());
        }

        public void AddGrid(string header, List<string> rows)
        {
            var ctrl = new DataGridView
            {
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AllowUserToOrderColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                GridColor = Color.Black,
                Location = new Point(80, 130),
                ColumnCount = 1,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Tag = CurrentPage
            };

            if (rows[0].StartsWith("http") && (rows[0].EndsWith(".jpg") || rows[0].EndsWith("png")))
            {
                ctrl.Columns.Add(new DataGridViewImageColumn(false));
            }

            ctrl.Columns[0].Name = header;

            foreach (var row in rows)
            {
                if (row.StartsWith("http") && (row.EndsWith(".jpg") || row.EndsWith("png")))
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(row);
                    ctrl.Rows.Add(System.Drawing.Image.FromStream(stream));
                    stream.Flush();
                    stream.Close();
                    client.Dispose();
                }
                else
                {
                    ctrl.Rows.Add(row);
                }
            }


            ctrl.ClientSize = new Size(ctrl.ClientSize.Width, ctrl.Rows[0].Height * (ctrl.RowCount + 1));

            ctrl.MouseEnter += control_MouseEnter;
            ctrl.MouseLeave += control_MouseLeave;
            ctrl.MouseDown += control_MouseDown;
            ctrl.MouseMove += control_MouseMove;
            ctrl.MouseUp += control_MouseUp;

            ctrl.BringToFront();
            pnControls.Controls.Add(ctrl);
            ctrl.Invalidate();
            OnContentChanged(new EventArgs());
        }

        public void AddListView(string header, List<string> rows)
        {
            var il = new ImageList();

            DownloadImagesFromWeb(rows, il);

            il.ImageSize = new Size(75, 75);

            var ctrl = new ListView
            {
                Location = new Point(200, 170),
                BackColor = Color.LightGray,
                Tag = CurrentPage,
                TileSize = new Size(85, 85),
                LargeImageList = il,
                Margin = new Padding(0),
                BackgroundImageTiled = false,
                BorderStyle = BorderStyle.None,
                ShowGroups = true,
            };

            ctrl.Groups.Add(new ListViewGroup("0", header));

            ctrl.BackColor = Color.White;

            for (var i = 0; i < rows.Count; i++)
            {
                ctrl.Items.Add(new ListViewItem()
                {
                    ImageIndex = i,
                    Text = (i + 1).ToString(),
                    Group = ctrl.Groups[0]
                });
            }

            ctrl.MouseEnter += control_MouseEnter;
            ctrl.MouseLeave += control_MouseLeave;
            ctrl.MouseDown += control_MouseDown;
            ctrl.MouseMove += control_MouseMove;
            ctrl.MouseUp += control_MouseUp;

            ctrl.BringToFront();
            pnControls.Controls.Add(ctrl);
            OnContentChanged(new EventArgs());
        }

        private void DownloadImagesFromWeb(List<string> rows, ImageList il)
        {
            foreach (string img in rows)
            {
                WebRequest request = WebRequest.Create(img);
                WebResponse resp = request.GetResponse();
                Stream respStream = resp.GetResponseStream();
                Bitmap bmp = new Bitmap(respStream);
                respStream.Dispose();

                il.Images.Add(bmp);
            }
        }

        public void AddPicture(string url)
        {
            var ctrl = new PictureBox
            {
                Location = new Point(200, 170),
                Size = new Size(75, 75),
                BackColor = Color.LightGray,
                Tag = CurrentPage,
                ImageLocation = url,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            ctrl.MouseEnter += control_MouseEnter;
            ctrl.MouseLeave += control_MouseLeave;
            ctrl.MouseDown += control_MouseDown;
            ctrl.MouseMove += control_MouseMove;
            ctrl.MouseUp += control_MouseUp;

            ctrl.BringToFront();
            pnControls.Controls.Add(ctrl);
            OnContentChanged(new EventArgs());
        }

        public void AddLineChart(string header, IEnumerable<string> headers, IEnumerable<double> values)
        {

            IChartValues value = new ChartValues<double>();

            foreach (var i in values)
            {
                value.Add(i);
            }

            var ctrl = new LiveCharts.WinForms.CartesianChart
            {
                Location = new Point(96, 46),
                Size = new Size(200, 100),
                DataTooltip = null,
                DisableAnimations = true,
                Tag = CurrentPage,
                Series = new SeriesCollection
                {
                    new ColumnSeries()
                    {
                        Values = value
                    }
                },
                AxisX = new AxesCollection() {
                    new Axis
                    {
                        Separator = new Separator()
                        {
                            Step = 1,
                        },
                        Title = header,
                        Labels = headers.ToArray()
                    }

                }
            };

            pnControls.Controls.Add(ctrl);

            ctrl.BackColor = Color.Transparent;
            ctrl.MouseEnter += control_MouseEnter;
            ctrl.MouseLeave += control_MouseLeave;
            ctrl.MouseDown += control_MouseDown;
            ctrl.MouseMove += control_MouseMove;
            ctrl.MouseUp += control_MouseUp;

            ctrl.BringToFront();
            SelectedControl = ctrl;
            pnControls.Invalidate();
            OnContentChanged(new EventArgs());
        }

        public void AddBarChart(string header, IEnumerable<string> headers, IEnumerable<double> values)
        {

            IChartValues value = new ChartValues<double>();

            foreach (var i in values)
            {
                value.Add(i);
            }

            var ctrl = new LiveCharts.WinForms.CartesianChart
            {
                Location = new Point(96, 46),
                Size = new Size(200, 100),
                DataTooltip = null,
                DisableAnimations = true,
                Tag = CurrentPage,
                Series = new SeriesCollection
                {
                    new ColumnSeries()
                    {
                        Values = value
                    }
                },
                AxisX = new AxesCollection() {
                    new Axis
                    {
                        Separator = new Separator()
                        {
                            Step = 1,

                        },
                        Title = header,
                        Labels = headers.ToArray()
                    }
                }
            };

            pnControls.Controls.Add(ctrl);

            ctrl.BackColor = Color.Transparent;
            ctrl.MouseEnter += control_MouseEnter;
            ctrl.MouseLeave += control_MouseLeave;
            ctrl.MouseDown += control_MouseDown;
            ctrl.MouseMove += control_MouseMove;
            ctrl.MouseUp += control_MouseUp;

            ctrl.BringToFront();
            SelectedControl = ctrl;
            pnControls.Invalidate();
            OnContentChanged(new EventArgs());
        }

        public void Export()
        {
            if (pnControls.Controls.Count <= 0) return;

            var obj = new SaveFileDialog
            {
                Filter = @"PDF (*.pdf)|*.pdf"
            };

            if (obj.ShowDialog() != DialogResult.OK) return;

            if (obj.FileName != "")
            {
                var document = new Document();
                PdfWriter.GetInstance(document, new FileStream(obj.FileName, FileMode.Create));
                document.Open();
                GotoPage(0);

                for (var i = 0; i <= totalpages; i++)
                {
                    if (i != 0)
                    {
                        document.NewPage();
                        NextPage();
                    }

                    var bitmap = new Bitmap(pnControls.Width, pnControls.Height);
                    bitmap.SetResolution(300,300);
                    pnControls.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    var layerImage = Image.GetInstance(bitmap, ImageFormat.Bmp);
                    layerImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                    document.Add(layerImage);
                }


                document.Close();

                GotoPage(CurrentPage);
            }
        }

        private void control_MouseUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var control = (Control)sender;
                Cursor.Clip = Rectangle.Empty;
                control.Invalidate();
                DrawControlBorder(control);
                OnContentChanged(new EventArgs());
            }
        }

        private void control_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var control = (Control)sender;
                var nextPosition = new Point();
                nextPosition = pnControls.PointToClient(MousePosition);
                nextPosition.Offset(mouseX, mouseY);
                control.Location = nextPosition;
                Invalidate();
            }
        }

        private void control_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                pnControls.Invalidate();
                SelectedControl = (Control)sender;
                var control = (Control)sender;
                mouseX = (int)-e.GetPosition(e.Device.Target).X;
                mouseY = (int)-e.GetPosition(e.Device.Target).Y;
                control.Invalidate();
                DrawControlBorder(control);
            }
        }

        private EventHandler onContentChanged;

        public event EventHandler ContentChanged
        {
            add => onContentChanged = value;
            remove => onContentChanged -= value;
        }

        protected virtual void OnContentChanged(EventArgs e)
        {
            onContentChanged?.Invoke(this, e);
        }

        public void Delete()
        {
            if (SelectedControl != null)
            {
                pnControls.Controls.Remove(SelectedControl);
                SelectedControl = null;
                pnControls.Invalidate();
            }
        }
    }
}