using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpeexJitterBufferPacket
    {
        [MarshalAs(UnmanagedType.ByValArray)]
        public byte[] data;
        public uint len;
        public uint timestamp;
        public uint span;
    }
}