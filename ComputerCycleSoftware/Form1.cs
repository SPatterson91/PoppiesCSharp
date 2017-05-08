// <author>Samuel Patterson</author>
// <email>s.patterson6797@student.leedsbeckett.ac.uk</email>
// <date>07-05-2015 09:40:00 AM</date>
// <version> 11 </version>
// <summary>Class to read data files and put information such as heart rate, speed, cadence etc into a datagridview. </summary>


//using - It is the same as import in Java
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
using ZedGraph;


namespace ComputerCycleSoftware
{
    public partial class Form1 : Form
    {



        private string cadence;
        private string altitude;
        private string power;
        private string HeartRate;
        private string speed;
        private string HRM;
        TimeSpan startTime;
        TimeSpan Length;
        DateTime Date;
        int Interval;
        int p;
        int SMode;
        public double RestHR;
        double Lower3;
        public double dec_length;

      


        public Form1()
        {
            InitializeComponent(); //it is required as it loads the form of a component 

        }

        OpenFileDialog openFileDialog1 = new OpenFileDialog();



        private void Form1_Load(object sender, EventArgs e)
        {
            columnheaders();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            readFile();

        }


        //this code will ask the user if they would like to exit the application when the "yes" button is pressed. The no button will cancel it. ComputerCycle is the name of the messagebox.  
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Would you like to quit the Application?", "Computer Cycle Software", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }

        }

        private void button1_Click1(object sender, EventArgs e) //calculates the average/min and max of the columns in the DataGridView and displays them in textboxes
        {
            averagemethod();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit(); //exits the application
            Process.Start(Application.ExecutablePath, "/restart" + Process.GetCurrentProcess().Id); //It starts the application again by getting the current process ID    
        }



