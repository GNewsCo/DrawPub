using System;
using System.IO;
using DrawPub.Core.Log;
using log4net;
using log4net.Config;


namespace DrawPub.ImageService.log
{
    public class Logger : ILogBase
    {
        private ILog log;

        string log4NetFileName = "log4net.xml";

        public Logger(Type type)
        {
            log = log4net.LogManager.GetLogger(type);

            configure();
        }

        private void configure()
        {

            string log4NetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, log4NetFileName);

            FileInfo configFile = new FileInfo(log4NetPath);

            XmlConfigurator.ConfigureAndWatch(configFile);
        }

        public void Debug(object message)
        {
            log.Debug(message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            log.Info(message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            log.Warn(message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            log.Error(message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

       
    }
}