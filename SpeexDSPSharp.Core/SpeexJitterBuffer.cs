using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core.Structures;
using System;

namespace SpeexDSPSharp.Core
{
    public class SpeexJitterBuffer : IDisposable
    {
        private readonly SpeexJitterBufferSafeHandler _handler;
        private bool _disposed;

        public SpeexJitterBuffer(int step_size)
        {
            _handler = NativeHandler.jitter_buffer_init(step_size);
        }

        ~SpeexJitterBuffer()
        {
            Dispose(false);
        }

        public void Reset()
        {
            ThrowIfDisposed();
            NativeHandler.jitter_buffer_reset(_handler);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Put(ref SpeexJitterBufferPacket packet)
        {
            ThrowIfDisposed();
            NativeHandler.jitter_buffer_put(_handler, ref packet);
        }

        public unsafe int Get(ref SpeexJitterBufferPacket packet, int desired_span, ref int start_offset)
        {
            ThrowIfDisposed();
            var result = NativeHandler.jitter_buffer_get(_handler, ref packet, desired_span, (int*)start_offset);
            CheckError(result);
            return result;
        }

        public int GetAnother(ref SpeexJitterBufferPacket packet)
        {
            ThrowIfDisposed();
            var result = NativeHandler.jitter_buffer_get_another(_handler, ref packet);
            CheckError(result);
            return result;
        }

        public unsafe int UpdateDelay(ref SpeexJitterBufferPacket packet, ref int start_offset)
        {
            ThrowIfDisposed();
            var result = NativeHandler.jitter_buffer_update_delay(_handler, ref packet, (int*)start_offset);
            CheckError(result);
            return result;
        }

        public int GetPointerTimestamp()
        {
            ThrowIfDisposed();
            var result = NativeHandler.jitter_buffer_get_pointer_timestamp(_handler);
            CheckError(result);
            return result;
        }

        public void Tick()
        {
            ThrowIfDisposed();
            NativeHandler.jitter_buffer_tick(_handler);
        }

        public void RemainingSpan(int remaining_span)
        {
            ThrowIfDisposed();
            NativeHandler.jitter_buffer_remaining_span(_handler, remaining_span);
        }

        public unsafe int Ctl(JitterBufferCtl request, ref int value)
        {
            ThrowIfDisposed();
            var result = NativeHandler.jitter_buffer_ctl(_handler, (int)request, ref value);
            CheckError(result);
            return result;
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
                throw new SpeexException(((JitterBufferState)result).ToString());
        }
    }
}
