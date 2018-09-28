using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHelpers.Model
{
   public  class HttpParams
    {
        //public HttpParams(string method)
        // {
        //     switch(method)
        //     {
        //         case "POST":
        //             Method = "POST";
        //             ContentType = "application/x-www-form-urlencoded";
        //             break;
        //         case "GET":
        //             Method = "GET";
        //             ContentType = "text/html;charset=UTF-8";
        //             break;
        //     }
        // }

        //public HttpParams(string url)
        //{
        //    Url = url;
        //}
        public string Url { get; set; }
        public string Data { get; set; } = "";
        //public string Method { get; set; }
        //public string ContentType { get; set; }
        public string Agent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
        public string Referer { get; set; } 
    }
}
