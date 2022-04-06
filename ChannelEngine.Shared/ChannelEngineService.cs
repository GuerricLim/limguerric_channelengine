using ChannelEngine.BusinessLogic.Interfaces;
using ChannelEngine.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChannelEngine.BusinessLogic
{
    public class ChannelEngineService : IChannelEngineService
    {
        public List<GetTopProductsSoldDto> GetTopProductsSold(GetOrderResponseDto contentResult, int numberOfTopProducts)
        {
            var topProductsSold = new List<GetTopProductsSoldDto>();

            if (contentResult.Content.Any())
            {
                var getAllLineItemsGroupedByProduct = contentResult.Content.SelectMany(x => x.Lines).GroupBy(x => x.MerchantProductNo).OrderByDescending(x => x.Sum(z =>z.Quantity)).Take(numberOfTopProducts).ToList();

                topProductsSold.AddRange(getAllLineItemsGroupedByProduct.Select(x => new GetTopProductsSoldDto
                {
                    Gtin = x.First().Gtin,
                    ProductName = x.First().Description,
                    TotalQuantity = x.Sum(z => z.Quantity),
                    MerchantProductNo = x.Key
                }));
            }

            return topProductsSold;
        }
    }
}
