using System;
using System.Data.SqlClient;
using System.Drawing;

public partial class Registration : System.Web.UI.Page
{
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataReader reader;
    String queryStr;

    public bool registerSuccess = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        //Clear home page session varibales
        Session["numberOfCurrencies"] = null;
        Session["selectedFiatCurrency"] = null;
        Session["selectedSortMethod"] = null;
    }

    protected void registerEventMethod(object sender, EventArgs e)
    {
        bool allTextFieldsFilled = checkAllTextFields();

        if (allTextFieldsFilled)
        {
            registerUser();
        }

    }

    private void registerUser()
    {

        //Check if confirm password and password match
        if (confirmPasswordTextBox.Text.Equals(passwordTextBox.Text))
        {
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

            conn = new SqlConnection(connString);
            conn.Open();

            //Check if the user email entered already exists
            queryStr = "";

            queryStr = "SELECT * FROM [user] WHERE email='" + emailTextBox.Text + "'";
            cmd = new SqlCommand(queryStr, conn);

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                notificationLabel.Visible = true;
                notificationLabel.BackColor = Color.LightGray;
                notificationLabel.Text = "Warning: The email entered already exists.";

                registerSuccess = false;
            }
            else
            {
                conn.Close();
                conn.Open();

                //If entered email valid, save user data into the database
                queryStr = "";

                queryStr = "INSERT INTO [user] (email, password)" + "VALUES('" + emailTextBox.Text + "', '" + passwordTextBox.Text + "')";
                cmd = new SqlCommand(queryStr, conn);

                cmd.ExecuteReader();

                conn.Close();

                registerSuccess = true;

                notificationLabel.Visible = true;
                notificationLabel.BackColor = Color.LightGreen;
                notificationLabel.Text = "You have succesfully registered. Redirecting...";                                
            }
        }
        else
        {
            notificationLabel.Visible = true;
            notificationLabel.BackColor = Color.LightGray;
            notificationLabel.Text = "Warning: The passwords do not match.";

            registerSuccess = false;
        }

    }

    protected bool checkAllTextFields()
    {
        bool allTextFieldsFilled = false;

        if (emailTextBox.Text.Equals("") || passwordTextBox.Text.Equals("") || confirmPasswordTextBox.Text.Equals(""))
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