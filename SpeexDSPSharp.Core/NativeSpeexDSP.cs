using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core.Structures;
using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core
{
    /// <summary>
    /// Native speexdsp handler that directly calls the exported opus functions.
    /// </summary>
    public static class NativeSpeexDSP
    {
#if MACOS || IOS || MACCATALYST
        private const string DllName = "__Internal__";
#else
        private const string DllName = "speexdsp";
#endif

        //Jitter Buffer
        /// <summary>
        /// Initializes jitter buffer.
        /// </summary>
        /// <param name="step_size">Starting value for the size of concealment packets and delay adjustment steps. Can be changed at any time using JITTER_BUFFER_SET_DELAY_STEP and JITTER_BUFFER_GET_CONCEALMENT_SIZE.</param>
        /// <returns>Newly created jitter buffer state.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexJitterBufferSafeHandler jitter_buffer_init(int step_size);

        /// <summary>
        /// Restores jitter buffer to its original state.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_reset(SpeexJitterBufferSafeHandler jitter);

        /// <summary>
        /// Destroys jitter buffer.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_destroy(IntPtr jitter);

        /// <summary>
        /// Put one packet into the jitter buffer.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        /// <param name="packet">Incoming packet.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_put(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet);

        /// <summary>
        /// Get one packet from the jitter buffer.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        /// <param name="packet">Returned packet.</param>
        /// <param name="desired_span">Number of samples (or units) we wish to get from the buffer (no guarantee).</param>
        /// <param name="start_offset">Timestamp for the returned packet.</param>
        /// <returns><see cref="JitterBufferState"/></returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int jitter_buffer_get(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet, int desired_span, int* start_offset);

        /// <summary>
        /// Used right after jitter_buffer_get() to obtain another packet that would have the same timestamp. This is mainly useful for media where a single "frame" can be split into several packets.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        /// <param name="packet">Returned packet.</param>
        /// <returns><see cref="JitterBufferState"/></returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jitter_buffer_get_another(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet);

        /// <summary>
        /// N.A.
        /// </summary>
        /// <param name="jitter"></param>
        /// <param name="packet"></param>
        /// <param name="start_offset"></param>
        /// <returns></returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int jitter_buffer_update_delay(SpeexJitterBufferSafeHandler jitter, ref SpeexJitterBufferPacket packet, int* start_offset);

        /// <summary>
        /// Get pointer timestamp of jitter buffer.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        /// <returns>I have no clue what this returns.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jitter_buffer_get_pointer_timestamp(SpeexJitterBufferSafeHandler jitter);

        /// <summary>
        /// Advance by one tick.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_tick(SpeexJitterBufferSafeHandler jitter);

        /// <summary>
        /// Telling the jitter buffer about the remaining data in the application buffer.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        /// <param name="rem">Amount of data buffered by the application (timestamp units).</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jitter_buffer_remaining_span(SpeexJitterBufferSafeHandler jitter, int rem);

        /// <summary>
        /// Used like the ioctl function to control the jitter buffer parameters.
        /// </summary>
        /// <param name="jitter">Jitter buffer state.</param>
        /// <param name="request">ioctl-type request (one of the JITTER_BUFFER_* macros).</param>
        /// <param name="ptr">Data exchanged to-from function.</param>
        /// <returns>0 if no error, -1 if request is unknown.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int jitter_buffer_ctl(SpeexJitterBufferSafeHandler jitter, int request, void* ptr);


        //Echo Cancellation
        /// <summary>
        /// Creates a new echo canceller state.
        /// </summary>
        /// <param name="frame_size">Number of samples to process at one time (should correspond to 10-20 ms).</param>
        /// <param name="filter_length">Number of samples of echo to cancel (should generally correspond to 100-500 ms).</param>
        /// <returns>Newly-created echo canceller state.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexEchoStateSafeHandler speex_echo_state_init(int frame_size, int filter_length);

        /// <summary>
        /// Creates a new multi-channel echo canceller state.
        /// </summary>
        /// <param name="frame_size">Number of samples to process at one time (should correspond to 10-20 ms).</param>
        /// <param name="filter_length">Number of samples of echo to cancel (should generally correspond to 100-500 ms).</param>
        /// <param name="nb_mic">Number of microphone channels.</param>
        /// <param name="nb_speaker">Number of speaker channels.</param>
        /// <returns>Newly-created echo canceller state.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexEchoStateSafeHandler speex_echo_state_init_mc(int frame_size, int filter_length, int nb_mic, int nb_speaker);

        /// <summary>
        /// Destroys an echo canceller state.
        /// </summary>
        /// <param name="st">Echo canceller state.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void speex_echo_state_destroy(IntPtr st);

        /// <summary>
        /// Performs echo cancellation a frame, based on the audio sent to the speaker (no delay is added to playback in this form).
        /// </summary>
        /// <param name="st">Echo canceller state.</param>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_cancellation(SpeexEchoStateSafeHandler st, short* rec, short* play, short* output);

        /// <summary>
        /// Perform echo cancellation using internal playback buffer, which is delayed by two frames to account for the delay introduced by most soundcards (but it could be off!).
        /// </summary>
        /// <param name="st">Echo canceller state.</param>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_capture(SpeexEchoStateSafeHandler st, short* rec, short* output);

        /// <summary>
        /// Let the echo canceller know that a frame was just queued to the soundcard.
        /// </summary>
        /// <param name="st">Echo canceller state.</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_playback(SpeexEchoStateSafeHandler st, short* play);

        /// <summary>
        /// Reset the echo canceller to its original state.
        /// </summary>
        /// <param name="st">Echo canceller state.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_echo_state_reset(SpeexEchoStateSafeHandler st);

        /// <summary>
        /// Used like the ioctl function to control the echo canceller parameters.
        /// </summary>
        /// <param name="st">Echo canceller state.</param>
        /// <param name="request">ioctl-type request (one of the SPEEX_ECHO_* macros).</param>
        /// <param name="ptr">Data exchanged to-from function.</param>
        /// <returns>0 if no error, -1 if request in unknown.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int speex_echo_ctl(SpeexEchoStateSafeHandler st, int request, void* ptr);


        //Preprocessor
        /// <summary>
        /// Creates a new preprocessing state. You MUST create one state per channel processed.
        /// </summary>
        /// <param name="frame_size">Number of samples to process at one time (should correspond to 10-20 ms). Must be the same value as that used for the echo canceller for residual echo cancellation to work.</param>
        /// <param name="sampling_rate">Sampling rate used for the input.</param>
        /// <returns>Newly created preprocessor state.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SpeexPreprocessStateSafeHandler speex_preprocess_state_init(int frame_size, int sampling_rate);

        /// <summary>
        /// Destroys a preprocessor state.
        /// </summary>
        /// <param name="st">Preprocessor state to destroy.</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void speex_preprocess_state_destroy(IntPtr st);

        /// <summary>
        /// Preprocess a frame.
        /// </summary>
        /// <param name="st">Preprocessor state.</param>
        /// <param name="x">Audio sample vector (in and out). Must be same size as specified in speex_preprocess_state_init().</param>
        /// <returns>Bool value for voice activity (1 for speech, 0 for noise/silence), ONLY if VAD turned on.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int speex_preprocess_run(SpeexPreprocessStateSafeHandler st, short* x);

        /// <summary>
        /// Update preprocessor state, but do not compute the output.
        /// </summary>
        /// <param name="st">Preprocessor state.</param>
        /// <param name="x">Audio sample vector (in only). Must be same size as specified in speex_preprocess_state_init().</param>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void speex_preprocess_estimate_update(SpeexPreprocessStateSafeHandler st, short* x);

        /// <summary>
        /// Used like the ioctl function to control the preprocessor parameters.
        /// </summary>
        /// <param name="st">Preprocessor state.</param>
        /// <param name="request">request ioctl-type request (one of the SPEEX_PREPROCESS_* macros).</param>
        /// <param name="ptr">Data exchanged to-from function.</param>
        /// <returns>0 if no error, -1 if request in unknown.</returns>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int speex_preprocess_ctl(SpeexPreprocessStateSafeHandler st, int request, void* ptr);
    }
}
