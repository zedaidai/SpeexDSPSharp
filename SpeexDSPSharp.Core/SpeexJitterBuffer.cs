using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core.Structures;
using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core
{
    public class SpeexJitterBuffer : IDisposable
    {
        private readonly SpeexJitterBufferSafeHandler _handler;
        private bool _disposed;

        public SpeexJitterBuffer(int tick)
        {
            _handler = NativeHandler.jitter_buffer_init(tick);
        }

        ~SpeexJitterBuffer()
        {
            Dispose(false);
        }

        public void Reset()
        {
            NativeHandler.jitter_buffer_reset(_handler);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Put(SpeexJitterBufferPacket packet)
        {
            IntPtr packetPtr = Marshal.AllocHGlobal(Marshal.SizeOf(packet));
            Marshal.StructureToPtr(packet, packetPtr, true);
            NativeHandler.jitter_buffer_put(_handler, packetPtr);
        }

        public void Get(ref SpeexJitterBufferPacket packet, int start_offset)
        {
            IntPtr packetPtr = Marshal.AllocHGlobal(Marshal.SizeOf(packet));
            Marshal.StructureToPtr(packet, packetPtr, true);

            var result = NativeHandler.jitter_buffer_get(_handler, packetPtr, ref start_offset);
            CheckError(result);
        }

        public int GetPointerTimestamp()
        {
            var result = NativeHandler.jitter_buffer_get_pointer_timestamp(_handler);
            CheckError(result);
            return result;
        }

        public void Tick()
        {
            NativeHandler.jitter_buffer_tick(_handler);
        }

        public unsafe int Ctl<T>(JitterBufferCtl request, void* output)
        {
            var result = NativeHandler.jitter_buffer_ctl(_handler, (int)request, output);
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

        protected static void CheckError(int result)
        {
            if (result < 0)
                throw new SpeexException(((JitterBufferState)result).ToString());
        }
    }
}
