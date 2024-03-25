using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Hazard_Assessment_Management_System
{
    public partial class Controls : System.Web.UI.Page
    {
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
                PopulateControlCategoriesDropdown();
            }
        }

        protected void PopulateControlCategoriesDropdown()
        {
            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT Con_Cat_ID, Con_Cat_Name FROM ControlCategory";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable controlCategoryTable = new DataTable();

                adapter.Fill(controlCategoryTable);

                ddlControlCategories.DataSource = controlCategoryTable;
                ddlControlCategories.DataTextField = "Con_Cat_Name";
                ddlControlCategories.DataValueField = "Con_Cat_ID";
                ddlControlCategories.DataBind();
            }
            ddlControlCategories.Items.Insert(0, new ListItem("<Select Control Category>", "0"));

        }

        protected void ddlControlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected control category ID
            string controlCategoryId = ddlControlCategories.SelectedValue;

            // Fetch the details of the selected control category from the database
            ControlCategoryDetails controlCategoryDetails = GetControlCategoryDetails(controlCategoryId);

            // Populate the text fields with the fetched details
            if (controlCategoryDetails != null)
            {
                txtCategoryName.Text = controlCategoryDetails.Name;
                txtCategoryDescription.Text = controlCategoryDetails.Description;

                // Fetch and display the controls associated with the selected control category
                PopulateControlsInCategoryDropdown(controlCategoryId);
            }

            // Show the category editing controls
            EditCategories.Style["display"] = "block";
        }

        private ControlCategoryDetails GetControlCategoryDetails(string controlCategoryId)
        {
            // Define your SQL query to retrieve control category details from the database
            string query = "SELECT Con_Cat_Name, Con_Cat_Desc FROM ControlCategory WHERE Con_Cat_ID = @ControlCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ControlCategoryID", controlCategoryId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Create a new ControlCategoryDetails object and populate it with the fetched details
                    ControlCategoryDetails controlCategoryDetails = new ControlCategoryDetails
                    {
                        Name = reader["Con_Cat_Name"].ToString(),
                        Description = reader["Con_Cat_Desc"].ToString()
                    };

                    reader.Close();
                    return controlCategoryDetails;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
        }

        private void PopulateControlsInCategoryDropdown(string controlCategoryId)
        {
            // Define your SQL query to retrieve controls associated with the selected control category
            string query = "SELECT Con_Name, Con_ID FROM Control WHERE Con_Cat_ID = @ControlCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ControlCategoryID", controlCategoryId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Clear existing items from the dropdown list
                ddlControlsInCategory.Items.Clear();

                // Add each control to the dropdown list
                
                while (reader.Read())
                {
                    ListItem item = new ListItem(reader["Con_Name"].ToString(), reader["Con_ID"].ToString());
                    ddlControlsInCategory.Items.Add(item);
                }

                reader.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get the selected control category ID
            string controlCategoryId = ddlControlCategories.SelectedValue;

            // Update the control category details in the database
            UpdateControlCategoryDetails(controlCategoryId, txtCategoryName.Text, txtCategoryDescription.Text);

            // Show a message or perform any other necessary actions after saving
        }

        private void UpdateControlCategoryDetails(string controlCategoryId, string categoryName, string categoryDescription)
        {
            // Define your SQL query to update control category details in the database
            string query = "UPDATE ControlCategory SET Con_Cat_Name = @CategoryName, Con_Cat_Desc = @CategoryDescription WHERE Con_Cat_ID = @ControlCategoryID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                command.Parameters.AddWithValue("@CategoryDescription", categoryDescription);
                command.Parameters.AddWithValue("@ControlCategoryID", controlCategoryId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        protected void btnNewControl_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewControl.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Implement your cancel logic here
        }
    }

    public class ControlCategoryDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
