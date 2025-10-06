using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using Ink_Canvas_Better.Resources;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// # TODO: Unsolved Issues.
    /// <para>The commented-out code below contains unresolved issues.
    /// BaiYang2238 currently lacks the capability to resolve them,
    /// and this code may be deprecated. Assistance is welcome if
    /// anyone has the expertise to address these problems.</para>
    /// 
    /// <para>Partial issues (all occurring under InkStyle.Simulative mode, mostly related to eraseByPoint):</para>
    /// <list type="number">
    ///     <item>Lag (not extremely pronounced, but noticeable with long strokes or excessive stroke counts)</item>
    ///     <item>Significant lag and abnormal ink behavior during erasure in eraseByPoint mode</item>
    ///     <item>Abnormal dynamic rendering of ink (refer to [refactor] commits part 30 ~ 33 in Git
    ///           history – these versions don't exhibit this issue, but their dynamic rendering
    ///           algorithm differs from the actual ink algorithm)</item>
    ///     <item>Switching simulated pen pressure modes after drawing a stroke causes abnormal
    ///           ink behavior during eraseByPoint operations</item>
    /// </list>
    /// 
    /// <para>Important note: In eraseByPoint mode, inkCanvas's default erase logic is:
    ///     Delete original stroke → Create two new strokes (calling DrawCore in the Stroke class).</para>
    /// <para>Therefore, the switch code block in my implementation causes the issue 4 above.</para>
    /// </summary>
    class CustomInkCanvas : InkCanvas
    {
        //private readonly CustomDynamicRenderer _customRenderer = new CustomDynamicRenderer();
        //public CustomStroke _customStroke;

        //public CustomInkCanvas() : base()
        //{
        //    DefaultDrawingAttributes = new DrawingAttributes
        //    {
        //        Color = Colors.Red,
        //        FitToCurve = true,
        //        Height = 3,
        //        Width = 3,
        //        StylusTip = StylusTip.Ellipse
        //    };
        //}

        //protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        //{
        //    this.Strokes.Remove(e.Stroke);
        //    this.Strokes.Add(_customStroke = new CustomStroke(e.Stroke.StylusPoints, RuntimeData.DrawingAttributes));
        //    DynamicRenderer = _customRenderer;
        //}
    }

    //class CustomStroke : Stroke
    //{
    //    public CustomStroke(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes) : base(stylusPoints)
    //    {
    //        this.DrawingAttributes = drawingAttributes.Clone();
    //    }

    //    protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
    //    {
    //        switch (RuntimeData.settingData.Runtime.InkStyle)
    //        {
    //            case InkStyle.Default:
    //                base.DrawCore(drawingContext, drawingAttributes);
    //                break;
    //            case InkStyle.Simulative:
    //                DrawSimulativeStroke(drawingContext);
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException(nameof(RuntimeData.settingData.Runtime.InkStyle), "Unsupported ink style.");
    //        }
    //    }

    //    private void DrawSimulativeStroke(DrawingContext drawingContext)
    //    {
    //        if (this.StylusPoints?.Count < 1)
    //            return;

    //        var bluntnessFactor = RuntimeData.settingData.Runtime.BluntnessFactor;
    //        var w1 = this.DrawingAttributes.Width;
    //        var prevPoint = new Point(double.NaN, double.NaN);
    //        var prevWidth = w1;

    //        for (int i = 0; i < StylusPoints.Count; i++)
    //        {
    //            var newPoint = (Point)this.StylusPoints[i];

    //            if (double.IsNaN(prevPoint.X) || double.IsNaN(prevPoint.Y))
    //            {
    //                drawingContext.DrawEllipse(new SolidColorBrush(DrawingAttributes.Color), null, newPoint, w1 / 2, w1 / 2);
    //                prevPoint = newPoint;
    //                prevWidth = w1;
    //                continue;
    //            }

    //            var vector = prevPoint - newPoint;
    //            var newWidth = DrawingAttributes.Width;
    //            if (vector.Length > 1)
    //            {
    //                newWidth = w1 * Math.Pow(bluntnessFactor, vector.Length);
    //            }
    //            var dx = (newPoint.X - prevPoint.X) / vector.Length;
    //            var dy = (newPoint.Y - prevPoint.Y) / vector.Length;
    //            var dw = (newWidth - prevWidth) / vector.Length;

    //            for (int j = 0; j < vector.Length; j++)
    //            {
    //                var currentPoint = new Point(prevPoint.X + dx * j, prevPoint.Y + dy * j);
    //                var currentWidth = prevWidth + dw * j;
    //                drawingContext.DrawEllipse(new SolidColorBrush(DrawingAttributes.Color), null, currentPoint, currentWidth / 2, currentWidth / 2);
    //            }

    //            prevPoint = newPoint;
    //            prevWidth = newWidth;
    //        }
    //    }
    //}

    //class CustomDynamicRenderer : DynamicRenderer
    //{
    //    protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
    //    {
    //        switch (RuntimeData.settingData.Runtime.InkStyle)
    //        {
    //            case InkStyle.Default:
    //                base.OnDraw(drawingContext, stylusPoints, geometry, fillBrush);
    //                break;
    //            case InkStyle.Simulative:
    //                DrawSimulativeStroke(drawingContext, stylusPoints);
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException(nameof(RuntimeData.settingData.Runtime.InkStyle), "Unsupported ink style.");
    //        }
    //    }

    //    private void DrawSimulativeStroke(DrawingContext drawingContext, StylusPointCollection stylusPoints)
    //    {
    //        var _stylusPoints = stylusPoints ?? RuntimeData.mainWindow.inkCanvas._customStroke.StylusPoints;
    //        if (_stylusPoints?.Count < 1)
    //            return;

    //        var bluntnessFactor = RuntimeData.settingData.Runtime.BluntnessFactor;
    //        var w1 = this.DrawingAttributes.Width;
    //        var prevPoint = new Point(double.NaN, double.NaN);
    //        var prevWidth = w1;

    //        for (int i = 0; i < _stylusPoints.Count; i++)
    //        {
    //            var newPoint = (Point)_stylusPoints[i];

    //            if (double.IsNaN(prevPoint.X) || double.IsNaN(prevPoint.Y))
    //            {
    //                drawingContext.DrawEllipse(new SolidColorBrush(DrawingAttributes.Color), null, newPoint, w1 / 2, w1 / 2);
    //                prevPoint = newPoint;
    //                prevWidth = w1;
    //                continue;
    //            }

    //            var vector = prevPoint - newPoint;
    //            var newWidth = DrawingAttributes.Width;
    //            if (vector.Length > 1)
    //            {
    //                newWidth = w1 * Math.Pow(bluntnessFactor, vector.Length);
    //            }
    //            var dx = (newPoint.X - prevPoint.X) / vector.Length;
    //            var dy = (newPoint.Y - prevPoint.Y) / vector.Length;
    //            var dw = (newWidth - prevWidth) / vector.Length;

    //            for (int j = 0; j < vector.Length; j++)
    //            {
    //                var currentPoint = new Point(prevPoint.X + dx * j, prevPoint.Y + dy * j);
    //                var currentWidth = prevWidth + dw * j;
    //                drawingContext.DrawEllipse(new SolidColorBrush(DrawingAttributes.Color), null, currentPoint, currentWidth / 2, currentWidth / 2);
    //            }

    //            prevPoint = newPoint;
    //            prevWidth = newWidth;
    //        }
    //    }
    //}
}