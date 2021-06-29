using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using HandyControl.Data;
using MGFM.Extensions;
using MGFM.Properties;
using MessageBox = HandyControl.Controls.MessageBox;

namespace MGFM.Models.FS
{
    public class Folder : FileBase
    {
        public Folder(string path, bool updateFiles = false) : base(path)
        {
            if (updateFiles)
            {
                UpdateFiles();
            }
        }

        public string ParentDirectory => Path == MyComputerFolder ? null : Info?.Parent?.FullName ?? "";

        private static readonly Icon DefaultIcon = FolderIcons.FolderLarge;

        public override DirectoryInfo Info => string.IsNullOrEmpty(Path) ? null : new DirectoryInfo(Path);

        public ObservableCollection<FileBase> Files { get; } = new();

        public override Icon Icon => FolderIcons.ExtractFromPath(Path) ?? DefaultIcon;

        public override string ShortName => Info?.Name ?? Resources.ThisComputer;

        public override FileSize Size => new();

        public void UpdateFiles()
        {
            try
            {
                Files.Clear();

                if (Path != MyComputerFolder)
                {
                    var foldersInDirectory = Info.GetDirectories();
                    foreach (var folder in foldersInDirectory)
                    {
                        Files.Add(new Folder(folder.FullName));
                    }

                    var filesInDirectory = Info.GetFiles();
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
            catch (Exception exception)
            {
                switch (exception)
                {
                    case UnauthorizedAccessException:
                        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                        break;
                    default:
                        throw;
                }
            }
        }

        public async Task UpdateFilesAsync(CancellationToken cancellationToken = default) => await Task.Run(UpdateFiles, cancellationToken);

        public static string MyComputerFolder => Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
    }
}