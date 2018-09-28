using MusicHelpers.Helpers;
using MusicHelpers.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicHelpers.Type
{
    class Kugou : Music
    {
        public static async Task<string> GetSongByIdRAsync(string id)
        {
            HttpParams hp = new HttpParams()
            {
                Url = "http://m.kugou.com/app/i/getSongInfo.php",
                Referer = "http://m.kugou.com/play/info/" + id,
                Data = "cmd=playInfo&hash=" + id,
            };
            return await HttpHelper.GetAsync(hp);
        }
        public static async Task<string> SearchRAsync(string name, int page)
        {
            HttpParams hp = new HttpParams()
            {
                //Url = "http://mobilecdn.kugou.com/api/v3/search/song",
                //Referer = "http://m.kugou.com/v2/static/html/search.html",
                //Data = "keyword=" + name + "&format=json&page=" + page + "&pagesize=10",
                Url = "http://songsearch.kugou.com/song_search_v2",
                //Url = "http://songsearch.kugou.com/song_search_v2?format=json&keyword=%E4%BA%94%E6%9C%88%E5%A4%A9&page=1&pagesize=30&userid=-1&clientver=&platform=WebFilter&tag=em&filter=2&iscorrection=1&privilege_filter=0&_=1538038781949",
                Referer = "http://www.kugou.com/yy/html/search.html",
                Data = "platform=WebFilter&page="+page+"&pagesize=10&format=json&keyword=" + HttpUtility.UrlEncode(name),
                //Agent = "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1"
            };
            return await HttpHelper.GetAsync(hp);
        }
        public static async Task<string> GetLrcRAsync(string id)
        {
            HttpParams hp = new HttpParams()
            {
                Url = "http://m.kugou.com/app/i/krc.php",
                Referer = "http://m.kugou.com/play/info/" + id,
                Data = "cmd=100&timelength=999999&hash=" + id,
            };
            return await HttpHelper.GetAsync(hp);
        }
        public override MusicInfo[] GetSongById(string id)
        {
            return GetSongsByIds(new string[] { id });
        }

        public override MusicInfo[] GetSongsByIds(string[] ids)
        {
            List<MusicInfo> mis = new List<MusicInfo>();
            foreach (string id in ids)
            {
                Task<string> songRes = GetSongByIdRAsync(id);
                Task<string> songLrc = GetLrcRAsync(id);
                JObject jo = JObject.Parse(songRes.Result);
                if (!jo["error"].ToString().Equals(""))
                {
                    continue;
                }
                mis.Add(new MusicInfo()
                {
                    url = jo["url"].ToString(),
                    title = jo["songName"].ToString(),
                    author = jo["singerName"].ToString(),
                    link = "http://www.kugou.com/song/#hash=" + id,
                    songid = id,
                    type = "kugou",
                    pic = !jo["imgUrl"].Equals("") ? jo["imgUrl"].ToString().Replace("{size}", "150") : jo["album_img"].ToString().Replace("{size}", "150"),
                    lrc = songLrc.Result
                });
            }
            return mis.Count > 0?mis.ToArray():null;
        }

        public override MusicInfo[] Search(string name, int page)
        {
            string str = SearchRAsync(name, page).Result;
            JObject jo = JObject.Parse(SearchRAsync(name, page).Result);
            var infos = jo["data"]["lists"].Children();
            List<string> ids = new List<string>();
            foreach (var info in infos)
            {
                //{--}SQFileHash,HQFileHash,FileHash
                //ids.Add(info["FileHash"].ToString());
                //没有登录，即使..也..
                ids.Add(info["HQFileHash"] == null || info["HQFileHash"].ToString().Equals("") ? info["FileHash"].ToString() : info["HQFileHash"].ToString());
            } 
            return GetSongsByIds(ids.ToArray());
        }
    }
}
