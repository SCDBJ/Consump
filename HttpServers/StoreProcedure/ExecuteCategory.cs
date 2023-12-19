using HttpServers.Model.Salary;
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
    public class ExecuteCategory
    {
        string connectionString;
        public ExecuteCategory() 
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public int AddCategoryCommand(CategoryAddModel categoryAddModel)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("AddCategory", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@categoryName", SqlDbType.NVarChar);
            myCommand.Parameters["@categoryName"].Value = categoryAddModel.categoryName;
            myCommand.Parameters.Add("@categoryType", SqlDbType.NVarChar);
            myCommand.Parameters["@categoryType"].Value = categoryAddModel.categoryType;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
        public string GetAllCategoryCommand()
        {
            DataSet ds = new DataSet();

            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("GetAllCategory", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            SqlDataReader adapter = myCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(adapter);
            List<CategoryAddModel> list = new List<CategoryAddModel>();
            if (dt.Rows.Count > 0)
            {
                CategoryAddModel categoryAddModel;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    categoryAddModel = new CategoryAddModel { categoryId = int.Parse(dt.Rows[i]["categoryId"].ToString()), categoryName= dt.Rows[i]["categoryName"].ToString(), categoryType= dt.Rows[i]["categoryType"].ToString(), createTime=DateTime.Parse(dt.Rows[i]["createTime"].ToString()) };
                    list.Add(categoryAddModel);
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
        public int DeleteCategoryCommand(int categoryId)
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            SqlCommand myCommand = new SqlCommand("DeleteCategory", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@categoryId", SqlDbType.Int);
            myCommand.Parameters["@categoryId"].Value = categoryId;

            int resultValue = myCommand.ExecuteNonQuery();
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return resultValue;
        }
    }
}
