using System;
using System.Drawing;
using System.IO;

namespace MGFM.Models.FS
{
    public class File : FileBase
    {
        public File(string path) : base(path)
        {

        }

        public override FileInfo Info => new(Path);
        
        public override Icon Icon => Icon.ExtractAssociatedIcon(Path);

        public override string ShortName => Info.Name;

        public override FileSize Size => new(Info.Length);
    }
}