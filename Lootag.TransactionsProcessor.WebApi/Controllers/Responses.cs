using Lootag.TransactionsProcessor.Domain;

public record GetSecurityTransactionsResponse(
    IEnumerable<SecurityTransactionDto> SecurityTransactions
)
{
    public static GetSecurityTransactionsResponse FromSecurityTransactions(IEnumerable<SecurityTransaction> securityTransactions)
    {
        return new GetSecurityTransactionsResponse(
            securityTransactions.Select(
                transaction => SecurityTransactionDto.FromSecurityTransaction(transaction)
            )
        );
    }
}