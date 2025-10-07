using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public sealed class FileLogger(
    Func<FileLoggerConfiguration> getCurrentConfig) : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        FileLoggerConfiguration config = getCurrentConfig();
        if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            if (config.LogDirectoryPath == null)
            {
                config.LogDirectoryPath = $"./Logs/";
            }
            if (!Directory.Exists(config.LogDirectoryPath))
            {
                Directory.CreateDirectory(config.LogDirectoryPath);
            }
            StreamWriter sw = new(config.LogDirectoryPath + $"{DateTime.Now:yyyy_MM_dd}.log", true);
            sw.WriteLine(string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logLevel, state, exception));
            sw.Close();
        }
    }
}
