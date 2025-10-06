using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Brush = System.Windows.Media.Brush;
using FontFamily = System.Windows.Media.FontFamily;
using Image = System.Windows.Controls.Image;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// ICB_Button
    /// </summary>
    public partial class ICB_Button : UserControl
    {
        private FontIcon fontIcon;

        public ICB_Button()
        {
            InitializeComponent();

            InnerButton.Click += (sender, e) =>
            {
                RaiseEvent(new RoutedEventArgs(ClickEvent, this));
            };
        }

        #region Properties

        #region Property_Text

        /// <summary>
        /// Change the text
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(String),
                typeof(ICB_Button),
                new PropertyMetadata(Text_OnValueChanged)
            );

        public String Text
        {
            get => (String)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void Text_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_Button)dependencyObject;
            control.TextBlock_1.Text = (String)eventArgs.NewValue;
        }

        /// <summary>
        /// Show or hide the text
        /// </summary>
        public static readonly DependencyProperty ShowTextProperty =
            DependencyProperty.Register(
                "IsShowText",
                typeof(bool),
                typeof(ICB_Button),
                new PropertyMetadata(true, ShowText_OnValueChanged)
            );

        public bool IsShowText
        {
            get => (bool)GetValue(ShowTextProperty);
            set => SetValue(ShowTextProperty, value);
        }

        private static void ShowText_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            ShowTextCheck(dependencyObject);
        }

        #endregion

        #region Property_Squeeze

        /// <summary>
        /// Squeeze the control
        /// </summary>
        public static readonly DependencyProperty SqueezeProperty =
            DependencyProperty.Register(
                "IsSqueezeHorizontally",
                typeof(bool),
                typeof(ICB_Button),
                new PropertyMetadata(Squeeze_OnValueChanged)
            );

        public bool IsSqueezeHorizontally
        {
            get => (bool)GetValue(SqueezeProperty);
            set => SetValue(SqueezeProperty, value);
        }

        private static void Squeeze_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            const int SQUEEZE = 5;
            var control = (ICB_Button)dependencyObject;

            if ((bool)eventArgs.NewValue)
            {
                control.Width -= SQUEEZE;
                control.SimpleStackPanel_1.Width -= SQUEEZE;
                control.TextBlock_1.Width -= SQUEEZE;
                control.InnerButton.Width -= SQUEEZE;
                control.Border.Width = control.Width - SQUEEZE;
            }
        }

        #endregion

        #region Property_CornerRadius

        /// <summary>
        /// CornerRadius
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                "CornerRadius",
                typeof(double),
                typeof(ICB_Button),
                new PropertyMetadata(CornerRadius_OnValueChanged)
            );

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void CornerRadius_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_Button)dependencyObject;

            control.Border.CornerRadius = new CornerRadius((double)eventArgs.NewValue);
        }

        #endregion

        #region Property_Status

        /// <summary>
        /// The status of the button
        /// </summary>
        public static readonly DependencyProperty StatusEnableRadiusProperty =
            DependencyProperty.Register(
                "IsStatusEnable",
                typeof(bool),
                typeof(ICB_Button),
                new PropertyMetadata(StatusEnable_OnValueChanged)
            );

        public bool IsStatusEnable
        {
            get => (bool)GetValue(StatusEnableRadiusProperty);
            set => SetValue(StatusEnableRadiusProperty, value);
        }

        private static void StatusEnable_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_Button)dependencyObject;
            if ((bool)eventArgs.NewValue)
            {
                control.Border.SetResourceReference(Border.BackgroundProperty, "ICBButtonStateEnable");
            }
            else
            {
                control.Border.SetResourceReference(Border.BackgroundProperty, "DefaultBackgroundColor_Opacity");
            }
        }

        #endregion

        #region Property_Icon

        /// <summary>
        /// Use fontIcon as its icon
        /// </summary>
        public static readonly DependencyProperty FontIconProperty =
            DependencyProperty.Register(
                "FontIcon",
                typeof(String),
                typeof(ICB_Button),
                new PropertyMetadata(FontIcon_OnValueChanged)
            );

        public String FontIcon
        {
            get => (String)GetValue(FontIconProperty);
            set => SetValue(FontIconProperty, value);
        }

        private static void FontIcon_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var fontIcon = new FontIcon((string)eventArgs.NewValue, (FontFamily)Application.Current.Resources["FluentIconFontFamily"]);
            SetIcon(dependencyObject, fontIcon);
        }

        /// <summary>
        /// Use image as its icon
        /// </summary>
        public static readonly DependencyProperty ImgProperty =
            DependencyProperty.Register(
                "Img",
                typeof(String),
                typeof(ICB_Button),
                new PropertyMetadata(Img_OnValueChanged)
            );

        public String Img
        {
            get => (String)GetValue(ImgProperty);
            set => SetValue(ImgProperty, value);
        }

        private static void Img_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var img = new Image { Source = new BitmapImage(new Uri((string)eventArgs.NewValue, UriKind.Relative)) };
            SetIcon(dependencyObject, img);
        }

        #endregion

        #region Property_Background

        /// <summary>
        /// Background (InnerButton)
        /// </summary>
        public static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush),
                typeof(ICB_Button),
                new PropertyMetadata(Background_OnValueChanged)
            );

        public new Brush Background
        {
            get => (Brush)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void Background_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_Button)dependencyObject;
            control.InnerButton.Background = (Brush)eventArgs.NewValue;
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Click event
        /// </summary>
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ICB_Button)
            );

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion

        /// <summary>
        /// 检查是否显示文本，对控件大小进行调整
        /// </summary>
        /// <param name="dependencyObject"></param>
        private static void ShowTextCheck(DependencyObject dependencyObject)
        {
            var control = (ICB_Button)dependencyObject;

            if (control.IsShowText)
            {
                control.TextBlock_1.Visibility = Visibility.Visible;
                control.SimpleStackPanel_1.Height = 38;
                if (control.SimpleStackPanel_1.Children[0] is FontIcon)
                {
                    control.fontIcon.Height = control.SimpleStackPanel_1.Height;
                    control.fontIcon.FontSize = control.fontIcon.Height - 10;
                }
            }
            else
            {
                control.TextBlock_1.Visibility = Visibility.Collapsed;
                control.SimpleStackPanel_1.Height = 58;
                if (control.SimpleStackPanel_1.Children[0] is FontIcon)
                {
                    control.fontIcon.Height = control.SimpleStackPanel_1.Height;
                    control.fontIcon.FontSize = control.fontIcon.Height - 20;
                }
            }
        }

        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="dependencyObject"></param>
        public static void SetIcon(DependencyObject dependencyObject, UIElement UIElement)
        {
            var control = (ICB_Button)dependencyObject;
            control.SimpleStackPanel_1.Children.Clear();
            control.SimpleStackPanel_1.Children.Add(UIElement);
            if (UIElement is FontIcon icon)
            {
                control.fontIcon = icon;
            }
            ShowTextCheck(dependencyObject);
        }
    }
}
