using Ink_Canvas.Helpers;
using Ink_Canvas.Windows;
using Ink_Canvas.Windows.Controls;
using iNKORE.UI.WPF.Controls;
using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Media;
using File = System.IO.File;
using MessageBox = System.Windows.MessageBox;

namespace Ink_Canvas
{
    public partial class MainWindow : Window
    {

        #region Window Initialization

        Magnify MagnifyWindow;
        public MainWindow()
        {
            /*
                处于画板模式内：Topmost == false / currentMode != 0
                处于 PPT 放映内：BtnPPTSlideShowEnd.Visibility
            */
            InitializeComponent();

            HideAllSetting();

            BlackboardLeftSide.Visibility = Visibility.Collapsed;
            BlackboardCenterSide.Visibility = Visibility.Collapsed;
            BlackboardRightSide.Visibility = Visibility.Collapsed;

            BorderTools.Visibility = Visibility.Collapsed;
            BorderSettings.Visibility = Visibility.Collapsed;

            BtnPPTSlideShowEnd.Visibility = Visibility.Collapsed;
            PPTNavigationBottomLeft.Visibility = Visibility.Collapsed;
            PPTNavigationBottomRight.Visibility = Visibility.Collapsed;
            PPTNavigationSidesLeft.Visibility = Visibility.Collapsed;
            PPTNavigationSidesRight.Visibility = Visibility.Collapsed;

            BorderSettings.Margin = new Thickness(0, 150, 0, 150);

            TwoFingerGestureBorder.Visibility = Visibility.Collapsed;
            BoardTwoFingerGestureBorder.Visibility = Visibility.Collapsed;
            BorderDrawShape.Visibility = Visibility.Collapsed;
            BoardBorderDrawShape.Visibility = Visibility.Collapsed;

            GridInkCanvasSelectionCover.Visibility = Visibility.Collapsed;

            ViewboxFloatingBar.Margin = new Thickness((SystemParameters.WorkArea.Width - 284) / 2, SystemParameters.WorkArea.Height - 60, -2000, -200);
            ViewboxFloatingBarMarginAnimation(100);

            try
            {
                if (File.Exists("Log.txt"))
                {
                    FileInfo fileInfo = new FileInfo("Log.txt");
                    long fileSizeInKB = fileInfo.Length / 1024;
                    if (fileSizeInKB > 512)
                    {
                        try
                        {
                            File.Delete("Log.txt");
                            LogHelper.WriteLogToFile("The Log.txt file has been successfully deleted. Original file size: " + fileSizeInKB + " KB", LogHelper.LogType.Info);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLogToFile(ex + " | Can not delete the Log.txt file. File size: " + fileSizeInKB + " KB", LogHelper.LogType.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }

            InitTimers();
            timeMachine.OnRedoStateChanged += TimeMachine_OnRedoStateChanged;
            timeMachine.OnUndoStateChanged += TimeMachine_OnUndoStateChanged;
            inkCanvas.Strokes.StrokesChanged += StrokesOnStrokesChanged;

            Microsoft.Win32.SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            try
            {
                if (File.Exists("SpecialVersion.ini")) SpecialVersionResetToSuggestion_Click();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }

            CheckColorTheme(true);
        }

        #endregion

        #region Ink Canvas Functions

        DrawingAttributes drawingAttributes;
        private void loadPenCanvas()
        {
            try
            {
                drawingAttributes = inkCanvas.DefaultDrawingAttributes;
                drawingAttributes.Color = Colors.Red;

                drawingAttributes.Height = 2.5;
                drawingAttributes.Width = 2.5;

                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                inkCanvas.Gesture += InkCanvas_Gesture;
            }
            catch { }
        }

        private void InkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            ReadOnlyCollection<GestureRecognitionResult> gestures = e.GetGestureRecognitionResults();
            try
            {
                foreach (GestureRecognitionResult gest in gestures)
                {
                    if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible)
                    {
                        if (gest.ApplicationGesture == ApplicationGesture.Left)
                        {
                            BtnPPTSlidesDown_Click(null, null);
                        }
                        if (gest.ApplicationGesture == ApplicationGesture.Right)
                        {
                            BtnPPTSlidesUp_Click(null, null);
                        }
                    }
                }
            }
            catch { }
        }

        private void inkCanvas_EditingModeChanged(object sender, RoutedEventArgs e)
        {
            var inkCanvas1 = sender as InkCanvas;
            if (inkCanvas1 == null) return;
            if (Settings.Canvas.IsShowCursor)
            {
                if (inkCanvas1.EditingMode == InkCanvasEditingMode.Ink || drawingShapeMode != 0)
                {
                    inkCanvas1.ForceCursor = true;
                }
                else
                {
                    inkCanvas1.ForceCursor = false;
                }
            }
            else
            {
                inkCanvas1.ForceCursor = false;
            }
            if (inkCanvas1.EditingMode == InkCanvasEditingMode.Ink) forcePointEraser = !forcePointEraser;
        }

        #endregion Ink Canvas Functions

        #region Definations and Loading

        public static SettingsClass Settings = new SettingsClass();
        public static string settingsFileName = "Settings.json";
        public static bool isLoaded = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadPenCanvas();
            //加载设置
            LoadSettings(true);
            if (Environment.Is64BitProcess)
            {
                GroupBoxInkRecognition.Visibility = Visibility.Collapsed;
            }

            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            SystemEvents_UserPreferenceChanged(null, null);

            // 显示“beta”字样
            String[] Version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            AppVersionTextBlock.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (int.TryParse(Version[3], out int i))
            {
                if (i > 0)
                {
                    AppVersionTextBlock.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - beta";
                }
            }

            LogHelper.WriteLogToFile("Ink Canvas Loaded", LogHelper.LogType.Event);
            isLoaded = true;

            RegisterGlobalHotkeys();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LogHelper.WriteLogToFile("Ink Canvas closing", LogHelper.LogType.Event);
            if (!CloseIsFromButton && Settings.Advanced.IsSecondConfimeWhenShutdownApp)
            {
                e.Cancel = true;
                if (MessageBox.Show("是否继续关闭 Ink Canvas 画板，这将丢失当前未保存的工作。", "Ink Canvas 画板", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    if (MessageBox.Show("真的狠心关闭 Ink Canvas 画板吗？", "Ink Canvas 画板", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
                    {
                        if (MessageBox.Show("是否取消关闭 Ink Canvas 画板？", "Ink Canvas 画板", MessageBoxButton.OKCancel, MessageBoxImage.Error) != MessageBoxResult.OK)
                        {
                            e.Cancel = false;
                        }
                    }
                }
            }
            if (e.Cancel)
            {
                LogHelper.WriteLogToFile("Ink Canvas closing cancelled", LogHelper.LogType.Event);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LogHelper.WriteLogToFile("Ink Canvas closed", LogHelper.LogType.Event);
        }

        #endregion Definations and Loading

        #region Setting Pages

        private void HideAllSetting()
        {
            Setting_General1.Visibility = Visibility.Collapsed;
            Setting_General2.Visibility = Visibility.Collapsed;
            Setting_StartAndUpgrade.Visibility = Visibility.Collapsed;
            GroupBoxAppearanceNewUI.Visibility = Visibility.Collapsed;
            Setting_CanvasAndGesture1.Visibility = Visibility.Collapsed;
            GroupBoxInkRecognition.Visibility = Visibility.Collapsed;
            Setting_CanvasAndGesture2.Visibility = Visibility.Collapsed;
            Setting_PPT.Visibility = Visibility.Collapsed;
            Setting_Advance.Visibility = Visibility.Collapsed;
            Setting_Others.Visibility = Visibility.Collapsed;
            Setting_Exp.Visibility = Visibility.Collapsed;
        }

        private void BtnSettingGeneral_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_General1.Visibility = Visibility.Visible;
            Setting_General2.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 基本";
        }

        private void BtnSettingStartAndUpgrade_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_StartAndUpgrade.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 启动与更新";
        }

        private void BtnSettingAppearance_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            GroupBoxAppearanceNewUI.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 外观";
        }

        private void BtnSettingCanvasAndGesture_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_CanvasAndGesture1.Visibility = Visibility.Visible;
            GroupBoxInkRecognition.Visibility = Visibility.Visible;
            Setting_CanvasAndGesture2.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 画板与手势";
        }

        private void BtnSettingPPT_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_PPT.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - PPT";
        }

