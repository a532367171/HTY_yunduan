using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Mobile
{
    /// <summary>
    /// api 的摘要说明
    /// </summary>
    public class api : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            // 假设当前页完整地址是：http://www.test.com/aaa/bbb.aspx?id=5&name=kelli

            //"http://"是协议名

            //"www.test.com"是域名

            //"aaa"是站点名

            //"bbb.aspx"是页面名（文件名）

            //"id=5&name=kelli"是参数

            //【1】获取 完整url （协议名 + 域名 + 站点名 + 文件名 + 参数）

            string url = HttpContext.Current.Request.Url.ToString();

            //url = http://www.test.com/aaa/bbb.aspx?id=5&name=kelli

            //【2】获取 站点名+页面名 + 参数：

            string url2 = HttpContext.Current.Request.RawUrl;

            //(或 string url = Request.Url.PathAndQuery;)

            //url = / aaa / bbb.aspx ? id = 5 & name = kelli

            //【3】获取 站点名+页面名：

            string url3 = HttpContext.Current.Request.Url.AbsolutePath;

            //(或 string url = HttpContext.Current.Request.Path;)

            //url = aaa / bbb.aspx

            //【4】获取 域名：

            string url4 = HttpContext.Current.Request.Url.Host;

            //url = www.test.com

            //【5】获取 参数：

            string url5 = HttpContext.Current.Request.Url.Query;

            //url = ? id = 5 & name = kelli

            //【6】获取参数

            //string id6 = HttpContext.Current.Request.QueryString["id"].ToString();
            //string name = HttpContext.Current.Request.QueryString["name"].ToString();

            string url7=null;
            var x = ParseUrl(url, out url7);



            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
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

        //public string Login([FromBody]LoginReq req)
        //{
        //    var token = TokenManager.GenerateToken(req.username);
        //    return token;
        //}

    }
}