        public void averagemethod()
        {

            int sum = 0;
            int sum1 = 0;
            int sum2 = 0;
            int sum3 = 0;
            int sum4 = 0;
            int sum5 = 0;

            //converts the rows to integers for the cells 
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells["heartrate"].Value);
                sum1 += Convert.ToInt32(dataGridView1.Rows[i].Cells["speed"].Value);
                sum2 += Convert.ToInt32(dataGridView1.Rows[i].Cells["cadence"].Value);
                sum3 += Convert.ToInt32(dataGridView1.Rows[i].Cells["alt"].Value);
                sum4 += Convert.ToInt32(dataGridView1.Rows[i].Cells["power"].Value);
                sum5 += Convert.ToInt32(dataGridView1.Rows[i].Cells["pwrBal"].Value);
            }

            int count_row = dataGridView1.Rows.Count;



            GetCalculations calc = new GetCalculations(); //use Get Calculations to determine average

            double avg = calc.CalculateAverage(sum, count_row);
            double avg1 = calc.CalculateAverage(sum1, count_row);
            double avg2 = calc.CalculateAverage(sum2, count_row);
            double avg3 = calc.CalculateAverage(sum3, count_row);
            double avg4 = calc.CalculateAverage(sum4, count_row);
            double avg5 = calc.CalculateAverage(sum5, count_row);


            ///<summary>
            ///initiates variable for min heartrate and max heartrate, speed, cadence, alt, power and power balance
            ///and converts it into an integer
            ///</summary>
            var MinID = dataGridView1.Rows.Cast<DataGridViewRow>()
                      .Min(r => Convert.ToInt32(r.Cells["heartrate"].Value));

            var MaxID = dataGridView1.Rows.Cast<DataGridViewRow>()
                      .Max(r => Convert.ToInt32(r.Cells["heartrate"].Value));

            var MaxID_1 = dataGridView1.Rows.Cast<DataGridViewRow>()
                                    .Max(r => Convert.ToInt32(r.Cells["speed"].Value));

            var MaxID_2 = dataGridView1.Rows.Cast<DataGridViewRow>()
                                  .Max(r => Convert.ToInt32(r.Cells["cadence"].Value));

            var MaxID_3 = dataGridView1.Rows.Cast<DataGridViewRow>()
                                  .Max(r => Convert.ToInt32(r.Cells["alt"].Value));

            var MaxID_4 = dataGridView1.Rows.Cast<DataGridViewRow>()
                                  .Max(r => Convert.ToInt32(r.Cells["power"].Value));

            var MaxID_5 = dataGridView1.Rows.Cast<DataGridViewRow>()
                                  .Max(r => Convert.ToInt32(r.Cells["pwrBal"].Value));


            ///<summary>
            ///distance is seconds /3600 to convert to fraction of an hour. 
            ///then times by the average speed per hour. 
            ///then it divides by 10, as a temporary measure until speed can be displayed as a decimal
            ///</summary>

            var time = Length.ToString(); //initiate variable for length of session
            double seconds = TimeSpan.Parse(time).TotalSeconds;  //convert the time of session to seconds 
            double distance = (seconds / 3600 * avg1);

            //calculation to work out min HR 
            double minheartrate = 500;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string hr = dataGridView1.Rows[i].Cells["heartrate"].Value.ToString();
                minheartrate = Math.Min(minheartrate, int.Parse(hr));
            }

            //calculation to work out speed into decimal point 
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                double miles = double.Parse(dataGridView1.Rows[i].Cells["speed"].Value.ToString()); //stores value of speed column 
                miles = miles / 10; //calclation into mph 
                dataGridView1.Rows[i].Cells["speed"].Value = Math.Round(miles, 2); //dispays correct format of speed }
            }

            ///<summary>
            ///displays the columns of data that have been calculated into textboxes.
            ///N2 puts a decimal point into the desired number of decimal places which is 2. 
            ///The system.environment.NewLine puts the calculation of each variable onto a new line.
            ///</summary>

            heartcal.Text = "Min Heart Rate:" + minheartrate.ToString() + System.Environment.NewLine
                + System.Environment.NewLine + "Average Heart Rate:"
                + avg.ToString("N0") + System.Environment.NewLine + System.Environment.NewLine +
                "Max Heart Rate:" + MaxID.ToString();

            speedcal.Text = "Average Speed:" + avg1.ToString("N2") + " kph" + System.Environment.NewLine
                + System.Environment.NewLine + "Max Speed:" + MaxID_1.ToString("N2") +
                " kph" + System.Environment.NewLine + System.Environment.NewLine +
                "Total Distance covered:" + distance.ToString("N2");

            var lines = File.ReadAllLines(openFileDialog1.FileName);
            foreach (var line in lines)
                if (line.StartsWith("SMode="))
                {
                    SMode = int.Parse(line.Replace("SMode=", " "));

                    ///<summary> 
                    /// the eighth character in SMode determines the units.. "0" is kilometres. "1" is miles.
                    ///after finding out which character using SModeIndex. uses a swith statement to display the appropriate units. 
                    ///</summary>
                    string unit_output;
                    string ret = SMode.ToString();
                    SModeIndex n = new SModeIndex();
                    unit_output = n.indexing(ret);

                    switch (unit_output)
                    {
                        case "0":

                            speedcal.Text = "Average Speed:" + avg1.ToString("N2") + " kph" + System.Environment.NewLine
                            + System.Environment.NewLine + "Max Speed:"
                            + MaxID_1.ToString("N2") + " kph"
                            + System.Environment.NewLine + System.Environment.NewLine
                            + "Total Distance covered:" + distance.ToString("N2") + " Kilometers";



                            break;

                        case "1":

                            speedcal.Text = "Average Speed:" + avg1.ToString("N2") + " mph" + System.Environment.NewLine
                           + System.Environment.NewLine + "Max Speed:"
                           + MaxID_1.ToString("N2") + " mph"
                           + System.Environment.NewLine + System.Environment.NewLine
                           + "Total Distance covered:" + distance.ToString("N2") + " Miles";

                            break;
                    }

                    cadencecal.Text = "Average Cadence:" + avg2.ToString("N0") + System.Environment.NewLine
                        + System.Environment.NewLine + "Max Cadence:" + MaxID_2.ToString();

                    altcal.Text = "Average Altitude:" + avg3.ToString("N0")
                        + System.Environment.NewLine + System.Environment.NewLine + "Max Altitude:"
                        + MaxID_3.ToString();

                    powercal.Text = "Average Power:" + avg4.ToString("N0")
                        + System.Environment.NewLine + System.Environment.NewLine + "Max Power:" + MaxID_4.ToString("");

                    pwrbalcal.Text = "Average Power Balance:" + avg5.ToString("N0")
                        + System.Environment.NewLine + System.Environment.NewLine + "Max Power Balance:"
                        + MaxID_5.ToString();



                }


        }


        public void columnheaders()
        {
            ///<summary>
            ///it represents columns which displays data in a grid
            ///the HeaderText is the name of each column
            ///The heartrate.name is useful for the calculations as it picks up the heartrate or other names
            ///it will then add the data to each column
            ///</summary>


            DataGridViewColumn heartrate = new DataGridViewTextBoxColumn();
            heartrate.HeaderText = "Heart Rate";
            heartrate.Name = "heartrate";
            int ColumnIndex1 = dataGridView1.Columns.Add(heartrate);

            DataGridViewColumn speed = new DataGridViewTextBoxColumn();

            speed.HeaderText = "Speed";
            speed.Name = "speed";
            speed.DefaultCellStyle.Format = "N1";
            double ColumnIndex2 = dataGridView1.Columns.Add(speed) / 10;

            DataGridViewColumn cadence = new DataGridViewTextBoxColumn();
            cadence.HeaderText = "Cadence";
            cadence.Name = "cadence";
            int ColumnIndex3 = dataGridView1.Columns.Add(cadence);

            DataGridViewColumn alt = new DataGridViewTextBoxColumn();
            alt.HeaderText = "Altitude";
            alt.Name = "alt";
            int ColumnIndex4 = dataGridView1.Columns.Add(alt);

            DataGridViewColumn power = new DataGridViewTextBoxColumn();
            power.HeaderText = "Power";
            power.Name = "power";
            int ColumnIndex5 = dataGridView1.Columns.Add(power);

            DataGridViewColumn pwrBal = new DataGridViewTextBoxColumn();
            pwrBal.HeaderText = "Power Balance";
            pwrBal.Name = "pwrBal";
            int ColumnIndex6 = dataGridView1.Columns.Add(pwrBal);

            DataGridViewColumn time = new DataGridViewTextBoxColumn();
            time.HeaderText = "Time";
            time.Name = "time";
            int ColumnIndex7 = dataGridView1.Columns.Add(time);

        }

        public void readFile()
        {
            openFileDialog1.Filter = "HRM | *.hrm"; //only allows HRM files to be opened 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }



            using (StreamReader Reader = new StreamReader(openFileDialog1.FileName)) //reads any HRM file that has been chosen
            {

            }

            StreamReader Reader1 = new StreamReader(openFileDialog1.FileName); //allows the file that has been opened by the openFileDialog1 to be read
            string rowValue;
            string[] cellValue;


            while ((HRM = Reader1.ReadLine()) != null) //reads the line of characters from the current stream and returns the data as a string 
            {
                if (HRM.IndexOf("[HRData]") != -1) //reports the zero-based index of the first occurrence of the specified string in this instance so if it finds HRData, it will start reading the lines after it. 
                {
                    break;
                }

            }

            while
              ((Reader1.Peek() != -1)) //returns the next available character but doesn't consume it. if it is -1 it means there are no characters to be read
            {
                rowValue = Reader1.ReadLine(); //reads the row value
                cellValue = rowValue.Split('\t'); //splits the columns into tabs by '\t'
                dataGridView1.Rows.Add(cellValue); //adds the rows to the datagrid
                // startTime = startTime.Add(Interval);

            }

            Date = new DateTime();

            ///<summary>
            ///initiates a variable called lines that will open the text file and read all of the lines of a file then close it.
            ///if it finds a that a line starts with Date, it will replace the line "Date" and format it into a year, month and date
            ///the date is then displayed into a richTextBox with some information in quotes to say what it represents
            ///The following StartTime, Interval and Length are similar to Date except Start time and Length uses TimeSpan to represent a time interval while Inteveral uses an integer
            ///These are then displayed into a richTextBox with information in quotes
            ///In the IF statement for StartTime, foreach adds the start time in each row in the datagrid
            ///</summary>
            var lines = File.ReadAllLines(openFileDialog1.FileName);
            foreach (var line in lines)
            {
                if (line.StartsWith("Date="))
                {
                    Date = DateTime.ParseExact(line.Replace("Date=", ""), "yyyyMMdd", null);
                    richTextBox2.AppendText(Environment.NewLine + "Date of training session: " + Date);

                }

                {
                    {
                        if (line.StartsWith("StartTime="))
                        {

                            startTime = TimeSpan.Parse(line.Replace("StartTime=", ""));
                            richTextBox2.AppendText(Environment.NewLine);
                            richTextBox2.AppendText(Environment.NewLine + "Time started: " + startTime);

                            for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                            {
                                dataGridView1.Rows[row].Cells["time"].Value
                                  = startTime.Add(new TimeSpan(0, 0, row + Interval)); //adds 1 second on to each row 
                            }

                        }
                    }

                    {
                        if (line.StartsWith("Interval="))
                        {
                            Interval = int.Parse(line.Replace("Interval=", ""));
                            richTextBox2.AppendText(Environment.NewLine);
                            richTextBox2.AppendText(Environment.NewLine + "Interval: " + Interval + " second");
                        }

                        {
                            if (line.StartsWith("Length="))
                            {
                                Length = TimeSpan.Parse(line.Replace("Length=", ""));
                                richTextBox2.AppendText(Environment.NewLine);
                                richTextBox2.AppendText(Environment.NewLine + "Length of Workout: " + Length);

                            }
                        }

                        Reader1.Close(); //closes the reader so the user can't keep adding the file again 
                    }
                }
            }

        }

        private void ZedGraph_Click(object sender, EventArgs e)
        {

            //  Form2 f1 = new Form2();
            //f1.ShowDialog(); //shows the form as a modal dialog box - cannot go to the parent form


        }

        private void Metricbutton_click(object sender, EventArgs e)
        {
            //metric button
            {
                double sum1 = 0;
                var time = Length.ToString(); //initiate variable for length of session
                double seconds = TimeSpan.Parse(time).TotalSeconds;
                double secondslength = seconds / 3600;//convert the time of session to seconds 




                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum1 += Convert.ToInt32(dataGridView1.Rows[i].Cells["speed"].Value);
                }

                int count_row = dataGridView1.Rows.Count;

                double avespeed;
                double distance;

                GetCalculations calc = new GetCalculations();
                avespeed = calc.CalculateAverage(sum1, count_row);
                distance = calc.CalculateDistance(secondslength, avespeed);

                var MaxID_1 = dataGridView1.Rows.Cast<DataGridViewRow>()
                                            .Max(r => Convert.ToInt32(r.Cells["speed"].Value));

                var lines = File.ReadAllLines(openFileDialog1.FileName);
                foreach (var line in lines)
                    if (line.StartsWith("SMode="))
                    {
                        SMode = int.Parse(line.Replace("SMode=", " ")); //find out if the eight character in SMode from the input file is a "0"  or a "1"



                        string unit_output;
                        string ret = SMode.ToString();
                        SModeIndex n = new SModeIndex();
                        unit_output = n.indexing(ret);

                        switch (unit_output)
                        {
                            case "0": //if "0" then units are displayed in kilometres

                                speedcal.Text = "Average Speed:" + avespeed.ToString("N2") + " kph" + System.Environment.NewLine
                                + System.Environment.NewLine + "Max Speed:"
                                + MaxID_1.ToString("N2") + " kph"
                                + System.Environment.NewLine + System.Environment.NewLine
                                + "Total Distance covered:" + distance.ToString("N2") + " kilometres";



                                break;

                            case "1": //if "1" units are displayed in miles

                                double mphavg = ConvertUnits.ConvertKilometersToMiles(avespeed);
                                double maxmph = ConvertUnits.ConvertKilometersToMiles(MaxID_1);
                                double distmiles = ConvertUnits.ConvertKilometersToMiles(distance);


                                speedcal.Text = "Average Speed:" + mphavg.ToString("N2") + " mph" + System.Environment.NewLine
                                + System.Environment.NewLine + "Max Speed:"
                                + maxmph.ToString("N2") + " mph"
                                + System.Environment.NewLine + System.Environment.NewLine
                                + "Total Distance covered:" + distmiles.ToString("N2") + " Miles";

                                break;
                        }
                    }
            }

        }

        private void imperialbutton_Click(object sender, EventArgs e)

          //imperial button
        {
            double sum1 = 0;
            var time = Length.ToString(); //initiate variable for length of session
            double seconds = TimeSpan.Parse(time).TotalSeconds;
            double secondslength = seconds / 3600;//convert the time of session to seconds 




            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum1 += Convert.ToInt32(dataGridView1.Rows[i].Cells["speed"].Value);
            }

            int count_row = dataGridView1.Rows.Count;

            double avespeed;
            double distance;

            GetCalculations calc = new GetCalculations();
            avespeed = calc.CalculateAverage(sum1, count_row);
            distance = calc.CalculateDistance(secondslength, avespeed);

            var MaxID_1 = dataGridView1.Rows.Cast<DataGridViewRow>()
                                        .Max(r => Convert.ToInt32(r.Cells["speed"].Value));

            var lines = File.ReadAllLines(openFileDialog1.FileName);
            foreach (var line in lines)
                if (line.StartsWith("SMode="))
                {
                    SMode = int.Parse(line.Replace("SMode=", " "));



                    string unit_output;
                    string ret = SMode.ToString();
                    SModeIndex n = new SModeIndex();
                    unit_output = n.indexing(ret);

                    switch (unit_output)
                    {
                        case "0": //Convert to miles using ConvertUnits class

                            double mphavg = ConvertUnits.ConvertKilometersToMiles(avespeed);
                            double maxmph = ConvertUnits.ConvertKilometersToMiles(MaxID_1);
                            double distmiles = ConvertUnits.ConvertKilometersToMiles(distance);


                            speedcal.Text = "Average Speed:" + mphavg.ToString("N2") + " mph" + System.Environment.NewLine
                            + System.Environment.NewLine + "Max Speed:"
                            + maxmph.ToString("N2") + " mph"
                            + System.Environment.NewLine + System.Environment.NewLine
                            + "Total Distance covered:" + distmiles.ToString("N2") + " Miles";

                            break;

                        case "1": //convert to kilometres using ConvertUnits class

                            double kphave = ConvertUnits.ConvertMilesToKilometers(avespeed);
                            double maxkph = ConvertUnits.ConvertMilesToKilometers(MaxID_1);
                            double distkilos = ConvertUnits.ConvertMilesToKilometers(distance);


                            speedcal.Text = "Average Speed:" + kphave.ToString("N2") + " mph" + System.Environment.NewLine
                            + System.Environment.NewLine + "Max Speed:"
                            + maxkph.ToString("N2") + " mph"
                            + System.Environment.NewLine + System.Environment.NewLine
                            + "Total Distance covered:" + distkilos.ToString("N2") + " Miles";

                            break;
                    }

                    ConvertUnits unitsclass = new ConvertUnits("Distance and Speed units have been changed to Miles");
                }

        }

        MonthCalendar MCalendar = new MonthCalendar();


        // Build the Chart
        private void CreateGraph(ZedGraphControl zgc)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.GraphPane.GraphObjList.Clear();

            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = "Cycling Graph";
            myPane.XAxis.Title.Text = "X-Axis";
            myPane.YAxis.Title.Text = "Y-Axis";



            double x, y, y2, y3, y4, y5;

            x = 0;

            PointPairList lineSpeed = new PointPairList();
            PointPairList lineCad = new PointPairList();
            PointPairList lineAlt = new PointPairList();
            PointPairList linePower = new PointPairList();
            PointPairList lineHR = new PointPairList();

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {

                if (checkBox1.Checked)
                {
                    y2 = (Convert.ToDouble(dataGridView1.Rows[i].Cells["speed"].Value));
                    lineSpeed.Add(x, y2);
                }
                if (checkBox2.Checked)
                {
                    y3 = (Convert.ToDouble(dataGridView1.Rows[i].Cells["cadence"].Value));
                    lineCad.Add(x, y3);
                }
                if (checkBox3.Checked)
                {
                    y4 = (Convert.ToDouble(dataGridView1.Rows[i].Cells["alt"].Value));
                    lineAlt.Add(x, y4);
                }
                if (checkBox5.Checked)
                {
                    y5 = (Convert.ToDouble(dataGridView1.Rows[i].Cells["power"].Value));
                    linePower.Add(x, y5);
                }
                if (checkBox6.Checked)
                {
                    y = (Convert.ToDouble(dataGridView1.Rows[i].Cells["heartrate"].Value));
                    lineHR.Add(x, y);
                }
                x++;
            }


            if (checkBox1.Checked)
            {
                LineItem speed = myPane.AddCurve("Speed", lineSpeed, Color.Blue, SymbolType.None);

            }
            if (checkBox2.Checked)
            {
                LineItem cadence = myPane.AddCurve("Cadence", lineCad, Color.Black, SymbolType.None);
            }
            if (checkBox3.Checked)
            {
                LineItem alt = myPane.AddCurve("Altitude", lineAlt, Color.Green, SymbolType.None);
            }
            if (checkBox5.Checked)
            {
                LineItem power = myPane.AddCurve("Power", linePower, Color.Red, SymbolType.None);
            }
            if (checkBox6.Checked)
            {
                LineItem power = myPane.AddCurve("Heart rate", lineHR, Color.Cyan, SymbolType.None);
            }


            zgc.AxisChange();
            zgc.Invalidate();
            zgc.Refresh();

        }

        private void CreateGraph1(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the GraphPane title
            myPane.Title.Text = "This is the cycling graph";
            myPane.Title.FontSpec.IsItalic = true;
            myPane.Title.FontSpec.Size = 24f;
            myPane.Title.FontSpec.Family = "Times New Roman";

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.Goldenrod, 45.0f);
            // No fill for the chart background
            myPane.Chart.Fill.Type = FillType.None;

            // Set the legend to an arbitrary location
            myPane.Legend.Position = LegendPos.Float;
            myPane.Legend.Location = new Location(0.95f, 0.15f, CoordType.PaneFraction,
                           AlignH.Right, AlignV.Top);
            myPane.Legend.FontSpec.Size = 10f;
            myPane.Legend.IsHStack = false;

            // Add some pie slices
            PieItem segment1 = myPane.AddPieSlice(20, Color.Navy, Color.White, 45f, 0, "Heart Rate");
            PieItem segment3 = myPane.AddPieSlice(30, Color.Purple, Color.White, 45f, .0, "Speed");
            PieItem segment4 = myPane.AddPieSlice(10.21, Color.LimeGreen, Color.White, 45f, 0, "Cadence");
            PieItem segment2 = myPane.AddPieSlice(40, Color.SandyBrown, Color.White, 45f, 0.2, "Altitude");
            PieItem segment6 = myPane.AddPieSlice(250, Color.Red, Color.White, 45f, 0, "Power");


            segment2.LabelDetail.FontSpec.FontColor = Color.Red;

            // Sum up the pie values                                                               
            CurveList curves = myPane.CurveList;
            double total = 0;
            for (int x = 0; x < curves.Count; x++)
                total += ((PieItem)curves[x]).Value;


            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
           
            
        }


        private void button5_Click(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
            CreateGraph1(zedGraphControl2);

        }

        private void Power_Click(object sender, EventArgs e)
        {

            int sum = 0;


            //converts the rows to integers for the cells 
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells["heartrate"].Value);
            }



            int count_row = dataGridView1.Rows.Count;

            GetCalculations calc = new GetCalculations(); //use Get Calculations to determine average

            double avg = calc.CalculateAverage(sum, count_row);


            PowerTextBox.AppendText(Environment.NewLine);
            PowerTextBox.AppendText(Environment.NewLine + "Average heart rate: " + avg.ToString("N0"));




            var lines = File.ReadAllLines(openFileDialog1.FileName);
            foreach (var line in lines)
            {


                if (line.StartsWith("Lower3="))
                {

                    Lower3 = double.Parse(line.Replace("Lower3=", ""));
                    PowerTextBox.AppendText(Environment.NewLine);
                    PowerTextBox.AppendText(Environment.NewLine + "Threshhold Heartrate: " + Lower3);
                }

                if (line.StartsWith("Length="))
                {
                    Length = TimeSpan.Parse(line.Replace("Length=", ""));

                    var time = Length.ToString(); //initiate variable for length of session
                    double seconds = TimeSpan.Parse(time).TotalSeconds;
                    double secondslength = seconds;//convert the time of session to seconds
                    double dec_length = secondslength / 3600;// time of session as a decimal




                    PowerTextBox.AppendText(Environment.NewLine);
                    PowerTextBox.AppendText(Environment.NewLine + "Length of Workout: " + dec_length.ToString("N2"));



                }

                if (line.StartsWith("RestHR="))
                {

                    RestHR = double.Parse(line.Replace("RestHR=", ""));
                    PowerTextBox.AppendText(Environment.NewLine);
                    PowerTextBox.AppendText(Environment.NewLine + "Resting heart rate: " + RestHR);

                    var time = Length.ToString(); //initiate variable for length of session
                    double seconds = TimeSpan.Parse(time).TotalSeconds;
                    double secondslength = seconds;//convert the time of session to seconds
                    double dec_length = secondslength / 3600;// time of session as a decimal

                    double TSS;
                    PowerCalculation powercalc = new PowerCalculation();
                    TSS = powercalc.CalculateTSS(avg, RestHR, Lower3, dec_length);
                    //TTS = powercalc.CalculateTSS(144, 46, 170, 1.1); these are actual figures from the file
                    PowerTextBox.AppendText(Environment.NewLine);
                    PowerTextBox.AppendText(Environment.NewLine + "Training Stress Score (TSS):" + "" + TSS.ToString("N2"));
                }

            }


            double NP; //normalised power
            double nnp = 168; ; //in our case I used the average heart rate and not the rolling average heart rate
            PowerCalculation powercalc1 = new PowerCalculation();
            NP = powercalc1.CalculateNP(nnp);

            double IFF; //Intensity Function
            double func = 320; ;//function threshold as supplied by the client
            int sum4 = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                sum4 += Convert.ToInt32(dataGridView1.Rows[i].Cells["power"].Value);




            }



            GetCalculations calc1 = new GetCalculations(); //use Get Calculations to determine average


            double avg4 = calc1.CalculateAverage(sum4, count_row);


            PowerCalculation powercalc2 = new PowerCalculation();
            IFF = powercalc2.CalculateIF(avg4, func);



            PowerTextBox.AppendText(Environment.NewLine);
            PowerTextBox.AppendText(Environment.NewLine + "Normalised Power (NP):" + NP);

            PowerTextBox.AppendText(Environment.NewLine);
            PowerTextBox.AppendText(Environment.NewLine + "Intensity Factor (IF):" + IFF.ToString("N2"));


            MovingAverageCalculator calculator = new MovingAverageCalculator(30);



            // loop through the values 1 to 10
            //for (int i = 1; i <= 10; i++)
            //{
            //    // write out the average
            //    Console.WriteLine(calculator.NextValue(i));
            //}



            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{

            //    float[] sumpower = new float[Convert.ToInt32(dataGridView1.Rows[i].Cells["power"].Value)];
            //    //PowerTextBox.AppendText(Environment.NewLine + calculator.NextValue(i));
            //    PowerTextBox.AppendText(Environment.NewLine + sumpower);
            //}


        }



        private void Displayavg_Click(object sender, EventArgs e)
        {

        }


    }
}

    

            

            

        
    



    





  


       
    

