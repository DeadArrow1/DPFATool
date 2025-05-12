namespace FATool
{
    partial class FIRFilterPopupInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnGenerateSignal = new System.Windows.Forms.Button();
            this.txtboxCount = new System.Windows.Forms.TextBox();
            this.txtboxMin = new System.Windows.Forms.TextBox();
            this.CbOnlyIntegers = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtboxMax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInsertFromFileFIR = new System.Windows.Forms.Button();
            this.cbBoxDistribution = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BtnGenerateSignal
            // 
            this.BtnGenerateSignal.Location = new System.Drawing.Point(39, 161);
            this.BtnGenerateSignal.Name = "BtnGenerateSignal";
            this.BtnGenerateSignal.Size = new System.Drawing.Size(128, 23);
            this.BtnGenerateSignal.TabIndex = 0;
            this.BtnGenerateSignal.Text = "Vygenerovat signál";
            this.BtnGenerateSignal.UseVisualStyleBackColor = true;
            this.BtnGenerateSignal.Click += new System.EventHandler(this.BtnGenerateSignal_Click);
            // 
            // txtboxCount
            // 
            this.txtboxCount.Location = new System.Drawing.Point(80, 38);
            this.txtboxCount.Name = "txtboxCount";
            this.txtboxCount.Size = new System.Drawing.Size(100, 20);
            this.txtboxCount.TabIndex = 1;
            // 
            // txtboxMin
            // 
            this.txtboxMin.Location = new System.Drawing.Point(80, 64);
            this.txtboxMin.Name = "txtboxMin";
            this.txtboxMin.Size = new System.Drawing.Size(100, 20);
            this.txtboxMin.TabIndex = 2;
            // 
            // CbOnlyIntegers
            // 
            this.CbOnlyIntegers.AutoSize = true;
            this.CbOnlyIntegers.Location = new System.Drawing.Point(80, 110);
            this.CbOnlyIntegers.Name = "CbOnlyIntegers";
            this.CbOnlyIntegers.Size = new System.Drawing.Size(92, 17);
            this.CbOnlyIntegers.TabIndex = 3;
            this.CbOnlyIntegers.Text = "Jen celá čísla";
            this.CbOnlyIntegers.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Min";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Max";
            // 
            // txtboxMax
            // 
            this.txtboxMax.Location = new System.Drawing.Point(80, 84);
            this.txtboxMax.Name = "txtboxMax";
            this.txtboxMax.Size = new System.Drawing.Size(100, 20);
            this.txtboxMax.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Počet hodnot";
            // 
            // btnInsertFromFileFIR
            // 
            this.btnInsertFromFileFIR.Location = new System.Drawing.Point(278, 44);
            this.btnInsertFromFileFIR.Name = "btnInsertFromFileFIR";
            this.btnInsertFromFileFIR.Size = new System.Drawing.Size(75, 44);
            this.btnInsertFromFileFIR.TabIndex = 8;
            this.btnInsertFromFileFIR.Text = "Vložit ze souboru";
            this.btnInsertFromFileFIR.UseVisualStyleBackColor = true;
            this.btnInsertFromFileFIR.Click += new System.EventHandler(this.btnInsertFromFileFIR_Click);
            // 
            // cbBoxDistribution
            // 
            this.cbBoxDistribution.DisplayMember = "DistributionName";
            this.cbBoxDistribution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxDistribution.FormattingEnabled = true;
            this.cbBoxDistribution.Location = new System.Drawing.Point(80, 11);
            this.cbBoxDistribution.Name = "cbBoxDistribution";
            this.cbBoxDistribution.Size = new System.Drawing.Size(100, 21);
            this.cbBoxDistribution.TabIndex = 9;
            this.cbBoxDistribution.ValueMember = "DistributionName";
            // 
            // FIRFilterPopupInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 196);
            this.Controls.Add(this.cbBoxDistribution);
            this.Controls.Add(this.txtboxCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtboxMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnInsertFromFileFIR);
            this.Controls.Add(this.txtboxMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CbOnlyIntegers);
            this.Controls.Add(this.BtnGenerateSignal);
            this.Name = "FIRFilterPopupInput";
            this.Text = "FIRFilterPopupInput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnGenerateSignal;
        private System.Windows.Forms.TextBox txtboxCount;
        private System.Windows.Forms.TextBox txtboxMin;
        private System.Windows.Forms.CheckBox CbOnlyIntegers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtboxMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInsertFromFileFIR;
        private System.Windows.Forms.ComboBox cbBoxDistribution;
    }
}