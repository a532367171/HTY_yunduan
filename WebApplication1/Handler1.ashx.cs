using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }


        /// <summary>
        /// 获取url字符串参数，返回参数值字符串
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="url">url字符串</param>
        /// <returns></returns>
        public string GetQueryString(string name, string url)
        {
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.MatchCollection mc = re.Matches(url);
            foreach (System.Text.RegularExpressions.Match m in mc)
            {
                if (m.Result("$2").Equals(name))
                {
                    return m.Result("$3");
                }
            }
            return "";
        }

        public static System.Collections.Specialized.NameValueCollection ParseUrl(string url, out string baseUrl)
        {
            baseUrl = "";
            if (string.IsNullOrEmpty(url))
                return null;
            System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();

            try
            {
                int questionMarkIndex = url.IndexOf('?');

                if (questionMarkIndex == -1)
                    baseUrl = url;
                else
                    baseUrl = url.Substring(0, questionMarkIndex);
                if (questionMarkIndex == url.Length - 1)
                    return null;
                string ps = url.Substring(questionMarkIndex + 1);

                // 开始分析参数对   
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
                System.Text.RegularExpressions.MatchCollection mc = re.Matches(ps);

                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
                }

            }
            catch { }
            return nvc;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}