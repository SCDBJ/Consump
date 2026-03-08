using HttpServers.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.IHttpServer
{
    public interface IHttpConsump
    {
        void SaveComsumpe(string content, HttpListenerContext httpListenerContext);
        void GetConsumpList(string content, HttpListenerContext httpListenerContext);

        void SaveConsumpW(string content, HttpListenerContext httpListenerContext);
        void GetConsumpStatW(string content, HttpListenerContext httpListenerContext);
        void GetStatisticAmount(string content, HttpListenerContext httpListenerContext);
        void GetStaticVerifyAmount(string content, HttpListenerContext httpListenerContext);
        void GetAllConsump(string content, HttpListenerContext httpListenerContext);
        void DeleteConsumpRecord(string content, HttpListenerContext httpListenerContext);
        void AutoAccount(string content, HttpListenerContext httpListenerContext);
        void SaveIncomeW(string content, HttpListenerContext httpListenerContext);
        void GetAllIncome(string content, HttpListenerContext httpListenerContext);
        void GetIncomeStatisticAmount(string content, HttpListenerContext httpListenerContext);
        void GetStatisticYearAmount(string content, HttpListenerContext httpListenerContext);
        void DeleteIncomeRecord(string content, HttpListenerContext httpListenerContext);
        void GetIncomeStatisticTypeAmount(string content, HttpListenerContext httpListenerContext);
        void GetIncomeStatisticMonthAmount(string content, HttpListenerContext httpListenerContext);
        void AddSalaryRecord(string content, HttpListenerContext httpListenerContext);
        void GetAllSalaryRecord(string content, HttpListenerContext httpListenerContext);
        void AddCategory(string content, HttpListenerContext httpListenerContext);
        void GetAllCategory(string content, HttpListenerContext httpListenerContext);
        void DeleteCategory(string content, HttpListenerContext httpListenerContext);
        void GetSalaryDateRecord(string content, HttpListenerContext httpListenerContext); 
        void GetIncomeExpMonth(string content, HttpListenerContext httpListenerContext);
        void GetIncomeExpYear(string content, HttpListenerContext httpListenerContext);
    }
}
