
using HttpServers.Model;
using HttpServers.Model.Salary;
using HttpServers.Model.WebSite;
using HttpServers.StoreProcedure;

using LogLib;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpServers.IHttpServer
{
    public class WebSiteResponse : IHttpWebSite
    {
        
        public void AddWebSite(string content, HttpListenerContext httpListenerContext)
        {
            WebSiteModel WebSiteModel = Newtonsoft.Json.JsonConvert.DeserializeObject<WebSiteModel>(content);
            int result = new ExecuteWebSite().AddWebSiteCommand(WebSiteModel);
            string msg = "{\"Status\":0,\"Msg\":\"保存失败\"}";
            if (result.Equals(1))
            {
                msg = "{\"Status\":0,\"Msg\":\"保存成功\"}";
            }
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(msg);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + msg + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
        public void GetWebSite(string content, HttpListenerContext httpListenerContext)
        {
            string WebSiteCategory = Regex.Match(content, @"\""WebSiteCategory\"":\""(?<WebSiteCategory>[\S\s]*?)\""").Groups["WebSiteCategory"].Value;
            string WebSiteName = Regex.Match(content, @"\""WebSiteName\"":\""(?<WebSiteName>[\S\s]*?)\""").Groups["WebSiteName"].Value;
            string result = new ExecuteWebSite().GetWebSiteCommand(WebSiteCategory, WebSiteName);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(result);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + result + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
        public void DeleteWebSite(string content, HttpListenerContext httpListenerContext)
        {
            int WebSiteId = int.Parse(Regex.Match(content, @"\""WebSiteId\"":\""(?<WebSiteId>[\S\s]*?)\""").Groups["WebSiteId"].Value);
            int result = new ExecuteWebSite().DeleteWebSiteCommand(WebSiteId);
            string msg = "{\"Status\":0,\"Msg\":\"删除失败\"}";
            if (result == 1)
            {
                msg = "{\"Status\":0,\"Msg\":\"删除成功\"}";
            }
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(msg);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + msg + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
        public void ModifyWebSite(string content, HttpListenerContext httpListenerContext)
        {
            int websiteId = int.Parse(Regex.Match(content, @"\""websiteId\"":\""(?<websiteId>[\S\s]*?)\""").Groups["websiteId"].Value);
            string commonUse = Regex.Match(content, @"\""commonUse\"":\""(?<commonUse>[\S\s]*?)\""").Groups["commonUse"].Value;
            int result = new ExecuteWebSite().ModifyWebSiteCommand(websiteId, commonUse);
            string msg = "{\"Status\":0,\"Msg\":\"设置失败\"}";
            if (result == 1)
            {
                msg = "{\"Status\":0,\"Msg\":\"设置成功\"}";
            }
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(msg);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + msg + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
    }
}
