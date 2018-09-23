﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace MusicHelpers.Helpers
{

    //id = songmid;  songid = songid
    public class QQ : MusicHelper
    {
        static string vkey;
        public QQ()
        {
            vkey = GetVkey();
        }
        //MC的方法
        public static string GetVkey()
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.Referer, "http://y.qq.com");
                //还要加一行这个？？醉了，好险试了下
                wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36");
                string url = "http://base.music.qq.com/fcgi-bin/fcg_musicexpress.fcg?";
                //string param = "json=3&guid=5150825362&format=json";
                string param = "guid=5150825362";
                string xmlStr = wc.DownloadString(url + param);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlStr);
                string key = xml["command-lable-xwl78-qq-music"]["cmd"]["info"].Attributes["key"].Value;
                return key;
            }
        }
        //public static string SearchR(string name,int page)
        //{
            
        //}
        public static string GetSongByIdR(string id)
        {
            string[] quality = { "M800", "M500", "C400" };
            for (int i = 0; i < quality.Length; i++)
            {

                //貌似要会员的vkey才能下载高音质？
                //虽然每次的请求中vkey都改变，但经测试，vkey都使用同一个也行
                //不不 vkey 会变的，只是一段时间没变，后来会变的，还是用GetVkey方法吧
                //string url = "http://183.252.54.24/amobile.music.tc.qq.com/" + quality[i] + id + ".mp3?vkey=93E5392730956D2839AFF7F19920427925C340B93F3D722EBCC8D73FF050EDCFC9D50B5D6A3BA122F094BF370B88E2F24097820E09002FF0&guid=5150825362&fromtag=1";
                //string url = "http://dl.stream.qqmusic.qq.com/" + quality[i] + id + ".mp3?vkey=93E5392730956D2839AFF7F19920427925C340B93F3D722EBCC8D73FF050EDCFC9D50B5D6A3BA122F094BF370B88E2F24097820E09002FF0&guid=5150825362&fromtag=1";
                //string url = "http://183.252.54.24/amobile.music.tc.qq.com/";
                string url = "http://dl.stream.qqmusic.qq.com/";
                string param = "vkey="+vkey+"&guid=5150825362&fromtag=1";
                string urlAll = url + quality[i] + id + ".mp3?"+param;
                WebRequest request = WebRequest.Create(urlAll);
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        return urlAll;
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
        /// <summary>
        /// 返回html页面
        /// </summary>
        /// <param name="id">mid</param>
        /// <returns></returns>
        public static string GetMusicInfo(string id)
        {
            using (WebClient wc = new WebClient())
            {
                string url = "https://y.qq.com/n/yqq/song/" + id + ".html";
                return wc.DownloadString(url);
            }
        }
        /// <summary>
        /// 获取歌词
        /// </summary>
        /// <param name="songid">songid(返回的html页面中的参数）</param>
        /// <returns></returns>
        public static string GetLrcBySongid(string songid)
        {
            using (WebClient wc = new WebClient())
            {
                string url = "https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric.fcg?";
                string param = "nobase64=1&musicid=" + songid;
                wc.Headers.Add(HttpRequestHeader.Referer, "https://y.qq.com/n/yqq/song");
                string lrcStr = wc.DownloadString(url + param);
                return lrcStr;
            }
        }
       /// <summary>
       /// 处理GetLrcBySongid获取的lrcStr，得到所需的正确歌词
       /// </summary>
       /// <param name="lrcStr"></param>
       /// <returns></returns>
        public static string GetLrc(string lrcStr)
        {
            Match matLrc = Regex.Match(lrcStr, "lyric\":\"(.+)\"}");
            if (matLrc.Success)
            {
                lrcStr = matLrc.Groups[1].Value;
                lrcStr.Replace("&#13;", "");
                lrcStr.Replace("&#10;", "\n");
                return  HttpUtility.HtmlDecode(lrcStr);
                
            }
            return null;
        }
        public static MusicInfo GetSongById(string id)
        {
            string htmlPage = GetMusicInfo(id);
            Match musicInfoMat = Regex.Match(htmlPage, "var g_SongData = (.+);");
            JObject MIJo = JObject.Parse(musicInfoMat.Groups[1].Value);
            string songid = MIJo["songid"].ToString();
            string authors = "";
            foreach (var author in MIJo["singer"].Children())
            {
                authors += author["name"].ToString() + ",";
            }
            MusicInfo mi = new MusicInfo()
            {
                songid = id,
                pic = "https://y.gtimg.cn/music/photo_new/T002R300x300M000" + MIJo["albummid"].ToString() + ".jpg",
                link = "https://y.qq.com/n/yqq/song/" + id + ".html",
                type = "qq",
                title = MIJo["songtitle"].ToString()+string.Format("({0})",MIJo["songsubtitle"].ToString()),
                author = authors.Substring(0,authors.Length - 1)
            };
            mi.url = GetSongByIdR(id);
            string lrcStr= GetLrcBySongid(songid);
            mi.lrc = GetLrc(lrcStr);
            return mi;
        }

   

        public override MusicInfo[] Search(string name, int page)
        {
            throw new NotImplementedException();
        }

        public override MusicInfo[] GetSongsByIds(string[] ids)
        {
            MusicInfo[] mis = new MusicInfo[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                mis[i] = GetSongById(ids[i]);
            }
            return mis;
        }
    }
}
