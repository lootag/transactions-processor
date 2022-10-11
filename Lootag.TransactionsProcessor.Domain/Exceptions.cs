namespace Lootag.TransactionsProcessor.Domain;
public class UnsupportedCountryException : Exception
{
    private readonly Country _country;
    public UnsupportedCountryException(Country country)
    {
        _country = country;
    }
    public override string Message => $"Cannot compute transaction costs for country {_country.ToString()}";
}