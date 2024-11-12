using NAudio.Wave;
using SpeexDSPSharp.Core;

namespace Tester
{
    public class EchoCancellationWaveProvider : IWaveProvider
    {
        private IWaveProvider _source;
        private SpeexEchoCanceller _canceller;

        public WaveFormat WaveFormat => _source.WaveFormat;

        public EchoCancellationWaveProvider(int frame_size_ms, int filter_length_ms, IWaveProvider source)
        {
            _source = source;
            var sampleRate = WaveFormat.SampleRate;
            var frame_size = frame_size_ms * sampleRate / 1000;
            var filter_length = filter_length_ms * sampleRate / 1000;

            _canceller = new SpeexEchoCanceller(frame_size, filter_length);
            _canceller.Ctl(EchoCancellationCtl.SPEEX_ECHO_SET_SAMPLING_RATE, ref sampleRate);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int samplesRead = _source.Read(buffer, offset, count);

            _canceller.EchoPlayback(buffer);
            return samplesRead;
        }

        public void Cancel(byte[] buffer, byte[] canceled)
        {
            _canceller.EchoCapture(buffer, canceled);
        }
    }
}
