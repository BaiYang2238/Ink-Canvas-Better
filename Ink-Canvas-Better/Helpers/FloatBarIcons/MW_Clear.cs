﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        public void ClearIcon_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }
    }
}
