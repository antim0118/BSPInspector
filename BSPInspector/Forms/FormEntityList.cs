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
    public partial class FormEntityList : Form
    {
        private string entities;
        public FormEntityList(string entities)
        {
            this.entities = entities;
            InitializeComponent();
        }

        private void FormEntityList_Load(object sender, EventArgs e)
        {
            string[] ents = entities.Split(new string[] { "{", "}" }, StringSplitOptions.RemoveEmptyEntries);
            EntList = new List<entity>();
            foreach(string ent in ents)
            {
                entity c_ent = new entity();
                c_ent.kv = new Dictionary<string, string>();
                c_ent.outputs = new List<string>();
                string[] keyvalues = ent.Split('\n');
                foreach(string keyvalue in keyvalues)
                {
                    string[] kv = keyvalue.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);
                    if (kv.Length < 3) continue;
                    string key = kv[0];
                    string value = kv[2];
                    if (key.StartsWith("On"))
                        c_ent.outputs.Add(key + "\0" + value);
                    else
                        c_ent.kv.Add(key, value);
                }
                if (c_ent.kv.ContainsKey("classname"))
                    EntList.Add(c_ent);
            }

            for(int i = 0; i < EntList.Count; i++)
            {
                var ent = EntList[i];
                string classname = ent.kv["classname"], targetname = "";
                if (ent.kv.ContainsKey("targetname")) targetname = ent.kv["targetname"];
                string itemname = classname + (targetname != "" ? " (" + targetname + ")" : "");
                comboBox_entities.Items.Add(itemname);
            }

            comboBox_entities.SelectedIndex = 0;
        }

        private List<entity> EntList;
        private class entity
        {
            public Dictionary<string, string> kv;
            public List<string> outputs;
        }

        private void ComboBox_entities_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ent = EntList[comboBox_entities.SelectedIndex];
            listView_kv.Items.Clear();
            foreach (var kv in ent.kv)
                listView_kv.Items.Add(new ListViewItem(new string[]
                {
                    kv.Key, kv.Value
                }));
        }
    }
}
