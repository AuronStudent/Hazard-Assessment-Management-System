using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Security.Cryptography;

namespace Hazard_Assessment_Management_System
{
    public partial class OHSForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadDepartments();
                LoadHazards();
                LoadControls();
            }
            
        }
        //Loads all the departments in the database into a dropdown menu
        private void LoadDepartments()
        {

            DataTable departments = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Dep_ID, Dep_Name FROM dbo.Department", con);
                    adapter.Fill(departments);
                   
                    ddlDep.DataSource = departments;
                    //sets the text of each option as the department name
                    ddlDep.DataTextField = "Dep_Name";
                    //sets the value of each option as the department id
                    ddlDep.DataValueField = "Dep_ID";
                    ddlDep.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorDep.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlDep.Items.Insert(0, new ListItem("<Select Department>", "0"));
            con.Close();
        }
        private void LoadHazards()
        {

            DataTable hazards = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Hazard_ID, Hazard_Name FROM dbo.Hazard", con);
                    adapter.Fill(hazards);

                    ddlHaz.DataSource = hazards;
                    //sets the text for each option to hazard name
                    //TODO make it from categories and not jusst choosing a hazard
                    ddlHaz.DataTextField = "Hazard_Name";
                    //sets the value of each option to hazard ID
                    ddlHaz.DataValueField = "Hazard_ID";
                    ddlHaz.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorHaz.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlHaz.Items.Insert(0, new ListItem("<Select Hazard>", "0"));
            con.Close();

        }
        private void LoadControls()
        {

            DataTable controls = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Con_ID, Con_Name FROM dbo.Control", con);
                    adapter.Fill(controls);

                    ddlCon.DataSource = controls;
                    //same as above
                    //TODO make it come from a category not from control
                    ddlCon.DataTextField = "Con_Name";
                    //same as above
                    ddlCon.DataValueField = "Con_ID";
                    ddlCon.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorCon.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlCon.Items.Insert(0, new ListItem("<Select Control>", "0"));
            con.Close();
        }
        protected void SubmitForm_Click(object sender, EventArgs e)
        {
            string formIDString = "";
            int formID = 0; //ID of the form
            //getting the current date and review date
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            int reviewYear = currentYear + 1; // added one to review year
            //setting both dates into one string
            string reviewDate = currentMonth.ToString()+"/"+currentDay.ToString()+"/"+reviewYear.ToString();
            string filledOutDate = currentMonth.ToString() + "/" + currentDay.ToString() + "/" + currentYear.ToString();
            //getting total risk
            int riskTotal = Int32.Parse(like.Text) + Int32.Parse(sev.Text) + Int32.Parse(freq.Text);
            try
            {
                con.Open(); //open connection
                
                string query = "insert into Form values("+ ddlDep.SelectedValue + ",'" +jobsite.Text+"','" + reviewDate + "','" + legalName.Text + "','" + filledOutDate + "',null,null,0," + riskTotal + ",'" + email.Text + "');"; //sql query string
                SqlCommand myCom = new SqlCommand(query, con); // put sql string into an sql command
                myCom.ExecuteNonQuery(); // execute the command and insert data into databse

                //This is to get the ID of the form we just inserted. if everything goes good, the other foreign key tables should be in sync with this one
                query = "SELECT TOP 1 form_id FROM form ORDER BY form_id DESC;";
                myCom = new SqlCommand(query, con);

                SqlDataReader reader = myCom.ExecuteReader();
                if (reader.Read())
                {
                    //read the formID
                    formIDString = reader["form_ID"].ToString();
                    reader.Close();
                }
                else
                {
                    reader.Close();
                }
                formID = Int32.Parse(formIDString);

                //now onto Foreign keys
                //this one is to store the control from the form
                //TODO add multiple controls
                query = "insert into FormControl values("+ddlCon.SelectedValue+","+formID+");";
                myCom = new SqlCommand(query, con);
                myCom.ExecuteNonQuery();

                //this one is to store the hazards of the form
                //TODO add multiple hazards
                query = "insert into FormHazard values(" + ddlHaz.SelectedValue + "," + formID + ");";
                myCom = new SqlCommand(query, con);
                myCom.ExecuteNonQuery();

                //this one is to store the tasks of the form
                //TODO add more tasks
                query = "insert into FormTask values("+formID+"," + ddlHaz.SelectedValue + ",'" + task.Text + "');";
                myCom = new SqlCommand(query, con);
                myCom.ExecuteNonQuery();

                //TODO Tasks can have multiple hazards and hazards can have only one control. There needs to be one control per hazard on each task.

                con.Close();
            }
            catch(Exception ex)
            {
                //error handling
                formError.Text = ex.Message;
            }
            finally
            {
                //set all values back to nothing to prevent postback errors.
                ddlDep.SelectedIndex = 0; 
                jobsite.Text = "";
                legalName.Text = "";
                email.Text = "";
                like.Text = "";
                sev.Text = "";
                freq.Text = "";
                task.Text = "";
                ddlCon.SelectedIndex = 0;
                ddlHaz.SelectedIndex = 0;
            }
            

        }
        protected void CancelForm_Click(object sender, EventArgs e)
        {
            //cancel button brings user back to home screen
            Response.Redirect("index.aspx");
        }
    }
}