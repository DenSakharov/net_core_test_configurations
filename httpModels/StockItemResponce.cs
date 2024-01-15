using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpModels
{
    public class StockItemResponce
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
}
