using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Ink_Canvas_Better.Controls.Controls.Panel
{
    [ContentProperty("Child")]
    public partial class ExPopup : Popup
    {
        static ExPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExPopup), new FrameworkPropertyMetadata(typeof(ExPopup)));
        }
    }
}
