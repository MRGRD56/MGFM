using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFM.Models.FS;

namespace MGFM.Models.FileManager
{
    public class FileManagerTab
    {
        public FileManagerTab()
        {
            Initialize(null);
        }

        public FileManagerTab(string folderPath)
        {
            Initialize(folderPath);
        }

        private void Initialize(string folderPath)
        {
            folderPath ??= Folder.MyComputerFolder;
            CurrentFolder = new Folder(folderPath);
            NavigationHistory = new ObservableCollection<Folder> { CurrentFolder };
        }

        public Folder CurrentFolder
        {
            get => NavigationHistory[_navigationHistoryPosition];
            private set => _navigationHistoryPosition = NavigationHistory.IndexOf(value);
        }

        public File SelectedFile { get; set; }

        public ObservableCollection<Folder> NavigationHistory { get; set; }

        private int _navigationHistoryPosition;

        public bool CanGoBack => _navigationHistoryPosition >= 1;

        public void GoBack()
        {
            if (CanGoBack)
            {
                _navigationHistoryPosition--;
            }
        }

        public bool CanGoForward => NavigationHistory != null && _navigationHistoryPosition < NavigationHistory.Count - 1;

        public void GoForward()
        {
            if (CanGoForward)
            {
                _navigationHistoryPosition++;
            }
        }

        public void Navigate(string folderPath)
        {
            if (CanGoForward)
            {
                for (var i = _navigationHistoryPosition + 1; i < NavigationHistory.Count; i++)
                {
                    NavigationHistory.RemoveAt(i);
                }
            }

            var folder = new Folder(folderPath);
            NavigationHistory.Add(folder);
            CurrentFolder = folder;
        }
    }
}
