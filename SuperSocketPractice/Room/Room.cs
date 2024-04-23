using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketPractice
{
    // 게임룸 클래스
    public class Room
    {
        public int RommNumber { get; set; }
        List<User> users = new List<User>();


        
    }
}
