using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCycleSoftware
{
    public class ConvertUnits
    {

        public string unitmess;

        public ConvertUnits(string msg)
        {
            unitmess = msg;
            //MessageBox.Show(unitmess);
        }



        public static double ConvertMilesToKilometers(double miles)
        {
            //
            // Multiply by this constant and return the result.
            //
            return miles * 1.609344;
        }

        public static double ConvertKilometersToMiles(double kilometers)
        {
            //
            // Multiply by this constant.
            //
            return kilometers * 0.621371192;
        }
    }
}

