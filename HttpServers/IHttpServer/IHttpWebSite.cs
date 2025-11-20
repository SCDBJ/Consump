using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServers.Model;
using System.Net;

namespace HttpServers.IHttpServer
{
    public interface IHttpWebSite
    {
        void AddWebSite(string content, HttpListenerContext httpListenerContext);
        void GetWebSite(string content, HttpListenerContext httpListenerContext);
        void DeleteWebSite(string content, HttpListenerContext httpListenerContext);
        void ModifyWebSite(string content, HttpListenerContext httpListenerContext);
    }
}
