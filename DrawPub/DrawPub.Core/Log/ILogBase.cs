namespace DrawPub.Core.Log
{
    public interface ILogBase
    {
        void Debug(object message);
        void DebugFormat(string format, params object[] args);

        void Info(object message);
        void InfoFormat(string format, params object[] args);

        void Warn(object message);
        void WarnFormat(string format, params object[] args);

        void Error(object message);
        void ErrorFormat(string format, params object[] args);

        
    }
}
