# Examples

Below are some general examples for OpusSharp's api.

## NAudio Example

# [Program.cs](#tab/programcs)

```csharp
using NAudio.Wave;
using Tester;

var format = new WaveFormat(48000, 1);
var buffer = new BufferedWaveProvider(format) { ReadFully = true };
var echo = new EchoCancellationWaveProvider(20, 200, buffer);
var recorder = new WaveInEvent()
{
    WaveFormat = format,
    BufferMilliseconds = 20
};
var output = new WaveOutEvent()
{
    DesiredLatency = 100
};

recorder.DataAvailable += Recorder_DataAvailable;

output.Init(echo);

Task.Delay(500).Wait();
output.Play();
recorder.StartRecording();

void Recorder_DataAvailable(object? sender, WaveInEventArgs e)
{
    try
    {
        var output = new byte[e.BytesRecorded];
        echo.Cancel(e.Buffer, output);
        buffer.AddSamples(output, 0, e.BytesRecorded);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }
}

Console.ReadLine();
```

# [EchoCancellationWaveProvider.cs](#tab/echocancellationwaveprovidercs)

```csharp
using NAudio.Wave;
using SpeexDSPSharp.Core;

namespace Tester
{
    public class EchoCancellationWaveProvider : IWaveProvider
    {
        private IWaveProvider _source;
        private SpeexDSPEchoCanceler _canceller;

        public WaveFormat WaveFormat => _source.WaveFormat;

        public EchoCancellationWaveProvider(int frame_size_ms, int filter_length_ms, IWaveProvider source)
        {
            _source = source;
            var sampleRate = WaveFormat.SampleRate;
            var frame_size = frame_size_ms * sampleRate / 1000;
            var filter_length = filter_length_ms * sampleRate / 1000;

            _canceller = new SpeexDSPEchoCanceler(frame_size, filter_length);
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

```