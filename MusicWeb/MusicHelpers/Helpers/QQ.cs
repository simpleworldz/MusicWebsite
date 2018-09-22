using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace MusicHelpers.Helpers
{
    
    //id = songmid;  songid = songid
    public class QQ : MusicHelper
    {
        static string cookie;
        public static string GetSongByIdR(string id)
        {
            string[] quality = { "M800", "M500", "C400" };
            for (int i = 0; i < quality.Length; i++)
            {

                //貌似要会员的vkey才能下载高音质？
                //虽然每次的请求中vkey都改变，但经测试，vkey都使用同一个也行
                string url = "http://dl.stream.qqmusic.qq.com/" + quality[i] + id + ".mp3?vkey=93E5392730956D2839AFF7F19920427925C340B93F3D722EBCC8D73FF050EDCFC9D50B5D6A3BA122F094BF370B88E2F24097820E09002FF0&guid=5150825362&fromtag=1";
                WebRequest request = WebRequest.Create(url);
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        return url;
                    }
                }
                catch
                {
                    continue;
                }

            }
            return "error";
        }
        #region test1
        //这个可以多个一起  然而是mv的
        //public static string GetMusicInfo(string[] ids)
        //    {
        //        using (WebClient wc = new WebClient())
        //        {
        //            string url = "https://u.y.qq.com/cgi-bin/musicu.fcg";
        //            //这个"=" ...
        //            //string param = "{\"mv\":{\"module\":\"MvService.MvInfoProServer\",\"method\":\"GetMvBySongid\",\"param\":{\"mids\":[\"002RkFPr1R58Z8\"]}}}";
        //            string build = "";
        //            for (int i = 0; i < ids.Length-1; i++)
        //            {
        //                build += "\""+ids[i]+"\"" + ",";
        //            }
        //            build += "\""+ids[ids.Length - 1]+"\"";
        //            string param = "{\"mv\":{\"module\":\"MvService.MvInfoProServer\",\"method\":\"GetMvBySongid\",\"param\":{\"mids\":["+build+"]}}}";
        //            param = HttpUtility.UrlEncode(param);
        //           //param = "data=%7B%22mv%22%3A%7B%22module%22%3A%22MvService.MvInfoProServer%22%2C%22method%22%3A%22GetMvBySongid%22%2C%22param%22%3A%7B%22mids%22%3A%5B%22002RkFPr1R58Z8%22%5D%7D%7D%7D";
        //            string result =  wc.DownloadString(url+"?data="+param);
        //            return result;
        //        }
        //    }
        #endregion
        #region test2
        //public static string GetMusicInfo(string id)
        //{
        //    using (WebClient wc = new WebClient())
        //    { string url = "https://y.qq.com/n/yqq/song/" + id + ".html";
        //        return wc.DownloadString(url);
        //    }
        //}
        #endregion
        public static string GetLrcBySongid(string songid)
        {
            using (WebClient wc = new WebClient())
            {
                string url = "https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric.fcg?";
                string param = "nobase64=1&musicid=5219940&callback=jsonp1&g_tk=1866593257&jsonpCallback=jsonp1&loginUin=932266563&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
                string lrcStr = wc.DownloadString(url + param);
                return lrcStr;
            }
        }
        public override MusicInfo[] GetSongById(string id)
        {
            throw new NotImplementedException();
        }

        public override MusicInfo[] GetSongsByIds(string[] ids)
        {
            throw new NotImplementedException();
        }

        public override MusicInfo[] Search(string name, int page)
        {
            throw new NotImplementedException();
        }
    }
}
