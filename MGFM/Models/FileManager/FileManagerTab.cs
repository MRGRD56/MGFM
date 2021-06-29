using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using MGFM.Models.FS;
using MgMvvmTools;
using File = MGFM.Models.FS.File;


namespace MGFM.Models.FileManager
{
    public class FileManagerTab : NotifyPropertyChanged
    {
        public FileManagerTab(EventHandler navigated = null)
        {
            Initialize(null, navigated);
        }

        public FileManagerTab(string folderPath, EventHandler navigated = null)
        {
            Initialize(folderPath, navigated);
            
        }

        private void Initialize(string folderPath, EventHandler navigated)
        {
            folderPath ??= Folder.MyComputerFolder;
            Navigate(folderPath);
            Navigated += navigated;
            //NavigationHistory = new ObservableCollection<Folder> { currentFolder };
        }

        private event EventHandler Navigated;

        public Folder CurrentFolder
        {
            get => NavigationHistory?.ElementAtOrDefault(NavigationHistoryPosition);
            private set => NavigationHistoryPosition = NavigationHistory?.IndexOf(value) ?? -1;
        }

        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                _currentPath = value;
                OnPropertyChanged();
            }
        }

        public void ActualizeCurrentPath(string path)
        {
            if (path != null)
            {
                CurrentPath = path;
            }
        }

        public void ActualizeCurrentPath()
        {
            if (CurrentFolder != null)
            {
                CurrentPath = CurrentFolder.Path;
            }
        }

        public File SelectedFile { get; set; }

        public ObservableCollection<Folder> NavigationHistory { get; set; } = new();

        private int NavigationHistoryPosition
        {
            get => _navigationHistoryPosition;
            set
            {
                _navigationHistoryPosition = value;
                OnPropertyChanged(nameof(CurrentFolder));
                ActualizeCurrentPath();
                Navigated?.Invoke(this, new EventArgs());
            }
        }

        private string _currentPath;
        private int _navigationHistoryPosition;

        public bool CanGoBack => NavigationHistoryPosition >= 1;

        public void GoBack()
        {
            if (!CanGoBack) return;

            NavigationHistoryPosition--;
            OnPropertyChanged(nameof(CurrentFolder));
        }

        public bool CanGoForward => NavigationHistory != null && NavigationHistoryPosition < NavigationHistory.Count - 1;

        public void GoForward()
        {
            if (!CanGoForward) return;
            
            NavigationHistoryPosition++;
            OnPropertyChanged(nameof(CurrentFolder));
        }

        private string ParentDirectory => CurrentFolder?.ParentDirectory;

        public bool CanGoToParent => ParentDirectory != null;

        public void GoToParent()
        {
            if (CanGoToParent)
            {
                Navigate(ParentDirectory);
            }
        }

        public void Navigate(Folder folder) => Navigate(folder.Path);

        public void Navigate(string folderPath)
        {
            if (!string.IsNullOrWhiteSpace(CurrentFolder?.Path))
            {
                Directory.SetCurrentDirectory(CurrentFolder.Path);
            }
            else if (CurrentFolder?.Path == Folder.MyComputerFolder)
            {
                if (Regex.IsMatch(folderPath, @"^\.+$"))
                {
                    folderPath = Folder.MyComputerFolder;
                }
            }

            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                folderPath = Path.GetFullPath(folderPath);
            }

            if (folderPath == null 
                || folderPath == CurrentFolder?.Path 
                || folderPath != Folder.MyComputerFolder && !new DirectoryInfo(folderPath).Exists)
            {
                ActualizeCurrentPath(folderPath);
                return;
            }

            if (CanGoForward)
            {
                for (var i = NavigationHistoryPosition + 1; i < NavigationHistory.Count; i++)
                {
                    NavigationHistory.RemoveAt(i);
                }
            }

            var folder = new Folder(folderPath, true);
            NavigationHistory.Add(folder);
            CurrentFolder = folder;
        }

        public ICommand GoBackCommand => new Command(GoBack, () => CanGoBack);

        public ICommand GoForwardCommand => new Command(GoForward, () => CanGoForward);

        public ICommand GoToParentCommand => new Command(GoToParent, () => CanGoToParent);

        public ICommand NavigateCommand => new Command(() => Navigate(CurrentPath));
        public ICommand NavigateToFolderCommand => new Command<Folder>(Navigate);

        public ICommand ActualizeCurrentPathCommand => new Command(ActualizeCurrentPath);

        public override string ToString() => CurrentFolder.ShortName;
    }
}
