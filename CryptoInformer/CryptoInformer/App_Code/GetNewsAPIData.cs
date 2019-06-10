using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for GetNewsAPIData
/// </summary>
public class GetNewsAPIData
{
    private static string API_KEY = "bd5af0e5e4eb37079010f33a1f9a32cd";

    public List<NewsDataClass> GetNewsAPIMain()
    {
        List<NewsDataClass> result = null;

        try
        {
            result = GetNewsArticles();
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    public List<NewsDataClass> GetNewsAPISingleCryptoCurrency(string cryptocurrencyName)
    {
        List<NewsDataClass> result = null;

        try
        {
            result = GetNewsArticlesSpecificCoin(cryptocurrencyName);
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }

    public List<NewsDataClass> GetNewsArticles()
    {
        var URL = new UriBuilder("https://cryptocontrol.io/api/v1/public/news");

        var queryString = HttpUtility.ParseQueryString(string.Empty);

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("x-api-key", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        var jsonResult = client.DownloadString(URL.ToString());

        List<NewsDataClass> result = new List<NewsDataClass>();

        var allData = JArray.Parse(jsonResult);

        if (allData != null)
        {
            //Loop through news articles and add them to the news object list
            foreach (var newsArticle in allData)
            {
                NewsDataClass news = new NewsDataClass();
                news.description = newsArticle["description"].ToString();
                news.title = newsArticle["title"].ToString();
                news.url = newsArticle["url"].ToString();

                var source = newsArticle["source"];
                news.source = source["name"].ToString();
                            
                news.imageURL = newsArticle["originalImageUrl"].ToString();

                //Getting the symbols of the coins mentioned in these articles
                var coins = newsArticle["coins"];
                List<string> coinList = new List<string>();

                foreach (var coin in coins)
                {
                    var temp = coin["tradingSymbol"].ToString();
                    temp = Regex.Replace(temp, "{", "");
                    temp = Regex.Replace(temp, "}", "");

                    coinList.Add(temp.ToUpper());
                }
                news.coins = coinList;

                var similarArticles = newsArticle["similarArticles"];
                List<SimilarArticles> similarArticlesListBuild = new List<SimilarArticles>();

                //Loop through similar news articles and add them to the similar news object list
                foreach (var article in similarArticles)
                {
                    SimilarArticles similarArticleInfo = new SimilarArticles();

                    similarArticleInfo.title = article["title"].ToString();
                    similarArticleInfo.source = article["sourceDomain"].ToString();
                    similarArticleInfo.url = article["url"].ToString();

                    similarArticlesListBuild.Add(similarArticleInfo);
                }
                news.similarArticlesList = similarArticlesListBuild;

                result.Add(news);
            }
        }

        return result;
    }

    public List<NewsDataClass> GetNewsArticlesSpecificCoin(string cryptocurrencyName)
    {
        var URL = new UriBuilder("https://cryptocontrol.io/api/v1/public/news/coin/" + cryptocurrencyName);

        var queryString = HttpUtility.ParseQueryString(string.Empty);

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("x-api-key", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        var jsonResult = client.DownloadString(URL.ToString());

        List<NewsDataClass> result = new List<NewsDataClass>();

        var allData = JArray.Parse(jsonResult);

        if (allData != null)
        {
            //Loop through news articles and add them to the news object list
            foreach (var newsArticle in allData)
            {
                NewsDataClass news = new NewsDataClass();
                news.description = newsArticle["description"].ToString();
                news.title = newsArticle["title"].ToString();
                news.url = newsArticle["url"].ToString();

                var source = newsArticle["source"];
                news.source = source["name"].ToString();

                news.imageURL = newsArticle["originalImageUrl"].ToString();

                //Getting the symbols of the coins mentioned in these articles
                var coins = newsArticle["coins"];
                List<string> coinList = new List<string>();

                foreach (var coin in coins)
                {
                    var temp = coin["tradingSymbol"].ToString();
                    temp = Regex.Replace(temp, "{", "");
                    temp = Regex.Replace(temp, "}", "");

                    coinList.Add(temp.ToUpper());
                }
                news.coins = coinList;

                var similarArticles = newsArticle["similarArticles"];
                List<SimilarArticles> similarArticlesListBuild = new List<SimilarArticles>();

                //Loop through similar news articles and add them to the similar news object list
                foreach (var article in similarArticles)
                {
                    SimilarArticles similarArticleInfo = new SimilarArticles();

                    similarArticleInfo.title = article["title"].ToString();
                    similarArticleInfo.source = article["sourceDomain"].ToString();
                    similarArticleInfo.url = article["url"].ToString();

                    similarArticlesListBuild.Add(similarArticleInfo);
                }
                news.similarArticlesList = similarArticlesListBuild;

                result.Add(news);
            }
        }

        return result;
    }
}