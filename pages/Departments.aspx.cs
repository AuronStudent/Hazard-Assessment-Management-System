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
    public partial class Departments : System.Web.UI.Page
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

            }
            if (Request.Form["btnDelDept_Click"] != null)
            {
                int departmentId = Convert.ToInt32(Request.Form["departmentId"]);
                btnDelDept_Click(departmentId);
            }
            if (Request.Form["btnEditDept_Click"] != null)
            {

                int departmentId = Convert.ToInt32(Request.Form["departmentId"]);

                btnEditDept_Click(departmentId);
            }

        }
        protected void PopulateDataTable()
        {
            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT Dep_ID, Dep_Name, Dep_Desc FROM Department";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable departmentTable = new DataTable();

                adapter.Fill(departmentTable);

                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<table class='depTable'>");
                htmlTable.Append("<tr><th>Department Name</th><th>Department Description</th></tr>");

                foreach (DataRow row in departmentTable.Rows)
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>" + row["Dep_Name"].ToString() + "</td>");
                    htmlTable.Append("<td style='width:300px;font-size:15px'>" + row["Dep_Desc"].ToString() + "</td>");
                    htmlTable.Append("<td>");
                    // htmlTable.Append("<button type='button' onclick='btnEditDept_Click(" + row["Dep_ID"] + ")'>Edit</button>");
                    htmlTable.Append("<form method='post'>");
                    //  htmlTable.Append("<input type='hidden' name='departmentID' value='" + row["Dep_ID"] + "' />");
                    htmlTable.Append("<button type='submit' name='btnEditDept_Click'>Edit</button>");
                    htmlTable.Append("<input type='hidden' name='departmentID' value='" + row["Dep_ID"] + "' />");
                    htmlTable.Append("<button type='submit' name='btnDelDept_Click'>Delete</button>");
                    htmlTable.Append("</form>");

                    //htmlTable.Append("<button type='button' onclick='btnDelDept_Click(" + row["Dep_ID"] + ")'>Delete</button>");
                    htmlTable.Append("</td>");
                    htmlTable.Append("</tr>");
                }

                htmlTable.Append("</table>");

                dataContainer.InnerHtml = htmlTable.ToString();
            }
        }
        protected void btnNewDept_Click(object sender, EventArgs e)
        {
            depName.Text = "";
            depDesc.Text = "";
            newDeptTable.Visible = true;
            
            saveEdit.Visible = false;
            makeNewDept.Visible = true;
        }
        protected void btnDelDept_Click(int depID)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

                // Define your SQL query to delete the department from the database
                string query = "DELETE FROM Department WHERE Dep_ID = @depID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@depID", depID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                confirmDep.Text = "Successfully deleted Department";
                // Refresh the table after deletion
                PopulateDataTable();
            }
            catch (Exception ex)
            {
                errorLabel.Text = "Deletion failed! There one or more form with this department linked to it.";
            }
        }
        protected void btnEditDept_Click(int depID)
        {
            addOrEdit.Text = "<h2> Edit Department</h2>";
            newDeptTable.Visible = true;
            makeNewDept.Visible = false;
            saveEdit.Visible = true;

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT Dep_ID, Dep_Name, Dep_Desc FROM Department WHERE Dep_ID = @depID";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable departmentTable = new DataTable();
                command.Parameters.AddWithValue("@depID", depID);
                adapter.Fill(departmentTable);


                foreach (DataRow row in departmentTable.Rows)
                {
                    depName.Text = row["Dep_Name"].ToString();
                    depDesc.Text = row["Dep_Desc"].ToString();
                    deptID.Text = row["Dep_ID"].ToString();
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Departments.aspx");

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
                    //newDeptTable.Visible = false;
                   
                    PopulateDataTable();
                }
                catch (Exception ex)
                {
                    //catch exception
                    confirmDep.Text = "An error occured when inserting a department " + ex.Message;
                }
            }
        }

        protected void saveEdit_Click(object sender, EventArgs e)
        {
            int departmentId = Convert.ToInt32(deptID.Text);

            string query = "UPDATE Department SET Dep_Name = @DepName, Dep_Desc = @DepDescription WHERE Dep_ID = @DepID";

            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DepName", depName.Text);
                command.Parameters.AddWithValue("@DepDescription", depDesc.Text);
                command.Parameters.AddWithValue("@DepID", departmentId);

                connection.Open();
                command.ExecuteNonQuery();
            }
            confirmDep.Text = "Successfully saved department changes";
            saveEdit.Visible = false;
            makeNewDept.Visible = true;
            depName.Text = "";
            depDesc.Text = "";
            addOrEdit.Text = "<h2> Add New Department</h2>";

            PopulateDataTable();
        }
    }
}