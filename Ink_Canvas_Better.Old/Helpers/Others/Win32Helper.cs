using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Ink_Canvas_Better.Helpers.Others
{
    internal class Win32Helper
    {
        #region Windows

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public static void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            var handle = new WindowInteropHelper((Window)sender).Handle;
            int extendedStyle = GetWindowLong(handle, GWL_EXSTYLE);
            SetWindowLong(
                handle,
                GWL_EXSTYLE,
                extendedStyle | WS_EX_TOOLWINDOW
            );
        }

        #endregion

    }
}
