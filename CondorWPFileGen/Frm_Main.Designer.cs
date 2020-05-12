namespace CondorWPFileGen
{
    partial class frm_Main
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
            this.tb_Output = new System.Windows.Forms.TextBox();
            this.tb_APTFilename = new System.Windows.Forms.TextBox();
            this.btn_BrowseApt = new System.Windows.Forms.Button();
            this.btn_SaveApt = new System.Windows.Forms.Button();
            this.tb_CondorCupFilename = new System.Windows.Forms.TextBox();
            this.btn_BrowseCondorCup = new System.Windows.Forms.Button();
            this.lbl_AptFile = new System.Windows.Forms.Label();
            this.lbl_InputCup = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_GenCombOutput = new System.Windows.Forms.Button();
            this.lbl_About = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_Output
            // 
            this.tb_Output.Location = new System.Drawing.Point(12, 212);
            this.tb_Output.Margin = new System.Windows.Forms.Padding(4);
            this.tb_Output.Multiline = true;
            this.tb_Output.Name = "tb_Output";
            this.tb_Output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Output.Size = new System.Drawing.Size(867, 458);
            this.tb_Output.TabIndex = 1;
            // 
            // tb_APTFilename
            // 
            this.tb_APTFilename.Location = new System.Drawing.Point(123, 60);
            this.tb_APTFilename.Margin = new System.Windows.Forms.Padding(4);
            this.tb_APTFilename.Name = "tb_APTFilename";
            this.tb_APTFilename.Size = new System.Drawing.Size(507, 22);
            this.tb_APTFilename.TabIndex = 2;
            // 
            // btn_BrowseApt
            // 
            this.btn_BrowseApt.Location = new System.Drawing.Point(643, 59);
            this.btn_BrowseApt.Margin = new System.Windows.Forms.Padding(4);
            this.btn_BrowseApt.Name = "btn_BrowseApt";
            this.btn_BrowseApt.Size = new System.Drawing.Size(85, 28);
            this.btn_BrowseApt.TabIndex = 3;
            this.btn_BrowseApt.Text = "Browse...";
            this.btn_BrowseApt.UseVisualStyleBackColor = true;
            this.btn_BrowseApt.Click += new System.EventHandler(this.btn_BrowseApt_Click);
            // 
            // btn_SaveApt
            // 
            this.btn_SaveApt.Location = new System.Drawing.Point(737, 59);
            this.btn_SaveApt.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SaveApt.Name = "btn_SaveApt";
            this.btn_SaveApt.Size = new System.Drawing.Size(143, 28);
            this.btn_SaveApt.TabIndex = 3;
            this.btn_SaveApt.Text = "Save As Cup File...";
            this.btn_SaveApt.UseVisualStyleBackColor = true;
            this.btn_SaveApt.Click += new System.EventHandler(this.btn_SaveApt_Click);
            // 
            // tb_CondorCupFilename
            // 
            this.tb_CondorCupFilename.Location = new System.Drawing.Point(123, 103);
            this.tb_CondorCupFilename.Margin = new System.Windows.Forms.Padding(4);
            this.tb_CondorCupFilename.Name = "tb_CondorCupFilename";
            this.tb_CondorCupFilename.Size = new System.Drawing.Size(507, 22);
            this.tb_CondorCupFilename.TabIndex = 2;
            // 
            // btn_BrowseCondorCup
            // 
            this.btn_BrowseCondorCup.Location = new System.Drawing.Point(643, 101);
            this.btn_BrowseCondorCup.Margin = new System.Windows.Forms.Padding(4);
            this.btn_BrowseCondorCup.Name = "btn_BrowseCondorCup";
            this.btn_BrowseCondorCup.Size = new System.Drawing.Size(85, 28);
            this.btn_BrowseCondorCup.TabIndex = 3;
            this.btn_BrowseCondorCup.Text = "Browse...";
            this.btn_BrowseCondorCup.UseVisualStyleBackColor = true;
            this.btn_BrowseCondorCup.Click += new System.EventHandler(this.btn_BrowseCondorCup_Click);
            // 
            // lbl_AptFile
            // 
            this.lbl_AptFile.AutoSize = true;
            this.lbl_AptFile.Location = new System.Drawing.Point(1, 65);
            this.lbl_AptFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AptFile.Name = "lbl_AptFile";
            this.lbl_AptFile.Size = new System.Drawing.Size(111, 17);
            this.lbl_AptFile.TabIndex = 4;
            this.lbl_AptFile.Text = "Condor APT File";
            // 
            // lbl_InputCup
            // 
            this.lbl_InputCup.AutoSize = true;
            this.lbl_InputCup.Location = new System.Drawing.Point(1, 108);
            this.lbl_InputCup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_InputCup.Name = "lbl_InputCup";
            this.lbl_InputCup.Size = new System.Drawing.Size(112, 17);
            this.lbl_InputCup.TabIndex = 4;
            this.lbl_InputCup.Text = "Condor CUP File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Console";
            // 
            // btn_GenCombOutput
            // 
            this.btn_GenCombOutput.Location = new System.Drawing.Point(243, 154);
            this.btn_GenCombOutput.Margin = new System.Windows.Forms.Padding(4);
            this.btn_GenCombOutput.Name = "btn_GenCombOutput";
            this.btn_GenCombOutput.Size = new System.Drawing.Size(275, 25);
            this.btn_GenCombOutput.TabIndex = 5;
            this.btn_GenCombOutput.Text = "Create Combined CUP File...";
            this.btn_GenCombOutput.UseVisualStyleBackColor = true;
            this.btn_GenCombOutput.Click += new System.EventHandler(this.btn_GenCombOutput_Click);
            // 
            // lbl_About
            // 
            this.lbl_About.AutoSize = true;
            this.lbl_About.Location = new System.Drawing.Point(193, 11);
            this.lbl_About.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_About.Name = "lbl_About";
            this.lbl_About.Size = new System.Drawing.Size(396, 17);
            this.lbl_About.TabIndex = 6;
            this.lbl_About.Text = "Version 1.0.7 Revised May 12 2020 by G. Frank Paynter (TA)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 678);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Clear Console";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_Main
            // 
            this.AccessibleName = "frm_Main";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 716);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_About);
            this.Controls.Add(this.btn_GenCombOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_InputCup);
            this.Controls.Add(this.lbl_AptFile);
            this.Controls.Add(this.btn_SaveApt);
            this.Controls.Add(this.btn_BrowseCondorCup);
            this.Controls.Add(this.btn_BrowseApt);
            this.Controls.Add(this.tb_CondorCupFilename);
            this.Controls.Add(this.tb_APTFilename);
            this.Controls.Add(this.tb_Output);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_Main";
            this.Text = "Condor APT to CUP Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Output;
        private System.Windows.Forms.TextBox tb_APTFilename;
        private System.Windows.Forms.Button btn_BrowseApt;
        private System.Windows.Forms.Button btn_SaveApt;
        private System.Windows.Forms.TextBox tb_CondorCupFilename;
        private System.Windows.Forms.Button btn_BrowseCondorCup;
        private System.Windows.Forms.Label lbl_AptFile;
        private System.Windows.Forms.Label lbl_InputCup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_GenCombOutput;
        private System.Windows.Forms.Label lbl_About;
        private System.Windows.Forms.Button button1;
    }
}

