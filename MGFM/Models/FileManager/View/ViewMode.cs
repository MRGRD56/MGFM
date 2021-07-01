using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MgMvvmTools;

namespace MGFM.Models.FileManager.View
{
    public class ViewMode : NotifyPropertyChanged
    {
        private bool _isActive;
        public string Title { get; }

        public string IconName { get; }

        public double IconSize { get; }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }

        public ViewMode(string title, string iconName, double iconSize = 24)
        {
            Title = title;
            IconName = iconName;
            IconSize = iconSize;
        }
    }
}
