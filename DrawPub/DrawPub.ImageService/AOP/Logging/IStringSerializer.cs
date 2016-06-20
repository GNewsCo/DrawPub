using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natmir.ImageService.AOP.Logging
{
    /// <summary>
    /// Provides the functionality that should be implemented by any serialization provider in use within logging.
    /// </summary>
    public interface IStringSerializer
    {
        /// <summary>
        /// Serializes a given object to its string representation.
        /// </summary>
        /// <param name="o">Object to serialize.</param>
        /// <param name="depth">Depth to which the object would be serialized to. Can be null, where the entire object
        /// would be serialized.</param>
        /// <returns>String representation of the object.</returns>
        string Serialize(object o, int depth);
    }
}