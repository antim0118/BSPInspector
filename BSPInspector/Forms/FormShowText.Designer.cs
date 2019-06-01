namespace BSPInspector
{
    partial class FormShowText
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_copy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(5, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(350, 350);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(307, 361);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(40, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // button_copy
            // 
            this.button_copy.Location = new System.Drawing.Point(199, 361);
            this.button_copy.Name = "button_copy";
            this.button_copy.Size = new System.Drawing.Size(102, 23);
            this.button_copy.TabIndex = 2;
            this.button_copy.Text = "Copy to clipboard";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Click += new System.EventHandler(this.Button_copy_Click);
            // 
            // FormShowText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 396);
            this.Controls.Add(this.button_copy);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.richTextBox1);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FormShowText";
            this.ShowIcon = false;
            this.Text = "FormShowText";
            this.Resize += new System.EventHandler(this.FormShowText_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_copy;
    }
}