using System;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using MGFM.Extensions;
using MgMvvmTools;

namespace MGFM.Models.FS
{
    public class File : FileBase
    {
        public File(string path) : base(path)
        {

        }

        public override FileInfo Info => new(Path);

        public override Icon IconSmall => FileIcons.ExtractFromPath(Path, false);//IconLarge;

        public override Icon IconLarge => FileIcons.ExtractFromPath(Path, true);//Icon.ExtractAssociatedIcon(Path);

        public override string ShortName => Info.Extension == ".lnk" ? Info.Name[..^4] : Info.Name;

        public override FileSize Size => new(Info.Length);

        public void Open() => FilesExtensions.Open(Path);
        public void OpenWith() => FilesExtensions.OpenWith(Path);

        public ICommand OpenCommand => new Command(Open);
        public ICommand OpenWithCommand => new Command(OpenWith);
    }
}