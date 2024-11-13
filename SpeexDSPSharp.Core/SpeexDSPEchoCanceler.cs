using SpeexDSPSharp.Core.SafeHandlers;
using System;

namespace SpeexDSPSharp.Core
{
    /// <summary>
    /// Speexdsp echo cancellation.
    /// </summary>
    public class SpeexDSPEchoCanceler
    {
        /// <summary>
        /// Direct safe handle for the <see cref="SpeexDSPEchoCanceler"/>. IT IS NOT RECOMMENDED TO CLOSE THE HANDLE DIRECTLY! Instead use <see cref="Dispose(bool)"/> to dispose the handle and object safely.
        /// </summary>
        protected readonly SpeexDSPEchoStateSafeHandler _handler;
        private bool _disposed;

        /// <summary>
        /// Creates a new speexdsp echo canceler.
        /// </summary>
        /// <param name="frame_size">Number of samples to process at one time (should correspond to 10-20 ms).</param>
        /// <param name="filter_length">Number of samples of echo to cancel (should generally correspond to 100-500 ms).</param>
        public SpeexDSPEchoCanceler(int frame_size, int filter_length)
        {
            _handler = NativeSpeexDSP.speex_echo_state_init(frame_size, filter_length);
        }

        /// <summary>
        /// Creates a new multi-channel speexdsp echo canceler.
        /// </summary>
        /// <param name="frame_size">Number of samples to process at one time (should correspond to 10-20 ms).</param>
        /// <param name="filter_length">Number of samples of echo to cancel (should generally correspond to 100-500 ms).</param>
        /// <param name="nb_mic">Number of microphone channels.</param>
        /// <param name="nb_speaker">Number of speaker channels.</param>
        public SpeexDSPEchoCanceler(int frame_size, int filter_length, int nb_mic, int nb_speaker)
        {
            _handler = NativeSpeexDSP.speex_echo_state_init_mc(frame_size, filter_length, nb_mic, nb_speaker);
        }

        /// <summary>
        /// Speexdsp echo canceler destructor.
        /// </summary>
        ~SpeexDSPEchoCanceler()
        {
            Dispose(false);
        }

        /// <summary>
        /// Reset the echo canceler to its original state.
        /// </summary>
        public void Reset()
        {
            ThrowIfDisposed();
            NativeSpeexDSP.speex_echo_state_reset(_handler);
        }

        /// <summary>
        /// Let the echo canceler know that a frame was just queued to the sound card.
        /// </summary>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        public unsafe void EchoPlayback(Span<byte> play)
        {
            ThrowIfDisposed();
            fixed (byte* playPtr = play)
            {
                NativeSpeexDSP.speex_echo_playback(_handler, (short*)playPtr);
            }
        }

        /// <summary>
        /// Let the echo canceler know that a frame was just queued to the sound card.
        /// </summary>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        public unsafe void EchoPlayback(Span<short> play)
        {
            ThrowIfDisposed();
            fixed (short* playPtr = play)
            {
                NativeSpeexDSP.speex_echo_playback(_handler, playPtr);
            }
        }

        /// <summary>
        /// Let the echo canceler know that a frame was just queued to the sound card.
        /// </summary>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        public unsafe void EchoPlayback(Span<float> play)
        {
            ThrowIfDisposed();
            fixed (float* playPtr = play)
            {
                NativeSpeexDSP.speex_echo_playback(_handler, (short*)playPtr);
            }
        }

        /// <summary>
        /// Let the echo canceler know that a frame was just queued to the sound card.
        /// </summary>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        public void EchoPlayback(byte[] play) => EchoPlayback(play.AsSpan());

        /// <summary>
        /// Let the echo canceler know that a frame was just queued to the sound card.
        /// </summary>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        public void EchoPlayback(short[] play) => EchoPlayback(play.AsSpan());

        /// <summary>
        /// Let the echo canceler know that a frame was just queued to the sound card.
        /// </summary>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        public void EchoPlayback(float[] play) => EchoPlayback(play.AsSpan());

        /// <summary>
        /// Perform echo cancellation using internal playback buffer, which is delayed by two frames to account for the delay introduced by most sound cards (but it could be off!).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCapture(Span<byte> rec, Span<byte> output)
        {
            ThrowIfDisposed();
            fixed (byte* recPtr = rec)
            fixed (byte* outPtr = output)
            {
                NativeSpeexDSP.speex_echo_capture(_handler, (short*)recPtr, (short*)outPtr);
            }
        }

        /// <summary>
        /// Perform echo cancellation using internal playback buffer, which is delayed by two frames to account for the delay introduced by most sound cards (but it could be off!).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCapture(Span<short> rec, Span<short> output)
        {
            ThrowIfDisposed();
            fixed (short* recPtr = rec)
            fixed (short* outPtr = output)
            {
                NativeSpeexDSP.speex_echo_capture(_handler, recPtr, outPtr);
            }
        }

