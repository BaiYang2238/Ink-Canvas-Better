using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        bool isFloatingBatFolded = false;

        public void FoldFloatingBar()
        {
            Border_DrawingTools.Visibility = Visibility.Collapsed;
            Border_Others.Visibility = Visibility.Collapsed;
            Border_TouchSetting.Visibility = Visibility.Collapsed;
            Border_ExitPPT.Visibility = Visibility.Collapsed;
            isFloatingBatFolded = true;
        }

        public void UnFoldFloatingBar()
        {
            Border_DrawingTools.Visibility = Visibility.Visible;
            Border_Others.Visibility = Visibility.Visible;
            Border_TouchSetting.Visibility = Visibility.Visible;
            Border_ExitPPT.Visibility = Visibility.Visible;
            isFloatingBatFolded = false;
        }

        public void SwitchFloatingBatFoldingState()
        {
            if (isFloatingBatFolded)
            {
                UnFoldFloatingBar();
            }
            else
            {
                FoldFloatingBar();
            }
        }

        private void DockWindowToBottom(object sender, EventArgs e)
        {
            double workHeight = SystemParameters.WorkArea.Height;
            double workWidth = SystemParameters.WorkArea.Width;

            var h = floatingBar.ActualHeight;
            var w = floatingBar.ActualWidth;
            ((TranslateTransform)floatingBar.RenderTransform).Y = workHeight - h - 5;
            ((TranslateTransform)floatingBar.RenderTransform).X = (workWidth - w)/2;
        }

    }
}
