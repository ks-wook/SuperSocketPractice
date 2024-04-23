using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketPractice
{
    public class EFBinaryRequestInfo : BinaryRequestInfo
    {
        public Int16 Size { get; private set; }
        public Int16 PacketID { get; private set; }
        public SByte Type { get; private set; }

        public EFBinaryRequestInfo(Int16 size, Int16 packetID, SByte type, byte[] body)
            : base(null, body)
        {
            this.Size = size;
            this.PacketID = packetID;
            this.Type = type;
        }
    }

    // TEST 패킷 헤더는 5바이트 고정
    public class ReceiveFilter : FixedHeaderReceiveFilter<EFBinaryRequestInfo>
    {
        public ReceiveFilter() : base(5)
        {
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            if (!BitConverter.IsLittleEndian) // littleEndian인 경우 뒤집는다.
            {
                Array.Reverse(header, offset, 5);
            }

            var packetSize = BitConverter.ToInt16(header, offset);
            var bodySize = packetSize - 5;
            return bodySize;
        }

        protected override EFBinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            if (!BitConverter.IsLittleEndian) // littleEndian인 경우 뒤집는다.
                Array.Reverse(header.Array, 0, 5);

            return new EFBinaryRequestInfo(BitConverter.ToInt16(header.Array, 0),
                                           BitConverter.ToInt16(header.Array, 2),
                                           (SByte)header.Array[4],
                                           bodyBuffer.CloneRange(offset, length));
        }
    }
}
