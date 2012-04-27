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

        }

        private void extractBtn_Click(object sender, EventArgs e)
        {
            SC2Macros.openMatchHistory(nameTxt.Text, codeTxt.Text);

            S2GSExtractor extractor = new S2GSExtractor();
            HashSet<String> hashes = extractor.extract();

            String results = "";
            foreach (String hash in hashes) {
                results += "http://us.depot.battle.net:1119/" + hash + ".s2gs\r\n\r\n";
            }

            resultsTxt.Text = results;
        }
    }
}
