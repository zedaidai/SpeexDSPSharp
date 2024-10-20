using SpeexDSPSharp.Core.SafeHandlers;
using System;

namespace SpeexDSPSharp.Core
{
    public class SpeexPreprocessor
    {
        private readonly SpeexPreprocessStateSafeHandler _handler;
        private bool _disposed;

        public SpeexPreprocessor(int frame_size, int sample_rate)
        {
            _handler = NativeHandler.speex_preprocess_state_init(frame_size, sample_rate);
        }

        ~SpeexPreprocessor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public unsafe int Ctl<T>(EchoCancellationCtl request, ref T value) where T : unmanaged
        {
            ThrowIfDisposed();
            fixed (void* valuePtr = &value)
            {
                var result = NativeHandler.speex_preprocess_ctl(_handler, (int)request, valuePtr);
                CheckError(result);
                return result;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (!_handler.IsClosed)
                    _handler.Close();
            }

            _disposed = true;
        }

        protected virtual void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        protected static void CheckError(int result)
        {
            if (result < 0)
                throw new SpeexException(result.ToString());
        }
    }
}
