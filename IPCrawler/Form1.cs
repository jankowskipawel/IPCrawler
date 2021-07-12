using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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
            int j = 0;
            //split array every 100 elements
            var query = from s in ipArray
                        let num = j++
                group s by num / 100 into g
                select g.ToArray();
            var ipArrays = query.ToArray();
            //get ip data
            List<string> ipData = new List<string>(lines.Length);
            foreach (var ipArr in ipArrays)
            {
                string apiString = CreateApiString(ipArr);
                ipData = GetIPData(apiString, ipData);
            }

            //generate csv

        }

        public string CreateApiString(string[] ipArray)
        {
            string apiString = "[";
            for (int i = 0; i < ipArray.Length; i++)
            {
                apiString += $"\"{ipArray[i]}\",";
            }
            apiString = apiString.TrimEnd(',');
            apiString += "]";
            return apiString;
        }

        public List<string> GetIPData(string apiString, List<string> ipDataList)
        {
            string[] ipData = new string[100];
            using (WebClient wc = new WebClient())
            {
                var result = wc.UploadString("http://ip-api.com/batch?fields=58105", "POST", apiString);
                result = result.TrimStart('[').TrimEnd(']');
                ipData = result.Split('}');
            }
            foreach (var ipSingleData in ipData)
            {
                ipDataList.Add(ipSingleData.TrimStart(',').Trim() + "}");
            }
            return ipDataList.SkipLast(1).ToList();
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



    public class Rootobject
    {
        public string ip { get; set; }
        public string version { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string region_code { get; set; }
        public string country { get; set; }
        public string country_name { get; set; }
        public string country_code { get; set; }
        public string country_code_iso3 { get; set; }
        public string country_capital { get; set; }
        public string country_tld { get; set; }
        public string continent_code { get; set; }
        public bool in_eu { get; set; }
        public string postal { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string timezone { get; set; }
        public string utc_offset { get; set; }
        public string country_calling_code { get; set; }
        public string currency { get; set; }
        public string currency_name { get; set; }
        public string languages { get; set; }
        public float country_area { get; set; }
        public float country_population { get; set; }
        public string message { get; set; }
        public string asn { get; set; }
        public string org { get; set; }
    }


}
