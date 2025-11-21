using HttpServers.ResponseHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.HttpContextResponse
{
    /// <summary>
    /// 消费记录(应用程序)
    /// </summary>
    public class ApplicatonConsump
    {
        public static void WriteResponse(HttpListenerContext ctx, string apiname, string content)
        {
            ConsumpResponse consumpResponse = new ConsumpResponse();
            switch (apiname)
            {
                case "SaveConsumpW":
                    consumpResponse.SaveConsumpW(content, ctx);
                    break;
                case "GetStatisticAmount":
                    consumpResponse.GetStatisticAmount(content, ctx);
                    break;
                case "GetStaticVerifyAmount":
                    consumpResponse.GetStaticVerifyAmount(content, ctx);
                    break;
                case "GetAllConsump":
                    consumpResponse.GetAllConsump(content, ctx);
                    break;
                case "DeleteConsumpRecord":
                    consumpResponse.DeleteConsumpRecord(content, ctx);
                    break;
                case "AutoAccount":
                    consumpResponse.AutoAccount(content, ctx);
                    break;
                case "SaveIncomeW":
                    consumpResponse.SaveIncomeW(content, ctx);
                    break;
                case "GetAllIncome":
                    consumpResponse.GetAllIncome(content, ctx);
                    break;
                case "GetIncomeStatisticAmount":
                    consumpResponse.GetIncomeStatisticAmount(content, ctx);
                    break;
                case "GetStatisticYearAmount":
                    consumpResponse.GetStatisticYearAmount(content, ctx);
                    break;
                case "DeleteIncomeRecord":
                    consumpResponse.DeleteIncomeRecord(content, ctx);
                    break;
                case "GetIncomeStatisticTypeAmount":
                    consumpResponse.GetIncomeStatisticTypeAmount(content, ctx);
                    break;
                case "GetIncomeStatisticMonthAmount":
                    consumpResponse.GetIncomeStatisticMonthAmount(content, ctx);
                    break;
                case "AddSalaryRecord":
                    consumpResponse.AddSalaryRecord(content, ctx);
                    break;
                case "GetAllSalaryRecord":
                    consumpResponse.GetAllSalaryRecord(content, ctx);
                    break;
                case "AddCategory":
                    consumpResponse.AddCategory(content, ctx);
                    break;
                case "GetAllCategory":
                    consumpResponse.GetAllCategory(content, ctx);
                    break;
                case "DeleteCategory":
                    consumpResponse.DeleteCategory(content, ctx);
                    break;
                case "GetSalaryDateRecord":
                    consumpResponse.GetSalaryDateRecord(content, ctx);
                    break;
            }
        }
    }
}
