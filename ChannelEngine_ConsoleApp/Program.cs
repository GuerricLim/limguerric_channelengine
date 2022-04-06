using ChannelEngine.BusinessLogic;
using ChannelEngine.BusinessLogic.Interfaces;
using ChannelEngine.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine_ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string _baseUrl = "https://api-dev.channelengine.net/api/v2/";
            string _apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
            int numberOfTopProductsToShow = 5;
            int newStockLevel = 25;

            IChannelEngineHelper _channelEngineHelper = new ChannelEngineHelper(_baseUrl, _apiKey);
            IChannelEngineService _channelEngineService = new ChannelEngineService();

            //get orders
            var getOrdersThatAreInProgress = await _channelEngineHelper.GetOrders("IN_PROGRESS");

            var getTopFiveProductsSold = _channelEngineService.GetTopProductsSold(getOrdersThatAreInProgress, numberOfTopProductsToShow);

            if (getTopFiveProductsSold.Any())
            {
                Console.WriteLine($"Top {numberOfTopProductsToShow} Products Sold");

                for (int i=0; i< getTopFiveProductsSold.Count; i++)
                {
                    Console.WriteLine($"{(i + 1).ToString()}. {getTopFiveProductsSold[i].ProductName} GTIN: {getTopFiveProductsSold[i].Gtin} Total Quantity: {getTopFiveProductsSold[i].TotalQuantity}");
                }

                Console.WriteLine("----");

                Console.WriteLine("Select product to set stock level to 25 (Indicate the # of the product):");
                Console.WriteLine("To exit the app, type in EXIT");
                var userInput = Console.ReadLine();

                while(true)
                {
                    var maximumOptions = getTopFiveProductsSold.Count;

                    if (userInput == "EXIT")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        int i;

                        if (int.TryParse(userInput, out i))
                        {
                            var getProductToSetStock = getTopFiveProductsSold[i - 1];

                            List<PatchProductDto> patches = new List<PatchProductDto>
                            {
                                new PatchProductDto
                                {
                                    value = newStockLevel,
                                    path = "Stock",
                                    op = "replace"
                                }
                            };

                            var updateStockLevel = await _channelEngineHelper.PatchProduct(getProductToSetStock.MerchantProductNo, patches);

                            if (updateStockLevel.Success)
                            {
                                Console.WriteLine("-----");
                                Console.WriteLine($"Updated {getProductToSetStock.ProductName}'s Stock to {newStockLevel}");
                                Console.WriteLine("-----");
                            }

                            Console.WriteLine("Select product to set stock level to 25 (Indicate the # of the product):");
                            Console.WriteLine("To exit the app, type in EXIT");
                            userInput = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                            userInput = Console.ReadLine();
                        }
                    }
                }


             

             

               
            }
            else
            {
                Console.WriteLine("No Products Sold");
            }

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

    }
}
