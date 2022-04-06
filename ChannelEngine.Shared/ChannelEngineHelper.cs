using ChannelEngine.BusinessLogic.Interfaces;
using ChannelEngine.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.BusinessLogic
{
    public class ChannelEngineHelper : IChannelEngineHelper
    {
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public ChannelEngineHelper(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
        }

        public ChannelEngineHelper(IOptions<AppSettings> appSettings)
        {
            _baseUrl = appSettings.Value.ChannelEngineBaseUrl;
            _apiKey = appSettings.Value.ApiKey;
        }

        public async Task<GetOrderResponseDto> GetOrders(string status)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest($"orders?apiKey={_apiKey}&statuses={status}", Method.Get);

            RestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<GetOrderResponseDto>(response.Content);

                return result;
            }
            else
            {
                throw new Exception("Something went wrong while getting orders.");
            }
        }

        public async Task<PatchProductResponseDto> PatchProduct(string merchantProductNumber, List<PatchProductDto> model)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest($"products/{merchantProductNumber}?apikey={_apiKey}", Method.Patch);

            request.AddJsonBody<List<PatchProductDto>>(model, "application/json-patch+json");

            RestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<PatchProductResponseDto>(response.Content);

                return result;
            }
            else
            {
                throw new Exception("Something went wrong while patching product.");
            }
        }
    }
}
