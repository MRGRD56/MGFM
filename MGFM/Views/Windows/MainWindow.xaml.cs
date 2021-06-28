using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MGFM.Models.FS;
using MGFM.ViewModels.WindowsViewModels;

namespace MGFM.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel) DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnFileDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HandleFileOpen(sender);
        }

        private void OnFileKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.Enter)
            {
                HandleFileOpen(sender);
            }
        }

        private void HandleFileOpen(object dataGridRowSender)
        {
            var row = (DataGridRow) dataGridRowSender;
            var file = (FileBase) row.DataContext;
            switch (file)
            {
                case File:
                    Process.Start("explorer.exe", file.Path);
                    break;
                case Folder:
                    ViewModel.CurrentTab.Navigate(file.Path);
                    break;
            }
        }
    }
}
