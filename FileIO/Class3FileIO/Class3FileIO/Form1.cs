using Newtonsoft.Json;
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
using System.Xml;

namespace Class3FileIO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("on the now item");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            combo_file.Items.Clear();
            button_file.Enabled = false;
            // Load an external file 
            string val = "";
            System.IO.StreamReader sr = new System.IO.StreamReader(@"../../../../pets.txt");

            while((val = sr.ReadLine()) != null)
            {
                combo_file.Items.Add(val);
            }
        }

        private void button_xml_Click(object sender, EventArgs e)
        {
            button_xml.Enabled = false;
            XmlReader xr = XmlReader.Create(@"../../../../pets.xml");
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Text)
                {
                    combo_xml.Items.Add(xr.Value);
                }
            }
        }

        private void button_json_Click(object sender, EventArgs e)
        {
            button_json.Enabled = false;
            StreamReader xr = new StreamReader(@"../../../../pets.json");
            string json = xr.ReadToEnd();
            List<string> vals = JsonConvert.DeserializeObject<List<string>>(json);
            foreach(string val in vals)
            {
                combo_json.Items.Add(val);
            }
        }
    }
}
