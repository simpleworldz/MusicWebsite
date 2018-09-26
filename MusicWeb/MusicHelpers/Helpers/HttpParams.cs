using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHelpers.Helpers
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
        public string Url { get; set; }
        public string Data { get; set; } = "";
        //public string Method { get; set; }
        //public string ContentType { get; set; }
        public string Agent { get; set; } = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";
        public string Referer { get; set; } 
    }
}
