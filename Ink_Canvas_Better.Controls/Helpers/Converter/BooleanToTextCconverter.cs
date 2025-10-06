using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ink_Canvas_Better.Controls.Helpers.Converter
{
    [Localizability(LocalizationCategory.NeverLocalize)]
    public sealed class BooleanToTextCconverter_Pin : IValueConverter
    {
        public BooleanToTextCconverter_Pin()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b;
            if (value == null)
            {
                b = false;
            }
            else
            {
                b = (bool)value;
            }
            return b ? "\ue77a" : "\ue718"; // Pinned : Unpinned
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String s)
            {
                if (s == "\ue77a")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
