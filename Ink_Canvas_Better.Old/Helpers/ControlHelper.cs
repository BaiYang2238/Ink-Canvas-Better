using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better.Helpers
{
    class ControlHelper
    {
        static Point _mouseDownPosition;
        static Point _mouseDownControlPosition;

        public static void Drag_MouseDown(UIElement draggedControl, Point mousePosition)
        {
            _mouseDownPosition = mousePosition;
            if (draggedControl.RenderTransform == null || !(draggedControl.RenderTransform is TranslateTransform))
            {
                draggedControl.RenderTransform = new TranslateTransform();
            }
            _mouseDownControlPosition = (draggedControl.RenderTransform as TranslateTransform).Transform(new Point(0, 0));
        }

        public static void Drag_MouseMove(UIElement draggedControl, Point mousePosition)
        {
            var dp = mousePosition - _mouseDownPosition;
            if (draggedControl.RenderTransform == null || !(draggedControl.RenderTransform is TranslateTransform))
            {
                draggedControl.RenderTransform = new TranslateTransform();
            }
            var transform = draggedControl.RenderTransform as TranslateTransform;
            transform.X = _mouseDownControlPosition.X + dp.X;
            transform.Y = _mouseDownControlPosition.Y + dp.Y;
        }
    }
}
