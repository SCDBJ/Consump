using HttpServers.Model;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.StoreProcedure
{
    public class ExecuteConsump
    {
        string connectionString;
        public ExecuteConsump()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AddConsumpRecordCommand(ConsumpWModel consumpWModel)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddConsumpRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@consumpType", SqlDbType.VarChar);
            myCommand.Parameters["@consumpType"].Value = consumpWModel.consumpType;
            myCommand.Parameters.Add("@consumpAmount", SqlDbType.Decimal, 10);
            myCommand.Parameters["@consumpAmount"].Value = consumpWModel.consumpAmount;
            myCommand.Parameters.Add("@consumpNote", SqlDbType.VarChar);
            myCommand.Parameters["@consumpNote"].Value = consumpWModel.consumpNote;
            myCommand.Parameters.Add("@consumpTime", SqlDbType.DateTime);
            myCommand.Parameters["@consumpTime"].Value = consumpWModel.consumpTime;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string GetAllConsumpRecordCommand()
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetAllConsumpRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            List<ConsumpAllModel> list = new List<ConsumpAllModel>();
            if (dt.Rows.Count > 0)
            {
                ConsumpAllModel consumpAllModel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    consumpAllModel = new ConsumpAllModel { consumpId = int.Parse(dt.Rows[i]["consumpId"].ToString()), consumpType =dt.Rows[i]["consumpType"].ToString(), consumpAmount = decimal.Parse(dt.Rows[i]["consumpAmount"].ToString()), consumpNote= dt.Rows[i]["consumpNote"].ToString(), consumpTime=DateTime.Parse(dt.Rows[i]["consumpTime"].ToString()), createTime=DateTime.Parse(dt.Rows[i]["createTime"].ToString()) };
                    list.Add(consumpAllModel);
                }
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                return json;
            }
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }

            return null;
        }
        public string GetStatisticAmountCommand(int year)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetStatisticAmount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@year", SqlDbType.Int);
            myCommand.Parameters["@year"].Value = year;
            myCommand.ExecuteNonQuery();

            SqlDataReader sqlDataReader = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            List<StatisticConsumpModel> list = new List<StatisticConsumpModel>();
            if (dt.Rows.Count > 0)
            {
                StatisticConsumpModel statisticSpendModel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    statisticSpendModel = new StatisticConsumpModel { consumpYear = int.Parse(dt.Rows[i]["consumpYear"].ToString()), consumpMonth = int.Parse(dt.Rows[i]["consumpMonth"].ToString()), statisticAmount = decimal.Parse(dt.Rows[i]["consumpAmount"].ToString())};
                    list.Add(statisticSpendModel);
                }
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                return json;
            }
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return null;
        }
        public string GetStaticVerifyAmountCommand(string year, int month)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetStaticVerifyAmount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@year", SqlDbType.NVarChar);
            myCommand.Parameters["@year"].Value = year;
            myCommand.Parameters.Add("@month", SqlDbType.Int);
            myCommand.Parameters["@month"].Value = month;
            myCommand.ExecuteNonQuery();

            SqlDataReader sqlDataReader = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            List<StatiVerifyModel> list = new List<StatiVerifyModel>();
            if (dt.Rows.Count > 0)
            {
                StatiVerifyModel statiVerifyModel;
                if(year != "全部"&month ==0|| year=="全部" & month != 0)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    statiVerifyModel = new StatiVerifyModel { consumpYear = int.Parse(dt.Rows[i]["年"].ToString()), consumpType = dt.Rows[i]["类别"].ToString(), statisticAmount = decimal.Parse(dt.Rows[i]["金额"].ToString()) };
                    list.Add(statiVerifyModel);
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        statiVerifyModel = new StatiVerifyModel { consumpYear = int.Parse(dt.Rows[i]["年"].ToString()), consumpMonth = int.Parse(dt.Rows[i]["月"].ToString()), consumpType = dt.Rows[i]["类别"].ToString(), statisticAmount = decimal.Parse(dt.Rows[i]["金额"].ToString()) };
                        list.Add(statiVerifyModel);
                    }
                }
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                return json;
            }
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return null;
        }
        public int DeleteConsumpRecordCommand(int consumpId)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DeleteConsumpRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@consumpId", SqlDbType.Int);
            myCommand.Parameters["@consumpId"].Value = consumpId;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public void AutoAccountCommand()
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AutoAccount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 小程序用
        /// </summary>
        /// <param name="consumpModel"></param>
        /// <returns></returns>
        public int AddConsumpRecordCommand(ConsumpModel consumpModel)
        {
            int resultValue;
            try
            {
                SqlConnection myConnection = new SqlConnection(connectionString);
                if (myConnection.State != ConnectionState.Open)
                {
                    myConnection.Open();
                }
                SqlCommand myCommand = new SqlCommand("AddConsumpRecord", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("@consumpType", SqlDbType.VarChar);
                myCommand.Parameters["@consumpType"].Value = consumpModel.consumpType;
                myCommand.Parameters.Add("@consumpAmount", SqlDbType.Decimal, 10);
                myCommand.Parameters["@consumpAmount"].Value = consumpModel.consumpAmount;
                myCommand.Parameters.Add("@consumpNote", SqlDbType.VarChar);
                myCommand.Parameters["@consumpNote"].Value = consumpModel.consumpNote;

                resultValue = myCommand.ExecuteNonQuery();
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return -1;
            }
            
            return resultValue;
        }


        /// <summary>
        /// （消费记录）小程序用
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetConsumpList(string startDate, string endDate)
        {
            List<ConsumpModel> consumpList = new List<ConsumpModel>();
            try
            {
                SqlConnection myConnection = new SqlConnection(connectionString);
                if (myConnection.State != ConnectionState.Open)
                {
                    myConnection.Open();
                }
                SqlCommand myCommand = new SqlCommand("GetConsumpRecordRange", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("@startDate", SqlDbType.VarChar);
                myCommand.Parameters["@startDate"].Value = startDate;
                myCommand.Parameters.Add("@endDate", SqlDbType.VarChar);
                myCommand.Parameters["@endDate"].Value = endDate;

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sqlDataReader);
                if (dt.Rows.Count > 0)
                {
                    ConsumpModel consumpModel = new ConsumpModel();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        consumpModel = new ConsumpModel { consumpType = dt.Rows[i]["consumpType"].ToString(), consumpAmount = double.Parse(dt.Rows[i]["consumpAmount"].ToString()), consumpTime=dt.Rows[i]["consumpTime"].ToString(), consumpNote= dt.Rows[i]["consumpNote"].ToString() };
                        consumpList.Add(consumpModel);
                    }
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(consumpList);
                    return json;
                }
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return null;
            }
            return null;
        }
        /// <summary>
        /// （消费统计）小程序用
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetConsumpStatisticW(string consumpStatisticW)
        {
            List<ConsumpStatisticeModel> consumpList = new List<ConsumpStatisticeModel>();
            try
            {
                SqlConnection myConnection = new SqlConnection(connectionString);
                if (myConnection.State != ConnectionState.Open)
                {
                    myConnection.Open();
                }
                SqlCommand myCommand = new SqlCommand("GetConsumpStatisticW", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("@ConsumpStatisticW", SqlDbType.VarChar);
                myCommand.Parameters["@ConsumpStatisticW"].Value = consumpStatisticW;

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sqlDataReader);
                if (dt.Rows.Count > 0)
                {
                    ConsumpStatisticeModel consumpModel = new ConsumpStatisticeModel();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        consumpModel = new ConsumpStatisticeModel { consumpType = dt.Rows[i]["consumpType"].ToString(), numbers = int.Parse(dt.Rows[i]["numbers"].ToString()), sumamount = Math.Round(decimal.Parse(dt.Rows[i]["sumamount"].ToString()),2), avgamount = Math.Round(decimal.Parse(dt.Rows[i]["avgamount"].ToString()),2) };
                        consumpList.Add(consumpModel);
                    }
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(consumpList);
                    return json;
                }
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return null;
            }
            return null;
        }
    }
}
