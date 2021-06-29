using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MGFM.Extensions
{
    public static class FrameworkElementExtensions
    {
        public static T GetDataContext<T>(this FrameworkElement frameworkElement) where T : class
        {
            return frameworkElement.DataContext as T;
        } 
    }
}
