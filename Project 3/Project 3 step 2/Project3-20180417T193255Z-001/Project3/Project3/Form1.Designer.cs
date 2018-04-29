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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.tab_people = new System.Windows.Forms.TabPage();
            this.tab_degrees = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tab_resources = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.resources_btn = new System.Windows.Forms.Button();
            this.emp_btn = new System.Windows.Forms.Button();
            this.research_btn = new System.Windows.Forms.Button();
            this.people_btn = new System.Windows.Forms.Button();
            this.degrees_btn = new System.Windows.Forms.Button();
            this.about_btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.body = new System.Windows.Forms.TabControl();
            this.about_tab = new System.Windows.Forms.TabPage();
            this.degrees_tab = new System.Windows.Forms.TabPage();
            this.people_tab = new System.Windows.Forms.TabPage();
            this.research_tab = new System.Windows.Forms.TabPage();
            this.emp_tab = new System.Windows.Forms.TabPage();
            this.resources_tab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_fac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.tab_people.SuspendLayout();
            this.tab_degrees.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.body.SuspendLayout();
            this.about_tab.SuspendLayout();
            this.degrees_tab.SuspendLayout();
            this.people_tab.SuspendLayout();
            this.research_tab.SuspendLayout();
            this.emp_tab.SuspendLayout();
            this.resources_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_about_desc
            // 
            this.rtb_about_desc.AccessibleName = "";
            this.rtb_about_desc.Location = new System.Drawing.Point(19, 24);
            this.rtb_about_desc.Name = "rtb_about_desc";
            this.rtb_about_desc.Size = new System.Drawing.Size(310, 153);
            this.rtb_about_desc.TabIndex = 1;
            this.rtb_about_desc.Text = "";
            // 
            // ll_pdf
            // 
            this.ll_pdf.AutoSize = true;
            this.ll_pdf.Location = new System.Drawing.Point(285, 194);
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
            this.ll_resources_appForm.Location = new System.Drawing.Point(368, 92);
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
            this.TabControl1.Controls.Add(this.tab_people);
            this.TabControl1.Controls.Add(this.tab_degrees);
            this.TabControl1.Controls.Add(this.tabPage5);
            this.TabControl1.Controls.Add(this.tabPage6);
            this.TabControl1.Controls.Add(this.tab_resources);
            this.TabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TabControl1.ItemSize = new System.Drawing.Size(25, 50);
            this.TabControl1.Location = new System.Drawing.Point(84, 46);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(418, 248);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.TabControl1.TabIndex = 10;
            // 
            // tab_people
            // 
            this.tab_people.Controls.Add(this.btn_people);
            this.tab_people.Controls.Add(this.pb_fac);
            this.tab_people.Location = new System.Drawing.Point(4, 54);
            this.tab_people.Name = "tab_people";
            this.tab_people.Padding = new System.Windows.Forms.Padding(3);
            this.tab_people.Size = new System.Drawing.Size(410, 190);
            this.tab_people.TabIndex = 0;
            this.tab_people.Text = "People";
            this.tab_people.UseVisualStyleBackColor = true;
            // 
            // tab_degrees
            // 
            this.tab_degrees.Controls.Add(this.listView2);
            this.tab_degrees.Location = new System.Drawing.Point(4, 54);
            this.tab_degrees.Name = "tab_degrees";
            this.tab_degrees.Padding = new System.Windows.Forms.Padding(3);
            this.tab_degrees.Size = new System.Drawing.Size(410, 190);
            this.tab_degrees.TabIndex = 1;
            this.tab_degrees.Text = "Degrees";
            this.tab_degrees.UseVisualStyleBackColor = true;
            this.tab_degrees.Enter += new System.EventHandler(this.tab_degrees_Enter);
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(25, 33);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(607, 217);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.DataGridView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 54);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(410, 190);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "DataGridView";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.Enter += new System.EventHandler(this.tabPage5_Enter);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.listView1);
            this.tabPage6.Controls.Add(this.btn_ListView);
            this.tabPage6.Location = new System.Drawing.Point(4, 54);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(410, 190);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "ListView";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tab_resources
            // 
            this.tab_resources.Location = new System.Drawing.Point(4, 54);
            this.tab_resources.Name = "tab_resources";
            this.tab_resources.Padding = new System.Windows.Forms.Padding(3);
            this.tab_resources.Size = new System.Drawing.Size(410, 190);
            this.tab_resources.TabIndex = 6;
            this.tab_resources.Text = "Resources";
            this.tab_resources.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.resources_btn);
            this.panel1.Controls.Add(this.emp_btn);
            this.panel1.Controls.Add(this.research_btn);
            this.panel1.Controls.Add(this.people_btn);
            this.panel1.Controls.Add(this.degrees_btn);
            this.panel1.Controls.Add(this.about_btn);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 549);
            this.panel1.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 458);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 91);
            this.panel3.TabIndex = 12;
            // 
            // resources_btn
            // 
            this.resources_btn.FlatAppearance.BorderSize = 0;
            this.resources_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.resources_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.resources_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resources_btn.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resources_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.resources_btn.Location = new System.Drawing.Point(0, 308);
            this.resources_btn.Name = "resources_btn";
            this.resources_btn.Size = new System.Drawing.Size(200, 43);
            this.resources_btn.TabIndex = 6;
            this.resources_btn.Text = "RESOURCES";
            this.resources_btn.UseVisualStyleBackColor = true;
            this.resources_btn.Click += new System.EventHandler(this.resources_btn_Click);
            this.resources_btn.MouseHover += new System.EventHandler(this.about_btn_MouseHover);
            // 
            // emp_btn
            // 
            this.emp_btn.FlatAppearance.BorderSize = 0;
            this.emp_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.emp_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.emp_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emp_btn.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emp_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.emp_btn.Location = new System.Drawing.Point(0, 266);
            this.emp_btn.Name = "emp_btn";
            this.emp_btn.Size = new System.Drawing.Size(200, 43);
            this.emp_btn.TabIndex = 5;
            this.emp_btn.Text = "EMPLOYMENT";
            this.emp_btn.UseVisualStyleBackColor = true;
            this.emp_btn.Click += new System.EventHandler(this.emp_btn_Click);
            this.emp_btn.MouseHover += new System.EventHandler(this.about_btn_MouseHover);
            // 
            // research_btn
            // 
            this.research_btn.FlatAppearance.BorderSize = 0;
            this.research_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.research_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.research_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.research_btn.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.research_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.research_btn.Location = new System.Drawing.Point(0, 224);
            this.research_btn.Name = "research_btn";
            this.research_btn.Size = new System.Drawing.Size(200, 43);
            this.research_btn.TabIndex = 4;
            this.research_btn.Text = "RESEARCH";
            this.research_btn.UseVisualStyleBackColor = true;
            this.research_btn.Click += new System.EventHandler(this.research_btn_Click);
            this.research_btn.MouseHover += new System.EventHandler(this.about_btn_MouseHover);
            // 
            // people_btn
            // 
            this.people_btn.FlatAppearance.BorderSize = 0;
            this.people_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.people_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.people_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.people_btn.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.people_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.people_btn.Location = new System.Drawing.Point(0, 182);
            this.people_btn.Name = "people_btn";
            this.people_btn.Size = new System.Drawing.Size(200, 43);
            this.people_btn.TabIndex = 3;
            this.people_btn.Text = "PEOPLE";
            this.people_btn.UseVisualStyleBackColor = true;
            this.people_btn.Click += new System.EventHandler(this.people_btn_Click);
            this.people_btn.MouseHover += new System.EventHandler(this.about_btn_MouseHover);
            // 
            // degrees_btn
            // 
            this.degrees_btn.FlatAppearance.BorderSize = 0;
            this.degrees_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.degrees_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.degrees_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.degrees_btn.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.degrees_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.degrees_btn.Location = new System.Drawing.Point(0, 141);
            this.degrees_btn.Name = "degrees_btn";
            this.degrees_btn.Size = new System.Drawing.Size(200, 43);
            this.degrees_btn.TabIndex = 2;
            this.degrees_btn.Text = "DEGREES";
            this.degrees_btn.UseVisualStyleBackColor = true;
            this.degrees_btn.Click += new System.EventHandler(this.degrees_btn_Click);
            this.degrees_btn.MouseHover += new System.EventHandler(this.about_btn_MouseHover);
            // 
            // about_btn
            // 
            this.about_btn.FlatAppearance.BorderSize = 0;
            this.about_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.about_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.about_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.about_btn.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.about_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.about_btn.Location = new System.Drawing.Point(0, 99);
            this.about_btn.Name = "about_btn";
            this.about_btn.Size = new System.Drawing.Size(200, 43);
            this.about_btn.TabIndex = 1;
            this.about_btn.Text = "ABOUT";
            this.about_btn.UseVisualStyleBackColor = true;
            this.about_btn.Click += new System.EventHandler(this.about_btn_Click);
            this.about_btn.MouseHover += new System.EventHandler(this.about_btn_MouseHover);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 58);
            this.panel2.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(50, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // body
            // 
            this.body.Controls.Add(this.about_tab);
            this.body.Controls.Add(this.degrees_tab);
            this.body.Controls.Add(this.people_tab);
            this.body.Controls.Add(this.research_tab);
            this.body.Controls.Add(this.emp_tab);
            this.body.Controls.Add(this.resources_tab);
            this.body.Location = new System.Drawing.Point(218, 12);
            this.body.Name = "body";
            this.body.SelectedIndex = 0;
            this.body.Size = new System.Drawing.Size(581, 525);
            this.body.TabIndex = 12;
            // 
            // about_tab
            // 
            this.about_tab.Controls.Add(this.TabControl1);
            this.about_tab.Location = new System.Drawing.Point(4, 22);
            this.about_tab.Name = "about_tab";
            this.about_tab.Padding = new System.Windows.Forms.Padding(3);
            this.about_tab.Size = new System.Drawing.Size(573, 499);
            this.about_tab.TabIndex = 0;
            this.about_tab.Text = "About";
            this.about_tab.UseVisualStyleBackColor = true;
            // 
            // degrees_tab
            // 
            this.degrees_tab.Controls.Add(this.label5);
            this.degrees_tab.Controls.Add(this.rtb_about_desc);
            this.degrees_tab.Controls.Add(this.ll_resources_appForm);
            this.degrees_tab.Controls.Add(this.ll_pdf);
            this.degrees_tab.Location = new System.Drawing.Point(4, 22);
            this.degrees_tab.Name = "degrees_tab";
            this.degrees_tab.Padding = new System.Windows.Forms.Padding(3);
            this.degrees_tab.Size = new System.Drawing.Size(573, 499);
            this.degrees_tab.TabIndex = 1;
            this.degrees_tab.Text = "Degrees";
            this.degrees_tab.UseVisualStyleBackColor = true;
            // 
            // people_tab
            // 
            this.people_tab.Controls.Add(this.label4);
            this.people_tab.Location = new System.Drawing.Point(4, 22);
            this.people_tab.Name = "people_tab";
            this.people_tab.Padding = new System.Windows.Forms.Padding(3);
            this.people_tab.Size = new System.Drawing.Size(573, 499);
            this.people_tab.TabIndex = 2;
            this.people_tab.Text = "People";
            this.people_tab.UseVisualStyleBackColor = true;
            // 
            // research_tab
            // 
            this.research_tab.Controls.Add(this.label1);
            this.research_tab.Location = new System.Drawing.Point(4, 22);
            this.research_tab.Name = "research_tab";
            this.research_tab.Padding = new System.Windows.Forms.Padding(3);
            this.research_tab.Size = new System.Drawing.Size(573, 499);
            this.research_tab.TabIndex = 3;
            this.research_tab.Text = "Research";
            this.research_tab.UseVisualStyleBackColor = true;
            // 
            // emp_tab
            // 
            this.emp_tab.Controls.Add(this.label2);
            this.emp_tab.Location = new System.Drawing.Point(4, 22);
            this.emp_tab.Name = "emp_tab";
            this.emp_tab.Padding = new System.Windows.Forms.Padding(3);
            this.emp_tab.Size = new System.Drawing.Size(573, 499);
            this.emp_tab.TabIndex = 4;
            this.emp_tab.Text = "Employment";
            this.emp_tab.UseVisualStyleBackColor = true;
            // 
            // resources_tab
            // 
            this.resources_tab.Controls.Add(this.label3);
            this.resources_tab.Location = new System.Drawing.Point(4, 22);
            this.resources_tab.Name = "resources_tab";
            this.resources_tab.Padding = new System.Windows.Forms.Padding(3);
            this.resources_tab.Size = new System.Drawing.Size(573, 499);
            this.resources_tab.TabIndex = 5;
            this.resources_tab.Text = "Resources";
            this.resources_tab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Research";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Employment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Resources";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "People";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Degrees";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(811, 549);
            this.Controls.Add(this.body);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Information Sciences and Technologies @ RIT";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_fac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.tab_people.ResumeLayout(false);
            this.tab_people.PerformLayout();
            this.tab_degrees.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.body.ResumeLayout(false);
            this.about_tab.ResumeLayout(false);
            this.degrees_tab.ResumeLayout(false);
            this.degrees_tab.PerformLayout();
            this.people_tab.ResumeLayout(false);
            this.people_tab.PerformLayout();
            this.research_tab.ResumeLayout(false);
            this.research_tab.PerformLayout();
            this.emp_tab.ResumeLayout(false);
            this.emp_tab.PerformLayout();
            this.resources_tab.ResumeLayout(false);
            this.resources_tab.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabPage tab_people;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tab_resources;
        private System.Windows.Forms.ListView listView2;
        protected internal System.Windows.Forms.TabPage tab_degrees;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button about_btn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button degrees_btn;
        private System.Windows.Forms.Button people_btn;
        private System.Windows.Forms.Button resources_btn;
        private System.Windows.Forms.Button emp_btn;
        private System.Windows.Forms.Button research_btn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl body;
        private System.Windows.Forms.TabPage about_tab;
        private System.Windows.Forms.TabPage degrees_tab;
        private System.Windows.Forms.TabPage people_tab;
        private System.Windows.Forms.TabPage research_tab;
        private System.Windows.Forms.TabPage emp_tab;
        private System.Windows.Forms.TabPage resources_tab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

