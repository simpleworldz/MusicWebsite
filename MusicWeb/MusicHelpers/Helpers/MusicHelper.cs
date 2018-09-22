using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicHelpers.Helpers
{
    public abstract class MusicHelper
    {
        static MusicHelper mh;
        public abstract MusicInfo[] Search(string name, int page) ;
        public abstract MusicInfo[] GetSongById(string id);
        public static MusicInfo[] GetSongByUrl(string uri)
        {//https://music.163.com/#/song?id=223779
            Match netease = Regex.Match(uri, "\\S+163.com\\S+id=(\\d+)");
            if(netease.Length != 0)
            {
                return Call(netease.Groups[1].Value, "netease", "id");
            }
            return null;
        }
        public abstract MusicInfo[] GetSongsByIds(string[] ids);
        public static MusicInfo[] Call(string input,string type, string filter, int page = 1)
        {
            if (filter == "url")
            {
                return GetSongByUrl(input);
            }
            switch(type)
            {
                case "netease":
                    mh = new Netease();
                    break;
            }
            switch(filter)
            {
                case "name":
                    return mh.Search(input, page);
                case "id":
                    return mh.GetSongsByIds(new string[] { input });
            }
            return null;
        }
    }
}
