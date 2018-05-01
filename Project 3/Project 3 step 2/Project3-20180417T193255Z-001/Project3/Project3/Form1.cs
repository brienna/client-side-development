using Newtonsoft.Json.Linq;
using RestUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Project3 {
    public partial class Form1 : Form {
        // global objects (to make json data accessible after retrieving)
        About about;
        Degrees degrees;
        Minors minors;
        People people;
        Research research;
        Employment employment;
        News news;
        Resources resources;
        Footer footer;
        
        // get our restful resources...
        public REST rj = new REST("http://ist.rit.edu/api");

        // stopwatch for testing...
        // Stopwatch sw = new Stopwatch();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Customize body tab control
            body.Appearance = TabAppearance.FlatButtons;
            body.ItemSize = new Size(0, 1);
            body.SizeMode = TabSizeMode.Fixed;
        }

        #region GetRest
        public string getRestData(string uri)
        {
            string baseUri = "http://ist.rit.edu/api";
            // connect to the api
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(baseUri + uri);
            try
            {
                WebResponse resp = req.GetResponse();
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            } catch (WebException ex)
            {
                WebResponse err = ex.Response;
                using (Stream respStream = err.GetResponseStream())
                {
                    StreamReader r = new StreamReader(respStream, Encoding.UTF8);
                    string errorText = r.ReadToEnd();
                    // log it
                }
                throw;
            }
        
        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // which link am I?
            LinkLabel me = sender as LinkLabel;
            // me
            me.LinkVisited = true;
            // navigate to the url
            System.Diagnostics.Process.Start(me.Tag.ToString());
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            // MessageBox.Show("i");
            int col = e.ColumnIndex; // won't use
            int row = e.RowIndex;

            string id = DataGridView1[0, row].Value.ToString();
            MessageBox.Show(id);
        }

        private void btn_ListView_Click(object sender, EventArgs e) {
            // dynamically create 
            listView1.View = View.Details; // we want text
            listView1.GridLines = true;
            listView1.FullRowSelect = true; // click, highlight full row
            listView1.Width = 710;

            // create the column headers...
            listView1.Columns.Add("Employers", 200); // 2nd arg is how big it shld be
            listView1.Columns.Add("Degrees", 200);
            listView1.Columns.Add("City", 200);
            listView1.Columns.Add("Term", 100);

            // and populate ListView
            ListViewItem item;

            for (int i = 0; i < employment.coopTable.coopInformation.Count; i++)
            {
                item = new ListViewItem(new String[] {
                    employment.coopTable.coopInformation[i].employer,
                    employment.coopTable.coopInformation[i].degree,
                    employment.coopTable.coopInformation[i].city,
                    employment.coopTable.coopInformation[i].term
                });

                listView1.Items.Add(item);
            }
        }

        private void about_btn_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        // When "ABOUT" is clicked
        private void about_btn_Click(object sender, EventArgs e) {
            // Change body view to About section
            body.SelectedTab = about_tab;

            // Ensure we have the data, fetch if we don't
            if (about == null) {
                Console.WriteLine("Loading about...");
                string jsonAbout = rj.getRestJSON("/about/");
                about = JToken.Parse(jsonAbout).ToObject<About>();

                // Dynamically load About section page
                about_title.Text = about.title;
                about_desc.Text = about.description;
                about_quote.Text = about.quote;
                about_quoteAuth.Text = about.quoteAuthor;
            }
        }

        // Change body view to Degrees section when "DEGREES" is clicked
        private void degrees_btn_Click(object sender, EventArgs e) {
            // Change body view to Degrees section
            body.SelectedTab = degrees_tab;

            // Ensure we have the data, fetch if we don't
            if (degrees == null) {
                Console.WriteLine("Loading degrees...");
                string jsonDeg = rj.getRestJSON("/degrees/");
                degrees = JToken.Parse(jsonDeg).ToObject<Degrees>();

                // Dynamically load undergraduate degrees
                int row = 0;
                int column = 0;
                for (int i = 0; i < degrees.undergraduate.Count; i++) {
                    Undergraduate currUgDeg = degrees.undergraduate[i];

                    // Create and populate panel for each degree
                    TableLayoutPanel degreePanel = new TableLayoutPanel();
                    degreePanel.ColumnCount = 1;
                    degreePanel.RowCount = 2;
                    degreePanel.AutoSize = true;
                    degreePanel.Dock = DockStyle.Fill;
                    degreePanel.BorderStyle = BorderStyle.FixedSingle;
                    foreach (RowStyle style in degreePanel.RowStyles) {
                        style.SizeType = SizeType.AutoSize;
                    }

                    // Degree title
                    Label degTitle = new Label();
                    degTitle.Text = currUgDeg.title;
                    degTitle.Dock = DockStyle.Fill;
                    degTitle.AutoSize = false;
                    degTitle.MaximumSize = new Size(100, 0);
                    degTitle.AutoSize = true;

                    // Degree description
                    TextBox degDesc = new TextBox();
                    degDesc.ReadOnly = true;
                    degDesc.Multiline = true;
                    degDesc.Dock = DockStyle.Fill;
                    degDesc.Text = currUgDeg.description;
                    SizeF size = degDesc.CreateGraphics()
                                .MeasureString(degDesc.Text,
                                                degDesc.Font,
                                                degDesc.Width,
                                                new StringFormat(0));
                    degDesc.Height = (int)size.Height;

                    // Add components to degree panel, then to main panel
                    degreePanel.Controls.Add(degTitle, 0, 0);
                    degreePanel.Controls.Add(degDesc, 0, 1);
                    ug_degrees.Controls.Add(degreePanel, column, row);

                    // Resize rows
                    foreach (RowStyle style in ug_degrees.RowStyles) {
                        style.SizeType = SizeType.AutoSize;
                    }

                    // Set onclick event handler to show degree details in popup
                    degreePanel.Click += (sender2, e2) => showUgDegreePopup(sender2, e2, currUgDeg);

                    // Jump to next row if current row is full
                    if ((i + 1) % 3 == 0) {
                        row++;
                        column = 0;
                    } else {
                        column++;
                    }
                }
                
                // Dynamically load graduate degrees and certificates
                row = 0;
                column = 0;
                for (int i = 0; i < degrees.graduate.Count; i++) {
                    Graduate currGradDeg = degrees.graduate[i];

                    // If certificate,
                    if (currGradDeg.degreeName == "graduate advanced certificates") {
                        int certRow = 0;
                        int certColumn = 0;
                        for (int j = 0; j < currGradDeg.availableCertificates.Count; j++) {
                            // Create and populate panel for each certificate
                            TableLayoutPanel certPanel = new TableLayoutPanel();
                            certPanel.ColumnCount = 1;
                            certPanel.RowCount = 1;
                            certPanel.AutoSize = true;
                            certPanel.Dock = DockStyle.Fill;
                            certPanel.BorderStyle = BorderStyle.FixedSingle;
                            foreach (RowStyle style in certPanel.RowStyles) {
                                style.SizeType = SizeType.AutoSize;
                            }

                            // Certificate title
                            LinkLabel certTitle = new LinkLabel();
                            certTitle.Text = degrees.graduate[i].availableCertificates[j];
                            certTitle.Dock = DockStyle.Fill;
                            certTitle.AutoSize = false;
                            certTitle.MaximumSize = new Size(100, 0);
                            certTitle.AutoSize = true;
                            if (certTitle.Text == "Web Development Advanced certificate") {
                                certTitle.Tag = "http://www.rit.edu/programs/web-development-adv-cert";
                            } else if (certTitle.Text == "Networking,Planning and Design Advanced Cerificate") {
                                certTitle.Tag = "http://www.rit.edu/programs/networking-planning-and-design-adv-cert";
                            }

                            // Add components to certificate panel, then to main panel
                            certPanel.Controls.Add(certTitle, 0, 0);
                            grad_certs.Controls.Add(certPanel, certColumn, certRow);

                            // Resize rows
                            foreach (RowStyle style in grad_certs.RowStyles) {
                                style.SizeType = SizeType.AutoSize;
                            }

                            // Jump to next row if current row is full
                            if ((j + 1) % 3 == 0) {
                                certRow++;
                                certColumn = 0;
                            } else {
                                certColumn++;
                            }
                        }

                        // End current iteration of i loop here
                        continue;
                    }

                    // Create and populate panel for each degree
                    TableLayoutPanel degreePanel = new TableLayoutPanel();
                    degreePanel.ColumnCount = 1;
                    degreePanel.RowCount = 2;
                    degreePanel.AutoSize = true;
                    degreePanel.Dock = DockStyle.Fill;
                    degreePanel.BorderStyle = BorderStyle.FixedSingle;
                    foreach (RowStyle style in degreePanel.RowStyles) {
                        style.SizeType = SizeType.AutoSize;
                    }

                    // Degree title
                    Label degTitle = new Label();
                    degTitle.Text = currGradDeg.title;
                    degTitle.Dock = DockStyle.Fill;
                    degTitle.AutoSize = false;
                    degTitle.MaximumSize = new Size(100, 0);
                    degTitle.AutoSize = true;

                    // Degree description
                    TextBox degDesc = new TextBox();
                    degDesc.ReadOnly = true;
                    degDesc.Multiline = true;
                    degDesc.Dock = DockStyle.Fill;
                    degDesc.Text = currGradDeg.description;
                    SizeF size = degDesc.CreateGraphics()
                                .MeasureString(degDesc.Text,
                                                degDesc.Font,
                                                degDesc.Width,
                                                new StringFormat(0));
                    degDesc.Height = (int)size.Height;

                    // Add components to degree panel, then to main panel
                    degreePanel.Controls.Add(degTitle, 0, 0);
                    degreePanel.Controls.Add(degDesc, 0, 1);
                    grad_degrees.Controls.Add(degreePanel, column, row);

                    // Resize rows
                    foreach (RowStyle style in grad_degrees.RowStyles) {
                        style.SizeType = SizeType.AutoSize;
                    }

                    // Set onclick event handler to show degree details in popup
                    degreePanel.Click += (sender2, e2) => showGradDegreeDetails(sender2, e2, currGradDeg);

                    // Jump to next row if current row is full
                    if ((i + 1) % 3 == 0) {
                        row++;
                        column = 0;
                    } else {
                        column++;
                    }
                }

            }
        }

        // Show overlay with details when grad degree is clicked
        private void showGradDegreeDetails(object sender2, EventArgs e2, Graduate gradDeg) {
            // Populate popup with data
            Popup popup = new Popup(gradDeg);
            popup.Show();
        }

        // Show overlay with details when ug degree is clicked
        private void showUgDegreePopup(object sender, EventArgs e, Undergraduate ugDeg) {
            // Populate popup with data
            Popup popup = new Popup(ugDeg);
            popup.Show();
        }

        // Show overlay with details when ug minor is clicked
        private void showUgMinorPopup(object sender2, EventArgs e2, UgMinor ugMinor) {
            // Populate popup with data
            Popup popup = new Popup(ugMinor);
            popup.Show();
        }

        // Load minors data when entering "Minors" tab for the first time
        private void tabControl3_Enter(object sender, EventArgs e) {
            // Ensure we have the data, fetch if we don't
            if (minors == null) {
                Console.WriteLine("Loading minors...");
                string jsonMinors = rj.getRestJSON("/minors/");
                minors = JToken.Parse(jsonMinors).ToObject<Minors>();

                // Dynamically load minors
                int row = 0;
                int column = 0;
                for (int i = 0; i < minors.UgMinors.Count; i++) {
                    UgMinor thisMinor = minors.UgMinors[i];
                    // Minor title
                    Label minorTitle = new Label();
                    minorTitle.Text = thisMinor.title;
                    minorTitle.MouseEnter += (sender2, e2) => changeCellColor(sender2, e2);
                    minorTitle.MouseLeave += (sender3, e3) => changeCellColor(sender3, e3);
                    minorTitle.Margin = new Padding(0, 0, minorTitle.Margin.Right, minorTitle.Margin.Right);
                    minorTitle.BorderStyle = BorderStyle.FixedSingle;
                    minorTitle.TextAlign = ContentAlignment.MiddleCenter;
                    minorTitle.Anchor = (AnchorStyles.Left | AnchorStyles.Right);

                    // Add components to degree panel, then to main panel
                    ug_minors.Controls.Add(minorTitle, column, row);

                    // Set onclick event handler to show degree details in popup
                    minorTitle.Click += (sender4, e4) => showUgMinorPopup(sender4, e4, thisMinor);

                    // Jump to next row if current row is full
                    if ((i + 1) % 3 == 0) {
                        row++;
                        column = 0;
                    } else {
                        column++;
                    }
                }

                // Resize rows
                foreach (RowStyle style in ug_minors.RowStyles) {
                    style.SizeType = SizeType.AutoSize;
                }
            }
        }

        // Change body view to People section when "PEOPLE" is clicked
        private void people_btn_Click(object sender, EventArgs e) {
            // Change body view to People section
            body.SelectedTab = people_tab;

            // Ensure we have the data, fetch if we don't
            if (people == null) {
                Console.WriteLine("Loading people...");
                string jsonPeople = rj.getRestJSON("/people/");
                people = JToken.Parse(jsonPeople).ToObject<People>();

                int row = 0;
                int column = 0;
                Console.WriteLine("Loading faculty...");
                for (var i = 0; i < people.faculty.Count; i++) {
                    Faculty thisFac = people.faculty[i];
                    Label facName = new Label();
                    facName.Text = thisFac.name;
                    facName.BorderStyle = BorderStyle.FixedSingle;
                    facName.MouseEnter += (sender2, e2) => changeCellColor(sender2, e2);
                    facName.MouseLeave += (sender3, e3) => changeCellColor(sender3, e3);
                    facName.Click += (sender4, e4) => showFacultyPopup(sender4, e4, thisFac);
                    facName.Margin = new Padding(0, 0, facName.Margin.Right, facName.Margin.Right);
                    facName.TextAlign = ContentAlignment.MiddleCenter;
                    facName.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                    faculty.Controls.Add(facName, column, row);

                    // Jump to next row if current row is full
                    if ((i + 1) % faculty.ColumnCount == 0) {
                        row++;
                        column = 0;
                    } else {
                        column++;
                    }
                }

                // Fix row heights
                foreach (RowStyle style in faculty.RowStyles) {
                    style.SizeType = SizeType.AutoSize;
                }
            }
        }

        // Hover event handler for table cells (actually operates on nested controls)
        private void changeCellColor(object sender2, EventArgs e2) {
            if (sender2 is Label) {
                Label currLabel = (Label)sender2;
                if (currLabel.BackColor == Color.Black) {
                    currLabel.BackColor = Color.Transparent;
                    currLabel.ForeColor = Color.Black;
                    this.Cursor = Cursors.Default;
                } else if (currLabel.BackColor == Color.Transparent) {
                    currLabel.BackColor = Color.Black;
                    currLabel.ForeColor = Color.LightGray;
                    this.Cursor = Cursors.Hand;
                }
            }
        }

        // Change body view to Research section when "RESEARCH" is clicked
        private void research_btn_Click(object sender, EventArgs e)
        {
            body.SelectedTab = research_tab;

            // Ensure we have the data, fetch if we don't
            if (research == null) {
                Console.WriteLine("Loading research...");
                string jsonResearch = rj.getRestJSON("/research/");
                research = JToken.Parse(jsonResearch).ToObject<Research>();

                // Dynamically load research by interest area
                int row = 0;
                int column = 0;
                for (int i = 0; i < research.byInterestArea.Count; i++) {
                    ByInterestArea area = research.byInterestArea[i];

                    Label areaName = new Label();
                    areaName.Text = area.areaName;
                    areaName.MouseEnter += (sender2, e2) => changeCellColor(sender2, e2);
                    areaName.MouseLeave += (sender3, e3) => changeCellColor(sender3, e3);
                    areaName.Margin = new Padding(0, 0, areaName.Margin.Right, areaName.Margin.Right);
                    areaName.BorderStyle = BorderStyle.FixedSingle;
                    areaName.TextAlign = ContentAlignment.MiddleCenter;
                    areaName.Anchor = (AnchorStyles.Left | AnchorStyles.Right);

                    interestareas.Controls.Add(areaName, column, row);

                    // Set onclick event handler to show degree details in popup
                    areaName.Click += (sender4, e4) => showResearchPopup(sender4, e4, area);

                    // Jump to next row if current row is full
                    if ((i + 1) % 3 == 0) {
                        row++;
                        column = 0;
                    } else {
                        column++;
                    }
                }

                // Resize rows
                foreach (RowStyle style in interestareas.RowStyles) {
                    style.SizeType = SizeType.AutoSize;
                }
            }
        }

        // Change body view to Faculty section when "By Faculty Area" is clicked
        private void faculty_research_tab_Enter(object sender, EventArgs e) {
            research_tabs.SelectedTab = faculty_research_tab;

            // Ensure data hasn't already been loaded
            if (faculty_research_panel.HasChildren) {
                return;
            }

            // Dynamically load research by faculty 
            int row = 0;
            int column = 0;
            for (int i = 0; i < research.byFaculty.Count; i++) {
                ByFaculty fac = research.byFaculty[i];

                Label facName = new Label();
                facName.Text = fac.facultyName;
                facName.MouseEnter += (sender2, e2) => changeCellColor(sender2, e2);
                facName.MouseLeave += (sender3, e3) => changeCellColor(sender3, e3);
                facName.Margin = new Padding(0, 0, facName.Margin.Right, facName.Margin.Right);
                facName.BorderStyle = BorderStyle.FixedSingle;
                facName.TextAlign = ContentAlignment.MiddleCenter;
                facName.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                faculty_research_panel.Controls.Add(facName, column, row);

                // Set onclick event handler to show degree details in popup
                facName.Click += (sender4, e4) => showResearchFacPopup(sender4, e4, fac);

                // Jump to next row if current row is full
                if ((i + 1) % 3 == 0) {
                    row++;
                    column = 0;
                } else {
                    column++;
                }
            }

            // Resize rows
            foreach (RowStyle style in faculty_research_panel.RowStyles) {
                style.SizeType = SizeType.AutoSize;
            }
            
        }

        // Popup for Research by Faculty
        private void showResearchFacPopup(object sender4, EventArgs e4, ByFaculty facName) {
            Popup popup = new Popup(facName);
            popup.Show();
        }

        // Popup for Research by interest area
        private void showResearchPopup(object sender4, EventArgs e4, ByInterestArea area) {
            Popup popup = new Popup(area);
            popup.Show();
        }

        // Change body view to Employment section when "EMPLOYMENT" is clicked
        private void emp_btn_Click(object sender, EventArgs e)
        {
            body.SelectedTab = emp_tab;

            // Ensure we have data, fetch if we don't
            if (employment == null) {
                Console.WriteLine("Loading employment...");
                // go get the /employment info
                string jsonEmp = rj.getRestJSON("/employment/");
                employment = JToken.Parse(jsonEmp).ToObject<Employment>();

                Introduction contents = employment.introduction;
                DegreeStatistics stats = employment.degreeStatistics;
                Employers employers = employment.employers;
                Careers careers = employment.careers;
                CoopTable coopTable = employment.coopTable;
                EmploymentTable empTable = employment.employmentTable;

                Label title = new Label();
                title.Text = employment.introduction.title;

                


            }

            // have I been built?
            if (DataGridView1.Rows.Count < 2) {
                // populate the dataGridView...
                for (int i = 0; i < employment.coopTable.coopInformation.Count; i++) {
                    DataGridView1.Rows.Add();
                    DataGridView1.Rows[i].Cells[0].Value = employment.coopTable.coopInformation[i].employer;
                    DataGridView1.Rows[i].Cells[1].Value = employment.coopTable.coopInformation[i].degree;
                    DataGridView1.Rows[i].Cells[2].Value = employment.coopTable.coopInformation[i].city;
                    DataGridView1.Rows[i].Cells[3].Value = employment.coopTable.coopInformation[i].term;
                }
            }
        }

        // Change body view to Resources section when "RESOURCES" is clicked
        private void resources_btn_Click(object sender, EventArgs e)
        {
            // Change body view to Resources section
            body.SelectedTab = resources_tab;

            // Ensure we have the data, fetch if we don't
            if (resources == null) {
                Console.WriteLine("Loading resources...");
                string jsonResources = rj.getRestJSON("/resources/");
                resources = JToken.Parse(jsonResources).ToObject<Resources>();

                // Set title and subtitle
                resources_title.Text = resources.title;
                resources_subtitle.Text = resources.subTitle;

                // Process each resource label and popup event handler
                resource1.Text = "Forms";
                resource1.Click += (sender2, e2) => showResourcesPopup(sender2, e2, resource1.Text);
                resource2.Text = resources.studyAbroad.title;
                resource2.Click += (sender3, e3) => showResourcesPopup(sender3, e3, resource2.Text);
                resource3.Text = resources.studentServices.title;
                resource3.Click += (sender4, e4) => showResourcesPopup(sender4, e4, resource3.Text);
                resource4.Text = resources.tutorsAndLabInformation.title;
                resource4.Click += (sender5, e5) => showResourcesPopup(sender5, e5, resource4.Text);
                resource5.Text = resources.studentAmbassadors.title;
                resource5.Click += (sender6, e6) => showResourcesPopup(sender6, e6, resource5.Text);
                resource6.Text = resources.coopEnrollment.title;
                resource6.Click += (sender7, e7) => showResourcesPopup(sender7, e7, resource6.Text);
            }
        }

        // Popup for Resources
        private void showResourcesPopup(object sender2, EventArgs e2, string type) {
            // Populate popup with data
            Popup popup = new Popup(type, resources);
            popup.Show();
        }

        // Popup for Faculty
        private void showFacultyPopup(object sender4, object e4, Faculty thisFac) {
            // Populate popup with data
            Popup popup = new Popup(thisFac);
            popup.Show();
        }

        // Popup for Staff
        private void showStaffPopup(object sender4, EventArgs e4, Staff thisStaff) {
            // Populate popup with data
            Popup popup = new Popup(thisStaff);
            popup.Show();
        }

        // Load staff when entering "Staff" tab for the first time
        private void staff_tab_Enter(object sender, EventArgs e) {
            // Change people view to Staff section
            people_tabControl.SelectedTab = staff_tab;

            // Stop loading if already loaded
            if (staff.HasChildren) {
                return;
            }

            int row = 0;
            int column = 0;
            Console.WriteLine("Loading staff...");
            for (var i = 0; i < people.staff.Count; i++) {
                Staff thisStaff = people.staff[i];
                Label staffName = new Label();
                staffName.Text = thisStaff.name;
                staffName.BorderStyle = BorderStyle.FixedSingle;
                staffName.MouseEnter += (sender2, e2) => changeCellColor(sender2, e2);
                staffName.MouseLeave += (sender3, e3) => changeCellColor(sender3, e3);
                staffName.Click += (sender4, e4) => showStaffPopup(sender4, e4, thisStaff);
                staffName.Margin = new Padding(0, 0, staffName.Margin.Right, staffName.Margin.Right);
                staffName.TextAlign = ContentAlignment.MiddleCenter;
                staffName.Dock = DockStyle.Fill;
                staff.Controls.Add(staffName, column, row);

                // Jump to next row if current row is full
                if ((i + 1) % staff.ColumnCount == 0) {
                    row++;
                    column = 0;
                } else {
                    column++;
                }
            }
        }

        // Load news when click "NEWS" button
        private void news_btn_Click(object sender, EventArgs e) {
            // Switch to news view
            body.SelectedTab = news_tab;

            if (news == null) {
                // Get news
                Console.WriteLine("Loading news...");
                string jsonNews = rj.getRestJSON("/news/");
                news = JToken.Parse(jsonNews).ToObject<News>();

                Label mainTitle = new Label();
                mainTitle.Text = "News";
                mainTitle.Width = news_panel.Width - (mainTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                mainTitle.Font = new Font("Arial", 18, FontStyle.Bold);
                news_panel.Controls.Add(mainTitle);

                // Load news
                for (int i = 0; i < news.older.Count; i++) {
                    Label newsTitle = new Label();
                    newsTitle.Text = news.older[i].title;
                    newsTitle.Width = news_panel.Width - (newsTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                    newsTitle.Font = new Font("Arial", 14, FontStyle.Bold);
                    
                    RichTextBox newsBox = new RichTextBox();
                    newsBox.ReadOnly = true;
                    newsBox.ContentsResized += rtb_ContentsResized;
                    newsBox.BorderStyle = BorderStyle.None;
                    newsBox.Width = news_panel.Width - (newsBox.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

                    newsBox.AppendText(news.older[i].date);
                    newsBox.AppendText(Environment.NewLine);
                    newsBox.AppendText(Environment.NewLine);
                    if (news.older[i].description != null) {
                        newsBox.AppendText(news.older[i].description);
                    }
                    
                    news_panel.Controls.Add(newsTitle);
                    news_panel.Controls.Add(newsBox);
                }
            }
        }

        // Utility event handler to resize richtextboxes
        private void rtb_ContentsResized(object sender, ContentsResizedEventArgs e) {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }
    }
}
