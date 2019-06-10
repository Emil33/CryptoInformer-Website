using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataReader reader;
    String queryStr;

    List<CryptocurrencyDataClass> APIResult;
    List<CryptocurrencyRatingsDataClass> APIRatingsResult;
    public GeneralCryptoDataClass APIGeneralResult;

    public bool ratingPresent = false;
    string defaultFiatCurrency = "USD";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if a different number of cryptocurrencies has been selected
        if (!string.IsNullOrEmpty(Request.QueryString["numberOfCurrencies"]))
        {
            int numberOfCurrencies = Int32.Parse(Request.QueryString["numberOfCurrencies"]);

            //Save the selected number of cryptocurrencies to be displayed into the session
            Session["numberOfCurrencies"] = numberOfCurrencies.ToString();

            checkSelectedFiatCurrency(numberOfCurrencies);
        }
        else
        {
            if(!string.IsNullOrEmpty(((string)(Session["numberOfCurrencies"]))))
            {
                checkSelectedFiatCurrency(Int32.Parse(((string)(Session["numberOfCurrencies"]))));
            }
            else
            {
                checkSelectedFiatCurrency(50);
            }
        }

        getCryptocurrencyRatings();
    }

    protected List<CryptocurrencyDataClass> getCryptoCurrencyTableData(string selectedFiatCurrency, int listSize, string selectedSortMethod)
    {
        GetCurrencyAPIData getCurrencyAPIData = new GetCurrencyAPIData();
        APIResult = getCurrencyAPIData.GetCurrencyAPIMain(selectedFiatCurrency, listSize, selectedSortMethod);

        return APIResult;
    }

    protected List<CryptocurrencyDataClass> aPIResult { get { return APIResult; } }
    

    protected void checkSelectedFiatCurrency(int numberOfCurrencies)
    {
        //Check if the user has selected a fiat currency
        if (!string.IsNullOrEmpty(Request.QueryString["selectedFiatCurrency"]))
        {
            //Get the currenct fiat currency in the session
            string selectedFiatCurrency = Request.QueryString["selectedFiatCurrency"];

            //Save the selected fiat currency into the session
            Session["selectedFiatCurrency"] = selectedFiatCurrency;

            checkSelectedSort(selectedFiatCurrency, numberOfCurrencies);
            getGeneralCryptoDataFunction(selectedFiatCurrency);
        }
        else
        {
            if (!string.IsNullOrEmpty((string)(Session["selectedFiatCurrency"])))
            {
                checkSelectedSort(((string)(Session["selectedFiatCurrency"])), numberOfCurrencies);
                getGeneralCryptoDataFunction(defaultFiatCurrency);
            }
            else
            {
                checkSelectedSort(defaultFiatCurrency, numberOfCurrencies);
                getGeneralCryptoDataFunction(defaultFiatCurrency);
            }
        }
    }

    protected void checkSelectedSort(string selectedFiatCurrency, int numberOfCurrencies)
    {
        //Check if the user has selected a fiat currency
        if (!string.IsNullOrEmpty(Request.QueryString["selectedSortMethod"]))
        {
            //Get the selected sort method
            string selectedSortMethod = Request.QueryString["selectedSortMethod"];

            //Save the selected sort method into the session
            Session["selectedSortMethod"] = selectedSortMethod;

            getCryptoCurrencyTableData(selectedFiatCurrency, numberOfCurrencies, selectedSortMethod);

        }
        else
        {
            if (!string.IsNullOrEmpty((string)(Session["selectedSortMethod"])))
            {
                getCryptoCurrencyTableData(selectedFiatCurrency, numberOfCurrencies, ((string)(Session["selectedSortMethod"])));
            }
            else
            {
                getCryptoCurrencyTableData(selectedFiatCurrency, numberOfCurrencies, "market_cap");
            }
            
        }
    }


    //Getting the cryptocurrency ratings
    protected List<CryptocurrencyRatingsDataClass> getCryptocurrencyRatings()
    {
        APIRatingsResult = readCryptocurrencyRatings();

        return APIRatingsResult;
    }

    protected List<CryptocurrencyRatingsDataClass> aPIRatingsResult { get { return APIRatingsResult; } }

    private List<CryptocurrencyRatingsDataClass> readCryptocurrencyRatings()
    {
        List<CryptocurrencyRatingsDataClass> ratingList = new List<CryptocurrencyRatingsDataClass>();       

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "SELECT * FROM currency;";
        cmd = new SqlCommand(queryStr, conn);

        reader = cmd.ExecuteReader();

        while (reader.HasRows && reader.Read())
        {
            CryptocurrencyRatingsDataClass rating = new CryptocurrencyRatingsDataClass();

            rating.cryptocurrencySymbol = reader.GetString(reader.GetOrdinal("currencySymbol"));
            rating.rating = reader.GetInt32(reader.GetOrdinal("rating")).ToString();

            ratingList.Add(rating);
        }
        
        conn.Close();

        return ratingList;
    }


    protected GeneralCryptoDataClass getGeneralCryptoDataFunction(string selectedFiatCurrency)
    {
        GetGeneralCryptoAPIData getGeneralCryptoAPIData = new GetGeneralCryptoAPIData();
        APIGeneralResult = getGeneralCryptoAPIData.GetGeneralCryptoAPIDataMain(selectedFiatCurrency);

        return APIGeneralResult;
    }

    protected GeneralCryptoDataClass aPIGeneralResult { get { return APIGeneralResult; } }
}

