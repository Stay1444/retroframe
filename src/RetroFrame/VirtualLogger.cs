using Serilog.Events;

namespace RetroFrame;

public class VirtualLogger
{
    internal record VirtualLog(string Message, LogEventLevel LogLevel, DateTimeOffset DateTimeOffset);

    internal List<VirtualLog> Logs { get; } = new List<VirtualLog>();

    private void Log(string message, LogEventLevel level, DateTimeOffset offset)
    {
        Logs.Insert(0, new VirtualLog(message, level, offset));
    }
    
    public void Info(string message)
    {
        Log(message, LogEventLevel.Information, DateTimeOffset.Now);
    }

    public void Error(string message)
    {
        Log(message, LogEventLevel.Error, DateTimeOffset.Now);
    }

    public void Warn(string message)
    {
        Log(message, LogEventLevel.Warning, DateTimeOffset.Now);
    }
}