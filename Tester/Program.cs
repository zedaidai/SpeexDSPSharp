using SpeexDSPSharp.Core;
using SpeexDSPSharp.Core.Structures;

var jitter = new SpeexJitterBuffer();

var inputData = new byte[960];
inputData[0] = 1;
inputData[1] = 2;
inputData[2] = 3;
inputData[3] = 4;
inputData[4] = 5;
inputData[5] = 6;
inputData[6] = 7;
inputData[7] = 8;
inputData[8] = 9;
inputData[9] = 10;
jitter.Put(inputData);

jitter.Get();

public class SpeexJitterBuffer
{
    private readonly SpeexDSPJitterBuffer buffer = new SpeexDSPJitterBuffer(1);
    private uint timestamp = 1;

    public void Get()
    {
        var data = new byte[960];
        var outPacket = new SpeexDSPJitterBufferPacket(data, (uint)data.Length);

        int temp = 0;
        if (buffer.Get(ref outPacket, 1, ref temp) != JitterBufferState.JITTER_BUFFER_OK)
        {
            Console.WriteLine("Missing Packet");
        }
        else
        {
            Console.WriteLine("Found Packet");
            Console.WriteLine(data[0]);
            Console.WriteLine(data[1]);
            Console.WriteLine(data[2]);
            Console.WriteLine(data[3]);
            Console.WriteLine(data[4]);
            Console.WriteLine(data[5]);
            Console.WriteLine(data[6]);
            Console.WriteLine(data[7]);
            Console.WriteLine(data[8]);
            Console.WriteLine(data[9]);
        }

        buffer.Tick();
    }

    /// <summary>
    /// Puts the <paramref name="frameData"/> into the buffer. Note that the given byte array
    /// is not copied so you transfer ownership to the buffer.
    /// </summary>
    public void Put(byte[] frameData)
    {
        var inPacket = new SpeexDSPJitterBufferPacket(frameData, (uint)frameData.Length);
        if (frameData == null)
            throw new ArgumentNullException("frameData");

        inPacket.sequence = 0;
        inPacket.span = 1;
        inPacket.timestamp = timestamp++;

        buffer.Put(ref inPacket);
    }
}