namespace Lootag.TransactionsProcessor.Services;
internal static class StringArrayExtensions
{
    internal static RawTransactionDto ToRawTransactionDto(this string[] transactionsCsvRow)
    {
        return new RawTransactionDto(
            Uti: transactionsCsvRow[0],
            Isin: transactionsCsvRow[1],
            Notional: float.Parse(transactionsCsvRow[2]),
            NotionalCurrency: transactionsCsvRow[3],
            Type: transactionsCsvRow[4],
            TransactionDateTime: transactionsCsvRow[5],
            Rate: float.Parse(transactionsCsvRow[6]),
            Lei: transactionsCsvRow[7]
        );
    }
}