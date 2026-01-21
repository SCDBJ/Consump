using HttpServers.Model.StockTrade;
using HttpServers.Model.WebSite;
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
    public class ExecuteStockTrade
    {
        string connectionString;
        public ExecuteStockTrade()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AddStockTradeCommand(StockTradeModel stockTradeModel)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddStockTrade", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@StockCode", SqlDbType.VarChar);
            myCommand.Parameters["@StockCode"].Value = stockTradeModel.StockCode;
            myCommand.Parameters.Add("@StockName", SqlDbType.VarChar);
            myCommand.Parameters["@StockName"].Value = stockTradeModel.StockName;
            myCommand.Parameters.Add("@TradeDate", SqlDbType.Date);
            myCommand.Parameters["@TradeDate"].Value = stockTradeModel.TradeDate;
            myCommand.Parameters.Add("@TradePrice", SqlDbType.Float);
            myCommand.Parameters["@TradePrice"].Value = stockTradeModel.TradePrice;
            myCommand.Parameters.Add("@TradeShares", SqlDbType.Int);
            myCommand.Parameters["@TradeShares"].Value = stockTradeModel.TradeShares;
            myCommand.Parameters.Add("@ProfitLossAmount", SqlDbType.Float);
            myCommand.Parameters["@ProfitLossAmount"].Value = stockTradeModel.ProfitLossAmount;
            myCommand.Parameters.Add("@TradeType", SqlDbType.VarChar);
            myCommand.Parameters["@TradeType"].Value = stockTradeModel.TradeType;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string GetStockTradeCommand(string stockCode, string stockName,string tradeStartDate,string tradeEndDate)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetStockTrade", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@StockCode", SqlDbType.VarChar);
            myCommand.Parameters["@StockCode"].Value = stockCode;
            myCommand.Parameters.Add("@StockName", SqlDbType.VarChar);
            myCommand.Parameters["@StockName"].Value = stockName;
            myCommand.Parameters.Add("@TradeStartDate", SqlDbType.Date);
            myCommand.Parameters["@TradeStartDate"].Value = tradeStartDate;
            myCommand.Parameters.Add("@TradeEndDate", SqlDbType.Date);
            myCommand.Parameters["@TradeEndDate"].Value = tradeEndDate;

            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            List<StockTradeModel> list = new List<StockTradeModel>();
            if (dt.Rows.Count > 0)
            {
                StockTradeModel stockTradeModel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    stockTradeModel = new StockTradeModel { StockId = dt.Rows[i]["StockId"].ToString(), StockCode = dt.Rows[i]["StockCode"].ToString(), StockName = dt.Rows[i]["StockName"].ToString(), TradeDate = dt.Rows[i]["TradeDate"].ToString(), TradePrice = float.Parse(dt.Rows[i]["TradePrice"].ToString()), TradeShares = int.Parse(dt.Rows[i]["TradeShares"].ToString()), ProfitLossAmount = decimal.Parse(dt.Rows[i]["ProfitLossAmount"].ToString()), TradeType = dt.Rows[i]["TradeType"].ToString() };
                    list.Add(stockTradeModel);
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
        public int DeleteStockTradeCommand(string StockId)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DeleteStockTrade", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@StockId", SqlDbType.Int);
            myCommand.Parameters["@StockId"].Value = StockId;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
    }
}
