using RestSharp;
using System;
using System.Net;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for GetChartPricesAPIData
/// </summary>
public class GetChartPricesAPIData
{
    //API key for coinmarketcap API provider
    private static string API_KEY = "c35278ad-1534-4e94-8092-2643a8ec81fe";


    public ChartPricesDataClass[] GetChartPricesAPIMain(string cryptoCurrencySymbol)
    {
        ChartPricesDataClass[] result = null;

        try
        {
            result = GetChartData(cryptoCurrencySymbol, "USD");
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    //Function that gets chart data
    private ChartPricesDataClass[] GetChartData(string cryptocurrencySymbol, string fiatSymbol)
    {
        string callString = "https://rest.coinapi.io/v1/ohlcv/" + cryptocurrencySymbol + "/" + fiatSymbol + "/latest?period_id=1MTH&limit=12";

        var client = new RestClient(callString);
        var request = new RestRequest(Method.GET);
        request.AddHeader("X-CoinAPI-Key", "99D3F0B0-694D-4B2D-A9C0-8FDEB5D6CA6F");
        IRestResponse response = client.Execute(request);
        String releases = response.Content.ToString();

        JavaScriptSerializer js = new JavaScriptSerializer();
        ChartPricesDataClass[] prices = js.Deserialize<ChartPricesDataClass[]>(releases);

        return prices;
    }

}