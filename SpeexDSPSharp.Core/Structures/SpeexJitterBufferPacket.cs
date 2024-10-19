using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpeexJitterBufferPacket
    {
        public byte[] Data;
        public uint Length;
        public uint Timestamp;
        public uint Span;

        public SpeexJitterBufferPacket(uint size, uint timestamp = 0, uint span = 0)
        {
            Data = new byte[size];
            Length = size;
            Timestamp = timestamp;
            Span = span;
        }
    }
}