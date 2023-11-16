using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class StatiVerifyModel
    {
        public int spendYear
        {
            get; set;
        }
        public int spendMonth
        {
            get; set;
        }
        public string spendType
        {
            get; set;
        }
        public decimal statisticAmount
        {
            get; set;
        }
    }
}
