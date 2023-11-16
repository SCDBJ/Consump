using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Commom
{
    public class DeviceMappingListModel
    {
        public string status
        {
            get; set;
        }
        public string msg
        {
            get; set;
        }
        public List<DeviceMappingModel> deviceList
        {
            get; set;
        }
    }
}
