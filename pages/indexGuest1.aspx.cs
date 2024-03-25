using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Collections.ObjectModel;

namespace Hazard_Assessment_Management_System
{
    public partial class indexGuest1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForms();
            }

        }

        private void LoadForms()
        {
            string query = "SELECT form_id,Job_Type,Name_Filled_Out,Date_Filled_Out,Reviewed_By_Name,Review_Date from form;";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {
                //this block is meant for making the data table on the forms screen
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    DataTable dt = new DataTable();
                    dt.Load(dr); //load datatable with values from reader
                    forms.DataSource = dt;
                    forms.DataBind(); // bind the data indexes
                }
            }
        }
        protected void Forms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //button for going to a specific form page with a certian value
            if (e.CommandName == "VIEW")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                int id = Convert.ToInt32(forms.DataKeys[index].Value.ToString());
                //redirect with the id of the clicked form
                Response.Redirect("SpecificFormGuest.aspx?id=" + id);
            }
        }
        protected void searchBtn_click(object sender, EventArgs e)
        {
            SearchResultsGrid.Visible = true;
            try
            {
                string searchTerm = SearchTextBox.Text.Trim();
                string filterResult = DropDownFilter.SelectedValue;
                //Probably going to have to use Joins and extra search term filters to get a complete form
                //Ex. Table Department and Form Right Join on Dep_ID WHERE Dep_Name LIKE @SearchTerm
                string query = "";
                switch (filterResult)
                {
                    case "Department":
                        query = "SELECT Form.*, Department.Dep_ID FROM Form FULL OUTER JOIN Department ON Form.Dep_ID = Department.Dep_ID WHERE department.Dep_name LIKE '" + searchTerm + "';";
                        break;
                    case "Control":
                        query = "SELECT Form.*, FormControl.Form_Control_ID FROM Form FULL OUTER JOIN FormControl ON Form.Form_ID = FormControl.Form_ID WHERE Con_name LIKE '" + searchTerm + "';";
                        break;
                    case "Hazard":
                        query = "SELECT Form.*, FormHazard.Form_Haz_ID FROM Form FULL OUTER JOIN FormHazard ON Form.Form_ID = FormHazard.Form_ID WHERE Hazard_name LIKE '" + searchTerm + "';";
                        break;
                    default:
                        Console.WriteLine("Please Input a value for the filter");
                        return;
                }


                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;
                SqlConnection cnn;


                cnn = new SqlConnection(connectionString);
                cnn.Open();

                SqlCommand command = new SqlCommand(query, cnn);

                command.ExecuteNonQuery();

                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {


                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    DataTable dt = new DataTable();
                    dt.Load(dr); //load datatable with values from reader
                    SearchResultsGrid.DataSource = dt;
                    SearchResultsGrid.DataBind(); // bind the data indexes
                    cnn.Close();
                }

                forms.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {

            }
        }
        protected void clearBtn_click(object sender, EventArgs e)
        {
            forms.Visible = true;
            SearchResultsGrid.Visible = false;
            SearchTextBox.Text = "";
        }

    }
}
