using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            SC2Macros.openMatchHistory(nameTxt.Text, codeTxt.Text);

            S2GSExtractor extractor = new S2GSExtractor((Server)serverBox.SelectedItem);
            HashSet<String> hashes = extractor.extract();

            StringBuilder results = new StringBuilder();
            foreach (String hash in hashes) {
                results.AppendLine(((Server)serverBox.SelectedItem).Url.Replace("{hash}", hash));
            }

            resultsTxt.Text = results.ToString();
        }
    }
}
