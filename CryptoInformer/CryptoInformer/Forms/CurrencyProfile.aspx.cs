using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;


public partial class Forms_CurrencyProfile : System.Web.UI.Page
{
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataReader reader;
    String queryStr;

    public string cryptoCurrencySymbol;

    CryptocurrencyDataClass APIResultFiat;
    CryptocurrencyDataClass APIResultCrypto;
    CryptocurrencyProfileDataClass APIProfileResult;
    ChartPricesDataClass[] APIChartPricesResult;

    protected void Page_Load(object sender, EventArgs e)
    {
        notificationLabel.Visible = false;

        //Check if a cryptocurrency has been selected
        if (!string.IsNullOrEmpty(Request.QueryString["currencySymbol"]))
        {
            cryptoCurrencySymbol = Request.QueryString["currencySymbol"];

            //getPricesFromDifferentExchanges(cryptoCurrencySymbol);

            getSingleCryptoCurrencyProfileDataFiat(cryptoCurrencySymbol, "USD");
            getSingleCryptoCurrencyProfileDataCrypto(cryptoCurrencySymbol);

            getProfilePageData(cryptoCurrencySymbol);

            getChartData(cryptoCurrencySymbol);

            FillChart();

            checkWhatButtonsToDisplay();
        }
        else
        {
            Console.WriteLine("NO DATA PROVIDED OR COULD NOT BE READ");
        }

        //Clear home page session varibales
        Session["numberOfCurrencies"] = null;
        Session["selectedFiatCurrency"] = null;
        Session["selectedSortMethod"] = null;

    }

    //Getting the cryptocurrency values (Fiat quote)
    protected CryptocurrencyDataClass getSingleCryptoCurrencyProfileDataFiat(string pickedCurrencySymbol, string cryptoCurrencySymbol)
    {
        GetCurrencyAPIData getCurrencyAPIData = new GetCurrencyAPIData();
        APIResultFiat = getCurrencyAPIData.GetSingleCurrencyAPIMain(cryptoCurrencySymbol, pickedCurrencySymbol);

        return APIResultFiat;
    }

    protected CryptocurrencyDataClass aPIResultFiat { get { return APIResultFiat; } }

    //Getting the cryptocurrency values (Crypto quote)
    protected CryptocurrencyDataClass getSingleCryptoCurrencyProfileDataCrypto(string cryptoCurrencySymbol)
    {
        GetCurrencyAPIData getCurrencyAPIData = new GetCurrencyAPIData();
        APIResultCrypto = getCurrencyAPIData.GetSingleCurrencyAPIMain(cryptoCurrencySymbol, cryptoCurrencySymbol);

        return APIResultCrypto;
    }

    protected CryptocurrencyDataClass aPIResultCrypto { get { return APIResultCrypto; } }
         
    //Getting the cryptocurrency profile data
    protected CryptocurrencyProfileDataClass getProfilePageData(string cryptoCurrencySymbol)
    {
        GetCurrencyProfileAPIData getCurrencyProfileAPIData = new GetCurrencyProfileAPIData();
        APIProfileResult = getCurrencyProfileAPIData.GetCurrencyProfileAPIMain(cryptoCurrencySymbol);

        return APIProfileResult;
    }

    protected CryptocurrencyProfileDataClass aPICoinDetailsResult { get { return APIProfileResult; } }

    //Getting the cryptocurrency chart data
    protected ChartPricesDataClass[] getChartData(string cryptoCurrencySymbol)
    {
        GetChartPricesAPIData getChartPricesAPIData = new GetChartPricesAPIData();
        APIChartPricesResult = getChartPricesAPIData.GetChartPricesAPIMain(cryptoCurrencySymbol);

        return APIChartPricesResult;
    }


