using HttpServers.ResponseHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HttpServers.HttpContextResponse
{
    public class MiniProgramConsump
    {
        /// <summary>
        /// 消费记录(小程序)
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="apiname"></param>
        /// <param name="content"></param>
        public static void WriteResponse(HttpListenerContext ctx,string apiname,string content)
        {
            ConsumpResponse consumpResponse = new ConsumpResponse();
            switch (apiname)
            {
                case "SaveConsump"://保存消费记录
                    consumpResponse.SaveComsumpe(content, ctx);
                    break;
                case "GetConsumpList"://获取消费记录
                    consumpResponse.GetConsumpList(content, ctx);
                    break;
                case "GetConsumpStatW"://消费统计
                    consumpResponse.GetConsumpStatW(content, ctx);
                    break;
                case "GetAllCategory"://消费类别
                    consumpResponse.GetAllCategory(content, ctx);
                    break;
                    
            }
        }
    }
}
