using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System
{
    [DebuggerStepThrough]
    public class DomainException : Exception
    {
        public DomainException(string message):base(message)
        {

        }
    }
}
