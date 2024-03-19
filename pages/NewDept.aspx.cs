using Microsoft.SqlServer.Server;
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
    public partial class NewDept : System.Web.UI.Page
    {
        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void MakeNewDept_Click(object sender, EventArgs e)
        {
            //if all fields are not filled in, fail
            if ((depName.Text == "") || (depDesc.Text == ""))
            {
                confirmDep.Text = "Please fill in all fields!";
            }
            else
            { //all fields are filled in
                try
                {
                    myCon.Open(); //open connection
                    string query = "insert into department values('" + depName.Text + "','" + depDesc.Text + "');"; //sql query string
                    SqlCommand myCom = new SqlCommand(query, myCon); // put sql string into an sql command
                    myCom.ExecuteNonQuery(); // execute the command and insert data into databse
                    confirmDep.Text = "Successfully entered a new department";
                    depName.Text = ""; depDesc.Text = ""; //set all textboxes back to blank to prevent postback errors
                }
                catch (Exception ex)
                {
                    //catch exception
                    confirmDep.Text = "An error occured when inserting a department" + ex.Message;
                }
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Departments.aspx");
        }

    }
}