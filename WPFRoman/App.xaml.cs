
using System.Windows;
using WPFRoman.Log;

namespace WPFRoman 
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Logger Logger { get; set; } = Logger.Log;

        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Information("        =============  Started Logging  =============        ");
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logger.Information("        =============  App Exit  =============        ");
            base.OnExit(e);
        }
    }
}
