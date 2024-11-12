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