using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSPInspector
{
    public class SourceBSP : IDisposable
    {
        public BinaryReader BR;
        public BinaryWriter BW;
        public Stream file;

        public SourceBSPStructs.dheader_t Header;

        public SourceBSP(string filepath)
        {
            file = File.Open(filepath, FileMode.Open);
            BR = new BinaryReader(file);
            BW = new BinaryWriter(file);
            Header = Utils.ByteToType<SourceBSPStructs.dheader_t>(BR);
        }

        public void Dispose()
        {
            BR.Dispose();
            BW.Dispose();
            file.Dispose();
        }
    }
}
