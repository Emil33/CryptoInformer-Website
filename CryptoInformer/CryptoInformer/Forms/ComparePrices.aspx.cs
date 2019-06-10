using System;
using System.Collections.Generic;

public partial class Forms_ComparePrices : System.Web.UI.Page
{
    List<ComparePricesDataClass> APIResult;

    public string currencySymbol;
    string defaultFiatCurrency = "USD";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if a cryptocurrency has been selected
        if (!string.IsNullOrEmpty(Request.QueryString["currencySymbol"]))
        {
            currencySymbol = Request.QueryString["currencySymbol"];

            checkSelectedFiatCurrency(currencySymbol);
        }
        else
        {
            Console.WriteLine("NO DATA PROVIDED OR COULD NOT BE READ");
        }        
    }

    protected List<ComparePricesDataClass> getPricesFromDifferentExchanges(string selectedFiatCurrency, string currencySymbol)
    {
        GetComparePricesAPIData getComparePricesAPIData = new GetComparePricesAPIData();
        APIResult = getComparePricesAPIData.GetComparePricesAPImain(selectedFiatCurrency, currencySymbol);

        return APIResult;
    }

    protected List<ComparePricesDataClass> aPIResult { get { return APIResult; } }

    protected void checkSelectedFiatCurrency(string currencySymbol)
    {
        //Check if the user has selected a fiat currency
        if (!string.IsNullOrEmpty(Request.QueryString["selectedFiatCurrency"]))
        {
            //Get the currenct fiat currency in the session
            string selectedFiatCurrency = Request.QueryString["selectedFiatCurrency"];

            getPricesFromDifferentExchanges(selectedFiatCurrency, currencySymbol);
        }
        else
        {
            getPricesFromDifferentExchanges(defaultFiatCurrency, currencySymbol);
        }
    }

}