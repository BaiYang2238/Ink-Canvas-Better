﻿using Ink_Canvas_Better.Resources;
using System;
using System.Collections.Generic;
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
        public void EraserIcon_Click(object sender, RoutedEventArgs e)
        {
            if (RuntimeData.CurrentMode != RuntimeData.Mode.Eraser)
            {
                MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.CurrentMode = RuntimeData.Mode.Eraser;
                // TODO
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                
            }
        }
    }
}
