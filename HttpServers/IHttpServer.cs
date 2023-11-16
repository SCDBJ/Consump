using HttpServers.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers
{
    public interface IHttpServer
    {
        void SaveComsumpe(string content, HttpListenerContext httpListenerContext);
        void GetConsumpList(string content, HttpListenerContext httpListenerContext);

        void SaveComsumpeW(string content, HttpListenerContext httpListenerContext);
        void GetStatisticAmount(string content, HttpListenerContext httpListenerContext);
        void GetStaticVerifyAmount(string content, HttpListenerContext httpListenerContext);
        void DeleteConsumpRecord(string content, HttpListenerContext httpListenerContext);
        void AutoAccount(string content, HttpListenerContext httpListenerContext);
    }
}
