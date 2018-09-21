using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using MusicHelpers.Helpers;

namespace CoreMVC.Controllers
{
    public class MusicController : Controller
    {
        public IActionResult Music()
        {
            Dictionary<string, string> musicTypeList = new Dictionary<string, string>();
            musicTypeList.Add("netease", "网易");
            musicTypeList.Add("qq", "ＱＱ");
            musicTypeList.Add("kugou", "酷狗");
            musicTypeList.Add("kuwo", "酷我");
            musicTypeList.Add("xiami", "虾米");
            musicTypeList.Add("baidu", "百度");
            musicTypeList.Add("1ting", "一听");
            musicTypeList.Add("migu", "咪咕");
            musicTypeList.Add("lizhi", "荔枝");
            musicTypeList.Add("qingting", "蜻蜓");
            musicTypeList.Add("ximalaya", "喜马拉雅");
            musicTypeList.Add("kg", "全民K歌");
            musicTypeList.Add("5singyc", "5sing原创");
            musicTypeList.Add("5singfc", "5sing翻唱");
            ViewBag.musicTypeList = musicTypeList;
            return View();
        }
        [HttpPost]
        public IActionResult Music(MusicAjax data)
        {
            //其实有些可以搬到这边来验证的
            if (Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return Music();
            //MusicHelper mh = new Netease();
            //string result = mh.SearchR(data.Input, data.Page);
            //JsonResult info = Json(result);
            MusicInfo[] mis =   MusicHelper.Call(data.Input, data.Type, data.Filter, data.Page);
            //Dictionary<string, MusicInfo[]> dict = new Dictionary<string, MusicInfo[]>();
            //dict.Add("data", mis);
            Data da = new Data() {data = mis, code = 200, error = "" };
            return Json(da);
        }


    }
}