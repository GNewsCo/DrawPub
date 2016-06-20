using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Natmir.ImageService.AOP
{
    public sealed class MethodExecutionArgs
    {
        private readonly object instance;
        private readonly MethodInfo targetMethod;
        private readonly MethodInfo methodContract;
        private readonly object[] arguments;

        /// <summary>
        /// Gets the information about the method being executed from the service (abstraction) type.
        /// </summary>
        public MethodInfo MethodContract
        {
            get { return methodContract; }
        }

        /// <summary>
        /// Gets the information about the method being executed on the target type.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return targetMethod;
            }
        }

        /// <summary>
        /// Gets the arguments with which the method has been invoked.
        /// 
        /// </summary>
        public object[] Arguments
        {
            get { return arguments; }
        }

        /// <summary>
        /// Gets or sets the method return value. 
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Gets the Task object when yielding from a task-based asynchronous operation
        /// </summary>
        public object YieldValue { get; set; }

        ///// <summary>
        ///// Gets or sets the value yielded by the iterator method. 
        ///// </summary>
        //public object YieldValue { get; set; }

        /// <summary>
        /// Gets the exception currently flying. 
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Determines the control flow of the target method once the advice is exited.
        /// 
        /// </summary>
        public FlowBehavior FlowBehavior { get; set; }

        /// <summary>
        /// User-defined state information whose lifetime is linked to the current method execution. 
        /// </summary>
        public object MethodExecutionState { get; set; }


        /// <summary>
        /// Gets or sets the object instance on which the method is being executed.
        /// </summary>
        public object Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Instantiates a new MethodExecutionArgs when the intercepted type has no type contract.
        /// </summary>
        /// <param name="instance">The instance of the intercepted type.</param>
        /// <param name="method">The intercepted method on the target</param>
        /// <param name="arguments">The arguments of the intercepted method.</param>
        public MethodExecutionArgs(object instance, MethodInfo method, object[] arguments)
            : this(instance, method, method, arguments)
        {
        }

        /// <summary>
        /// Instantiates a new MethodExecutionArgs when the intercepted type has type contract.
        /// </summary>
        /// <param name="instance">The instance of the intercepted type.</param>
        /// <param name="methodContract">The method info on the contract</param>
        /// <param name="targetMethod">The intercepted method on the target</param>
        /// <param name="arguments">The arguments of the intercepted method.</param>
        /// <remarks>The difference between target method and method contract is when they are applied different attributes.</remarks>
        public MethodExecutionArgs(object instance, MethodInfo targetMethod, MethodInfo methodContract, object[] arguments)
        {
            if (targetMethod == null)
                throw new ArgumentNullException("targetMethod");

            if (methodContract == null)
                throw new ArgumentNullException("methodContract");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.instance = instance;
            this.methodContract = methodContract;
            this.arguments = arguments;
            this.targetMethod = targetMethod;
        }

    }
}