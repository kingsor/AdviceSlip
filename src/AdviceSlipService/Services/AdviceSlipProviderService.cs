using AdviceSlipService.Models;
using AdviceSlipService.Services.Interfaces;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace AdviceSlipService.Services
{
    public class AdviceSlipProviderService : IAdviceSlipProviderService
    {
        
        private readonly string _apiUriString = "https://api.adviceslip.com/";

        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private readonly CacheOptions _options;
        private readonly ILogger<AdviceSlipProviderService> _logger;

        public AdviceSlipProviderService(IMemoryCache cache, IOptions<CacheOptions> options, ILogger<AdviceSlipProviderService> logger)
        {
            _cache = cache;
            _options = options.Value;
            _cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(_options.SlidingExpirationSecs))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_options.AbsoluteExpirationSecs));
            _logger = logger;
        }
        
        public async Task<AdviceResponse> GetAdviceSlip(AdviceRequest request)
        {
            if (_cache.TryGetValue(request.Topic, out List<string> adviceList))
            {
                _logger.LogInformation($"Advice list found in cache for topic: '{request.Topic}'.");
            }
            else
            {
                _logger.LogInformation($"Advice list not found in cache for topic: '{request.Topic}'. Fetching from api.");
                adviceList = await SearchAdviceFromApi(request.Topic);
                _cache.Set(request.Topic, adviceList, _cacheEntryOptions);
            }

            return new AdviceResponse(adviceList.Take(request.Amount).ToList());
        }

        private async Task<List<string>> SearchAdviceFromApi(string topic)
        {
            var adviceList = new List<string>();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_apiUriString);
                    var response = await client.GetAsync($"advice/search/{topic}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(stringResult))
                    {
                        var jsonDom = JsonSerializer.Deserialize<JsonObject>(stringResult)!;
                        if (jsonDom.ContainsKey("message"))
                        {
                            _logger.LogInformation($"Message from service: {stringResult}");
                        }
                        else
                        {
                            var searchResult = JsonSerializer.Deserialize<SearchObject>(stringResult)!;

                            foreach (var item in searchResult.Slips)
                            {
                                if (!string.IsNullOrEmpty(item.Advice))
                                {
                                    adviceList.Add(item.Advice);
                                }
                            }
                        }
                    }
                }
                catch (HttpRequestException httpRequestException)
                {
                    _logger.LogError($"Error getting advice slip from {_apiUriString}: {httpRequestException.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error deserializing message from {_apiUriString}: {ex}");
                }

                return adviceList;
            }
        }
    }
}
