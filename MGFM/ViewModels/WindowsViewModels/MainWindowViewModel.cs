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
        public ObservableCollection<FileManagerTab> Tabs { get; set; } = new() { new FileManagerTab(@"C:\Users\SU") };

        public FileManagerTab CurrentTab { get; set; }

        public MainWindowViewModel()
        {
            CurrentTab = Tabs.FirstOrDefault();
        }
    }
}
