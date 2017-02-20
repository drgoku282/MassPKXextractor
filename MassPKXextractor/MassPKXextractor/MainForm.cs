using MassPKXextractor.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MassPKXextractor
{
    public partial class MainForm : Form
    {
        string FolderIn;
        string FolderOut;
        List<string> invalidSaves;
        List<string> validSaves;
        int totalsaves;
        int currentsave;
        IEnumerable<string> SaveList;
        Control[] disableWhenWorking;

        public MainForm()
        {
            InitializeComponent();
            disableWhenWorking = new Control[] { TB_Input, Btn_Input, TB_Output, Btn_Output, CB_Recursive, CB_File, CB_Box, Btn_Start };
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
            CB_Recursive.Checked = Settings.Default.CheckRecursive;
            CB_File.Checked = Settings.Default.CheckFile;
            CB_Box.Checked = Settings.Default.CheckBox;
        }

        private void DisableControls()
        {
            foreach (Control c in disableWhenWorking)
            {
                c.Enabled = false;
            }
            Btn_Stop.Enabled = true;
        }

        private void EnableControls()
        {
            foreach (Control c in disableWhenWorking)
            {
                c.Enabled = true;
            }
            Btn_Stop.Enabled = false;
        }

        private void SaveSettings()
        {
            if (Directory.Exists(TB_Input.Text))
            {
                Settings.Default.InputFolder = TB_Input.Text;
            }
            if (Directory.Exists(TB_Output.Text))
            {
                Settings.Default.OutputFolder = TB_Output.Text;
            }
            Settings.Default.CheckRecursive = CB_Recursive.Checked;
            Settings.Default.CheckFile = CB_File.Checked;
            Settings.Default.CheckBox = CB_Box.Checked;
            Settings.Default.Save();
        }

        private void Btn_Input_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TB_Input.Text = dialog.FileName;
            }
        }

        private void Btn_Output_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TB_Output.Text = dialog.FileName;
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                // Get settings and check folders
                FolderIn = TB_Input.Text;
                FolderOut = TB_Output.Text;
                bool Recursive = CB_Recursive.Checked;
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
                DisableControls();
                SaveSettings();

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
            try
            {
                // Process all save files
                currentsave = 0;
                invalidSaves = new List<string> { };
                validSaves = new List<string> { };
                foreach (var file in SaveList)
                {
                    if (Worker.CancellationPending)
                    {
                        break;
                    }
                    string result;
                    string finalpath;
                    SaveFile SAV = SaveUtil.getVariantSAV(File.ReadAllBytes(file));
                    if (SAV == null)
                    {
                        invalidSaves.Add(file);
                        currentsave++;
                        Worker.ReportProgress(currentsave * 100 / totalsaves);
                        continue;
                    }
                    validSaves.Add(file);
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
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Build log
            var sb = new StringBuilder();
            sb.AppendLine("Start MassPKMextractor Log" + Environment.NewLine);
            sb.AppendLine("Date: " + DateTime.Now);
            sb.AppendLine("Saves processed: " + totalsaves + Environment.NewLine);
            sb.AppendLine("Valid saves:");
            foreach (string sav in validSaves) sb.AppendLine(sav);
            sb.AppendLine(Environment.NewLine + "Invalid saves");
            foreach (string sav in invalidSaves) sb.AppendLine(sav);
            sb.AppendLine(Environment.NewLine + "End MassPKMextractor Log");

            string logpath = Application.StartupPath;
            DialogResult logwrite = DialogResult.Yes;
            while (logwrite != DialogResult.Cancel)
            {
                try
                {
                    File.WriteAllText(Path.Combine(logpath, "MassPKMextractorLog-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"), sb.ToString());
                    logwrite = DialogResult.Cancel;
                }
                catch (Exception ex)
                {
                    logwrite = MessageBox.Show("A error ocurred while saving log:\r\n" + ex.Message + "\r\nDo you want to save it in another location?", "MassPKMextractor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (logwrite == DialogResult.Yes)
                    {
                        var dialog = new CommonOpenFileDialog();
                        dialog.IsFolderPicker = true;
                        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                        {
                            logpath = dialog.FileName;
                        }
                    }
                }
            }

            if (invalidSaves.Count > 0)
            {
                string invalidlist = string.Join("\r\n", invalidSaves.ToArray());
                MessageBox.Show($"{validSaves.Count} save files processed correctly.\r\n{invalidSaves.Count} saves were not processed, see log for details.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(validSaves.Count + " save files processed correctly.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            EnableControls();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Worker.IsBusy)
            {
                DialogResult result = MessageBox.Show("There is a extraction operation in progress, do you want to close the application?", "Close?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveSettings();
                }
                if (result == DialogResult.No)
                {
                    Worker.CancelAsync();
                    e.Cancel = true;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                SaveSettings();
            }
        }
    }
}
