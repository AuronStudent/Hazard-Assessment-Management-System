using System;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Configuration;

namespace Hazard_Assessment_Management_System
{
    public partial class Hazards : System.Web.UI.Page
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
                saveEdit.Visible = false;
                PopulateDataTable();
                PopulateHazardCategoriesDropdown();

            }
            if (Request.Form["btnDelHaz_Click"] != null)
            {
                int hazId = Convert.ToInt32(Request.Form["hazId"]);
                btnDelHaz_Click(hazId);
            }
            if (Request.Form["btnEditHaz_Click"] != null)
            {

                int hazId = Convert.ToInt32(Request.Form["hazId"]);

                btnEditHaz_Click(hazId);
            }

        }
        protected void PopulateDataTable()
        {
            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT hazard_ID, hazard_Name, hazard_Desc FROM hazard";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable hazardTable = new DataTable();

                adapter.Fill(hazardTable);

                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<table class='depTable'>");
                htmlTable.Append("<tr><th>Hazard Name</th><th>Hazard Description</th></tr>");

                foreach (DataRow row in hazardTable.Rows)
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td style='width:185px;font-size:20px'>" + row["hazard_Name"].ToString() + "</td>");
                    htmlTable.Append("<td style='width:300px;font-size:15px'>" + row["hazard_Desc"].ToString() + "</td>");
                    htmlTable.Append("<td>");
                    // htmlTable.Append("<button type='button' onclick='btnEditDept_Click(" + row["Dep_ID"] + ")'>Edit</button>");
                    htmlTable.Append("<form method='post'>");
                    //  htmlTable.Append("<input type='hidden' name='departmentID' value='" + row["Dep_ID"] + "' />");
                    htmlTable.Append("<button type='submit' name='btnEditHaz_Click'>Edit</button>");
                    htmlTable.Append("<input type='hidden' name='hazID' value='" + row["hazard_ID"] + "' />");
                    htmlTable.Append("<button type='submit' name='btnDelHaz_Click'>Delete</button>");
                    htmlTable.Append("</form>");

                    //htmlTable.Append("<button type='button' onclick='btnDelDept_Click(" + row["Dep_ID"] + ")'>Delete</button>");
                    htmlTable.Append("</td>");
                    htmlTable.Append("</tr>");
                }

                htmlTable.Append("</table>");

                dataContainer.InnerHtml = htmlTable.ToString();
            }
        }
        protected void btnNewHaz_Click(object sender, EventArgs e)
        {
            hazName.Text = "";
            hazDesc.Text = "";
            newHazTable.Visible = true;

            saveEdit.Visible = false;
            makeNewHaz.Visible = true;
        }
        protected void btnDelHaz_Click(int hazID)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

                // Define your SQL query to delete the department from the database
                string query = "DELETE FROM hazard WHERE hazard_ID = @hazID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@hazID", hazID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                confirmHaz.Text = "Successfully deleted Hazard";
                // Refresh the table after deletion
                PopulateDataTable();
            }
            catch (Exception ex)
            {
                errorLabel.Text = "Deletion failed! There one or more form with this hazard linked to it.";
            }
        }
        protected void btnEditHaz_Click(int hazID)
        {
            addOrEdit.Text = "<h2> Edit Hazard</h2>";
            newHazTable.Visible = true;
            makeNewHaz.Visible = false;
            saveEdit.Visible = true;

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT hazard_ID, hazard_Name, hazard_Desc, hazard_cat_id FROM hazard WHERE hazard_ID = @hazID";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable hazardTable = new DataTable();
                command.Parameters.AddWithValue("@hazID", hazID);
                adapter.Fill(hazardTable);


                foreach (DataRow row in hazardTable.Rows)
                {
                    hazName.Text = row["hazard_Name"].ToString();
                    hazDesc.Text = row["hazard_Desc"].ToString();
                    hazardID.Text = row["hazard_ID"].ToString();
                    ddlHazardCategories.SelectedValue = row["hazard_cat_Id"].ToString();
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hazards.aspx");
            addOrEdit.Text = "<h2> Add New Hazard</h2>";
        }

        protected void MakeNewHaz_Click(object sender, EventArgs e)
        {

            //if all fields are not filled in, fail
            if ((hazName.Text == "") || (hazDesc.Text == ""))
            {
                confirmHaz.Text = "Please fill in all fields!";
            }
            else
            { //all fields are filled in
                try
                {
                    myCon.Open(); //open connection
                    string query = "insert into hazard values('" + hazName.Text + "','" + hazDesc.Text + "','" + ddlHazardCategories.SelectedValue + "');"; //sql query string
                    SqlCommand myCom = new SqlCommand(query, myCon); // put sql string into an sql command
                    myCom.ExecuteNonQuery(); // execute the command and insert data into databse
                    confirmHaz.Text = "Successfully entered a new hazard";
                    hazName.Text = ""; hazDesc.Text = ""; ddlHazardCategories.SelectedIndex = -1; //set all textboxes back to blank to prevent postback errors
                                                                                                  //newDeptTable.Visible = false;

                    PopulateDataTable();
                }
                catch (Exception ex)
                {
                    //catch exception
                    confirmHaz.Text = "An error occured when inserting a hazard " + ex.Message;
                }
            }
        }

        protected void saveEdit_Click(object sender, EventArgs e)
        {
            int hazId = Convert.ToInt32(hazardID.Text);

            string query = "UPDATE hazard SET hazard_Name = @hazName, hazard_Desc = @hazDescription, hazard_cat_ID = @hazCatId WHERE hazard_ID = @hazID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@hazName", hazName.Text);
                command.Parameters.AddWithValue("@hazDescription", hazDesc.Text);
                command.Parameters.AddWithValue("@hazID", hazId);
                command.Parameters.AddWithValue("@hazCatId", ddlHazardCategories.SelectedValue);

                connection.Open();
                command.ExecuteNonQuery();
            }
            confirmHaz.Text = "Successfully saved hazard changes";
            saveEdit.Visible = false;
            makeNewHaz.Visible = true;
            hazName.Text = "";
            hazDesc.Text = "";
            ddlHazardCategories.SelectedIndex = -1;
            addOrEdit.Text = "<h2> Add New Hazard</h2>";


            PopulateDataTable();
        }

        protected void PopulateHazardCategoriesDropdown()
        {
            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT hazard_Cat_ID, hazard_Cat_Name FROM HazardCategory";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable hazardCategoryTable = new DataTable();

                adapter.Fill(hazardCategoryTable);

                ddlHazardCategories.DataSource = hazardCategoryTable;
                ddlHazardCategories.DataTextField = "hazard_Cat_Name";
                ddlHazardCategories.DataValueField = "hazard_Cat_ID";
                ddlHazardCategories.DataBind();
            }
            ddlHazardCategories.Items.Insert(0, new ListItem("<Select Hazard Category>", "0"));

        }

        protected void ddlHazardCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected control category ID
            string hazardCategoryId = ddlHazardCategories.SelectedValue;

            // Fetch the details of the selected control category from the database
            HazardCategoryDetails hazardCategoryDetails = GetHazardCategoryDetails(hazardCategoryId);
            /**
             // Populate the text fields with the fetched details
             if (controlCategoryDetails != null)
             {
                 txtCategoryName.Text = controlCategoryDetails.Name;
                 txtCategoryDescription.Text = controlCategoryDetails.Description;

                 // Fetch and display the controls associated with the selected control category
                 PopulateControlsInCategoryDropdown(controlCategoryId);
             }
            
             // Show the category editing controls
             EditCategories.Style["display"] = "block"**/
        }

        private HazardCategoryDetails GetHazardCategoryDetails(string hazardCategoryId)
        {
            // Define your SQL query to retrieve control category details from the database
            string query = "SELECT hazard_Cat_Name, hazard_Cat_Desc FROM HazardCategory WHERE hazard_Cat_ID = @hazCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@hazCategoryID", hazardCategoryId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Create a new ControlCategoryDetails object and populate it with the fetched details
                    HazardCategoryDetails hazardCategoryDetails = new HazardCategoryDetails
                    {
                        Name = reader["hazard_Cat_Name"].ToString(),
                        Description = reader["hazard_Cat_Desc"].ToString()
                    };

                    reader.Close();
                    return hazardCategoryDetails;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
        }

        /** protected void btnSave_Click(object sender, EventArgs e)
         {
             // Get the selected control category ID
             string controlCategoryId = ddlControlCategories.SelectedValue;

             // Update the control category details in the database
             UpdateControlCategoryDetails(controlCategoryId, txtCategoryName.Text, txtCategoryDescription.Text);

             // Show a message or perform any other necessary actions after saving
         }
        **/
        private void UpdateHazardCategoryDetails(string hazardCategoryId, string categoryName, string categoryDescription)
        {
            // Define your SQL query to update control category details in the database
            string query = "UPDATE HazardCategory SET hazard_Cat_Name = @CategoryName, hazard_Cat_Desc = @CategoryDescription WHERE hazard_Cat_ID = @HazardCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                command.Parameters.AddWithValue("@CategoryDescription", categoryDescription);
                command.Parameters.AddWithValue("@HazardCategoryID", hazardCategoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    public class HazardCategoryDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}