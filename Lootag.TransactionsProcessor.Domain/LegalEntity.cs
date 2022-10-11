using Lootag.TransactionsProcessor.Domain;

public class LegalEntity
{
    public LegalEntity(
        Lei lei,
        string legalName,
        IEnumerable<string> bic,
        Country country
    )
    {
        Lei = lei;
        LegalName = legalName;
        Bic = bic;
        Country = country;
    }
    public Lei Lei { get; }
    public String LegalName { get; }
    public IEnumerable<string> Bic { get; }
    public Country Country { get; }
}