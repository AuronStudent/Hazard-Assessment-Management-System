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
    public partial class NewControl : System.Web.UI.Page
    {
        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        protected void MakeNewControl_Click(object sender, EventArgs e)
        {
            //if all fields are not filled in, fail
            if ((controlName.Text == "") || (controlDesc.Text == ""))
            {
                confirmControl.Text = "Please fill in all fields!";
            }
            else
            { //all fields are filled in
                try
                {
                    myCon.Open(); //open connection
                    string query = "insert into control values('" + controlName.Text + "','" + controlDesc.Text + "','" + conCate.SelectedValue + "');"; //sql query string
                    SqlCommand myCom = new SqlCommand(query, myCon); // put sql string into an sql command
                    myCom.ExecuteNonQuery(); // execute the command and insert data into databse
                    confirmControl.Text = "Successfully entered new control";
                    controlName.Text = ""; controlDesc.Text = ""; conCate.SelectedIndex = 0; //set all textboxes back to blank to prevent postback errors
                }
                catch (Exception ex)
                {
                    //catch exception
                    confirmControl.Text = "An error occured when inserting control" + ex.Message;
                }
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Controls.aspx");
        }

    }
}