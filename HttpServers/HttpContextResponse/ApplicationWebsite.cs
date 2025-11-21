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
    public class ApplicationWebsite
    {
        public static void WriteResponse(HttpListenerContext ctx, string apiname, string content)
        {
            WebSiteResponse webSiteResponse = new WebSiteResponse();
            switch (apiname)
            {
                case "AddWebSite":
                    webSiteResponse.AddWebSite(content, ctx);
                    break;
                case "GetWebSite":
                    webSiteResponse.GetWebSite(content, ctx);
                    break;
                case "DeleteWebSite":
                    webSiteResponse.DeleteWebSite(content, ctx);
                    break;
                case "ModifyWebSite":
                    webSiteResponse.ModifyWebSite(content, ctx);
                    break;
            }
        }
    }
}
