using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hotel_Fufel.Converters
{
    public class BoolToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool)value ? "Да" : "Нет";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString().ToLower() == "да";
    }
}
