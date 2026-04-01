namespace MM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelOk = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLoses = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelOverall = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLatestData = new System.Windows.Forms.Label();
            this.labelLatestText = new System.Windows.Forms.Label();
            this.button_ping = new System.Windows.Forms.Button();
            this.richTextBoxDebug = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.checkBoxReverseLogs = new System.Windows.Forms.CheckBox();
            this.VPN = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "-1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelOk);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelLoses);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelOverall);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelLatestData);
            this.groupBox1.Controls.Add(this.labelLatestText);
            this.groupBox1.Controls.Add(this.button_ping);
            this.groupBox1.Location = new System.Drawing.Point(407, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 170);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ping";
            // 
            // labelOk
            // 
            this.labelOk.AutoSize = true;
            this.labelOk.Location = new System.Drawing.Point(150, 19);
            this.labelOk.Name = "labelOk";
            this.labelOk.Size = new System.Drawing.Size(18, 15);
            this.labelOk.TabIndex = 10;
            this.labelOk.Text = "-1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Ok:";
            // 
            // labelLoses
            // 
            this.labelLoses.AutoSize = true;
            this.labelLoses.Location = new System.Drawing.Point(150, 42);
            this.labelLoses.Name = "labelLoses";
            this.labelLoses.Size = new System.Drawing.Size(18, 15);
            this.labelLoses.TabIndex = 8;
            this.labelLoses.Text = "-1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Loses:";
            // 
            // labelOverall
            // 
            this.labelOverall.AutoSize = true;
            this.labelOverall.Location = new System.Drawing.Point(53, 19);
            this.labelOverall.Name = "labelOverall";
            this.labelOverall.Size = new System.Drawing.Size(18, 15);
            this.labelOverall.TabIndex = 6;
            this.labelOverall.Text = "-1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Overall:";
            // 
            // labelLatestData
            // 
            this.labelLatestData.AutoSize = true;
            this.labelLatestData.Location = new System.Drawing.Point(53, 113);
            this.labelLatestData.Name = "labelLatestData";
            this.labelLatestData.Size = new System.Drawing.Size(18, 15);
            this.labelLatestData.TabIndex = 4;
            this.labelLatestData.Text = "-1";
            // 
            // labelLatestText
            // 
            this.labelLatestText.AutoSize = true;
            this.labelLatestText.Location = new System.Drawing.Point(6, 113);
            this.labelLatestText.Name = "labelLatestText";
            this.labelLatestText.Size = new System.Drawing.Size(41, 15);
            this.labelLatestText.TabIndex = 3;
            this.labelLatestText.Text = "Latest:";
            // 
            // button_ping
            // 
            this.button_ping.Location = new System.Drawing.Point(6, 141);
            this.button_ping.Name = "button_ping";
            this.button_ping.Size = new System.Drawing.Size(75, 23);
            this.button_ping.TabIndex = 3;
            this.button_ping.Text = "Start";
            this.button_ping.UseVisualStyleBackColor = true;
            this.button_ping.Click += new System.EventHandler(this.button_ping_Click);
            // 
            // richTextBoxDebug
            // 
            this.richTextBoxDebug.Location = new System.Drawing.Point(12, 162);
            this.richTextBoxDebug.Name = "richTextBoxDebug";
            this.richTextBoxDebug.Size = new System.Drawing.Size(389, 150);
            this.richTextBoxDebug.TabIndex = 3;
            this.richTextBoxDebug.Text = "";
            this.richTextBoxDebug.TextChanged += new System.EventHandler(this.richTextBoxDebug_TextChanged);
            this.richTextBoxDebug.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.richTextBoxDebug_MouseDoubleClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(218, 324);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(419, 150);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDoubleClick);
            // 
            // checkBoxReverseLogs
            // 
            this.checkBoxReverseLogs.AutoSize = true;
            this.checkBoxReverseLogs.Checked = true;
            this.checkBoxReverseLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReverseLogs.Location = new System.Drawing.Point(10, 446);
            this.checkBoxReverseLogs.Name = "checkBoxReverseLogs";
            this.checkBoxReverseLogs.Size = new System.Drawing.Size(91, 19);
            this.checkBoxReverseLogs.TabIndex = 5;
            this.checkBoxReverseLogs.Text = "Reverse logs";
            this.checkBoxReverseLogs.UseVisualStyleBackColor = true;
            this.checkBoxReverseLogs.CheckedChanged += new System.EventHandler(this.checkBoxReverseLogs_CheckedChanged);
            // 
            // VPN
            // 
            this.VPN.Location = new System.Drawing.Point(557, 15);
            this.VPN.Name = "VPN";
            this.VPN.Size = new System.Drawing.Size(75, 23);
            this.VPN.TabIndex = 6;
            this.VPN.Text = "VPN";
            this.VPN.UseVisualStyleBackColor = true;
            this.VPN.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(148, 51);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(403, 105);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(557, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 106);
            this.button2.TabIndex = 8;
            this.button2.Text = "Git clean";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 477);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.VPN);
            this.Controls.Add(this.checkBoxReverseLogs);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.richTextBoxDebug);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelLatestData;
        private System.Windows.Forms.Label labelLatestText;
        private System.Windows.Forms.Button button_ping;
        private System.Windows.Forms.RichTextBox richTextBoxDebug;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label labelLoses;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelOverall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxReverseLogs;
        private System.Windows.Forms.Button VPN;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button2;
    }
}
