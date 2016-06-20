using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    /// <summary>
    /// Provides functionality to perform logging of the method traces based on applicable attributes.
    /// </summary>
    public interface ILoggingHandler
    {
        /// <summary>
        /// Handles the actual logging for the executing method based on applicable attribute found.
        /// </summary>
        /// <param name="args">The <see cref="MethodExecutionArgs"/> passed in from the aspect interceptor.</param>
        /// <param name="adviceType">The <see cref="AdviceType"/> specifying if it is Entry, Exit or Success currently
        /// been traced.</param>
        void LogMessage(MethodExecutionArgs args, AdviceType adviceType);
    }
}