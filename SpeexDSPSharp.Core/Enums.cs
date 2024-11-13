namespace SpeexDSPSharp.Core
{
    /// <summary>
    /// SpeexDSPJitterBuffer state codes.
    /// </summary>
    public enum JitterBufferState
    {
        /// <summary>
        /// Packet has been retrieved.
        /// </summary>
        JITTER_BUFFER_OK = 0,

        /// <summary>
        /// Packet is lost or is late.
        /// </summary>
        JITTER_BUFFER_MISSING = 1,

        /// <summary>
        /// A "fake" packet is meant to be inserted here to increase buffering.
        /// </summary>
        JITTER_BUFFER_INSERTION = 2,

        /// <summary>
        /// There was an error in the jitter buffer.
        /// </summary>
        JITTER_BUFFER_INTERNAL_ERROR = -1,

        /// <summary>
        /// Invalid argument.
        /// </summary>
        JITTER_BUFFER_BAD_ARGUMENT = -2,
    }

    /// <summary>
    /// JitterBuffer values for CTL interface.
    /// </summary>
    public enum JitterBufferCtl
    {
        /// <summary>
        /// Set minimum amount of extra buffering required (margin).
        /// </summary>
        JITTER_BUFFER_SET_MARGIN = 0,

        /// <summary>
        /// Get minimum amount of extra buffering required (margin).
        /// </summary>
        JITTER_BUFFER_GET_MARGIN = 1,

        /// <summary>
        /// Get the amount of available packets currently buffered.
        /// </summary>
        JITTER_BUFFER_GET_AVAILABLE_COUNT = 3,

        /// <summary>
        /// Assign a function to destroy unused packet. When setting that, the jitter buffer no longer copies packet data.
        /// </summary>
        JITTER_BUFFER_SET_DESTROY_CALLBACK = 4,

        /// <summary>
        /// N.A.
        /// </summary>
        JITTER_BUFFER_GET_DESTROY_CALLBACK = 5,

        /// <summary>
        /// Tell the jitter buffer to only adjust the delay in multiples of the step parameter provided.
        /// </summary>
        JITTER_BUFFER_SET_DELAY_STEP = 6,

        /// <summary>
        /// N.A.
        /// </summary>
        JITTER_BUFFER_GET_DELAY_STEP = 7,

        /// <summary>
        /// Tell the jitter buffer to only do concealment in multiples of the size parameter provided.
        /// </summary>
        JITTER_BUFFER_SET_CONCEALMENT_SIZE = 8,

        /// <summary>
        /// N.A.
        /// </summary>
        JITTER_BUFFER_GET_CONCEALMENT_SIZE = 9,

        /// <summary>
        /// Absolute max amount of loss that can be tolerated regardless of the delay. Typical loss should be half of that or less.
        /// </summary>
        JITTER_BUFFER_SET_MAX_LATE_RATE = 10,

        /// <summary>
        /// N.A.
        /// </summary>
        JITTER_BUFFER_GET_MAX_LATE_RATE = 11,

        /// <summary>
        /// Equivalent cost of one percent late packet in timestamp units.
        /// </summary>
        JITTER_BUFFER_SET_LATE_COST = 12,

        /// <summary>
        /// N.A.
        /// </summary>
        JITTER_BUFFER_GET_LATE_COST = 13
    }

    /// <summary>
    /// Echo cancellation values for CTL interface.
    /// </summary>
    public enum EchoCancellationCtl
    {
        /// <summary>
        /// Obtain frame size used by the AEC.
        /// </summary>
        SPEEX_ECHO_GET_FRAME_SIZE = 3,

        /// <summary>
        /// Set sampling rate.
        /// </summary>
        SPEEX_ECHO_SET_SAMPLING_RATE = 24,

        /// <summary>
        /// Get sampling rate.
        /// </summary>
        SPEEX_ECHO_GET_SAMPLING_RATE = 25,

        /// <summary>
        /// Get size of impulse response (int).
        /// </summary>
        SPEEX_ECHO_GET_IMPULSE_RESPONSE_SIZE = 27,

        /// <summary>
        /// Get impulse response (int[]).
        /// </summary>
        SPEEX_ECHO_GET_IMPULSE_RESPONSE = 29
    }

    /// <summary>
    /// Preprocessor values for CTL interface.
    /// </summary>
    public enum PreprocessorCtl
    {
        /// <summary>
        /// Set preprocessor denoiser state
        /// </summary>
        SPEEX_PREPROCESS_SET_DENOISE = 0,

        /// <summary>
        /// Get preprocessor denoiser state
        /// </summary>
        SPEEX_PREPROCESS_GET_DENOISE = 1,

        /// <summary>
        /// Set preprocessor Automatic Gain Control state.
        /// </summary>
        SPEEX_PREPROCESS_SET_AGC = 2,

        /// <summary>
        /// Get preprocessor Automatic Gain Control state
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC = 3,

        /// <summary>
        /// Set preprocessor Voice Activity Detection state.
        /// </summary>
        SPEEX_PREPROCESS_SET_VAD = 4,

        /// <summary>
        /// Get preprocessor Voice Activity Detection state
        /// </summary>
        SPEEX_PREPROCESS_GET_VAD = 5,

        /// <summary>
        /// Set preprocessor Automatic Gain Control level (float).
        /// </summary>
        SPEEX_PREPROCESS_SET_AGC_LEVEL = 6,

        /// <summary>
        /// Get preprocessor Automatic Gain Control level (float).
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC_LEVEL = 7,

        /// <summary>
        /// Set preprocessor de-reverb state.
        /// </summary>
        SPEEX_PREPROCESS_SET_DEREVERB = 8,

        /// <summary>
        /// Get preprocessor de-reverb state.
        /// </summary>
        SPEEX_PREPROCESS_GET_DEREVERB = 9,

        /// <summary>
        /// Set preprocessor de-reverb level.
        /// </summary>
        SPEEX_PREPROCESS_SET_DEREVERB_LEVEL = 10,

        /// <summary>
        /// Get preprocessor de-reverb level.
        /// </summary>
        SPEEX_PREPROCESS_GET_DEREVERB_LEVEL = 11,

        /// <summary>
        /// Set preprocessor de-reverb decay.
        /// </summary>
        SPEEX_PREPROCESS_SET_DEREVERB_DECAY = 12,

        /// <summary>
        /// Get preprocessor de-reverb decay.
        /// </summary>
        SPEEX_PREPROCESS_GET_DEREVERB_DECAY = 13,

        /// <summary>
        /// Set probability required for the VAD to go from silence to voice.
        /// </summary>
        SPEEX_PREPROCESS_SET_PROB_START = 14,

        /// <summary>
        /// Get probability required for the VAD to go from silence to voice
        /// </summary>
        SPEEX_PREPROCESS_GET_PROB_START = 15,

        /// <summary>
        /// Set probability required for the VAD to stay in the voice state (integer percent).
        /// </summary>
        SPEEX_PREPROCESS_SET_PROB_CONTINUE = 16,

        /// <summary>
        /// Get probability required for the VAD to stay in the voice state (integer percent).
        /// </summary>
        SPEEX_PREPROCESS_GET_PROB_CONTINUE = 17,

        /// <summary>
        /// Set maximum attenuation of the noise in dB (negative number).
        /// </summary>
        SPEEX_PREPROCESS_SET_NOISE_SUPPRESS = 18,

        /// <summary>
        /// Get maximum attenuation of the noise in dB (negative number)
        /// </summary>
        SPEEX_PREPROCESS_GET_NOISE_SUPPRESS = 19,

        /// <summary>
        /// Set maximum attenuation of the residual echo in dB (negative number).
        /// </summary>
        SPEEX_PREPROCESS_SET_ECHO_SUPPRESS = 20,

        /// <summary>
        /// Get maximum attenuation of the residual echo in dB (negative number).
        /// </summary>
        SPEEX_PREPROCESS_GET_ECHO_SUPPRESS = 21,

        /// <summary>
        /// Set maximum attenuation of the residual echo in dB when near end is active (negative number).
        /// </summary>
        SPEEX_PREPROCESS_SET_ECHO_SUPPRESS_ACTIVE = 22,

        /// <summary>
        /// Get maximum attenuation of the residual echo in dB when near end is active (negative number).
        /// </summary>
        SPEEX_PREPROCESS_GET_ECHO_SUPPRESS_ACTIVE = 23,

        /// <summary>
        /// Set the corresponding echo canceler state so that residual echo suppression can be performed (NULL for no residual echo suppression).
        /// </summary>
        SPEEX_PREPROCESS_SET_ECHO_STATE = 24,

        /// <summary>
        /// Get the corresponding echo canceler state.
        /// </summary>
        SPEEX_PREPROCESS_GET_ECHO_STATE = 25,

        /// <summary>
        /// Set maximal gain increase in dB/second (int).
        /// </summary>
        SPEEX_PREPROCESS_SET_AGC_INCREMENT = 26,

        /// <summary>
        /// Get maximal gain increase in dB/second (int).
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC_INCREMENT = 27,

        /// <summary>
        /// Set maximal gain decrease in dB/second (int).
        /// </summary>
        SPEEX_PREPROCESS_SET_AGC_DECREMENT = 28,

        /// <summary>
        /// Get maximal gain decrease in dB/second (int).
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC_DECREMENT = 29,

        /// <summary>
        /// Set maximal gain in dB (int32)
        /// </summary>
        SPEEX_PREPROCESS_SET_AGC_MAX_GAIN = 30,

        /// <summary>
        /// Get maximal gain in dB (int32)
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC_MAX_GAIN = 31,

        /// <summary>
        /// Get loudness.
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC_LOUDNESS = 33,

        /// <summary>
        /// Get current gain (int percent).
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC_GAIN = 35,

        /// <summary>
        /// Get spectrum size for power spectrum (int).
        /// </summary>
        SPEEX_PREPROCESS_GET_PSD_SIZE = 37,

        /// <summary>
        /// Get power spectrum (int[] of squared values).
        /// </summary>
        SPEEX_PREPROCESS_GET_PSD = 39,

        /// <summary>
        /// Get spectrum size for noise estimate (int).
        /// </summary>
        SPEEX_PREPROCESS_GET_NOISE_PSD_SIZE = 41,

        /// <summary>
        /// Get noise estimate (int32 of squared values).
        /// </summary>
        SPEEX_PREPROCESS_GET_NOISE_PSD = 43,

        /// <summary>
        /// Get speech probability in last frame (int).
        /// </summary>
        SPEEX_PREPROCESS_GET_PROB = 45,

        /// <summary>
        /// Set preprocessor Automatic Gain Control level (int).
        /// </summary>
        SPEEX_PREPROCESS_SET_AGC_TARGET = 46,

        /// <summary>
        /// Get preprocessor Automatic Gain Control level (int).
        /// </summary>
        SPEEX_PREPROCESS_GET_AGC_TARGET = 47
    }
}