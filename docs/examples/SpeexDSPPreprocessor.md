# SpeexDSPPreprocessor Examples


## Preprocessor Run
```csharp
using NAudio.Wave;
using SpeexDSPSharp.Core;

var format = new WaveFormat(48000, 1);
var preprocessor = new SpeexDSPPreprocessor(20 * format.SampleRate / 1000, format.SampleRate);
var @true = 1;
preprocessor.Ctl(PreprocessorCtl.SPEEX_PREPROCESS_SET_AGC, ref @true);
preprocessor.Ctl(PreprocessorCtl.SPEEX_PREPROCESS_SET_DENOISE, ref @true);
preprocessor.Ctl(PreprocessorCtl.SPEEX_PREPROCESS_SET_VAD, ref @true);
var gain = 30000;
preprocessor.Ctl(PreprocessorCtl.SPEEX_PREPROCESS_SET_AGC_TARGET, ref gain);
var buffer = new BufferedWaveProvider(format) { ReadFully = true };
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

output.Init(buffer);

Task.Delay(500).Wait();
output.Play();
recorder.StartRecording();

void Recorder_DataAvailable(object? sender, WaveInEventArgs e)
{
    try
    {
        Console.WriteLine($"Detecting Voice: {(preprocessor.Run(e.Buffer) == 1? true : false)}");
        buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

Console.ReadLine();
```