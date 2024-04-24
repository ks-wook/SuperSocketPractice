namespace SuperSocketPractice
{
    internal class Program
    {
        //dotnet ChatServer.dll --uniqueID 1 --roomMaxCount 16 --roomMaxUserCount 4 --roomStartNumber 1 --maxUserCount 100


        // 에코서버 -> 이후 룸 개념 추가해서 채팅 서버


        // 구현된 것
        // 1. appServer 상속받은 리스너 생성 및 실행
        // 2. 패킷 프로세서 생성하여 패킷 처리 쓰레드, 네트워크 IO 쓰레드 분리
        // 3. MemoryPack 이용하여 패킷 deserialize

        // 구현해야 할 것
        // 1. 사용자 로그인 처리(DB 연동 X)
        // 2. 룸 개념 도입, 2명이 입장가능한 룸 생성 및 룸 관련 기능 추가
        // 



        static void Main(string[] args)
        {
            Console.WriteLine("Server Start");

            // 리스너 생성 및 실행
            Listener listener = new Listener();
            listener.Init();



            listener.CreateAndStart();
            

            while (true)
            {
                System.Threading.Thread.Sleep(50);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.KeyChar == 'q')
                    {
                        Console.WriteLine("Server Terminate ~~~");
                        listener.StopServer();
                        break;
                    }
                }

            }
        }
    }
}
