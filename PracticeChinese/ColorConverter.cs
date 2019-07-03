using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PracticeChinese
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte red = 255;
            byte blue = 0;
            byte green = 0;
            int level = (int)value;
            double d = Math.Round((level / 100d * 255d));
            byte r = byte.Parse(d.ToString());
            red -= r;
            green += r;
            Color color = Color.FromRgb(red, green, blue);
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
