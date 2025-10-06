using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Controls.Helpers;

namespace Ink_Canvas_Better.Controls.Controls.Panel
{
    public partial class Subpanel
    {
        #region TitleBar

        // title
        private readonly TextBlock _titleTextBlock = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 18,
            FontWeight = FontWeights.Bold
        };

        // close button
        private readonly Button _closeButton = new Button
        {
            Background = Brushes.Transparent,
            FontFamily = ThemeHelper.SegoeFluentIcons,
            Padding = new Thickness(0),
            Content = "\ue8bb",
            FontSize = 14,
            Width = 24,
            BorderThickness = new Thickness(0)
        };

        // pin button
        private readonly Button _pinButton = new Button
        {
            Background = Brushes.Transparent,
            FontFamily = ThemeHelper.SegoeFluentIcons,
            Padding = new Thickness(0),
            Width = 24,
            BorderThickness = new Thickness(0)
        };
        private readonly TextBlock _pinTextBlock = new TextBlock
        {
            Text = "\ue718",
            FontSize = 16
        };

        // title bar
        private readonly Grid _titleBarGrid = new Grid
        {
            Margin = new Thickness(5, 0, 5, 0),
            Height = 30,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        #endregion

        // this grid is used to keep the gap between the popup and the target element
        // i know offset property exists, but it has some issues when the target element is near the screen edge
        private readonly Grid _transparentGrid = new Grid
        {
            Background = Brushes.Transparent
        };

        private readonly Border _mainBorder = new Border
        {
            MinHeight = 30,
            Background = ThemeHelper.DefaultBackgroundColor,
            BorderBrush = ThemeHelper.DefaultBorderColor,
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(5)
        };

        private readonly Grid _mainGrid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            }
        };

        private readonly ContentPresenter _contentPresenter = new ContentPresenter
        {
            Margin = new Thickness(10)
        };
    }
}
