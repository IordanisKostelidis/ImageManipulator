using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator
{
    public partial class HistogramForm : Form
    {
        public HistogramForm()
        {
            InitializeComponent();
        }

        public HistogramForm(string FullPath,int PictureWidth,int PictureHeight)
        {
            InitializeComponent();
            this.FullPath = FullPath;
            this.PictureWidth = PictureWidth;
            this.PictureHeight = PictureHeight;
        }

        string FullPath ;
        int PictureWidth;
        int PictureHeight;
        private void chart1_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }
        
        private void HistogramForm_Load(object sender, EventArgs e)
        {
            FullPath = Path.GetFullPath(FullPath);
            this.chart1.Series["ImageHistogram"].Points.AddXY("Test3", 33);

            this.chart1.Series["ImageHistogram"].Points.AddXY("Test4", 33);

            this.chart1.Series["ImageHistogram"].Points.AddXY("Test5", 33);

            this.chart1.Series["ImageHistogram"].Points.AddXY("Test6", 33);


            ImageLib ImageLibrary = new ImageLib();

            //UInt16[,,] image =ImageLib.imread(FullPath);
            int[,] arrayOfNumberOfValuesOfHistogram = new int[PictureWidth, PictureHeight];
            UInt16[,,] ColoredImage = ImageLib.imread(FullPath);
            UInt16[,] ImageGray = ImageLib.rgb2gray(ColoredImage);
            UInt16[] HistogramResults = ImageLib.imhistgray(ImageGray);
            for (int i = 0; i < HistogramResults.Length; ++i)
            {
                this.chart1.Series["ImageHistogram"].Points.AddXY(i.ToString(), HistogramResults[i]);
            }



        }
    }
}
