namespace Class3FileIO
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combo_file = new System.Windows.Forms.ComboBox();
            this.button_file = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gb_XML = new System.Windows.Forms.GroupBox();
            this.combo_xml = new System.Windows.Forms.ComboBox();
            this.button_xml = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combo_json = new System.Windows.Forms.ComboBox();
            this.button_json = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.gb_XML.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combo_file);
            this.groupBox1.Controls.Add(this.button_file);
            this.groupBox1.Location = new System.Drawing.Point(226, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 169);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load the file";
            this.toolTip1.SetToolTip(this.groupBox1, "Howdy");
            // 
            // combo_file
            // 
            this.combo_file.FormattingEnabled = true;
            this.combo_file.Location = new System.Drawing.Point(31, 55);
            this.combo_file.Name = "combo_file";
            this.combo_file.Size = new System.Drawing.Size(121, 21);
            this.combo_file.TabIndex = 1;
            // 
            // button_file
            // 
            this.button_file.Location = new System.Drawing.Point(57, 111);
            this.button_file.Name = "button_file";
            this.button_file.Size = new System.Drawing.Size(75, 23);
            this.button_file.TabIndex = 0;
            this.button_file.Text = "Load file";
            this.button_file.UseVisualStyleBackColor = true;
            this.button_file.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Tag = "";
            this.toolTip1.ToolTipTitle = "Howdy";
            // 
            // gb_XML
            // 
            this.gb_XML.Controls.Add(this.combo_xml);
            this.gb_XML.Controls.Add(this.button_xml);
            this.gb_XML.Location = new System.Drawing.Point(27, 88);
            this.gb_XML.Name = "gb_XML";
            this.gb_XML.Size = new System.Drawing.Size(193, 169);
            this.gb_XML.TabIndex = 3;
            this.gb_XML.TabStop = false;
            this.gb_XML.Text = "Load the XML";
            this.toolTip1.SetToolTip(this.gb_XML, "Howdy");
            // 
            // combo_xml
            // 
            this.combo_xml.FormattingEnabled = true;
            this.combo_xml.Location = new System.Drawing.Point(31, 55);
            this.combo_xml.Name = "combo_xml";
            this.combo_xml.Size = new System.Drawing.Size(121, 21);
            this.combo_xml.TabIndex = 1;
            // 
            // button_xml
            // 
            this.button_xml.Location = new System.Drawing.Point(57, 111);
            this.button_xml.Name = "button_xml";
            this.button_xml.Size = new System.Drawing.Size(75, 23);
            this.button_xml.TabIndex = 0;
            this.button_xml.Text = "Load XML";
            this.button_xml.UseVisualStyleBackColor = true;
            this.button_xml.Click += new System.EventHandler(this.button_xml_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combo_json);
            this.groupBox2.Controls.Add(this.button_json);
            this.groupBox2.Location = new System.Drawing.Point(425, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 169);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load the JSON";
            this.toolTip1.SetToolTip(this.groupBox2, "Howdy");
            // 
            // combo_json
            // 
            this.combo_json.FormattingEnabled = true;
            this.combo_json.Location = new System.Drawing.Point(31, 55);
            this.combo_json.Name = "combo_json";
            this.combo_json.Size = new System.Drawing.Size(121, 21);
            this.combo_json.TabIndex = 1;
            // 
            // button_json
            // 
            this.button_json.Location = new System.Drawing.Point(57, 111);
            this.button_json.Name = "button_json";
            this.button_json.Size = new System.Drawing.Size(75, 23);
            this.button_json.TabIndex = 0;
            this.button_json.Text = "Load file";
            this.button_json.UseVisualStyleBackColor = true;
            this.button_json.Click += new System.EventHandler(this.button_json_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // meToolStripMenuItem
            // 
            this.meToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nowToolStripMenuItem});
            this.meToolStripMenuItem.Name = "meToolStripMenuItem";
            this.meToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.meToolStripMenuItem.Text = "Me";
            // 
            // nowToolStripMenuItem
            // 
            this.nowToolStripMenuItem.Name = "nowToolStripMenuItem";
            this.nowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.nowToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.nowToolStripMenuItem.Text = "Now";
            this.nowToolStripMenuItem.Click += new System.EventHandler(this.nowToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 585);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_XML);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.gb_XML.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combo_file;
        private System.Windows.Forms.Button button_file;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.GroupBox gb_XML;
        private System.Windows.Forms.ComboBox combo_xml;
        private System.Windows.Forms.Button button_xml;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combo_json;
        private System.Windows.Forms.Button button_json;
    }
}

