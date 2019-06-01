using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace BSPInspector
{
    public partial class Form1 : Form
    {
        public string BSPPath;
        public SourceBSP bsp;
        #region Form init
        public Form1(string bsppath)
        {
            BSPPath = bsppath;
            bsp = new SourceBSP(bsppath);
            InitializeComponent();
            textBox_bsp.Text = bsppath;
        }
        #endregion
        #region Form load
        private void Form1_Load(object sender, EventArgs e)
        {
            if(bsp.Header.ident != SourceBSPStructs.VBSP)
            {
                label_bspinfo.Text += $"Identifier={bsp.Header.ident} " + (bsp.Header.ident == SourceBSPStructs.VBSP ? "(Valid)" : "(Not valid)");
                return;
            }

            #region print lumps
            int lumpslen = bsp.Header.lumps.Length;
            int lumpsskip = 0;
            for (int i = 0; i < lumpslen; i++)
            {
                var lump = bsp.Header.lumps[i];
                if (lump.filelen == 0) { lumpsskip++; continue; }
                string lump_name = SourceBSPStructs.GetLump(i).ToString();
                listBox_lumps.Items.Add($"{i}: {lump_name}");
            }
            #endregion

            label_bspinfo.Text += $"Identifier={bsp.Header.ident} " + (bsp.Header.ident == SourceBSPStructs.VBSP ? "(Valid)" : "(Not valid)") + "\n" +
                    $"Version={bsp.Header.version}\n" +
                    $"{bsp.Header.lumps.Length} Lumps ({lumpsskip} skipped)\n" +
                    $"The map's revision (iteration, version)={bsp.Header.mapRevision}\n";

            this.TopMost = true;
            Application.DoEvents();
            this.TopMost = false;
        }
        #endregion
        #region Form resize
        private void Form1_Resize(object sender, EventArgs e)
        {
            textBox_bsp.Width = Width - 72;
            listBox_lumps.Size = new Size(Width - 43, Height - 183);
            button_parse.Top = Height - 70;
            button_readtext.Top = Height - 70;
            label_lumpinfo.Top = Height - 70;
        }
        #endregion
        #region Form closed
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            bsp.Dispose();
        }
        #endregion
        #region Form events
        private void ListBox_lumps_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label_lumpinfo.Text
            var lump = bsp.Header.lumps[listBox_lumps_index];
            label_lumpinfo.Text = $"Lump info:    filelen={lump.filelen}    fileofs={lump.fileofs}    version={lump.version}";
        }

        private void ListBox_lumps_DoubleClick(object sender, EventArgs e)
        {
            Button_parse_Click(sender, e);
        }

        private void Button_parse_Click(object sender, EventArgs e)
        {
            if (listBox_lumps_index < 0) return;
            var lump = bsp.Header.lumps[listBox_lumps_index];
            var lumptype = SourceBSPStructs.GetLump(listBox_lumps_index);

            if (lumptype == SourceBSPStructs.Lumps.LUMP_PAKFILE) //Lump 40
            {
                string filepath = $"{Application.StartupPath}/pak_temp.zip";
                if (File.Exists(filepath)) File.Delete(filepath);
                File.WriteAllBytes(filepath, lump.Parse<byte>(bsp.BR));
                //SharpVPK.VpkArchive vpk = new SharpVPK.VpkArchive();
                //vpk.Load(lump.Parse<byte>(bsp.BR), SharpVPK.VpkVersions.Versions.V1);
                new FormFileBrowser(filepath).ShowDialog();
            }
            else
            {
                var table = new FormShowTable();
                if (CreateLumpTable(lump, lumptype, table))
                {
                    table.ShowDialog();
                }
                else
                {
                    MessageBox.Show("This lump is not supported yet.");
                    table.Dispose();
                }
            }
            
            GC.Collect();
        }
        private bool CreateLumpTable(SourceBSPStructs.lump_t lump, SourceBSPStructs.Lumps lumptype, FormShowTable table, bool isv2 = false)
        {
            if (lumptype == SourceBSPStructs.Lumps.LUMP_PLANES) //Lump 1
                table.CreateTable(lump.Parse<SourceBSPStructs.dplane_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_TEXDATA) //Lump 2
                table.CreateTable(lump.Parse<SourceBSPStructs.dtexdata_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_VERTEXES) //Lump 3
                table.CreateTable(lump.Parse<SourceBSPStructs.dvertexes_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_NODES) //Lump 5
                table.CreateTable(lump.Parse<SourceBSPStructs.dnode_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_TEXINFO) //Lump 6
                table.CreateTable(lump.Parse<SourceBSPStructs.texinfo_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_FACES ||
                lumptype == SourceBSPStructs.Lumps.LUMP_ORIGINALFACES) //Lump 7 + Lump 27
                table.CreateTable(lump.Parse<SourceBSPStructs.dface_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_LIGHTING ||
                lumptype == SourceBSPStructs.Lumps.LUMP_LIGHTING_HDR || //Lump 8 (LDR) + Lump 53 (HDR)
                lumptype == SourceBSPStructs.Lumps.LUMP_DISP_LIGHTMAP_ALPHAS ||
                lumptype == SourceBSPStructs.Lumps.LUMP_DISP_LIGHTMAP_SAMPLE_POSITIONS) //Lump 32 + Lump 34
                table.CreateTable(lump.Parse<SourceBSPStructs.ColorRGBExp32>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_LEAFS) //Lump 10
                table.CreateTable(lump.Parse<SourceBSPStructs.dleaf_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_EDGES) //Lump 12
                table.CreateTable(lump.Parse<SourceBSPStructs.dedge_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_SURFEDGES) //Lump 13
                table.CreateTable(lump.Parse<SourceBSPStructs.dsurfedge_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_MODELS) //Lump 14
                table.CreateTable(lump.Parse<SourceBSPStructs.dmodel_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_LEAFFACES) //Lump 16
                table.CreateTable(lump.Parse<SourceBSPStructs.dleafface_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_LEAFBRUSHES) //Lump 17
                table.CreateTable(lump.Parse<SourceBSPStructs.dleafbrush_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_BRUSHES) //Lump 18
                table.CreateTable(lump.Parse<SourceBSPStructs.dbrush_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_BRUSHSIDES) //Lump 19
                table.CreateTable(lump.Parse<SourceBSPStructs.dbrushside_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_DISPINFO) //Lump 26
                table.CreateTable(lump.Parse<SourceBSPStructs.ddispinfo_t>(bsp.BR), isv2);

            else if (lumptype == SourceBSPStructs.Lumps.LUMP_DISP_VERTS) //Lump 33
                table.CreateTable(lump.Parse<SourceBSPStructs.dDispVert>(bsp.BR), isv2);

            else return false;
            return true;
        }

        private void Button_readtext_Click(object sender, EventArgs e)
        {
            if (listBox_lumps_index < 0) return;
            var lump = bsp.Header.lumps[listBox_lumps_index];
            new FormShowText(lump.Read(bsp.BR), "ASCII Text of selected lump").ShowDialog();
        }
        #endregion

        private int listBox_lumps_index
        {
            get
            {
                if (listBox_lumps.SelectedItem == null) return -1;
                string val = (string)listBox_lumps.SelectedItem;
                if (val.Contains(":")) return int.Parse(val.Split(':').First());
                else return -1;
            }
        }
    }
}
