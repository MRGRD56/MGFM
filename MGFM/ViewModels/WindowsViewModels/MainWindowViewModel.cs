using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
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
            Tabs = new ObservableCollection<FileManagerTab> { new(@"C:\Users\SU", OnNavigated) };
            CurrentTab = Tabs.FirstOrDefault();
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
            //FIXME КОСТЫЛЬ
            //var currentTab = CurrentTab;
            //SynchronizationContext.Current.Send(() =>
            //{
            //    CurrentTab = null;
            //});
            //SynchronizationContext.Current.Send(() =>
            //{
            //    CurrentTab = currentTab;
            //});
        }
    }
}
