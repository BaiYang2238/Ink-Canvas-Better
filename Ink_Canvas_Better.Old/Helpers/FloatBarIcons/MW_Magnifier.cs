using Ink_Canvas_Better.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        public void MagnifierIcon_Click(object sender, RoutedEventArgs e)
        {
            if (MagnifierWindow.Visibility == Visibility.Visible)
            {
                MagnifierWindow.MagnifyCompleted();
                MagnifierWindow.HideMagnify_Click(null, null);
            }
            else
            {
                MagnifierWindow.MagnifierRunning();
                MagnifierWindow.Visibility = Visibility.Visible;
            }

        }

    }
}
