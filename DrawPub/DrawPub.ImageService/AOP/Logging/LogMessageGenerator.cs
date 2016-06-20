using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    public class LogMessageGenerator : ILogMessageGenerator
    {
        private readonly IStringSerializer serializer;

        /// <param name="serializer"><see cref="IStringSerializer"/> instance used by <see cref="ILogBase"/> implementation
        /// to serialize objects to the log.</param>
        public LogMessageGenerator(IStringSerializer serializer)
        {
            this.serializer = serializer;
        }

        /// <summary>
        /// Performs the logging of the entry message based on the <see cref="LogAttribute"/>(s) applicable to the 
        /// current executing method.
        /// </summary>
        /// <param name="logMethodArgs">An instance of <see cref="MethodExecutionArgs"/> giving details of the currently 
        /// executing method.</param>
        /// <param name="logAttribute">The instance of <see cref="LogAttribute"/> that is applicable for the current
        /// method being traced.</param>
        /// <param name="methodName">Name of the method being executed.</param>
        /// <param name="serializationDepth">String containing all parameters and arguments concatenated together.</param>
        public string GetEntryMessage(MethodExecutionArgs logMethodArgs, LogAttribute logAttribute, string methodName, int serializationDepth)
        {
            if (!String.IsNullOrEmpty(logAttribute.EntryMessage))
            {
                return logAttribute.EntryMessage;
            }

            var message = String.Format("Entering method '{0}'", methodName);

            string argumentsString = SerializeArguments(logMethodArgs.Method.GetParameters(), logMethodArgs.Arguments, serializationDepth);
            if (!String.IsNullOrEmpty(argumentsString))
            {
                message += String.Format(" with arguments : {0}", argumentsString);
            }

            return message;
        }

        /// <summary>
        /// Performs the logging of the exiting message based on the <see cref="LogAttribute"/>(s) applicable to the 
        /// current executing method.
        /// </summary>
        /// <param name="logAttribute">An instance of <see cref="MethodExecutionArgs"/> giving details of the currently 
        /// executing method.</param>
        /// <param name="methodName">Name of the method being executed.</param>
        /// <param name="logMethodArgs">An instance of <see cref="MethodExecutionArgs"/> giving details of the currently executing method.</param>
        public string GetExitMessage(MethodExecutionArgs logMethodArgs, LogAttribute logAttribute, string methodName)
        {
            if (!String.IsNullOrEmpty(logAttribute.ExitMessage))
            {
                return logAttribute.ExitMessage;
            }

            if (logMethodArgs.Exception == null)
            {
                return String.Format("Exited method '{0}' successfully.", methodName);
            }

            return String.Format("Exited method '{0}' with {1}: {2}", methodName, logMethodArgs.Exception.GetType().FullName, logMethodArgs.Exception.Message);
        }

        /// <summary>
        /// Performs the logging of the message for successful completion of method execution based on the 
        /// <see cref="LogAttribute"/>(s) applicable to the current executing method.
        /// </summary>
        /// <param name="logMethodArgs">An instance of <see cref="MethodExecutionArgs"/> giving details of the currently 
        /// executing method.</param>
        /// <param name="methodName">Name of the method being executed.</param>
        /// <param name="serializationDepth">String containing all parameters and arguments concatenated together.</param>
        public string GetSuccessMessage(MethodExecutionArgs logMethodArgs, string methodName, int serializationDepth)
        {
            var message = String.Format("Method '{0}' was executed successfully.", methodName);

            if (logMethodArgs.ReturnValue != null)
            {
                var returnValueString = serializer.Serialize(logMethodArgs.ReturnValue, serializationDepth);
                message += string.Format("{0}Return value: {1}", Environment.NewLine, returnValueString ?? "<NULL>");
            }

            return message;
        }

        /// <summary>
        /// Retrieves a string containing parameter names and values passed into the current method of execution.
        /// </summary>
        /// <param name="parameters">List of parameters defined for the current method.</param>
        /// <param name="arguments">List of arguments passed in to the execution of current method.</param>
        /// <param name="serializationDepth">How deep you want the argument objects be serialized</param>
        /// <returns>String containing all parameters and arguments concatenated together.</returns>
        public string SerializeArguments(ParameterInfo[] parameters, object[] arguments, int serializationDepth)
        {
            var parameterValueList = new StringBuilder();

            for (var i = 0; i < parameters.Length; i++)
            {
                string value = serializer.Serialize(arguments[i], serializationDepth);
                parameterValueList.AppendFormat("{0}{1}: {2}", Environment.NewLine, parameters[i].Name, value);
            }

            return parameterValueList.ToString();
        }
    }
}