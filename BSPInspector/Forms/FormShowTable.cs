using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSPInspector
{
    public partial class FormShowTable : Form
    {
        public FormShowTable()
        {
            InitializeComponent();
        }

        public void CreateTable<T>(T[] objs, bool isv2) where T : struct
        {
            this.Text += Utils.TypeLastLevel(typeof(T).ToString());

            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            #region add columns
            foreach (FieldInfo info in fields)
            {
                dataGridView1.Columns.Add(info.Name, $"{info.Name}\n[{Utils.TypeLastLevel(info.FieldType.ToString())}]");
            }
            foreach (PropertyInfo info in props)
            {
                dataGridView1.Columns[dataGridView1.Columns.Add(info.Name, info.Name)].Width *= 2;
            }
            #endregion
            int objsLen = objs.Length;
            int fieldsLen = fields.Length;
            int propsLen = props.Length;

            #region warning
            if (objsLen > 100000)
            {
                var warning = MessageBox.Show($"This lump contains >100k objects.\nYour PC may freeze or crash, if you have less than {(int)(0.00138 * objsLen / 1024f + 1)}gb RAM.", "Are you sure?", MessageBoxButtons.YesNo);
                if (warning == DialogResult.No) return;
            }
            #endregion

            #region start loader
            FormLoader loader = null;
            new Thread(delegate ()
            {
                loader = new FormLoader();
                Application.Run(loader);
            }).Start();
            while (loader == null || !loader.Visible) Thread.Sleep(100);
            #endregion
            
            #region add rows
            int delload = objsLen > 100 ? objsLen / 100 : objsLen;
            DataGridViewRow[] rows = new DataGridViewRow[objsLen];
            for (int u = 0; u < objsLen; u++)
            {
                if (u % delload == 1) loader.UpdateValue(u, objsLen);
                var obj = objs[u];
                rows[u] = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                for (int i = 0; i < fieldsLen; i++)
                {
                    object value = fields[i].GetValue(obj);
                    if (value.GetType() == typeof(short[]))
                        rows[u].Cells[i].Value = Utils.ArrayToStr<short>(value);
                    else if (value.GetType() == typeof(int[]))
                        rows[u].Cells[i].Value = Utils.ArrayToStr<int>(value);
                    else if (value.GetType() == typeof(Vector2[]))
                        rows[u].Cells[i].Value = Utils.ArrayToStr<Vector2>(value, false);
                    else if (value.GetType() == typeof(byte[]))
                        rows[u].Cells[i].Value = Utils.ArrayToStr<byte>(value);
                    else
                        rows[u].Cells[i].Value = value.ToString();
                }
                for (int i = 0; i < propsLen; i++)
                {
                    object value = props[i].GetValue(obj);
                    rows[u].Cells[fieldsLen + i].Value = value.ToString();
                }
                rows[u].Height = 18;
            }
            #endregion
            dataGridView1.Rows.AddRange(rows);
            loader.CloseForm();
        }

        


        private void FormShowTable_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(Width - 25, Height - 85);
            button_ok.Location = new Point(Width - 68, Height - 74);
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
