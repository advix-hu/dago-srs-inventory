using SRSInventory.Interfaces;

namespace SRSInventory.Classes
{
    public class Logger : ILogger
    {
        public void Log(LogLevel level, string message, Exception exception = null)
        {
            //TODO: use NLOG instead! ;)
        }
    }
}
