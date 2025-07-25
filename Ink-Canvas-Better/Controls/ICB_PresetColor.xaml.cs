﻿using Ink_Canvas_Better.Resources;
using Ink_Canvas_Better.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// ICB_PresetColor.xaml 的交互逻辑
    /// </summary>
    public partial class ICB_PresetColor : UserControl
    {
        public ICB_PresetColor()
        {
            InitializeComponent();
        }

        #region Properties

        #region Color

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color", 
                typeof(Color), 
                typeof(ICB_PresetColor), 
                new PropertyMetadata(Color_OnValueChanged)
            );

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void Color_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_PresetColor)dependencyObject;
            control.InnerBorder.Background = new SolidColorBrush((Color)eventArgs.NewValue);
        }

        #endregion

        #region IsSelected

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                "IsSelected",
                typeof(bool),
                typeof(ICB_PresetColor),
                new PropertyMetadata(IsSelected_OnValueChanged)
            );

        public bool IsSelected
        {
            get { return (bool)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void IsSelected_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_PresetColor)dependencyObject;
            if ((bool)eventArgs.NewValue)
            {
                control.Viewbox1.Visibility = Visibility.Visible;
            }
            else
            {
                control.Viewbox1.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #endregion

        #region Events

        public static readonly RoutedEvent ColorSelectedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorSelected",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ICB_PresetColor));

        public event RoutedEventHandler ColorSelected
        {
            add => AddHandler(ColorSelectedEvent, value);
            remove => RemoveHandler(ColorSelectedEvent, value);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.DrawingAttributes.Color = ((SolidColorBrush)InnerBorder.Background).Color;
            
            var args = new RoutedEventArgs(ColorSelectedEvent, this);
            RaiseEvent(args);
            e.Handled = true;
        }
    }
}
