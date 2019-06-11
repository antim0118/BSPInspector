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
            this.button_parse = new System.Windows.Forms.Button();
            this.button_readtext = new System.Windows.Forms.Button();
            this.listView_lumps = new System.Windows.Forms.ListView();
            this.col_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_offset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_export = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
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
            // button_parse
            // 
            this.button_parse.Location = new System.Drawing.Point(12, 260);
            this.button_parse.Name = "button_parse";
            this.button_parse.Size = new System.Drawing.Size(50, 23);
            this.button_parse.TabIndex = 4;
            this.button_parse.Text = "Parse";
            this.button_parse.UseVisualStyleBackColor = true;
            this.button_parse.Click += new System.EventHandler(this.Button_parse_Click);
            // 
            // button_readtext
            // 
            this.button_readtext.Location = new System.Drawing.Point(68, 260);
            this.button_readtext.Name = "button_readtext";
            this.button_readtext.Size = new System.Drawing.Size(80, 23);
            this.button_readtext.TabIndex = 5;
            this.button_readtext.Text = "Read as text";
            this.button_readtext.UseVisualStyleBackColor = true;
            this.button_readtext.Click += new System.EventHandler(this.Button_readtext_Click);
            // 
            // listView_lumps
            // 
            this.listView_lumps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_id,
            this.col_Name,
            this.col_size,
            this.col_offset});
            this.listView_lumps.FullRowSelect = true;
            this.listView_lumps.Location = new System.Drawing.Point(12, 107);
            this.listView_lumps.MultiSelect = false;
            this.listView_lumps.Name = "listView_lumps";
            this.listView_lumps.Size = new System.Drawing.Size(610, 150);
            this.listView_lumps.TabIndex = 7;
            this.listView_lumps.UseCompatibleStateImageBehavior = false;
            this.listView_lumps.View = System.Windows.Forms.View.Details;
            this.listView_lumps.SelectedIndexChanged += new System.EventHandler(this.ListView_lumps_SelectedIndexChanged);
            // 
            // col_id
            // 
            this.col_id.Text = "ID";
            this.col_id.Width = 30;
            // 
            // col_Name
            // 
            this.col_Name.Text = "Name";
            this.col_Name.Width = 150;
            // 
            // col_size
            // 
            this.col_size.Text = "Size";
            // 
            // col_offset
            // 
            this.col_offset.Text = "Offset";
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(572, 260);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(50, 23);
            this.button_export.TabIndex = 8;
            this.button_export.Text = "Export";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.Button_export_Click);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(516, 260);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(50, 23);
            this.button_import.TabIndex = 9;
            this.button_import.Text = "Import";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.Button_import_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 291);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.listView_lumps);
            this.Controls.Add(this.button_readtext);
            this.Controls.Add(this.button_parse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_bsp);
            this.Controls.Add(this.label_bspinfo);
            this.MinimumSize = new System.Drawing.Size(350, 330);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "BSPInspector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_bspinfo;
        private System.Windows.Forms.TextBox textBox_bsp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_parse;
        private System.Windows.Forms.Button button_readtext;
        private System.Windows.Forms.ListView listView_lumps;
        private System.Windows.Forms.ColumnHeader col_Name;
        private System.Windows.Forms.ColumnHeader col_size;
        private System.Windows.Forms.ColumnHeader col_offset;
        private System.Windows.Forms.ColumnHeader col_id;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.Button button_import;
    }
}

