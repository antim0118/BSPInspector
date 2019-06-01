using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSPInspector
{
    public static class Utils
    {
        public static T ByteToType<T>(BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return theStructure;
        }

        public static string ArrayToStr<T>(object arr, bool usebrackets = true)
        {
            var nvalue = "";
            foreach (T num in (T[])arr)
                nvalue += usebrackets ? $"<{num}> " : $"{num} ";
            return nvalue;
        }

        public static T[] GetFlags<T>(int _flag) where T : Enum
        {
            T flag = (T)Enum.Parse(typeof(T), _flag.ToString());
            var names = Enum.GetNames(typeof(T));
            var values = Enum.GetValues(typeof(T));
            int vallen = values.Length;
            List<T> ret = new List<T>();
            for (int i = 0; i < vallen; i++)
            {
                if (_flag != 0 && (int)values.GetValue(i) == 0) continue;
                T val = (T)values.GetValue(i);
                if (flag.HasFlag(val)) ret.Add(val);
            }
            return ret.ToArray();
        }
        public static string GetFlagsAsString<T>(int _flag) where T : Enum
        {
            T[] flags = GetFlags<T>(_flag);
            string ret = "";
            foreach (T flag in flags) ret += flag.ToString() + " | ";
            return ret.Substring(0, ret.Length - 3);
        }

        public static string ByteSizeToStr(long size)
        {
            //bytes
            if (size < 1024) return $"{size} bytes";

            //kb
            size /= 1024;
            if (size < 1024) return $"{size} KB";

            //mb
            size /= 1024;
            return $"{size} MB";
        }

        public static string GetLastLevel(this string str, char separator)
        {
            int min = 1;
            if (str.EndsWith(separator.ToString())) min++;
            if (str.Contains(separator))
            {
                string[] spl = str.Split(separator);
                return spl[spl.Length - min];
            }
            return str;
        }

        public static int GetLevels(this string str, char separator)
        {
            if (str.Contains(separator)) return str.Split(separator).Length - 1;
            return 0;
        }

        public static string GetPathWithLevel(this string path, int level, char separator)
        {
            //level0/level1/level2/level3
            string ret = "";
            if (!path.Contains(separator)) return path;
            string[] spl = path.Split(separator);
            for(int i = 0; i <= level; i++)
            {
                if (i >= spl.Length - 1) break;
                ret += spl[i] + separator;
            }
            return ret;
        }
        public static List<string> GetPathsWithLevel(this List<string> paths, int level, char separator)
        {
            List<string> ret = new List<string>();
            foreach (string path in paths)
            {
                string p = path.GetPathWithLevel(level, separator);
                if (!ret.Contains(p)) ret.Add(p);
            }
            return ret;
        }

        public static List<TreeNode> GetAllNodes(this TreeView _self)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (TreeNode child in _self.Nodes)
            {
                result.AddRange(child.GetAllNodes());
            }
            return result;
        }

        public static List<TreeNode> GetAllNodes(this TreeNode _self)
        {
            List<TreeNode> result = new List<TreeNode>();
            result.Add(_self);
            foreach (TreeNode child in _self.Nodes)
            {
                result.AddRange(child.GetAllNodes());
            }
            return result;
        }
    }
}
