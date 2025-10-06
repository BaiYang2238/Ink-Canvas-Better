using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.ViewModel;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        public void ToolsIcon_Click(object sender, RoutedEventArgs e)
        {
            // Temp
            if (RuntimeData.settingWindow == null || !IsWindowValid(RuntimeData.settingWindow))
            {
                RuntimeData.settingWindow = new SettingWindow();
                RuntimeData.settingWindow.Closed += (s, args) => RuntimeData.settingWindow = null;
                RuntimeData.settingWindow.Show();
            }
            else
            {
                RuntimeData.settingWindow.Activate();

                if (RuntimeData.settingWindow.WindowState == WindowState.Minimized)
                {
                    RuntimeData.settingWindow.WindowState = WindowState.Normal;
                }

                RuntimeData.settingWindow.Topmost = true;
                RuntimeData.settingWindow.Topmost = false;
            }
        }

        // Temp
        private bool IsWindowValid(Window window)
        {
            return window != null && PresentationSource.FromVisual(window) != null;
        }
    }
}
