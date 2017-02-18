using MassPKXextractor.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Windows.Forms;

namespace MassPKXextractor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Settings.Default.InputFolder))
            {
                TB_Input.Text = Settings.Default.InputFolder;
            }
            else
            {
                TB_Input.Text = Application.StartupPath;
            }
            if (Directory.Exists(Settings.Default.OutputFolder))
            {
                TB_Output.Text = Settings.Default.OutputFolder;
            }
            else
            {
                TB_Output.Text = Application.StartupPath;
            }
        }

        private void Btn_Input_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TB_Input.Text = dialog.FileName;
                Settings.Default.InputFolder = dialog.FileName;
                Settings.Default.Save();
            }
        }

        private void Btn_Output_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TB_Output.Text = dialog.FileName;
                Settings.Default.OutputFolder = dialog.FileName;
                Settings.Default.Save();
            }
        }
    }
}
