using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP
{
    public abstract class Aspect
    {
        /// <summary>
        /// Gets the order in which the given aspect will be applied.
        /// </summary>
        public virtual int SequenceOrder
        {
            get
            {
                return AspectsSequence.GetOrder(this);
            }
        }

        /// <summary>
        /// When implemented specifies under which condition the given type should be intercepted with the current aspect.
        /// </summary>
        /// <param name="targetType">The concrete type being intercepted.</param>
        /// <returns>Whether or not the current aspect is applicable to the condition of the given type.</returns>
        public abstract bool IsApplicable(Type targetType);

        /// <summary>
        /// Handles the operations to be performed upon entering a method.
        /// </summary>
        /// <param name="args">Arguments specific for the method being entered for the current aspect.</param>
        public virtual void OnEntry(MethodExecutionArgs args)
        {
        }

        /// <summary>
        /// Handles the operations to be performed upon exiting a method.
        /// </summary>
        /// <param name="args">Arguments specific for the method being exited for the current aspect.</param>
        public virtual void OnExit(MethodExecutionArgs args)
        {
        }

        /// <summary>
        /// Handles the operations to be performed upon encountering an exception on a method.
        /// </summary>
        /// <param name="args">Arguments specific for the method having the exception for the current aspect.</param>
        public virtual void OnException(MethodExecutionArgs args)
        {
        }


        /// <summary>
        /// Handles the operations to be performed upon successful execution of the method.
        /// </summary>
        /// <param name="args">Arguments specific for the method completed execution successfully for the current 
        ///     aspect.</param>
        public virtual void OnSuccess(MethodExecutionArgs args)
        {
        }

    }
}