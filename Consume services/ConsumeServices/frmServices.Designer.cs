namespace ConsumeServices
{
    partial class frmServices
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_xml = new System.Windows.Forms.Button();
            this.btn_soap = new System.Windows.Forms.Button();
            this.btn_rest = new System.Windows.Forms.Button();
            this.cb_xml = new System.Windows.Forms.ComboBox();
            this.cb_soap = new System.Windows.Forms.ComboBox();
            this.cb_rest = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.cb_xml);
            this.groupBox1.Controls.Add(this.btn_xml);
            this.groupBox1.Location = new System.Drawing.Point(143, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "XML-RPC";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.cb_soap);
            this.groupBox2.Controls.Add(this.btn_soap);
            this.groupBox2.Location = new System.Drawing.Point(393, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 205);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SOAP";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.cb_rest);
            this.groupBox3.Controls.Add(this.btn_rest);
            this.groupBox3.Location = new System.Drawing.Point(639, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 205);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RESTful";
            // 
            // btn_xml
            // 
            this.btn_xml.Location = new System.Drawing.Point(42, 148);
            this.btn_xml.Name = "btn_xml";
            this.btn_xml.Size = new System.Drawing.Size(93, 23);
            this.btn_xml.TabIndex = 3;
            this.btn_xml.Text = "Get XMLRPC";
            this.tip1.SetToolTip(this.btn_xml, "Click");
            this.btn_xml.UseVisualStyleBackColor = true;
            this.btn_xml.Click += new System.EventHandler(this.btn_xml_Click);
            // 
            // btn_soap
            // 
            this.btn_soap.Location = new System.Drawing.Point(63, 159);
            this.btn_soap.Name = "btn_soap";
            this.btn_soap.Size = new System.Drawing.Size(75, 23);
            this.btn_soap.TabIndex = 0;
            this.btn_soap.Text = "Get SOAP";
            this.btn_soap.UseVisualStyleBackColor = true;
            this.btn_soap.Click += new System.EventHandler(this.btn_soap_Click);
            // 
            // btn_rest
            // 
            this.btn_rest.Location = new System.Drawing.Point(59, 164);
            this.btn_rest.Name = "btn_rest";
            this.btn_rest.Size = new System.Drawing.Size(75, 23);
            this.btn_rest.TabIndex = 0;
            this.btn_rest.Text = "Get REST";
            this.btn_rest.UseVisualStyleBackColor = true;
            this.btn_rest.Click += new System.EventHandler(this.btn_rest_Click);
            // 
            // cb_xml
            // 
            this.cb_xml.FormattingEnabled = true;
            this.cb_xml.Location = new System.Drawing.Point(42, 50);
            this.cb_xml.Name = "cb_xml";
            this.cb_xml.Size = new System.Drawing.Size(121, 21);
            this.cb_xml.TabIndex = 4;
            // 
            // cb_soap
            // 
            this.cb_soap.FormattingEnabled = true;
            this.cb_soap.Location = new System.Drawing.Point(46, 65);
            this.cb_soap.Name = "cb_soap";
            this.cb_soap.Size = new System.Drawing.Size(121, 21);
            this.cb_soap.TabIndex = 1;
            // 
            // cb_rest
            // 
            this.cb_rest.FormattingEnabled = true;
            this.cb_rest.Location = new System.Drawing.Point(27, 66);
            this.cb_rest.Name = "cb_rest";
            this.cb_rest.Size = new System.Drawing.Size(121, 21);
            this.cb_rest.TabIndex = 1;
            // 
            // frmServices
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 436);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmServices";
            this.Text = "sdfadsf";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cb_xml;
        private System.Windows.Forms.Button btn_xml;
        private System.Windows.Forms.ComboBox cb_soap;
        private System.Windows.Forms.Button btn_soap;
        private System.Windows.Forms.ComboBox cb_rest;
        private System.Windows.Forms.Button btn_rest;
        private System.Windows.Forms.ToolTip tip1;
    }
}

