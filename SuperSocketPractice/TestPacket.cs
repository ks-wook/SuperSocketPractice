using MemoryPack;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketPractice
{
    public class ServerPacketData
    {
        public Int16 PacketSize;
        public string SessionID;
        public Int16 PacketID;
        public SByte Type;
        public byte[] BodyData;


        public void AssignPacketValue(string sessionID, Int16 packetID, byte[] packetBodyData)
        {
            SessionID = sessionID;
            PacketID = packetID;

            if (packetBodyData.Length > 0) // 패킷 바디가 있는 경우
            {
                BodyData = packetBodyData;
            }
        }

        //public static ServerPacketData MakeNTFInConnectOrDisConnectClientPacket(bool isConnect, string sessionID)
        //{
        //    var packet = new ServerPacketData();

        //    if (isConnect)
        //    {
        //        packet.PacketID = (Int32)PACKETID.NTF_IN_CONNECT_CLIENT;
        //    }
        //    else
        //    {
        //        packet.PacketID = (Int32)PACKETID.NTF_IN_DISCONNECT_CLIENT;
        //    }

        //    packet.SessionID = sessionID;
        //    return packet;
        //}
    }



    // 로그인 요청
    [MemoryPackable]
    public partial class PKTTest
    {
        public string Msg { get; set; } = string.Empty;
    }




    public enum PacketId : short
    {

        // Network 1001 ~
        S_Connect = 1001,
        S_Disconnect = 1002,


        // Test 2001 ~ 
        S_Test = 2001,

    }
}
