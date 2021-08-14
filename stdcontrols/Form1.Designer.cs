namespace stdcontrols
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxTask1Path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelStatus1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxC1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxC2 = new System.Windows.Forms.CheckBox();
            this.button7 = new System.Windows.Forms.Button();
            this.textBoxTask2Path = new System.Windows.Forms.TextBox();
            this.labelStatus2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonSendCommand = new System.Windows.Forms.Button();
            this.textBoxSendCommand = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 69);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(326, 384);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDoubleClick);
            // 
            // textBoxTask1Path
            // 
            this.textBoxTask1Path.Location = new System.Drawing.Point(70, 30);
            this.textBoxTask1Path.Name = "textBoxTask1Path";
            this.textBoxTask1Path.Size = new System.Drawing.Size(100, 20);
            this.textBoxTask1Path.TabIndex = 6;
            this.textBoxTask1Path.Text = "d:\\Work_vve\\!Services\\mp_av\\MySeenParserBot.exe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Task Path";
            // 
            // labelStatus1
            // 
            this.labelStatus1.AutoSize = true;
            this.labelStatus1.Location = new System.Drawing.Point(96, 14);
            this.labelStatus1.Name = "labelStatus1";
            this.labelStatus1.Size = new System.Drawing.Size(61, 13);
            this.labelStatus1.TabIndex = 8;
            this.labelStatus1.Text = "Not Started";
            this.labelStatus1.TextChanged += new System.EventHandler(this.labelStatus1_TextChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(176, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "Start";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.TextChanged += new System.EventHandler(this.button6_TextChanged);
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxC1);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.textBoxTask1Path);
            this.groupBox1.Controls.Add(this.labelStatus1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(375, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 80);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Task 1";
            // 
            // checkBoxC1
            // 
            this.checkBoxC1.AutoSize = true;
            this.checkBoxC1.Checked = true;
            this.checkBoxC1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxC1.Location = new System.Drawing.Point(7, 57);
            this.checkBoxC1.Name = "checkBoxC1";
            this.checkBoxC1.Size = new System.Drawing.Size(73, 17);
            this.checkBoxC1.TabIndex = 10;
            this.checkBoxC1.Text = "Command";
            this.checkBoxC1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxC2);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.textBoxTask2Path);
            this.groupBox2.Controls.Add(this.labelStatus2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(375, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 80);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Task 2";
            // 
            // checkBoxC2
            // 
            this.checkBoxC2.AutoSize = true;
            this.checkBoxC2.Checked = true;
            this.checkBoxC2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxC2.Location = new System.Drawing.Point(11, 57);
            this.checkBoxC2.Name = "checkBoxC2";
            this.checkBoxC2.Size = new System.Drawing.Size(73, 17);
            this.checkBoxC2.TabIndex = 11;
            this.checkBoxC2.Text = "Command";
            this.checkBoxC2.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(176, 28);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 9;
            this.button7.Text = "Start";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.TextChanged += new System.EventHandler(this.button7_TextChanged);
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBoxTask2Path
            // 
            this.textBoxTask2Path.Location = new System.Drawing.Point(70, 30);
            this.textBoxTask2Path.Name = "textBoxTask2Path";
            this.textBoxTask2Path.Size = new System.Drawing.Size(100, 20);
            this.textBoxTask2Path.TabIndex = 6;
            this.textBoxTask2Path.Text = "d:\\Work_vve\\!Services\\mp_kufar\\MySeenParserBot.exe";
            // 
            // labelStatus2
            // 
            this.labelStatus2.AutoSize = true;
            this.labelStatus2.Location = new System.Drawing.Point(96, 14);
            this.labelStatus2.Name = "labelStatus2";
            this.labelStatus2.Size = new System.Drawing.Size(61, 13);
            this.labelStatus2.TabIndex = 8;
            this.labelStatus2.Text = "Not Started";
            this.labelStatus2.TextChanged += new System.EventHandler(this.labelStatus2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Task Path";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonSendCommand);
            this.groupBox3.Controls.Add(this.textBoxSendCommand);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(362, 353);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(554, 100);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send command";
            // 
            // buttonSendCommand
            // 
            this.buttonSendCommand.Location = new System.Drawing.Point(176, 11);
            this.buttonSendCommand.Name = "buttonSendCommand";
            this.buttonSendCommand.Size = new System.Drawing.Size(75, 23);
            this.buttonSendCommand.TabIndex = 13;
            this.buttonSendCommand.Text = "Send";
            this.buttonSendCommand.UseVisualStyleBackColor = true;
            this.buttonSendCommand.Click += new System.EventHandler(this.buttonSendCommand_Click);
            // 
            // textBoxSendCommand
            // 
            this.textBoxSendCommand.Location = new System.Drawing.Point(70, 13);
            this.textBoxSendCommand.Name = "textBoxSendCommand";
            this.textBoxSendCommand.Size = new System.Drawing.Size(100, 20);
            this.textBoxSendCommand.TabIndex = 9;
            this.textBoxSendCommand.Text = "Ping";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Command";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 465);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBoxTask1Path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStatus1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBoxTask2Path;
        private System.Windows.Forms.Label labelStatus2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxC1;
        private System.Windows.Forms.CheckBox checkBoxC2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonSendCommand;
        private System.Windows.Forms.TextBox textBoxSendCommand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

