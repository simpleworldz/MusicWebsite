using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelpers.Helpers
{
    public class NeteaseEncryptHelper
    {
        public static string AesEncrypt(string plaintextData, string key, string iv = "0102030405060708")
        {
            try
            {
                if (string.IsNullOrEmpty(plaintextData)) return null;
                Byte[] toEncryptArray = Encoding.UTF8.GetBytes(plaintextData);
                RijndaelManaged rm = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    IV = Encoding.UTF8.GetBytes(iv),
                    Mode = CipherMode.CBC
                };
                ICryptoTransform cTransform = rm.CreateEncryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch /*(Exception ex)*/
            {
                return "";
            }
        }
    }
}
