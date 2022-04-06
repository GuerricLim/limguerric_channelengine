using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.BusinessLogic.Models
{
    public class PatchProductDto
    {
        public int value { get; set; }
        public string path { get; set; }
        public string op { get; set; } = "replace";
    }
}
