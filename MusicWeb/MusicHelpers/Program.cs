using MusicHelpers.Helpers;
using System;
using System.Net;

namespace MusicHelpers
{
    class Program
    {
        static void Main(string[] args)
        {
            #region netease test
            // MusicHelper mh = new Netease();
            ////mh.GetSongById(513360721);
            ////mh.Search("我是", 1);
            // Netease.GetLrcR("513360721");
            //// Netease.GetDetail(513360721);

            ////Netease.GetDetailsR(new string[] { "513360721", "1311076476" });
            //mh.GetSongsByIds(new string[] { "513360721" });
            //mh.Search("我是", 1);
            #endregion 
            #region qq test
            // MusicHelper qq = new QQ();
            //string re = QQ.GetSongByIdR("002B2EAA3brD5b");
            // QQ.GetSongByIdR("002B2EAA");
            //QQ.GetMusicInfo(new string[] { "002B2EAA3brD5b", "002RkFPr1R58Z8" });
            //QQ.GetLrcBySongid("5219940");
            // QQ.GetSongById("002B2EAA3brD5b");
            //QQ.SearchR("五月天", 1);
            //qq.Search("五月天", 2);
            // Console.WriteLine(re);
            //Console.ReadKey();
            #endregion 
            #region qq test2
            //string url1 = "http://dl.stream.qqmusic.qq.com/M800002RkFPr1R58Z8.mp3?vkey=0DF2A36C7EB8D45E426E65B4FE288CC081FD3845296081255CDC6EA547408EFA8692E49CEAA01299012628FA6C8408FA9DF3E1A81754E6D7&guid=5150825362&fromtag=";
            //for (int i = 0; i < 100; i++)
            //{
            //    string url = url1 + i;
            //    WebRequest req = WebRequest.Create(url);
            //    try
            //    {
            //        using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            //        {
            //            Console.WriteLine(i);
            //        }
            //    }
            //    catch 
            //    {

            //        continue;
            //    }
            //}
            //Console.WriteLine("complete!");
            //Console.ReadKey();
            #endregion
            WebClient wc = new WebClient();
            wc.DownloadString("https://y.qq.com/n/yqq/song/000eXvbv2GXLdW.html");
            string cookies = wc.ResponseHeaders[HttpRequestHeader.Cookie];
            string server = wc.ResponseHeaders["server"];
            wc.Dispose();
          string str =   wc.DownloadString("https://y.qq.com/n/yqq/song/000eXvbv2GXLdW.html");
            //CookieContainer cookie = new CookieContainer();

        }
    }
}
