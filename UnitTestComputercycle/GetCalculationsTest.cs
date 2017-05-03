using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using ComputerCycleSoftware;

namespace UnitTestComputercycle
{

    [TestClass]
    public class GetCalculationsTest
    {
        [TestMethod]
        public void GetCalulationsTestMethod()
        {
            //arrange
            double expected_result = 5;
            double expected_result1 = 75;

            //add
            GetCalculations n = new GetCalculations();
            double actual_result = n.CalculateAverage(100, 20);
            double actual_result1 = n.CalculateDistance(5, 15);

            


            //assert

            Assert.AreEqual(expected_result, actual_result);
            Assert.AreEqual(expected_result1, actual_result1);
        }
    }
}
