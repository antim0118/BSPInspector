using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BSPInspector
{
    public partial class FormFileBrowser : Form
    {
        private string zippath;
        private ZipArchive ziparchive;
        private List<string> folders;

        public FormFileBrowser(string zippath)
        {
            this.zippath = zippath;
            InitializeComponent();
        }

        private void FormFileBrowser_Load(object sender, EventArgs e)
        {
            folders = new List<string>();
            ziparchive = ZipFile.OpenRead(zippath);
            foreach (ZipArchiveEntry entry in ziparchive.Entries)
            {
                //listView1.Items.Add(entry.FullName);
                string folder = entry.FullName.Replace(entry.Name, "");
                if (!folders.Contains(folder)) folders.Add(folder);
            }

            PopulateTreeView(treeView1, folders, '/');

            if (treeView1.Nodes.Count > 0)
                TreeView1_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(treeView1.Nodes[0], MouseButtons.Left, 1, 1, 1));
        }

        private static void PopulateTreeView(TreeView treeView, List<string> paths, char pathSeparator)
        {
            TreeNode lastNode = null;
            string subPathAgg;
            foreach (string path in paths)
            {
                subPathAgg = string.Empty;
                foreach (string subPath in path.Split(pathSeparator))
                {
                    if (string.IsNullOrEmpty(subPath)) continue;
                    subPathAgg += subPath + pathSeparator;
                    TreeNode[] nodes = treeView.Nodes.Find(subPathAgg, true);
                    if (nodes.Length == 0)
                        if (lastNode == null)
                            lastNode = treeView.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                    else
                        lastNode = nodes[0];
                }
                lastNode = null; // This is the place code was changed

            }
        }

        private string currentPath = "";
        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;
            string fullpath = newSelected.FullPath.Replace("\\", "/") + "/";
            currentPath = newSelected.FullPath;
            if (fullpath.GetLevels('/') > 1)
                listView1.Items.Add(new ListViewItem("..", 0));
            foreach (string dir in folders.GetPathsWithLevel(fullpath.GetLevels('/'), '/'))
            {
                if (!dir.StartsWith(fullpath) || dir == fullpath) continue;
                item = new ListViewItem(dir.GetLastLevel('/'), 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory")};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (ZipArchiveEntry entry in ziparchive.Entries)
            {
                if (fullpath != entry.FullName.Replace(entry.Name, "")) continue;
                item = new ListViewItem(entry.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File"),
                    new ListViewItem.ListViewSubItem(item, Utils.ByteSizeToStr(entry.Length))};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void FormFileBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            ziparchive.Dispose();
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string sel = listView1.SelectedItems[0].Text;
                string curpath = currentPath.Replace("\\", "/");
                if (sel == "..")
                {
                    foreach (TreeNode node in treeView1.GetAllNodes())
                    {
                        if (node.Name == curpath.GetPathWithLevel(curpath.GetLevels('/') - 1, '/'))
                        {
                            TreeView1_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 1, 1, 1));
                            break;
                        }
                    }
                }
                else
                {
                    sel = curpath + "/" + sel + "/";
                    foreach (TreeNode node in treeView1.GetAllNodes())
                    {
                        if (node.Name == sel)
                        {
                            TreeView1_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 1, 1, 1));
                            break;
                        }
                    }
                }
            }
        }
    }
}
