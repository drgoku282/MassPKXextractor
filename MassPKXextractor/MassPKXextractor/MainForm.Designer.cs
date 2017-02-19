namespace MassPKXextractor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TB_Input = new System.Windows.Forms.TextBox();
            this.TB_Output = new System.Windows.Forms.TextBox();
            this.lb_input = new System.Windows.Forms.Label();
            this.lb_output = new System.Windows.Forms.Label();
            this.Btn_Input = new System.Windows.Forms.Button();
            this.Btn_Output = new System.Windows.Forms.Button();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.CB_Recursive = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CB_Box = new System.Windows.Forms.CheckBox();
            this.CB_File = new System.Windows.Forms.CheckBox();
            this.Worker = new System.ComponentModel.BackgroundWorker();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // TB_Input
            // 
            this.TB_Input.Location = new System.Drawing.Point(60, 12);
            this.TB_Input.Name = "TB_Input";
            this.TB_Input.Size = new System.Drawing.Size(250, 20);
            this.TB_Input.TabIndex = 0;
            // 
            // TB_Output
            // 
            this.TB_Output.Location = new System.Drawing.Point(60, 41);
            this.TB_Output.Name = "TB_Output";
            this.TB_Output.Size = new System.Drawing.Size(250, 20);
            this.TB_Output.TabIndex = 1;
            // 
            // lb_input
            // 
            this.lb_input.AutoSize = true;
            this.lb_input.Location = new System.Drawing.Point(20, 15);
            this.lb_input.Name = "lb_input";
            this.lb_input.Size = new System.Drawing.Size(34, 13);
            this.lb_input.TabIndex = 2;
            this.lb_input.Text = "Input:";
            // 
            // lb_output
            // 
            this.lb_output.AutoSize = true;
            this.lb_output.Location = new System.Drawing.Point(12, 44);
            this.lb_output.Name = "lb_output";
            this.lb_output.Size = new System.Drawing.Size(42, 13);
            this.lb_output.TabIndex = 2;
            this.lb_output.Text = "Output:";
            // 
            // Btn_Input
            // 
            this.Btn_Input.Location = new System.Drawing.Point(316, 10);
            this.Btn_Input.Name = "Btn_Input";
            this.Btn_Input.Size = new System.Drawing.Size(75, 23);
            this.Btn_Input.TabIndex = 3;
            this.Btn_Input.Text = "Browse...";
            this.Btn_Input.UseVisualStyleBackColor = true;
            this.Btn_Input.Click += new System.EventHandler(this.Btn_Input_Click);
            // 
            // Btn_Output
            // 
            this.Btn_Output.Location = new System.Drawing.Point(316, 39);
            this.Btn_Output.Name = "Btn_Output";
            this.Btn_Output.Size = new System.Drawing.Size(75, 23);
            this.Btn_Output.TabIndex = 3;
            this.Btn_Output.Text = "Browse...";
            this.Btn_Output.UseVisualStyleBackColor = true;
            this.Btn_Output.Click += new System.EventHandler(this.Btn_Output_Click);
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(316, 68);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(75, 23);
            this.Btn_Start.TabIndex = 4;
            this.Btn_Start.Text = "Start";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // CB_Recursive
            // 
            this.CB_Recursive.AutoSize = true;
            this.CB_Recursive.Location = new System.Drawing.Point(15, 72);
            this.CB_Recursive.Name = "CB_Recursive";
            this.CB_Recursive.Size = new System.Drawing.Size(74, 17);
            this.CB_Recursive.TabIndex = 5;
            this.CB_Recursive.Text = "Recursive";
            this.toolTip1.SetToolTip(this.CB_Recursive, "When checked, it will search for Save Files in every\r\nsub-folder of the Input fol" +
        "der.");
            this.CB_Recursive.UseVisualStyleBackColor = true;
            // 
            // CB_Box
            // 
            this.CB_Box.AutoSize = true;
            this.CB_Box.Location = new System.Drawing.Point(209, 72);
            this.CB_Box.Name = "CB_Box";
            this.CB_Box.Size = new System.Drawing.Size(101, 17);
            this.CB_Box.TabIndex = 9;
            this.CB_Box.Text = "Separate Boxes";
            this.toolTip1.SetToolTip(this.CB_Box, "When checked, it will create an additional folder for\r\neach Save File and then on" +
        "e folder for each box.\r\n");
            this.CB_Box.UseVisualStyleBackColor = true;
            this.CB_Box.CheckedChanged += new System.EventHandler(this.CB_Box_CheckedChanged);
            // 
            // CB_File
            // 
            this.CB_File.AutoSize = true;
            this.CB_File.Location = new System.Drawing.Point(102, 72);
            this.CB_File.Name = "CB_File";
            this.CB_File.Size = new System.Drawing.Size(93, 17);
            this.CB_File.TabIndex = 9;
            this.CB_File.Text = "Separate Files";
            this.toolTip1.SetToolTip(this.CB_File, "When checked, it will create an additional folder for\r\neach Save File in the Outp" +
        "ut folder.");
            this.CB_File.UseVisualStyleBackColor = true;
            this.CB_File.CheckedChanged += new System.EventHandler(this.CB_File_CheckedChanged);
            // 
            // Worker
            // 
            this.Worker.WorkerReportsProgress = true;
            this.Worker.WorkerSupportsCancellation = true;
            this.Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.Worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            this.Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_RunWorkerCompleted);
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.Enabled = false;
            this.Btn_Stop.Location = new System.Drawing.Point(316, 97);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.Btn_Stop.TabIndex = 4;
            this.Btn_Stop.Text = "Stop";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 97);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(295, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(404, 132);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CB_File);
            this.Controls.Add(this.CB_Box);
            this.Controls.Add(this.CB_Recursive);
            this.Controls.Add(this.Btn_Stop);
            this.Controls.Add(this.Btn_Start);
            this.Controls.Add(this.Btn_Output);
            this.Controls.Add(this.Btn_Input);
            this.Controls.Add(this.lb_output);
            this.Controls.Add(this.lb_input);
            this.Controls.Add(this.TB_Output);
            this.Controls.Add(this.TB_Input);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 6, 6);
            this.Text = "Mass PKX Extractor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Input;
        private System.Windows.Forms.TextBox TB_Output;
        private System.Windows.Forms.Label lb_input;
        private System.Windows.Forms.Label lb_output;
        private System.Windows.Forms.Button Btn_Input;
        private System.Windows.Forms.Button Btn_Output;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.CheckBox CB_Recursive;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox CB_Box;
        private System.Windows.Forms.CheckBox CB_File;
        private System.ComponentModel.BackgroundWorker Worker;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

