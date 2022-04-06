using ChannelEngine.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.BusinessLogic.Interfaces
{
    public interface IChannelEngineService
    {
        List<GetTopProductsSoldDto> GetTopProductsSold(GetOrderResponseDto contentResult, int numberOfTopProducts);
    }
}
