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
    public class SmodeIndexTest
    {
        [TestMethod]
        public void IndexTest()
        {
            //arrange 

            int SMode;

            var lines = File.ReadAllLines("I:\\Leeds Met Uni Work\\Year 4\\Semester 2\\Advanced Software Engineering B\\ComputerCycleSoftware\\ASDBExampleCycleComputerData.HRM");
            foreach (var line in lines)
                if (line.StartsWith("SMode="))


                {
                    
                    SMode = int.Parse(line.Replace("SMode=", " "));


                   SModeIndex index = new SModeIndex();
                    string expected_result = "0";


                    //add


                    string ret = SMode.ToString();
                    string actual_result = index.indexing(ret);


                    //assert

                    Assert.AreEqual(expected_result, actual_result);

                }
        }
    }
}
