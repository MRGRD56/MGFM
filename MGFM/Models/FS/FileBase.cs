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

        public abstract Icon IconSmall { get; }
        public abstract Icon IconLarge { get; }
        public abstract string ShortName { get; }
        public abstract FileSize Size0 { get; }
        public abstract FileSize Size2 { get; }
        public DateTime ModifiedDate => Info.LastWriteTime;


        public ImageSource IconSmallSource => IconSmall.ToBitmap().ToImageSource();
        public ImageSource IconLargeSource => IconLarge.ToBitmap().ToImageSource();

        public bool IsFile => this is File;
        public bool IsFolder => this is Folder;
    }
}
