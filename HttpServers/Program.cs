
using HttpServers.Common;
using LogLib;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Timers;
using HttpServers.StoreProcedure;
using HttpServers.IHttpServer;
using HttpServers.ResponseHttp;
using HttpServers.HttpContextResponse;

namespace HttpServers
{
    class Program
    {
        static string ip = "";
        static string port = "26500";
        static void Main(string[] args)
        {
            try
            {
                ip = GetLocalIP();
                TimerTick();
                //ip = "127.0.0.1";
                HttpListener listerner = new HttpListener();
                {
                    for (; true;)
                    {
                        try
                        {
                            
                            listerner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证 Anonymous匿名访问
                            ListenerBinding(ref listerner);
                            listerner.Start();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("未能成功连接服务器.....");
                            listerner = new HttpListener();
                            continue;
                        }
                        break;
                    }
                    Console.WriteLine("服务器启动成功.......");

                    int maxThreadNum, portThreadNum;

                    //线程池
                    int minThreadNum;
                    ThreadPool.GetMaxThreads(out maxThreadNum, out portThreadNum);
                    ThreadPool.GetMinThreads(out minThreadNum, out portThreadNum);
                    Console.WriteLine("最大线程数：{0}", maxThreadNum);
                    Console.WriteLine("最小空闲线程数：{0}", minThreadNum);
                    //Console.WriteLine("API Name:" + apiName+"\r\n");
                    Console.WriteLine("\n\n等待客户连接中。。。。");
                    while (true)
                    {
                        //等待请求连接
                        //没有请求则GetContext处于阻塞状态
                        HttpListenerContext ctx = listerner.GetContext();

                        ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProc), ctx);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("Press any key to continue . . . ");
                Console.ReadKey();
            }

            Console.ReadKey();
        }
        private static void ListenerBinding(ref HttpListener listerner)
        {
            string configPath = Environment.CurrentDirectory + @"\config.ini";
            string miniProgram = OperationFile.ReadIniData("Interface", "MiniProgramConsump", "", configPath);
            List<string> miniProgramList = new List<string>();
            List<string> interList = new List<string>();
            if ( miniProgram != null )
            {
                for (int i = 0; i < miniProgram.Split('|').Length; i++)
                {
                    miniProgramList.Add("MiniConsump/"+ miniProgram.Split('|')[i]);
                }
                interList.AddRange(miniProgramList);
            }
            string appProgram = OperationFile.ReadIniData("Interface", "AppProgramConsump", "", configPath);
            List<string> appProgramList = new List<string>();
            if (appProgram!=null )
            {
                for (int i = 0; i < appProgram.Split('|').Length; i++)
                {
                    appProgramList.Add("AppConsump/" + appProgram.Split('|')[i]);
                }
                interList.AddRange(appProgramList);
            }
            string appWebSite = OperationFile.ReadIniData("Interface", "AppWebSite", "", configPath);
            List<string> appWebSiteList = new List<string>();
            if (appWebSite != null )
            {
                for (int i = 0; i < appWebSite.Split('|').Length; i++)
                {
                    appWebSiteList.Add("WebSiteEntry/" + appWebSite.Split('|')[i]);
                }
                interList.AddRange(appWebSiteList);
            }
            string appStockTrade = OperationFile.ReadIniData("Interface", "AppStockTrade", "", configPath);
            List<string> appStockTradeList = new List<string>();
            if (appStockTrade != null)
            {
                for (int i = 0; i < appStockTrade.Split('|').Length; i++)
                {
                    appStockTradeList.Add("StockTrade/" + appStockTrade.Split('|')[i]);
                }
                interList.AddRange(appStockTradeList);
            }


            string uriPreFix = "http://" + ip + ":" + port + "/";
            for (int i = 0; i < interList.Count; i++)
            {
                listerner.Prefixes.Add(uriPreFix + interList[i]+"/");
            }
        }
        static void TaskProc(object o)
        {
            Console.WriteLine("\n\n客户连接成功。。。。");
            Console.WriteLine("\n\n连接时间：" + DateTime.Now);
            HttpListenerContext ctx = (HttpListenerContext)o;

            ctx.Response.StatusCode = 200;//设置返回给客服端http状态代码
            if (ctx.Request == null)
            {
                return;
            }
            Stream stream = ctx.Request.InputStream;
            var urlSegments = ctx.Request.Url.Segments;
            Console.WriteLine("RequestUrl:" + ctx.Request.Url);
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string content = "";
            try
            {
                if (reader.BaseStream.CanRead)
                {
                    content = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[TaskProc]Exception:" + ex.Message);
                //LogHelper.Error("[reader.ReadToEnd()]Exception");
                return;
            }
            //LogHelper.Info("content:" + content);
            if (!string.IsNullOrEmpty(content.Trim()))
            {
                try
                {
                    content = content.Trim();
                    Console.WriteLine("content:"+ content);
                    switch (urlSegments[1].Replace("/",""))
                    {
                        case "MiniConsump"://消费记录(小程序)
                            MiniProgramConsump.WriteResponse(ctx, urlSegments[2].Replace("/", ""), content);
                            break;
                        case "AppConsump"://消费记录(应用程序)
                            ApplicatonConsump.WriteResponse(ctx, urlSegments[2].Replace("/", ""), content);
                            break;
                        case "WebSiteEntry"://网站录入(电脑端)
                            ApplicationWebsite.WriteResponse(ctx, urlSegments[2].Replace("/", ""), content);
                            break;
                        case "StockTrade"://股票交易记录
                            ApplicationStockTrade.WriteResponse(ctx, urlSegments[2].Replace("/", ""), content);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //LogHelper.Error("[TaskProc]Exception:" + ex.Message);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
                {
                    string msg = "请检查数据格式";
                    writer.Write(msg);
                    if (writer.BaseStream.CanRead)
                    {
                        writer.Close();
                        ctx.Response.Close();
                    }
                    Console.WriteLine("\n\n服务端返回信息:" + msg);
                }
            }
        }
        /// <summary>
        /// 获取本机IP
        /// </summary>
        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                //LogHelper.Error("[GetLocalIP]Exception:" + ex.Message);
                return ex.Message;
            }
        }
        private static void TimerTick()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Tick;
            timer.Interval = 60000;
            timer.Start();
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            if (day == 6 & hour == 6 & minute == 1)
            {
                new ExecuteAutoAccount().AutoAccountCommand();
            }
        }
    }
}
