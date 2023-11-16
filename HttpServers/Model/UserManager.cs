using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    [SugarTable("UserManager")]
    public class UserManager
    {
        [SugarColumn(ColumnName = "ID", Length = 50, IsPrimaryKey = true)]
        public string ID
        {
            get; set;
        }
        [SugarColumn(ColumnName = "userName", Length = 50)]
        public string userName
        {
            get; set;
        }
        [SugarColumn(ColumnName = "password", Length = 50)]
        public string password
        {
            get; set;
        }
        [SugarColumn(ColumnName = "realName", Length = 50)]
        public string realName
        {
            get; set;
        }
        [SugarColumn(ColumnName = "note", Length = 50)]
        public string note
        {
            get; set;
        }
        [SugarColumn(ColumnName = "role", Length = 50)]
        public string role
        {
            get; set;
        }
        [SugarColumn(ColumnName = "inUse")]
        public int inUse
        {
            get; set;
        }
        [SugarColumn(ColumnName = "addTime")]
        public DateTime addTime
        {
            get; set;
        }
        [SugarColumn(ColumnName = "unUseTime")]
        public DateTime unUseTime
        {
            get; set;
        }
        [SugarColumn(ColumnName = "company")]
        public string company
        {
            get; set;
        }
        [SugarColumn(ColumnName = "phone")]
        public string phone
        {
            get; set;
        }
    }
}
