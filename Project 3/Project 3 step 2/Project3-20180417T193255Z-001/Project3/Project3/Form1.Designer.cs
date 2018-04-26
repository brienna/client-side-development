namespace Project3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb_about_desc = new System.Windows.Forms.RichTextBox();
            this.ll_pdf = new System.Windows.Forms.LinkLabel();
            this.ll_resources_appForm = new System.Windows.Forms.LinkLabel();
            this.btn_people = new System.Windows.Forms.Button();
            this.pb_fac = new System.Windows.Forms.PictureBox();
            this.btn_ListView = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.emp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.city = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listView1 = new System.Windows.Forms.ListView();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pb_fac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_about_desc
            // 
            this.rtb_about_desc.AccessibleName = "";
            this.rtb_about_desc.Location = new System.Drawing.Point(12, 12);
            this.rtb_about_desc.Name = "rtb_about_desc";
            this.rtb_about_desc.Size = new System.Drawing.Size(310, 153);
            this.rtb_about_desc.TabIndex = 1;
            this.rtb_about_desc.Text = "";
            // 
            // ll_pdf
            // 
            this.ll_pdf.AutoSize = true;
            this.ll_pdf.Location = new System.Drawing.Point(420, 15);
            this.ll_pdf.Name = "ll_pdf";
            this.ll_pdf.Size = new System.Drawing.Size(98, 13);
            this.ll_pdf.TabIndex = 2;
            this.ll_pdf.TabStop = true;
            this.ll_pdf.Text = "Click me for the pdf";
            this.ll_pdf.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ll_resources_appForm
            // 
            this.ll_resources_appForm.AutoSize = true;
            this.ll_resources_appForm.Location = new System.Drawing.Point(548, 15);
            this.ll_resources_appForm.Name = "ll_resources_appForm";
            this.ll_resources_appForm.Size = new System.Drawing.Size(55, 13);
            this.ll_resources_appForm.TabIndex = 3;
            this.ll_resources_appForm.TabStop = true;
            this.ll_resources_appForm.Text = "linkLabel1";
            // 
            // btn_people
            // 
            this.btn_people.Location = new System.Drawing.Point(49, 44);
            this.btn_people.Name = "btn_people";
            this.btn_people.Size = new System.Drawing.Size(75, 23);
            this.btn_people.TabIndex = 4;
            this.btn_people.Text = "Get people";
            this.btn_people.UseVisualStyleBackColor = true;
            this.btn_people.Click += new System.EventHandler(this.btn_people_Click);
            // 
            // pb_fac
            // 
            this.pb_fac.Location = new System.Drawing.Point(49, 92);
            this.pb_fac.Name = "pb_fac";
            this.pb_fac.Size = new System.Drawing.Size(100, 50);
            this.pb_fac.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_fac.TabIndex = 5;
            this.pb_fac.TabStop = false;
            // 
            // btn_ListView
            // 
            this.btn_ListView.Location = new System.Drawing.Point(215, 211);
            this.btn_ListView.Name = "btn_ListView";
            this.btn_ListView.Size = new System.Drawing.Size(162, 23);
            this.btn_ListView.TabIndex = 7;
            this.btn_ListView.Text = "Populate ListView";
            this.btn_ListView.UseVisualStyleBackColor = true;
            this.btn_ListView.Click += new System.EventHandler(this.btn_ListView_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.emp,
            this.deg,
            this.city,
            this.term});
            this.DataGridView1.Location = new System.Drawing.Point(38, 72);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(549, 150);
            this.DataGridView1.TabIndex = 8;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // emp
            // 
            this.emp.HeaderText = "Employer";
            this.emp.Name = "emp";
            // 
            // deg
            // 
            this.deg.HeaderText = "Degree";
            this.deg.Name = "deg";
            // 
            // city
            // 
            this.city.HeaderText = "City";
            this.city.Name = "city";
            // 
            // term
            // 
            this.term.HeaderText = "Term";
            this.term.Name = "term";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(98, 13);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(279, 192);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.tabPage1);
            this.TabControl1.Controls.Add(this.tabPage2);
            this.TabControl1.Controls.Add(this.tabPage5);
            this.TabControl1.Controls.Add(this.tabPage6);
            this.TabControl1.Controls.Add(this.tabPage3);
            this.TabControl1.Location = new System.Drawing.Point(137, 245);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(656, 292);
            this.TabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_people);
            this.tabPage1.Controls.Add(this.pb_fac);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(648, 266);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "People";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(648, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Degrees";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.DataGridView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(648, 266);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "DataGridView";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.Enter += new System.EventHandler(this.tabPage5_Enter);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.listView1);
            this.tabPage6.Controls.Add(this.btn_ListView);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(648, 266);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "ListView";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(648, 266);
            this.tabPage3.TabIndex = 6;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 549);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.ll_resources_appForm);
            this.Controls.Add(this.ll_pdf);
            this.Controls.Add(this.rtb_about_desc);
            this.Name = "Form1";
            this.Text = "Degrees";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_fac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_about_desc;
        private System.Windows.Forms.LinkLabel ll_pdf;
        private System.Windows.Forms.LinkLabel ll_resources_appForm;
        private System.Windows.Forms.Button btn_people;
        private System.Windows.Forms.PictureBox pb_fac;
        private System.Windows.Forms.Button btn_ListView;
        private System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn emp;
        private System.Windows.Forms.DataGridViewTextBoxColumn deg;
        private System.Windows.Forms.DataGridViewTextBoxColumn city;
        private System.Windows.Forms.DataGridViewTextBoxColumn term;
        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage3;
    }
}

