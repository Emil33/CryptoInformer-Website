using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for GetComparePricesAPIDataClass
/// </summary>
public class GetComparePricesAPIData
{
    private static string API_KEY = "ffac7f792b2b2390693949fc605fc6f8ea74e4d4b34d36b760fcab8ff36653f3";    

    public List<ComparePricesDataClass> GetComparePricesAPImain(string pickedCurrencySymbol, string pickedCryptoCurrencySymbol)
    {
        List<ComparePricesDataClass> result = null;

        try
        {
            result = GetCryptoCurrencies(pickedCurrencySymbol, pickedCryptoCurrencySymbol);
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    //Function that get data about many currencies
    private List<ComparePricesDataClass> GetCryptoCurrencies(string pickedCurrencySymbol, string pickedCryptoCurrencySymbol)
    {
        var URL = new UriBuilder("https://min-api.cryptocompare.com/data/top/exchanges/full");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["fsym"] = pickedCryptoCurrencySymbol;
        queryString["tsym"] = pickedCurrencySymbol;
        queryString["limit"] = "50";

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("Apikey", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        var jsonResult = client.DownloadString(URL.ToString());

        List<ComparePricesDataClass> result = new List<ComparePricesDataClass>();

        var allData = JObject.Parse(jsonResult);
        var data = allData["Data"];

        var exchanges = data["Exchanges"];

        if (data != null)
        {
            //Loop through cryptocurrency info and add them to the currency object list
            foreach (var APIDataExchanges in exchanges.Children())
            {
                ComparePricesDataClass currency = new ComparePricesDataClass();

                var coinInfo = data["CoinInfo"];

                currency.currencyName = coinInfo["FullName"].ToString();
                currency.currencyLogoURL = coinInfo["ImageUrl"].ToString();

                var aggregatedData = data["AggregatedData"];

                try
                {
                    currency.allVolume24Hours = Math.Round((Double)aggregatedData["TOTALVOLUME24H"], 2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                currency.exchangeName = APIDataExchanges["MARKET"].ToString();
                currency.cryptoSymbol = APIDataExchanges["FROMSYMBOL"].ToString();
                currency.fiatSymbol = APIDataExchanges["TOSYMBOL"].ToString();
                currency.price = Math.Round((Double)APIDataExchanges["PRICE"], 2);
                currency.volume24HoursCrypto = Math.Round((Double)APIDataExchanges["VOLUME24HOUR"], 2);
                currency.volume24HoursFiat = Math.Round((Double)APIDataExchanges["VOLUME24HOURTO"], 2);
                currency.priceChange24Hours = Math.Round((Double)APIDataExchanges["CHANGEPCT24HOUR"], 2);

                result.Add(currency);
            }
        }

        return result;
    }
}