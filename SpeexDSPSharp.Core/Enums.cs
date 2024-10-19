namespace SpeexDSPSharp.Core
{
    public enum JitterBufferState
    {
        JITTER_BUFFER_OK = 0,
        JITTER_BUFFER_MISSING = 1,
        JITTER_BUFFER_INCOMPLETE = 2,
        JITTER_BUFFER_INTERNAL_ERROR = -1,
        JITTER_BUFFER_BAD_ARGUMENT = -2,
    }

    public enum JitterBufferCtl
    {
        JITTER_BUFFER_SET_MARGIN = 0,
        JITTER_BUFFER_GET_MARGIN = 1,
        JITTER_BUFFER_GET_AVAILABLE_COUNT = 3, //Grammar mistake in official speex docs so I just corrected it.
    }
}