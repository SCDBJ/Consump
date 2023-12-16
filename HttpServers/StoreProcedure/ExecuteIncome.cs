
using HttpServers.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HttpServers.StoreProcedure
{
    public class ExecuteIncome
    {
        string connectionString;
        public ExecuteIncome()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AddIncomeRecordCommand(IncomeWModel income)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddIncomeRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@incomeType", SqlDbType.VarChar);
            myCommand.Parameters["@incomeType"].Value = income.incomeType;
            myCommand.Parameters.Add("@incomeAmount", SqlDbType.Decimal, 10);
            myCommand.Parameters["@incomeAmount"].Value = income.incomeAmount;
            myCommand.Parameters.Add("@incomeTime", SqlDbType.DateTime);
            myCommand.Parameters["@incomeTime"].Value = income.incomeTime;
            myCommand.Parameters.Add("@incomeNote", SqlDbType.VarChar);
            myCommand.Parameters["@incomeNote"].Value = income.incomeNote;


            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string GetAllIncomeRecordCommand()
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetAllIncomeRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            List<IncomeAddModel> list = new List<IncomeAddModel>();
            if (dt.Rows.Count > 0)
            {
                IncomeAddModel incomeAddModel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    incomeAddModel = new IncomeAddModel { incomeId = int.Parse(dt.Rows[i]["incomeId"].ToString()), incomeType = dt.Rows[i]["incomeType"].ToString(), incomeAmount = decimal.Parse(dt.Rows[i]["incomeAmount"].ToString()), incomeNote = dt.Rows[i]["incomeNote"].ToString(), incomeTime = DateTime.Parse(dt.Rows[i]["incomeTime"].ToString()), createTime = DateTime.Parse(dt.Rows[i]["createTime"].ToString()),incomeDate= int.Parse((DateTime.Parse(dt.Rows[i]["incomeTime"].ToString()).Year).ToString()+ (DateTime.Parse(dt.Rows[i]["incomeTime"].ToString()).Month.ToString()).PadLeft(2,'0')) };
                    list.Add(incomeAddModel);
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
        public string GetStatisticAmountCommand(string year)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetIncomeStatisticAmount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@year", SqlDbType.NVarChar);
            myCommand.Parameters["@year"].Value = year;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            List<StatisticIncomeModel> list = new List<StatisticIncomeModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new StatisticIncomeModel { issueYear = int.Parse(dt.Rows[i]["consumpYear"].ToString()), issueMonth = int.Parse(dt.Rows[i]["consumpMonth"].ToString()), incomeAmount = decimal.Parse(dt.Rows[i]["incomeAmount"].ToString()), spendAmount = decimal.Parse(dt.Rows[i]["spendAmount"].ToString()), netincomeAmount = decimal.Parse(dt.Rows[i]["netincomeAmount"].ToString()) });
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
        public string GetStatisticYearAmountCommand(string year)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetIncomeStatisticYearAmount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@year", SqlDbType.NVarChar);
            myCommand.Parameters["@year"].Value = year;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            IList<StatisticIncomeModel> list = new List<StatisticIncomeModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new StatisticIncomeModel { issueYear = int.Parse(dt.Rows[i]["incomeYear"].ToString()), incomeAmount = decimal.Parse(dt.Rows[i]["incomeAmount"].ToString()), incomeType = dt.Rows[i]["incomeType"].ToString() });
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
        public string GetStatisticTypeAmountCommand(string selectType)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetIncomeStatisticTypeAmount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@incomeType", SqlDbType.NVarChar);
            myCommand.Parameters["@incomeType"].Value = selectType;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            IList<StatisticIncomeModel> list = new List<StatisticIncomeModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new StatisticIncomeModel { issueYear = int.Parse(dt.Rows[i]["incomeYear"].ToString()), incomeAmount = decimal.Parse(dt.Rows[i]["incomeAmount"].ToString()), incomeType = dt.Rows[i]["incomeType"].ToString() });
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
        public string GetStatisticMonthAmountCommand(string selectType)
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetIncomeStatisticMonthAmount", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@incomeType", SqlDbType.NVarChar);
            myCommand.Parameters["@incomeType"].Value = selectType;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            IList<StatisticIncomeModel> list = new List<StatisticIncomeModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new StatisticIncomeModel { issueYear = int.Parse(dt.Rows[i]["incomeYear"].ToString()), issueMonth = int.Parse(dt.Rows[i]["incomeMonth"].ToString()), incomeAmount = decimal.Parse(dt.Rows[i]["incomeAmount"].ToString()), incomeType = dt.Rows[i]["incomeType"].ToString() });
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
        public int DeleteIncomeRecordCommand(int incomeId)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DeleteIncomeRecord", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@incomeId", SqlDbType.Int);
            myCommand.Parameters["@incomeId"].Value = incomeId;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
    }
}
