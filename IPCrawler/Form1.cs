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

namespace IPCrawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void inputFilePickerButton_Click(object sender, EventArgs e)
        {
            inputFileDialog.ShowDialog();
            inputFilePathTextBox.Text = inputFileDialog.FileName;
            outputFolderPathTextBox.Text = Path.GetDirectoryName(inputFileDialog.FileName);
        }

        private void outputDirecrotyPickerButton_Click(object sender, EventArgs e)
        {
            outputBrowserDialog.ShowDialog();
            outputFolderPathTextBox.Text = outputBrowserDialog.SelectedPath;
        }

        private void GenerateCSVButton_Click(object sender, EventArgs e)
        {
            //check if paths are valid
            try
            {
                CheckFilePath(inputFilePathTextBox.Text);
                CheckDirectoryPath(outputFolderPathTextBox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
            //read IPs
            string[] lines = System.IO.File.ReadAllLines(inputFilePathTextBox.Text);
            string[] ipArray = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                ipArray[i] = lines[i].Split('-').First().Trim();
            }
            //get ip data
            //generate csv

        }

        public static bool IsPathValid(string filePath)
        {
            HashSet<char> invalidCharacters = new HashSet<char>(Path.GetInvalidPathChars());
            return !string.IsNullOrEmpty(filePath) && !filePath.Any(pc => invalidCharacters.Contains(pc));
        }

        public static void CheckFilePath(string path)
        {
            if (path.Length == 0)
            {
                throw new ArgumentException("File path cannot be empty");
            }

            if (!File.Exists(path) || !IsPathValid(path))
            {
                throw new ArgumentException($"{path} is not a valid directory");
            }
        }

        public static void CheckDirectoryPath(string path)
        {
            if (path.Length == 0)
            {
                throw new ArgumentException("Directory path cannot be empty");
            }

            if (!Directory.Exists(path) || !IsPathValid(path))
            {
                throw new ArgumentException($"{path} is not a valid directory");
            }
        }
    }
}
