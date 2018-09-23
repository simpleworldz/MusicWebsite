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
       // public abstract MusicInfo GetSongById(string id);
        public abstract MusicInfo[] GetSongsByIds(string[] ids);
        public static MusicInfo[] GetSongByUrl(string uri)
        {//https://music.163.com/#/song?id=223779
            Match netease = Regex.Match(uri, "\\S+163.com\\S+id=(\\d+)");
            if(netease.Success)
            {
                return Call(netease.Groups[1].Value, "netease", "id");
            }
            //https://y.qq.com/n/yqq/song/003SeXGZ1JUtaX.html
            Match qq = Regex.Match(uri, "\\S+qq.com\\S+song/(.+)[.]html");
            if (qq.Success)
            {
                return Call(qq.Groups[1].Value, "qq", "id");
            }
            return null;

        }
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
                case "qq":
                    mh = new QQ();
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
