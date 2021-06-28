using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MGFM.Extensions;

namespace MGFM.Models.FS
{
    public class Folder : FileBase
    {
        public Folder(string path) : base(path)
        {
            
        }

        private static readonly Icon DefaultIcon = DefaultIcons.FolderLarge;

        public DirectoryInfo DirectoryInfo => new(Path);

        public ObservableCollection<FileBase> Files { get; } = new();

        public override Icon Icon => DefaultIcon;

        public override string ShortName => DirectoryInfo.Name;

        public void UpdateFiles()
        {
            Files.Clear();

            if (Path != MyComputerFolder)
            {
                var foldersInDirectory = DirectoryInfo.GetDirectories();
                foreach (var folder in foldersInDirectory)
                {
                    Files.Add(new Folder(folder.FullName));
                }

                var filesInDirectory = DirectoryInfo.GetFiles();
                foreach (var file in filesInDirectory)
                {
                    Files.Add(new File(file.FullName));
                }
            }
            else
            {
                var drives = DriveInfo.GetDrives();
                foreach (var drive in drives)
                {
                    Files.Add(new Folder(drive.Name));
                }
            }
        }

        public async Task UpdateFilesAsync(CancellationToken cancellationToken = default) => await Task.Run(UpdateFiles, cancellationToken);

        public static string MyComputerFolder => Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
    }
}