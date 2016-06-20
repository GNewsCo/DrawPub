using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP
{
    public enum FlowBehavior
    {
        /// <summary>
        /// Default flow behaviour for the current method. For OnEntry(MethodExecutionArgs), OnExit(MethodExecutionArgs) and OnSuccess(MethodExecutionArgs), the default flow is Continue, for OnException(MethodExecutionArgs) it is RethrowException.
        /// </summary>
        Default,

        /// <summary>
        /// Continue normally. In an OnException(MethodExecutionArgs) advice, the Continue behaviour does not rethrow the exception, but continues the normal execution flow after the block protected by the advise.
        /// </summary>
        Continue,

        /// <summary>
        /// The current exception will be rethrown. Available only for OnException(MethodExecutionArgs).
        /// </summary>
        RethrowException,

        /// <summary>
        ///Return immediately from the current method. Available only for OnEntry(MethodExecutionArgs) and OnException(MethodExecutionArgs). Note that you may want to set the ReturnValue property, otherwise you may get a NullReferenceException.
        /// </summary>
        Return,

        /// <summary>
        /// Throws the exception contained in the Exception property. Available only for OnException(MethodExecutionArgs).
        /// </summary>        
        ThrowException,
    }
}