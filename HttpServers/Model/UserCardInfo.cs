using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    [SugarTable("UserCardInfo")]
    public class UserCardInfo
    {
        [SugarColumn(ColumnName = "ID", Length = 50)]
        public int ID
        {
            get; set;
        }
        [SugarColumn(ColumnName = "cardID", Length = 50)]
        public string cardID
        {
            get; set;
        }
        [SugarColumn(ColumnName = "userName", Length = 50)]
        public string userName
        {
            get; set;
        }
        [SugarColumn(ColumnName = "phonenum", Length = 50)]
        public string phonenum
        {
            get; set;
        }
        [SugarColumn(ColumnName = "companyName", Length = 50)]
        public string companyName
        {
            get; set;
        }
        [SugarColumn(ColumnName = "shipCode", Length = 50)]
        public string shipCode
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
    }
}
