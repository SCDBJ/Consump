using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.IHttpServer
{
    public interface IHttpStockTrade
    {
        void AddStockTrade(string content, HttpListenerContext httpListenerContext);
        void GetStockTrade(string content, HttpListenerContext httpListenerContext);
        void DeleteStockTrade(string content, HttpListenerContext httpListenerContext);
    }
}
