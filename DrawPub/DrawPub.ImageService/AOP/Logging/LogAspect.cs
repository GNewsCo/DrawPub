using System;
using System.Linq;


namespace Natmir.ImageService.AOP.Logging
{
    public class LogAspect : Aspect
    {
        private readonly ILoggingHandler loggingHandler;

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        public LogAspect(ILoggingHandler loggingHandler)
        {
            this.loggingHandler = loggingHandler;
        }

        /// <summary>
        /// Called by aspect interceptor to check if the current type has Log or IgnoreLog attributes applied.
        /// </summary>
        public override bool IsApplicable(Type targetType)
        {
            // If assembly level has log attribute applied, consider that type should be intercepted with the log aspect
            // irrespective of having ignore log or not. 
            if (targetType.Assembly.IsDefined(typeof(LogAttribute), false))
                return true;

            // If class level has log attribute, specify for intercepting for logging.
            if (targetType.IsDefined(typeof(LogAttribute), false))
                return true;

            // If the class has ignore attribute or assembly doesn't have a log attribute on it, we need to inspect 
            // class method level to check and see if any of the methods has log attribute on them. The first log
            // attribute found would cause the class to be marked for interception for logging.
            return targetType.GetMethods().Any(m => m.IsDefined(typeof(LogAttribute), false));
        }

        /// <summary>
        /// Handles the operations to be performed upon entering a method.
        /// </summary>
        /// <param name="args">An instance of <see cref="MethodExecutionArgs"/> giving information specific for the 
        /// method being entered.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            ValidateAndHandleLogging(args, AdviceType.Entry);
        }

        /// <summary>
        /// Handles the operations to be performed upon exiting a method.
        /// </summary>
        /// <param name="args">An instance of <see cref="MethodExecutionArgs"/> giving information specific for the 
        /// method being exited.</param>
        public override void OnExit(MethodExecutionArgs args)
        {
            ValidateAndHandleLogging(args, AdviceType.Exit);
        }

        /// <summary>
        /// Handles the operations to be performed upon successful execution of the method.
        /// </summary>
        /// <param name="args">An instance of <see cref="MethodExecutionArgs"/> giving information specific for the 
        /// method completed execution successfully.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            ValidateAndHandleLogging(args, AdviceType.Success);
        }

        private void ValidateAndHandleLogging(MethodExecutionArgs args, AdviceType adviceType)
        {
            if (!ValidateLogMethodArgs(args))
                return;

            if (adviceType == AdviceType.Exit && args.Exception == null)
                return;

            loggingHandler.LogMessage(args, adviceType);
        }

        /// <summary>
        /// Validates the <see cref="MethodExecutionArgs"/> instance passed to the <see cref="LogAspect"/> to ensure 
        /// that all required information are available.
        /// </summary>
        /// <param name="args">The instance of <see cref="MethodExecutionArgs"/> to be validated.</param>
        /// <returns><c>True</c> if the instance contains all required information; <c>False</c>, otherwise.</returns>
        private static bool ValidateLogMethodArgs(MethodExecutionArgs args)
        {
            if (args == null) throw new ArgumentNullException("args");

            if (args.Method == null)
                throw new ArgumentException("MethodInfo object contained within the MethodExecutionArgs cannot be null.");

            if (args.Method.DeclaringType == null)
                throw new ArgumentException("DeclaringType object contained within the MethodExecutionArgs.Method cannot be null.");

            return true;
        }
    }
}