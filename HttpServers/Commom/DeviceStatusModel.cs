using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Commom
{
    public class DeviceStatusModel
    {
        public int ID
        {
            get;set;
        }
        public string deviceNo
        {
            get; set;
        }
        /// <summary>
        /// 设备状态:使用中/未使用/挂起
        /// </summary>
        public string status
        {
            get;set;
        }
        /// <summary>
        /// 使用前表读数
        /// </summary>
        public string beforeDegree
        {
            get;set;
        }
        /// <summary>
        /// 当前表读数
        /// </summary>
        public string currentDegree
        {
            get;set;
        }
        public string cardID
        {
            get;set;
        }
        public bool isUse
        {
            get; set;
        }
        public string companyName 
        {
            get;set;
        }
        public string shipCode
        {
            get;set;
        }
        public double useDegree
        {
            get;set;
        }
    }
}
