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
    public partial class Controls : System.Web.UI.Page
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
                PopulateControlCategoriesDropdown();

            }
            if (Request.Form["btnDelCon_Click"] != null)
            {
                int conId = Convert.ToInt32(Request.Form["conId"]);
                btnDelCon_Click(conId);
            }
            if (Request.Form["btnEditCon_Click"] != null)
            {

                int conId = Convert.ToInt32(Request.Form["conId"]);

                btnEditCon_Click(conId);
            }

        }
        protected void PopulateDataTable()
        {
            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT con_ID, con_Name, con_Desc FROM control";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable controlTable = new DataTable();

                adapter.Fill(controlTable);

                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<table class='depTable'>");
                htmlTable.Append("<tr><th>Control Name</th><th>Control Description</th></tr>");

                foreach (DataRow row in controlTable.Rows)
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td style='width:185px;font-size:20px'>" + row["con_Name"].ToString() + "</td>");
                    htmlTable.Append("<td style='width:300px;font-size:15px'>" + row["con_Desc"].ToString() + "</td>");
                    htmlTable.Append("<td>");
                    // htmlTable.Append("<button type='button' onclick='btnEditDept_Click(" + row["Dep_ID"] + ")'>Edit</button>");
                    htmlTable.Append("<form method='post'>");
                    //  htmlTable.Append("<input type='hidden' name='departmentID' value='" + row["Dep_ID"] + "' />");
                    htmlTable.Append("<button type='submit' name='btnEditCon_Click'>Edit</button>");
                    htmlTable.Append("<input type='hidden' name='conID' value='" + row["con_ID"] + "' />");
                    htmlTable.Append("<button type='submit' name='btnDelCon_Click'>Delete</button>");
                    htmlTable.Append("</form>");

                    //htmlTable.Append("<button type='button' onclick='btnDelDept_Click(" + row["Dep_ID"] + ")'>Delete</button>");
                    htmlTable.Append("</td>");
                    htmlTable.Append("</tr>");
                }

                htmlTable.Append("</table>");

                dataContainer.InnerHtml = htmlTable.ToString();
            }
        }
        protected void btnNewCon_Click(object sender, EventArgs e)
        {
            conName.Text = "";
            conDesc.Text = "";
            newConTable.Visible = true;

            saveEdit.Visible = false;
            makeNewCon.Visible = true;
        }
        protected void btnDelCon_Click(int conID)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

                // Define your SQL query to delete the department from the database
                string query = "DELETE FROM control WHERE con_ID = @conID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@conID", conID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                confirmCon.Text = "Successfully deleted Control";
                // Refresh the table after deletion
                PopulateDataTable();
            }
            catch (Exception ex)
            {
                errorLabel.Text = "Deletion failed! There one or more form with this control linked to it.";
            }
        }
        protected void btnEditCon_Click(int conID)
        {
            addOrEdit.Text = "<h2> Edit Control</h2>";
            newConTable.Visible = true;
            makeNewCon.Visible = false;
            saveEdit.Visible = true;

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT con_ID, con_Name, con_Desc, con_cat_id FROM control WHERE con_ID = @conID";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable controlTable = new DataTable();
                command.Parameters.AddWithValue("@conID", conID);
                adapter.Fill(controlTable);


                foreach (DataRow row in controlTable.Rows)
                {
                    conName.Text = row["con_Name"].ToString();
                    conDesc.Text = row["con_Desc"].ToString();
                    controlID.Text = row["con_ID"].ToString();
                    ddlControlCategories.SelectedValue = row["con_cat_Id"].ToString();
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Controls.aspx");
            addOrEdit.Text = "<h2> Add New Control</h2>";
        }

        protected void MakeNewCon_Click(object sender, EventArgs e)
        {

            //if all fields are not filled in, fail
            if ((conName.Text == "") || (conDesc.Text == ""))
            {
                confirmCon.Text = "Please fill in all fields!";
            }
            else
            { //all fields are filled in
                try
                {
                    myCon.Open(); //open connection
                    string query = "insert into control values('" + conName.Text + "','" + conDesc.Text + "','"+ddlControlCategories.SelectedValue+"');"; //sql query string
                    SqlCommand myCom = new SqlCommand(query, myCon); // put sql string into an sql command
                    myCom.ExecuteNonQuery(); // execute the command and insert data into databse
                    confirmCon.Text = "Successfully entered a new control";
                    conName.Text = ""; conDesc.Text = ""; ddlControlCategories.SelectedIndex = -1;//set all textboxes back to blank to prevent postback errors
                                                                                                 //newDeptTable.Visible = false;

                    PopulateDataTable();
                }
                catch (Exception ex)
                {
                    //catch exception
                    confirmCon.Text = "An error occured when inserting a control " + ex.Message;
                }
            }
        }

        protected void saveEdit_Click(object sender, EventArgs e)
        {
            int conId = Convert.ToInt32(controlID.Text);

            string query = "UPDATE control SET con_Name = @ConName, con_Desc = @ConDescription, con_cat_ID = @conCatId WHERE con_ID = @ConID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ConName", conName.Text);
                command.Parameters.AddWithValue("@ConDescription", conDesc.Text);
                command.Parameters.AddWithValue("@ConID", conId);
                command.Parameters.AddWithValue("@conCatId", ddlControlCategories.SelectedValue);

                connection.Open();
                command.ExecuteNonQuery();
            }
            confirmCon.Text = "Successfully saved control changes";
            saveEdit.Visible = false;
            makeNewCon.Visible = true;
            conName.Text = "";
            conDesc.Text = "";
            ddlControlCategories.SelectedIndex = -1;
            addOrEdit.Text = "<h2> Add New Control</h2>";


            PopulateDataTable();
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

       /** protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get the selected control category ID
            string controlCategoryId = ddlControlCategories.SelectedValue;

            // Update the control category details in the database
            UpdateControlCategoryDetails(controlCategoryId, txtCategoryName.Text, txtCategoryDescription.Text);

            // Show a message or perform any other necessary actions after saving
        }
       **/
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
    }
    public class ControlCategoryDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}