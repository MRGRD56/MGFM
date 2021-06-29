using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MGFM.Models.FileManager;
using MGFM.Models.FS;
using MgMvvmTools;

namespace MGFM.ViewModels.WindowsViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private FileManagerTab _currentTab;
        public ObservableCollection<FileManagerTab> Tabs { get; } = new() { new FileManagerTab(@"C:\Users\SU") };

        public FileManagerTab CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            CurrentTab = Tabs.FirstOrDefault();
        }

        public void OpenInNewTab(string folderPath)
        {
            var newTab = new FileManagerTab(folderPath);
            Tabs.Add(newTab);
            CurrentTab = newTab;
        }
    }
}
