using MusicHelpers.Helpers;
using System;

namespace MusicHelpers
{
    class Program
    {
        static void Main(string[] args)
        {
            MusicHelper mh = new Netease();
            //mh.GetSongById(513360721);
            //mh.Search("我是", 1);
            //Netease.GetLrc(513360721);
            // Netease.GetDetail(513360721);

            //Netease.GetDetailsR(new string[] { "513360721", "1311076476" });
            //mh.GetSongsByIds(new string[] { "513360721", "1311076476" });
            mh.Search("我是", 1);
        }
    }
}
