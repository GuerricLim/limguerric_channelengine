using ChannelEngine.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.BusinessLogic.Interfaces
{
    public interface IChannelEngineHelper
    {
        Task<GetOrderResponseDto> GetOrders(string status);

        Task<PatchProductResponseDto> PatchProduct(string merchantProductNumber, List<PatchProductDto> model);
    }
}
