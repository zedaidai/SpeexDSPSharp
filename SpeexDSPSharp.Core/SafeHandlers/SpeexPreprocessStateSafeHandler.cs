using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core.SafeHandlers
{
    public class SpeexPreprocessStateSafeHandler : SafeHandle
    {
        public SpeexPreprocessStateSafeHandler() : base(IntPtr.Zero, true)
        {

        }

        public override bool IsInvalid => handle == IntPtr.Zero;
        protected override bool ReleaseHandle()
        {
            NativeSpeexDSP.speex_preprocess_state_destroy(handle);
            return true;
        }
    }
}
