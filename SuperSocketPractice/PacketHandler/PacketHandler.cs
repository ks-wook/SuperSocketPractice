using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketPractice
{
    public abstract class PacketHandler
    {
        Listener _listener;


        public PacketHandler(Listener listener) 
        {
            _listener = listener;
        }

        public abstract void RegisterPacketHandler(Dictionary<int, Action<ServerPacketData>> packetHandlerMap);

    }
}
