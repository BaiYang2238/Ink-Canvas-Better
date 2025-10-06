using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Ink_Canvas_Better.Controls.Helpers
{
    public static class ThemeHelper
    {
        public static ResourceDictionary Dictionary { get; set; }
        static ThemeHelper()
        {
            Dictionary = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Ink_Canvas_Better.Controls;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
            };
        }

        #region Fonts

        public static FontFamily GetFont(string key)
        {
            if (Dictionary.Contains(key) && Dictionary[key] is FontFamily fontFamily)
            {
                return fontFamily;
            }
            return new FontFamily(key);
        }

        public static FontFamily SegoeFluentIcons => GetFont("SegoeFluentIcons");

        #endregion

        #region Colors

        public static Brush GetBrush(string key)
        {
            if (Dictionary.Contains(key) && Dictionary[key] is Brush brush)
            {
                return brush;
            }
            return Brushes.Transparent;
        }

        public static Brush DefaultBackgroundColor => GetBrush("DefaultBackgroundColor");
        public static Brush DefaultBackgroundColor_Opacity => GetBrush("DefaultBorderColor");
        public static Brush DefaultForegroundColor => GetBrush("DefaultBorderColor");
        public static Brush DefaultBorderColor => GetBrush("DefaultBorderColor");
        public static Brush DefaultButtonHoverColor => GetBrush("DefaultHoverColor");

        #endregion
    }
}
