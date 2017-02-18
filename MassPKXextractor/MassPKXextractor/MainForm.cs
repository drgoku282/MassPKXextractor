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
                string FolderIn = TB_Input.Text;
                string FolderOut = TB_Output.Text;
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
                IEnumerable<string> SaveList;
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

                // All files 
                foreach (var file in SaveList)
                {
                    string result;
                    string finalpath;
                    SaveFile SAV = SaveUtil.getVariantSAV(File.ReadAllBytes(file));
                    if (CB_File.Checked)
                    {
                        finalpath = getNextFolderName(FolderOut, SAV);
                    }
                    else
                    {
                        finalpath = FolderOut;
                    }
                    Directory.CreateDirectory(finalpath); // Make sure out directory exists
                    SAVUtil.dumpBoxes(SAV, finalpath, out result, CB_Box.Checked);
                }

                MessageBox.Show(SaveList.Count().ToString() + " save files processed correctly", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A error has ocurred:\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getNextFolderName(string parent, SaveFile SAV)
        {
            // Common variables
            int index = 0;
            string savename = $"{SAV.OT} ({SAV.Version})";
            // Check if simple folder name is available
            string current = Path.Combine(parent, savename);
            while (Directory.Exists(current))
            {
                index++;
                current = Path.Combine(parent, savename + " (" + index + ")");
            }
            return current;
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
    }
}
