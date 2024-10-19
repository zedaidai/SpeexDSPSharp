using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core.Structures;
using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core
{
    public static class NativeHandler
    {
        const string DllName = "libspeexdsp-1.dll";
        //Jitter Buffer, Cannot figure out how this works so I'm just not gonna use it. Tends to freeze the application.
        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexJitterBufferSafeHandler jitter_buffer_init(int step_size);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_reset(SpeexJitterBufferSafeHandler jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_destroy(IntPtr jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_put(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int jitter_buffer_get(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet, int desired_span, int* start_offset);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jitter_buffer_get_another(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int jitter_buffer_update_delay(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet, int* start_offset);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jitter_buffer_get_pointer_timestamp(SpeexJitterBufferSafeHandler jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_tick(SpeexJitterBufferSafeHandler jitter);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_remaining_span(SpeexJitterBufferSafeHandler jitter, int rem);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int jitter_buffer_ctl(SpeexJitterBufferSafeHandler jitter, int request, void* ptr);


        //Echo Cancellation
        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexEchoStateSafeHandler speex_echo_state_init(int frame_size, int filter_length);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexEchoStateSafeHandler speex_echo_state_init_mc(int frame_size, int filter_length, int nb_mic, int nb_speaker);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void speex_echo_state_destroy(IntPtr st);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_cancellation(SpeexEchoStateSafeHandler st, short* rec, short* play, short* output);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_capture(SpeexEchoStateSafeHandler st, short* rec, short* output);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_playback(SpeexEchoStateSafeHandler st, short* play);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_state_reset(SpeexEchoStateSafeHandler st);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int speex_echo_ctl(SpeexEchoStateSafeHandler st, int request, void* ptr);


        //Preprocessor
        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexPreprocessStateSafeHandler speex_preprocess_state_init(int frame_size, int sampling_rate);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void speex_preprocess_state_destroy(IntPtr st);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int speex_preprocess_run(SpeexPreprocessStateSafeHandler st, short* x);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_preprocess_estimate_update(SpeexPreprocessStateSafeHandler st, short* x);

        [DllImportAttribute(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int speex_preprocess_ctl(SpeexPreprocessStateSafeHandler st, int request, void* ptr);
    }
}
