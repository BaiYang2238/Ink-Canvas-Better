using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ColorConverter = Ink_Canvas_Better.Helpers.Others.ColorConverter;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// ICB_ColorPicker.xaml 的交互逻辑
    /// </summary>
    public partial class ICB_ColorPicker : UserControl
    {
        private bool _temp = true;
        public ICB_ColorPicker()
        {
            InitializeComponent();
        }

        #region Properties

        #region IsOpen

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                "IsOpen",
                typeof(bool),
                typeof(ICB_ColorPicker),
                new PropertyMetadata(IsOpen_OnValueChanged)
            );

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static void IsOpen_OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ICB_ColorPicker;
            control.ColorPicker_Popup.IsOpen = (bool)e.NewValue;
        }

        #endregion

        #region SelectedColor

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register(
                "SelectedColor",
                typeof(Color),
                typeof(ICB_ColorPicker),
                new PropertyMetadata(SelectedColor_OnValueChanged)
            );

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static void SelectedColor_OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ICB_ColorPicker;
            control.SquareColorPicker.SelectedColor = (Color)e.NewValue;
        }

        #endregion

        #region PlacementTarget

        readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.Register(
                "PlacementTarget",
                typeof(UIElement),
                typeof(ICB_ColorPicker),
                new PropertyMetadata(PlacementTarget_OnValueChanged)
            );

        public UIElement PlacementTarget
        {
            get { return (UIElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        public static void PlacementTarget_OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ICB_ColorPicker;
            control.ColorPicker_Popup.PlacementTarget = (UIElement)e.NewValue;
        }

        #endregion

        #endregion

        #region Events

        public static readonly RoutedEvent ColorPicker_ColorSelectedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorSelected",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ICB_ColorPicker));

        public static void AddColorPicker_ColorSelectedEventHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement element)
            {
                element.AddHandler(ColorPicker_ColorSelectedEvent, handler);
            }
        }

        public static void RemoveColorPicker_ColorSelectedEventHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement element)
            {
                element.RemoveHandler(ColorPicker_ColorSelectedEvent, handler);
            }
        }

        #endregion

        private void SquarePicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            if (_temp)
            {
                _temp = false;
                Color c = SquareColorPicker.SelectedColor;
                R.Value = c.R;
                G.Value = c.G;
                B.Value = c.B;
                Hex.Text = ColorConverter.ColorToHex(c);
                SelectedColor = c;
                var args = new RoutedEventArgs(ColorPicker_ColorSelectedEvent, this);
                this.PlacementTarget.RaiseEvent(args);
                _temp = true;
            }
        }

        private void NumberBox_ValueChanged(iNKORE.UI.WPF.Modern.Controls.NumberBox sender, iNKORE.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (_temp)
            {
                _temp = false;
                Color c = new Color();
                switch (sender.Tag)
                {
                    case "RGB":
                        c = Color.FromRgb((byte)R.Value, (byte)G.Value, (byte)B.Value);
                        SquareColorPicker.SelectedColor = c;
                        Hex.Text = ColorConverter.ColorToHex(c);
                        break;
                }
                SelectedColor = c;
                var args1 = new RoutedEventArgs(ColorPicker_ColorSelectedEvent, this);
                this.PlacementTarget.RaiseEvent(args1);
                _temp = true;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_temp)
            {
                _temp = false;
                Color c;
                if (!Regex.IsMatch(((TextBox)sender).Text, @"^#?([0-9a-fA-F]{6})$"))
                {
                    Hex.Text = "000000";
                    throw new FormatException("Invalid Hex Color Format. Expected format: #RRGGBB or RRGGBB.");
                }
                else
                {
                    c = ColorConverter.HexToColor(Hex.Text);
                    SquareColorPicker.SelectedColor = c;
                    R.Value = c.R;
                    G.Value = c.G;
                    B.Value = c.B;
                }
                SelectedColor = c;
                var args = new RoutedEventArgs(ColorPicker_ColorSelectedEvent, this);
                this.PlacementTarget.RaiseEvent(args);
                _temp = true;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ColorPicker_Popup.IsOpen = false;
        }
    }
}
