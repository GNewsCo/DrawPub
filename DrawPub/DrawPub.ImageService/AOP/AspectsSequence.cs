using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Natmir.ImageService.AOP
{
    static class AspectsSequence
    {
        /// <remarks>
        /// The aspects will be applied in the following order. The target method will be invoked after the last aspect.
        /// </remarks>
        private readonly static List<Type> aspectsSequence = new List<Type>
        {
            //typeof(ExceptionAspect),
            //typeof(AuthorizationAspect),
            //typeof(CurrentSessionAspect),
            //typeof(ValidationAspect),
            //typeof(CacheAspect),
            //typeof(InvalidateCacheAspect),
            //typeof(LogAspect),
        };


        /// <summary>
        /// Gets the sequence order in which the given aspect will run.
        /// </summary>
        /// <param name="aspect">The aspect to get the sequence order of</param>
        public static int GetOrder(Aspect aspect)
        {
            Type aspectType = aspect.GetType();
            var index = aspectsSequence.IndexOf(aspectType);
            if (index >= 0)
            {
                return index;
            }

            string message = string.Format("Sequence order is not specified for {0}. Please  go to {1} and specify the sequence order of {0}.", aspectType.Name, MethodBase.GetCurrentMethod().DeclaringType);
            throw new Exception(message);
        }
    }
}