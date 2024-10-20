using SpeexDSPSharp.Core.SafeHandlers;
using System;

namespace SpeexDSPSharp.Core
{
    public class SpeexEchoCanceller
    {
        private readonly SpeexEchoStateSafeHandler _handler;
        private bool _disposed;

        public SpeexEchoCanceller(int frame_size, int filter_length)
        {
            _handler = NativeHandler.speex_echo_state_init(frame_size, filter_length);
        }

        public SpeexEchoCanceller(int frame_size, int filter_length, int nb_mic, int nb_speaker)
        {
            _handler = NativeHandler.speex_echo_state_init_mc(frame_size, filter_length, nb_mic, nb_speaker);
        }

        ~SpeexEchoCanceller()
        {
            Dispose(false);
        }

        public void Reset()
        {
            ThrowIfDisposed();
            NativeHandler.speex_echo_state_reset(_handler);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public unsafe void EchoCancel(short[] rec, short[] play, out short[] output)
        {
            ThrowIfDisposed();
            output = new short[rec.Length];

            fixed (short* recPtr = rec)
            fixed (short* playPtr = play)
            fixed (short* outPtr = output)
            {
                NativeHandler.speex_echo_cancellation(_handler, recPtr, playPtr, outPtr);
            }
        }

        public unsafe void EchoCapture(short[] rec, out short[] output)
        {
            ThrowIfDisposed();
            output = new short[rec.Length];

            fixed (short* recPtr = rec)
            fixed (short* outPtr = output)
            {
                NativeHandler.speex_echo_capture(_handler, recPtr, outPtr);
            }
        }

        public unsafe void EchoPlayback(short[] play)
        {
            ThrowIfDisposed();
            fixed (short* playPtr = play)
            {
                NativeHandler.speex_echo_playback(_handler, playPtr);
            }
        }

        public unsafe int Ctl<T>(EchoCancellationCtl request, ref T value) where T : unmanaged
        {
            ThrowIfDisposed();
            fixed (void* valuePtr = &value)
            {
                var result = NativeHandler.speex_echo_ctl(_handler, (int)request, valuePtr);
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
