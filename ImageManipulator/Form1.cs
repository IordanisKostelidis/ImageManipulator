﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public int ImageBoxWidth { get; private set; }
        public int ImageBoxHeight { get; private set; }
        public int ImageMissingSize = 0;
        public int ImageWidth = 0;
        public int ImageHeight = 0;
        string FullPath { get; set; }


        public void SetPath(string path)
        {
            this.FullPath = path;
        }

        public string GetPath()
        {
            return this.FullPath;
        }

       // int[,] arrayOfNumberOfValuesOfHistogram; = new int[];



        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                    if (open.ShowDialog() == DialogResult.OK)
                    {

                        Bitmap bit = new Bitmap(open.FileName);
                        ImageBoxWidth = pictureBox1.Width;
                        ImageBoxHeight = pictureBox1.Height;
                        ImageWidth = bit.Width;
                        ImageHeight = bit.Height;                    
                        if (ImageBoxWidth >= ImageBoxHeight)
                        {
                            if (ImageWidth >= ImageHeight)
                            {
                                ImageMissingSize = (ImageWidth * ImageBoxHeight) / (ImageHeight);
                                pictureBox1.Image = ResizeImage(bit, ImageMissingSize, ImageBoxHeight);
                            }
                        }
                        if (ImageBoxWidth < ImageBoxHeight ) {
                            if (ImageWidth < ImageHeight)
                            {
                                ImageMissingSize = (ImageWidth * ImageBoxHeight) / (ImageWidth);
                                pictureBox1.Image = ResizeImage(bit, ImageWidth, ImageMissingSize);

                            }
                        }
                    }
                    FullPath = Path.GetFullPath(open.FileName);

                }
                catch (Exception)
                {
                    throw new ApplicationException("Failed loading image");
                }
            }
        }

        private void showHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistogramForm HistogramForm = new HistogramForm(FullPath,pictureBox1.Width,pictureBox1.Height);
            HistogramForm.Show();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
