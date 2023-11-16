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
    public class ExecuteSpend
    {
        string connectionString;
        public ExecuteSpend()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AddConsumpRecordCommand(ConsumpWModel spend)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddConsumpRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@consumpType", SqlDbType.VarChar);
            myCommand.Parameters["@consumpType"].Value = spend.spendType;
            myCommand.Parameters.Add("@consumpAmount", SqlDbType.Decimal, 10);
            myCommand.Parameters["@consumpAmount"].Value = spend.spendAmount;
            myCommand.Parameters.Add("@consumpNote", SqlDbType.VarChar);
            myCommand.Parameters["@consumpNote"].Value = spend.spendNote;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public DataTable GetAllConsumpRecordCommand()
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

            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            adapter.Fill(ds);
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            if (ds.Tables.Count > 0)
            {
                DataTable dataTable = ds.Tables[0];
                return dataTable;
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
            List<StatisticSpendModel> list = new List<StatisticSpendModel>();
            if (dt.Rows.Count > 0)
            {
                StatisticSpendModel statisticSpendModel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    statisticSpendModel = new StatisticSpendModel { spendYear = int.Parse(dt.Rows[i]["spendYear"].ToString()), spendMonth = int.Parse(dt.Rows[i]["spendMonth"].ToString()), statisticAmount = decimal.Parse(dt.Rows[i]["consumpTime"].ToString())};
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    statiVerifyModel = new StatiVerifyModel { spendYear = int.Parse(dt.Rows[i]["spendYear"].ToString()), spendMonth = int.Parse(dt.Rows[i]["spendMonth"].ToString()), spendType = dt.Rows[i]["spendType"].ToString(), statisticAmount = decimal.Parse(dt.Rows[i]["statisticAmount"].ToString()) };
                    list.Add(statiVerifyModel);
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
        /// 小程序用
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
    }
}
