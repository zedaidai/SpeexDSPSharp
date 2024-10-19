using SpeexDSPSharp.Core;
using SpeexDSPSharp.Core.Structures;

try
{
    var buffer = new SpeexJitterBuffer(1);
    var packet = new SpeexJitterBufferPacket() { data = new byte[960], len = 960, span = 20, timestamp = 0 };
    packet.data[0] = 1;

    buffer.Put(ref packet);

    var start_offset = 0;
    packet.timestamp = 1;
    buffer.Get(ref packet, 20, ref start_offset);
    Console.WriteLine(packet.data[0]);
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}