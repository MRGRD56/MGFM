using System.Drawing;
using System.IO;

namespace MGFM.Models.FS
{
    public class File : FileBase
    {
        public File(string path) : base(path)
        {

        }

        public FileInfo FileInfo => new(Path);

        public override Icon Icon => Icon.ExtractAssociatedIcon(Path);

        public override string ShortName => FileInfo.Name;
    }
}