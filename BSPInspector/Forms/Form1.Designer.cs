namespace BSPInspector
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_bspinfo = new System.Windows.Forms.Label();
            this.textBox_bsp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_lumps = new System.Windows.Forms.ListBox();
            this.button_parse = new System.Windows.Forms.Button();
            this.button_readtext = new System.Windows.Forms.Button();
            this.label_lumpinfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_bspinfo
            // 
            this.label_bspinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_bspinfo.Location = new System.Drawing.Point(12, 37);
            this.label_bspinfo.Name = "label_bspinfo";
            this.label_bspinfo.Size = new System.Drawing.Size(278, 67);
            this.label_bspinfo.TabIndex = 0;
            this.label_bspinfo.Text = "BSP info:\r\n";
            // 
            // textBox_bsp
            // 
            this.textBox_bsp.Location = new System.Drawing.Point(44, 12);
            this.textBox_bsp.Name = "textBox_bsp";
            this.textBox_bsp.ReadOnly = true;
            this.textBox_bsp.Size = new System.Drawing.Size(578, 20);
            this.textBox_bsp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File:";
            // 
            // listBox_lumps
            // 
            this.listBox_lumps.FormattingEnabled = true;
            this.listBox_lumps.Location = new System.Drawing.Point(15, 107);
            this.listBox_lumps.Name = "listBox_lumps";
            this.listBox_lumps.Size = new System.Drawing.Size(607, 147);
            this.listBox_lumps.TabIndex = 3;
            this.listBox_lumps.SelectedIndexChanged += new System.EventHandler(this.ListBox_lumps_SelectedIndexChanged);
            this.listBox_lumps.DoubleClick += new System.EventHandler(this.ListBox_lumps_DoubleClick);
            // 
            // button_parse
            // 
            this.button_parse.Location = new System.Drawing.Point(15, 260);
            this.button_parse.Name = "button_parse";
            this.button_parse.Size = new System.Drawing.Size(50, 23);
            this.button_parse.TabIndex = 4;
            this.button_parse.Text = "Parse";
            this.button_parse.UseVisualStyleBackColor = true;
            this.button_parse.Click += new System.EventHandler(this.Button_parse_Click);
            // 
            // button_readtext
            // 
            this.button_readtext.Location = new System.Drawing.Point(71, 260);
            this.button_readtext.Name = "button_readtext";
            this.button_readtext.Size = new System.Drawing.Size(80, 23);
            this.button_readtext.TabIndex = 5;
            this.button_readtext.Text = "Read as text";
            this.button_readtext.UseVisualStyleBackColor = true;
            this.button_readtext.Click += new System.EventHandler(this.Button_readtext_Click);
            // 
            // label_lumpinfo
            // 
            this.label_lumpinfo.Location = new System.Drawing.Point(157, 260);
            this.label_lumpinfo.Name = "label_lumpinfo";
            this.label_lumpinfo.Size = new System.Drawing.Size(465, 23);
            this.label_lumpinfo.TabIndex = 6;
            this.label_lumpinfo.Text = "Lump info:";
            this.label_lumpinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 291);
            this.Controls.Add(this.label_lumpinfo);
            this.Controls.Add(this.button_readtext);
            this.Controls.Add(this.button_parse);
            this.Controls.Add(this.listBox_lumps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_bsp);
            this.Controls.Add(this.label_bspinfo);
            this.MinimumSize = new System.Drawing.Size(300, 330);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "BSPInspector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_bspinfo;
        private System.Windows.Forms.TextBox textBox_bsp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox_lumps;
        private System.Windows.Forms.Button button_parse;
        private System.Windows.Forms.Button button_readtext;
        private System.Windows.Forms.Label label_lumpinfo;
    }
}

