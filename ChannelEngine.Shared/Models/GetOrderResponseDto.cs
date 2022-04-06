using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.BusinessLogic.Models
{
    public class GetOrderResponseDto
    {
        public List<GetOrderDetails> Content { get; set; }
    }

    public class GetOrderDetails
    {
        public int Id { get; set; }
        public List<GetOrderLinesDto> Lines { get; set; }
    }

    public class GetOrderLinesDto
    {
        public string Status { get; set; }
        public string Gtin { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
