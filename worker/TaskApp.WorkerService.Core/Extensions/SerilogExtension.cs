using Serilog;
using Serilog.Events;

namespace TaskApp.WorkerService.Core.Extensions 
{
    public static class SerilogExtension
    {
        public static void AddSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
