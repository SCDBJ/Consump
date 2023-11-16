using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    [SugarTable("WaterMeterTemp")]
    public class WaterMeterTemp
    {
        //[SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
        //public int ID
        //{
        //    get; set;
        //}
        [SugarColumn(ColumnName = "deviceNo", Length = 50)]
        public string deviceNo
        {
            get; set;
        }
        [SugarColumn(ColumnName = "degree", Length = 50)]
        public string degree
        {
            get; set;
        }
        [SugarColumn(ColumnName = "cardNo", Length = 50)]
        public string cardNo
        {
            get; set;
        }
        [SugarColumn(ColumnName = "tag", Length = 10)]
        public string tag
        {
            get; set;
        }
        [SugarColumn(ColumnName = "storetime")]
        public DateTime storetime
        {
            get; set;
        }
    }
}
