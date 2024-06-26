﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace SuperSocketPractice
{
    public class PacketProcessor
    {
        Dictionary<int, Action<MemoryPackBinaryRequestInfo>>_packetHandlerMap = new Dictionary<int, Action<MemoryPackBinaryRequestInfo>>(); // 패킷의 ID와 패킷 핸들러를 같이 등록한다.
        NetworkPacketHandler _networkPacketHandler = new NetworkPacketHandler();
        
        System.Threading.Thread? _processThread = null; // 패킷 처리용 쓰레드 선언

        bool IsThreadRunning = false;

        // 비동기 접근 가능한 bufferBlock, 여러 스레드가 동시에 접근하여도 블럭킹 되지 않는다.
        BufferBlock<MemoryPackBinaryRequestInfo> _recvBuffer = new BufferBlock<MemoryPackBinaryRequestInfo>();


        // 룸 개념은 나중에 추가
        public void CreateAndStart()
        {
            // 패킷 처리용 쓰레드를 생성하고, 패킷 처리를 도맡아한다.    

            RegisterPakcetHandler();



            IsThreadRunning = true;
            _processThread = new System.Threading.Thread(this.Process);
            _processThread.Start();
        }

        void RegisterPakcetHandler()
        {
            // 여러 종류의 패킷 핸들러에 선언된 핸들러들을 패킷 프로세서의 핸들러에 최종 등록한다.
            _networkPacketHandler.RegisterPacketHandler(_packetHandlerMap);
        }


        public void Destory()
        {
            IsThreadRunning = false;
            _recvBuffer.Complete();
        }


        public void Insert(MemoryPackBinaryRequestInfo packet)
        {
            _recvBuffer.Post(packet);
        }



        void Process()
        {
            while (IsThreadRunning)
            {
                try
                {
                    var packet = _recvBuffer.Receive();

                    var header = new MemoryPackPacketHeadInfo();
                    header.Read(packet.Data);
                    Console.WriteLine(header.Id);

                    if (_packetHandlerMap.ContainsKey(header.Id))
                    {
                        _packetHandlerMap[header.Id](packet);
                    }
                    else
                    {
                        Console.WriteLine("세션 번호 {0}, PacketID {1}, 받은 데이터 크기: {2}", packet.SessionID, header.Id, packet.Body.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }

    

}
