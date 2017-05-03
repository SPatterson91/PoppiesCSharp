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
    public class UnitConversionTest
    {
        
        [TestMethod]
        public void UnitConversionTestMethod()
        {

            

            double expected_result = 1.609344;


            //add

            double testmiles=1;
            //do not need new instance of object as method is static.
            double actual_result = ConvertUnits.ConvertMilesToKilometers(testmiles);


            //assert

            Assert.AreEqual(expected_result, actual_result);
     


            //add



            //assert
        }
    }
}
