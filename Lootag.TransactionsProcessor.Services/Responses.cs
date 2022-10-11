using Lootag.TransactionsProcessor.Domain;

namespace Lootag.TransactionsProcessor.Services;

internal record GleifResponse(
    IEnumerable<GleifResponseDataDto> Data
)
{
    internal IEnumerable<LegalEntity> ToLegalEntities()
    {
        return Data.Select(data => data.Attributes.ToLegalEntity());
    }
}
internal record RawSecurityTransactionsResponse(
    IEnumerable<RawTransactionDto> RawTransactionDtos
)
{
    internal IEnumerable<Lei> Leis()
    {
        return RawTransactionDtos.Select(dto => new Lei(dto.Lei))
                                 .GroupBy(lei => lei.Value)
                                 .Select(g => g.First());
    }

    internal IEnumerable<SecurityTransaction> ToSecurityTransactions(
        IEnumerable<LegalEntity> legalEntities
    )
    {
        return RawTransactionDtos.Select(
            dto => dto.ToSecurityTransaction(
                GetLegalEntityWithLeiValue(legalEntities, dto.Lei)
            )
        );
    }

    private LegalEntity GetLegalEntityWithLeiValue(
        IEnumerable<LegalEntity> legalEntities,
        string leiValue
    )
    {
        return legalEntities.Where(le => le.Lei.Value.Equals(leiValue))
                            .Single();
    }
}
