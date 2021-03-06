﻿using MusicHelpers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicHelpers.Type
{
    public abstract class Music
    {
        static Music mh;
        public abstract MusicInfo[] Search(string name, int page) ;
        //这虽然id只有一个，但还是转为数组
        public abstract MusicInfo[] GetSongById(string id);
        public abstract MusicInfo[] GetSongsByIds(string[] ids);
        //public abstract string SearchRAsync(string name, int page);
        public static MusicInfo[] GetSongByUrl(string url)
        {//https://music.163.com/#/song?id=223779
            Match netease = Regex.Match(url, "\\S+163.com\\S+id=(\\d+)");
            if(netease.Success)
            {
                return Call(netease.Groups[1].Value, "netease", "id");
            }
            //https://y.qq.com/n/yqq/song/003SeXGZ1JUtaX.html
            Match qq = Regex.Match(url, "\\S+qq.com\\S+song/(.+)[.]html");
            if (qq.Success)
            {
                return Call(qq.Groups[1].Value, "qq", "id");
            }
            Match kugou = Regex.Match(url, "\\S+kugou.com\\S+song/#hash=(\\w+)");
            if (kugou.Success)
            {
                return Call(kugou.Groups[1].Value, "kugou", "id");
            }
            Match xiami = Regex.Match(url, "\\S+xiami.com\\S+/(\\d{5,})\\S*");
            if (xiami.Success)
            {
                return Call(xiami.Groups[1].Value, "xiami", "id");
            }
            http://music.taihe.com/song/751126?pst=sug
            Match baidu1 = Regex.Match(url, "\\S+taihe.com\\S+/(\\d+)\\S*");
            if (baidu1.Success)
            {
                return Call(baidu1.Groups[1].Value, "baidu", "id");
            }
            Match baidu2 = Regex.Match(url, "\\S+baidu.com\\S+/(\\d+)\\S*");
            if (baidu2.Success)
            {
                return Call(baidu2.Groups[1].Value, "baidu", "id");
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
                case "kugou":
                    mh = new Kugou();
                    break;
                case "xiami":
                    mh = new Xiami();
                    break;
                case "baidu":
                    mh = new Baidu();
                    break;
            }
            switch(filter)
            {
                case "name":
                    return mh.Search(input, page);
                case "id":
                    return mh.GetSongById(input);
            }
            return null;
        }
    }
}
