
using HttpServers.Model;
using HttpServers.Model.Salary;
using HttpServers.Model.WebSite;
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
                    if (consumpModel.consumpType.Equals("请选择"))
                    {
                        msg = "{\"Status\":0,\"Msg\":\"请选择消费类型\"}";
                    }
                    else
                    {
                        int result = new ExecuteConsump().AddConsumpRecordCommand(consumpModel);
                        if (result == 1)
                        {
                            msg = "{\"Status\":1,\"msg\":\"保存成功\"}";
                        }
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
            string json= new ExecuteConsump().GetConsumpList(startTime, endTime);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void SaveConsumpW(string content, HttpListenerContext httpListenerContext)
        {
            ConsumpWModel consumpWModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ConsumpWModel>(content);
            string msg = "{\"Status\":0,\"Msg\":\"保存失败\"}";
            if (consumpWModel != null)
            {
                try
                {
                    int result = new ExecuteConsump().AddConsumpRecordCommand(consumpWModel);
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
        public void GetConsumpStatW(string content, HttpListenerContext httpListenerContext)
        {
            string consumpStatisticW = Regex.Match(content, @"\""consumpStatisticW\"":\""(?<consumpStatisticW>[\S\s]*?)\""").Groups["consumpStatisticW"].Value;
            string json = new ExecuteConsump().GetConsumpStatisticW(consumpStatisticW);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
        public void GetStatisticAmount(string content, HttpListenerContext httpListenerContext)
        {
            int year = int.Parse(Regex.Match(content, @"\""consumpYear\"":\""(?<consumpYear>[\S\s]*?)\""").Groups["consumpYear"].Value);
            string json = new ExecuteConsump().GetStatisticAmountCommand(year);
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
            int year = int.Parse(Regex.Match(content, @"\""consumpYear\"":\""(?<consumpYear>[\S\s]*?)\""").Groups["consumpYear"].Value);
            int month = int.Parse(Regex.Match(content, @"\""consumpMonth\"":\""(?<consumpMonth>[\S\s]*?)\""").Groups["consumpMonth"].Value);
            string json = new ExecuteConsump().GetStaticVerifyAmountCommand(year.ToString(), month);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
        public void GetAllConsump(string content, HttpListenerContext httpListenerContext)
        {
            string json = new ExecuteConsump().GetAllConsumpRecordCommand();
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
            int result = new ExecuteConsump().DeleteConsumpRecordCommand(consumpId);
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
            new ExecuteConsump().AutoAccountCommand();
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

        public void SaveIncomeW(string content, HttpListenerContext httpListenerContext)
        {
            IncomeWModel incomeWModel = Newtonsoft.Json.JsonConvert.DeserializeObject<IncomeWModel>(content);
            string msg = "{\"Status\":0,\"Msg\":\"保存失败\"}";

            if (incomeWModel != null)
            {
                try
                {
                    int result = new ExecuteIncome().AddIncomeRecordCommand(incomeWModel);
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

        public void GetAllIncome(string content, HttpListenerContext httpListenerContext)
        {
            string json = new ExecuteIncome().GetAllIncomeRecordCommand();
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void GetIncomeStatisticAmount(string content, HttpListenerContext httpListenerContext)
        {
            string year = Regex.Match(content, @"\""incomeYear\"":\""(?<incomeYear>[\S\s]*?)\""").Groups["incomeYear"].Value;
            string json = new ExecuteIncome().GetStatisticAmountCommand(year);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void GetStatisticYearAmount(string content, HttpListenerContext httpListenerContext)
        {
            int year = int.Parse(Regex.Match(content, @"\""incomeYear\"":\""(?<incomeYear>[\S\s]*?)\""").Groups["incomeYear"].Value);
            string json = new ExecuteIncome().GetStatisticYearAmountCommand(year.ToString());
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void DeleteIncomeRecord(string content, HttpListenerContext httpListenerContext)
        {
            int incomeId = int.Parse(Regex.Match(content, @"\""incomeId\"":\""(?<incomeId>[\S\s]*?)\""").Groups["incomeId"].Value);
            int result = new ExecuteIncome().DeleteIncomeRecordCommand(incomeId);
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

        public void GetIncomeStatisticTypeAmount(string content, HttpListenerContext httpListenerContext)
        {
            string incomeType = Regex.Match(content, @"\""incomeType\"":\""(?<incomeType>[\S\s]*?)\""").Groups["incomeType"].Value;
            string json = new ExecuteIncome().GetStatisticTypeAmountCommand(incomeType);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void GetIncomeStatisticMonthAmount(string content, HttpListenerContext httpListenerContext)
        {
            string incomeType = Regex.Match(content, @"\""incomeType\"":\""(?<incomeType>[\S\s]*?)\""").Groups["incomeType"].Value;
            string json = new ExecuteIncome().GetStatisticMonthAmountCommand(incomeType);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void AddSalaryRecord(string content, HttpListenerContext httpListenerContext)
        {
            SalaryItem salaryItem= Newtonsoft.Json.JsonConvert.DeserializeObject<SalaryItem>(content);
            int result = new ExecuteSalary().AddSalaryRecordCommand(salaryItem);
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

        public void GetAllSalaryRecord(string content, HttpListenerContext httpListenerContext)
        {
            string json = new ExecuteSalary().GetAllSalaryRecordCommand();
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void AddCategory(string content, HttpListenerContext httpListenerContext)
        {
            CategoryAddModel categoryAddModel = Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryAddModel>(content);
            int result = new ExecuteCategory().AddCategoryCommand(categoryAddModel);
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

        public void GetAllCategory(string content, HttpListenerContext httpListenerContext)
        {
            string categoryType = Regex.Match(content, @"\""categoryType\"":\""(?<categoryType>[\S\s]*?)\""").Groups["categoryType"].Value;
            string json = new ExecuteCategory().GetAllCategoryCommand(categoryType);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(json);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + json + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void DeleteCategory(string content, HttpListenerContext httpListenerContext)
        {
            int categoryId = int.Parse(Regex.Match(content, @"\""categoryId\"":\""(?<categoryId>[\S\s]*?)\""").Groups["categoryId"].Value);
            int result = new ExecuteCategory().DeleteCategoryCommand(categoryId);
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

        public void GetSalaryDateRecord(string content, HttpListenerContext httpListenerContext)
        {
            int datacyear = int.Parse(Regex.Match(content, @"\""datacyear\"":\""(?<datacyear>\d+)\""").Groups["datacyear"].Value);
            string datacperiod = Regex.Match(content, @"\""datacperiod\"":\""(?<datacperiod>\d+)\""").Groups["datacperiod"].Value;
            string result= new ExecuteSalary().GetSalaryDateRecordCommand(datacyear, datacperiod);
            bool isExist = result.Equals("1") ? true : false;
            string msg = "{\"Status\":1,\"Msg\":\""+ isExist + "\"}";
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(msg);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + msg + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
        public void AddWebSite(string content, HttpListenerContext httpListenerContext)
        {
            WebSiteModel webSiteModel = Newtonsoft.Json.JsonConvert.DeserializeObject<WebSiteModel>(content);
            int result = new ExecuteWebSite().AddWebSiteCommand(webSiteModel);
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
        public void GetWebSite(string content, HttpListenerContext httpListenerContext)
        {
            string websiteCategory = Regex.Match(content, @"\""websiteCategory\"":\""(?<websiteCategory>[\S\s]*?)\""").Groups["websiteCategory"].Value;
            string websiteName = Regex.Match(content, @"\""websiteName\"":\""(?<websiteName>[\S\s]*?)\""").Groups["websiteName"].Value;
            string result = new ExecuteWebSite().GetWebSiteCommand(websiteCategory, websiteName);
            using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
            {
                writer.Write(result);
                writer.Close();
                httpListenerContext.Response.Close();
                Console.WriteLine("\n\n服务端返回信息:" + result + "\n时间:" + DateTime.Now.ToString());
                Console.WriteLine("----------------------------------------------------");
            }
        }
        public void DeleteWebSite(string content, HttpListenerContext httpListenerContext)
        {
            int websiteId = int.Parse(Regex.Match(content, @"\""websiteId\"":\""(?<websiteId>[\S\s]*?)\""").Groups["websiteId"].Value);
            int result = new ExecuteWebSite().DeleteWebSiteCommand(websiteId);
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
        public void ModifyWebSite(string content, HttpListenerContext httpListenerContext)
        {
            int websiteId = int.Parse(Regex.Match(content, @"\""websiteId\"":\""(?<websiteId>[\S\s]*?)\""").Groups["websiteId"].Value);
            int commonUse = int.Parse(Regex.Match(content, @"\""commonUse\"":\""(?<commonUse>[\S\s]*?)\""").Groups["commonUse"].Value);
            int result = new ExecuteWebSite().ModifyWebSiteCommand(websiteId, commonUse);
            string msg = "{\"Status\":0,\"Msg\":\"设置失败\"}";
            if (result == 1)
            {
                msg = "{\"Status\":0,\"Msg\":\"设置成功\"}";
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
    }
}
