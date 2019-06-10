using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CryptocurrencyCommentsDataClass
/// </summary>
public class CryptocurrencyCommentsDataClass
{
    public string ratingID { get; set; }
    public string currencySymbol { get; set; }
    public string userID { get; set; }
    public string rating { get; set; }
    public string comment { get; set; }

    public CryptocurrencyCommentsDataClass()
    {
    }
}