using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.ViewModel;

namespace Ink_Canvas_Better.Pages.SettingPages
{
    /// <summary>
    /// Appearance.xaml 的交互逻辑
    /// </summary>
    public partial class Appearance : Page
    {
        public Appearance()
        {
            InitializeComponent();
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Theme_ComboBox.SelectedIndex)
            {
                case 0:
                    RuntimeData.SettingModel.Appearance.Theme = null;
                    Setting.ApplySystemTheme(null);
                    break;
                case 1:
                    RuntimeData.SettingModel.Appearance.Theme = true;
                    Setting.ApplySystemTheme(true);
                    break;
                case 2:
                    RuntimeData.SettingModel.Appearance.Theme = false;
                    Setting.ApplySystemTheme(false);
                    break;
            }
            Setting.SaveSettings();
        }
    }
}
