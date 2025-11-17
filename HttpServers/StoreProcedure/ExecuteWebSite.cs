using HttpServers.Model.Salary;
using HttpServers.Model.WebSite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServers.Model;

namespace HttpServers.StoreProcedure
{
    public class ExecuteWebSite
    {
        string connectionString;
        public ExecuteWebSite()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AddWebSiteCommand(WebSiteModel webSiteModel)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddWebSite", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@websiteName", SqlDbType.VarChar);
            myCommand.Parameters["@websiteName"].Value = webSiteModel.websiteName;
            myCommand.Parameters.Add("@websiteHome", SqlDbType.VarChar);
            myCommand.Parameters["@websiteHome"].Value = webSiteModel.websiteHome;
            myCommand.Parameters.Add("@websiteDetail", SqlDbType.VarChar);
            myCommand.Parameters["@websiteDetail"].Value = webSiteModel.websiteDetail;
            myCommand.Parameters.Add("@websiteCategory", SqlDbType.VarChar);
            myCommand.Parameters["@websiteCategory"].Value = webSiteModel.websiteCategory;
            myCommand.Parameters.Add("@contentTitle", SqlDbType.VarChar);
            myCommand.Parameters["@contentTitle"].Value = webSiteModel.contentTitle;
            myCommand.Parameters.Add("@websiteRemark", SqlDbType.VarChar);
            myCommand.Parameters["@websiteRemark"].Value = webSiteModel.websiteRemark;
            myCommand.Parameters.Add("@commonUse", SqlDbType.Int);
            myCommand.Parameters["@commonUse"].Value = webSiteModel.commonUse;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string GetWebSiteCommand(string websiteCategory,string websiteName)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetWebSite", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@websiteCategory", SqlDbType.VarChar);
            myCommand.Parameters["@websiteCategory"].Value = websiteCategory;
            myCommand.Parameters.Add("@websiteName", SqlDbType.VarChar);
            myCommand.Parameters["@websiteName"].Value = websiteName;

            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            List<WebSiteModel> list = new List<WebSiteModel>();
            if (dt.Rows.Count > 0)
            {
                WebSiteModel webSiteModel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    webSiteModel = new WebSiteModel { websiteId = dt.Rows[i]["websiteId"].ToString(), websiteName = dt.Rows[i]["websiteName"].ToString(), websiteHome = dt.Rows[i]["websiteHome"].ToString(), websiteDetail = dt.Rows[i]["websiteDetail"].ToString(), websiteCategory = dt.Rows[i]["websiteCategory"].ToString(), contentTitle = dt.Rows[i]["contentTitle"].ToString(), websiteRemark = dt.Rows[i]["websiteRemark"].ToString(), createTime = DateTime.Parse(dt.Rows[i]["createTime"].ToString()),commonUse=int.Parse(dt.Rows[i]["commonUse"].ToString()) };
                    list.Add(webSiteModel);
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
        public int DeleteWebSiteCommand(int websiteId)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DeleteWebSite", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@websiteId", SqlDbType.Int);
            myCommand.Parameters["@websiteId"].Value = websiteId;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public int ModifyWebSiteCommand(int websiteId,int commonUse)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("ModifyWebSite", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@websiteId", SqlDbType.Int);
            myCommand.Parameters["@websiteId"].Value = websiteId;
            myCommand.Parameters.Add("@commonUse", SqlDbType.Int);
            myCommand.Parameters["@commonUse"].Value = commonUse;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
    }
}
