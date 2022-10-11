using Xunit;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Lootag.TransactionsProcessor.IntegrationTests;

public class UnitTest1
{
    [Fact]
    public async Task TestSecurityTransactionsApiReturnsNonEmptyList()
    {
        //Arrange
        var client = new HttpClient();
        var url = "http://localhost:80/api/v1/security-transactions";

        //Act
        var response = await client.GetFromJsonAsync<SecurityTransactionResponse>(url);

        //Assert
        Assert.True(response!.SecurityTransactions.ToList().Count() > 0);
    }

    [Fact]
    public async Task TestSecurityTransactionsApiDoesNotReturnAnyNullValues()
    {
        //Arrange
        var client = new HttpClient();
        var url = "http://localhost:80/api/v1/security-transactions";

        //Act
        var response = await client.GetFromJsonAsync<SecurityTransactionResponse>(url);

        //Assert
        var noTransactionHasNullFields = response!.SecurityTransactions.All(
            transaction => transaction.TransactionUti != null &&
                           transaction.Isin != null &&
                           transaction.NotionalCurrency != null &&
                           transaction.TransactionType != null &&
                           transaction.TransactionDateTime != null &&
                           transaction.Lei != null &&
                           transaction.LegalName != null &&
                           transaction.Bic != null
        );

        Assert.True(noTransactionHasNullFields);
    }
}

public record SecurityTransactionResponse(
    IEnumerable<SecurityTransactionDto> SecurityTransactions
);

public record SecurityTransactionDto(
    string TransactionUti,
    string Isin,
    float Notional,
    string NotionalCurrency,
    string TransactionType,
    string TransactionDateTime,
    float Rate,
    string Lei,
    string LegalName,
    IEnumerable<string> Bic,
    float TransactionCosts
);
