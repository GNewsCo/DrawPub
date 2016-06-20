using System;
using DrawPub.ImageService.log;

namespace DrawPub.Core.Log
{
    /// <summary>
    /// ILog factory
    /// </summary>
    public class LogFactory : ILogFactory
    {
        public ILogBase CreateLog(Type type)
        {
            return new Logger(type);
        }
    }
}