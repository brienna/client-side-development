using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            spinner.Visible = false;
            //timer1.Start();
        }


        // bad, hangs
        #region inline
        //Inline!
        private void button1_Click(object sender, EventArgs e)
        {
            // Start spinner
            spinner.Visible = true;
            timer1.Start();
            Cursor.Current = Cursors.WaitCursor;
 

            //take some time.
            getStuff();

            // Stop spinner
            spinner.Visible = false;
            timer1.Stop();


            //this way sucks.  We are stuck in the main thread and
            //can't have the form redraw the spinner or the 
            //loadbar...

        }


        #endregion inline

        // can't see cuz thread starts at the same time
        #region threaded
        //Threaded... Launch up a new single thread to 
        //process the requests.
        private void button2_Click(object sender, EventArgs e)
        {
            //turn on the visuals...
            spinner.Visible = true;


            //create a thread.
            ThreadStart ts = new ThreadStart(getStuff);
            Thread t = new Thread(ts);
            t.Start();

            //turn off visuals
            spinner.Visible = false;

            //cant see anything - why?  It's a thread.
            //seperate process that makes this code forge ahead and
            //not wait at all.

        }


        #endregion threaded


        #region backgroundWorker
        //BackgroundWorker - a way
        /*
         * The core issue that BW tried to solve was the need to 
         * execute synchronous code on a background thread.
         * (like updating a UI after load...)
         * 
         */
        private void button3_Click(object sender, EventArgs e)
        {
            spinner.Visible = true;
            timer1.Start();

            //BW
            var bw = new BackgroundWorker();
            // define it
            bw.DoWork += Bw_DoWork; // use += so can do many things 
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

            // execute it 
            bw.RunWorkerAsync();
            
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            getStuff();
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spinner.Visible = false;
            timer1.Stop();
        }





        #endregion backgroundWorker

        #region task
        //task - another way...
        //threaded and ASYNC.
        private async void button4_Click(object sender, EventArgs e)
        {
            timer1.Start();
            spinner.Visible = true;

            //create the Task
            await Task.Run(() => { 
                getStuff();
            });

            timer1.Stop();
            spinner.Visible = false;
        }
        #endregion task

        private void getStuff()
        {


            for (int j = 0; j < 5; j++)
            {
                //List<string> orgs = new List<string>();
                string uri = @"http://simon.ist.rit.edu:8080/Services/resources/ESD/Organizations";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                req.Method = "GET";
                Console.WriteLine("in");
                try
                {
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    //converts the response into a usable stream
                    Stream str = res.GetResponseStream();
                    // reads the stream as an XML object
                    XmlReader xr = XmlReader.Create(str);

                    XmlDocument xmlDoc = new XmlDocument();
                    xr.Read();
                    xmlDoc.Load(xr);
                    XmlNodeList names = xmlDoc.GetElementsByTagName("name");
                    for (int i = 0; i < names.Count; i++)
                    {
                        //waisting time...
                    }
                    res.Close();
                }
                catch (Exception ex)
                {
                    Console.Write("Error");
                }
            }

            Console.Write("Done");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Added a Timer
            if (progressBar1.Value < 99)
            {
                progressBar1.Value++;
            } else
            {
                progressBar1.Value = 0;
            }
        }
    }
}
