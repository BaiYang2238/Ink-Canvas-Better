using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Windows.FloatingBarIcons;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ink_Canvas_Better.ViewModel
{
    static class RuntimeData
    {
        #region instance

        public static SettingWindow settingWindow;
        public static MainWindow mainWindow;
        public static FloatingBar_Pen floatingBar_Pen;
        public static FloatingBar_Highlighter floatingBar_Highlighter;
        public static ICB_ColorPicker colorPicker;

        #endregion

        #region Shape parameters

        public static string CurrentShape { get; set; } = "Shape_Line";

        public static bool IsShapeModePersistent { get; set; } = false;

        public static int CurrentDrawStep = 0; // Shape

        // special parameters for shapes
        private static double Shape_Para_0;
        public static double GetShapePara_0() { return Shape_Para_0; }
        public static void UpdateShapePara_0() { Shape_Para_0 = Math.Round(Math.Log(mainWindow.MainInkCanvas.DefaultDrawingAttributes.Width + Math.E), 3); }

        public static double Shape_Para_1;

        #endregion

        public static bool isCloseFromButton = false;
        public static string settingsFileName = "settings.json";
        public static Metadata currentMetadata = new Metadata();

        public static DrawingAttributes CurrentDrawingAttributes_Pen { get; set; } = new DrawingAttributes()
        {
            Color = Color.FromRgb(255,0,0)
        };
        public static DrawingAttributes CurrentDrawingAttributes_Highlighter { get; set; } = new DrawingAttributes {
            Color = Color.FromRgb(255,255,0),
            StylusTip = StylusTip.Rectangle,
            Width = 1
        };

        public static DrawingMode LastDrawingMode {  get; set; } = DrawingMode.None;

        private static DrawingMode _currentDrawingMode = DrawingMode.None;
        public static DrawingMode CurrentDrawingMode
        {
            get { return _currentDrawingMode; }
            set
            {
                // Check
                if (_currentDrawingMode != DrawingMode.Shape)
                {
                    mainWindow.CursorIcon.IsStatusEnable = false;
                    mainWindow.PenIcon.IsStatusEnable = false;
                    mainWindow.HighlighterIcon.IsStatusEnable = false;
                    mainWindow.EraserIcon.IsStatusEnable = false;
                    mainWindow.PickIcon.IsStatusEnable = false;
                    LastDrawingMode = _currentDrawingMode;
                }
                else { mainWindow.ShapeIcon.IsStatusEnable = false; }
                // Update _currentDrawingMode
                _currentDrawingMode = value;
                switch (_currentDrawingMode)
                {
                    case DrawingMode.None:
                        break;
                    case DrawingMode.Cursor:
                        mainWindow.MainInkCanvas_Hitable = false;
                        mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.None;
                        mainWindow.CursorIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Pen:
                        mainWindow.MainInkCanvas_Hitable = true;
                        mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                        mainWindow.MainInkCanvas.DefaultDrawingAttributes = CurrentDrawingAttributes_Pen;
                        mainWindow.PenIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Highlighter:
                        mainWindow.MainInkCanvas_Hitable = true;
                        mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                        mainWindow.MainInkCanvas.DefaultDrawingAttributes = CurrentDrawingAttributes_Highlighter;
                        mainWindow.HighlighterIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Eraser:
                        mainWindow.MainInkCanvas_Hitable = true;
                        if (CurrentEraserMode == EraserMode.Stroke) mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke; else mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                        mainWindow.MainInkCanvas.EraserShape = CurrentEraserShape;
                        mainWindow.EraserIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Pick:
                        mainWindow.MainInkCanvas_Hitable = true;
                        mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                        mainWindow.PickIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Shape:
                        mainWindow.MainInkCanvas_Hitable = true;
                        mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.None;
                        mainWindow.ShapeIcon.IsStatusEnable = true;
                        break;
                    default:
                        var _ = _currentDrawingMode;
                        _currentDrawingMode = DrawingMode.None;
                        throw new NotImplementedException($"Unimplemented drawing mode: {_}");
                }
            }
        }

        private static void DefaultDrawingAttributes_PropertyDataChanged(object sender, PropertyDataChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static EraserMode CurrentEraserMode { get; set; } = EraserMode.Point;

        public static StylusShape CurrentEraserShape { get; set; } = new EllipseStylusShape(30, 30);

        public static void ApplyMetadata()
        {
            // TODO
        }

        public enum DrawingMode
        {
            None,
            Cursor,
            Pen,
            Highlighter,
            Eraser,
            Pick, // Select
            Shape
        }

        public enum EraserMode
        {
            Stroke,
            Point
        }

    }

    public class Metadata
    {
        [JsonProperty("metadataVer")]
        public string MetadataVer { get; set; } = "1.0";

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
