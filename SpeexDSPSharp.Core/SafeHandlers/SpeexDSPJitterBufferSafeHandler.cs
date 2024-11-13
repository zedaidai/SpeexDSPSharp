using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core.SafeHandlers
{
    /// <summary>
    /// Managed wrapper over the SpeexDSPJitterBuffer state.
    /// </summary>
    public class SpeexDSPJitterBufferSafeHandler : SafeHandle
    {

        /// <summary>
        /// Creates a new <see cref="SpeexDSPJitterBufferSafeHandler"/>.
        /// </summary>
        public SpeexDSPJitterBufferSafeHandler() : base(IntPtr.Zero, true)
        {

        }

        /// <inheritdoc/>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <inheritdoc/>
        protected override bool ReleaseHandle()
        {
            NativeSpeexDSP.jitter_buffer_destroy(handle);
            return true;
        }
    }
}
