namespace Asyncs
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
            this.buttonStartTask = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStopTask = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartTask
            // 
            this.buttonStartTask.Location = new System.Drawing.Point(17, 28);
            this.buttonStartTask.Name = "buttonStartTask";
            this.buttonStartTask.Size = new System.Drawing.Size(75, 23);
            this.buttonStartTask.TabIndex = 0;
            this.buttonStartTask.Text = "Start Task";
            this.buttonStartTask.UseVisualStyleBackColor = true;
            this.buttonStartTask.Click += new System.EventHandler(this.buttonStartTask_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonStopTask);
            this.groupBox1.Controls.Add(this.buttonStartTask);
            this.groupBox1.Location = new System.Drawing.Point(27, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Async with cancel";
            // 
            // buttonStopTask
            // 
            this.buttonStopTask.Enabled = false;
            this.buttonStopTask.Location = new System.Drawing.Point(17, 57);
            this.buttonStopTask.Name = "buttonStopTask";
            this.buttonStopTask.Size = new System.Drawing.Size(75, 23);
            this.buttonStopTask.TabIndex = 1;
            this.buttonStopTask.Text = "Stop Task";
            this.buttonStopTask.UseVisualStyleBackColor = true;
            this.buttonStopTask.Click += new System.EventHandler(this.buttonStopTask_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStartTask;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStopTask;
    }
}

