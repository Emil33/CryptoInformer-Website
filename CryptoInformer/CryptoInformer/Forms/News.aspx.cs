using System;
using System.Collections.Generic;

public partial class Forms_News : System.Web.UI.Page
{
    List<NewsDataClass> APIResult;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get currency name
        string currencyName = Request.QueryString["currencyName"];

        if (currencyName != null)
        {
            buildNewsCoinSpecificCards(currencyName);
        }
        else
        {
            buildNewsCards();
        }

        //Clear home page session varibales
        Session["numberOfCurrencies"] = null;
        Session["selectedFiatCurrency"] = null;
        Session["selectedSortMethod"] = null;
    }

    protected List<NewsDataClass> buildNewsCards()
    {
        GetNewsAPIData getNewsAPIData = new GetNewsAPIData();
        APIResult = getNewsAPIData.GetNewsAPIMain();

        return APIResult;
    }
     
    protected List<NewsDataClass> buildNewsCoinSpecificCards(string currencyName)
    {
        GetNewsAPIData getNewsAPIData = new GetNewsAPIData();
        APIResult = getNewsAPIData.GetNewsAPISingleCryptoCurrency(currencyName);

        return APIResult;
    }

    protected List<NewsDataClass> aPIResult { get { return APIResult; } }
}