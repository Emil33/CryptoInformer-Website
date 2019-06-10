using System.Collections.Generic;

/// <summary>
/// Summary description for NewsDataClass
/// </summary>
public class NewsDataClass
{
    public string description { get; set; }
    public string title { get; set; }
    public string url { get; set; }
    public string source { get; set; }
    public string imageURL { get; set; }
    public List<string> coins { get; set; }
    public List<SimilarArticles> similarArticlesList { get; set; }

    public NewsDataClass()
    {

    }
}

public class SimilarArticles
{
    public string title { get; set; }
    public string source { get; set; }
    public string url { get; set; }

    public SimilarArticles()
    {

    }
}