using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core.SafeHandlers
{
    public class SpeexEchoStateSafeHandler : SafeHandle
    {
        public SpeexEchoStateSafeHandler() : base(IntPtr.Zero, true)
        {

        }

        public override bool IsInvalid => handle == IntPtr.Zero;
        protected override bool ReleaseHandle()
        {
            NativeSpeexDSP.speex_echo_state_destroy(handle);
            return true;
        }
    }
}
