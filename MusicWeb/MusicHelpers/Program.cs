using MusicHelpers.Helpers;
using System;

namespace MusicHelpers
{
    class Program
    {
        static void Main(string[] args)
        {
            #region netease test
            MusicHelper mh = new Netease();
            ////mh.GetSongById(513360721);
            ////mh.Search("我是", 1);
            ////Netease.GetLrc(513360721);
            //// Netease.GetDetail(513360721);

            ////Netease.GetDetailsR(new string[] { "513360721", "1311076476" });
            mh.GetSongsByIds(new string[] { "513360721" });
            //mh.Search("我是", 1);
            #endregion
            //string re = QQ.GetSongByIdR("002B2EAA3brD5b");
           // QQ.GetSongByIdR("002B2EAA3brD5b");
            //QQ.GetMusicInfo(new string[] { "002B2EAA3brD5b", "002RkFPr1R58Z8" });
           // QQ.GetLrcBySongid("5219940");
           // Console.WriteLine(re);
            //Console.ReadKey();

        }
    }
}
