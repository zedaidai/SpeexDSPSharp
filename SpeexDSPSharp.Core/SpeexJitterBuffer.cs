using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core.Structures;

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

        public void Put(ref SpeexJitterBufferPacket packet)
        {
            NativeHandler.jitter_buffer_put(_handler, ref packet);
        }

        public unsafe int Get(ref SpeexJitterBufferPacket packet, int desired_span, ref int start_offset)
        {
            start_offset = 0;
            var result = NativeHandler.jitter_buffer_get(_handler, ref packet, desired_span, (int*)start_offset);
            CheckError(result);
            return result;
        }

        public int GetAnother(ref SpeexJitterBufferPacket packet)
        {
            var result = NativeHandler.jitter_buffer_get_another(_handler, ref packet);
            CheckError(result);
            return result;
        }

        public unsafe int UpdateDelay(ref SpeexJitterBufferPacket packet, ref int start_offset)
        {
            var result = NativeHandler.jitter_buffer_update_delay(_handler, ref packet, (int*)start_offset);
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
