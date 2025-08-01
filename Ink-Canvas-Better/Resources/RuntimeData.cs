﻿using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Windows.FloatingBarIcons;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace Ink_Canvas_Better.Resources
{
    static class RuntimeData
    {
        public static bool isCloseFromButton = false;
        public static SettingWindow settingWindow;
        public static MainWindow mainWindow;
        public static FloatingBar_Pen floatingBar_Pen;
        public static String settingsFileName = "settings.json";
        public static SettingData settingData = new SettingData();
        public static Metadata currentMetadata = new Metadata();

        public static DrawingAttributes DrawingAttributes { get; set; } = new DrawingAttributes();

        private static DrawingMode _currentDrawingMode = DrawingMode.None;
        public static DrawingMode currentDrawingMode
        {
            get { return _currentDrawingMode; }
            set
            {
                _currentDrawingMode = value;
                mainWindow.CursorIcon.IsStatusEnable = false;
                mainWindow.PenIcon.IsStatusEnable = false;
                mainWindow.HighlighterIcon.IsStatusEnable = false;
                mainWindow.EraserIcon.IsStatusEnable = false;
                mainWindow.PickIcon.IsStatusEnable = false;
                switch (_currentDrawingMode)
                {
                    case DrawingMode.None:
                        break;
                    case DrawingMode.Cursor:
                        mainWindow.CursorIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Pen:
                        mainWindow.PenIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Highlighter:
                        mainWindow.HighlighterIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.EraseByPoint:
                    case DrawingMode.EraseByStroke:
                        mainWindow.EraserIcon.IsStatusEnable = true;
                        break;
                    default:
                        _currentDrawingMode = DrawingMode.None;
                        throw new NotImplementedException();
                }
            }
        }

        public enum DrawingMode
        {
            None,
            Cursor,
            Pen,
            Highlighter,
            EraseByPoint,
            EraseByStroke
        }

        public static void ApplyMetadata()
        {
            // TODO
        }
    }

    public class Metadata
    {
        [JsonProperty("metadataVer")]
        public String MetadataVer { get; set; } = "1.0";

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("icbMode")]
        public ICBMode CurrentICBMode { get; set; } = ICBMode.None;



        [JsonProperty("metadata_PPT")]
        public Metadata_PPT ExInfo_PPT { get; set; } = new Metadata_PPT();

        [JsonProperty("metadata_whiteBoard")]
        public Metadata_whiteBoard ExInfo_whiteBoard { get; set; } = new Metadata_whiteBoard();
        
        public enum ICBMode
        {
            None,
            PPT,
            WhiteBoard
        }
    }

    public class Metadata_PPT
    {
        [JsonProperty("slideCount")]
        public int CurrentSlideIndex { get; set; } = 0;
    }

    public class Metadata_whiteBoard
    {
        [JsonProperty("slideCount")]
        public int CurrentSlideIndex { get; set; } = 0;
    }
}
