using System;
using System.Runtime.InteropServices;

namespace SpeexDSPSharp.Core.Structures
{
    /// <summary>
    /// Definition of an incoming packet.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SpeexDSPJitterBufferPacket
    {
        /// <summary>
        /// Data bytes contained in the packet.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray)]
        public Memory<byte> data;

        /// <summary>
        /// Length of the packet in bytes.
        /// </summary>
        public uint len;

        /// <summary>
        /// Timestamp for the packet
        /// </summary>
        public uint timestamp;

        /// <summary>
        /// Time covered by the packet (same units as timestamp).
        /// </summary>
        public uint span;

        /// <summary>
        /// RTP Sequence number if available (0 otherwise).
        /// </summary>
        public ushort sequence;

        /// <summary>
        /// Put whatever data you like here (it's ignored by the jitter buffer).
        /// </summary>
        public uint user_data;
    }
}