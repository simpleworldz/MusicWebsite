using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using MusicHelpers.Helpers;
using MusicHelpers.Model;
using Newtonsoft.Json.Linq;

namespace MusicHelpers.Type
{
    public class Xiami : Music, IMusicRAsync
    {
        static List<MusicInfo> mis;
        public Xiami()
        {
            //HttpHelper.cookie.SetCookies(new Uri("http://api.xiami.com"), "user_from=2;XMPLAYER_addSongsToggler=0;XMPLAYER_isOpen=0;_xiamitoken=cb8bfadfe130abdbf5e2282c30f0b39a");
            mis = new List<MusicInfo>();
        }
        public async Task<string> GetLrcRAsync(string url)
        {
            HttpParams hp = new HttpParams()
            {
                Url = url.Contains("http")?url:"http:" + url
            };
            string lrcStr =  await HttpHelper.GetAsync(hp, "");
            return   Regex.Replace(lrcStr, "<\\d+>", "");
        }

        public override MusicInfo[] GetSongById(string id)
        {
            string res = GetSongByIdRAsync(id).Result;
            JObject jo = JObject.Parse(res);
            //JToken info = jo["data"]["trackList"];
            //mis.Add(GetSongInfo(info));
            foreach (var song in jo["data"]["trackList"])
            {
                string lrcStr = song["lyric"].Value<string>();
                Task<string> getLrc = null;
                if (!lrcStr.Equals(""))
                {
                    getLrc = GetLrcRAsync(lrcStr);
                }
                mis.Add(
                    new MusicInfo()
                    {//还是用.Value<string>()吧
                        type = "xiami",
                        link = "http://www.xiami.com/song/" + id,
                        songid = id,
                        title = song["songName"].Value<string>(),
                        author = song["singers"].Value<string>(),
                        url = HigherQ(XiamiUrlDecode(song["location"].Value<string>())),
                        pic = song["album_pic"].Value<string>(),
                        lrc = getLrc != null ? getLrc.Result : ""
                    });
            }
            return mis.Count > 0? mis.ToArray():null;
        }

        public async Task<string> GetSongByIdRAsync(string id)
        {
            #region 方法一 
            HttpParams hp = new HttpParams()
            {
                Url = "http://www.xiami.com/song/playlist/id/" + id + "/type/0/cat/json",
                Referer = "http://www.xiami.com"
            };
            return await HttpHelper.GetAsync(hp);
            #endregion
            #region 方法二 （有时没用）
            //HttpParams hp = new HttpParams()
            //{
            //    Url = "http://api.xiami.com/web",
            //    Referer = "http://h.xiami.com/",
            //    Data = "v=2.0&app_key=1&r=song/detail&id=" + id,
            //    Agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.75 Safari/537.36"
            //};
            //return await HttpHelper.GetAsync(hp);
            #endregion
        }
        public string HigherQ(string url)
        {
            //_l , _h
            if (url.Contains("_l."))
            {
                //string url320 = url.Replace("m128", "m320").Replace("_l.", "_h.");
                //HttpParams hp = new HttpParams()
                //{
                //    Url = url320
                //};
                //if (HttpHelper.TeskAsync(hp).Result)
                //{
                //    return url320;
                //}
                //string url192 = url.Replace("m128", "m192");
                //hp.Url = url192;
                //if(HttpHelper.TeskAsync(hp).Result)
                //{
                //    return url192;
                //}
                return url;
            }
            else
            {
                return url.Replace("m128", "m320");
            }
        }
        public MusicInfo GetSongInfo(JToken info)
        {
            // List<MusicInfo> mis = new List<MusicInfo>();
            //JObject jo = JObject.Parse(res);
            //var ss = jo["data"][key];
            //foreach (var info in jo["data"][key])
            //{
            string lrcStr = info["lyric"].Value<string>();
            Task<string> getLrc = null; 
            if (!lrcStr.Equals(""))
            {
                getLrc = GetLrcRAsync(lrcStr);
            }
            return new MusicInfo()
            {
                type = "xiami",
                link = "http://www.xiami.com/song/" + info["song_id"].ToString(),
                songid = info["song_id"].ToString(),
                title = info["song_name"].ToString(),
                author = info["artist_name"].ToString(),
                url = HigherQ(info["listen_file"].ToString()),
                pic = info["album_logo"].ToString(),
                //pic = info["album_logo"].ToString(),
                lrc = getLrc != null ? getLrc.Result : ""
            };
            //}
           // return mis.ToArray();
        }
        public override MusicInfo[] GetSongsByIds(string[] ids)
        {
            throw new NotImplementedException();
        }

