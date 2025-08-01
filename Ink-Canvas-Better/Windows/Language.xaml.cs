﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ink_Canvas_Better.Windows
{
    /// <summary>
    /// Language.xaml 的交互逻辑
    /// </summary>
    public partial class Language : Window
    {
        public Language()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private readonly ArrayList languages = new ArrayList()
        {
            "zh-CN => 简体中文",
            "en-US => English"
        };

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LanguageListBox.ItemsSource = languages;
        }
        private void LanguageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = LanguageListBox.SelectedIndex != -1 ? OK.IsEnabled = true : OK.IsEnabled = false;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
