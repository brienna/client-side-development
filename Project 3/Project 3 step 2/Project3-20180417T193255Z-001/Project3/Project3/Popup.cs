using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3 {
    public partial class Popup : Form {
        // Initialize globals 
        Resources resources = null;
        Faculty thisFaculty = null;
        Staff thisStaff = null;
        Graduate thisGradDeg = null;
        Undergraduate thisUgDeg = null;
        UgMinor thisUgMinor = null;
        List<CourseInfo> courseInfos = null;
        Form1 utilityForm = null;
        string type = null;

        public Popup() {
            InitializeComponent();
        }

        // Constructs popup for Resources
        public Popup(string typeParam, Resources r) {
            InitializeComponent();
            resources = r;
            type = typeParam;
        }

        // Constructs popup for Faculty
        public Popup(Faculty f) {
            InitializeComponent();
            thisFaculty = f;
        }

        // Constructs popup for Staff
        public Popup(Staff s) {
            InitializeComponent();
            thisStaff = s;
        }

        // Constructs popup for Graduate degree
        public Popup(Graduate gDeg) {
            InitializeComponent();
            thisGradDeg = gDeg;
        }

        // Constructs popup for Undergraduate degree
        public Popup(Undergraduate ugDeg) {
            InitializeComponent();
            thisUgDeg = ugDeg;
        }

        // Constructs popup for Undergraduate minor
        public Popup(UgMinor ugMinor) {
            InitializeComponent();
            thisUgMinor = ugMinor;
        }

        private void Popup_Load(object sender, EventArgs e) {
            // Hide tab headers
            popup_tabs.Appearance = TabAppearance.FlatButtons;
            popup_tabs.ItemSize = new Size(0, 1);
            popup_tabs.SizeMode = TabSizeMode.Fixed;

            // Based on type of form specified, show view 
            if (type == "Forms") {
                loadForms();
            } else if (resources != null && type == resources.studyAbroad.title) {
                loadStudyAbroad();
            } else if (resources != null && type == resources.studentServices.title) {
                loadAdvising();
            } else if (resources != null && type == resources.tutorsAndLabInformation.title) {
                loadTutors();
            } else if (resources != null && type == resources.coopEnrollment.title) {
                loadCoop();
            } else if (resources != null && type == resources.studentAmbassadors.title) {
                loadAmbassadors();
            } else if (thisFaculty != null) {
                loadFacultyDetails();
            } else if (thisStaff != null) {
                loadStaffDetails();
            } else if (thisGradDeg != null) {
                loadGradDegree();
            } else if (thisUgDeg != null) {
                loadUgDegree();
            } else if (thisUgMinor != null) {
                loadUgMinor();
            }
        }

        // Loads undergrad minor details
        private void loadUgMinor() {
            // Show and populate Degree tab
            popup_tabs.SelectedTab = deg_tab;

            Label minorTitle = new Label();
            minorTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            minorTitle.Text = thisUgMinor.title;
            minorTitle.Width = degreePanel.Width - (minorTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            RichTextBox minorDesc = new RichTextBox();
            minorDesc.ReadOnly = true;
            minorDesc.Text = thisUgMinor.description;
            minorDesc.ContentsResized += rtb_ContentsResized;
            minorDesc.BorderStyle = BorderStyle.None;
            minorDesc.Width = degreePanel.Width - (minorDesc.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            Label coursesAvailableTitle = new Label();
            coursesAvailableTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            coursesAvailableTitle.Text = "Courses Available (Select to view description)";
            coursesAvailableTitle.Width = degreePanel.Width - (coursesAvailableTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            List<String> names = new List<String>();
            foreach (string course in thisUgMinor.courses) {
                // If unique
                if (!names.Contains(course)) {
                    names.Add(course);
                }
            }

            // Sort courses by alphabetical order
            names.Sort();

            // Add each course to view
            ListView courseList = new ListView();
            courseList.View = View.Details; // we want text
            courseList.Columns.Add("Courses", -2);
            courseList.HeaderStyle = ColumnHeaderStyle.None;
            //listView1.GridLines = true;
            courseList.FullRowSelect = true; // click, highlight full row
            courseList.Width = degreePanel.Width - (courseList.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            courseList.Height = 200;
            foreach (string name in names) {
                courseList.Items.Add(new ListViewItem(name));
            }

            // Process course selection & message box dialog popup with descriptions
            courseInfos = new List<CourseInfo>();
            courseList.ItemSelectionChanged += (sender2, e2) => toggleCourseInfo(sender2, e2);

            RichTextBox note = new RichTextBox();
            note.ReadOnly = true;
            note.Text = thisUgMinor.note;
            note.ContentsResized += rtb_ContentsResized;
            note.BorderStyle = BorderStyle.None;
            note.Width = degreePanel.Width - (note.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            degreePanel.Controls.Add(minorTitle);
            degreePanel.Controls.Add(minorDesc);
            degreePanel.Controls.Add(coursesAvailableTitle);
            degreePanel.Controls.Add(courseList);
            if (note.Text != "") {
                degreePanel.Controls.Add(note);
            }
        }

        // Loads undergrad degree details
        private void loadUgDegree() {
            // Show and populate Degree tab
            popup_tabs.SelectedTab = deg_tab;

            string degName = "BS" + thisUgDeg.degreeName.ToUpper(); // save to fetch courses later
            Label degTitle = new Label();
            degTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            degTitle.Text = thisUgDeg.title;
            degTitle.Width = degreePanel.Width - (degTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;  

            RichTextBox ugDegDesc = new RichTextBox();
            ugDegDesc.ReadOnly = true;
            ugDegDesc.Text = thisUgDeg.description;
            ugDegDesc.ContentsResized += rtb_ContentsResized;
            ugDegDesc.BorderStyle = BorderStyle.None;
            ugDegDesc.Width = degreePanel.Width - (ugDegDesc.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            Label degConcTitle = new Label();
            degConcTitle.Text = "Concentrations";
            degConcTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            degConcTitle.Width = degreePanel.Width - (degConcTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            RichTextBox ugDegConcs = new RichTextBox();
            ugDegConcs.ReadOnly = true;
            ugDegConcs.ContentsResized += rtb_ContentsResized;
            ugDegConcs.BorderStyle = BorderStyle.None;
            ugDegConcs.Width = degreePanel.Width - (ugDegConcs.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            
            for (int i = 0; i < thisUgDeg.concentrations.Count; i++) {
                ugDegConcs.AppendText("\u2022  " + thisUgDeg.concentrations[i]);
                if (i != thisUgDeg.concentrations.Count - 1) {
                    ugDegConcs.AppendText(Environment.NewLine);
                }
            }
            
            Label coursesAvailableTitle = new Label();
            coursesAvailableTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            coursesAvailableTitle.Text = "Courses Available (Select to view description)";
            coursesAvailableTitle.Width = degreePanel.Width - (coursesAvailableTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            
            // Get courses, passing specific path
            Courses courses = GetCourses("/courses/degreeName=" + degName);
            // Keep each course if...
            List<String> names = new List<String>();
            foreach (string course in courses.courses) {
                Regex courseRegex = new Regex("[A-Z]{3,4}-(?!0)\\d{2,3}");
                Match m = courseRegex.Match(course);
                // its name is valid
                if (m.Success) {
                    // and unique
                    if (!names.Contains(course)) {
                        names.Add(course);
                    } 
                } 
            }

            // Sort courses by alphabetical order
            names.Sort();

            // Add each course to view
            ListView courseList = new ListView();
            courseList.View = View.Details; // we want text
            courseList.Columns.Add("Courses", -2);
            courseList.HeaderStyle = ColumnHeaderStyle.None;
            //listView1.GridLines = true;
            courseList.FullRowSelect = true; // click, highlight full row
            courseList.Width = degreePanel.Width - (courseList.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            courseList.Height = 200;
            foreach (string name in names) {
                courseList.Items.Add(new ListViewItem(name));
            }

            // Process course selection & message box dialog popup with descriptions
            courseInfos = new List<CourseInfo>();
            courseList.ItemSelectionChanged += (sender2, e2) => toggleCourseInfo(sender2, e2);

            degreePanel.Controls.Add(degTitle);
            degreePanel.Controls.Add(ugDegDesc);
            degreePanel.Controls.Add(degConcTitle);
            degreePanel.Controls.Add(ugDegConcs);
            degreePanel.Controls.Add(coursesAvailableTitle);
            degreePanel.Controls.Add(courseList);
        }

        // Show course info  
        private void toggleCourseInfo(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.IsSelected) {
                // Check which item was selected
                ListViewItem selectedItem = e.Item;
                string selectedCourse = selectedItem.Text;

                // Check if we have course info on the item
                CourseInfo courseInfo = courseInfos.Find(x => x.courseID == selectedCourse);
                // If we don't, we need to load its info
                if (courseInfo == null) {
                    // Load course info
                    Console.WriteLine("Loading info for " + selectedCourse);
                    // Use REST utility from Form1 to request data
                    if (utilityForm == null) {
                        utilityForm = new Form1();
                    }
                    string jsonCourseInfo = utilityForm.rj.getRestJSON("/course/courseID=" + selectedCourse);
                    courseInfo = JToken.Parse(jsonCourseInfo).ToObject<CourseInfo>();
                    // Add to array for tracking
                    courseInfos.Add(courseInfo);
                }

                // Show course info
                MessageBox.Show(courseInfo.courseID + ": " + courseInfo.title + "\n\n" + courseInfo.description);
            }
        }

        // Utility function to fetch courses for specific degree
        private Courses GetCourses(string coursePath) {
            // Load courses
            Console.WriteLine("Loading courses for ...");
            // Use REST utility from Form1 to request data
            if (utilityForm == null) {
                utilityForm = new Form1();
            }
            string jsonCourses = utilityForm.rj.getRestJSON(coursePath);
            return JToken.Parse(jsonCourses).ToObject<Courses>();
        }

        // Loads grad degree details
        private void loadGradDegree() {
            // Show and populate Degree tab
            popup_tabs.SelectedTab = deg_tab;

            string degName;
            // Exception is IST which is malformed
            if (thisGradDeg.degreeName == "ist") {
                degName = "MSIT";
            } else {
                degName = "MS" + thisGradDeg.degreeName.ToUpper();
            }
            Label degTitle = new Label();
            degTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            degTitle.Text = thisGradDeg.title;
            degTitle.Width = degreePanel.Width - (degTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            RichTextBox gDegDesc = new RichTextBox();
            gDegDesc.ReadOnly = true;
            gDegDesc.Text = thisGradDeg.description;
            gDegDesc.ContentsResized += rtb_ContentsResized;
            gDegDesc.BorderStyle = BorderStyle.None;
            gDegDesc.Width = degreePanel.Width - (gDegDesc.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            Label degConcTitle = new Label();
            degConcTitle.Text = "Concentrations";
            degConcTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            degConcTitle.Width = degreePanel.Width - (degConcTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            RichTextBox gDegConcs = new RichTextBox();
            gDegConcs.ContentsResized += rtb_ContentsResized;
            gDegConcs.ReadOnly = true;
            gDegConcs.BorderStyle = BorderStyle.None;
            gDegConcs.Width = degreePanel.Width - (gDegConcs.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            for (int i = 0; i < thisGradDeg.concentrations.Count; i++) {
                gDegConcs.AppendText("\u2022  " + thisGradDeg.concentrations[i]);
                if (i != thisGradDeg.concentrations.Count - 1) {
                    gDegConcs.AppendText(Environment.NewLine);
                }
            }

            Label coursesAvailableTitle = new Label();
            coursesAvailableTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            coursesAvailableTitle.Text = "Courses Available (Select to view description)";
            coursesAvailableTitle.Width = degreePanel.Width - (coursesAvailableTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            // Get courses, passing specific path
            Courses courses = GetCourses("/courses/degreeName=" + degName);
            // Keep each course if...
            List<String> names = new List<String>();
            foreach (string course in courses.courses) {
                Regex courseRegex = new Regex("[A-Z]{3,4}-(?!0)\\d{2,3}");
                Match m = courseRegex.Match(course);
                // its name is valid
                if (m.Success) {
                    // and unique
                    if (!names.Contains(course)) {
                        names.Add(course);
                    }
                }
            }

            // Sort courses by alphabetical order
            names.Sort();

            // Add each course to view
            ListView courseList = new ListView();
            courseList.View = View.Details; // we want text
            courseList.Columns.Add("Courses", -2);
            courseList.HeaderStyle = ColumnHeaderStyle.None;
            //listView1.GridLines = true;
            courseList.FullRowSelect = true; // click, highlight full row
            courseList.Width = degreePanel.Width - (courseList.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            courseList.Height = 200;
            foreach (string name in names) {
                courseList.Items.Add(new ListViewItem(name));
            }

            // Process course selection & message box dialog popup with descriptions
            courseInfos = new List<CourseInfo>();
            courseList.ItemSelectionChanged += (sender2, e2) => toggleCourseInfo(sender2, e2);

            degreePanel.Controls.Add(degTitle);
            degreePanel.Controls.Add(gDegDesc);
            degreePanel.Controls.Add(degConcTitle);
            degreePanel.Controls.Add(gDegConcs);
            degreePanel.Controls.Add(coursesAvailableTitle);
            degreePanel.Controls.Add(courseList);
        }

        // Loads staff details
        private void loadStaffDetails() {
            // Show and populate People tab
            popup_tabs.SelectedTab = people_popup_tab;
            person_name.Text = thisStaff.name;
            string title = thisStaff.title;
            if (thisStaff.tagline != "") {
                title = title + ", " + thisStaff.tagline;
            }

            person_title.Text = title;
            person_img.Load(thisStaff.imagePath);

            if (thisStaff.office != "") {
                person_details.AppendText("\u2022  Office: " + thisStaff.office);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisStaff.phone != "") {
                person_details.AppendText("\u2022  Phone: " + thisStaff.phone);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisStaff.website != "") {
                person_details.AppendText("\u2022  Website: " + thisStaff.website);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisStaff.email != "") {
                person_details.AppendText("\u2022  Email: " + thisStaff.email);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisStaff.facebook != "") {
                person_details.AppendText("\u2022  Facebook: " + thisStaff.facebook);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisStaff.twitter != "") {
                person_details.AppendText("\u2022  Twitter: " + thisStaff.twitter);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisStaff.interestArea != "") {
                person_details.AppendText("\u2022  Interest areas: " + thisStaff.interestArea.ToUpper());
            }
        }

        // Loads faculty details
        private void loadFacultyDetails() {
            // Show and populate People tab
            popup_tabs.SelectedTab = people_popup_tab;
            person_name.Text = thisFaculty.name;
            string title = thisFaculty.title;
            if (thisFaculty.tagline != "") {
                title = title + ", " + thisFaculty.tagline;
            }
            person_title.Text = title;
            person_img.SizeMode = PictureBoxSizeMode.AutoSize;
            person_img.Load(thisFaculty.imagePath);
            int oldWidth = person_img.Width; 
            int oldHeight = person_img.Height;
            int newWidth = people_popup_tab.Width / 3;
            int newHeight = oldHeight * (newWidth / oldWidth);
            person_img.SizeMode = PictureBoxSizeMode.StretchImage;
            person_img.Width = newWidth;

            if (thisFaculty.office != "") {
                person_details.AppendText("\u2022  Office: " + thisFaculty.office);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisFaculty.phone != "") {
                person_details.AppendText("\u2022  Phone: " + thisFaculty.phone);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisFaculty.website != "") {
                person_details.AppendText("\u2022  Website: " + thisFaculty.website);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisFaculty.email != "") {
                person_details.AppendText("\u2022  Email: " + thisFaculty.email);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisFaculty.facebook != "") {
                person_details.AppendText("\u2022  Facebook: " + thisFaculty.facebook);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisFaculty.twitter != "") {
                person_details.AppendText("\u2022  Twitter: " + thisFaculty.twitter);
                person_details.AppendText(Environment.NewLine);
            }
            if (thisFaculty.interestArea != "") {
                person_details.AppendText("\u2022  Interest areas: " + thisFaculty.interestArea.ToUpper());
            }
        }

        // Loads ambassadors
        private void loadAmbassadors() {
            // Show and populate Ambassadors tab
            popup_tabs.SelectedTab = ambassadors_tab;
            Label ambassadors_title = new Label();
            ambassadors_title.Text = resources.studentAmbassadors.title;
            ambassadors_title.Width = ambassadors_panel.Width - (ambassadors_title.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            PictureBox img = new PictureBox();
            img.Load(resources.studentAmbassadors.ambassadorsImageSource);
            int oldWidth = img.Width;
            img.SizeMode = PictureBoxSizeMode.StretchImage;
            img.Width = ambassadors_panel.Width - (img.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            img.Height = img.Height * (img.Width / oldWidth);
            ambassadors_panel.Controls.Add(ambassadors_title);
            ambassadors_panel.Controls.Add(img);
            for (int i = 0; i < resources.studentAmbassadors.subSectionContent.Count; i++) {
                SubSectionContent section = resources.studentAmbassadors.subSectionContent[i];
                Label sectionTitle = new Label();
                sectionTitle.Text = section.title;
                sectionTitle.Width = ambassadors_panel.Width - (sectionTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                RichTextBox sectionDesc = new RichTextBox();
                sectionDesc.BorderStyle = BorderStyle.None;
                sectionDesc.ReadOnly = true;
                sectionDesc.Text = section.description;
                sectionDesc.Width = ambassadors_panel.Width - (sectionTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                sectionDesc.ContentsResized += rtb_ContentsResized;
                ambassadors_panel.Controls.Add(sectionTitle);
                ambassadors_panel.Controls.Add(sectionDesc);
            }
            LinkLabel appForm = new LinkLabel();
            appForm.Text = "Google Forms link.";
            appForm.Tag = resources.studentAmbassadors.applicationFormLink;
            appForm.Width = ambassadors_panel.Width - (appForm.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            RichTextBox note = new RichTextBox();
            note.BorderStyle = BorderStyle.None;
            note.ReadOnly = true;
            note.Text = resources.studentAmbassadors.note;
            note.Width = ambassadors_panel.Width - (note.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            note.ContentsResized += rtb_ContentsResized;
            ambassadors_panel.Controls.Add(appForm);
            ambassadors_panel.Controls.Add(note);
        }

        // Loads coop resource
        private void loadCoop() {
            // Show and populate Coop Enrollment tab
            popup_tabs.SelectedTab = coop_tab;
            Label coop_title = new Label();
            coop_title.Text = resources.coopEnrollment.title;
            coop_title.Width = coop_panel.Width - (coop_title.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            LinkLabel guide = new LinkLabel();
            guide.Text = "Please refer to our Co-op Guide!";
            guide.Tag = resources.coopEnrollment.RITJobZoneGuidelink;
            guide.Width = coop_panel.Width - (guide.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            coop_panel.Controls.Add(coop_title);
            coop_panel.Controls.Add(guide);
            for (int i = 0; i < resources.coopEnrollment.enrollmentInformationContent.Count; i++) {
                EnrollmentInformationContent info = resources.coopEnrollment.enrollmentInformationContent[i];
                Label infoTitle = new Label();
                infoTitle.Text = info.title;
                infoTitle.Width = coop_panel.Width - (infoTitle.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                RichTextBox infoDesc = new RichTextBox();
                infoDesc.BorderStyle = BorderStyle.None;
                infoDesc.ReadOnly = true;
                infoDesc.Text = info.description;
                infoDesc.ContentsResized += rtb_ContentsResized;
                infoDesc.Width = coop_panel.Width - (infoDesc.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                coop_panel.Controls.Add(infoTitle);
                coop_panel.Controls.Add(infoDesc);
            }
        }

        // Loads tutors resource
        private void loadTutors() {
            // Show and populate Tutors tab
            popup_tabs.SelectedTab = tutors_tab;
            Label tutors_title = new Label();
            tutors_title.Text = resources.tutorsAndLabInformation.title;
            tutors_title.Width = tutors_panel.Width - (tutors_title.Margin.Right * 2);
            RichTextBox tutors_desc = new RichTextBox();
            tutors_desc.ReadOnly = true;
            tutors_desc.BorderStyle = BorderStyle.None;
            tutors_desc.Text = resources.tutorsAndLabInformation.description;
            tutors_desc.ContentsResized += rtb_ContentsResized;
            tutors_desc.Width = tutors_panel.Width - (tutors_desc.Margin.Right * 2);
            LinkLabel tutors_schedule = new LinkLabel();
            tutors_schedule.Text = "Lab Hours and TA Schedule";
            tutors_schedule.Tag = resources.tutorsAndLabInformation.tutoringLabHoursLink;
            tutors_schedule.Width = tutors_panel.Width - (tutors_schedule.Margin.Right * 2);
            tutors_panel.Controls.Add(tutors_title);
            tutors_panel.Controls.Add(tutors_desc);
            tutors_panel.Controls.Add(tutors_schedule);
        }

        // Loads advising resource
        private void loadAdvising() {
            // Show and populate Advising tab
            popup_tabs.SelectedTab = advising_tab;

            // Academic advisors
            Label aaLabel = new Label();
            aaLabel.Text = resources.studentServices.academicAdvisors.title;
            aaLabel.Width = advising_panel.Width - (aaLabel.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            RichTextBox aaRtb = new RichTextBox();
            aaRtb.BorderStyle = BorderStyle.None;
            aaRtb.ReadOnly = true;
            aaRtb.ContentsResized += rtb_ContentsResized;
            aaRtb.Text = resources.studentServices.academicAdvisors.description;
            aaRtb.Width = advising_panel.Width - (aaRtb.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            LinkLabel aaFaq = new LinkLabel();
            aaFaq.Text = resources.studentServices.academicAdvisors.faq.title;
            aaFaq.Tag = resources.studentServices.academicAdvisors.faq.contentHref;
            aaFaq.Width = advising_panel.Width - (aaFaq.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            advising_panel.Controls.Add(aaLabel);
            advising_panel.Controls.Add(aaRtb);
            advising_panel.Controls.Add(aaFaq);

            // Faculty advisors
            Label faLabel = new Label();
            faLabel.Text = resources.studentServices.facultyAdvisors.title;
            faLabel.Width = advising_panel.Width - (faLabel.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            RichTextBox faRtb = new RichTextBox();
            faRtb.BorderStyle = BorderStyle.None;
            faRtb.ReadOnly = true;
            faRtb.ContentsResized += rtb_ContentsResized;
            faRtb.Text = resources.studentServices.facultyAdvisors.description;
            faRtb.Width = advising_panel.Width - (faRtb.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            advising_panel.Controls.Add(faLabel);
            advising_panel.Controls.Add(faRtb);

            // IST minor advisors
            Label imaLabel = new Label();
            imaLabel.Text = resources.studentServices.istMinorAdvising.title;
            imaLabel.Width = advising_panel.Width - (imaLabel.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            RichTextBox imaRtb = new RichTextBox();
            imaRtb.BorderStyle = BorderStyle.None;
            imaRtb.ReadOnly = true;
            imaRtb.ContentsResized += rtb_ContentsResized;
            imaRtb.Width = advising_panel.Width - (imaRtb.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            for (int i = 0; i < resources.studentServices.istMinorAdvising.minorAdvisorInformation.Count; i++) {
                MinorAdvisorInformation advisor = resources.studentServices.istMinorAdvising.minorAdvisorInformation[i];
                imaRtb.AppendText("\u2022  " + advisor.advisor + " (" + advisor.email.Trim() + "): " + advisor.title);
                if (i != resources.studentServices.istMinorAdvising.minorAdvisorInformation.Count - 1) {
                    imaRtb.AppendText(Environment.NewLine);
                }
            }
            advising_panel.Controls.Add(imaLabel);
            advising_panel.Controls.Add(imaRtb);

            // Professional advisors
            Label paLabel = new Label();
            paLabel.Text = resources.studentServices.professonalAdvisors.title;
            paLabel.Width = advising_panel.Width - (paLabel.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            RichTextBox paRtb = new RichTextBox();
            paRtb.BorderStyle = BorderStyle.None;
            paRtb.ReadOnly = true;
            paRtb.ContentsResized += rtb_ContentsResized;
            paRtb.Width = advising_panel.Width - (paRtb.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            for (int i = 0; i < resources.studentServices.professonalAdvisors.advisorInformation.Count; i++) {
                AdvisorInformation advisor = resources.studentServices.professonalAdvisors.advisorInformation[i];
                paRtb.AppendText("\u2022  " + advisor.name + " (" + advisor.email.Trim() + "): " + advisor.department);
                if (i != resources.studentServices.professonalAdvisors.advisorInformation.Count - 1) {
                    paRtb.AppendText(Environment.NewLine);
                }
            }
            advising_panel.Controls.Add(paLabel);
            advising_panel.Controls.Add(paRtb);
        }

        // Loads study abroad resource
        private void loadStudyAbroad() {
            // Show and populate Study Abroad tab
            popup_tabs.SelectedTab = studyAbroad_tab;
            studyAbroad_title.Text = resources.studyAbroad.title;
            studyAbroad_desc.Text = resources.studyAbroad.description;
            for (int i = 0; i < resources.studyAbroad.places.Count; i++) {
                studyAbroad_places.AppendText("\u2022  " + resources.studyAbroad.places[i].nameOfPlace +
                    " -- " + resources.studyAbroad.places[i].description);
                if (i != resources.studyAbroad.places.Count - 1) {
                    studyAbroad_places.AppendText(Environment.NewLine);
                    studyAbroad_places.AppendText(Environment.NewLine);
                }
            }
        }

        // Loads forms resource
        private void loadForms() {
            // Show and populate Forms tab
            popup_tabs.SelectedTab = forms_tab;
            forms_title.Text = resources.title;
            // Undergraduate forms
            Label ugLabel = new Label();
            ugLabel.Text = "Undergraduate Forms";
            forms_list.Controls.Add(ugLabel);
            for (int i = 0; i < resources.forms.undergraduateForms.Count; i++) {
                LinkLabel form = new LinkLabel();
                form.Text = resources.forms.undergraduateForms[i].formName;
                form.Tag = resources.forms.undergraduateForms[i].href;
                forms_list.Controls.Add(form);
            }
            // Graduate forms 
            Label gradLabel = new Label();
            gradLabel.Text = "Graduate Forms";
            forms_list.Controls.Add(gradLabel);
            for (int i = 0; i < resources.forms.graduateForms.Count; i++) {
                LinkLabel form = new LinkLabel();
                form.Text = resources.forms.graduateForms[i].formName;
                form.Tag = resources.forms.graduateForms[i].href;
                forms_list.Controls.Add(form);
            }
        }

        // Utility event handler to resize richtextboxes
        private void rtb_ContentsResized(object sender, ContentsResizedEventArgs e) {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }
    }
}
