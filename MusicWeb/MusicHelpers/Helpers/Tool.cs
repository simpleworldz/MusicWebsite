using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHelpers.Helpers
{
  public  class Tool
    {
        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <param name="cKey"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        public static string Merge(JToken jt,string key,string cKey,string connect = ",")
        {
            string res = "";
            foreach (var value in jt[key].Children())
            {
                res += value[cKey].ToString() + ",";
            }
            return res.Substring(0, res.Length - 1);
        }
    }
}
