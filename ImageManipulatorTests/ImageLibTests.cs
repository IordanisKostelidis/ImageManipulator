using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageManipulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulator.Tests
{
    [TestClass()]
    public class ImageLibTests
    {
        [TestMethod()]
        public void imreadTest()
        {
            UInt16[,,] test = ImageLib.imread(@"C:\VSTest\test.jpg");
        }

        [TestMethod()]
        public void rgb2grayTest()
        {
            UInt16[,,] test = ImageLib.imread(@"C:\VSTest\test.jpg");
            UInt16[,] test2 = ImageLib.rgb2gray(test);
        }

        [TestMethod()]
        public void imhistgrayTest()
        {
            UInt16[,,] test = ImageLib.imread(@"C:\VSTest\test.jpg");
            UInt16[,] test2 = ImageLib.rgb2gray(test);
            UInt16[] test3 = ImageLib.imhistgray(test2);
        }

        [TestMethod()]
        public void histeqgrayTest()
        {
            UInt16[,,] test = ImageLib.imread(@"C:\VSTest\test.jpg");
            UInt16[,] test2 = ImageLib.rgb2gray(test);
            UInt16[] test3 = ImageLib.imhistgray(test2);
            UInt16[] test4 = ImageLib.histeqgray(test2);
        }

    }
}