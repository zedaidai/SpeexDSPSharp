using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core.Structures;
using System;

namespace SpeexDSPSharp.Core
{
    /// <summary>
    /// Speexdsp jitter buffer.
    /// </summary>
    public class SpeexDSPJitterBuffer : IDisposable
    {
        /// <summary>
        /// Direct safe handle for the <see cref="SpeexDSPJitterBuffer"/>. IT IS NOT RECOMMENDED TO CLOSE THE HANDLE DIRECTLY! Instead use <see cref="Dispose(bool)"/> to dispose the handle and object safely.
        /// </summary>
        protected readonly SpeexDSPJitterBufferSafeHandler _handler;
        private bool _disposed;

        /// <summary>
        /// Creates a new speexdsp jitter buffer.
        /// </summary>
        /// <param name="step_size">Starting value for the size of concealment packets and delay adjustment steps. Can be changed at any time using JITTER_BUFFER_SET_DELAY_STEP and JITTER_BUFFER_GET_CONCEALMENT_SIZE.</param>
        public SpeexDSPJitterBuffer(int step_size)
        {
            _handler = NativeSpeexDSP.jitter_buffer_init(step_size);
        }

        /// <summary>
        /// Speexdsp jitter buffer destructor.
        /// </summary>
        ~SpeexDSPJitterBuffer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Reset the jitter buffer to its original state.
        /// </summary>
        public void Reset()
        {
            ThrowIfDisposed();
            NativeSpeexDSP.jitter_buffer_reset(_handler);
        }

        /// <summary>
        /// Put one packet into the jitter buffer.
        /// </summary>
        /// <param name="packet">Incoming packet.</param>
        public void Put(ref SpeexDSPJitterBufferPacket packet)
        {
            ThrowIfDisposed();
            NativeSpeexDSP.jitter_buffer_put(_handler, ref packet);
        }

        /// <summary>
        /// Get one packet from the jitter buffer.
        /// </summary>
        /// <param name="packet">Returned packet.</param>
        /// <param name="desired_span">Number of samples (or units) we wish to get from the buffer (no guarantee).</param>
        /// <param name="start_offset">Timestamp for the returned packet.</param>
        /// <returns><see cref="JitterBufferState"/></returns>
        public unsafe JitterBufferState Get(ref SpeexDSPJitterBufferPacket packet, int desired_span, ref int start_offset)
        {
            ThrowIfDisposed();
            var result = NativeSpeexDSP.jitter_buffer_get(_handler, ref packet, desired_span, (int*)start_offset);
            return (JitterBufferState)result;
        }

        /// <summary>
        /// Used right after jitter_buffer_get() to obtain another packet that would have the same timestamp. This is mainly useful for media where a single "frame" can be split into several packets.
        /// </summary>
        /// <param name="packet">Returned packet.</param>
        /// <returns><see cref="JitterBufferState"/></returns>
        public int GetAnother(ref SpeexDSPJitterBufferPacket packet)
        {
            ThrowIfDisposed();
            var result = NativeSpeexDSP.jitter_buffer_get_another(_handler, ref packet);
            return result;
        }

        /// <summary>
        /// N.A.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="start_offset"></param>
        /// <returns></returns>
        public unsafe int UpdateDelay(ref SpeexDSPJitterBufferPacket packet, ref int start_offset)
        {
            ThrowIfDisposed();
            var result = NativeSpeexDSP.jitter_buffer_update_delay(_handler, ref packet, (int*)start_offset);
            CheckError(result);
            return result;
        }

        /// <summary>
        /// Get pointer timestamp of jitter buffer.
        /// </summary>
        /// <returns>I have no clue what this returns.</returns>
        public int GetPointerTimestamp()
        {
            ThrowIfDisposed();
            return NativeSpeexDSP.jitter_buffer_get_pointer_timestamp(_handler);
        }

        /// <summary>
        /// Advance by one tick.
        /// </summary>
        public void Tick()
        {
            ThrowIfDisposed();
            NativeSpeexDSP.jitter_buffer_tick(_handler);
        }

        /// <summary>
        /// Telling the jitter buffer about the remaining data in the application buffer.
        /// </summary>
        /// <param name="remaining_span">Amount of data buffered by the application (timestamp units).</param>
        public void RemainingSpan(int remaining_span)
        {
            ThrowIfDisposed();
            NativeSpeexDSP.jitter_buffer_remaining_span(_handler, remaining_span);
        }

        /// <summary>
        /// Performs a ctl request.
        /// </summary>
        /// <typeparam name="T">The type you want to input/output.</typeparam>
        /// <param name="request">The request you want to specify.</param>
        /// <param name="value">The input/output value.</param>
        /// <returns>0 if no error, -1 if request is unknown.</returns>
        public unsafe int Ctl<T>(JitterBufferCtl request, ref T value) where T : unmanaged
        {
            ThrowIfDisposed();
            fixed (void* valuePtr = &value)
            {
                var result = NativeSpeexDSP.jitter_buffer_ctl(_handler, (int)request, valuePtr);
                CheckError(result);
                return result;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose logic.
        /// </summary>
        /// <param name="disposing">Set to true if fully disposing.</param>
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

        /// <summary>
        /// Throws an exception if this object is disposed or the handler is closed.
        /// </summary>
        /// <exception cref="ObjectDisposedException" />
        protected virtual void ThrowIfDisposed()
        {
            if (_disposed || _handler.IsClosed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        /// <summary>
        /// Checks if there is an opus error and throws if the error is a negative value.
        /// </summary>
        /// <param name="error">The error code to input.</param>
        /// <exception cref="SpeexDSPException"></exception>
        protected static void CheckError(int error)
        {
            if (error < 0)
                throw new SpeexDSPException(error.ToString());
        }
    }
}
