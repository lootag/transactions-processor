using Lootag.TransactionsProcessor.Domain;

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
)
{
    public static SecurityTransactionDto FromSecurityTransaction(SecurityTransaction securityTransaction)
    {
        return new SecurityTransactionDto(
            securityTransaction.Uti,
            securityTransaction.Isin,
            securityTransaction.Notional,
            securityTransaction.NotionalCurrency.ToString(),
            securityTransaction.Type.ToString(),
            securityTransaction.TransactionDateTime.ToString(),
            securityTransaction.Rate,
            securityTransaction.LegalEntity.Lei.Value,
            securityTransaction.LegalEntity.LegalName,
            securityTransaction.LegalEntity.Bic,
            securityTransaction.TransactionCosts()
        );
    }
}