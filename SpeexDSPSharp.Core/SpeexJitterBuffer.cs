using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core.Structures;
using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core
{
    public class SpeexJitterBuffer : Disposable
    {
        private readonly SpeexJitterBufferSafeHandler _handler;

        public SpeexJitterBuffer(int step_size)
        {
            _handler = NativeHandler.jitter_buffer_init(step_size);
        }

        public void Reset()
        {
            NativeHandler.jitter_buffer_reset(_handler);
        }

        public void Put(SpeexJitterBufferPacket packet)
        {
            IntPtr packetPtr = Marshal.AllocHGlobal(Marshal.SizeOf(packet));
            Marshal.StructureToPtr(packet, packetPtr, true);
            NativeHandler.jitter_buffer_put(_handler, packetPtr);
        }

        public unsafe int Get(ref SpeexJitterBufferPacket packet, int desired_span, ref int start_offset)
        {
            IntPtr packetPtr = Marshal.AllocHGlobal(Marshal.SizeOf(packet));
            Marshal.StructureToPtr(packet, packetPtr, true);

            start_offset = 0;
            var result = NativeHandler.jitter_buffer_get(_handler, packetPtr, desired_span, (int*)start_offset);
            CheckError(result);
            return result;
        }

        public int GetAnother(ref SpeexJitterBufferPacket packet)
        {
            IntPtr packetPtr = Marshal.AllocHGlobal(Marshal.SizeOf(packet));
            Marshal.StructureToPtr(packet, packetPtr, true);

            var result = NativeHandler.jitter_buffer_get_another(_handler, packetPtr);
            CheckError(result);
            return result;
        }

        public unsafe int UpdateDelay(ref SpeexJitterBufferPacket packet, ref int start_offset)
        {
            IntPtr packetPtr = Marshal.AllocHGlobal(Marshal.SizeOf(packet));
            Marshal.StructureToPtr(packet, packetPtr, true);

            var result = NativeHandler.jitter_buffer_update_delay(_handler, packetPtr, (int*)start_offset);
            CheckError(result);
            return result;
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

        public void RemainingSpan(int remaining_span)
        {
            NativeHandler.jitter_buffer_remaining_span(_handler, remaining_span);
        }

        public unsafe int Ctl(JitterBufferCtl request, void* output)
        {
            var result = NativeHandler.jitter_buffer_ctl(_handler, (int)request, output);
            CheckError(result);
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_handler.IsClosed)
                    _handler.Close();
            }
        }

        protected static void CheckError(int result)
        {
            if (result < 0)
                throw new SpeexException(((JitterBufferState)result).ToString());
        }
    }
}
