﻿namespace Project3 {
    partial class Popup {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.popup_tabs = new System.Windows.Forms.TabControl();
            this.forms_tab = new System.Windows.Forms.TabPage();
            this.forms_list = new System.Windows.Forms.FlowLayoutPanel();
            this.forms_title = new System.Windows.Forms.Label();
            this.studyAbroad_tab = new System.Windows.Forms.TabPage();
            this.studyAbroad_desc = new System.Windows.Forms.RichTextBox();
            this.studyAbroad_places = new System.Windows.Forms.RichTextBox();
            this.studyAbroad_title = new System.Windows.Forms.Label();
            this.advising_tab = new System.Windows.Forms.TabPage();
            this.advising_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.tutors_tab = new System.Windows.Forms.TabPage();
            this.ambassadors_tab = new System.Windows.Forms.TabPage();
            this.coop_tab = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tutors_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.coop_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.ambassadors_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.popup_tabs.SuspendLayout();
            this.forms_tab.SuspendLayout();
            this.studyAbroad_tab.SuspendLayout();
            this.advising_tab.SuspendLayout();
            this.tutors_tab.SuspendLayout();
            this.ambassadors_tab.SuspendLayout();
            this.coop_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // popup_tabs
            // 
            this.popup_tabs.Controls.Add(this.forms_tab);
            this.popup_tabs.Controls.Add(this.studyAbroad_tab);
            this.popup_tabs.Controls.Add(this.advising_tab);
            this.popup_tabs.Controls.Add(this.tutors_tab);
            this.popup_tabs.Controls.Add(this.ambassadors_tab);
            this.popup_tabs.Controls.Add(this.coop_tab);
            this.popup_tabs.Controls.Add(this.tabPage7);
            this.popup_tabs.Controls.Add(this.tabPage8);
            this.popup_tabs.Location = new System.Drawing.Point(12, 12);
            this.popup_tabs.Name = "popup_tabs";
            this.popup_tabs.SelectedIndex = 0;
            this.popup_tabs.Size = new System.Drawing.Size(553, 434);
            this.popup_tabs.TabIndex = 0;
            // 
            // forms_tab
            // 
            this.forms_tab.Controls.Add(this.forms_list);
            this.forms_tab.Controls.Add(this.forms_title);
            this.forms_tab.Location = new System.Drawing.Point(4, 22);
            this.forms_tab.Name = "forms_tab";
            this.forms_tab.Padding = new System.Windows.Forms.Padding(3);
            this.forms_tab.Size = new System.Drawing.Size(545, 408);
            this.forms_tab.TabIndex = 0;
            this.forms_tab.Text = "Forms";
            this.forms_tab.UseVisualStyleBackColor = true;
            // 
            // forms_list
            // 
            this.forms_list.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.forms_list.Location = new System.Drawing.Point(120, 88);
            this.forms_list.Name = "forms_list";
            this.forms_list.Size = new System.Drawing.Size(326, 283);
            this.forms_list.TabIndex = 1;
            // 
            // forms_title
            // 
            this.forms_title.AutoSize = true;
            this.forms_title.Location = new System.Drawing.Point(233, 27);
            this.forms_title.Name = "forms_title";
            this.forms_title.Size = new System.Drawing.Size(35, 13);
            this.forms_title.TabIndex = 0;
            this.forms_title.Text = "label1";
            // 
            // studyAbroad_tab
            // 
            this.studyAbroad_tab.Controls.Add(this.studyAbroad_desc);
            this.studyAbroad_tab.Controls.Add(this.studyAbroad_places);
            this.studyAbroad_tab.Controls.Add(this.studyAbroad_title);
            this.studyAbroad_tab.Location = new System.Drawing.Point(4, 22);
            this.studyAbroad_tab.Name = "studyAbroad_tab";
            this.studyAbroad_tab.Padding = new System.Windows.Forms.Padding(3);
            this.studyAbroad_tab.Size = new System.Drawing.Size(545, 408);
            this.studyAbroad_tab.TabIndex = 1;
            this.studyAbroad_tab.Text = "Study Abroad";
            this.studyAbroad_tab.UseVisualStyleBackColor = true;
            // 
            // studyAbroad_desc
            // 
            this.studyAbroad_desc.Location = new System.Drawing.Point(33, 57);
            this.studyAbroad_desc.Name = "studyAbroad_desc";
            this.studyAbroad_desc.Size = new System.Drawing.Size(458, 96);
            this.studyAbroad_desc.TabIndex = 4;
            this.studyAbroad_desc.Text = "";
            this.studyAbroad_desc.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.rtb_ContentsResized);
            // 
            // studyAbroad_places
            // 
            this.studyAbroad_places.Location = new System.Drawing.Point(33, 172);
            this.studyAbroad_places.Name = "studyAbroad_places";
            this.studyAbroad_places.Size = new System.Drawing.Size(458, 120);
            this.studyAbroad_places.TabIndex = 3;
            this.studyAbroad_places.Text = "";
            this.studyAbroad_places.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.rtb_ContentsResized);
            // 
            // studyAbroad_title
            // 
            this.studyAbroad_title.AutoSize = true;
            this.studyAbroad_title.Location = new System.Drawing.Point(236, 26);
            this.studyAbroad_title.Name = "studyAbroad_title";
            this.studyAbroad_title.Size = new System.Drawing.Size(35, 13);
            this.studyAbroad_title.TabIndex = 0;
            this.studyAbroad_title.Text = "label1";
            // 
            // advising_tab
            // 
            this.advising_tab.Controls.Add(this.advising_panel);
            this.advising_tab.Location = new System.Drawing.Point(4, 22);
            this.advising_tab.Name = "advising_tab";
            this.advising_tab.Padding = new System.Windows.Forms.Padding(3);
            this.advising_tab.Size = new System.Drawing.Size(545, 408);
            this.advising_tab.TabIndex = 2;
            this.advising_tab.Text = "Advising";
            this.advising_tab.UseVisualStyleBackColor = true;
            // 
            // advising_panel
            // 
            this.advising_panel.AutoScroll = true;
            this.advising_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advising_panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.advising_panel.Location = new System.Drawing.Point(3, 3);
            this.advising_panel.Name = "advising_panel";
            this.advising_panel.Size = new System.Drawing.Size(539, 402);
            this.advising_panel.TabIndex = 0;
            this.advising_panel.WrapContents = false;
            // 
            // tutors_tab
            // 
            this.tutors_tab.Controls.Add(this.tutors_panel);
            this.tutors_tab.Location = new System.Drawing.Point(4, 22);
            this.tutors_tab.Name = "tutors_tab";
            this.tutors_tab.Padding = new System.Windows.Forms.Padding(3);
            this.tutors_tab.Size = new System.Drawing.Size(545, 408);
            this.tutors_tab.TabIndex = 3;
            this.tutors_tab.Text = "Tutors";
            this.tutors_tab.UseVisualStyleBackColor = true;
            // 
            // ambassadors_tab
            // 
            this.ambassadors_tab.Controls.Add(this.ambassadors_panel);
            this.ambassadors_tab.Location = new System.Drawing.Point(4, 22);
            this.ambassadors_tab.Name = "ambassadors_tab";
            this.ambassadors_tab.Padding = new System.Windows.Forms.Padding(3);
            this.ambassadors_tab.Size = new System.Drawing.Size(545, 408);
            this.ambassadors_tab.TabIndex = 4;
            this.ambassadors_tab.Text = "Ambassadors";
            this.ambassadors_tab.UseVisualStyleBackColor = true;
            // 
            // coop_tab
            // 
            this.coop_tab.Controls.Add(this.coop_panel);
            this.coop_tab.Location = new System.Drawing.Point(4, 22);
            this.coop_tab.Name = "coop_tab";
            this.coop_tab.Padding = new System.Windows.Forms.Padding(3);
            this.coop_tab.Size = new System.Drawing.Size(545, 408);
            this.coop_tab.TabIndex = 5;
            this.coop_tab.Text = "Coop";
            this.coop_tab.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(545, 408);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(545, 408);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tutors_panel
            // 
            this.tutors_panel.AutoScroll = true;
            this.tutors_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tutors_panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.tutors_panel.Location = new System.Drawing.Point(3, 3);
            this.tutors_panel.Name = "tutors_panel";
            this.tutors_panel.Size = new System.Drawing.Size(539, 402);
            this.tutors_panel.TabIndex = 0;
            this.tutors_panel.WrapContents = false;
            // 
            // coop_panel
            // 
            this.coop_panel.AutoScroll = true;
            this.coop_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coop_panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.coop_panel.Location = new System.Drawing.Point(3, 3);
            this.coop_panel.Name = "coop_panel";
            this.coop_panel.Size = new System.Drawing.Size(539, 402);
            this.coop_panel.TabIndex = 0;
            this.coop_panel.WrapContents = false;
            // 
            // ambassadors_panel
            // 
            this.ambassadors_panel.AutoScroll = true;
            this.ambassadors_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ambassadors_panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ambassadors_panel.Location = new System.Drawing.Point(3, 3);
            this.ambassadors_panel.Name = "ambassadors_panel";
            this.ambassadors_panel.Size = new System.Drawing.Size(539, 402);
            this.ambassadors_panel.TabIndex = 0;
            this.ambassadors_panel.WrapContents = false;
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 450);
            this.Controls.Add(this.popup_tabs);
            this.Name = "Popup";
            this.Text = "Popup";
            this.Load += new System.EventHandler(this.Popup_Load);
            this.popup_tabs.ResumeLayout(false);
            this.forms_tab.ResumeLayout(false);
            this.forms_tab.PerformLayout();
            this.studyAbroad_tab.ResumeLayout(false);
            this.studyAbroad_tab.PerformLayout();
            this.advising_tab.ResumeLayout(false);
            this.tutors_tab.ResumeLayout(false);
            this.ambassadors_tab.ResumeLayout(false);
            this.coop_tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl popup_tabs;
        private System.Windows.Forms.TabPage forms_tab;
        private System.Windows.Forms.TabPage studyAbroad_tab;
        private System.Windows.Forms.TabPage advising_tab;
        private System.Windows.Forms.TabPage tutors_tab;
        private System.Windows.Forms.TabPage ambassadors_tab;
        private System.Windows.Forms.TabPage coop_tab;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Label forms_title;
        private System.Windows.Forms.Label studyAbroad_title;
        private System.Windows.Forms.FlowLayoutPanel forms_list;
        private System.Windows.Forms.RichTextBox studyAbroad_places;
        private System.Windows.Forms.RichTextBox studyAbroad_desc;
        private System.Windows.Forms.FlowLayoutPanel advising_panel;
        private System.Windows.Forms.FlowLayoutPanel tutors_panel;
        private System.Windows.Forms.FlowLayoutPanel coop_panel;
        private System.Windows.Forms.FlowLayoutPanel ambassadors_panel;
    }
}