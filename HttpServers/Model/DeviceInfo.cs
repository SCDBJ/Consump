using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    [SugarTable("DeviceInfo")]
    public class DeviceInfo
    {
        [SugarColumn(ColumnName = "deviceNo", Length = 50,IsPrimaryKey =true)]
        public string deviceNo
        {
            get; set;
        }
        /// <summary>
        /// 设备类型(水表/电表)
        /// </summary>
        [SugarColumn(ColumnName = "deviceType")]
        public string deviceType
        {
            get; set;
        }
        /// <summary>
        /// 客户端是否主动请求关闭
        /// </summary>
        [SugarColumn(ColumnName = "deviceStatus")]
        public string deviceStatus
        {
            get; set;
        }
       
    }
}
