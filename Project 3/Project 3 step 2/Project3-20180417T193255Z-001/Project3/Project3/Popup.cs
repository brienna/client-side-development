﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3 {
    public partial class Popup : Form {
        // Initialize globals 
        Resources resources;
        string type;

        public Popup() {
            InitializeComponent();
        }

        public Popup(string typeParam, Resources r) {
            InitializeComponent();
            resources = r;
            type = typeParam;
        }

        public Popup(String type) {
            InitializeComponent();
        }

        private void Popup_Load(object sender, EventArgs e) {
            // Hide tab headers
            popup_tabs.Appearance = TabAppearance.FlatButtons;
            popup_tabs.ItemSize = new Size(0, 1);
            popup_tabs.SizeMode = TabSizeMode.Fixed;

            // Based on type of form specified, show view 
            if (type == "Forms") {
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
            } else if (type == resources.studyAbroad.title) {
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
            } else if (type == resources.studentServices.title) {
                // Show and populate Advising tab
                popup_tabs.SelectedTab = advising_tab;

                // Academic advisors
                Label aaLabel = new Label();
                aaLabel.Text = resources.studentServices.academicAdvisors.title;
                aaLabel.Width = advising_panel.Width - (aaLabel.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                RichTextBox aaRtb = new RichTextBox();
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
            } else if (type == resources.tutorsAndLabInformation.title) {
                // Show and populate Tutors tab
                popup_tabs.SelectedTab = tutors_tab;
                Label tutors_title = new Label();
                tutors_title.Text = resources.tutorsAndLabInformation.title;
                tutors_title.Width = tutors_panel.Width - (tutors_title.Margin.Right * 2);
                RichTextBox tutors_desc = new RichTextBox();
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
            } else if (type == resources.coopEnrollment.title) {
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
                    infoDesc.Text = info.description;
                    infoDesc.ContentsResized += rtb_ContentsResized;
                    infoDesc.Width = coop_panel.Width - (infoDesc.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                    coop_panel.Controls.Add(infoTitle);
                    coop_panel.Controls.Add(infoDesc);
                }
            } else if (type == resources.studentAmbassadors.title) {
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
                note.Text = resources.studentAmbassadors.note;
                note.Width = ambassadors_panel.Width - (note.Margin.Right * 2) - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                note.ContentsResized += rtb_ContentsResized;
                ambassadors_panel.Controls.Add(appForm);
                ambassadors_panel.Controls.Add(note);
            }
        }

        // Utility event handler to resize richtextboxes
        private void rtb_ContentsResized(object sender, ContentsResizedEventArgs e) {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }
    }
}
