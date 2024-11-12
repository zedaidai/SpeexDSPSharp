using System;

namespace SpeexDSPSharp.Core
{
    public class SpeexDSPException : Exception
    {
        public SpeexDSPException(string message) : base(message) { }

        public SpeexDSPException(string message, Exception innerException) : base(message, innerException) { }
    }
}
