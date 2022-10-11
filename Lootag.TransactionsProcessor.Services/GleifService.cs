using System.Text.Json;
using System.Net.Http.Json;
using Polly;
using Lootag.TransactionsProcessor.Domain;

namespace Lootag.TransactionsProcessor.Services;

public interface IGleifService
{
    Task<IEnumerable<LegalEntity>> RetrieveLegalEntities(IEnumerable<Lei> leis);
}

public class GleifService : IGleifService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public GleifService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IEnumerable<LegalEntity>> RetrieveLegalEntities(IEnumerable<Lei> leis)
    {
        var numberOfRetries = 5;
        var retryPolicy = Policy.Handle<HttpRequestException>()
                           .WaitAndRetryAsync(numberOfRetries, retryAttempt =>
                                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                            );
        var gleifResponse = await retryPolicy.ExecuteAsync(async () => await RequestLegalEntitesFromGleifApi(leis));
        return gleifResponse!.ToLegalEntities();
    }

    private async Task<GleifResponse?> RequestLegalEntitesFromGleifApi(
        IEnumerable<Lei> leis
    )
    {
        var baseUrl = "https://api.gleif.org/api/v1/lei-records?filter[lei]=";
        var leisUrlParameter = leis
            .Select(lei => lei.Value)
            .Aggregate((value1, value2) => $"{value1},{value2}");
        var requestUrl = $"{baseUrl}{leisUrlParameter}";
        var client = _httpClientFactory.CreateClient();
        return await client.GetFromJsonAsync<GleifResponse>(requestUrl);
    }
}