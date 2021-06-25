using System.Drawing;
using System.IO;

namespace MGFM.Models.FS
{
    public class File : FileBase
    {
        public FileInfo FileInfo => new(Path);

        public override Icon Icon => Icon.ExtractAssociatedIcon(Path);
    }
}