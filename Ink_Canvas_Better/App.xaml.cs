using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Ink_Canvas_Better
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IAppHost
    {
        private static string[]? StartArgs = null;
        public readonly static string RootPath = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas Better\\";

        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_OnExit);
            Debug.WriteLine("hit - 1");
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            StartArgs = e.Args;

            Debug.WriteLine("hit - 1");

            #region log
            IAppHost.InitAppHost();
            ILogger _logger = IAppHost.Host.Services.GetRequiredService<ILogger<App>>();
            Debug.WriteLine("hit - 2");
            this.DispatcherUnhandledException += (sender, e) =>
            {
                _logger.LogCritical(e.ToString());
                // TODO: show in the messagebox
                // Ink_Canvas.MainWindow.ShowNewMessage($"抱歉，出现预料之外的异常，可能导致 Ink Canvas 画板运行不稳定。\n建议保存墨迹后重启应用。\n报错信息：\n{e.ToString()}", true);
                e.Handled = true;
            };
            Debug.WriteLine("hit - 3");
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                _logger.LogWarning(e.ToString());
            };
            Debug.WriteLine("hit - 4");
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                _logger.LogError(e.ToString());
                e.SetObserved();
            };
            #endregion

            //Log.NewLog(string.Format("Ink-Canvas-Better Starting (Version: {0})", Assembly.GetExecutingAssembly().GetName().Version?.ToString()));


            //Mutex _ = new Mutex(true, "Ink_Canvas_Better", out bool ret);

            //if (!ret && !e.Args.Contains("-m")) // -m multiple
            //{
            //    Log.NewLog("Detected existing instance");
            //    MessageBox.Show("已有一个程序实例正在运行");
            //    Log.NewLog("Ink-Canvas-Batter automatically closed");
            //    Environment.Exit(0);
            //}
            Debug.WriteLine("hit - 5");
            _logger.LogInformation("===== Ink Canvas Better is running =====");

        }

        void App_OnExit(object sender, ExitEventArgs e)
        {
            ILogger _logger = IAppHost.Host.Services.GetRequiredService<ILogger<App>>();
            _logger.LogInformation("===== Ink Canvas Better exited =====");
        }
    }
}
