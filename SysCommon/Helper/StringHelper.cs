using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Sys.Common.Helper
{ 
 /// <summary>
 /// 字符串处理助手
 /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 将指定类型的对象转换成JSON格式字符串
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="obj">待转换的对象</param>
        /// <returns></returns>
        public static String ToJson<T>(T obj)
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(obj, iso);
        }

        /// <summary>
        /// 将JSON格式字符串转换成指定类型的对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="json">待转换的JSON格式字符串</param>
        /// <returns></returns>
        public static T FromJson<T>(String json)
        {
            return (T)JsonConvert.DeserializeObject(json, typeof(T));
        }

        /// <summary>
        /// 获得（iLength）位随机数
        /// </summary>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string GetRandom(int iLength)
        {
            int iRV = 1;
            for (int i = 1; i < iLength - 1; i++)
            {
                iRV *= 10;
            }
            Random rd = new Random();
            return rd.Next(iRV, iRV * 10).ToString();
        }
        /// <summary>
        /// 获得14位时间字符+(iLength)位的时间随机数
        /// </summary>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string GetDateRandom(int iLength)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + GetRandom(iLength);
        }

        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Match m = null;
            //r = new Regex(@"(\r\n)",RegexOptions.IgnoreCase);
            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }
        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);

        /// <summary>
        /// 清除给定字符串中的HTML字符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearHTML(string str)
        {
            return Regex.Replace(str, "<.+?>", "", RegexOptions.Singleline);
        }
        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        /// 返回 URL 字符串的UTF-8编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的指定sEncoding编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="sEncoding">编码名</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str, string sEncoding)
        {
            return HttpUtility.UrlEncode(str, Encoding.GetEncoding(sEncoding));
        }
    }
}
