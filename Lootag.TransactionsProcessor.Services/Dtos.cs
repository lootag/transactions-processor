namespace Lootag.TransactionsProcessor.Services;

using Lootag.TransactionsProcessor.Domain;

internal record RawTransactionDto(
    string Uti,
    string Isin,
    float Notional,
    string NotionalCurrency,
    string Type,
    string TransactionDateTime,
    float Rate,
    string Lei
)
{
    internal SecurityTransaction ToSecurityTransaction(LegalEntity legalEntity)
    {
        return new SecurityTransaction(
            Uti,
            Isin,
            Notional,
            Enum.Parse<NotionalCurrency>(NotionalCurrency),
            Enum.Parse<TransactionType>(Type),
            DateTime.Parse(TransactionDateTime),
            Rate,
            legalEntity
        );
    }
}
internal record GleifResponseDataDto(
    string Type,
    string Id,
    GleifResponseAttributesDto Attributes
);
internal record GleifResponseAttributesDto(
    string Lei,
    LegalEntityDto Entity,
    IEnumerable<string> Bic
)
{
    internal LegalEntity ToLegalEntity()
    {
        return new LegalEntity(
            new Lei(Lei),
            Entity.LegalName.Name,
            Bic,
            Enum.Parse<Country>(Entity.LegalAddress.Country)
        );
    }
}

internal record LegalEntityDto(
    LegalNameDto LegalName,
    LegalAddressDto LegalAddress
);
internal record LegalNameDto(
    string Name
);

internal record LegalAddressDto(
    string Country
);