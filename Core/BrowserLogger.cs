using System.Threading.Tasks;


namespace Core
{
    class BrowserLogger : ILogger
    {
        readonly IInteropService _interop;
        
        public BrowserLogger(IInteropService interop)
        {
            _interop = interop;
        }

        public Task Info(string value)
        {
            return _interop.ConsoleLog(value);
        }
    }
}