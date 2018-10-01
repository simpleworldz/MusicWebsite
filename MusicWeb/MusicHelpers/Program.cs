using MusicHelpers.Helpers;
using MusicHelpers.Model;
using MusicHelpers.Type;
using System;
using System.Net;

namespace MusicHelpers
{
    class Program
    {
        static void Main(string[] args)
        {
            Music mh;
            #region netease test
            //MusicHelper mh = new Netease();
            //mh.GetSongById("021");
            //mh.Search("我是", 1);
            //Netease.GetLrcR("513360721");
            //// Netease.GetDetail(513360721);

            ////Netease.GetDetailsR(new string[] { "513360721", "1311076476" });
            //mh.GetSongsByIds(new string[] { "513360721" });
            //mh.Search("我是", 1);
            #endregion
            #region qq test
            //MusicHelper qq;
            //string re = QQ.GetSongByIdR("002B2EAA3brD5b");
            // QQ.GetSongByIdR("002B2EAA3brD5b");
            //QQ.GetMusicInfo(new string[] { "002B2EAA3brD5b", "002RkFPr1R58Z8" });
            //QQ.GetLrcBySongid("5219940");
            //string l=  QQ.GetLrcById("002B2EAA3brD5b");
            //qq = new QQ();
            // qq.GetSongsByIds(new string[] { "002B2EAA3brD5b" });
            //QQ.SearchR("五月天", 1);
            //for (int i = 1; i < 10; i++)
            //{   qq= new QQ();
            //    Console.WriteLine(i);
            // qq.Search("002B2EAA3brD5b", 1);
            //}

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
            #region test
            //  WebClient wc = new WebClient();
            //  wc.DownloadString("https://y.qq.com/n/yqq/song/000eXvbv2GXLdW.html");
            //  string cookies = wc.ResponseHeaders[HttpRequestHeader.Cookie];
            //  string server = wc.ResponseHeaders["server"];
            //  wc.Dispose();
            //string str =   wc.DownloadString("https://y.qq.com/n/yqq/song/000eXvbv2GXLdW.html");
            //CookieContainer cookie = new CookieContainer();
            //HttpParams hp = new HttpParams();
            ////hp.Url = "https://music.163.com/#/song";
            ////hp.Data = "id=492151019";
            //hp.Url = "https://www.baidu.com/";
            //string str = HttpHelper.GetAsync(hp).Result;
            //string str1 = HttpHelper.GetAsync(hp).Result;
            //hp.Url = "https://music.163.com/weapi/song/enhance/player/url";
            //hp.Data = "params=A1keA%2FsfVh9A7X3J0ntclJHHkz2X%2FhVigSxhnlwckMkUd%2BQ84w9XNIi4DUJahtOnwgQ%2BqNQd4eJ5PQ7fR9ToEa3u5BSvcu8eu00n%2B66IHsObhavrzwKf5%2BKVfXpck4qfixcGMokcXpDyi2WpfjZG2w%3D%3D&encSecKey=0c5fa5062c7a95315f52fbd586db8086159e5ac2141b81793ffb48bd55418384a8b6f884ea6ec16fbc55efbbad705c06b66844feb584ead64353ee04abfc5fc36008688bb9102f6eb52ab0248d83222f49a45618c396a6a5cfb2d2e7812caaa70a29a9c2c29441b725cba66818e83a74178153db5048950fe37b2ce73b2687a3";
            //string str = HttpHelper.HttpRequestAsync(hp).Result;
            #endregion
            // mh = new Kugou();
            //mh.GetSongById("08228af3cb404e8a4e7e9871bf543ff6");
            //mh.GetSongById("63414fd91ac2ef376cdd574209a5bf5e");
            // mh.Search("63414fd91ac2ef376cdd574209a5bf5e", 1);
            // string str = Kugou.GetLrcR("08228af3cb404e8a4e7e9871bf543ff6").Result;
            mh = new Xiami();
            mh.Search("211324832", 1);
            //mh.GetSongById("211324832");
           // HttpParams hp = new HttpParams()
           // {
           //     Url = "http://img.xiami.net/lyric/48/2113248_1528191767_9549.lrc"
           // };
           //string str =  HttpHelper.GetAsync(hp, "").Result;
        }
    }
}
