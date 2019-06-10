using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for GetGeneralCryptoAPIData
/// </summary>
public class GetGeneralCryptoAPIData
{
    //API key for coinmarketcap API provider
    private static string API_KEY = "c35278ad-1534-4e94-8092-2643a8ec81fe";

    public GeneralCryptoDataClass GetGeneralCryptoAPIDataMain(string selectedFiatCurrency)
    {
        GeneralCryptoDataClass result = null;

        try
        {
            result = GetGeneralCryptoData(selectedFiatCurrency);
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    //Function that gets data about many currencies
    private GeneralCryptoDataClass GetGeneralCryptoData(string selectedFiatCurrency)
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/global-metrics/quotes/latest");

        //Define the paramaters needed for this API call
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        queryString["convert"] = selectedFiatCurrency;

        URL.Query = queryString.ToString();

        //Create a header that will be sent with the API call URL
        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        var jsonResult = client.DownloadString(URL.ToString());

        GeneralCryptoDataClass result = new GeneralCryptoDataClass();

        var allData = JObject.Parse(jsonResult);
        var data = allData["data"];        

        if (data != null)
        {
            result.activeCryptocurrencies = data["active_cryptocurrencies"].ToString();
            result.activeExchanges = data["active_exchanges"].ToString();
            result.btcDominance = Math.Round((Double)data["btc_dominance"], 2).ToString();

            var quote = data["quote"];
            var innerQuoute = quote[selectedFiatCurrency];

            result.totalMarketCap = Math.Round((Double)innerQuoute["total_market_cap"], 2).ToString();
            result.totalVolume24h = Math.Round((Double)innerQuoute["total_volume_24h"], 2).ToString();

        }

        return result;
    }

}