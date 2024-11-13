using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core.SafeHandlers
{
    /// <summary>
    /// Managed wrapper over the SpeexDSPPreprocess state.
    /// </summary>
    public class SpeexDSPPreprocessStateSafeHandler : SafeHandle
    {
        /// <summary>
        /// Creates a new <see cref="SpeexDSPPreprocessStateSafeHandler"/>.
        /// </summary>
        public SpeexDSPPreprocessStateSafeHandler() : base(IntPtr.Zero, true)
        {

        }

        /// <inheritdoc/>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <inheritdoc/>
        protected override bool ReleaseHandle()
        {
            NativeSpeexDSP.speex_preprocess_state_destroy(handle);
            return true;
        }
    }
}
