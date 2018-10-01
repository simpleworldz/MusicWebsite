using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using MusicHelpers.Model;

namespace MusicHelpers.Helpers
{
    public class HttpHelper
    {
       public  static CookieContainer cookie = new CookieContainer();
       public static async Task<string> PostAsync(HttpParams hp)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hp.Url);
           // request.Headers.Add(HttpRequestHeader.Cookie, "_iuqxldmzr_=32; _ntes_nnid=cfb20d675b37ef82883687bed10b0f68,1533467251037; _ntes_nuid=cfb20d675b37ef82883687bed10b0f68; WM_TID=TkaWyJQS0UM8cqq7iCxEIqBzpfEF9AMQ; usertrack=ezq0pFuHk1U3p4UQBckbAg==; __f_=1535854971328; vjuids=737ea9cc5.1659fcb94f jsessioni8.0.908420a2b47f7; vjlast=1535984244.1535984244.30; vinfo_n_f_l_n3=954b8e5bcd63e6d7.1.0.1535984243979.0.1535984250209; hb_MA-9ADA-91BF1A6C9E06_source=www.google.com; mp_MA-9ADA-91BF1A6C9E06_hubble=%7B%22sessionReferrer%22%3A%20%22https%3A%2F%2Fcampus.163.com%2Fapp%2Fhy%2Flh%22%2C%22updatedTime%22%3A%201536561490090%2C%22sessionStartTime%22%3A%201536560972018%2C%22sendNumClass%22%3A%20%7B%22allNum%22%3A%203%2C%22errSendNum%22%3A%200%7D%2C%22deviceUdid%22%3A%20%220e9c86ef-0338-4c3d-a87f-b67f20309fc8%22%2C%22persistedTime%22%3A%201536560972009%2C%22LASTEVENT%22%3A%20%7B%22eventId%22%3A%20%22da_screen%22%2C%22time%22%3A%201536561490091%7D%2C%22sessionUuid%22%3A%20%228a2f1ffb-4854-4b86-aed8-dfc7ce8a8baf%22%7D; JSESSIONID-WYYY=%5C26JeRzJnXdr6MS39TovmAvR869vqAKafZu1ZH9hZfozdTvccBjshTIdTy0dS3NbcJ9pVwuJrDjYFGAhM7GGZzx%2Br51OIAk8eyc7orjp39hffDKV7aM4tvi5eYEO8YJy5VbifIP2DTW9dBjQIHi4G1tluv4kzrvViMsYMAT9tH2VhM2l%3A1537696218369; WM_NI=yrWHuZoW1g%2FcANgvcm3jqJgrG%2BPXt1fpyfOykUaesXPxX6o3S3NTVtqVqOiIOkuhcIEX%2B5IKV%2F23lwFxuaII%2FIMieCVn24Mt0rGvXdfXfzsa%2BB%2FpC5xryg9S4qyEo8BITnc%3D; WM_NIKE=9ca17ae2e6ffcda170e2e6eed2c64891ad8cb7f144f2bc8aa6d44a939b8b84bc6b928efd9bbc62a9ae86b5b72af0fea7c3b92abbae99a9f852ad89ff88ae698392a997e267f1b6bfabe67a85f5a0b9f73ba6b0b787ce73a98baf86f25494a898d7ee3991ed82b5d580b38abfb0ec438db5bb89e2438788e5d6cc4196f1fed4ef7d86a8bb82cc43ed97838ae84996b2fed5d080f4ae8997db70a688aa9bf26be99ee191c23fbcaeb788bc59f8afb7a6ee3482979c8fe237e2a3; __remember_me=true;MUSIC_U=457f1c671a49ed2bccd53d319c01f8b8f42d0c452979f5610acb7aac9f21921dc3c1140b5735a834269c8ca95be0a91f31b299d667364ed3; __csrf=64110e6ce458515052955efc9357c556");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = hp.Referer;
            request.UserAgent = hp.Agent;
            request.CookieContainer = cookie;
            //要吗 不能直接这样，会把 = 一并给encode了
            //string data = HttpUtility.UrlEncode(hp.Data); 
            byte[] daby = Encoding.ASCII.GetBytes(hp.Data);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(daby, 0, daby.Length);
            }
            HttpWebResponse response =(HttpWebResponse) await request.GetResponseAsync();
            //保存cookie
            response.Cookies = cookie.GetCookies(response.ResponseUri);
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
            //Stream myResposeStream = response.GetResponseStream();
            //StreamReader myStreamReader = new StreamReader(myResposeStream);
            //string responseStr = myStreamReader.ReadToEnd();
            //myStreamReader.Close();
            //myResposeStream.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="connect">连接字符 ？ or #</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(HttpParams hp,string connect = "?")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hp.Url + connect + hp.Data);
            request.Method = "GET";
            request.ContentType = "text/html; charset=utf-8";
            request.Referer = hp.Referer;
            request.UserAgent = hp.Agent;
            //request.Headers.Add(HttpRequestHeader.Cookie, "user_from=2;XMPLAYER_addSongsToggler=0;XMPLAYER_isOpen=0;_xiamitoken=cb8bfadfe130abdbf5e2282c30f0b39a");
            // request.Timeout = 15000;
            request.CookieContainer = cookie;
            HttpWebResponse response =(HttpWebResponse) await request.GetResponseAsync();
            response.Cookies = cookie.GetCookies(response.ResponseUri);
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
        public static async Task<bool> TeskAsync(HttpParams hp)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hp.Url);
            request.AddRange(1);
            try
            {
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                request.Abort();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
