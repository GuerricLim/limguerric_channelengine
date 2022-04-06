using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.BusinessLogic.Models
{
    public class GetTopProductsSoldDto
    {
        public string ProductName { get; set; }
        public string Gtin { get; set; }
        public int TotalQuantity { get; set; }
        public string MerchantProductNo { get; set; }
    }
}
