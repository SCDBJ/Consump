
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
            string miniProgram = OperationFile.ReadIniData("Interface", "MiniProgram", "", configPath);
            List<string> interList = new List<string>();
            if ( miniProgram != null )
            {
                interList.AddRange(miniProgram.Split('|').ToList());
            }
            string appProgram = OperationFile.ReadIniData("Interface", "AppProgram", "", configPath);
            if(appProgram!=null )
            {
                interList.AddRange(appProgram.Split('|').ToList());
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
            var absolutePath = ctx.Request.Url.AbsolutePath;
            Console.WriteLine("API Name:" + absolutePath);
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            ResponseHttp response = new ResponseHttp();
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
                    switch (absolutePath.Replace("/", ""))
                    {
                        case "SaveConsump"://保存消费记录(小程序)
                            response.SaveComsumpe(content, ctx);
                            break;
                        case "GetConsumpList"://获取消费记录(小程序)
                            response.GetConsumpList(content, ctx);
                            break;
                        case "SaveConsumpW"://保存消费记录(其他应用程序)
                            response.SaveConsumpW(content, ctx);
                            break;
                        case "GetConsumpStatW"://消费统计(小程序)
                            response.GetConsumpStatW(content, ctx);
                            break;
                        case "GetStatisticAmount":
                            response.GetStatisticAmount(content, ctx);
                            break;
                        case "GetStaticVerifyAmount":
                            response.GetStaticVerifyAmount(content, ctx);
                            break;
                        case "GetAllConsump":
                            response.GetAllConsump(content, ctx);
                            break;
                        case "DeleteConsumpRecord":
                            response.DeleteConsumpRecord(content, ctx);
                            break;
                        case "AutoAccount":
                            response.AutoAccount(content, ctx);
                            break;
                        case "SaveIncomeW":
                            response.SaveIncomeW(content, ctx);
                            break;
                        case "GetAllIncome":
                            response.GetAllIncome(content, ctx);
                            break;
                        case "GetIncomeStatisticAmount":
                            response.GetIncomeStatisticAmount(content, ctx);
                            break;
                        case "GetStatisticYearAmount":
                            response.GetStatisticYearAmount(content, ctx);
                            break;
                        case "DeleteIncomeRecord":
                            response.DeleteIncomeRecord(content, ctx);
                            break;
                        case "GetIncomeStatisticTypeAmount":
                            response.GetIncomeStatisticTypeAmount(content, ctx);
                            break;
                        case "GetIncomeStatisticMonthAmount":
                            response.GetIncomeStatisticMonthAmount(content, ctx);
                            break;
                        case "AddSalaryRecord":
                            response.AddSalaryRecord(content, ctx);
                            break;
                        case "GetAllSalaryRecord":
                            response.GetAllSalaryRecord(content, ctx);
                            break;
                        case "AddCategory":
                            response.AddCategory(content, ctx);
                            break;
                        case "GetAllCategory":
                            response.GetAllCategory(content, ctx);
                            break;
                        case "DeleteCategory":
                            response.DeleteCategory(content, ctx);
                            break;
                        case "GetSalaryDateRecord":
                            response.GetSalaryDateRecord(content, ctx);
                            break;
                        case "AddWebSite":
                            response.AddWebSite(content, ctx);
                            break;
                        case "GetWebSite":
                            response.GetWebSite(content, ctx);
                            break;
                        case "DeleteWebSite":
                            response.DeleteWebSite(content, ctx);
                            break;
                        case "ModifyWebSite":
                            response.ModifyWebSite(content, ctx);
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
