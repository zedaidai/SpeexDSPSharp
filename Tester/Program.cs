using NAudio.Wave;
using Tester;

var format = new WaveFormat(48000, 1);
var buffer = new BufferedWaveProvider(format) { ReadFully = true };
var echo = new EchoCancellationWaveProvider(20, 100, buffer);
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
output.Play();
recorder.StartRecording();

void Recorder_DataAvailable(object? sender, WaveInEventArgs e)
{
    try
    {
        echo.Cancel(e.Buffer);
        buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }
}

Console.ReadLine();