        private void BtnSettingAdvance_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_Advance.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 高级";
        }

        private void BtnSettingExp_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_Exp.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 实验性功能";
        }
        private void BtnSettingOthers_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_Others.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 其它";
        }

        #endregion

        #region Shortcut

        ShortcutSetting[] ShortcutSetting;
        public void LoadShortcuts()
        {
            if (Settings.Shortcut.ShortcutName == null)
            {
                return;
            }
            int shortcutAmount = Settings.Shortcut.ShortcutName.Count;
            Setting_Shortcuts_Edit.Children.Clear();
            ShortcutSetting = new ShortcutSetting[shortcutAmount];
            
            for (int i = 0; i < shortcutAmount; i++)
            {
                // 务必注意，此处 i 与 List 中的索引一致
                ShortcutSetting[i] = new ShortcutSetting(i, Settings.Shortcut.ShortcutEnable[i], Settings.Shortcut.ShortcutName[i], Settings.Shortcut.ShortcutUrls[i]);
                Setting_Shortcuts_Edit.Children.Add(ShortcutSetting[i]);
                ShortcutSetting[i].Visibility = Visibility.Visible;
            }
        }

        private void BtnNewShortcut_Click(object sender, RoutedEventArgs e)
        {
            Settings.Shortcut.ShortcutName.Add("显示的名称");
            Settings.Shortcut.ShortcutUrls.Add("修改为文件、程序或网页的URL");
            Settings.Shortcut.ShortcutEnable.Add(true);
            SaveSettingsToFile();

            LoadShortcuts();
        }

        #endregion

    }

}