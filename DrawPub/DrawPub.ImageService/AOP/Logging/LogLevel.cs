using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    public enum LogLevel
    {
        /// <summary>
        /// The default level is Debug when decorating the methods and Info on class or assembly level.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Logs at the Debug level.
        /// </summary>
        Debug,

        /// <summary>
        /// Logs at the Info level.
        /// </summary>
        Info,

        /// <summary>
        /// Logs at the Warn level.
        /// </summary>
        Warn
    }
}