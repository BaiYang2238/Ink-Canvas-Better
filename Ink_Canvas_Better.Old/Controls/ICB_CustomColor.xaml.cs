using Ink_Canvas_Better.ViewModel;
using Ink_Canvas_Better.Windows.FloatingBarIcons;
using iNKORE.UI.WPF.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better.Controls
{
    public partial class ICB_CustomColor : UserControl
    {
        public ICB_CustomColor()
        {
            InitializeComponent();
            ICB_ColorPicker.AddColorPicker_ColorSelectedEventHandler(this, ColorPicker_ColorSelectedEventHandler);
        }

        #region Properties

        #region Color

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(Color),
                typeof(ICB_CustomColor),
                new PropertyMetadata(Color_OnValueChanged)
            );

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void Color_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_CustomColor)dependencyObject;
            control.InnerBorder.Background = new SolidColorBrush((Color)eventArgs.NewValue);
        }

        #endregion

        #region IsSelected

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                "IsSelected",
                typeof(bool),
                typeof(ICB_CustomColor),
                new PropertyMetadata(IsSelected_OnValueChanged)
            );

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        private static void IsSelected_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_CustomColor)dependencyObject;
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

        #region IsCustomizingColor

        public static readonly DependencyProperty IsCustomizingColorProperty =
            DependencyProperty.Register(
                "IsCustomizingColor",
                typeof(bool),
                typeof(ICB_CustomColor),
                new PropertyMetadata(IsCustomizingColor_OnValueChanged)
            );

        public bool IsCustomizingColor
        {
            get { return (bool)GetValue(IsCustomizingColorProperty); }
            set { SetValue(IsCustomizingColorProperty, value); }
        }

        private static void IsCustomizingColor_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            // do nothing
        }

        #endregion

        #endregion

        #region Events

        public static readonly RoutedEvent ColorSelectedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorSelected",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ICB_CustomColor));

        public event RoutedEventHandler ColorSelected
        {
            add => AddHandler(ColorSelectedEvent, value);
            remove => RemoveHandler(ColorSelectedEvent, value);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsCustomizingColor)
            {
                if (RuntimeData.colorPicker == null)
                {
                    RuntimeData.colorPicker = new ICB_ColorPicker();
                    if (this.FindAscendant<FloatingBar_Pen>() != null)
                    {
                        RuntimeData.mainWindow.Popup_Pen.StaysOpen = true;
                    }
                    else if (this.FindAscendant<FloatingBar_Highlighter>() != null)
                    {
                        RuntimeData.mainWindow.Popup_Highlighter.StaysOpen = true;
                    }
                    RuntimeData.mainWindow.MainWindow_Grid.Children.Add(RuntimeData.colorPicker);
                }
                RuntimeData.colorPicker.PlacementTarget = sender as UIElement;
                RuntimeData.colorPicker.IsOpen = false;
                RuntimeData.colorPicker.IsOpen = true;
                RuntimeData.colorPicker.SelectedColor = this.Color;
            }
            else
            {
                var args = new RoutedEventArgs(ColorSelectedEvent, this);
                RaiseEvent(args);
                e.Handled = true;
            }
        }

        private void ColorPicker_ColorSelectedEventHandler(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is ICB_ColorPicker colorPicker)
            {
                this.Color = colorPicker.SelectedColor;
            }
        }
    }
}