    private void FillChart()
    {
        // Retrieve the Series to which we want to add DataPoints
        Series series = Chart1.Series["Series1"];

        Title title = new Title();
        title.Font = new Font("Arial", 18, FontStyle.Bold);
        title.Text = "Price Change Over Time";
        Chart1.Titles.Add(title);

        // Add X and Y values using AddXY() method
        for (int i = APIChartPricesResult.Length - 1; i > -1; i--)
        {
            string date = APIChartPricesResult[i].time_period_end.Substring(0, 7);
            double originalPrice = Convert.ToDouble(APIChartPricesResult[i].price_close);

            series.Points.AddXY(date, originalPrice);
        }

        Chart1.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 15);
        Chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 15);

    }

    /// <summary>
    /// Functions that deal with subscribing user to a cryptocurrency
    /// </summary>    
    public void checkWhatButtonsToDisplay()
    {
        //Get user ID
        string userID = (String)(Session["userID"]);
        if (userID != null)
        {
            bool subscribed = checkIfUserIsSubscribed(userID);
            if (subscribed)
            {
                subscribeButton.Visible = false;
                unsubscribeButton.Visible = true;
            }
            else
            {
                subscribeButton.Visible = true;
                unsubscribeButton.Visible = false;
            }

            lbresult.Visible = true;
            txtreview.Visible = true;
            btnsubmit.Visible = true;
            Rating.Visible = true;
        }
        else
        {
            subscribeButton.Visible = true;
            unsubscribeButton.Visible = false;

            lbresult.Visible = false;
            txtreview.Visible = false;
            btnsubmit.Visible = false;
            Rating.Visible = false;
        }
    }

    public void subscribeUser(object sender, EventArgs e)
    {
        //Get user user ID
        String userID = (String)(Session["userID"]);

        if(userID != null)
        {
            saveUserSubscription(userID);

            checkWhatButtonsToDisplay();
        }
        else
        {
            notificationLabel.Visible = true;
            notificationLabel.BackColor = Color.LightGray;
            notificationLabel.Text = "Warning: Only logged in users can subscribe to a currency.";
        }
    }

    public void unsubscribeUser(object sender, EventArgs e)
    {
        //Get user ID
        string userID = (String)(Session["userID"]);


        deleteUserSubscription(userID);

        notificationLabel.Visible = true;
        notificationLabel.BackColor = Color.LightGreen;
        notificationLabel.Text = "Success: You have successfully unsubscribed from: " + cryptoCurrencySymbol;

        checkWhatButtonsToDisplay();
    }

    private void saveUserSubscription(string userID)
    {

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        //If the user is not subsribed, subsribe them
        queryStr = "";

        queryStr = "INSERT INTO [currencySubscription] (currencySymbol, userID)" + "VALUES('" + cryptoCurrencySymbol + "', '" + userID + "')";
        cmd = new SqlCommand(queryStr, conn);

        cmd.ExecuteReader();

        conn.Close();

        notificationLabel.Visible = true;
        notificationLabel.BackColor = Color.LightGreen;
        notificationLabel.Text = "Success: You have successfully  subscribed to: " + cryptoCurrencySymbol;
        
    }

    private void deleteUserSubscription(string userID)
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "DELETE FROM [currencySubscription] WHERE  currencySymbol= '" + cryptoCurrencySymbol + "'AND userID = '" + userID + "' ;";
        cmd = new SqlCommand(queryStr, conn);

        cmd.ExecuteReader();

        conn.Close();
    }

    private bool checkIfUserIsSubscribed(string userID)
    {
        bool subscribed = false;

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        //Check if the user is already subscribed
        queryStr = "";

        queryStr = "SELECT * FROM [currencySubscription] WHERE userID='" + userID + "'AND currencySymbol = '" + cryptoCurrencySymbol + "' ;";
        cmd = new SqlCommand(queryStr, conn);

        reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            subscribed = true;
        }

        conn.Close();

        return subscribed;
    }

    /// <summary>
    /// Rating Functions
    /// </summary>
    public void btnsubmit_Click(object sender, EventArgs e)
    {        
        string userID = (String)(Session["userID"]);
        
        //Check if the user has already rated this cryptocurrency
        bool ratingPresent = checkIfUserRatingPresent(userID);

        //Depending if a rating is present either create a new record with the rating or update the existing rating
        if (!ratingPresent)
        {
            saveUserRating(userID);

            notificationLabel.Visible = true;
            notificationLabel.BackColor = Color.LightGreen;
            notificationLabel.Text = "Success: Your rating has been successfully saved.";

            txtreview.Text = "";
            Rating.CurrentRating = 0;
        }
        else
        {
            updateUserRating(userID);

            notificationLabel.Visible = true;
            notificationLabel.BackColor = Color.LightGreen;
            notificationLabel.Text = "Success: Your rating has been successfully updated.";

            txtreview.Text = "";
            Rating.CurrentRating = 0;
        }

        manageAverageRating();
        
    }

    //Check if the user has already given a rating for this cryptocurrency
    private bool checkIfUserRatingPresent(string userID)
    {
        bool ratingPresent = false;
        string currencySumbol = Request.QueryString["currencySymbol"];

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();
                
        queryStr = "";

        queryStr = "SELECT * FROM [currencyRating] WHERE userID='" + userID + "' AND currencySymbol = '" + currencySumbol + "' ;";
        cmd = new SqlCommand(queryStr, conn);

        reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            ratingPresent = true;
        }

        conn.Close();

        return ratingPresent;
    }

    //Save the users rating and comment to the database
    private void saveUserRating(string userID)
    {
        string currencySumbol = Request.QueryString["currencySymbol"];
        string rating = Rating.CurrentRating.ToString();
        string comment = txtreview.Text;

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "INSERT INTO [currencyRating] (currencySymbol, userID, rating, comment)" + "VALUES('" + currencySumbol + "', '" + userID + "', '" + rating + "', '" + comment + "')";
        cmd = new SqlCommand(queryStr, conn);

        cmd.ExecuteReader();

        conn.Close();
    }

    //Update users rating and comment in the database
    private void updateUserRating(string userID)
    {
        string currencySumbol = Request.QueryString["currencySymbol"];
        string rating = Rating.CurrentRating.ToString();
        string comment = txtreview.Text;

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "UPDATE [currencyRating] SET currencySymbol= '" + currencySumbol + "', " +
            "userID= '" + userID + "', rating= '" + rating + "', comment= '" + comment + "' WHERE userID = '" + userID + "' ;";
        cmd = new SqlCommand(queryStr, conn);

        cmd.ExecuteReader();

        conn.Close();
    }


    /// <summary>
    /// Updating Average Rating
    /// </summary>
    private void manageAverageRating()
    {
        string currencySymbol = Request.QueryString["currencySymbol"];
        string averageRating = getAverageRating(currencySymbol).ToString();

        //Check if the user has already rated this cryptocurrency
        bool averageRatingPresent = checkIfAverageRatingPresent(currencySymbol);

        //Depending if a rating is present either create a new record with the rating or update the existing rating
        if (!averageRatingPresent)
        {
            saveAverageRating(currencySymbol, averageRating);
        }
        else
        {
            updateAverageRating(currencySymbol, averageRating);
        }

    }

    private bool checkIfAverageRatingPresent(string currencySymbol)
    {
        bool ratingPresent = false;

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "SELECT * FROM [currency] WHERE currencySymbol='" + currencySymbol + "' ;";
        cmd = new SqlCommand(queryStr, conn);

        reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            ratingPresent = true;
        }

        conn.Close();

        return ratingPresent;
    }

    private double getAverageRating(string currencySymbol)
    {
        double averageRating = 0;

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "SELECT * FROM [currencyRating] WHERE currencySymbol='" + currencySymbol + "' ;";
        cmd = new SqlCommand(queryStr, conn);

        reader = cmd.ExecuteReader();

        int counter = 0;
        int ratingsTotal = 0;

        while (reader.HasRows && reader.Read())
        {
            counter++;
            ratingsTotal += Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("rating")));
        }

        averageRating = ratingsTotal / counter;

        conn.Close();

        return averageRating;
    }

    private void saveAverageRating(string currencySymbol, string averageRating)
    {
        string rating = Rating.CurrentRating.ToString();

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "INSERT INTO [currency] (currencySymbol, rating)" + "VALUES('" + currencySymbol + "', '" + averageRating + "')";
        cmd = new SqlCommand(queryStr, conn);

        cmd.ExecuteReader();

        conn.Close();
    }

    private void updateAverageRating(string currencySymbol, string averageRating)
    {
        string rating = Rating.CurrentRating.ToString();

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "UPDATE [currency] SET rating= '" + averageRating + "' WHERE currencySymbol = '" + currencySymbol + "' ;";
        cmd = new SqlCommand(queryStr, conn);

        cmd.ExecuteReader();

        conn.Close();
    }

}



//https://www.codeguru.com/columns/dotnet/passing-data-between-pages-in-asp.net.htm

