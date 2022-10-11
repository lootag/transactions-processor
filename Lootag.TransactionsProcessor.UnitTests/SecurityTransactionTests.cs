using Xunit;
using System;
using System.Collections.Generic;
using Lootag.TransactionsProcessor.Domain;

namespace Lootag.TransactionsProcessor.UnitTests;

public class SecurityTransactionTests
{
    [Fact]
    public void TestTransactionCostsAreComputedCorrectlyWhenCountryIsGB()
    {
        //Arrange        
        var transaction = new SecurityTransaction(
            uti: "someUti",
            isin: "someIsin",
            notional: 4000000,
            notionalCurrency: NotionalCurrency.GBP,
            type: TransactionType.Buy,
            transactionDateTime: DateTime.Now,
            rate: 0.5f,
            legalEntity: new LegalEntity(
                new Lei("someLei"),
                legalName: "someName",
                bic: new List<string>(){"BIC1", "BIC2", "BIC3"},
                country: Country.GB
            ) 
        );
        //Act
        var transactionCosts = transaction.TransactionCosts();

        //Assert
        Assert.Equal(transactionCosts, -2000000f);
    }

    [Fact]
    public void TestTransactionCostsAreComputedCorrectlyWhenCountryIsNL()
    {
        //Arrange        
        var transaction = new SecurityTransaction(
            uti: "someUti",
            isin: "someIsin",
            notional: 4000000,
            notionalCurrency: NotionalCurrency.EUR,
            type: TransactionType.Buy,
            transactionDateTime: DateTime.Now,
            rate: 0.5f,
            legalEntity: new LegalEntity(
                new Lei("someLei"),
                legalName: "someName",
                bic: new List<string>(){"BIC1", "BIC2", "BIC3"},
                country: Country.NL
            ) 
        );
        //Act
        var transactionCosts = transaction.TransactionCosts();

        //Assert
        Assert.Equal(transactionCosts, 4000000f);
    }
}
