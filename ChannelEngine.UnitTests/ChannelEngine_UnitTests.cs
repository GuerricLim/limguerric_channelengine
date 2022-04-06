using ChannelEngine.BusinessLogic;
using ChannelEngine.BusinessLogic.Interfaces;
using ChannelEngine.BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ChannelEngine.UnitTests
{
    [TestClass]
    public class ChannelEngine_UnitTests
    {
        [TestMethod]
        public void GetTopFiveProductsSold()
        {
            IChannelEngineService _channelEngineService = new ChannelEngineService();


            //preparation of data

            var expectedTopFive = new List<GetTopProductsSoldDto>
            {
                new GetTopProductsSoldDto {
                    ProductName = "Test Product 5",
                    Gtin = "123452678",
                    TotalQuantity = 50,
                    MerchantProductNo = "1234522677"
                },
                 new GetTopProductsSoldDto {
                    ProductName = "Test Product 3",
                    Gtin = "234563",
                    TotalQuantity = 15,
                    MerchantProductNo = "4657687"
                },
                  new GetTopProductsSoldDto {
                    ProductName = "Test Product 2",
                    Gtin = "9876543",
                    TotalQuantity = 10,
                    MerchantProductNo = "12345647"
                },
                   new GetTopProductsSoldDto {
                    ProductName = "Test Product 1",
                    Gtin = "12345678",
                    TotalQuantity = 5,
                    MerchantProductNo = "12345677"
                },
                    new GetTopProductsSoldDto {
                    ProductName = "Test Product 4",
                    Gtin = "123458878",
                    TotalQuantity = 2,
                    MerchantProductNo = "123455677"
                }
            };

            var dummyData = new GetOrderResponseDto
            {
                Content = new System.Collections.Generic.List<GetOrderDetails>()
            };

            dummyData.Content.Add(new GetOrderDetails
            {
                Id = 1,
                Lines = new System.Collections.Generic.List<GetOrderLinesDto>
                {
                    new GetOrderLinesDto
                    {
                        Gtin = "12345678",
                        Quantity = 5,
                        Description = "Test Product 1",
                        MerchantProductNo = "12345677"
                    }
                }
            });

            dummyData.Content.Add(new GetOrderDetails
            {
                Id = 1,
                Lines = new System.Collections.Generic.List<GetOrderLinesDto>
                {
                    new GetOrderLinesDto
                    {
                        Gtin = "9876543",
                        Quantity = 10,
                        Description = "Test Product 2",
                        MerchantProductNo = "12345647"
                    }
                }
            });

            dummyData.Content.Add(new GetOrderDetails
            {
                Id = 1,
                Lines = new System.Collections.Generic.List<GetOrderLinesDto>
                {
                    new GetOrderLinesDto
                    {
                        Gtin = "234563",
                        Quantity = 15,
                        Description = "Test Product 3",
                        MerchantProductNo = "4657687"
                    }
                }
            });

            dummyData.Content.Add(new GetOrderDetails
            {
                Id = 1,
                Lines = new System.Collections.Generic.List<GetOrderLinesDto>
                {
                    new GetOrderLinesDto
                    {
                        Gtin = "123458878",
                        Quantity = 2,
                        Description = "Test Product 4",
                        MerchantProductNo = "123455677"
                    }
                }
            });

            dummyData.Content.Add(new GetOrderDetails
            {
                Id = 1,
                Lines = new System.Collections.Generic.List<GetOrderLinesDto>
                {
                    new GetOrderLinesDto
                    {
                        Gtin = "123452678",
                        Quantity = 50,
                        Description = "Test Product 5",
                        MerchantProductNo = "1234522677"
                    }
                }
            });

            var getTopOrders = _channelEngineService.GetTopProductsSold(dummyData, 5);


            //test

            for (int i =0; i<getTopOrders.Count; i++)
            {
                var expected = expectedTopFive[i];
                var actual = getTopOrders[i];

                Assert.AreEqual(expected.Gtin, actual.Gtin);
                Assert.AreEqual(expected.ProductName, actual.ProductName);
                Assert.AreEqual(expected.TotalQuantity, actual.TotalQuantity);
                Assert.AreEqual(expected.MerchantProductNo, actual.MerchantProductNo);
            }


        }
    }
}
