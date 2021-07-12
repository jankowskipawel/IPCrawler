using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;

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
            if (lines.Length > 1500)
            {
                MessageBox.Show(
                    "WARNING! Selected file has more than 1500 records. Only first 1500 IPs will be checked.");
            }
            string[] ipArray = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                ipArray[i] = lines[i].Split('-').First().Trim();
            }
            //count hits
            Dictionary<string, int> numberOfHits = CountHits(ipArray);
            List<IPData> IPObjects = ConvertIPStringsToObjects(ipArray.Distinct().ToArray());
            //generate csv
            SaveToCsv(IPObjects, numberOfHits);
        }

        public Dictionary<string, int> CountHits(string[] ipArray)
        {
            Dictionary<string, int> numberOfHits = new Dictionary<string, int>();
            foreach (var ip in ipArray)
            {
                if (numberOfHits.ContainsKey(ip))
                {
                    numberOfHits[ip]++;
                }
                else
                {
                    numberOfHits.Add(ip, 1);
                }
            }
            return numberOfHits;
        }

        public void SaveToCsv(List<IPData> IPObjects, Dictionary<string, int> numberOfHits)
        {
            DateTime date = DateTime.Now;
            using (var writer = new StreamWriter(outputFolderPathTextBox.Text + $"\\{date.ToString("yyyyMMdd_hhmmss")}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<CsvIPDataModel>();
                csv.NextRecord();
                foreach (var ip in IPObjects)
                {
                    string geolocation = $"{ip.country}, {ip.regionName}, {ip.city}, {ip.zip}, {ip.lat} {ip.lon}";
                    CsvIPDataModel record = new CsvIPDataModel(ip.query, geolocation, ip.isp, numberOfHits[ip.query]);
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }
            }
        }

        public List<IPData> ConvertIPStringsToObjects(string[] ipArray)
        {
            //split array every 100 elements (because api limits)
            var ipArrays = SplitArrayEachNElements(ipArray, 100);
            //get ip data
            List<string> jsonIPData = new List<string>(ipArray.Length);
            foreach (var ipArr in ipArrays)
            {
                string apiString = CreateApiString(ipArr);
                jsonIPData = GetIPDataFromAPI(apiString, jsonIPData);
            }

            List<IPData> ipData = new List<IPData>(ipArray.Length);
            foreach (var jsonIP in jsonIPData)
            {
                IPData ipd = JsonSerializer.Deserialize<IPData>(jsonIP);
                ipData.Add(ipd);
            }
            return ipData;
        }

        public string[][] SplitArrayEachNElements(string[] array, int n)
        {
            int i = 0;
            var query = from s in array
                        let num = i++
                group s by num / n into g
                select g.ToArray();
            return query.ToArray();
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

        public List<string> GetIPDataFromAPI(string apiString, List<string> ipDataList)
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

}
