using Ink_Canvas_Better.Pages.SettingPages;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using iUWM = iNKORE.UI.WPF.Modern;

namespace Ink_Canvas_Better.Windows
{
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        private void Navigation_SelectionChanged(iUWM.Controls.NavigationView sender, iUWM.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            switch (((iUWM.Controls.NavigationViewItem)sender.SelectedItem).Name)
            {
                case "SettingNaviagtion_Item_Home":
                    SettingFrame.Navigate(new Home());
                    break;
                case "SettingNaviagtion_Item_StartupAndUpdate":
                    break;
                case "SettingNaviagtion_Item_Appearance":
                    SettingFrame.Navigate(new Appearance());
                    break;
                case "SettingNaviagtion_Item_PPT":
                    break;
                case "SettingNaviagtion_Item_ExperimentalFeatures":
                    break;
            }
        }
    }
}
