
using HttpServers.Model;
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

namespace HttpServers
{
    public class ResponseHttp : IHttpServer
    {
        public void SaveComsumpe(string content, HttpListenerContext httpListenerContext)
        {
            ConsumpModel consumpModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ConsumpModel>(content);
            string msg = "{\"Status\":0,\"Msg\":\"保存失败\"}";

            if (consumpModel != null)
            {
                try
                {
                    int result = new ExecuteSpend().AddConsumpRecordCommand(consumpModel);
                    if (result == 1)
                    {
                        msg = "{\"Status\":1,\"msg\":\"保存成功\"}";
                    }
                }
                catch (Exception ex)
                {
                    msg = "{\"Status\":0,\"msg\":\"" + ex.Message + "\"}";
                }

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
        public void GetConsumpList(string content, HttpListenerContext httpListenerContext)
        {
            string startTime = Regex.Match(content, @"\""startDate\"":\""(?<startDate>[\S\s]*?)\""").Groups["startDate"].Value;
            string endTime = Regex.Match(content, @"\""endDate\"":\""(?<endDate>[\S\s]*?)\""").Groups["endDate"].Value;
            string json= new ExecuteSpend().GetConsumpList(startTime, endTime);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void SaveComsumpeW(string content, HttpListenerContext httpListenerContext)
        {
            ConsumpWModel consumpWModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ConsumpWModel>(content);
            string msg = "{\"Status\":0,\"Msg\":\"保存失败\"}";
            if (consumpWModel != null)
            {
                try
                {
                    int result = new ExecuteSpend().AddConsumpRecordCommand(consumpWModel);
                    Console.WriteLine("result:" + result);
                    if (result == 1)
                    {
                        msg = "{\"Status\":1,\"msg\":\"保存成功\"}";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                    msg = "{\"Status\":0,\"msg\":\"" + ex.Message + "\"}";
                }

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
        public void GetStatisticAmount(string content, HttpListenerContext httpListenerContext)
        {
            int year = int.Parse(Regex.Match(content, @"\""year\"":\""(?<year>[\S\s]*?)\""").Groups["year"].Value);
            string json = new ExecuteSpend().GetStatisticAmountCommand(year);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void GetStaticVerifyAmount(string content, HttpListenerContext httpListenerContext)
        {
            int year = int.Parse(Regex.Match(content, @"\""year\"":\""(?<year>[\S\s]*?)\""").Groups["year"].Value);
            int month = int.Parse(Regex.Match(content, @"\""month\"":\""(?<month>[\S\s]*?)\""").Groups["month"].Value);
            string json = new ExecuteSpend().GetStaticVerifyAmountCommand(year.ToString(), month);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void DeleteConsumpRecord(string content, HttpListenerContext httpListenerContext)
        {
            int consumpId = int.Parse(Regex.Match(content, @"\""consumpId\"":\""(?<consumpId>[\S\s]*?)\""").Groups["consumpId"].Value);
            int result = new ExecuteSpend().DeleteConsumpRecordCommand(consumpId);
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

        public void AutoAccount(string content, HttpListenerContext httpListenerContext)
        {
            new ExecuteSpend().AutoAccountCommand();
            string msg = "{\"Status\":0,\"Msg\":\"更新成功\"}";
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
