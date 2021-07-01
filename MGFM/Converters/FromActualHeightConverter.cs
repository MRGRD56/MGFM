using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MGFM.Converters
{
    public class FromActualHeightConverter : IValueConverter
    {
        private const double Increment = 15;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var actualHeight = (double) value;
            return actualHeight - Increment;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var height = (double) value;
            return height + Increment;
        }
    }
}
