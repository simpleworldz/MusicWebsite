using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MusicHelpers.Helpers;
using MusicHelpers.Model;
using Newtonsoft.Json.Linq;

namespace MusicHelpers.Type
{
    public class Baidu : Music, IMusicRAsync
    {
        public async Task<string> GetLrcRAsync(string url)
        {
            HttpParams hp = new HttpParams()
            {
                Url = url,
                Referer = "http://music.baidu.com/song/"
            };
            return await HttpHelper.GetAsync(hp,"");
        }

        public override MusicInfo[] GetSongById(string id)
        {
            return GetSongsByIds(new string[] { id });
        }

        public Task<string> GetSongByIdRAsync(string id)
        {
            throw new NotImplementedException();
        }

        public override MusicInfo[] GetSongsByIds(string[] ids)
        {
            string res = GetSongsByIdsRAsync(ids).Result;
            List<MusicInfo> mis = new List<MusicInfo>();
            JObject jo = JObject.Parse(res);
            if (jo["data"].ToString().Equals(""))
            {
                return null;
            }
            foreach (var info in jo["data"]["songList"])
            {
                 string lrcLink = info["lrcLink"].Value<string>();
                var getLrc = lrcLink.Equals("")?null:GetLrcRAsync(lrcLink);
                mis.Add(new MusicInfo()
                {
                    type = "kugou",
                    link = "http://www.kugou.com/song/#hash=" + info["songId"].Value<string>(),
                    songid = info["songId"].Value<string>(),
                    title = info["songName"].Value<string>(),
                    author = info["artistName"].Value<string>(),
                    url = info["songLink"].Value<string>(),
                    pic = info["songPicBig"].Value<string>(),
                    lrc =getLrc == null?"":getLrc.Result
                });
            }
            return mis.ToArray();
        }

        public async Task<string> GetSongsByIdsRAsync(string[] ids)
        {
            string idsStr = Tool.Merge(ids); 
            HttpParams hp = new HttpParams()
            {
                Url = "http://music.baidu.com/data/music/links",
                Referer = "music.baidu.com/song/",
                Data = "songIds=["+idsStr+"]"
            };
            return await HttpHelper.GetAsync(hp);
        }

        public override MusicInfo[] Search(string name, int page)
        {
            string res = SearchRAsync(name, page).Result;
            JObject jo = JObject.Parse(res);
            List<string> ids = new List<string>();
            foreach (var info in jo["song_list"])
            {
                ids.Add(info["song_id"].Value<string>());
            }
            return GetSongsByIds(ids.ToArray());
        }

        public async Task<string> SearchRAsync(string name, int page)
        {
            HttpParams hp = new HttpParams()
            {
                Url = "http://musicapi.qianqian.com/v1/restserver/ting",
                Referer = "http://music.baidu.com/",
                Data = "method=baidu.ting.search.common&query=" + HttpUtility.UrlEncode(name) + "&format=json&page_size=10&page_no=" + page
            };
            return await HttpHelper.GetAsync(hp);
        }
    }
}
