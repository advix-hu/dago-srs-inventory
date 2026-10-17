namespace SRSInventory.Interfaces
{
    public enum LogLevel
    {
        Trace,
        Info,
        Warning,
        Error
    }

    public interface ILogger
    {
        void Log(LogLevel level, string message, Exception exception = null);
    }
}
