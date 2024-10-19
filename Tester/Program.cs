using SpeexDSPSharp.Core;
using SpeexDSPSharp.Core.Structures;

var buffer = new SpeexJitterBuffer(960);
var packet = new SpeexJitterBufferPacket() { Data = new byte[960], Length = 960, Span = 960, Timestamp = 0 };
buffer.Put(packet);
var outPacket = new SpeexJitterBufferPacket(960);
buffer.Get(ref outPacket, 0);
Console.WriteLine(outPacket.Data);