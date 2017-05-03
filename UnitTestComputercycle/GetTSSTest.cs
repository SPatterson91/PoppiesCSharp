using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComputerCycleSoftware;

namespace CycleSoftwareTests
{
    [TestClass]
    public class GetTSS
    {
        [TestMethod]
        public void GetTSSTestMethod()
        {

            //Arrange   
            PowerCalculation nn = new PowerCalculation();
            string expected_result = "68.71";

            //Add


            string actual_result = nn.CalculateTSS(144, 46, 170, 1.1).ToString("N2");

            //Assert

            Assert.AreEqual(expected_result, actual_result);
        }
    }
}
