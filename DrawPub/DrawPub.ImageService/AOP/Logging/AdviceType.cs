using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    public enum AdviceType
    {
        /// <summary>
        /// Entry message for the method.
        /// </summary>
        Entry,

        /// <summary>
        /// Exit message for the method.
        /// </summary>
        Exit,

        /// <summary>
        /// Successful message for the method.
        /// </summary>
        Success
    }
}