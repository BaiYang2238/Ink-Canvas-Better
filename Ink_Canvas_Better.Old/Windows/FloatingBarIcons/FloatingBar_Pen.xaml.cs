using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.ViewModel;

namespace Ink_Canvas_Better.Windows.FloatingBarIcons
{
    public partial class FloatingBar_Pen : UserControl
    {
        private readonly Collection<UIElement> CustomColorCollection = new Collection<UIElement>();
        
        public FloatingBar_Pen()
        {
            InitializeComponent();

            AddAllCustomColorToCollection();
            RuntimeData.floatingBar_Pen = this;
            AddHandler(ICB_CustomColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
        }

        private void OnColorSelected(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            if (e.OriginalSource is ICB_CustomColor customSelector)
            {
                ColorPreview.Fill = new SolidColorBrush(customSelector.Color);
                RuntimeData.CurrentDrawingAttributes_Pen.Color = customSelector.Color;
                foreach (ICB_CustomColor item in CustomColorCollection.Cast<ICB_CustomColor>())
                {
                    item.IsSelected = false;
                }
                customSelector.IsSelected = true;
            }
        }

        private void AddAllCustomColorToCollection()
        {
            CustomColorCollection.Add(Color0);
            CustomColorCollection.Add(Color1);
            CustomColorCollection.Add(Color2);
            CustomColorCollection.Add(Color3);
            CustomColorCollection.Add(Color4);
            CustomColorCollection.Add(Color5);
            CustomColorCollection.Add(Color6);
            CustomColorCollection.Add(Color7);
            CustomColorCollection.Add(Color8);
            CustomColorCollection.Add(Color9);
            CustomColorCollection.Add(Color10);
        }

        private void Slider_StrokeThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SwitchEdittingMode();
            RuntimeData.CurrentDrawingAttributes_Pen.Width = e.NewValue;
            RuntimeData.CurrentDrawingAttributes_Pen.Height = e.NewValue;
        }

        public void ToggleButton_inkStyle_Unchecked(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            RuntimeData.SettingModel.Runtime.InkStyle = InkStyle.Default;
            //inkstyleTextBlock.Text = Application.Current.Resources["Off"].ToString();
            ToggleButton_inkStyle.IsChecked = false;
            Setting.SaveSettings();
        }

        public void ToggleButton_inkStyle_Checked(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            RuntimeData.SettingModel.Runtime.InkStyle = InkStyle.Simulative;
            //inkstyleTextBlock.Text = Application.Current.Resources["On"].ToString();
            ToggleButton_inkStyle.IsChecked = true;
            Setting.SaveSettings();
        }

        private void SwitchEdittingMode()
        {
            if (RuntimeData.mainWindow != null && RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Pen)
            {
                RuntimeData.mainWindow.MainInkCanvas_Hitable = true;
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Pen;
                RuntimeData.mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.MainInkCanvas.DefaultDrawingAttributes = RuntimeData.CurrentDrawingAttributes_Pen;
            }
        }

        private void ToggleButton_CustomColor_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ICB_CustomColor item in CustomColorCollection.Cast<ICB_CustomColor>())
            {
                item.IsCustomizingColor = true;
            }
        }

        private void ToggleButton_CustomColor_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (ICB_CustomColor item in CustomColorCollection.Cast<ICB_CustomColor>())
            {
                item.IsCustomizingColor = false;
            }
        }
    }
}
