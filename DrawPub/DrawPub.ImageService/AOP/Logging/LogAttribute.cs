using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    /// <summary>
    /// Attribute which would be used to decorate the assemblies/classes/methods with for the tracing functionality
    /// to be enabled for them.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LogAttribute : AspectAttribute
    {
        /// <summary>
        /// Create an instance of the LogAttribute.
        /// </summary>
        public LogAttribute(LogLevel level = LogLevel.Default, string entryMessage = "", string exitMessage = "", int serializationDepth = SerializationDepth.Default)
        {
            this.LogLevel = level;
            this.EntryMessage = entryMessage;
            this.ExitMessage = exitMessage;
            this.LogSerializationDepth = serializationDepth;
        }

        /// <summary>
        /// Create an instance of the LogAttribute.
        /// </summary>
        public LogAttribute()
        {
            this.LogSerializationDepth = SerializationDepth.Default;
        }

        /// <summary>
        /// Gets/sets the custom message which would be logged along with the generic entering and exiting 
        /// messages when tracing methods.
        /// </summary>
        public string EntryMessage { get; set; }

        /// <summary>
        /// Gets/sets the custom message which would be logged in place of the generic exiting messages when tracing 
        /// methods.
        /// </summary>
        public string ExitMessage { get; set; }

        /// <summary>
        /// Gets/sets the level at which the current tracing information should be logged at by the log aspect.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets/sets the depth of the serialization when objects are serialized for logging. By default the first level will be serialized.
        /// Use <see cref="SerializationDepth"/> class for more options./>
        /// </summary>
        public int LogSerializationDepth { get; set; }
    }
}