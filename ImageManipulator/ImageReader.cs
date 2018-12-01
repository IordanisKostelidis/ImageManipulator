using System;
using System.Drawing;

namespace ImageManipulator
{
    public class ImageLib
    {
        //The function accepts the full path of the file and returns the 3D image (colored(RGB)) 
        public static UInt16[,,] imread(String file)
        {
            Bitmap bitmap = new Bitmap(file);
            UInt16[,,] image = new UInt16[bitmap.Width, bitmap.Height, 3];

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    image[i, j, 0] = bitmap.GetPixel(i, j).R;
                    image[i, j, 1] = bitmap.GetPixel(i, j).G;
                    image[i, j, 2] = bitmap.GetPixel(i, j).B;
                }
            }

            return image;

        }

        //The function accepts the colored image and returns the grayscale image
        public static UInt16[,] rgb2gray(UInt16[,,] rgb)
        {
            UInt16[,] gray = new UInt16[rgb.GetLength(0), rgb.GetLength(1)];
            for (int i = 0; i < rgb.GetLength(0); i++)
            {
                for (int j = 0; j < rgb.GetLength(1); j++)
                {
                    gray[i, j] = Convert.ToUInt16(0.2989 * rgb[i, j, 0] + 0.5870 * rgb[i, j, 1] + 0.1140 * rgb[i, j, 2]);
                }
            }

            return gray;
        }

        //The function accepts a grascale image and returns an array of the histogram
        public static UInt16[] imhistgray(UInt16[,] gray)
        {
            UInt16[] hist = new UInt16[256];
            for (int i = 0; i < 256; i++)
            {
                hist[i] = 0;
            }

            for (int i = 0; i < gray.GetLength(0); i++)
            {
                for (int j = 0; j < gray.GetLength(1); j++)
                {
                    hist[gray[i, j]]++;
                }
            }

            return hist;
        }

        //The function accepts the grayscale image, and and returns the array of the equalized histogram
        public static UInt16[] histeqgray(UInt16[,] gray)
        {
            UInt16[] hist = imhistgray(gray);
            float[] canonicalHist = new float[256];

            int nonZeros = 0;
            for (int i = 0; i < 255; i++)
            {
                if (hist[i] != 0)
                {
                    nonZeros++;
                }
            }

            for (int i = 0; i < 255; i++)
            {
                long tmp = hist[i] / nonZeros;

                canonicalHist[i] = (float)hist[i] / (float)(gray.GetLength(0) * gray.GetLength(1));
            }

            float prevNonZero = 0;
            float[] posibilities = new float[256];

            for (int i = 0; i < 255; i++)
            {
                if (canonicalHist[i] == 0)
                {
                    continue;
                }
                else
                {
                    posibilities[i] = (float)Math.Round(prevNonZero + canonicalHist[i],4);
                    prevNonZero = posibilities[i];
                }


            }

            float min = findMin(posibilities,true);
            UInt16[] eq = new UInt16[256];
            for (int i = 0; i < 255; i++)
            {

                if(posibilities[i] == 0)
                {
                    continue;
                }

                double tmp = ((posibilities[i]) - (min)) / (1 - min) * 255;

                eq[i] = Convert.ToUInt16(Math.Round(tmp));
            }

            return eq;
        }
        //The function finds the minimum of a given table, and a a boolean value, that ask to find the non-zero values
        //It returns the minimum number of the array
        private static float findMin(float[] table, Boolean nonZeroOnly)
        {
            float result = table[0];

            if(nonZeroOnly)
            {
                for(int i=0;i<table.Length;i++)
                {
                    if(table[i] < result && table[i] != 0)
                    {
                        result = table[i];
                    }
                }
            } else
            {
                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] < result)
                    {
                        result = table[i];
                    }
                }
            }

            return result;
        }
    }
}
