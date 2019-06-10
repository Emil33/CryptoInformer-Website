using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class CurrencyComments : System.Web.UI.Page
{
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataReader reader;
    String queryStr;

    List<CryptocurrencyCommentsDataClass> APIResult;

    protected void Page_Load(object sender, EventArgs e)
    {
        getCryptocurrencyRatings();
    }

    //Getting the cryptocurrency ratings
    protected List<CryptocurrencyCommentsDataClass> getCryptocurrencyRatings()
    {
        APIResult = readCryptocurrencyComments();

        return APIResult;
    }

    protected List<CryptocurrencyCommentsDataClass> aPIResult { get { return APIResult; } }

    private List<CryptocurrencyCommentsDataClass> readCryptocurrencyComments()
    {
        string currencySymbol = Request.QueryString["currencySymbol"];

        List<CryptocurrencyCommentsDataClass> commentList = new List<CryptocurrencyCommentsDataClass>();

        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        conn = new SqlConnection(connString);
        conn.Open();

        queryStr = "";

        queryStr = "SELECT * FROM [currencyRating] WHERE currencySymbol='" + currencySymbol + "' ;"; 
        cmd = new SqlCommand(queryStr, conn);

        reader = cmd.ExecuteReader();

        while (reader.HasRows && reader.Read())
        {
            CryptocurrencyCommentsDataClass comment = new CryptocurrencyCommentsDataClass();

            comment.ratingID = reader.GetInt32(reader.GetOrdinal("ratingID")).ToString();
            comment.currencySymbol = reader.GetString(reader.GetOrdinal("currencySymbol"));
            comment.userID = reader.GetInt32(reader.GetOrdinal("userID")).ToString();
            comment.rating = reader.GetInt32(reader.GetOrdinal("rating")).ToString();
            comment.comment = reader.GetString(reader.GetOrdinal("comment"));
            
            commentList.Add(comment);
        }

        conn.Close();

        return commentList;
    }
}