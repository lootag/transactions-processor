using Lootag.TransactionsProcessor.Domain;

namespace Lootag.TransactionsProcessor.Services;
public interface ISecurityTransactionService
{
    Task<IEnumerable<SecurityTransaction>> RetrieveSecurityTransactions();
}
public class SecurityTransactionService : ISecurityTransactionService
{
    public SecurityTransactionService(IGleifService gleifService)
    {
        _gleifService = gleifService;
    }
    private readonly IGleifService _gleifService;
    public async Task<IEnumerable<SecurityTransaction>> RetrieveSecurityTransactions()
    {
        var rawSecurityTransactionsResponse = await RetrieveRawTransactions();
        var leis = rawSecurityTransactionsResponse.Leis();
        var legalEntities = await _gleifService.RetrieveLegalEntities(leis);
        return rawSecurityTransactionsResponse.ToSecurityTransactions(legalEntities);
    }

    //Talking to Serkan I found out that in reality the users don't have the csv file
    //attached to the assignment. That data comes from an external source. This function
    //simulates that external source, and for the sake of simplicity it reads the csv from
    //the file system.
    private async Task<RawSecurityTransactionsResponse> RetrieveRawTransactions()
    {
        var inputCsvFilePath = "input.csv";
        var lines = await File.ReadAllLinesAsync(inputCsvFilePath);
        var csvRows = lines.TakeLast(lines.Length - 1);
        var rawTransactionDtos = csvRows.Select(row => row.Split(",").ToRawTransactionDto());
        return new RawSecurityTransactionsResponse(rawTransactionDtos);
    }
}
