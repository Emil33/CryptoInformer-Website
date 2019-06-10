using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ComparePricesDataClass
/// </summary>
public class ComparePricesDataClass
{
    public string currencyName { get; set; }
    public string currencyLogoURL { get; set; }

    public double allVolume24Hours { get; set; }

    public string exchangeName { get; set; }
    public string cryptoSymbol { get; set; }
    public string fiatSymbol { get; set; }
    public double price { get; set; }
    public double volume24HoursCrypto { get; set; }
    public double volume24HoursFiat { get; set; }
    public double priceChange24Hours { get; set; }

    public ComparePricesDataClass()
    {
    }
}