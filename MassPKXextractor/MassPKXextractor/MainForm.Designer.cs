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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TX_Output = new System.Windows.Forms.TextBox();
            this.TB_Output = new System.Windows.Forms.TextBox();
            this.lb_input = new System.Windows.Forms.Label();
            this.lb_output = new System.Windows.Forms.Label();
            this.Btn_Input = new System.Windows.Forms.Button();
            this.Btn_Output = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TX_Output
            // 
            this.TX_Output.Location = new System.Drawing.Point(60, 12);
            this.TX_Output.Name = "TX_Output";
            this.TX_Output.Size = new System.Drawing.Size(250, 20);
            this.TX_Output.TabIndex = 0;
            // 
            // TB_Output
            // 
            this.TB_Output.Location = new System.Drawing.Point(60, 38);
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
            this.lb_output.Location = new System.Drawing.Point(12, 41);
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
            // 
            // Btn_Output
            // 
            this.Btn_Output.Location = new System.Drawing.Point(316, 36);
            this.Btn_Output.Name = "Btn_Output";
            this.Btn_Output.Size = new System.Drawing.Size(75, 23);
            this.Btn_Output.TabIndex = 3;
            this.Btn_Output.Text = "Browse...";
            this.Btn_Output.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(412, 76);
            this.Controls.Add(this.Btn_Output);
            this.Controls.Add(this.Btn_Input);
            this.Controls.Add(this.lb_output);
            this.Controls.Add(this.lb_input);
            this.Controls.Add(this.TB_Output);
            this.Controls.Add(this.TX_Output);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 6, 6);
            this.Text = "Mass PKX Extractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TX_Output;
        private System.Windows.Forms.TextBox TB_Output;
        private System.Windows.Forms.Label lb_input;
        private System.Windows.Forms.Label lb_output;
        private System.Windows.Forms.Button Btn_Input;
        private System.Windows.Forms.Button Btn_Output;
    }
}

