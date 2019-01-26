using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRoman.LogerImplementation
{
    public class Logger
    {
        const string Version = "0.0.1";
        private static Serilog.Core.Logger loger;

        private static readonly Lazy<Logger> lazy =
            new Lazy<Logger>(() => new Logger());

        public static Logger Log { get => lazy.Value; }

        static Logger()
        {
            loger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithProperty(nameof(Version), Version)
                .WriteTo.File(new CompactJsonFormatter(), "logs/log-.json", rollingInterval: RollingInterval.Minute)
                .WriteTo.ColoredConsole(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .CreateLogger();
        }

        private Logger()
        {
        }

        public void Warning(string mss)
        {
            loger.Warning(mss);
            RaiseEvent(nameof(Logger), mss);
        }

        public void Debug(string mss)
        {
            loger.Debug(mss);
            RaiseEvent(nameof(Logger), mss);
        }

        public void Information(string mss)
        {
            loger.Information(mss);
            RaiseEvent(nameof(Logger), mss);
        }

        public void Erorr(string mss)
        {
            loger.Error(mss);
            RaiseEvent(nameof(Logger), mss);
        }

        public event EventHandler<string> SubLog;
        public void RaiseEvent(object o, string inf)
        {
            SubLog?.Invoke(o, inf);
        }

        ~Logger()
        {
        }
    }
}
