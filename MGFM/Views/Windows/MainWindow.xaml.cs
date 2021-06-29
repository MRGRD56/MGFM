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
using MGFM.Extensions;
using MGFM.Models.FS;
using MGFM.ViewModels.WindowsViewModels;

namespace MGFM.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => this.GetDataContext<MainWindowViewModel>();
        
        private IInputElement GetFocusedElement() => FocusManager.GetFocusedElement(this);
        private T GetFocusedElement<T>() where T : class, IInputElement => GetFocusedElement() as T;

        //private TabItem SelectedTabItem => FileManagerTabControl.SelectedItem as TabItem;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnFileDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HandleFileOpen(sender);
        }

        private static FileBase GetFileFromDataGridRowSender(object sender)
        {
            var row = (DataGridRow) sender;
            var file = row.GetDataContext<FileBase>();
            return file;
        }

        private void OnFileKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.Enter)
            {
                HandleFileOpen(sender);
                e.Handled = true;
            }
        }

        private void HandleFileOpen(object dataGridRowSender)
        {
            var fileBase = GetFileFromDataGridRowSender(dataGridRowSender);
            switch (fileBase)
            {
                case File file:
                    file.Open();
                    break;
                case Folder:
                    ViewModel.CurrentTab.Navigate(fileBase.Path);
                    break;
            }
        }

        private void OnFileMouseDown(object sender, MouseButtonEventArgs e)
        {
            var fileBase = GetFileFromDataGridRowSender(sender);

            if (e.ChangedButton != MouseButton.Middle) return;
            
            switch (fileBase)
            {
                case Folder folder:
                    ViewModel.OpenInNewTab(folder.Path);
                    break;
                case File file:
                    file.OpenWith();
                    break;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //var focusedElement = GetFocusedElement<FrameworkElement>();
            //if (focusedElement == null) return;

            //if (e.Key is Key.Up or Key.Down)
            //{
            //    if (focusedElement.DataContext is FileBase) return;


            //}
        }

        private void OnCurrentPathTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentTab.ActualizeCurrentPath();
        }
    }
}
