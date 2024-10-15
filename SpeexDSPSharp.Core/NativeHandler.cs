using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core
{
    public static class NativeHandler
    {
        [DllImportAttribute("libspeexdsp.dll")]
        public static extern IntPtr speex_echo_state_init(int frameSize, int filterLength);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern void speex_echo_state_destroy(IntPtr st);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern void speex_echo_cancellation(IntPtr st, IntPtr recorded, IntPtr played, IntPtr echoRemoved);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern void speex_echo_playback(IntPtr st, IntPtr played);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern void speex_echo_capture(IntPtr st, IntPtr recorded, IntPtr echoRemoved);

        [DllImportAttribute("libspeexdsp.dll")]
        public unsafe static extern int speex_echo_ctl(IntPtr st, int id, ref IntPtr val);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern IntPtr speex_preprocess_state_init(int frameSize, int sampleRate);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern int speex_preprocess_ctl(IntPtr st, int id, IntPtr val);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern int speex_preprocess_run(IntPtr st, IntPtr recorded);

        [DllImportAttribute("libspeexdsp.dll")]
        public static extern void speex_preprocess_state_destroy(IntPtr st);
    }
}
