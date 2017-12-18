using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    /// <summary>
    /// Provides method for checking parameter.
    /// </summary>
    static class ArgumentUtilities
    {
        internal static T NotNull<T>(T obj,string parameterName) where T : class
        {
            if (obj == null)
            {
                NotNull(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }
            return obj;
        }
    }
}
