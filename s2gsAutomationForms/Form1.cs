using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

            S2GSExtractor extractor = new S2GSExtractor((Server)serverBox.SelectedItem);
            extractor.AsyncExtract(new Action<HashSet<string>>(ExtractDone));
        }

        void ExtractDone(HashSet<string> hashes)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<HashSet<string>>(ExtractDone), hashes);
                return;
            }
            SetAll(true);

            StringBuilder results = new StringBuilder();
            foreach (String hash in hashes)
            {
                if (!hashesChkBox.Checked)
                    results.AppendLine(((Server)serverBox.SelectedItem).Url.Replace("{hash}", hash));
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
        }

        void SetAll(bool enabled)
        {
            extractBtn.Enabled = 
                matchHistoryChkBox.Enabled = 
                serverBox.Enabled = 
                hashesChkBox.Enabled = enabled;
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
