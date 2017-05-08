using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCycleSoftware
{
    public class GetCalculations
    {


        public double CalculateAverage(double sum, double count)//calculate the average
        {
            double sum1 = sum;
            double count1 = count;
            double res = sum1 / count1;
            return res;

        }

        public double CalculateDistance(double sessiontime, double avespeed)//calculate the speed
        {
            double time1 = sessiontime;
            double avespeed1 = avespeed;
            double distance = time1 * avespeed1;
            return distance;


        }



    }
}
