using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Measure;
using VisionUnit;

namespace CDIVisionControl
{
    public partial class FormCalibration : Form
    {
        public event EventHandler CamUpdate;
        int row = 28, col = 45;
        double rowPitch = 1, colPitch = 1;
        bool[,] matrix;
        List<BlobPoint> blobs = new List<BlobPoint>();
        BlobPoint[,] blobmatrix;
        int BlobThres = 127;
        int BlobMinArea = 1000;
        int BlobMaxArea = 2000;
        bool BlobWhiteBlob = false;
        Rectangle BlobSearchRect;
        double PixelWidthX, PixelWidthY;

        private static FormCalibration _instance;
        public static FormCalibration Instance
        {
            get
            {
                if (_instance == null) _instance = new FormCalibration();
                return _instance;
            }
        }
        private VisionControl _owner;
        public new VisionControl Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                BlobSearchRect = new Rectangle(0, 0, _owner.imgWidth, _owner.imgHeight);
            }
        }
        public FormCalibration()
        {
            InitializeComponent();
            matrix = new bool[col, row];
        }

        private void BlobBar_Scroll(object sender, EventArgs e)
        {
            int pos;
            pos = this.BlobBar.Value;
            this.Thres.Text = pos.ToString();

            VSTest.Vision_Binary(pos);
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            row = int.Parse(textBoxRow.Text);
            col = int.Parse(textBoxCol.Text);
            rowPitch = double.Parse(textBoxRowPitch.Text);
            colPitch = double.Parse(textBoxColPitch.Text);
            BlobThres = this.BlobBar.Value;
            BlobMinArea = int.Parse(TextBoxMinArea.Text);
            BlobMaxArea = int.Parse(TextBoxMaxArea.Text);

            BlobWhiteBlob = CheckBoxWhiteBlob.Checked;

            VSTest.Vision_MarkClean();
            VSTest.Vision_ShowRect(BlobSearchRect, VSBase.COLOR_BLUE, 2);
        }
        double minx, miny, maxx, maxy, pitchx, pitchy;

        private void optionBox25_Click(object sender, EventArgs e)
        {
            VSTest.Control_Zoom(25);
        }

        private void optionBox50_Click(object sender, EventArgs e)
        {
            VSTest.Control_Zoom(50);
        }

        private void optionBox100_Click(object sender, EventArgs e)
        {
            VSTest.Control_Zoom(100);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            VSGlobalControl.m_CamParam.CameraKX = double.Parse(textBoxPixelWidthX.Text);
            VSGlobalControl.m_CamParam.CameraKY = double.Parse(textBoxPixelWidthY.Text);
            VSGlobalControl.ReadWriteCameraParam(false);
            if (CamUpdate != null) CamUpdate(null, null);
        }

        private void AddData(double x, double y)
        {
            blobs.Add(new BlobPoint(x, y));
            if (x < minx) minx = x;
            if (y < miny) miny = y;
            if (x > maxx) maxx = x;
            if (y > maxy) maxy = y;
        }
        private void GetPitch()
        {
            pitchx = (maxx - minx) / (col - 1);
            pitchy = (maxy - miny) / (row - 1);
            foreach (BlobPoint point in blobs)
            {
                matrix[(int)((point.x - minx) / pitchx + 0.5), (int)((point.y - miny) / pitchy + 0.5)] = true;
                blobmatrix[(int)((point.x - minx) / pitchx + 0.5), (int)((point.y - miny) / pitchy + 0.5)] = point;
            }
            pictureBox1.Refresh();
        }
        private void CalcPixelWidth()
        {
            BlobPoint start;
            double sum;
            int count;
            //Y
            sum = 0;
            count = 0;
            for (int i = 0; i < col; i++)
            {
                start = blobmatrix[i, 0];
                for (int j = 1; j < row; j++)
                {
                    if (start != null && blobmatrix[i, j] != null)
                    {
                        sum += blobmatrix[i, j].y - start.y;
                        count++;
                    }
                    start = blobmatrix[i, j];

                }
            }
            PixelWidthY = rowPitch / (sum / count);
            //X
            sum = 0;
            count = 0;
            for (int j = 0; j < row; j++)
            {
                start = blobmatrix[0, j];
                for (int i = 1; i < col; i++)
                {
                    if (start != null && blobmatrix[i, j] != null)
                    {
                        sum += blobmatrix[i, j].x - start.x;
                        count++;
                    }
                    start = blobmatrix[i, j];

                }
            }
            PixelWidthX = colPitch / (sum / count);
            textBoxPixelWidthX.Text = PixelWidthX.ToString();
            textBoxPixelWidthY.Text = PixelWidthY.ToString();
        }
        private void buttonGetBlob_Click(object sender, EventArgs e)
        {
            double cx = 0;
            double cy = 0;
            double Area = 0;

            int ret = 0;

            int pos;
            pos = this.BlobBar.Value;
            matrix = new bool[col, row];
            blobmatrix = new BlobPoint[col, row];
            minx = miny = 100000;
            maxx = maxy = -1;
            blobs.Clear();
            VSTest.Vision_MarkClean();
            ret = VSTest.Vision_FindBlobMultiplePoint(false, pos, BlobMinArea, BlobMaxArea, BlobSearchRect);

            for (int i = 0; i < ret; i++)
            {
                VSTest.Vision_GetBlobMultiplePointResult(i, ref cx, ref cy, ref Area);
                VSTest.Vision_DrawCross(Convert.ToInt32(cx), Convert.ToInt32(cy), 5, 250, 2);
                AddData(cx, cy);
            }
            GetPitch();
            CalcPixelWidth();
            MessageBox.Show("标定完成。");
        }

        private void FormCalibration_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen circle = new Pen(Color.Black);
            SolidBrush fill = new SolidBrush(Color.RoyalBlue);
            int rowpitch = pictureBox1.ClientSize.Height / (row + 1);
            int colpitch = pictureBox1.ClientSize.Width / (col + 1);
            int rowsize = rowpitch * 2 / 3;
            int colsize = colpitch * 2 / 3;
            if (rowsize < colsize)
                colsize = rowsize;
            else
                rowsize = colsize;
            for (int i = 1; i <= col; i++)
                for (int j = 1; j <= row; j++)
                {
                    if (matrix[i - 1, j - 1])
                        e.Graphics.FillEllipse(fill, colpitch * i, rowpitch * j, rowsize, colsize);
                    e.Graphics.DrawEllipse(circle, colpitch * i, rowpitch * j, rowsize, colsize);
                }
        }

        private void buttonShot_Click(object sender, EventArgs e)
        {
            VSGlobalControl.ShotOneCallBack(3, null);
        }
    }
    public class BlobPoint
    {
        public double x, y;
        public BlobPoint(double px, double py)
        {
            x = px; y = py;
        }
    }
}
