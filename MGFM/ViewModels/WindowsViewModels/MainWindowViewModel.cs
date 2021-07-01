using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MGFM.Models.FileManager;
using MGFM.Models.FileManager.View;
using MGFM.Models.FS;
using MgMvvmTools;
using MessageBox = HandyControl.Controls.MessageBox;

namespace MGFM.ViewModels.WindowsViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private FileManagerTab _currentTab;
        private ViewMode _activeViewMode;
        public ObservableCollection<FileManagerTab> Tabs { get; }

        public FileManagerTab CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                OnPropertyChanged();
                //CurrentTabChanged?.Invoke(CurrentTab, new EventArgs());
            }
        }

        //private event EventHandler CurrentTabChanged;

        public MainWindowViewModel()
        {
            Tabs = new ObservableCollection<FileManagerTab> { new(Folder.UserProfileFolder, OnNavigated) };
            CurrentTab = Tabs.FirstOrDefault();
            ViewModes = new List<ViewMode>
            {
                TableViewMode,
                GridViewMode,
                ListViewMode
            };
            ActiveViewMode = ViewModes.First();
            //CurrentTabChanged += OnCurrentTabChanged;
        }

        //private void OnCurrentTabChanged(object sender, EventArgs e)
        //{
        //    if (CurrentTab != null) return;

        //    var firstTab = Tabs.FirstOrDefault();
        //    if (firstTab != null)
        //    {
        //        CurrentTab = firstTab;
        //    }
        //    else
        //    {
        //        OpenInNewTab();
        //    }
        //}

        //private void OnTabsClosed()
        //{
        //    if (!Tabs.Any())
        //    {
        //        OpenInNewTab();
        //    }
        //}

        public void OpenInNewTab(string folderPath = null)
        {
            var newTab = new FileManagerTab(folderPath);
            Tabs.Add(newTab);
            CurrentTab = newTab;
        }

        public ICommand OpenInNewTabCommand => new Command<Folder>(file =>
        {
            OpenInNewTab(file.Path);
        });

        private void OnNavigated(object sender, EventArgs e)
        {
        }

        public ViewMode TableViewMode { get; } = new("Table", "TableLarge");
        public ViewMode GridViewMode { get; } = new("Grid", "ViewGrid");
        public ViewMode ListViewMode { get; } = new("List", "ViewList", 30);
        
        public List<ViewMode> ViewModes { get; }

        public ViewMode ActiveViewMode
        {
            get => _activeViewMode;
            set
            {
                if (_activeViewMode == value) return;
                _activeViewMode = value;
                OnPropertyChanged();
                ViewModes.ForEach(viewMode =>
                {
                    viewMode.IsActive = viewMode == _activeViewMode;
                });
            }
        }

        public ICommand ChangeActiveViewModeCommand => new Command<ViewMode>(viewMode =>
        {
            ActiveViewMode = viewMode;
        });

        public ICommand RunTerminalCommand => new Command(() =>
        {
            var currentPath = CurrentTab.CurrentFolder.Path;

            var terminals = new[]
            {
                (FileName: "wt.exe", Arguments: $"-d {currentPath}"), 
                (FileName: "pwsh.exe", Arguments: $"-WorkingDirectory {currentPath}"), 
                (FileName: "powershell.exe", Arguments: $"-NoExit -Command cd \"{currentPath}\""), 
                (FileName: "cmd.exe", Arguments: $"/K \"cd /d {currentPath}")
            };

            foreach (var terminal in terminals)
            {
                Process process = null;
                try
                {
                    process = Process.Start(terminal.FileName, terminal.Arguments);
                    return;
                }
                catch (Exception)
                {
                    process?.Dispose();
                    continue;
                }
            }

            MessageBox.Show($"Failed to start any terminal of: {string.Join(", ", terminals.Select(x => $"\"{x.FileName}\""))}.", 
                "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        });

        public ICommand RunExplorerCommand => new Command(() =>
        {
            Process.Start("explorer.exe", $"\"{CurrentTab.CurrentFolder.Path}\"");
        });

        private bool IsFileSelected => CurrentTab?.SelectedFile != null;

        public ICommand RunFileCommand => new Command(() =>
        {
            switch (CurrentTab.SelectedFile)
            {
                case File file:
                    file.Open();
                    break;
                case Folder folder:
                    CurrentTab.Navigate(folder);
                    break;
            }
        }, () => IsFileSelected);

        public ICommand CopyFilesCommand => new Command<IEnumerable<FileBase>>(selectedFiles =>
        {
            var filesToCopy = new StringCollection();
            filesToCopy.AddRange(selectedFiles.Select(x => x.Path).ToArray());
            Clipboard.SetFileDropList(filesToCopy);
        }, _ => IsFileSelected);
    }
}
