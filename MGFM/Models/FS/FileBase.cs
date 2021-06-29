using System;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using MGFM.Extensions;
using MGFM.Models.FS.Additional;
using MgMvvmTools;

namespace MGFM.Models.FS
{
    public abstract class FileBase : NotifyPropertyChanged
    {
        private string _path;

        public FileBase(string path)
        {
            Path = path;
        }

        public ComparableFile ComparableFile => new(this);

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

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