        public override MusicInfo[] Search(string name, int page)
        {
            string res = SearchRAsync(name, page).Result;
            JObject jo = JObject.Parse(res);
            foreach (JToken info in jo["data"]["songs"])
            {
                mis.Add(GetSongInfo(info));
            }
            return mis.Count > 0 ? mis.ToArray() : null;
        }

        public async Task<string> SearchRAsync(string name, int page)
        {
            #region 方法一
            HttpParams hp = new HttpParams()
            {
                Url = "http://api.xiami.com/web",
                Referer = "http://h.xiami.com/",
                Data = "key=" + HttpUtility.UrlEncode(name) + "&v=2.0&app_key=1&r=search/songs&page=" + page + "&limit=10",
                Agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.75 Safari/537.36"
            };
            return await HttpHelper.GetAsync(hp);
            #endregion
            #region 方法二失败（指令[cookie]会过期）
            //HttpParams hp = new HttpParams()
            //{
            //    Url = "https://h5api.m.xiami.com/h5/mtop.alimusic.search.searchservice.searchsongs/1.0/",
            //    //Referer = "http://m.xiami.com",
            //    Data = "appKey=12574478&t=1538141651547&sign=2e0492c39e681e4c15ab504fd628d0d6&v=1.0&type=originaljson&dataType=json&api=mtop.alimusic.search.searchservice.searchsongs&data=%7B%22requestStr%22%3A%22%7B%5C%22header%5C%22%3A%7B%5C%22platformId%5C%22%3A%5C%22win%5C%22%2C%5C%22remoteIp%5C%22%3A%5C%22192.168.1.106%5C%22%2C%5C%22callId%5C%22%3A1538141651539%2C%5C%22sign%5C%22%3A%5C%22872e314826a9afefd3227ed492f7e3af%5C%22%2C%5C%22appId%5C%22%3A200%2C%5C%22deviceId%5C%22%3A%5C%225fd4d48586fc6093bbb8558cf949a43b2eb643d1e5b50f7a9ef1f8408a459cb8%5C%22%2C%5C%22network%5C%22%3A1%2C%5C%22appVersion%5C%22%3A3010200%2C%5C%22resolution%5C%22%3A%5C%221178*704%5C%22%2C%5C%22utdid%5C%22%3A%5C%225fd4d48586fc6093bbb8558cf949a43b2eb643d1e5b50f7a9ef1f8408a459cb8%5C%22%7D%2C%5C%22model%5C%22%3A%7B%5C%22key%5C%22%3A%5C%22%E4%BD%A0%E6%98%AF%5C%22%2C%5C%22pagingVO%5C%22%3A%7B%5C%22page%5C%22%3A1%7D%7D%7D%22%7D",
            //    Agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) XIAMI-MUSIC/3.1.2 Chrome/56.0.2924.87 Electron/1.6.11 Safari/537.36",
            //};
            //return await HttpHelper.GetAsync(hp);
            #endregion
            #region 方法三 
            //    HttpParams hp = new HttpParams()
            //{
            //    Url = "https://www.xiami.com/search",
            //    Data = "key=" + HttpUtility.UrlEncode(name) + "&pos=" + page,
            //};
            //return await HttpHelper.GetAsync(hp);
            #endregion
        }
        public static string XiamiUrlDecode(string str)
        {
            int rows = str[0] - '0';
            var url = str.Substring(1);
            var len_url = url.Length;
            var cols = len_url / rows;
            var re_col = len_url % rows;
            StringBuilder ret_url = new StringBuilder();

            for (int i = 0; i < len_url; i++)
            {
                int index = (i % rows) * (cols + 1) + (i / rows);
                if ((i % rows) >= re_col)
                {
                    index -= (i % rows) - re_col;
                }
                if (index >= url.Length)
                    ret_url.Append('-');
                else
                    ret_url.Append(url[index]);
            }
            var ret = ret_url.ToString();
            ret = Uri.UnescapeDataString(ret);
            ret = ret.Replace("^", "0");
            if (!ret.StartsWith("http:"))
                ret = "http:" + ret;
            return ret;
        }

        public Task<string> GetSongsByIdsRAsync(string[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
