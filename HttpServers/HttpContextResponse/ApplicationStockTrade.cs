using HttpServers.IHttpServer;
using HttpServers.ResponseHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.HttpContextResponse
{
    public class ApplicationStockTrade
    {
        public static void WriteResponse(HttpListenerContext ctx, string apiname, string content)
        {
            StockTradeResponse stockTradeResponse = new StockTradeResponse();
            switch (apiname)
            {
                case "AddStockTrade":
                    stockTradeResponse.AddStockTrade(content, ctx);
                    break;
                case "GetStockTrade":
                    stockTradeResponse.GetStockTrade(content, ctx);
                    break;
                case "DeleteStockTrade":
                    stockTradeResponse.DeleteStockTrade(content, ctx);
                    break;
            }
        }
    }
}
