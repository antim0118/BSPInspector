using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSPInspector
{
    static class Program
    {
        public static Form1 form1;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Source BSP File Format (*.bsp)|*.bsp",
                Title = "Open bsp file"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
                Application.Run(form1 = new Form1(ofd.FileName));
            //else exit
        }
    }
}
