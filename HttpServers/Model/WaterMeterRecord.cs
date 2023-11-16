using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    [SugarTable("WaterMeterRecord")]
    public class WaterMeterRecord
    {
        [SugarColumn(ColumnName = "cardNo", Length = 50, IsPrimaryKey = true)]
        public string cardNo
        {
            get; set;
        }
        [SugarColumn(ColumnName = "deviceNo", Length = 50)]
        public string deviceNo
        {
            get; set;
        }
        [SugarColumn(ColumnName = "beginTime")]
        public DateTime beginTime
        {
            get; set;
        }
        [SugarColumn(ColumnName = "beginDegree", Length = 50)]
        public string beginDegree
        {
            get; set;
        }
        [SugarColumn(ColumnName = "endTime")]
        public DateTime endTime
        {
            get; set;
        }
        [SugarColumn(ColumnName = "endDegree", Length = 50)]
        public string endDegree
        {
            get; set;
        }
        [SugarColumn(ColumnName = "tag", Length = 10)]
        public string tag
        {
            get; set;
        }
    }
}
