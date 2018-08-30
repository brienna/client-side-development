using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using CookComputing.XmlRpc;

namespace ConsumeServices
{

    [XmlRpcUrl("http://alvin.ist.rit.edu:8100")]
    public interface IBeers : IXmlRpcProxy {
        [XmlRpcMethod("beer.getBeers")]
        string[] getBeers();
    }

    public partial class frmServices : Form
    {
        public frmServices()
        {
            InitializeComponent();
        }

        private void btn_soap_Click(object sender, EventArgs e)
        {
            serviceBeer.BeerClient bc = new serviceBeer.BeerClient();
            string[] beers = bc.getBeers();
            cb_soap.Items.Clear();
            for (int i = 0; i < beers.Length; i++)
            {
                cb_soap.Items.Add(beers[i]);
            }
        }

        private void btn_rest_Click(object sender, EventArgs e)
        {
            List<string> beers = new List<string>();
            string uri = "http://simon.ist.rit.edu:8080/BeerService/resources/Services/Beers";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "GET"; // get method request on the above web request

            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream str = res.GetResponseStream();

                // need to read the XML that comes back...
                XmlReader xr = XmlReader.Create(str);

                XmlDocument xmlDoc = new XmlDocument();
                xr.Read();
                xmlDoc.Load(xr);
                // now have an xml document
                XmlNodeList names = xmlDoc.GetElementsByTagName("name");
                for (int i = 0; i < names.Count; i++)
                {
                    cb_rest.Items.Add(names.Item(i).InnerText);
                }
            } 
            catch(Exception ex)
            {
                Console.Write(ex);
            }
        }

        private void btn_xml_Click(object sender, EventArgs e)
        {
            IBeers proxy = XmlRpcProxyGen.Create<IBeers>();
            string[] beers = proxy.getBeers();
            cb_xml.Items.Clear();
            for (int i = 0; i < beers.Length; i++)
            {
                cb_xml.Items.Add(beers[i]);
            }
        }
    }
}
