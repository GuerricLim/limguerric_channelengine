using ChannelEngine.BusinessLogic.Interfaces;
using ChannelEngine.BusinessLogic.Models;
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
                throw new Exception("Something went wrong while processing payment.");
            }
            throw new NotImplementedException();
        }
    }
}
