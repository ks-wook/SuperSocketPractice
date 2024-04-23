using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketEngine;

namespace SuperSocketPractice
{
    // AppServer를 상속받는 Listener 생성
    public class Listener : AppServer<ClientSession, EFBinaryRequestInfo>
    {
        SuperSocket.SocketBase.Config.IServerConfig m_Config;
        PacketProcessor MainPacketProcessor = new PacketProcessor();


        public Listener()
            : base(new DefaultReceiveFilterFactory<ReceiveFilter, EFBinaryRequestInfo>())
        {
            NewSessionConnected += new SessionHandler<ClientSession>(OnConnected);
            SessionClosed += new SessionHandler<ClientSession, CloseReason>(OnClosed);
            NewRequestReceived += new RequestHandler<ClientSession, EFBinaryRequestInfo>(OnPacketReceived);
        }

        public void Init()
        {
            m_Config = new SuperSocket.SocketBase.Config.ServerConfig
            {
                Name = "SuperSocketChat",
                Ip = "Any", // 모든 주소 연결 허용
                Port = 8282, // 포트 할당
                Mode = SocketMode.Tcp,
                MaxConnectionNumber = 10, // 최대 동접 수
                MaxRequestLength = 100,
                ReceiveBufferSize = 2048, // recv 버퍼 사이즈 2048 할당
                SendBufferSize = 2048, // send 버퍼 사이즈 2048 할당
            };
        }

        public void createAndStart()
        {
            bool result = Setup(new SuperSocket.SocketBase.Config.RootConfig(), m_Config);

            if(result == false)
            {
                Console.WriteLine("ChatServer 초기화 실패");
            }
            else
            {
                Console.WriteLine("ChatServer 초기화 성공");
            }


            CreateComponent();
            Start(); // 서버 Listening 시작

        }

        void CreateComponent()
        {
            MainPacketProcessor = new PacketProcessor();
            MainPacketProcessor.CreateAndStart(); // 프로세서 초기화
        }


        public bool SendData(string sessionID, byte[] sendData)
        {
            var session = GetSessionByID(sessionID);

            try
            {
                if (session == null)
                {
                    return false;
                }

                session.Send(sendData, 0, sendData.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                session.Close();
            }
            return true;
        }


        void OnConnected(ClientSession session)
        {
            Console.WriteLine($"[OnConnected] sesseionID: {session.SessionID}");
        } 

        void OnClosed(ClientSession session, CloseReason closeReason) 
        {
            Console.WriteLine($"[OnClosed] sesseionID: {session.SessionID}, ${closeReason.ToString()}");
        }

        void OnPacketReceived(ClientSession clientSession, EFBinaryRequestInfo requestInfo)
        {
            Console.WriteLine($"[OnPacketReceived] sesseionId: {clientSession.SessionID}");

            var packet = new ServerPacketData();
            // 받은 패킷에 대해 파싱 + 패킷 조립

            packet.SessionID = clientSession.SessionID;
            packet.PacketSize = requestInfo.Size;
            packet.PacketID = requestInfo.PacketID;
            packet.Type = requestInfo.Type;
            packet.BodyData = requestInfo.Body;

            // TODO 패킷 프로세서에게 전달





        }

    }
}
