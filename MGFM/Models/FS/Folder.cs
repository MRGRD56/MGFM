using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using MGFM.Extensions;

namespace MGFM.Models.FS
{
    public class Folder : FileBase
    {
        private static readonly Icon DefaultIcon = DefaultIcons.FolderLarge;

        public DirectoryInfo DirectoryInfo => new(Path);

        public ObservableCollection<File> Files { get; set; }

        public override Icon Icon => DefaultIcon;
    }
}