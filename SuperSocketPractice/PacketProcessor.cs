using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketPractice
{
    public class PacketProcessor
    {
        Dictionary<int, Action<ServerPacketData>>_packetHandlerMap = new Dictionary<int, Action<ServerPacketData>>(); // 패킷의 ID와 패킷 핸들러를 같이 등록한다.
        NetworkPacketHandler _networkPacketHandler;

        // TODO bufferblock이용해서 패킷에 대해 처리


        public void CreateAndStart()
        {
            // TODO 패킷 처리용 쓰레드를 생성하고, 패킷 처리를 도맡아한다.    
        }

        

        public void Destory()
        {

        }


        public void Insert()
        {

        }


        void RegisterPakcetHandler()
        {
            // 여러 종류의 패킷 핸들러에 선언된 핸들러들을 패킷 프로세서의 핸들러에 최종 등록한다.
            _networkPacketHandler.RegisterPacketHandler(_packetHandlerMap);
        }

    }

    

}
