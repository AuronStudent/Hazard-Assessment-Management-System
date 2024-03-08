using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hazard_Assessment_Management_System.pages
{
    public partial class LoginPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginForm_Click(object sender, EventArgs e)
        {
            string user = "";//username var
            //not enchrypting just yet
            string pass = "";//password var
            try
            {
                con.Open();
                //this query searches all usernames with the one inputted by the user
                string query = "select * from AdminUser where Auser_name = '" + username.Text + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                //if username is found, get that username and password
                if (reader.Read())
                {
                    user = reader["Auser_name"].ToString();
                    pass = reader["password"].ToString();

                    reader.Close();
                }
                else
                {
                    reader.Close();
                }
                //compare username and password to each other. it wont work if one is incorrect
                if((user == username.Text)&&(pass == password.Text))
                {
                    //successful login. redirect to index page.
                    Response.Redirect("index.aspx");
                }
                else
                {
                    //login failed. wrong username or password
                    loginError.Text = "Incorrect Username or Password";
                }

            }catch(Exception ex)
            {
                //login failed or connection failed
                loginError.Text = "Login Failed"+ ex.Message;
            }
        }
    }
}