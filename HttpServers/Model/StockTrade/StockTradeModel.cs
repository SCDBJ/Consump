using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServers.Model.StockTrade
{
    public class StockTradeModel
    {
        public string StockId
        {
            get;set;
        }
        public string StockCode
        {
            get; set;
        }
        public string StockName
        {
            get; set;
        }
        public string TradeDate
        {
            get; set;
        }
        public float TradePrice
        {
            get; set;
        }
        public int TradeShares
        {
            get; set;
        }
        public decimal ProfitLossAmount
        {
            get; set;
        }
        public string TradeType
        {
            get; set;
        }
    }
}
