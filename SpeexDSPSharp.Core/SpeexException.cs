using System;

namespace SpeexDSPSharp.Core
{
    public class SpeexException : Exception
    {
        public SpeexException(string message) : base(message) { }

        public SpeexException(string message, Exception innerException) : base(message, innerException) { }
    }
}
