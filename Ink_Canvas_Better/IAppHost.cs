using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ink_Canvas_Better.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better
{
    internal interface IAppHost
    {
#pragma warning disable CS8618
        public static IHost Host;
#pragma warning restore CS8618

        public static void InitAppHost()
        {
            IAppHost.Host = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder().
                ConfigureServices((context, service) =>
                {

                }).
                ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddFileLogger(configuration =>
                    {
                        configuration.LogDirectoryPath = App.RootPath + $"Logs\\";
                    });
                }).
                Build();
        }

        public static T GetService<T>()
        {
            var s = Host?.Services.GetService(typeof(T));
            if (s != null)
            {
                return (T)s;
            }

            throw new ArgumentException($"Service {typeof(T)} is null!");
        }

        public static T? TryGetService<T>()
        {
            return (T?)Host?.Services.GetService(typeof(T));
        }
    }
}