        /// <summary>
        /// Perform echo cancellation using internal playback buffer, which is delayed by two frames to account for the delay introduced by most sound cards (but it could be off!).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCapture(Span<float> rec, Span<float> output)
        {
            ThrowIfDisposed();
            fixed (float* recPtr = rec)
            fixed (float* outPtr = output)
            {
                NativeSpeexDSP.speex_echo_capture(_handler, (short*)recPtr, (short*)outPtr);
            }
        }

        /// <summary>
        /// Perform echo cancellation using internal playback buffer, which is delayed by two frames to account for the delay introduced by most sound cards (but it could be off!).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCapture(byte[] rec, byte[] output) => EchoCapture(rec.AsSpan(), output.AsSpan());

        /// <summary>
        /// Perform echo cancellation using internal playback buffer, which is delayed by two frames to account for the delay introduced by most sound cards (but it could be off!).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCapture(short[] rec, short[] output) => EchoCapture(rec.AsSpan(), output.AsSpan());

        /// <summary>
        /// Perform echo cancellation using internal playback buffer, which is delayed by two frames to account for the delay introduced by most sound cards (but it could be off!).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCapture(float[] rec, float[] output) => EchoCapture(rec.AsSpan(), output.AsSpan());

        /// <summary>
        /// Performs echo cancellation a frame, based on the audio sent to the speaker (no delay is added to playback in this form).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCancel(Span<byte> rec, Span<byte> play, Span<byte> output)
        {
            ThrowIfDisposed();

            fixed (byte* recPtr = rec)
            fixed (byte* playPtr = play)
            fixed (byte* outPtr = output)
            {
                NativeSpeexDSP.speex_echo_cancellation(_handler, (short*)recPtr, (short*)playPtr, (short*)outPtr);
            }
        }

        /// <summary>
        /// Performs echo cancellation a frame, based on the audio sent to the speaker (no delay is added to playback in this form).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCancel(Span<short> rec, Span<short> play, Span<short> output)
        {
            ThrowIfDisposed();

            fixed (short* recPtr = rec)
            fixed (short* playPtr = play)
            fixed (short* outPtr = output)
            {
                NativeSpeexDSP.speex_echo_cancellation(_handler, recPtr, playPtr, outPtr);
            }
        }

        /// <summary>
        /// Performs echo cancellation a frame, based on the audio sent to the speaker (no delay is added to playback in this form).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public unsafe void EchoCancel(Span<float> rec, Span<float> play, Span<float> output)
        {
            ThrowIfDisposed();

            fixed (float* recPtr = rec)
            fixed (float* playPtr = play)
            fixed (float* outPtr = output)
            {
                NativeSpeexDSP.speex_echo_cancellation(_handler, (short*)recPtr, (short*)playPtr, (short*)outPtr);
            }
        }


        /// <summary>
        /// Performs echo cancellation a frame, based on the audio sent to the speaker (no delay is added to playback in this form).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public void EchoCancel(byte[] rec, byte[] play, byte[] output) => EchoCancel(rec.AsSpan(), play.AsSpan(), output.AsSpan());


        /// <summary>
        /// Performs echo cancellation a frame, based on the audio sent to the speaker (no delay is added to playback in this form).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public void EchoCancel(short[] rec, short[] play, short[] output) => EchoCancel(rec.AsSpan(), play.AsSpan(), output.AsSpan());


        /// <summary>
        /// Performs echo cancellation a frame, based on the audio sent to the speaker (no delay is added to playback in this form).
        /// </summary>
        /// <param name="rec">Signal from the microphone (near end + far end echo).</param>
        /// <param name="play">Signal played to the speaker (received from far end).</param>
        /// <param name="output">Returns near-end signal with echo removed.</param>
        public void EchoCancel(float[] rec, float[] play, float[] output) => EchoCancel(rec.AsSpan(), play.AsSpan(), output.AsSpan());

        /// <summary>
        /// Performs a ctl request.
        /// </summary>
        /// <typeparam name="T">The type you want to input/output.</typeparam>
        /// <param name="request">The request you want to specify.</param>
        /// <param name="value">The input/output value.</param>
        /// <returns>0 if no error, -1 if request is unknown.</returns>
        public unsafe int Ctl<T>(EchoCancellationCtl request, ref T value) where T : unmanaged
        {
            ThrowIfDisposed();
            fixed (void* valuePtr = &value)
            {
                var result = NativeSpeexDSP.speex_echo_ctl(_handler, (int)request, valuePtr);
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
