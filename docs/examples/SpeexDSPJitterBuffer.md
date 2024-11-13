# SpeexDSPJitterBuffer Examples

## Put and Get
```csharp
using SpeexDSPSharp.Core;
using SpeexDSPSharp.Core.Structures;

var jitter = new SpeexJitterBuffer();

var inputData = new byte[960];
for(byte i = 0; i < 25; i++)
{
    inputData[i] = (byte)Random.Shared.Next(byte.MinValue, byte.MaxValue);
    Console.WriteLine($"Input: {inputData[i]}");
}
jitter.Put(inputData);

Console.WriteLine();
jitter.Get(); //Found Packet
jitter.Get(); //Missing Packet

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
            for (byte i = 0; i < 25; i++)
            {
                Console.WriteLine($"Output: {data[i]}");
            }
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
```