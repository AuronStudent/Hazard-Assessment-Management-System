using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Hazard_Assessment_Management_System
{
    public partial class Hazards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateHazardCategoriesDropdown();
            }
        }

        protected void PopulateHazardCategoriesDropdown()
        {
            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve hazard categories from the database
            string query = "SELECT Hazard_Cat_ID, Hazard_Cat_Name FROM HazardCategory";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable HazardCategoryTable = new DataTable();

                adapter.Fill(HazardCategoryTable);

                ddlHazardCategories.DataSource = HazardCategoryTable;
                ddlHazardCategories.DataTextField = "Hazard_Cat_Name";
                ddlHazardCategories.DataValueField = "Hazard_Cat_ID";
                ddlHazardCategories.DataBind();
            }
            ddlHazardCategories.Items.Insert(0, new ListItem("<Select Hazard Category>", "0"));

        }

        protected void ddlHazardCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected hazard category ID
            string HazardCategoryId = ddlHazardCategories.SelectedValue;

            // Fetch the details of the selected hazard category from the database
            HazardCategoryDetails HazardCategoryDetails = GetHazardCategoryDetails(HazardCategoryId);

            // Populate the text fields with the fetched details
            if (HazardCategoryDetails != null)
            {
                txtCategoryName.Text = HazardCategoryDetails.Name;
                txtCategoryDescription.Text = HazardCategoryDetails.Description;

                // Fetch and display the hazards associated with the selected hazard category
                PopulateHazardInCategoryDropdown(HazardCategoryId);
            }

            // Show the category editing hazards
            EditCategories.Style["display"] = "block";
        }

        private HazardCategoryDetails GetHazardCategoryDetails(string HazardCategoryId)
        {
            // Define your SQL query to retrieve hazard category details from the database
            string query = "SELECT Hazard_Cat_Name, Hazard_Cat_Desc FROM HazardCategory WHERE Hazard_Cat_ID = @HazardCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@HazardCategoryID", HazardCategoryId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Create a new hazardCategoryDetails object and populate it with the fetched details
                    HazardCategoryDetails HazardCategoryDetails = new HazardCategoryDetails
                    {
                        Name = reader["Hazard_Cat_Name"].ToString(),
                        Description = reader["Hazard_Cat_Desc"].ToString()
                    };

                    reader.Close();
                    return HazardCategoryDetails;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
        }

        private void PopulateHazardInCategoryDropdown(string HazardCategoryId)
        {
            // Define your SQL query to retrieve hazards associated with the selected hazard category
            string query = "SELECT Hazard_Name, Hazard_ID FROM Hazard WHERE Hazard_Cat_ID = @HazardCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@HazardCategoryID", HazardCategoryId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Clear existing items from the dropdown list
                ddlHazardInCategory.Items.Clear();

                // Add each hazard to the dropdown list
                while (reader.Read())
                {
                    ListItem item = new ListItem(reader["Hazard_Name"].ToString(), reader["Hazard_ID"].ToString());
                    ddlHazardInCategory.Items.Add(item);
                }

                reader.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get the selected hazard category ID
            string HazardCategoryId = ddlHazardCategories.SelectedValue;

            // Update the hazard category details in the database
            UpdateHazardCategoryDetails(HazardCategoryId, txtCategoryName.Text, txtCategoryDescription.Text);

            // Show a message or perform any other necessary actions after saving
        }

        private void UpdateHazardCategoryDetails(string HazardCategoryId, string categoryName, string categoryDescription)
        {
            // Define your SQL query to update hazard category details in the database
            string query = "UPDATE HazardCategory SET Hazard_Cat_Name = @CategoryName, Hazard_Cat_Desc = @CategoryDescription WHERE Hazard_Cat_ID = @HazardCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                command.Parameters.AddWithValue("@CategoryDescription", categoryDescription);
                command.Parameters.AddWithValue("@HazardCategoryID", HazardCategoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Implement your cancel logic here
        }
        protected void btnNewHazard_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewHazard.aspx");
        }
        

        
    }

    public class HazardCategoryDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
