using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model
{
    public class StatisticIncomeModel
    {
        public int issueYear
        {
            get; set;
        }
        public int issueMonth
        {
            get; set;
        }
        public decimal incomeAmount
        {
            get; set;
        }
        public decimal spendAmount
        {
            get; set;
        }
        public decimal netincomeAmount
        {
            get; set;
        }
        public string incomeType
        {
            get; set;
        }
    }
}
