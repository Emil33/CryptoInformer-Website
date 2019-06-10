using System;
using System.Data.SqlClient;
using System.Drawing;

public partial class Login : System.Web.UI.Page
{
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataReader reader;
    String queryStr;
    String email;

    public bool loginSuccess = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        notificationLabel.Visible = false;

        //Clear home page session varibales
        Session["numberOfCurrencies"] = null;
        Session["selectedFiatCurrency"] = null;
        Session["selectedSortMethod"] = null;
    }

    protected void loginEventMethod(object sender, EventArgs e)
    {
        string userID = null;

        bool allTextFieldsFilled = checkAllTextFields();

        if (allTextFieldsFilled)
        {

            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

            conn = new SqlConnection(connString);
            conn.Open();

            queryStr = "";


            queryStr = "SELECT * FROM [user] WHERE email='" + emailTextBox.Text + "' AND password='" + passwordTextBox.Text + "'";
            cmd = new SqlCommand(queryStr, conn);

            reader = cmd.ExecuteReader();

            email = "";
            while (reader.HasRows && reader.Read())
            {
                email = reader.GetString(reader.GetOrdinal("email"));
                userID = reader.GetInt32(reader.GetOrdinal("userID")).ToString();
            }

            if (reader.HasRows)
            {
                Session["email"] = email;
                Session["userID"] = userID;
                Response.BufferOutput = true;

                notificationLabel.Visible = true;
                notificationLabel.BackColor = Color.LightGreen;
                notificationLabel.Text = "You have succesfully logged in. Redirecting...";

                loginSuccess = true;

            }
            else
            {
                loginSuccess = false;

                notificationLabel.Visible = true;
                notificationLabel.BackColor = Color.LightGray;
                notificationLabel.Text = "Warning: Invalid email or password.";

                loginSuccess = false;
            }

            reader.Close();
            conn.Close();
        }

    }

    protected bool checkAllTextFields()
    {
        bool allTextFieldsFilled = false;
        
        if(emailTextBox.Text.Equals("") || passwordTextBox.Text.Equals(""))
        {
            notificationLabel.Visible = true;
            notificationLabel.BackColor = Color.LightGray;
            notificationLabel.Text = "Warning: All the available fields have to be filled in.";

            allTextFieldsFilled = false;
        }
        else
        {
            allTextFieldsFilled = true;
        }

        return allTextFieldsFilled;
    }

}

