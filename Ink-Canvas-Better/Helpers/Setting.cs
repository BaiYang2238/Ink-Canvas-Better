﻿using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.ViewModel;
using iNKORE.UI.WPF.Modern;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using File = System.IO.File;

namespace Ink_Canvas_Better.Helpers
{
    internal class Setting
    {

        public static void LoadSettings(bool isStartup = false)
        {
            try
            {
                if (File.Exists(App.RootPath + RuntimeData.settingsFileName))
                {
                    try
                    {
                        string text = File.ReadAllText(App.RootPath + RuntimeData.settingsFileName);
                        RuntimeData.SettingProperties = JsonConvert.DeserializeObject<SettingProperties>(text);
                    }
                    catch { }
                }
                else
                {
                    ResetSettings();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.ToString(), Log.LogType.Error);
            }

            #region Startup
            if (isStartup)
            {
                // There hasn`t a need to check it.
                // Pherhaps it will be used in the future.
            }
            try
            {
                if (RuntimeData.SettingProperties.StartupAndUpdate.IsAutoStartup)
                {
                    if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Ink Canvas Better.lnk"))
                    {
                        StartAutomaticallyCreate("Ink Canvas Better");
                    }
                }
                else
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Ink Canvas Better.lnk"))
                    {
                        StartAutomaticallyDel("Ink Canvas Better");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.ToString(), Log.LogType.Error);
            }
            #endregion

            #region StartupAndUpdate
            if (RuntimeData.SettingProperties.StartupAndUpdate != null)
            {
                if (RuntimeData.SettingProperties.StartupAndUpdate.IsAutoUpdate)
                {
                    //TODO
                }
                else
                {
                    //TODO
                }
            }
            else
            { RuntimeData.SettingProperties.StartupAndUpdate = new StartupAndUpdate(); }
            #endregion

            #region Appearance
            if (RuntimeData.SettingProperties.Appearance != null)
            {
                ApplySystemTheme(RuntimeData.SettingProperties.Appearance.Theme);
            }
            else
            { RuntimeData.SettingProperties.Appearance = new Appearance(); }
            #endregion

            #region PPT
            if (RuntimeData.SettingProperties.PPT != null)
            {

            }
            else { RuntimeData.SettingProperties.PPT = new PPT(); }
            #endregion

            #region ExperimentalFeatures
            if (RuntimeData.SettingProperties.ExperimentalFeatures != null)
            {

            }
            else { RuntimeData.SettingProperties.ExperimentalFeatures = new ExperimentalFeatures(); }
            #endregion

            #region Others
            if (RuntimeData.SettingProperties.Others != null)
            {
                SwitchLanguage(RuntimeData.SettingProperties.Others.Language);
            }
            else { RuntimeData.SettingProperties.Others = new Model.Others(); }
            #endregion

            #region Runtime
            if (RuntimeData.SettingProperties.Runtime != null)
            {
                switch (RuntimeData.SettingProperties.Runtime.InkStyle)
                {
                    case InkStyle.Default:
                        RuntimeData.floatingBar_Pen.ToggleButton_inkStyle_Unchecked(null, null);
                        break;
                    case InkStyle.Simulative:
                        RuntimeData.floatingBar_Pen.ToggleButton_inkStyle_Checked(null, null);
                        break;
                }
            }
            else { RuntimeData.SettingProperties.Runtime = new Runtime(); }
            #endregion
        }

        public static void SaveSettings()
        {
            string text = JsonConvert.SerializeObject(RuntimeData.SettingProperties, Formatting.Indented);
            try
            {
                File.WriteAllText(App.RootPath + RuntimeData.settingsFileName, text);
            }
            catch { }
        }

        public static void ResetSettings()
        {
            RuntimeData.SettingProperties = new SettingProperties();
            SaveSettings();
        }

        public static bool StartAutomaticallyCreate(string exeName)
        {
            try
            {
                string f = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk";
                if (File.Exists(f))
                {
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(f);
                    shortcut.TargetPath = Process.GetCurrentProcess().MainModule.FileName;
                    shortcut.WorkingDirectory = Environment.CurrentDirectory;
                    shortcut.WindowStyle = 1;
                    shortcut.Description = exeName + "_Ink";
                    shortcut.Save();
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        public static bool StartAutomaticallyDel(string exeName)
        {
            try
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk");
                return true;
            }
            catch (Exception e) { Log.WriteLogToFile(e.ToString(), Log.LogType.Error); }
            return false;
        }

        public static void SwitchLanguage(string languageCode)
        {
            string path = $"Resources/Language/{languageCode}.xaml";
            ResourceDictionary newDict;
            try
            {
                newDict = new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };
            }
            catch (Exception)
            {
                //Log.WriteLogToFile("");
                newDict = new ResourceDictionary { Source = new Uri("Resources/Language/en.xaml", UriKind.Relative) };
            }

            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.OriginalString.Contains("Languages/") == true);

            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }

            Application.Current.Resources.MergedDictionaries.Add(newDict);
        }

        public static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.General)
            {
                ApplySystemTheme(RuntimeData.SettingProperties.Appearance.Theme);
            }
        }
        public static void ApplySystemTheme(bool? b)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var oldTheme = new ResourceDictionary();
                var newTheme = new ResourceDictionary();
                if (b == null | b == true)
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                    newTheme.Source = new Uri("Resources/Styles/Light.xaml", UriKind.Relative);
                    oldTheme.Source = new Uri("Resources/Styles/Dark.xaml", UriKind.Relative);
                }
                else
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                    oldTheme.Source = new Uri("Resources/Styles/Light.xaml", UriKind.Relative);
                    newTheme.Source = new Uri("Resources/Styles/Dark.xaml", UriKind.Relative);
                }
                Application.Current.Resources.MergedDictionaries.Remove(oldTheme);
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            });
        }
    }
}
