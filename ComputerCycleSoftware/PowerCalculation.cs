using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
namespace ComputerCycleSoftware
{
    public class PowerCalculation
    {


        public PowerCalculation()
        {
        }

        public PowerCalculation(double HRAve, double HRRest, double HRThresh, double Hours)
        {
            HRAve = 0;
            HRRest = 0;
            HRThresh = 0;
            Hours = 0;
        }


        //calculate TSS which is Training Stress Score. This is a measure of how much stress is put on your body during a ride.
        //It can be measued uding heart rates. this TSS is designated as TSShr.
        //The formula is as follows
        //TSShr = [(HRavg-HRrest)/(HRthr-HRrest)]^2 *100*hrs
        //ie 2hr 24 mins @ avg HR of 155bpm.
        //Threshhold HR rate of 172bpm
        // Resting Heart Rate of 52 bpm
        // 
        // TSShr = [(155-52)/(172-52)]^2 * 100 * 2.4
        //TSShr = 176.81

        public double CalculateTSS(double ave, double rest, double thresh, double hrs)
        {
            double HRAve = ave; //average heart rate
            double HRRest = rest; //Resting heart rate
            double HRThresh = thresh; //threshhold heart rate
            double Hours = hrs; //Time in Hours as a decimal

            double PreTSS = Math.Pow((HRAve - HRRest) / (HRThresh - HRRest), 2);

            double tss = PreTSS * 100 * Hours;

            return tss;

        }


        //Normalised Power is  it is an estimate of the power that you could have maintained for the same physiological "cost"
        //if your power output had been perfectly constant (e.g., as on a stationary cycle ergometer), rather than variable. 

        //to calulate it do this
        //take the rolling 30 second average of power in Watts. raise to the 4th power. 
        //Take the average and then take the 4th root of the value. 

        public double CalculateNP(double np)
        {

            // No formla coded as yet

            double normal = np;
            return normal;

        }


        //the intensity factor is simply the Normalized Power of a given ride divided by the athlete’s Functional Threshold. 
        // the Functional Threshold Power is the maximum power you can maintain through an hours effor without fatiguing

        public double CalculateIF(double normalised, double functional)
        {
            double norm = normalised;
            double func = functional;// rider has provided this and it is 320 watts

            double iff = norm / func;
            return iff;


        }



        //public double[] CalculateRollingAverage(double frameSize, double [] data)
        //{
        //    {
        //        double sum = 0;
        //       // double [] avgPoints = Convert.ToDouble[data.Length - frameSize + 1];
        //        for (int counter = 0; counter <= data.Length - frameSize; counter++)
        //        {
        //            int innerLoopCounter = 0;
        //            int index = counter;
        //            while (innerLoopCounter < frameSize)
        //            {
        //                sum = sum + data[index];

        //                innerLoopCounter += 1;

        //                index += 1;

        //            }

        //            avgPoints[counter] = sum / frameSize;

        //            sum = 0;

        //        }
        //        return avgPoints;

    }
        }
