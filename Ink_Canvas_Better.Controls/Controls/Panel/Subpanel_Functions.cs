using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ink_Canvas_Better.Controls.Controls.Panel
{
    public partial class Subpanel
    {
        protected virtual void OnPinButtonClicked(Object s, RoutedEventArgs args)
        {
            StaysOpen = !StaysOpen;
        }

        protected virtual void OnCloseButtonClicked(Object s, RoutedEventArgs args)
        {
            this.IsOpen = false;
        }

        public void Show()
        {
            this.IsOpen = true;
        }

        public void Hide()
        {
            this.IsOpen = false;
        }
    }
}
