using HttpServers.IHttpServer;
using HttpServers.Model;
using HttpServers.Model.StockTrade;
using HttpServers.StoreProcedure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpServers.ResponseHttp
{
    public class StockTradeResponse : IHttpStockTrade
    {
        public void AddStockTrade(string content, HttpListenerContext httpListenerContext)
        {
            StockTradeModel stockTradeModel = Newtonsoft.Json.JsonConvert.DeserializeObject<StockTradeModel>(content);
            int result = new ExecuteStockTrade().AddStockTradeCommand(stockTradeModel);
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

        public void GetStockTrade(string content, HttpListenerContext httpListenerContext)
        {
            string StockCode = Regex.Match(content, @"\""StockCode\"":\""(?<StockCode>[\S\s]*?)\""").Groups["StockCode"].Value;
            string StockName = Regex.Match(content, @"\""StockName\"":\""(?<StockName>[\S\s]*?)\""").Groups["StockName"].Value;
            string TradeStartDate = Regex.Match(content, @"\""TradeStartDate\"":\""(?<TradeStartDate>[\S\s]*?)\""").Groups["TradeStartDate"].Value;
            string TradeEndDate = Regex.Match(content, @"\""TradeEndDate\"":\""(?<TradeEndDate>[\S\s]*?)\""").Groups["TradeEndDate"].Value;
            string result = new ExecuteStockTrade().GetStockTradeCommand(StockCode, StockName, TradeStartDate, TradeEndDate);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(result);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + result + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
    }
}
