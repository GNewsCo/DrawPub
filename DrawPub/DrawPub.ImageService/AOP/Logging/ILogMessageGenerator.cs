using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    /// <summary>
    /// Generates the log message that would be logged based on the advice type along with any input or output parameters.
    /// </summary>
    public interface ILogMessageGenerator
    {
        /// <summary>
        /// Performs the logging of the entry message based on the <see cref="LogAttribute"/>(s) applicable to the 
        /// current executing method.
        /// </summary>
        /// <param name="logMethodArgs">An instance of <see cref="MethodExecutionArgs"/> giving details of the currently 
        /// executing method.</param>
        /// <param name="logAttributes">The list of <see cref="LogAttribute"/>(s) applicable to the currently 
        /// executing method.</param>
        /// <param name="methodName">Name of the method being executed.</param>
        /// <param name="serializationDepth">String containing all parameters and arguments concatenated together.</param>
        string GetEntryMessage(MethodExecutionArgs logMethodArgs, LogAttribute logAttributes, string methodName, int serializationDepth);

        /// <summary>
        /// Performs the logging of the exiting message based on the <see cref="LogAttribute"/>(s) applicable to the 
        /// current executing method.
        /// </summary>
        /// <param name="logAttributes">The list of <see cref="LogAttribute"/>(s) applicable to the currently 
        /// executing method.</param>
        /// <param name="methodName">Name of the method being executed.</param>
        /// /// <param name="logMethodArgs">An instance of <see cref="MethodExecutionArgs"/> giving details of the currently 
        /// executing method.</param>
        string GetExitMessage(MethodExecutionArgs logMethodArgs, LogAttribute logAttributes, string methodName);

        /// <summary>
        /// Performs the logging of the message for successful completion of method execution based on the 
        /// <see cref="LogAttribute"/>(s) applicable to the current executing method.
        /// </summary>
        /// <param name="logMethodArgs">An instance of <see cref="MethodExecutionArgs"/> giving details of the currently 
        /// executing method.</param>
        /// <param name="methodName">Name of the method being executed.</param>
        /// <param name="serializationDepth">String containing all parameters and arguments concatenated together.</param>
        string GetSuccessMessage(MethodExecutionArgs logMethodArgs, string methodName, int serializationDepth);

        /// <summary>
        /// Retrieves a string containing parameter names and values passed into the current method of execution.
        /// </summary>
        /// <param name="parameters">List of parameters defined for the current method.</param>
        /// <param name="arguments">List of arguments passed in to the execution of current method.</param>
        /// <param name="serializationDepth">How deep you want the argument objects be serialized</param>
        /// <returns>String containing all parameters and arguments concatenated together.</returns>
        string SerializeArguments(ParameterInfo[] parameters, object[] arguments, int serializationDepth);
    }
}