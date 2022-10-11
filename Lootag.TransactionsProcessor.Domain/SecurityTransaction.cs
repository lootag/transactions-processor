namespace Lootag.TransactionsProcessor.Domain;
public class SecurityTransaction
{
    public SecurityTransaction(
        string uti,
        string isin,
        float notional,
        NotionalCurrency notionalCurrency,
        TransactionType type,
        DateTime transactionDateTime,
        float rate,
        LegalEntity legalEntity
    )
    {
        Uti = uti;
        Isin = isin;
        Notional = notional;
        NotionalCurrency = notionalCurrency;
        Type = type;
        TransactionDateTime = transactionDateTime;
        Rate = rate;
        LegalEntity = legalEntity;
    }
    public string Uti { get; }
    public string Isin { get; }
    public float Notional { get; }
    public NotionalCurrency NotionalCurrency { get; }
    public TransactionType Type { get; }
    public DateTime TransactionDateTime { get; }
    public float Rate { get; }
    public LegalEntity LegalEntity { get; }
    public float TransactionCosts()
    {
        switch (LegalEntity.Country)
        {
            case Country.GB:
                var ret = Notional * Rate - Notional;
                return ret;
            case Country.NL:
                var ret2 = Math.Abs((Notional * 1 / Rate) - Notional);
                return ret2;
            default:
                throw new UnsupportedCountryException(LegalEntity.Country);
        }
    }
}


