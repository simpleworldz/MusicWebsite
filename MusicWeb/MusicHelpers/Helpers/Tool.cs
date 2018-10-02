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
            StringBuilder sb = new StringBuilder();
            foreach (var value in jt[key].Children())
            {
                sb.Append(value[cKey].ToString() + connect);
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }
        public static string Merge(string[] ids,string connect = ",")
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ids.Length - 1; i++)
            {
                sb.Append(ids[i] + connect);
            }
          sb.Append(ids[ids.Length - 1]);
            return sb.ToString();
        }
    }
}
