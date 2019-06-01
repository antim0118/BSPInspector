using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSPInspector
{
    public partial class FormLoader : Form
    {
        public FormLoader()
        {
            InitializeComponent();
        }

        public void UpdateValue(int val, int max)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    progressBar1.Maximum = max;
                    progressBar1.Value = val;
                    label1.Text = $"{val}/{max}";
                });
            }
            catch { }
        }

        public void CloseForm()
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                this.Close();
            });
        }

        int time = 0;
        private void Timer_counter_Tick(object sender, EventArgs e)
        {
            time++;
            label_time.Text = $"{time}s.";
        }
    }
}
