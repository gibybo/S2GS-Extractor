using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s2gsAutomationForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serverBox.Items.AddRange(Server.Servers);
            serverBox.SelectedIndex = 0;
        }

        private void extractBtn_Click(object sender, EventArgs e)
        {
            if (!matchHistoryChkBox.Checked)
                SC2Macros.openMatchHistory(nameTxt.Text, codeTxt.Text);

            SetAll(false);
            SetStatusText("Extracting...");
            S2GSExtractor extractor = new S2GSExtractor((Server)serverBox.SelectedItem);
            extractor.AsyncExtract(new Action<HashSet<string>>(ExtractDone));
        }

        /// <summary>
        /// Runs when the extraction is done
        /// </summary>
        /// <param name="hashes">The set of extracted hashes</param>
        void ExtractDone(HashSet<string> hashes)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<HashSet<string>>(ExtractDone), hashes);
                return;
            }

            Server server = (Server)serverBox.SelectedItem;
            StringBuilder results = new StringBuilder();
            foreach (String hash in hashes)
            {
                if (!hashesChkBox.Checked)
                    results.AppendLine(server.Url.Replace("{hash}", hash));
                else
                    results.AppendLine(hash);
            }

            resultsTxt.Text = results.ToString();
            urlStatusLabel.Text = string.Format("s2gs URLs ({0} found)", hashes.Count);
            string tmp = Path.Combine(Path.GetTempPath(), "s2gs_list.txt");
            File.WriteAllText(tmp, results.ToString());
            tmpFileLink.Text = tmp;
            tmpFileLink.Visible = true;
            tmpFileLink.Left = urlStatusLabel.Right + 3;

            SetStatusText("Done");

            if (AskDownload(hashes.Count))
            {
                string folder = "";
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.SelectedPath = folder;
                fbd.ShowNewFolderButton = true;
                fbd.Description = "Select folder to save s2gs files to";

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    HashSet<string> urls = new HashSet<string>();
                    urls.UnionWith(from h in hashes select server.Url.Replace("{hash}", h));
                    Download(urls, fbd.SelectedPath);
                }
            }
            else
                SetAll(true);
        }

        /// <summary>
        /// Downloads a set of files to a folder. Will run async.
        /// </summary>
        /// <param name="urls">Files to download</param>
        /// <param name="path">Path to download to</param>
        void Download(HashSet<string> urls, string path)
        {
            if (Thread.CurrentThread.Name != "DownloadThread")
            {
                Thread t = new Thread(new ThreadStart(delegate() {
                    Download(urls, path);
                }));
                t.Name = "DownloadThread";
                t.Start();
                return;
            }
            SetStatusText("Downloading...");
            int success = 0;
            int current = 0;
            foreach (string url in urls)
            {
                SetStatusText(string.Format("Downloading {0} of {1}", current+1, urls.Count));
                if (DownloadSingle(url, path))
                    success++;
                SetProgress(true, 0, urls.Count, current++);
            }
            DownloadDone(success, urls.Count);
        }
        /// <summary>
        /// Downloads a single file to a path
        /// </summary>
        /// <param name="url">File to download</param>
        /// <param name="path">Path to download to</param>
        /// <returns>true if successful, false otherwise</returns>
        bool DownloadSingle(string url, string path)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                string fname = Path.Combine(path, url.Split('/').Last<string>());
                FileStream fs = File.Open(fname, FileMode.OpenOrCreate);

                resp.GetResponseStream().CopyTo(fs);

                resp.Close();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[DOWNLOAD] Error: {0}", e);
                return false;
            }
        }
        /// <summary>
        /// Run when the download of files is done
        /// </summary>
        /// <param name="success">Number of successful downloads</param>
        /// <param name="total">Total number of downloads</param>
        void DownloadDone(int success, int total)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, int>(DownloadDone), success, total);
                return;
            }

            MessageBox.Show(string.Format("Successfully downloaded {0} of {1} files", success, total));
            SetStatusText("Done");
            SetProgress(false, 0, 0, 0);
            SetAll(true);
        }

        /// <summary>
        /// Displays a message asking if the user wants to download files
        /// </summary>
        /// <param name="count">Number of files</param>
        /// <returns>true if the user wants to download the files</returns>
        bool AskDownload(int count)
        {
            return MessageBox.Show(string.Format("{0} hashes found. Would you like to download them?", count), "Download", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes;
        }
        
        /// <summary>
        /// Enables or disables all input fields
        /// </summary>
        /// <param name="enabled">Enabled if true, disabled if false</param>
        void SetAll(bool enabled)
        {
            extractBtn.Enabled = 
                matchHistoryChkBox.Enabled = 
                serverBox.Enabled = 
                hashesChkBox.Enabled = enabled;
        }

        /// <summary>
        /// Set the progressbar to a value (can be called from other threads than the main)
        /// </summary>
        /// <param name="visible">Show the progress bar</param>
        /// <param name="min">Progress bar minimum value (ignored if visible is false)</param>
        /// <param name="max">Progress bar maximum value (ignored if visible is false)</param>
        /// <param name="value">Progress bar value (ignored if visible is false)</param>
        void SetProgress(bool visible, int min, int max, int value)
        {
            if (InvokeRequired)
                Invoke(new Action<bool, int, int, int>(SetProgress), visible, min, max, value);
            else
            {
                if (progressBar.Visible = visible)
                {
                    progressBar.Minimum = min;
                    progressBar.Maximum = max;
                    progressBar.Value = value;
                }
            }
        }
        /// <summary>
        /// Sets the status text (can be called from other threads than the main)
        /// </summary>
        /// <param name="text">Text to set the label to</param>
        void SetStatusText(string text)
        {
            if (InvokeRequired)
                Invoke(new Action<string>(SetStatusText), text);
            else
                statusLbl.Text = text;
        }

        private void matchHistoryChkBox_CheckedChanged(object sender, EventArgs e)
        {
            nameTxt.Enabled = codeTxt.Enabled = !matchHistoryChkBox.Checked;
        }

        private void tmpFileLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(tmpFileLink.Text);
        }
    }
}
