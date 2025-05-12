using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FATool
{
    //https://stackoverflow.com/questions/218060/random-gaussian-variables
    
    
    
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public static TextBox FIRFilterInputCountInstance;
        //public static TextBox IIRFilterInputCountInstance;
        public Form1()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            cbboxRoundingTolerance.DataSource = new ComboItem[]
            {
                new ComboItem{ Count = 1},
                new ComboItem{ Count = 2},
                new ComboItem{ Count = 3},
                new ComboItem{ Count = 4},
                new ComboItem{ Count = 5}
            };

            WindowsDropdown.DataSource = new ComboItemWindow[]
            {

                
                new ComboItemWindow{ WindowName = "Rectangular"},
                new ComboItemWindow{ WindowName = "Hamming"},
                new ComboItemWindow{ WindowName = "Hann"},
                new ComboItemWindow{ WindowName = "Triangular"},
                new ComboItemWindow{ WindowName = "Blackman"}
            };

           ApproximationDropdown.DataSource = new ComboItemApproximation[]
           {


                new ComboItemApproximation{ ApproximationName = "Butterworth 2. řád"}
                
           };

           


            DropdownFilterType.DataSource = new ComboItemApproximation[]
           {


                new ComboItemApproximation{ ApproximationName = "Dolní Propust"},
                new ComboItemApproximation{ ApproximationName = "Horní Propust"},
                new ComboItemApproximation{ ApproximationName = "Pásmová Propust"},
                new ComboItemApproximation{ ApproximationName = "Pásmová Zádrž"}

           };

            InitializeChart();
            instance = this;
            FIRFilterInputCountInstance = FIRFilterInputCount;
  
        }
        public double NextGaussian(int min, int max, Random Rand)
        {
            double mean = (min + max) / 2;
            double stdDev = 1;

            double u1 = Rand.NextDouble();
            double u2 = Rand.NextDouble();

            double result = mean + stdDev * Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);

            return result;
        }

        private void InitializeChart()
        {
            // Setting up chart parameters

            var objChart = CoeficientsChart.ChartAreas[0];
            var objSeries = CoeficientsChart.Series[0];
            objChart.AxisY.Enabled = AxisEnabled.True;
            objChart.AxisY.Enabled = AxisEnabled.True;
            objChart.AxisX.Title = "Frekvence (Hz)";
            objChart.AxisY.Title = "Síla";
            objChart.AxisX.Minimum = 0;
            objChart.AxisX.MajorGrid.LineWidth = 0;
            objChart.AxisY.MajorGrid.LineWidth = 0;
            objSeries.ChartType = SeriesChartType.Point; // or SeriesChartType.Point
            objSeries.Name = "MySeries";



            var objChart2 = CoeficientsChart2.ChartAreas[0];
            var objSeries2 = CoeficientsChart2.Series[0];
            objChart2.AxisY.Enabled = AxisEnabled.True;
            objChart2.AxisY.Enabled = AxisEnabled.True;
            objChart2.AxisX.Title = "Koeficient";
            objChart2.AxisY.Title = "Velikost";
            objChart2.AxisX.Minimum = 0;
            objSeries2.BorderWidth = 2;
            
            objChart2.AxisX.MajorGrid.LineWidth = 0;
            objChart2.AxisY.MajorGrid.LineWidth = 0;
            objSeries2.ChartType = SeriesChartType.BoxPlot; // or SeriesChartType.Point
            objSeries2.Name = "MySeries";


            var objChart3 = FIRChartInputs.ChartAreas[0];
            var objSeries3 = FIRChartInputs.Series[0];
            objChart3.AxisY.Enabled = AxisEnabled.True;
            objChart3.AxisY.Enabled = AxisEnabled.True;
            objChart3.AxisX.Title = "Koeficient";
            objChart3.AxisY.Title = "Velikost";
            objChart3.AxisX.Minimum = 0;

            objChart3.AxisX.MajorGrid.LineWidth = 0;
            objChart3.AxisY.MajorGrid.LineWidth = 0;
            objSeries3.ChartType = SeriesChartType.Line; // or SeriesChartType.Point
            objSeries3.Name = "MySeries";


            var objChart4 = FIRChartOutputs.ChartAreas[0];
            var objSeries4 = FIRChartOutputs.Series[0];
            objChart4.AxisY.Enabled = AxisEnabled.True;
            objChart4.AxisY.Enabled = AxisEnabled.True;
            objChart4.AxisX.Title = "Koeficient";
            objChart4.AxisY.Title = "Velikost";
            objChart4.AxisX.Minimum = 0;

            objChart4.AxisX.MajorGrid.LineWidth = 0;
            objChart4.AxisY.MajorGrid.LineWidth = 0;
            objSeries4.ChartType = SeriesChartType.Line; // or SeriesChartType.Point
            objSeries4.Name = "MySeries";


            var objChart5 = IIRChartInputs.ChartAreas[0];
            var objSeries5 = IIRChartInputs.Series[0];
            objChart5.AxisY.Enabled = AxisEnabled.True;
            objChart5.AxisY.Enabled = AxisEnabled.True;
            objChart5.AxisX.Title = "Koeficient";
            objChart5.AxisY.Title = "Velikost";
            objChart5.AxisX.Minimum = 0;

            objChart5.AxisX.MajorGrid.LineWidth = 0;
            objChart5.AxisY.MajorGrid.LineWidth = 0;
            objSeries5.ChartType = SeriesChartType.Line; // or SeriesChartType.Point
            objSeries5.Name = "MySeries";

            var objChart6 = IIRChartOutputs.ChartAreas[0];
            var objSeries6 = IIRChartOutputs.Series[0];
            objChart6.AxisY.Enabled = AxisEnabled.True;
            objChart6.AxisY.Enabled = AxisEnabled.True;
            objChart6.AxisX.Title = "Koeficient";
            objChart6.AxisY.Title = "Velikost";
            objChart6.AxisX.Minimum = 0;

            objChart6.AxisX.MajorGrid.LineWidth = 0;
            objChart6.AxisY.MajorGrid.LineWidth = 0;
            objSeries6.ChartType = SeriesChartType.Line; // or SeriesChartType.Point
            objSeries6.Name = "MySeries";



        }

        private List<double> GenerateWindowWeights(int Count,int WindowIndex)
        {
            if (WindowIndex == 0)//Rectangular
            {
                List<double> weightList = new List<double>();

                for (int n = 0; n < Count; n++)
                {
                    double value = 1;
                    weightList.Add(value);
                }


                return weightList;
            }
            if (WindowIndex == 1)//Hamming
            {
                List<double> weightList = new List<double>();
                //https://www.sciencedirect.com/topics/computer-science/windowing
                for (int n = 0; n < Count; n++)
                {
                    double value = 0.54 - (0.46*Math.Cos((2 * Math.PI * n) / (Count-1)));
                    weightList.Add(value);
                }


                return weightList;
            }
            if (WindowIndex == 2)//Hann
            {
                List<double> weightList = new List<double>();
               
                for (int n = 0; n < Count; n++)
                {
                    double value = 0.5 * (1 - Math.Cos((2 * Math.PI * n) / (Count - 1)));
                    weightList.Add(value);
                }


                return weightList;
            }
            if (WindowIndex == 3)//Triangular
            {
                List<double> weightList = new List<double>();

                for (int n = 0; n < Count; n++)
                {
                    double value = 1.0-Math.Abs((n-((Count - 1.0)/2.0)) / ((Count+1) / 2.0));
                    weightList.Add(value);
                }


                return weightList;
            }
            if (WindowIndex == 4)//Blackman
            {

                List<double> weightList = new List<double>();

                for (int n = 0; n < Count; n++)
                {
                    double cos1 = 0.5 * Math.Cos((2.0 * Math.PI * n) / (Count - 1));
                    double cos2 = 0.08 * Math.Cos((4.0 * Math.PI * n) / (Count - 1));
                    double value = 0.42 - cos1 + cos2;
                    weightList.Add(value);
                }
                return weightList;  
            }
            return new List<double>();
        }


        class ComboItem
        {
            public int Count { get; set; }
        }

        class ComboItemWindow
        {
            public string WindowName { get; set; }
        }

        class ComboItemApproximation
        {
            public string ApproximationName { get; set; }
        }
        #region TAB1
        private void Process_Click(object sender, EventArgs e)
        {
            CoeficientsChart.Series[0].Points.Clear();
            int numberOfCoeficients = 0;
            if (Int32.TryParse(TxtBoxNumberOfCoeficients.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {


                List<double> inputValues = new List<double>();
                for (int i = 0; i < numberOfCoeficients; i++)
                {
                    double value = 0;
                    try
                    {
                        value = Convert.ToDouble(this.flowLayoutPanelInputs.Controls[i].Text);
                    }
                    catch                    
                    {
                        this.flowLayoutPanelInputs.Controls[i].Text = "0";
                    }


                    inputValues.Add(value);
                }

                

                //VISUALIZE OUTPUT TODO
                List<string> output = DFT(inputValues);


                

                for (int i = 0; i < inputValues.Count; i++)
                {
                    this.flowLayoutPanelOutputs.Controls[i].Text = output[i].ToString();
                }
            }
            //VISUALIZE OUTPUT
        }

        private List<string> DFT(List<double> inputValues)
        {
            List<string> output = new List<string>();
            int numberOfCoeficients = 0;
            if (Int32.TryParse(TxtBoxNumberOfCoeficients.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {

                for (int i = 0; i < numberOfCoeficients; i++)
                {
                    output.Add(calculateCoeficient(inputValues, i));

                }
                return output;
            }
            return output;
        }

        private string calculateCoeficient(List<double> inputValues, int targetValue)
        {
            double numberOfValues = (double)inputValues.Count();


            double realSuma = 0;
            double imaginalSuma = 0;

            int numberOfCoeficients = 0;
            if (Int32.TryParse(TxtBoxNumberOfCoeficients.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {
                //EXPONENT CALCULATION
                for (int i = 0; i <= numberOfCoeficients - 1; i++)
                {
                    double angle = (-2.0 * Math.PI * (double)targetValue * (double)i) / numberOfValues;

                    double exponentRealValue = inputValues[i] * Math.Cos(angle);

                    double exponentImaginalValue = inputValues[i] * Math.Sin(angle);

                    if (chckBoxRoundingAllowed.Checked)
                    {
                        realSuma += Math.Round(exponentRealValue, (int)cbboxRoundingTolerance.SelectedValue);
                        imaginalSuma += Math.Round(exponentImaginalValue, (int)cbboxRoundingTolerance.SelectedValue);
                    }
                    else
                    {
                        realSuma += exponentRealValue;
                        imaginalSuma += exponentImaginalValue;
                    }
                    //funguje, problém je zaokrouhlování
                }
                //EXPONENT CALCULATION END

                double magnitude = Math.Sqrt(realSuma * realSuma + imaginalSuma * imaginalSuma);

                CoeficientsChart.Series[0].Points.AddXY(targetValue, magnitude);


                if (imaginalSuma < 0)
                {
                    return realSuma.ToString() + " -j" + Math.Abs(imaginalSuma).ToString();
                }
                return realSuma.ToString() + " +j" + imaginalSuma.ToString();

                

            }
            return "";
        }
        private void FFT_Click(object sender, EventArgs e)
        {
            CoeficientsChart.Series[0].Points.Clear();
            int numberOfCoeficients = 0;
            if (Int32.TryParse(TxtBoxNumberOfCoeficients.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {
                double Power = Math.Log(numberOfCoeficients, 2);
                if (Power % 1 == 0 ) //FFT IS ALLOWED ONLY IF NUMBER OF COEFICIENTS IS A POWER OF 2
                {
                    List<double> inputValues = new List<double>();
               
                    for (int i = 0; i < numberOfCoeficients; i++)
                    {
                        double value = 0;
                        try
                        {
                            value = Convert.ToDouble(this.flowLayoutPanelInputs.Controls[i].Text);
                        }
                        catch
                        {
                            this.flowLayoutPanelInputs.Controls[i].Text = "0";
                        }


                        inputValues.Add(value);
                    }


                    //VISUALIZE OUTPUT TODO
                    List<string> output = new List<string>();
                    for (int i = 0; i < inputValues.Count; i++)
                    {
                        Complex outputComplex = ProcessFFT(inputValues, inputValues.Count, i, 1);

                        if (chckBoxRoundingAllowed.Checked)
                        {
                            Complex outputComplexRoundex = Math.Round(outputComplex.Real, (int)cbboxRoundingTolerance.SelectedValue);
                      
                        }



                            double magnitude = Math.Sqrt(outputComplex.Real * outputComplex.Real + outputComplex.Imaginary * outputComplex.Imaginary);

                        CoeficientsChart.Series[0].Points.AddXY(i, magnitude);


                        if (outputComplex.Imaginary < 0)
                        {
                            if (chckBoxRoundingAllowed.Checked)
                            {
                                output.Add(outputComplex.Real.ToString() + " -j" + Math.Round(Math.Abs(outputComplex.Imaginary), (int)cbboxRoundingTolerance.SelectedValue).ToString());
                            }
                            else 
                            {                                
                                output.Add(outputComplex.Real.ToString() + " -j" + Math.Abs(outputComplex.Imaginary).ToString());
                            }
                        }
                        else
                        {
                            if (chckBoxRoundingAllowed.Checked)
                            {
                                output.Add(outputComplex.Real.ToString() + " +j" + Math.Round(Math.Abs(outputComplex.Imaginary), (int)cbboxRoundingTolerance.SelectedValue).ToString());
                            }
                            else
                            {
                                output.Add(outputComplex.Real.ToString() + " +j" + Math.Abs(outputComplex.Imaginary).ToString());
                            }            
                        }


                    }

                    for (int i = 0; i < inputValues.Count; i++)
                    {
                        this.flowLayoutPanelOutputs.Controls[i].Text = output[i].ToString();
                    }
                }
                else 
                {
                    Process_Click(sender, e);
                }
            }


            //VISUALIZE OUTPUT
        }

        public Complex ProcessFFT(List<double> inputValues, int N, int targetIndex, int splitOrder)
        {
            if (inputValues.Count == 1)
            {
                return inputValues[0];
            }
            else
            {
                List<double> EvenList = new List<double>();
                List<double> OddList = new List<double>();
                for (int i = 0; i < inputValues.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        EvenList.Add(inputValues[i]);
                    }
                    else
                    {
                        OddList.Add(inputValues[i]);
                    }
                }

                //eulers cos x + i sin x

                double angle = (-Math.Pow(2, splitOrder) * Math.PI * (double)targetIndex) / N;
                double cosAngleValue = 0;
                Complex sinAngleValue = 0;

                cosAngleValue = Math.Round(Math.Cos(angle), 3);

                sinAngleValue = Complex.Multiply(new Complex(0, 1), (double)Math.Round(Math.Sin(angle), 3));


                return Complex.Add(ProcessFFT(EvenList, N, targetIndex, splitOrder + 1), (cosAngleValue + sinAngleValue) * ProcessFFT(OddList, N, targetIndex, splitOrder + 1));
            }
        }
        

        private void FillWithData(string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                this.flowLayoutPanelInputs.Controls[i].Text = data[i].ToString();
            }
        }



        private void BrowseFileDFT_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Filter = "*.txt|";
                string output = "";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    StreamReader reader = new StreamReader(ofd.FileName);
                    output = reader.ReadToEnd();
                    output = output.Replace("\r\n", "");
                    output = output.Replace(".", ",");

                    string[] values = output.Split(';');

                    TxtBoxNumberOfCoeficients.Text = values.Length.ToString();
                    FillWithData(values);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtBoxNumberOfCoeficients_TextChanged(object sender, EventArgs e)
        {
            int numberOfCoeficients = 0;
            if (Int32.TryParse(TxtBoxNumberOfCoeficients.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {
                int count = this.flowLayoutPanelInputs.Controls.Count;

                if (count < numberOfCoeficients)
                {
                    while (count < numberOfCoeficients)
                    {
                        TextBox inputtextBox = new TextBox();
                        this.flowLayoutPanelInputs.Controls.Add(inputtextBox);

                        TextBox outputtextBox = new TextBox();
                        this.flowLayoutPanelOutputs.Controls.Add(outputtextBox);
                        count++;
                    }
                }
                if (count > numberOfCoeficients)
                {
                    while (count > numberOfCoeficients)
                    {
                        this.flowLayoutPanelInputs.Controls[count - 1].Dispose();
                        this.flowLayoutPanelOutputs.Controls[count - 1].Dispose();
                        count--;
                    }

                }
            }
        }


        private void SaveCoeficients_Click(object sender, EventArgs e)
        {
            int numberOfCoeficients = 0;
            if (Int32.TryParse(TxtBoxNumberOfCoeficients.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Application.StartupPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    StreamWriter sw = new StreamWriter(path);

                    
                    
                    
                    for (int i = 0; i < numberOfCoeficients; i++)
                    {    
                       sw.WriteLine(this.flowLayoutPanelOutputs.Controls[i].Text);
                    }

                    sw.Close();
                }
            }
        }

        #endregion










        #region TAB2
        
        private void CalculatePass(bool IsLowPass)
        {
            
            
           

            CoeficientsChart2.Series[0].Points.Clear();
            int filterOrder;
            List<double> weightValues=new List<double>();
            int Fs;
            int Fc;

            if (Int32.TryParse(txtBoxFilterOrder.Text,out filterOrder) && Int32.TryParse(txtBoxSamplingFrequency.Text, out Fs) && Int32.TryParse(txtBoxCutoutFrequency.Text, out Fc) && filterOrder >= 0 && Fs >= 0 && Fc >= 0)
            {
                if (Fc >= Fs / 2)
                {
                    return;
                }

                //perioda vzorkování
                double period = (2 * Math.PI * Fc) / Fs;

                //
                int k = 0;
                if (filterOrder % 2 == 0)
                {
                    k = filterOrder / 2 + 1;
                    
                    
                }
                else 
                {
                    k = (filterOrder+1) / 2;
                }
                if (WindowsDropdown.SelectedValue.ToString() == "Rectangular")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 0);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Hamming")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 1);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Hann")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 2);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Triangular")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 3);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Blackman")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 4);
                }


                CoeficientsChart2.ChartAreas[0].AxisX.Minimum = 0;
                CoeficientsChart2.ChartAreas[0].AxisX.Maximum = filterOrder + 1;
                CoeficientsChart2.ChartAreas[0].AxisX.Crossing = 0;
                CoeficientsChart2.ChartAreas[0].AxisY.Crossing = 0;




                List<string> results = new List<string>();
                List<double> resultsForChart = new List<double>();


                for (int i = 0; i < k; i++)
                {
                    if (i == 0)
                    {

                        double result = (period / Math.PI);

                        results.Add(result.ToString());
                        resultsForChart.Add(result);
                    }
                    else
                    {
                        if (IsLowPass)
                        {
                            double sinResult = Math.Sin(i * period);
                            double result = ((period / Math.PI) * (sinResult / (i * period)))* weightValues[i + k-1];
                            results.Add(result.ToString());
                            resultsForChart.Add(result);

                        }
                        else
                        {
                            double sinResult = Math.Sin(i * period);
                            double result = -((period / Math.PI) * (sinResult / (i * period))) * weightValues[i + k-1];
                            results.Add(result.ToString());
                            resultsForChart.Add(result);

                        }
                        
                    }
                }

                List<string> finalList = new List<string>(results);
                List<double> FinalResultsForChart = new List<double>(resultsForChart);


                //weightValues[i - 1]
                //Add mirrored values to list
                if (filterOrder % 2 == 0)
                {
                    for (int i = 1; i <= filterOrder / 2; i++)
                    {
                        finalList.Insert(0, results[i]);
                        FinalResultsForChart.Insert(0, resultsForChart[i]);
                    }
                }
                else 
                {
                    for (int i = 0; i <= (filterOrder-1) / 2; i++)
                    {
                        finalList.Insert(0, results[i]);
                        FinalResultsForChart.Insert(0, resultsForChart[i]);
                    }
                }

               
                






                //Add mirrored values to list END

                string resultString = "";

             
                foreach (string Svalue in finalList)
                {
                    resultString += Svalue + "\r\n";


                }
                resultString = resultString.Remove(resultString.LastIndexOf("\r\n"));


                int index = 0;
                foreach (double value in FinalResultsForChart)
                {
                    

                    CoeficientsChart2.Series[0].Points.AddXY(index, value);
                    index++;
                }

                lblResultArea.Text = resultString;
            }

        }
        private void CalculateDP_Click(object sender, EventArgs e)
        {
            CalculatePass(true);
        }
        private void CalculateHP_Click(object sender, EventArgs e)
        {
            CalculatePass(false);
        }


        private void CalculateBandPassBandStop(bool IsBandPass)
        {
            CoeficientsChart2.Series[0].Points.Clear();
            int Fc1;
            int Fc2;
            List<double> weightValues = new List<double>();
            //get values
            int filterOrder;

            int Fs;
     

            if (Int32.TryParse(txtBoxFilterOrder.Text, out filterOrder) && Int32.TryParse(txtBoxSamplingFrequency.Text, out Fs) && Int32.TryParse(txtBoxCutoutFrequency1.Text, out Fc1) && Int32.TryParse(txtBoxCutoutFrequency2.Text, out Fc2) && filterOrder >= 0 && Fs >= 0 && Fc1 >= 0 && Fc2 >= 0)
            {
                if (Fc2-Fc1 >= Fs / 2)
                {
                    return;
                }


                //perioda vzorkování
                double periodFc1 = 2 * Math.PI * Fc1 / Fs;
                double periodFc2 = 2 * Math.PI * Fc2 / Fs;

                double periodtotal = (2 * Math.PI * (Fc2 - Fc1)) / Fs;

                //\
                int k = 0;
                if (filterOrder % 2 == 0)
                {
                    k = filterOrder / 2 + 1;


                }
                else
                {
                    k = (filterOrder + 1) / 2;
                }

                if (WindowsDropdown.SelectedValue.ToString() == "Rectangular")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 0);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Hamming")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 1);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Hann")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 2);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Triangular")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 3);
                }
                else if (WindowsDropdown.SelectedValue.ToString() == "Blackman")
                {
                    weightValues = GenerateWindowWeights(filterOrder + 1, 4);
                }

                CoeficientsChart2.ChartAreas[0].AxisX.Minimum = 0;
                CoeficientsChart2.ChartAreas[0].AxisX.Maximum = filterOrder + 1;
                CoeficientsChart2.ChartAreas[0].AxisX.Crossing = 0;
                CoeficientsChart2.ChartAreas[0].AxisY.Crossing = 0;

                List<double> resultsForChart = new List<double>();
                List<string> results = new List<string>();

                for (int i = 0; i < k; i++)
                {
                    if (i == 0)
                    {

                        double result = (periodtotal / Math.PI);

                        results.Add(result.ToString());
                        resultsForChart.Add(result);

                    }
                    else
                    {
                        double sinResultFc1 = Math.Sin(i * periodFc1);
                        double sinResultFc2 = Math.Sin(i * periodFc2);
                        double result = 0;
                        if (IsBandPass)
                        {



                            result = (((1 / (Math.PI * i)) * sinResultFc2) - ((1 / (Math.PI * i)) * sinResultFc1))* weightValues[i + k - 1];
                            resultsForChart.Add(result);
                        }
                        else
                        {
                            result = -(((1 / (Math.PI * i)) * sinResultFc2) - ((1 / (Math.PI * i)) * sinResultFc1))*weightValues[i + k - 1];
                            resultsForChart.Add(result);
                        }



                        results.Add(result.ToString());

                    }
                }

                List<string> finalList = new List<string>(results);
                List<double> FinalResultsForChart = new List<double>(resultsForChart);

                //Add mirrored values to list
                if (filterOrder % 2 == 0)
                {
                    for (int i = 1; i <= filterOrder / 2; i++)
                    {
                        finalList.Insert(0, results[i]);
                        FinalResultsForChart.Insert(0, resultsForChart[i]);
                    }
                }
                else
                {
                    for (int i = 0; i <= (filterOrder - 1) / 2; i++)
                    {
                        finalList.Insert(0, results[i]);
                        FinalResultsForChart.Insert(0, resultsForChart[i]);
                    }
                }

                //Add mirrored values to list END

                string resultString = "";

                foreach (string Svalue in finalList)
                {
                    resultString += Svalue + "\r\n";
                }

                resultString = resultString.Remove(resultString.LastIndexOf("\r\n"));

                int index = 0;
                foreach (double value in FinalResultsForChart)
                {


                    CoeficientsChart2.Series[0].Points.AddXY(index, value);
                    index++;
                }
                lblResultArea.Text = resultString;

            }
        }


        private void BandPassClick_Click(object sender, EventArgs e)
        {

            CalculateBandPassBandStop(true);
            
        }

        private void BandStop_Click(object sender, EventArgs e)
        {
            CalculateBandPassBandStop(false);
        }




        #endregion

        #region TAB3

        public void FillFromPopup(int Count,int Min,int Max,bool IntegerChecked,string Distribution)
        {
            Random Rand = new Random();
            FIRFilterInputCountInstance.Text = Count.ToString();      
            for (int i = 0; i < Count; i++)
            {
                if (Distribution.ToLower().Equals("equal"))
                    if (IntegerChecked)
                    {
                        int random = Rand.Next(Min, Max + 1);
                        this.FIRFilterInputFieldsLayout.Controls[i].Text = random.ToString();
                    }
                    else
                    {
                        double random = Rand.NextDouble() * (Max - Min) + Min;
                        this.FIRFilterInputFieldsLayout.Controls[i].Text = random.ToString();

                    }
                else if(Distribution.ToLower().Equals("normal"))
                {

                    double randNormal = NextGaussian(Min, Max, Rand);
                    
                    if (IntegerChecked)
                    {
                        randNormal = Math.Round(randNormal);
                    }
                    

                    this.FIRFilterInputFieldsLayout.Controls[i].Text = randNormal.ToString();
                }
            }
        }

        private void FIRFilterInputCount_TextChanged(object sender, EventArgs e)
        {
            int numberOfCoeficients = 0;
            if (Int32.TryParse(FIRFilterInputCount.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {
                int count = this.FIRFilterInputFieldsLayout.Controls.Count;

                if (count < numberOfCoeficients)
                {
                    while (count < numberOfCoeficients)
                    {
                        TextBox inputtextBox = new TextBox();
                        this.FIRFilterInputFieldsLayout.Controls.Add(inputtextBox);

                        

                        TextBox outputtextBox = new TextBox();
                        this.FIRFilterOutputFieldsLayout.Controls.Add(outputtextBox);
                        count++;
                    }
                }
                if (count > numberOfCoeficients)
                {
                    while (count > numberOfCoeficients)
                    {
                        this.FIRFilterInputFieldsLayout.Controls[count - 1].Dispose();
                        
                        this.FIRFilterOutputFieldsLayout.Controls[count - 1].Dispose();
                        count--;
                    }

                }
            }
        }






       

        private void BtnLoadWeights_Click(object sender, EventArgs e)
        {
            string weights = lblResultArea.Text;

            string[] WeightsArray = weights.Replace("\r\n", "|").Split('|');

            FilterOrderTextBox.Text = WeightsArray.Length.ToString();

            for (int i = 0; i < WeightsArray.Length; i++)
            {
                this.FIRFilterWeightsFieldsLayout.Controls[i].Text = WeightsArray[i].ToString();
            }

        }

        private void FilterOrderTextBox_TextChanged(object sender, EventArgs e)
        {
            int numberOfCoeficients = 0;
            if (Int32.TryParse(FilterOrderTextBox.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {
                int count = this.FIRFilterWeightsFieldsLayout.Controls.Count;

                if (count < numberOfCoeficients)
                {
                    while (count < numberOfCoeficients)
                    {
                        TextBox inputtextBox = new TextBox();
                        this.FIRFilterWeightsFieldsLayout.Controls.Add(inputtextBox);

                        count++;
                    }
                }
                if (count > numberOfCoeficients)
                {
                    while (count > numberOfCoeficients)
                    {
                        this.FIRFilterWeightsFieldsLayout.Controls[count - 1].Dispose();
                        count--;
                    }

                }
            }
        }

        private void BtnProcessSignal_Click(object sender, EventArgs e)
        {
            FIRChartInputs.Series[0].Points.Clear();
            FIRChartOutputs.Series[0].Points.Clear();

            
            Int32.TryParse(FIRFilterInputCount.Text, out int numberOfInputs);

            if(numberOfInputs> 0 && numberOfInputs >= 0)
            {
                FIRChartInputs.ChartAreas[0].AxisX.Minimum = 0;
                FIRChartInputs.ChartAreas[0].AxisX.Maximum = numberOfInputs;
                FIRChartInputs.ChartAreas[0].AxisX.Crossing = 0;
                FIRChartInputs.ChartAreas[0].AxisY.Crossing = 0;

                FIRChartOutputs.ChartAreas[0].AxisX.Minimum = 0;
                FIRChartOutputs.ChartAreas[0].AxisX.Maximum = numberOfInputs;
                FIRChartOutputs.ChartAreas[0].AxisX.Crossing = 0;
                FIRChartOutputs.ChartAreas[0].AxisY.Crossing = 0;



                List<double> InputList = new List<double>();
                List<double> OutputList = new List<double>();
                List<double> WeightsList = new List<double>();

                for (int i = 0; i < numberOfInputs; i++)
                {
                    double value = 0;
                    try
                    {
                        value = Convert.ToDouble(this.FIRFilterInputFieldsLayout.Controls[i].Text);
                    }
                    catch
                    {
                        this.FIRFilterInputFieldsLayout.Controls[i].Text = "0";
                    }
                    InputList.Add(value);
                    FIRChartInputs.Series[0].Points.AddXY(i, value);
                }

                Int32.TryParse(FilterOrderTextBox.Text, out int FilterOrder);
                if (FilterOrder < 0)
                {
                    return;
                }

                for (int i = 0; i < FilterOrder; i++)
                {
                    double value = 0;
                    try
                    {
                        value = Convert.ToDouble(this.FIRFilterWeightsFieldsLayout.Controls[i].Text);
                    }
                    catch
                    {
                        this.FIRFilterWeightsFieldsLayout.Controls[i].Text = "0";
                    }
                    WeightsList.Add(value);
                }


                for (int i = 0; i < numberOfInputs; i++)
                {
                    double value = CalculateOutputSignalOnIndex(InputList, WeightsList, FilterOrder-1, i);
                    OutputList.Add(value);
                    FIRChartOutputs.Series[0].Points.AddXY(i, value);
                }


                for (int i = 0; i < numberOfInputs; i++)
                {
                    this.FIRFilterOutputFieldsLayout.Controls[i].Text = OutputList[i].ToString();
                }
            }
                
        }

        double CalculateOutputSignalOnIndex(List<double> InputList, List<double> WeightsList,int filterOrder,int index)
        {
            double result = 0;
            for (int i = 0; i <= filterOrder; i++)
            {
                if (index - i < 0)
                { }
                else 
                {
                    result += WeightsList[i] * InputList[index - i];
                }
                
            }
            return result;

        }

        private void BtnGenerateSignal_Click(object sender, EventArgs e)
        {
            FIRFilterPopupInput form =new FIRFilterPopupInput();
            form.ShowDialog();
        }

        public void FillWithDataFIR(string[] data)
        {
            FIRFilterInputCount.Text = data.Length.ToString();
            for (int i = 0; i < data.Length; i++)
            {
                this.FIRFilterInputFieldsLayout.Controls[i].Text = data[i].ToString();
            }

        }


        #endregion

        #region TAB4
        private void BtnProcessIIRFilter_Click(object sender, EventArgs e)
        {

            TxtBoxaCoeficients.Text = "";
            TxtBoxbCoeficients.Text = "";

            Int32.TryParse(TxtBoxIIRSampleFrequency.Text, out int IIRSampleFrequency);
            Int32.TryParse(TxtBoxIIRCutout1Frequency.Text, out int IIRCutout1Frequency);
            Int32.TryParse(TxtBoxIIRCutout2Frequency.Text, out int IIRCutout2Frequency);

            if (!(IIRSampleFrequency > 0 && IIRCutout1Frequency > 0 ))
            {
                return;            
            }

            double SamplingPeriod = 1.0 / IIRSampleFrequency;

            switch (ApproximationDropdown.SelectedValue.ToString())
            {

                case "Butterworth 2. řád":
                    switch (DropdownFilterType.SelectedValue.ToString())
                    {
                        case "Pásmová Zádrž":
                            if (!(IIRCutout2Frequency > 0 && IIRCutout1Frequency < IIRCutout2Frequency))
                            {
                                return;
                            }

                            double OmegaN1 = 2 * Math.PI * IIRCutout1Frequency;
                            double OmegaN2 = 2 * Math.PI * IIRCutout2Frequency;

                            double OmegaAnN1= (2 / SamplingPeriod) * Math.Tan((OmegaN1 * SamplingPeriod) / 2.0);
                            double OmegaAnN2= (2 / SamplingPeriod) * Math.Tan((OmegaN2 * SamplingPeriod) / 2.0);

                            //FREQUENCY CHECK
                            double IIRCutout1FrequencyModified = OmegaAnN1 / (2 * Math.PI);
                            double IIRCutout2FrequencyModified = OmegaAnN2 / (2 * Math.PI);
                            /////////////////

                        


                            double DeltaOmega = OmegaAnN2 - OmegaAnN1;
                            double OmegaSQ = OmegaAnN2 * OmegaAnN1;

                            //SO FAR CORRECT VALUES



                            List<double> aAnalogCoeficients = new List<double>();
                            aAnalogCoeficients.Add(OmegaSQ * OmegaSQ);
                            aAnalogCoeficients.Add(0);
                            aAnalogCoeficients.Add(2 * OmegaSQ);
                            aAnalogCoeficients.Add(0);
                            aAnalogCoeficients.Add(1);

                            List<double> bAnalogCoeficients = new List<double>();
                            bAnalogCoeficients.Add(OmegaSQ * OmegaSQ);
                            bAnalogCoeficients.Add(Math.Sqrt(2)* DeltaOmega* OmegaSQ);
                            bAnalogCoeficients.Add(DeltaOmega* DeltaOmega + 2* OmegaSQ);
                            bAnalogCoeficients.Add(Math.Sqrt(2)* DeltaOmega);
                            bAnalogCoeficients.Add(1);

                            List<double> cDigitalCoeficients = new List<double>();
                            cDigitalCoeficients.Add(16 + 4 * aAnalogCoeficients[2] * Math.Pow(SamplingPeriod, 2) + aAnalogCoeficients[0]*Math.Pow(SamplingPeriod, 4));
                            cDigitalCoeficients.Add(-64 + (4 * aAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4)));
                            cDigitalCoeficients.Add(96 - (8 * aAnalogCoeficients[2] * Math.Pow(SamplingPeriod, 2)) + (6* aAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4)));
                            cDigitalCoeficients.Add(-64 + (4 * aAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4)));
                            cDigitalCoeficients.Add(16 + 4 * aAnalogCoeficients[2] * Math.Pow(SamplingPeriod, 2) + aAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4));

                            List<double> dDigitalCoeficients = new List<double>();


                            dDigitalCoeficients.Add(16 + 8 * bAnalogCoeficients[3] * SamplingPeriod + 4 * bAnalogCoeficients[2] * Math.Pow(SamplingPeriod, 2) + 2 * bAnalogCoeficients[1] * Math.Pow(SamplingPeriod, 3) + bAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficients.Add(-64 - 16 * bAnalogCoeficients[3] * SamplingPeriod + 4 * bAnalogCoeficients[1] * Math.Pow(SamplingPeriod, 3) + 4 * bAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficients.Add(96-8* bAnalogCoeficients[2]* Math.Pow(SamplingPeriod, 2)+6* bAnalogCoeficients[0]* Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficients.Add(-64 + 16 * bAnalogCoeficients[3] * SamplingPeriod - 4 * bAnalogCoeficients[1] * Math.Pow(SamplingPeriod, 3) + 4 * bAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficients.Add(16 - 8 * bAnalogCoeficients[3] * SamplingPeriod + 4 * bAnalogCoeficients[2] * Math.Pow(SamplingPeriod, 2) - 2 * bAnalogCoeficients[1] * Math.Pow(SamplingPeriod, 3) + bAnalogCoeficients[0] * Math.Pow(SamplingPeriod, 4));


                            string resultString = "";
                            foreach (double value in cDigitalCoeficients)
                            {
                                resultString += value/ dDigitalCoeficients[0] + "\r\n";
                            }
                            resultString = resultString.Remove(resultString.LastIndexOf("\r\n"));
                            TxtBoxbCoeficients.Text += resultString;


                            resultString = "";
                            foreach (double value in dDigitalCoeficients)
                            {
                                resultString += value/ dDigitalCoeficients[0] + "\r\n";
                            }
                            resultString = resultString.Remove(resultString.LastIndexOf("\r\n"));
                            TxtBoxaCoeficients.Text += resultString;

                            break;


                        case "Pásmová Propust":
                            if (!(IIRCutout2Frequency > 0 && IIRCutout1Frequency < IIRCutout2Frequency))
                            {
                                return;
                            }

                            double OmegaN1P = 2 * Math.PI * IIRCutout1Frequency;
                            double OmegaN2P = 2 * Math.PI * IIRCutout2Frequency;

                            double OmegaAnN1P = (2 / SamplingPeriod) * Math.Tan((OmegaN1P * SamplingPeriod) / 2.0);
                            double OmegaAnN2P = (2 / SamplingPeriod) * Math.Tan((OmegaN2P * SamplingPeriod) / 2.0);

                            //FREQUENCY CHECK
                            double IIRCutout1FrequencyModifiedP = OmegaAnN1P / (2 * Math.PI);
                            double IIRCutout2FrequencyModifiedP = OmegaAnN2P / (2 * Math.PI);
                            /////////////////




                            double DeltaOmegaP = OmegaAnN2P - OmegaAnN1P;
                            double OmegaSQP = OmegaAnN2P * OmegaAnN1P;

                            //SO FAR CORRECT VALUES



                            List<double> aAnalogCoeficientsP = new List<double>();
                            aAnalogCoeficientsP.Add(0);
                            aAnalogCoeficientsP.Add(0);
                            aAnalogCoeficientsP.Add(DeltaOmegaP * DeltaOmegaP);
                            aAnalogCoeficientsP.Add(0);
                            aAnalogCoeficientsP.Add(0);

                            List<double> bAnalogCoeficientsP = new List<double>();
                            bAnalogCoeficientsP.Add(OmegaSQP * OmegaSQP);
                            bAnalogCoeficientsP.Add(Math.Sqrt(2) * DeltaOmegaP * OmegaSQP);
                            bAnalogCoeficientsP.Add(DeltaOmegaP * DeltaOmegaP + 2 * OmegaSQP);
                            bAnalogCoeficientsP.Add(Math.Sqrt(2) * DeltaOmegaP);
                            bAnalogCoeficientsP.Add(1);

                            List<double> cDigitalCoeficientsP = new List<double>();
                            cDigitalCoeficientsP.Add(4 * aAnalogCoeficientsP[2] * Math.Pow(SamplingPeriod, 2));
                            cDigitalCoeficientsP.Add(0);
                            cDigitalCoeficientsP.Add(- 8 * aAnalogCoeficientsP[2] * Math.Pow(SamplingPeriod, 2));
                            cDigitalCoeficientsP.Add(0);
                            cDigitalCoeficientsP.Add(4 * aAnalogCoeficientsP[2] * Math.Pow(SamplingPeriod, 2));

                            List<double> dDigitalCoeficientsP = new List<double>();


                            dDigitalCoeficientsP.Add(16 + 8 * bAnalogCoeficientsP[3] * SamplingPeriod + 4 * bAnalogCoeficientsP[2] * Math.Pow(SamplingPeriod, 2) + 2 * bAnalogCoeficientsP[1] * Math.Pow(SamplingPeriod, 3) + bAnalogCoeficientsP[0] * Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficientsP.Add(-64 - 16 * bAnalogCoeficientsP[3] * SamplingPeriod + 4 * bAnalogCoeficientsP[1] * Math.Pow(SamplingPeriod, 3) + 4 * bAnalogCoeficientsP[0] * Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficientsP.Add(96 - 8 * bAnalogCoeficientsP[2] * Math.Pow(SamplingPeriod, 2) + 6 * bAnalogCoeficientsP[0] * Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficientsP.Add(-64 + 16 * bAnalogCoeficientsP[3] * SamplingPeriod - 4 * bAnalogCoeficientsP[1] * Math.Pow(SamplingPeriod, 3) + 4 * bAnalogCoeficientsP[0] * Math.Pow(SamplingPeriod, 4));
                            dDigitalCoeficientsP.Add(16 - 8 * bAnalogCoeficientsP[3] * SamplingPeriod + 4 * bAnalogCoeficientsP[2] * Math.Pow(SamplingPeriod, 2) - 2 * bAnalogCoeficientsP[1] * Math.Pow(SamplingPeriod, 3) + bAnalogCoeficientsP[0] * Math.Pow(SamplingPeriod, 4));


                            string resultStringP = "";
                            foreach (double value in cDigitalCoeficientsP)
                            {
                                resultStringP += value / dDigitalCoeficientsP[0] + "\r\n";
                            }
                            resultStringP = resultStringP.Remove(resultStringP.LastIndexOf("\r\n"));
                            TxtBoxbCoeficients.Text += resultStringP;


                            resultStringP = "";
                            foreach (double value in dDigitalCoeficientsP)
                            {
                                resultStringP += value / dDigitalCoeficientsP[0] + "\r\n";
                            }
                            resultStringP = resultStringP.Remove(resultStringP.LastIndexOf("\r\n"));
                            TxtBoxaCoeficients.Text += resultStringP;

                            break;


                        case "Dolní Propust":
                            calculatePassIIRButterworth(IIRCutout1Frequency, SamplingPeriod, false);

                            break;

                        case "Horní Propust":
                            calculatePassIIRButterworth(IIRCutout1Frequency, SamplingPeriod, true);

                            break;
                    }
                    break;         
            }
        }

        private void calculatePassIIRButterworth(double IIRCutout1Frequency,double SamplingPeriod,bool IsHighPass)
        {
            double OmegaN = 2 * Math.PI * IIRCutout1Frequency;


            double OmegaAnN = (2 / SamplingPeriod) * Math.Tan((OmegaN * SamplingPeriod) / 2.0);


            //FREQUENCY CHECK
            double IIRCutoutFrequencyModified = OmegaAnN / (2 * Math.PI);


            //SO FAR CORRECT VALUES



            List<double> aAnalogCoeficientsLP = new List<double>();
            aAnalogCoeficientsLP.Add(OmegaAnN * OmegaAnN);
            aAnalogCoeficientsLP.Add(0);
            aAnalogCoeficientsLP.Add(0);


            List<double> bAnalogCoeficientsLP = new List<double>();
            bAnalogCoeficientsLP.Add(OmegaAnN * OmegaAnN);
            bAnalogCoeficientsLP.Add(Math.Sqrt(2) * OmegaAnN);
            bAnalogCoeficientsLP.Add(1);


            List<double> cDigitalCoeficientsLP = new List<double>();
            
            
            cDigitalCoeficientsLP.Add(aAnalogCoeficientsLP[0] * Math.Pow(SamplingPeriod, 2));

            if (IsHighPass)
            {
                cDigitalCoeficientsLP.Add(-2 * aAnalogCoeficientsLP[0] * Math.Pow(SamplingPeriod, 2));
            }
            else 
            {
                cDigitalCoeficientsLP.Add(2 * aAnalogCoeficientsLP[0] * Math.Pow(SamplingPeriod, 2));
            }
            
            cDigitalCoeficientsLP.Add(aAnalogCoeficientsLP[0] * Math.Pow(SamplingPeriod, 2));

            List<double> dDigitalCoeficientsLP = new List<double>();

            dDigitalCoeficientsLP.Add(4 * bAnalogCoeficientsLP[2] + 2 * SamplingPeriod * bAnalogCoeficientsLP[1] + bAnalogCoeficientsLP[0] * Math.Pow(SamplingPeriod, 2));
            dDigitalCoeficientsLP.Add(-8 * bAnalogCoeficientsLP[2] + 2 * bAnalogCoeficientsLP[0] * Math.Pow(SamplingPeriod, 2));
            dDigitalCoeficientsLP.Add(4 * bAnalogCoeficientsLP[2] - 2 * SamplingPeriod * bAnalogCoeficientsLP[1] + bAnalogCoeficientsLP[0] * Math.Pow(SamplingPeriod, 2));


            string resultStringLP = "";
            foreach (double value in cDigitalCoeficientsLP)
            {
                resultStringLP += value / dDigitalCoeficientsLP[0] + "\r\n";
            }
            resultStringLP = resultStringLP.Remove(resultStringLP.LastIndexOf("\r\n"));
            TxtBoxbCoeficients.Text += resultStringLP;


            resultStringLP = "";
            foreach (double value in dDigitalCoeficientsLP)
            {
                resultStringLP += value / dDigitalCoeficientsLP[0] + "\r\n";
            }
            resultStringLP = resultStringLP.Remove(resultStringLP.LastIndexOf("\r\n"));
            TxtBoxaCoeficients.Text += resultStringLP;

        }




        #endregion

        private void DropdownFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DropdownFilterType.SelectedValue.ToString())
            {
                case "Dolní Propust":
                    TxtBoxIIRCutout2Frequency.Visible = false;
                    lblFc1.Text = "Mezní frekvence";
                    lblFc2.Visible = false;
                    break;

                case "Horní Propust":
                    TxtBoxIIRCutout2Frequency.Visible = false;
                    lblFc1.Text = "Mezní frekvence";
                    lblFc2.Visible = false;
                    break;


                case "Pásmová Zádrž":
                    TxtBoxIIRCutout2Frequency.Visible = true;
                    lblFc1.Text = "Dolní mezní frekvence";
                    lblFc2.Visible = true;
                    break;

                case "Pásmová Propust":

                    TxtBoxIIRCutout2Frequency.Visible = true;
                    lblFc1.Text = "Dolní mezní frekvence";
                    lblFc2.Visible = true;
                    break;
            }
        }

        private void btnLoadIIRFilter_Click(object sender, EventArgs e)
        {
            /*
            string weights = lblResultArea.Text;

            string[] WeightsArray = weights.Replace("\r\n", "|").Split('|');

            FilterOrderTextBox.Text = WeightsArray.Length.ToString();

            for (int i = 0; i < WeightsArray.Length; i++)
            {
            IRRFilterBWeights
                this.FIRFilterWeightsFieldsLayout.Controls[i].Text = WeightsArray[i].ToString();
            }
            */


            string result = "yₙ = ";
            
            List<double> cDigitalCoeficients = new List<double>();
            
            string weights = TxtBoxbCoeficients.Text;

            string[] cWeightsArray = weights.Replace("\r\n", "|").Split('|');


            txtBoxBWeightsCount.Text = cWeightsArray.Length.ToString();
            for (int i = cWeightsArray.Length-1; i >= 0; i--)
            {
                Double.TryParse(cWeightsArray[i].ToString(), out double dweight);

                if (Math.Abs(dweight) < 0.01)
                {
                    cDigitalCoeficients.Add(0);
                    dweight = 0;              
                    this.IRRFilterBWeights.Controls[i].Text = dweight.ToString();
                }
                else 
                {
                    dweight = Math.Round(dweight,3);
                    cDigitalCoeficients.Add(dweight);
                    this.IRRFilterBWeights.Controls[i].Text = dweight.ToString();
                }
                if (dweight != 0)
                {
                    if (cWeightsArray.Length - 1 - i == 0)
                    {
                        result += dweight.ToString() + " * x(k) ";
                    }
                    else
                    {
                        result += " + " + dweight.ToString() + " * x(k - " + (cWeightsArray.Length - 1 - i).ToString() + ") ";
                    }
                }
                //this.FIRFilterWeightsFieldsLayout.Controls[i].Text = WeightsArray[i].ToString();
            }


            List<double> dDigitalCoeficients = new List<double>();

            string dweights = TxtBoxaCoeficients.Text;

            string[] dWeightsArray = dweights.Replace("\r\n", "|").Split('|');

           

            txtBoxAWeightsCount.Text = dWeightsArray.Length.ToString();
            for (int i = dWeightsArray.Length-1; i > 0; i--)
            {
                
                Double.TryParse(dWeightsArray[i].ToString(), out double ddweight);

                if (Math.Abs(ddweight) < 0.01)
                {
                    ddweight = 0;
                    dDigitalCoeficients.Add(0);
                    this.IRRFilterAWeights.Controls[i].Text = ddweight.ToString();
                }
                else
                {
                    ddweight = Math.Round(ddweight, 3);
                    dDigitalCoeficients.Add(ddweight);
                    this.IRRFilterAWeights.Controls[i].Text = ddweight.ToString();
                }
                if (ddweight != 0)
                {                                     
                        result += " - " + ddweight.ToString() + " * y(k - " + (i).ToString() + ") ";
                }
                
                //this.FIRFilterWeightsFieldsLayout.Controls[i].Text = WeightsArray[i].ToString();
            }
           










            txtBoxIIREquation.Text= result;


            //ₐ ₑ ₕ ᵢ ⱼ ₖ ₗ ₘ ₙ ₒ ₚ ᵣ ₛ ₜ ᵤ ᵥ ₓ
            //¹²³⁴⁵⁶⁷⁸⁹⁰
        }

        private void btnIRRGenerateSignal_Click(object sender, EventArgs e)
        {            
            IIRFilterPopupInput form = new IIRFilterPopupInput();
            form.ShowDialog();           
        }

        public void FillFromPopupIIR(int Count, int Min, int Max, bool IntegerChecked, string Distribution)
        {
            Random Rand = new Random();
            IIRFilterInputCountInstance.Text = Count.ToString();
            for (int i = 0; i < Count; i++)
            {
                if (Distribution.ToLower().Equals("equal"))
                    if (IntegerChecked)
                    {
                        int random = Rand.Next(Min, Max + 1);
                        this.IIRFilterInputPanel.Controls[i].Text = random.ToString();
                    }
                    else
                    {
                        double random = Rand.NextDouble() * (Max - Min) + Min;
                        this.IIRFilterInputPanel.Controls[i].Text = random.ToString();

                    }
                else if (Distribution.ToLower().Equals("normal"))
                {

                    double randNormal = NextGaussian(Min, Max, Rand);

                    if (IntegerChecked)
                    {
                        randNormal = Math.Round(randNormal);
                    }


                    this.IIRFilterInputPanel.Controls[i].Text = randNormal.ToString();
                }
            }
        }

        private void IIRFilterInputCountInstance_TextChanged(object sender, EventArgs e)
        {
            int numberOfCoeficients = 0;
            if (Int32.TryParse(IIRFilterInputCountInstance.Text, out numberOfCoeficients) && numberOfCoeficients>=0)
            {
                int count = this.IIRFilterInputPanel.Controls.Count;

                if (count < numberOfCoeficients)
                {
                    while (count < numberOfCoeficients)
                    {
                        TextBox inputtextBox = new TextBox();
                        this.IIRFilterInputPanel.Controls.Add(inputtextBox);



                        TextBox outputtextBox = new TextBox();
                        this.IIRFilterOutputPanel.Controls.Add(outputtextBox);
                        count++;
                    }
                }
                if (count > numberOfCoeficients)
                {
                    while (count > numberOfCoeficients)
                    {
                        this.IIRFilterInputPanel.Controls[count - 1].Dispose();

                        this.IIRFilterOutputPanel.Controls[count - 1].Dispose();
                        count--;
                    }

                }
            }
        }

        private void ProcesSignalIIR_Click(object sender, EventArgs e)
        {
            IIRChartInputs.Series[0].Points.Clear();
            IIRChartOutputs.Series[0].Points.Clear();

            
            Int32.TryParse(IIRFilterInputCountInstance.Text, out int numberOfInputs);

            if (numberOfInputs > 0 && numberOfInputs >= 0)
            {
                IIRChartInputs.ChartAreas[0].AxisX.Minimum = 0;
                IIRChartInputs.ChartAreas[0].AxisX.Maximum = numberOfInputs;
                IIRChartInputs.ChartAreas[0].AxisX.Crossing = 0;
                IIRChartInputs.ChartAreas[0].AxisY.Crossing = 0;

                IIRChartOutputs.ChartAreas[0].AxisX.Minimum = 0;
                IIRChartOutputs.ChartAreas[0].AxisX.Maximum = numberOfInputs;
                IIRChartOutputs.ChartAreas[0].AxisX.Crossing = 0;
                IIRChartOutputs.ChartAreas[0].AxisY.Crossing = 0;



                List<double> InputList = new List<double>();
                List<double> OutputList = new List<double>();
                List<double> WeightsBList = new List<double>();
                List<double> WeightsAList = new List<double>();

                for (int i = 0; i < numberOfInputs; i++)
                {
                    double value = 0;
                    try
                    {
                        value = Convert.ToDouble(this.IIRFilterInputPanel.Controls[i].Text);
                    }
                    catch
                    {
                        this.IIRFilterInputPanel.Controls[i].Text = "0";
                    }
                    InputList.Add(value);
                    IIRChartInputs.Series[0].Points.AddXY(i, value);
                }

                Int32.TryParse(txtBoxBWeightsCount.Text, out int FilterOrder);
                if (FilterOrder < 0)
                {
                    return;
                }
                //B and A WEIGHTS LOADING
                for (int i = 0; i < FilterOrder; i++)
                {
                    double value = 0;
                    try
                    {
                        value = Convert.ToDouble(this.IRRFilterBWeights.Controls[i].Text);
                    }
                    catch
                    {
                        this.IRRFilterBWeights.Controls[i].Text = "0";
                    }
                    WeightsBList.Add(value);
                }


                Int32.TryParse(txtBoxAWeightsCount.Text, out FilterOrder);
                if (FilterOrder < 0)
                {
                    return;
                }
                for (int i = 0; i < FilterOrder; i++)
                {
                    double value = 0;
                    try
                    {
                        value = Convert.ToDouble(this.IRRFilterAWeights.Controls[i].Text);
                    }
                    catch
                    {
                        this.IRRFilterAWeights.Controls[i].Text = "0";
                    }
                    WeightsAList.Add(value);
                }

                //B and A WEIGHTS LOADING END




               
                for (int i = 0; i < numberOfInputs; i++)
                {
                    double value = CalculateOutputSignalOnIndexIIR(InputList, WeightsBList,WeightsAList, OutputList, FilterOrder - 1, i);
                    OutputList.Add(value);
                    IIRChartOutputs.Series[0].Points.AddXY(i, value);
                }


                for (int i = 0; i < numberOfInputs; i++)
                {
                    this.IIRFilterOutputPanel.Controls[i].Text = OutputList[i].ToString();
                }
            }
        }

        private void txtBoxBWeightsCount_TextChanged(object sender, EventArgs e)
        {
            int numberOfCoeficients = 0;
            if (Int32.TryParse(txtBoxBWeightsCount.Text, out numberOfCoeficients) && numberOfCoeficients>=0)
            {
                int count = this.IRRFilterBWeights.Controls.Count;

                if (count < numberOfCoeficients)
                {
                    while (count < numberOfCoeficients)
                    {
                        TextBox inputtextBox = new TextBox();
                        this.IRRFilterBWeights.Controls.Add(inputtextBox);

                        count++;
                    }
                }
                if (count > numberOfCoeficients)
                {
                    while (count > numberOfCoeficients)
                    {
                        this.IRRFilterBWeights.Controls[count - 1].Dispose();
                        count--;
                    }

                }
            }
        }

        private void txtBoxAWeightsCount_TextChanged(object sender, EventArgs e)
        {
            int numberOfCoeficients = 0;
            if (Int32.TryParse(txtBoxAWeightsCount.Text, out numberOfCoeficients) && numberOfCoeficients >= 0)
            {
                int count = this.IRRFilterAWeights.Controls.Count;

                if (count < numberOfCoeficients)
                {
                    while (count < numberOfCoeficients)
                    {
                        TextBox inputtextBox = new TextBox();
                        this.IRRFilterAWeights.Controls.Add(inputtextBox);

                        count++;
                    }
                }
                if (count > numberOfCoeficients)
                {
                    while (count > numberOfCoeficients)
                    {
                        this.IRRFilterAWeights.Controls[count - 1].Dispose();
                        count--;
                    }

                }
            }
        }
        double CalculateOutputSignalOnIndexIIR(List<double> InputList, List<double> WeightsBList, List<double> WeightsAList, List<double> OutputList, int filterOrder, int index)
        {
            double result = 0;
            for (int i = 0; i <= filterOrder; i++)
            {
                if (index - i < 0)
                { }
                else
                {
                    result += WeightsBList[filterOrder-i] * InputList[index - i];

                    if (OutputList.Count() > index - i)
                    {                       
                            result += -WeightsAList[i] * OutputList[index - i];
                    }

                }

            }
            return result;

        }

        public void FillWithDataIIR(string[] data)
        {
            IIRFilterInputCountInstance.Text = data.Length.ToString();
            for (int i = 0; i < data.Length; i++)
            {
                this.IIRFilterInputPanel.Controls[i].Text = data[i].ToString();
            }

        }
    }
}
