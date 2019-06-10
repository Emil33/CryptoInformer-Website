/// <summary>
/// Summary description for CryptocurrencyDataClass
/// </summary>
/// 
public class CryptocurrencyDataClass
{
    public string name { get; set; }
    public string symbol { get; set; }
    public double circulatingSupply { get; set; }
    public string currencySymbol { get; set; }
    public double price { get; set; }
    public double marketCap { get; set; }
    public double volume24Hours { get; set; }
    public double priceChange24Hours { get; set; }


    public CryptocurrencyDataClass()
    {
    }
}