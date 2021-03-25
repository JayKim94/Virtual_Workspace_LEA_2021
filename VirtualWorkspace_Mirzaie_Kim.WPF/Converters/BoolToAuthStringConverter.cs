using System;
using System.Globalization;
using System.Windows.Data;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Converters
{
    public class BoolToAuthStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "online" : "offline";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
