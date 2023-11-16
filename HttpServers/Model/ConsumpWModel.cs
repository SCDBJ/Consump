using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class ConsumpWModel
    {
        public int spendId
        {
            get; set;
        }
        public string spendType
        {
            get; set;
        }
        public decimal spendAmount
        {
            get; set;
        }
        public string spendNote
        {
            get; set;
        }
        public DateTime spendTime
        {
            get; set;
        }
        public DateTime createTime
        {
            get; set;
        }
    }
}
