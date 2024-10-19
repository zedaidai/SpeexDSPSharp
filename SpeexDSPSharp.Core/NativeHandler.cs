using SpeexDSPSharp.Core.SafeHandlers;
using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core
{
    public static class NativeHandler
    {
        const string DllName = "libspeexdsp-1.dll";
        //Jitter Buffer
        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexJitterBufferSafeHandler jitter_buffer_init(int tick);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_reset(SpeexJitterBufferSafeHandler jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_destroy(IntPtr jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_put(SpeexJitterBufferSafeHandler jitter, IntPtr packet);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jitter_buffer_get(SpeexJitterBufferSafeHandler jitter, IntPtr packet, ref int start_offset);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jitter_buffer_get_pointer_timestamp(SpeexJitterBufferSafeHandler jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_tick(SpeexJitterBufferSafeHandler jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int jitter_buffer_ctl(SpeexJitterBufferSafeHandler jitter, int request, void* ptr);
    }
}
