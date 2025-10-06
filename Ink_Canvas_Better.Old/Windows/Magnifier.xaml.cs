
using System;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Ink_Canvas_Better.Windows
{
    /// <summary>
    /// Interaction logic for Magnifier.xaml
    /// </summary>
    public partial class Magnifier : Window
    {
        DispatcherTimer dispatcherTimer;
        private bool MagnifyTheRightScreem = false;
        public Magnifier()
        {
            InitializeComponent();
        }

        public void MagnifierRunning()
        {
            // 启动定时器，截屏
            dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(33), };
            double leftTopPointWidth = MagnifyTheRightScreem ? Width : -Width;
            double leftTopPointHeight = MagnifyTheRightScreem ? 0 : 0;
            double rightBottomPointWidth = MagnifyTheRightScreem ? Width * 2 : 0;
            double rightBottomPointHeight = MagnifyTheRightScreem ? Height : Height;
            dispatcherTimer.Tick += (s, e) =>
            {
                // gdi+截屏，,使用PointToScreen消除dpi影响
                var leftTop = PointToScreen(new Point(leftTopPointWidth, leftTopPointHeight));
                var rightBottom = PointToScreen(new Point(rightBottomPointWidth, rightBottomPointHeight));
                var bm = Snapshot((int)leftTop.X, (int)leftTop.Y, (int)(rightBottom.X - leftTop.X), (int)(rightBottom.Y - leftTop.Y));
                var wb = BitmapToWriteableBitmap(bm);
                // 显示到界面
                ib.ImageSource = wb;
            };
            dispatcherTimer.Start();
        }

        public void MagnifyCompleted()
        {
            dispatcherTimer.Stop();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// 截取一帧图片
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>截屏后的位图对象，需要调用Dispose手动释放资源。</returns>
        public static System.Drawing.Bitmap Snapshot(int x, int y, int width, int height)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height), System.Drawing.CopyPixelOperation.SourceCopy);
            }
            return bitmap;
        }

        // 将Bitmap 转换成WriteableBitmap 
        public static WriteableBitmap BitmapToWriteableBitmap(System.Drawing.Bitmap src)
        {
            var wb = CreateCompatibleWriteableBitmap(src);
            System.Drawing.Imaging.PixelFormat format = src.PixelFormat;
            if (wb == null)
            {
                wb = new WriteableBitmap(src.Width * 2, src.Height, 0, 0, PixelFormats.Bgra32, null);
                format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            }
            BitmapCopyToWriteableBitmap(src, wb, new System.Drawing.Rectangle(0, 0, src.Width, src.Height), 0, 0, format);
            return wb;
        }

        // 创建尺寸和格式与Bitmap兼容的WriteableBitmap
        public static WriteableBitmap CreateCompatibleWriteableBitmap(System.Drawing.Bitmap src)
        {
            PixelFormat format;
            switch (src.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                    format = PixelFormats.Bgr555;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                    format = PixelFormats.Bgr565;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    format = PixelFormats.Bgr24;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    format = PixelFormats.Bgr32;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                    format = PixelFormats.Pbgra32;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    format = PixelFormats.Bgra32;
                    break;
                default:
                    return null;
            }
            return new WriteableBitmap(src.Width, src.Height, 0, 0, format, null);
        }

        // 将Bitmap数据写入WriteableBitmap中
        public static void BitmapCopyToWriteableBitmap(System.Drawing.Bitmap src, WriteableBitmap dst, System.Drawing.Rectangle srcRect, int destinationX, int destinationY, System.Drawing.Imaging.PixelFormat srcPixelFormat)
        {
            var data = src.LockBits(new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), src.Size), System.Drawing.Imaging.ImageLockMode.ReadOnly, srcPixelFormat);
            dst.WritePixels(new Int32Rect(srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height), data.Scan0, data.Height * data.Stride, data.Stride, destinationX, destinationY);
            src.UnlockBits(data);
        }

        public void HideMagnify_Click(object sender, MouseButtonEventArgs e)
        {
            MagnifyCompleted();
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnSwitchMagnifyScream_Click(object sender, RoutedEventArgs e)
        {
            MagnifyTheRightScreem = !MagnifyTheRightScreem;
            if (MagnifyTheRightScreem == true)
            {
                MagnifyCompleted();
                MagnifierRunning();
                LabelSwitchMagnifyScream.Content = "点击放大左侧";
            }
            else
            {
                MagnifyCompleted();
                MagnifierRunning();
                LabelSwitchMagnifyScream.Content = "点击放大右侧";
            }
        }
    }
}