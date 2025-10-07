using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;
using Ink_Canvas_Better.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Ink_Canvas_Better.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IAppHost
    {
        public static string[]? StartArgs = null;
        public static string RootPath = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas Better\\";

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_OnExit);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Log.NewLog(e.Exception.ToString());
            // TODO: show in the messagebox
            // Ink_Canvas.MainWindow.ShowNewMessage($"抱歉，出现预料之外的异常，可能导致 Ink Canvas 画板运行不稳定。\n建议保存墨迹后重启应用。\n报错信息：\n{e.ToString()}", true);
            e.Handled = true;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            StartArgs = e.Args;

            IAppHost.InitAppHost();

            //Log.NewLog(string.Format("Ink-Canvas-Better Starting (Version: {0})", Assembly.GetExecutingAssembly().GetName().Version?.ToString()));


            //Mutex _ = new Mutex(true, "Ink_Canvas_Better", out bool ret);

            //if (!ret && !e.Args.Contains("-m")) // -m multiple
            //{
            //    Log.NewLog("Detected existing instance");
            //    MessageBox.Show("已有一个程序实例正在运行");
            //    Log.NewLog("Ink-Canvas-Batter automatically closed");
            //    Environment.Exit(0);
            //}

            ILogger _logger = IAppHost.Host.Services.GetRequiredService<ILogger<App>>();

            _logger.Log(LogLevel.Information, "Ink Canvas Better is running");

        }

        void App_OnExit(object sender, ExitEventArgs e)
        {
            ILogger _logger = IAppHost.Host.Services.GetRequiredService<ILogger<App>>();

            _logger.Log(LogLevel.Information, "Ink Canvas Better exited");
        }
    }
}
