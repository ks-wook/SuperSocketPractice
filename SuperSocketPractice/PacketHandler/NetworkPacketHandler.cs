using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketPractice
{
    public  class NetworkPacketHandler : PacketHandler
    {
        public NetworkPacketHandler(Listener listener) : base(listener)
        {
        }

        public override void RegisterPacketHandler(Dictionary<int, Action<ServerPacketData>> packetHandlerMap)
        {
            // 테스트 패킷 핸들러 등록
            packetHandlerMap.Add((int)PacketId.S_Connect, OnRecvTestPacket);

        }


        public void OnRecvTestPacket(ServerPacketData packet)
        {
            var sessionId = packet.SessionID;

            // TODO 테스트 패킷에 대한 로그 작성





        }


    }
}
