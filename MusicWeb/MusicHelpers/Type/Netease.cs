using MusicHelpers.Helpers;
using MusicHelpers.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MusicHelpers.Type
{
    //最先写的,没用上HttpHelp
    public class Netease : Music
    {
        //参考 https://www.zhanghuanglong.com/detail/csharp-version-of-netease-cloud-music-api-analysis-(with-source-code)
        //d e f g... 皆为网易云js 上的参数名称
        static readonly string  encSecKey = "6957c8a2c94cbe585787e534436dcf29d6b3c6140950dcb05a251fbe5bfd3e8f5386be030295ceb3ffb0b492f69e989fca1ebedb80a82bcd6805f2dd70c28ed4de70705a1ed4949faa6b832aac4c2ab64f15e8369fb1629c5207df037e0c2d66f898ba0bce59cf9f72a1826b39bf534c52a9c54b7fb577365a96b7b13e2e9006";
        static readonly string i = "TOjaAPDYnEmuv9Fb";
        //static string e = "010001";
        //static string f = "00e0b509f6259df8642dbc35662901477df22677ec152b5ff68ace615bb7b725152b3ab17a876aea8a5aa76d2e417629ec4ee341f56135fccf695280104e0312ecbda92557c93870114af6c9d05c4f7f0c3685b7a46bee255932575cce10b424d813cfe4875d3e82047b97ddef52741d546b8e289dc6935b3ece0462db0a22b8e7";
        static readonly string g = "0CoJUm6Qyw8W8jud";

        //public static string GetSongByIdR(int id)
        //{//"http://music.163.com/song/media/outer/url?id="+ id+".mp3"
        //    string url = "https://music.163.com/weapi/song/enhance/player/url"; /*HTTP/1.1*/
        //                                                                                    // string d = "{ \"ids\":\"[492151019,513360721,432509483,544081686,1298219148]\",\"br\":128000,\"csrf_token\":\"\"}";
        //    string d = "{\"ids\":\"[" + id + "]\",\"br\":128000}";
        //    string songJson = Request(url, d);
        //    return songJson;
        //}
        public static string SearchR(string name, int page)
        {
            string url = "https://music.163.com/weapi/cloudsearch/get/web";
            string d = "{\"s\":\"" + name + "\",\"type\":\"1\",\"offset\":\"" + (page - 1) * 10 + "\",\"total\":\"false\",\"limit\":\"10\"}";
            string songsJson = Request(url, d);
            return songsJson;


        }
        public static string GetSongsByIdsR(string[] ids)
        {
            string url = "https://music.163.com/weapi/song/enhance/player/url";
            string build = "";
            for (int i = 0; i < ids.Length - 1; i++)
            {
                build += ids[i] + ",";
            }
            build += ids[ids.Length - 1];
            //音质320000,192000,128000//未登录（的cookie）默认128000
            string d = "{\"ids\":\"[" + build + "]\",\"br\":320000}";
            string songsInfoJson = Request(url, d);

            return songsInfoJson;
        }
        public static string GetLrcR(string id)
        {
            string url = "https://music.163.com/weapi/song/lyric";
            string d = "{\"id\":\"" + id + "\",\"lv\":1}";
            //string d = "{\"id\":\""+id+"\",\"lv\":1,\"tv\":-1,\"csrf_token\":\"\"}";
            string lrcInfo = Request(url, d);
            return lrcInfo;
        }
        public static string GetDetailsR(string[] ids)
        {
            string url = "https://music.163.com/weapi/v3/song/detail";
            // string d = "{\"c\":\"[{\\\"id\\\":\\\""+id+"\\\"}]\"}";
            //string d = "{\"c\":\"[{\\\"id\\\":\\\"1311076476\\\"},{\\\"id\\\":\\\"513360721\\\"}]\"}";
            string build = "";
            for (int i = 0; i < ids.Length; i++)
            {
                build += "{\\\"id\\\":\\\"" + ids[i] + "\\\"},";
            }
            build += "{\\\"id\\\":\\\"" + ids[ids.Length - 1] + "\\\"}";
            string d = "{\"c\":\"[" + build + "]\"}";
            string detail = Request(url, d);
            return detail;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="d">参数的一部分(处理前)</param>
        /// <returns></returns>
        public static string Request(string url, string d)
        {
            using (WebClient wc = new WebClient())
            {    //https://music.163.com/weapi/song/enhance/player/url 请求头 上的cookies
                //用会员登录后的cookie，可以听（及下载）付费的,以及高音质的（感觉应该不会被封，但以防万一，还是不放上来了）
               
                //非会员的(小号)cookie 不能听付费音乐和部分高音质 
                 //wc.Headers.Add(HttpRequestHeader.Cookie, "_iuqxldmzr_=32; _ntes_nnid=cfb20d675b37ef82883687bed10b0f68,1533467251037; _ntes_nuid=cfb20d675b37ef82883687bed10b0f68; WM_TID=TkaWyJQS0UM8cqq7iCxEIqBzpfEF9AMQ; usertrack=ezq0pFuHk1U3p4UQBckbAg==; __f_=1535854971328; vjuids=737ea9cc5.1659fcb94f jsessioni8.0.908420a2b47f7; vjlast=1535984244.1535984244.30; vinfo_n_f_l_n3=954b8e5bcd63e6d7.1.0.1535984243979.0.1535984250209; hb_MA-9ADA-91BF1A6C9E06_source=www.google.com; mp_MA-9ADA-91BF1A6C9E06_hubble=%7B%22sessionReferrer%22%3A%20%22https%3A%2F%2Fcampus.163.com%2Fapp%2Fhy%2Flh%22%2C%22updatedTime%22%3A%201536561490090%2C%22sessionStartTime%22%3A%201536560972018%2C%22sendNumClass%22%3A%20%7B%22allNum%22%3A%203%2C%22errSendNum%22%3A%200%7D%2C%22deviceUdid%22%3A%20%220e9c86ef-0338-4c3d-a87f-b67f20309fc8%22%2C%22persistedTime%22%3A%201536560972009%2C%22LASTEVENT%22%3A%20%7B%22eventId%22%3A%20%22da_screen%22%2C%22time%22%3A%201536561490091%7D%2C%22sessionUuid%22%3A%20%228a2f1ffb-4854-4b86-aed8-dfc7ce8a8baf%22%7D; JSESSIONID-WYYY=%5C26JeRzJnXdr6MS39TovmAvR869vqAKafZu1ZH9hZfozdTvccBjshTIdTy0dS3NbcJ9pVwuJrDjYFGAhM7GGZzx%2Br51OIAk8eyc7orjp39hffDKV7aM4tvi5eYEO8YJy5VbifIP2DTW9dBjQIHi4G1tluv4kzrvViMsYMAT9tH2VhM2l%3A1537696218369; WM_NI=yrWHuZoW1g%2FcANgvcm3jqJgrG%2BPXt1fpyfOykUaesXPxX6o3S3NTVtqVqOiIOkuhcIEX%2B5IKV%2F23lwFxuaII%2FIMieCVn24Mt0rGvXdfXfzsa%2BB%2FpC5xryg9S4qyEo8BITnc%3D; WM_NIKE=9ca17ae2e6ffcda170e2e6eed2c64891ad8cb7f144f2bc8aa6d44a939b8b84bc6b928efd9bbc62a9ae86b5b72af0fea7c3b92abbae99a9f852ad89ff88ae698392a997e267f1b6bfabe67a85f5a0b9f73ba6b0b787ce73a98baf86f25494a898d7ee3991ed82b5d580b38abfb0ec438db5bb89e2438788e5d6cc4196f1fed4ef7d86a8bb82cc43ed97838ae84996b2fed5d080f4ae8997db70a688aa9bf26be99ee191c23fbcaeb788bc59f8afb7a6ee3482979c8fe237e2a3; __remember_me=true;MUSIC_U=457f1c671a49ed2bccd53d319c01f8b8f42d0c452979f5610acb7aac9f21921dc3c1140b5735a834269c8ca95be0a91f31b299d667364ed3; __csrf=64110e6ce458515052955efc9357c556");
                //未登录的cookie(貌似和登录有的非会员cookie没有差别）
                wc.Headers.Add(HttpRequestHeader.Cookie, "_iuqxldmzr_=32; _ntes_nnid=cfb20d675b37ef82883687bed10b0f68,1533467251037; _ntes_nuid=cfb20d675b37ef82883687bed10b0f68; WM_TID=TkaWyJQS0UM8cqq7iCxEIqBzpfEF9AMQ; usertrack=ezq0pFuHk1U3p4UQBckbAg==; __f_=1535854971328; vjuids=737ea9cc5.1659fcb94f8.0.908420a2b47f7; vjlast=1535984244.1535984244.30; vinfo_n_f_l_n3=954b8e5bcd63e6d7.1.0.1535984243979.0.1535984250209; hb_MA-9ADA-91BF1A6C9E06_source=www.google.com; mp_MA-9ADA-91BF1A6C9E06_hubble=%7B%22sessionReferrer%22%3A%20%22https%3A%2F%2Fcampus.163.com%2Fapp%2Fhy%2Flh%22%2C%22updatedTime%22%3A%201536561490090%2C%22sessionStartTime%22%3A%201536560972018%2C%22sendNumClass%22%3A%20%7B%22allNum%22%3A%203%2C%22errSendNum%22%3A%200%7D%2C%22deviceUdid%22%3A%20%220e9c86ef-0338-4c3d-a87f-b67f20309fc8%22%2C%22persistedTime%22%3A%201536560972009%2C%22LASTEVENT%22%3A%20%7B%22eventId%22%3A%20%22da_screen%22%2C%22time%22%3A%201536561490091%7D%2C%22sessionUuid%22%3A%20%228a2f1ffb-4854-4b86-aed8-dfc7ce8a8baf%22%7D; JSESSIONID-WYYY=cha43%2FRsDSXxlmmRXHImTjszy%5C0d9A6GspWI523%5CfNYMXp2p2sIHCd3XUzcvJfPeTA2bvbNskphV8jNt%2Bf7u5HThbbBT5O2aRuvQ0Qq%2FOYf9J00Vxu4uAFYpbkO%5CQNcxs9X%2BEMSwvbjErSSxEWmK7iT0CQyFBj%5C68n4k1ZlW6JZ1P%5CBp%3A1537349540539; WM_NI=t68UQyVGA1HnMmYeOy5xEFAEsZqBCjWXLH33l3NVuYkSLTkeghn6u%2Bf5lwEw1pK0Kw6JIkfhV2U7sDqUj2k2AvmusKEvIZUQXvaXGK9SBZpvknDpQiV8MRzyX3VwMONtalE%3D; WM_NIKE=9ca17ae2e6ffcda170e2e6eeb7fb5b919dfea6ec3bb1adaf92d54d8fa69789ca53f78781a3b421a1afbc91ec2af0fea7c3b92ab8eaf8b6d47396998fd2cb668fb39786ce73f7a88ba7c44fb186bfa3c262958884a9b239b0ed82b7f853a98d84b5ea6fa5b8a2d7f821a6eba498c87df3b085d1ca7df696878ce56496ee9bd8f250b38ba6d3ea3a83aaa396f24187aa87bbd85b9caaafb9c641fb8d8ebbf37ea9b28c91d46396e883b3f9739ced9a98d0458eacaf8dc837e2a3");
                //wc.Headers.Add(HttpRequestHeader.Referer, "https://music.163.com/");
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                //wc.Headers.Add(HttpRequestHeader.ContentLength, "412");
                //wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
                //wc.Headers.Add("Origin", "https://music.163.com");
                //wc.Headers.Add(HttpRequestHeader.KeepAlive, "True");
                string h = NeteaseEncryptHelper.AesEncrypt(d, g);
                string param = NeteaseEncryptHelper.AesEncrypt(h, i);
                param = HttpUtility.UrlEncode(param);
                string myParameters = "params=" + param + "&" + "encSecKey=" + encSecKey;
                //myParameters = "params=ovej7TkHiTCtM7raDtVCmxJXontP%2FynUonCQZrN6SeJ%2BXm%2F%2FPgORcE4KPkLqfHTSntkCpUoEmTQf1VHwqUOA4H0zHP0C9hJvQODqNPFvwVpVYZdCyhS5OrbP89JBP5AtsocRUybxQk%2F5MzLKsU%2F5iQ%3D%3D&encSecKey=89356a0b76edf4d371b8d7d324b41364de5737cdb4d0ad42454fddfaa7b5ba3e71c556b25c0f51088431ba5934929b4f38b89e4ad362389f0e2b3151b43a477682edde022ba38dcbe2cf2f38065fdf0b468b07d9f0661b6d836f34b6416e67515c67f9438bb519cfc3cc7f55e86669e882aa43d8aacf28de50eef17b2586fcc9";
                string htmlResult = wc.UploadString(url, myParameters);
                return htmlResult;
            }
        }

        public override MusicInfo[] Search(string name, int page)
        {
            string songsJson = SearchR(name, page);
            JObject songsJo = JObject.Parse(songsJson);
            if (songsJo["result"]["songCount"].ToString().Equals("0"))
            {
                return null;
            }
            IList<JToken> songs = songsJo["result"]["songs"].Children().ToList();
            List<string> ids = new List<string>();
            foreach (JToken song in songs)
            {
                ids.Add(song["id"].ToString());
            }
            return GetSongsByIds(ids.ToArray());
        }

        public override MusicInfo[] GetSongsByIds(string[] ids)
        {   
            JObject jo;
            Dictionary<string, string> lrcDict = new Dictionary<string, string>();
            List<Task> taskList = new List<Task>();
            foreach (string id in ids)
            {
                taskList.Add(Task.Run(() =>
                {
                    string lrcJson = GetLrcR(id);
                    jo = JObject.Parse(lrcJson);
                    try
                    {
                        lrcDict.Add(id, jo["lrc"]["lyric"].ToString());
                    }
                    catch
                    {

                        lrcDict.Add(id, "");
                    }
                }));
            }
            Dictionary<string, MusicInfo> misDict = new Dictionary<string, MusicInfo>();
            string detailsJson = GetDetailsR(ids);
            jo = JObject.Parse(detailsJson);
            if (jo["code"].ToString().Equals("400"))
            {
                return null;
            }
            foreach (var detail in jo["songs"].Children())
            {
                string id = detail["id"].ToString();
                misDict.Add(id, new MusicInfo()
                {
                    type = "netease",
                    link = "https://music.163.com/#/song?id=" + id,
                    title = detail["name"].ToString(),
                    songid = id,
                    //pic = detail["picUrl"].ToString(),
                });
                misDict[id].pic = detail["al"]["picUrl"].ToString() + "?param=300x300";
                string authors = "";
                foreach (var author in detail["ar"].Children())
                {
                    authors += author["name"].ToString() + ",";
                }
                // authors = authors.Substring(0, authors.Length - 1);
                misDict[id].author = authors.Substring(0, authors.Length - 1);
            }
            string songsJson = GetSongsByIdsR(ids);
            //{0}为null时的检测
            jo = JObject.Parse(songsJson);
            foreach (var data in jo["data"].Children())
            {
                if (data["url"].ToString() == "")
                {
                    return null;
                }
                misDict[data["id"].ToString()].url = data["url"].ToString();
            }
            Task.WaitAll(taskList.ToArray());
            foreach (string id in ids)
            {
                misDict[id].lrc = lrcDict[id];
            }

            //MusicInfo[] musicInfo = new MusicInfo[ids.Length];
            //int i = 0;
            //foreach (MusicInfo mi in misDict.Values)
            //{
            //    musicInfo[i++] = mi; 
            //}
            return misDict.Values.ToArray();
        }

        public override MusicInfo[] GetSongById(string id)
        {
            if (Regex.IsMatch(id,"^\\d+$"))
            {
            return GetSongsByIds(new string[] { id });
            }
            return null;
        }
    }
}
