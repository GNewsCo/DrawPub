using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    public class LoggingHandler : ILoggingHandler
    {
        private readonly ILogFactory logFactory;
        private readonly ILogMessageGenerator messageGenerator;
        private readonly ILogAttributeFinder logAttributeFinder;

        public LoggingHandler(ILogFactory logFactory, ILogMessageGenerator messageGenerator, ILogAttributeFinder logAttributeFinder)
        {
            this.logFactory = logFactory;
            this.messageGenerator = messageGenerator;
            this.logAttributeFinder = logAttributeFinder;
        }

        /// <summary>
        /// Logs the message specific to the log message type requested to be logged.
        /// </summary>
        /// <param name="args">An instance of <see cref="MethodExecutionArgs"/> giving information specific for the 
        /// method being traced.</param>
        /// <param name="adviceType">An instance of <see cref="AdviceType"/> giving the type of tracing 
        ///     requested.</param>
        public void LogMessage(MethodExecutionArgs args, AdviceType adviceType)
        {
            var logger = logFactory.CreateLogger(args.Method.DeclaringType);

            // If logger is not enabled at either debug, info or warn, tracing is not required via log aspect.
            if (!logger.IsDebugEnabled && !logger.IsInfoEnabled && !logger.IsWarnEnabled)
                return;

            // Capture the lowest enabled level in logger.
            var loggerLogLevel = logger.IsDebugEnabled ? LogLevel.Debug :
                                    logger.IsInfoEnabled ? LogLevel.Info : LogLevel.Warn;

            LogLevel defaultLogLevel;

            // Retrieve the attribute that is most applicable for the current executing method.
            var currentAttribute = logAttributeFinder.GetApplicableAttribute(args.Method.DeclaringType, args.Method,
                                                                             loggerLogLevel,
                                                                             out defaultLogLevel);

            if (currentAttribute == null)
                return;

            // If the applicable attribute is an "IgnoreLog" attribute, skip tracing the method.
            if ((currentAttribute as IgnoreLogAttribute) != null)
                return;

            var logAttribute = currentAttribute as LogAttribute;

            if (logAttribute == null)
                return;

            // Retrieve the log level at which the current method should be traced.
            LogLevel logLevel = logAttribute.LogLevel == LogLevel.Default ? defaultLogLevel : logAttribute.LogLevel;

            if (!NeedToLog(logLevel, logger))
                return;

            var message = CreateLogMessage(args, logAttribute, adviceType);

            LogMessage(message, logLevel, logger);
        }

        /// <summary>
        /// Checks if the log level specified/derived for the current log attribute qualifies for logging under the 
        /// log level allowed in the current logger. If not, not required to proceed with logging.
        /// </summary>
        private bool NeedToLog(LogLevel logLevel, ILogBase logger)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    return logger.IsInfoEnabled;
                case LogLevel.Warn:
                    return logger.IsWarnEnabled;
                case LogLevel.Debug:
                    return logger.IsDebugEnabled;
                default:
                    throw new NotSupportedException(logLevel + " is not supported.");
            }
        }

        /// <summary>
        /// Retrieves/generates the log message that would be logged from the current log attribute, method execution
        /// args and the advice type of the current execution.
        /// </summary>
        /// <returns>The message that would be logged.</returns>
        private string CreateLogMessage(MethodExecutionArgs args, LogAttribute logAttribute, AdviceType adviceType)
        {
            var methodName = args.Method.Name;

            string message;

            switch (adviceType)
            {
                case AdviceType.Entry:
                    message = messageGenerator.GetEntryMessage(args, logAttribute, methodName, logAttribute.LogSerializationDepth);
                    break;
                case AdviceType.Exit:
                    message = messageGenerator.GetExitMessage(args, logAttribute, methodName);
                    break;
                case AdviceType.Success:
                    message = messageGenerator.GetSuccessMessage(args, methodName, logAttribute.LogSerializationDepth);
                    break;
                default:
                    throw new NotSupportedException(adviceType.ToString());
            }

            return message;
        }

        /// <summary>
        /// Performs the actual logging with the logger in use and logs the message specified at the specified log level.
        /// </summary>
        private void LogMessage(string message, LogLevel logLevel, ILogBase logger)
        {
            if (logLevel == LogLevel.Info)
            {
                logger.Info(message);
            }
            else if (logLevel == LogLevel.Warn)
            {
                logger.Warn(message);
            }
            else
            {
                logger.Debug(message);
            }
        }
    }
}