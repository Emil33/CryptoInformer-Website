using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for GetCurrencyProfileAPIData
/// </summary>
public class GetCurrencyProfileAPIData
{
    private static string API_KEY = "c35278ad-1534-4e94-8092-2643a8ec81fe";

    public CryptocurrencyProfileDataClass GetCurrencyProfileAPIMain(string pickedCryptoCurrencySymbol)
    {
        CryptocurrencyProfileDataClass result = null;

        try
        {
            result = GetCryptoCurrencyProfile(pickedCryptoCurrencySymbol);
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    public CryptocurrencyProfileDataClass GetCryptoCurrencyProfile(string pickedCryptoCurrencySymbol)
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/info");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["symbol"] = pickedCryptoCurrencySymbol;

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        var jsonResult = client.DownloadString(URL.ToString());

        CryptocurrencyProfileDataClass result = new CryptocurrencyProfileDataClass();

        var allData = JObject.Parse(jsonResult);
        var data = allData["data"];
        var coinData = data[pickedCryptoCurrencySymbol];

        if (data != null)
        {
            result.name = coinData["name"].ToString();
            result.logoURL = coinData["logo"].ToString();
            result.description = coinData["description"].ToString();

            var urls = coinData["urls"];

            result.websiteURL = urls["website"][0].ToString();

            result.explorerURL = urls["explorer"][0].ToString();
            result.sourceCodeURL = urls["source_code"][0].ToString();

  
            try
            {                
                result.messageBoardURL = urls["message_board"][0].ToString();
              
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

        }

        return result;
    }
}


//MyString.Trim();