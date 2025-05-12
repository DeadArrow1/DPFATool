using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FATool
{
    public partial class FIRFilterPopupInput : Form
    {
        public FIRFilterPopupInput()
        {
            InitializeComponent();

            cbBoxDistribution.DataSource = new ComboItemDistribution[]
            {
                new ComboItemDistribution{ DistributionName = "Equal"},
                new ComboItemDistribution{ DistributionName = "Normal"}

            };
        }
        class ComboItemDistribution
        {
            public string DistributionName { get; set; }
        }
        private void BtnGenerateSignal_Click(object sender, EventArgs e)
        {
            bool IntegerChecked = CbOnlyIntegers.Checked;
            Int32.TryParse(txtboxCount.Text, out int Count);
            Int32.TryParse(txtboxMin.Text, out int Min);
            Int32.TryParse(txtboxMax.Text, out int Max);

            Form1.instance.FillFromPopup(Count, Min, Max, IntegerChecked, cbBoxDistribution.SelectedValue.ToString());
        }

        private void btnInsertFromFileFIR_Click(object sender, EventArgs e)
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


                    Form1.instance.FillWithDataFIR(values);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
