using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for GetAPIData
/// </summary>
public class GetCurrencyAPIData
{
    //API key for coinmarketcap API provider
    private static string API_KEY = "c35278ad-1534-4e94-8092-2643a8ec81fe";

    public List<CryptocurrencyDataClass> GetCurrencyAPIMain(string pickedCurrencySymbol, int numberOfCurrencies, string selectedSortMethod)
    {
        List<CryptocurrencyDataClass> result = null;

        try
        {
            result = GetCryptoCurrencies(pickedCurrencySymbol, numberOfCurrencies, selectedSortMethod);            
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    public CryptocurrencyDataClass GetSingleCurrencyAPIMain(string pickedCurrencySymbol, string pickedCryptoCurrencySymbol)
    {
        CryptocurrencyDataClass result = null;

        try
        {
            result = GetCryptoCurrency(pickedCurrencySymbol, pickedCryptoCurrencySymbol);
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    //Function that gets data about many currencies
    private List<CryptocurrencyDataClass> GetCryptoCurrencies(string pickedCurrencySymbol, int numberOfCurrencies, string selectedSortMethod)
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        //Define the paramaters needed for this API call
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = numberOfCurrencies.ToString();
        queryString["convert"] = pickedCurrencySymbol;
        queryString["sort"] = selectedSortMethod;

        URL.Query = queryString.ToString();

        //Create a header that will be sent with the API call URL
        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        var jsonResult = client.DownloadString(URL.ToString());

        List<CryptocurrencyDataClass> result = new List<CryptocurrencyDataClass>();

        var allData = JObject.Parse(jsonResult);
        var data = allData["data"];

        if (data != null)
        {
            //Loop through cryptocurrency info and add them to the currency object list
            foreach (var APIData in data.Children())
            {
                CryptocurrencyDataClass currency = new CryptocurrencyDataClass();
                currency.name = APIData["name"].ToString();
                currency.symbol = APIData["symbol"].ToString();

                if(!string.IsNullOrEmpty(APIData["circulating_supply"].ToString()))
                {
                    currency.circulatingSupply = Convert.ToDouble(APIData["circulating_supply"]);
                }
                else
                {
                    currency.circulatingSupply = 0;
                }                

                var quote = APIData["quote"];
                var innerQuoute = quote[pickedCurrencySymbol];

                currency.currencySymbol = pickedCurrencySymbol;
                currency.price = Math.Round((Double)innerQuoute["price"], 2);
                currency.marketCap = Math.Round((Double)innerQuoute["market_cap"], 2);
                               
                if (!string.IsNullOrEmpty(innerQuoute["volume_24h"].ToString()))
                {
                    currency.volume24Hours = Math.Round((Double)innerQuoute["volume_24h"], 2);
                }
                else
                {
                    currency.volume24Hours = 0;
                }

                if (!string.IsNullOrEmpty(innerQuoute["percent_change_24h"].ToString()))
                {
                    currency.priceChange24Hours = Math.Round((Double)innerQuoute["percent_change_24h"], 2);
                }
                else
                {
                    currency.priceChange24Hours = 0;
                }
                                

                result.Add(currency);
            }
        }

        return result;
    }

    //Function that get data about one specific currency
    private CryptocurrencyDataClass GetCryptoCurrency(string pickedCurrencySymbol, string pickedCryptoCurrencySymbol)
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

        //Define the paramaters needed for this API call
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["symbol"] = pickedCryptoCurrencySymbol;

        queryString["convert"] = pickedCurrencySymbol;

        URL.Query = queryString.ToString();

        //Create a header that will be sent with the API call URL
        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        var jsonResult = client.DownloadString(URL.ToString());

        CryptocurrencyDataClass result = new CryptocurrencyDataClass();

        var allData = JObject.Parse(jsonResult);
        var dataOut = allData["data"];

        var dataIn = dataOut[pickedCryptoCurrencySymbol];

        if (dataIn != null)
        {
            CryptocurrencyDataClass currency = new CryptocurrencyDataClass();
            currency.name = dataIn["name"].ToString();
            currency.symbol = dataIn["symbol"].ToString();
            currency.circulatingSupply = Convert.ToDouble(dataIn["circulating_supply"]);

            //Get fiat currency data
            var quote = dataIn["quote"];
            var innerQuouteFiat = quote[pickedCurrencySymbol];

            currency.currencySymbol = pickedCurrencySymbol;
            currency.price = Math.Round((Double)innerQuouteFiat["price"], 2);
            currency.marketCap = Math.Round((Double)innerQuouteFiat["market_cap"], 2);
            currency.volume24Hours = Math.Round((Double)innerQuouteFiat["volume_24h"], 2);

            result = currency;
        }

        return result;
    }

}
