using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hazard_Assessment_Management_System
{
    public partial class NewHazard : System.Web.UI.Page
    {
        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                string username = Session["username"].ToString();
            }
            else
            {
                Response.Redirect("PleaseLogIn.aspx");
            }
            if (!IsPostBack)
            {
                
            }
        }
        protected void MakeNewHazard_Click(object sender, EventArgs e)
        {
            if ((hazName.Text == "") || (hazDesc.Text == ""))
            {
                confirmHazard.Text = "Please fill in all fields!";
            }
            else
            {
                try
                {
                    myCon.Open(); //open connection
                    string query = "insert into hazard values('" + hazName.Text + "','" + hazDesc.Text + "','" + hazCate.SelectedValue + "');"; //sql query string
                    SqlCommand myCom = new SqlCommand(query, myCon); // put sql string into an sql command
                    myCom.ExecuteNonQuery(); // execute the command and insert data into databse
                    confirmHazard.Text = "Successfully entered new hazard";
                    hazName.Text = ""; hazDesc.Text = ""; hazCate.SelectedIndex = 0; //set everything back to blank to prevent postback errors
                }
                catch (Exception ex)
                {
                    //catch exception
                    confirmHazard.Text = "An error occured when inserting control" + ex.Message;
                }
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hazards.aspx");
        }

    }
}