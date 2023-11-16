using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class StatisticSpendModel
    {
        public int spendYear
        {
            get; set;
        }
        public int spendMonth
        {
            get; set;
        }
        public decimal statisticAmount
        {
            get; set;
        }
    }
}
