using Ink_Canvas_Better.ViewModel;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        #region Cursor
        public void CursorIcon_Click(object? sender, RoutedEventArgs? e)
        {
            if (RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Cursor)
            {
                CursorIcon_ChangeToCursor();
                IsHideDrawingTools = true;
            }
        }

        public void CursorIcon_ChangeToCursor()
        {
            RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Cursor;
        }

        #endregion

        #region Pen
        public void PenIcon_Click(Object sender, RoutedEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Cursor)
            {
                IsHideDrawingTools = false;
            }
            if (RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Pen)
            {
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Pen;
            }
            else
            {
                RuntimeData.mainWindow.Popup_Pen.IsOpen = true;
            }
        }
        #endregion

        #region Highlighter
        public void HighlighterIcon_Click(object sender, RoutedEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Highlighter)
            {
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Highlighter;
            }
            else
            {
                RuntimeData.mainWindow.Popup_Highlighter.IsOpen = true;
            }
        }
        #endregion

        #region Eraser
        public void EraserIcon_Click(object sender, RoutedEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Eraser)
            {
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Eraser;
            }
            else
            {
                Popup_Eraser.IsOpen = true;
            }
        }
        #endregion

        #region Pick
        public void PickIcon_Click(object sender, RoutedEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Pick)
            {
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Pick;
            }
        }
        #endregion

        #region Clear
        public void ClearIcon_Click(object sender, RoutedEventArgs e)
        {
            MainInkCanvas.Strokes.Clear();
        }
        #endregion
    }
}
