using Ink_Canvas_Better.Helpers.Others;
using Ink_Canvas_Better.ViewModel;
using iNKORE.UI.WPF.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ink_Canvas_Better.Windows.FloatingBarIcons
{
    public partial class FloatingBar_Shape : UserControl
    {
        public FloatingBar_Shape()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RuntimeData.CurrentDrawStep = 0;
            RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Shape;
            RuntimeData.CurrentShape = ((Image)sender).Name;
        }
    }
}
