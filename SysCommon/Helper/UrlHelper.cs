﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Common.Helper
{
    /// <summary>
    /// URL常用方法
    /// </summary>
    public class UrlHelper
    {
        private static string _encoding = "UTF-8";
        /// <summary>
        /// 设置默认编码
        /// </summary>
        /// <param name="sEncoding">默认编码：UTF-8</param>
        public static void SetEncoding(string sEncoding)
        {
            UrlHelper._encoding = sEncoding;
        }

        private static string _referer = "";
        /// <summary>
        /// 设置申请访问源
        /// </summary>
        /// <param name="sReferer">访问地址</param>
        public static void SetReferer(string sReferer)
        {
            UrlHelper._referer = sReferer;
        }


        /// <summary>
        /// 读取指定Url的Html源代码
        /// </summary>
        /// <param name="sUrl">指定Url</param>
        /// <param name="sEncoding">指定读取编码</param>
        /// <returns></returns>
        public static string GetUrlHtml(string sUrl)
        {
            return GetUrlHtml(sUrl, _encoding);
        }

        /// <summary>
        /// 读取指定Url的Html源代码
        /// </summary>
        /// <param name="sUrl">指定Url</param>
        /// <param name="sEncoding">指定读取编码</param>
        /// <returns></returns>
        public static string GetUrlHtml(string sUrl, string encoding)
        {
            Console.WriteLine("      " + sUrl);
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(sUrl);
            Req.Referer = UrlHelper._referer;
            Req.Method = "GET";
            Req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1;)";
            Req.AllowAutoRedirect = true;
            Req.MaximumAutomaticRedirections = 10;
            // 超时时间30000=30秒
            Req.Timeout = 30000;
            //  是否建立TCP持久连接
            Req.KeepAlive = false;

            HttpWebResponse response = (HttpWebResponse)Req.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding myEncoding = Encoding.GetEncoding(encoding);
            StreamReader streamReader = new StreamReader(stream, myEncoding);
            string html = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            stream.Close();
            stream.Dispose();
            response.Close();

            return html;
        }
    }
}
