using ChannelEngine.BusinessLogic.Interfaces;
using ChannelEngine.BusinessLogic.Models;
using ChannelEngine_WebView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine_WebView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChannelEngineHelper _channelEngineHelper;
        private readonly IChannelEngineService _channelEngineService;
        private readonly string _orderStatusToQuery = "IN_PROGRESS";
        private readonly int numberOfTopProductsToShow = 5;
        private readonly int newStockLevel = 25;

        public HomeController(ILogger<HomeController> logger, IChannelEngineHelper channelEngineHelper, IChannelEngineService channelEngineService)
        {
            _channelEngineHelper = channelEngineHelper;
            _channelEngineService = channelEngineService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string addStockResult = "")
        {
            var getOrdersForInProgress = await _channelEngineHelper.GetOrders(_orderStatusToQuery);

            var getTopProducts = _channelEngineService.GetTopProductsSold(getOrdersForInProgress, numberOfTopProductsToShow);

            ViewBag.TopProducts = getTopProducts;
            ViewBag.AddStockResult = addStockResult;
            return View();
        }

        public async Task<IActionResult> AddStock(string merchantRefNumber)
        {
            List<PatchProductDto> patches = new List<PatchProductDto>
                {
                    new PatchProductDto
                    {
                        value = newStockLevel,
                        path = "Stock",
                        op = "replace"
                    }
                };

            var updateStockLevel = await _channelEngineHelper.PatchProduct(merchantRefNumber, patches);

            if (updateStockLevel.Success)
            {
                return RedirectToAction("Index", new { addStockResult = $"Adding Stock to product wwith Merchant No. {merchantRefNumber} Successful"});
            }
            else
            {
                return RedirectToAction("Index", new { addStockResult = "Something went wrong while adding stock" });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
