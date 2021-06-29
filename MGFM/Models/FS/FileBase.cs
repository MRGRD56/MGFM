using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using MGFM.Extensions;

namespace MGFM.Models.FS
{
    public abstract class FileBase
    {
        public FileBase(string path)
        {
            Path = path;
        }

        public string Path { get; set; }

        public abstract FileSystemInfo Info { get; }

        public abstract Icon Icon { get; }
        public abstract string ShortName { get; }
        public abstract FileSize Size { get; }
        public DateTime ModifiedDate => Info.LastWriteTime;


        public ImageSource IconSource => Icon.ToBitmap().ToImageSource();

        public bool IsFile => this is File;
        public bool IsFolder => this is Folder;
    }
}
