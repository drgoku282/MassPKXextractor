using MassPKXextractor.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MassPKXextractor
{
    public partial class MainForm : Form
    {
        string FolderIn;
        string FolderOut;
        int totalsaves;
        int currentsave;
        IEnumerable<string> SaveList;

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

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if input and output folders exist
                FolderIn = TB_Input.Text;
                FolderOut = TB_Output.Text;
                if (!Directory.Exists(FolderIn))
                {
                    MessageBox.Show("The input folder is invalid or does not exist.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!Directory.Exists(TB_Output.Text))
                {
                    MessageBox.Show("The output folder is invalid or does not exist.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Search for save files
                SaveList = null;
                if (!SAVUtil.getSavesFromFolder(FolderIn, CB_Recursive.Checked, out SaveList))
                {
                    MessageBox.Show("A error has ocurred:\r\n\r\n" + SaveList.First(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (SaveList == null)
                {
                    MessageBox.Show("The input folder doesn't seem to have any save files.", "No save files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                totalsaves = SaveList.Count();
                progressBar1.Value = 0;
                Btn_Stop.Enabled = true;
                Btn_Start.Enabled = false;

                if (Worker.IsBusy != true)
                {
                    Worker.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getFolderName(string parent, SaveFile SAV)
        {
            string foldername = Path.GetFileNameWithoutExtension(Util.CleanFileName(SAV.BAKName));
            return Path.Combine(parent, foldername.Substring(foldername.IndexOf('[') + 1, foldername.Length - 3));
    }

        private void CB_Box_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_Box.Checked)
            {
                CB_File.Checked = true;
            }
        }

        private void CB_File_CheckedChanged(object sender, EventArgs e)
        {
            if (!CB_File.Checked)
            {
                CB_Box.Checked = false;
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            Worker.CancelAsync();
        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Process all save files
            currentsave = 0;
            foreach (var file in SaveList)
            {
                if (Worker.CancellationPending)
                {
                    break;
                }
                string result;
                string finalpath;
                SaveFile SAV = SaveUtil.getVariantSAV(File.ReadAllBytes(file));
                if (CB_File.Checked)
                {
                    finalpath = getFolderName(FolderOut, SAV);
                }
                else
                {
                    finalpath = FolderOut;
                }
                Directory.CreateDirectory(finalpath); // Make sure out directory exists
                SAVUtil.dumpBoxes(SAV, finalpath, out result, CB_Box.Checked);
                currentsave++;
                Worker.ReportProgress(currentsave * 100 / totalsaves);
            }
        }

        private void Worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(currentsave + " save files processed correctly", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Btn_Stop.Enabled = false;
            Btn_Start.Enabled = true;
        }
    }
}
