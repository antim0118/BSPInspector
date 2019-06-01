using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSPInspector
{
    public partial class FormShowText : Form
    {
        public FormShowText(string text, string caption = "")
        {
            InitializeComponent();

            this.Text = caption;
            richTextBox1.Text = text;
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void FormShowText_Resize(object sender, EventArgs e)
        {
            richTextBox1.Size = new Size(Width - 15, Height - 85);
            button_ok.Location = new Point(Width - 68, Height - 74);
            button_copy.Location = new Point(Width - 176, Height - 74);
        }
    }
}
