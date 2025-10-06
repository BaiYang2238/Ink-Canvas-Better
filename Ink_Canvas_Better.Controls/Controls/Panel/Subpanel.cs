using Ink_Canvas_Better.Controls.Helpers;
using Ink_Canvas_Better.Controls.Helpers.Converter;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;

[assembly: XmlnsDefinition("Ink_Canvas_Better.Controls", "Ink_Canvas_Better.Controls.Controls.Panel")]
namespace Ink_Canvas_Better.Controls.Controls.Panel
{
    [ContentProperty("Child")]
    [TemplatePart(Name = "PART_PinButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public partial class Subpanel : Popup
    {
        static Subpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Subpanel), new FrameworkPropertyMetadata(typeof(Subpanel)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            BindingInit();

            _titleTextBlock.Text = Title;
            _titleTextBlock.SetBinding(TextBlock.TextProperty, titleBinding);

            _titleBarGrid.Background = ThemeHelper.DefaultBackgroundColor;
            _titleBarGrid.Children.Add(_titleTextBlock);
            Grid.SetColumn(_titleTextBlock, 0);

            _pinTextBlock.SetBinding(TextBlock.TextProperty, staysOpenBinding);
            _pinButton.Content = _pinTextBlock;
            _pinButton.Click += OnPinButtonClicked;
            _titleBarGrid.Children.Add(_pinButton);
            Grid.SetColumn(_pinButton, 1);

            _closeButton.Click += OnCloseButtonClicked;
            _titleBarGrid.Children.Add(_closeButton);
            Grid.SetColumn(_closeButton, 2);

            _contentPresenter.SetBinding(ContentPresenter.ContentProperty, childBinding);

            _mainGrid.Children.Add(_titleBarGrid);
            Grid.SetRow(_titleBarGrid, 0);

            _mainGrid.Children.Add(_contentPresenter);
            Grid.SetRow(_contentPresenter, 1);

            _titleBarGrid.SetBinding(VisibilityProperty, isShowHeaderBinding);

            _mainBorder.Child = _mainGrid;
            _mainBorder.SetBinding(MarginProperty, marginBinding);

            _transparentGrid.Children.Add(_mainBorder);
            Child = _transparentGrid;
        }

        private void BindingInit()
        {
            titleBinding = new Binding("Title")
            {
                Source = this
            };
            isShowHeaderBinding = new Binding("ShowHeader")
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            };

            childBinding = new Binding("Child")
            {
                Source = MemberwiseClone()
            };
            marginBinding = new Binding("Margin")
            {
                Source = this
            };
            staysOpenBinding = new Binding("StaysOpen")
            {
                Source = this,
                Converter = new BooleanToTextCconverter_Pin()
            };
        }
    }
}
