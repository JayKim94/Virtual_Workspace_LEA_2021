using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Converters
{
    public class ExtensionToIconSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case ".png":
                    return @"/Images/Extension/png.png";
                case ".jpg":
                case ".jpeg":
                    return @"/Images/Extension/jpg.png";
                case ".doc":
                case ".docx":
                    return @"/Images/Extension/doc.png";
                case ".gif":
                    return @"/Images/Extension/gif.png";
                case ".svg":
                    return @"/Images/Extension/svg.png";
                case ".sql":
                    return @"/Images/Extension/sql.png";
                case ".zip":
                    return @"/Images/Extension/zip.png";
                case ".html":
                    return @"/Images/Extension/html.png";
                case ".css":
                    return @"/Images/Extension/css.png";
                case ".avi":
                    return @"/Images/Extension/avi.png";
                case ".js":
                    return @"/Images/Extension/js.png";
                case ".eps":
                    return @"/Images/Extension/eps.png";
                case ".pdf":
                    return @"/Images/Extension/pdf.png";
                case ".php":
                    return @"/Images/Extension/php.png";
                default:
                    return @"/Images/Extension/txt.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
