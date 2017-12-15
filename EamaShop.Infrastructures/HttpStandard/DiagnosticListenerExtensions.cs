using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EamaShop.Infrastructures.HttpStandard
{
    internal static class DiagnosticListenerExtensions
    {
        public const string HTTP_STANDARD = "EamaShop.StandardHttp";
        public static void WriteMessage(this DiagnosticListener diagnostic, string message, object parameter)
        {
            if (diagnostic == null)
            {
                throw new ArgumentNullException(nameof(diagnostic));
            }
            if (diagnostic.IsEnabled(HTTP_STANDARD))
            {
                diagnostic.Write(message, parameter);
            }
        }
    }
}
