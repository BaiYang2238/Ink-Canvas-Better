using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Helpers.Others;
using Ink_Canvas_Better.Windows;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.ViewModel;

namespace Ink_Canvas_Better
{
    public partial class MainWindow : Window
    {
        Point lastInitPoint;
        Point lastEndPoint;
        Point iniPoint;
        bool _isMouseDown = false;
        StrokeCollection lastTempStrokeCollection = new StrokeCollection();

        #region Initialize

        readonly Magnifier MagnifierWindow = new Magnifier();

        /// <summary>
        /// Initialize MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Setting.LoadSettings(isStartup : true);
            RuntimeData.mainWindow = this;

            this.SourceInitialized += Win32Helper.MainWindow_SourceInitialized;
            this.Loaded += DockWindowToBottom;

            CursorIcon_Click(null, null);
        }

        #endregion

        #region Properties

        private bool _mainInkCanvas_Hitable = true;
        public bool MainInkCanvas_Hitable
        {
            get { return _mainInkCanvas_Hitable; }
            set
            {
                _mainInkCanvas_Hitable= value;
                if (_mainInkCanvas_Hitable) MainInkCanvas.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                else MainInkCanvas.Background = Brushes.Transparent;
            }
        }

        private bool _isHideDrawingTools = false;
        public bool IsHideDrawingTools
        {
            get { return _isHideDrawingTools; }
            set
            {
                _isHideDrawingTools = value;
                if (_isHideDrawingTools) HideDrawingTools(); else ShowDrawingTools();
            }
        }

        #endregion

        #region MainInkCanvas

        private void MainInkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RuntimeData.UpdateShapePara_0();
            _isMouseDown = true;
            iniPoint = e.GetPosition(MainInkCanvas);
        }

        private void MainInkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape && _isMouseDown)
            {
                MainInkCanvas_DrawShape(e.GetPosition(MainInkCanvas));
            }
        }

        private void MainInkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // check if need to switch back to last mode
            void func0(ref int currentDrawStep, int maximum) // step count from 0 to maximum
            {
                if (currentDrawStep < maximum)
                {
                    currentDrawStep += 1;
                }
                else
                {
                    func1();
                    currentDrawStep = 0;
                }
            }
            // switch back to last mode
            void func1()
            {
                if (!RuntimeData.IsShapeModePersistent && RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape)
                {
                    RuntimeData.CurrentDrawingMode = RuntimeData.LastDrawingMode;
                }
            }

            lastTempStrokeCollection.Clear();
            lastInitPoint = iniPoint;
            switch (RuntimeData.CurrentShape)
            {
                case "Shape_Hyperbola":
                    func0(ref RuntimeData.CurrentDrawStep, 1);
                    break;
                case "Shape_Cuboid":
                    func0(ref RuntimeData.CurrentDrawStep, 1);
                    break;
                default:
                    func1();
                    break;
            }
            _isMouseDown = false;
        }

        private void MainInkCanvas_TouchDown(object sender, TouchEventArgs e)
        {
            iniPoint = e.GetTouchPoint(MainInkCanvas).Position;
        }

        private void MainInkCanvas_TouchMove(object sender, TouchEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape)
            {
                MainInkCanvas_DrawShape(e.GetTouchPoint(MainInkCanvas).Position);
            }
        }

        private void MainInkCanvas_TouchUp(object sender, TouchEventArgs e)
        {
            if (!RuntimeData.IsShapeModePersistent && RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape)
            {
                RuntimeData.CurrentDrawingMode = RuntimeData.LastDrawingMode;
            }
            _isMouseDown = false;
            lastTempStrokeCollection.Clear();
        }

        #endregion
    }
}
