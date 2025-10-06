using Ink_Canvas_Better.Helpers.Others;
using Ink_Canvas_Better.ViewModel;
using iNKORE.UI.WPF.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ink_Canvas_Better.Windows.FloatingBarIcons
{
    // imperfect but works, need to be improved
    public partial class FloatingBar_Eraser : UserControl
    {
        private bool _isSquare = false;

        public FloatingBar_Eraser()
        {
            InitializeComponent();
        }

        private void Slider_StrokeThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ChangeToEraserMode();
            ChangeEraserShape();
        }

        private void ToggleButton_EraseByStroke_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Circular.IsEnabled = false;
            ToggleButton_Square.IsEnabled = false;
            ToggleButton_EraseByStroke.IsChecked = true;
            ToggleButton_EraseByPoint.IsChecked = false;
            RuntimeData.mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Eraser;
            RuntimeData.CurrentEraserMode = RuntimeData.EraserMode.Stroke;
        }

        private void ToggleButton_EraseByPoint_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Circular.IsEnabled = true;
            ToggleButton_Square.IsEnabled = true;
            ToggleButton_EraseByStroke.IsChecked = false;
            ToggleButton_EraseByPoint.IsChecked = true;
            RuntimeData.mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Eraser;
            RuntimeData.CurrentEraserMode = RuntimeData.EraserMode.Point;
        }

        private void ToggleButton_Square_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Square.IsChecked = true;
            ToggleButton_Circular.IsChecked = false;
            Preview_Square.Visibility = Visibility.Visible;
            Preview_Circular.Visibility = Visibility.Collapsed;
            _isSquare = true;
            ChangeToEraserMode();
            ChangeEraserShape();
        }

        private void ToggleButton_Circular_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Square.IsChecked= false;
            ToggleButton_Circular.IsChecked = true;
            Preview_Square.Visibility = Visibility.Collapsed;
            Preview_Circular.Visibility = Visibility.Visible;
            _isSquare = false;
            ChangeToEraserMode();
            ChangeEraserShape();
        }

        private void ChangeEraserShape()
        {
            var value = Slider_StrokeThickness.Value;
            if (RuntimeData.mainWindow != null)
            {
                if (_isSquare)
                {
                    RuntimeData.CurrentEraserShape = new RectangleStylusShape(value, value);
                }
                else
                {
                    RuntimeData.CurrentEraserShape = new EllipseStylusShape(value, value);
                }
                // it's necessary
                RuntimeData.mainWindow.MainInkCanvas.EraserShape = RuntimeData.CurrentEraserShape;
                RuntimeData.mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
        }

        private void ChangeToEraserMode()
        {
            if (RuntimeData.mainWindow != null && (RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Eraser))
            {
                RuntimeData.mainWindow.MainInkCanvas_Hitable = true;
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Eraser;
                if (RuntimeData.CurrentEraserMode == RuntimeData.EraserMode.Stroke)
                {
                    RuntimeData.mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                }
                else
                {
                    RuntimeData.mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                }
            }
        }
    }
}
