namespace CodeCompilation
{
    partial class MainForm
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
            this.buttonWork = new System.Windows.Forms.Button();
            this.buttonWork2 = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonWork
            // 
            this.buttonWork.Location = new System.Drawing.Point(13, 13);
            this.buttonWork.Name = "buttonWork";
            this.buttonWork.Size = new System.Drawing.Size(181, 23);
            this.buttonWork.TabIndex = 0;
            this.buttonWork.Text = "Work";
            this.buttonWork.UseVisualStyleBackColor = true;
            this.buttonWork.Click += new System.EventHandler(this.buttonWork_Click);
            // 
            // buttonWork2
            // 
            this.buttonWork2.Location = new System.Drawing.Point(12, 42);
            this.buttonWork2.Name = "buttonWork2";
            this.buttonWork2.Size = new System.Drawing.Size(182, 23);
            this.buttonWork2.TabIndex = 1;
            this.buttonWork2.Text = "Work in re-compiled source";
            this.buttonWork2.UseVisualStyleBackColor = true;
            this.buttonWork2.Click += new System.EventHandler(this.buttonWork2_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(61, 72);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(225, 20);
            this.textBoxResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Result";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 108);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonWork2);
            this.Controls.Add(this.buttonWork);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonWork;
        private System.Windows.Forms.Button buttonWork2;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label label1;
    }
}

