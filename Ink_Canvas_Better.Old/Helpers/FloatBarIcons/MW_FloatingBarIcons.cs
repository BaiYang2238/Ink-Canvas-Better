using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        public void HideDrawingTools()
        {
            var _ = Visibility.Collapsed;
            ClearIcon.Visibility = _;
            HighlighterIcon.Visibility = _;
            EraserIcon.Visibility = _;
            PickIcon.Visibility = _;
            ShapeIcon.Visibility = _;
            RedoIcon.Visibility = _;
            UndoIcon.Visibility = _;
        }

        public void ShowDrawingTools()
        {
            var _ = Visibility.Visible;
            ClearIcon.Visibility = _;
            HighlighterIcon.Visibility = _;
            EraserIcon.Visibility = _;
            PickIcon.Visibility = _;
            ShapeIcon.Visibility = _;
            RedoIcon.Visibility = _;
            UndoIcon.Visibility = _;

        }
    }
}
