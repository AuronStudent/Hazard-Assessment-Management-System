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

namespace Hazard_Assessment_Management_System
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (!IsPostBack)
            {
                PopulateDataTable();
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
                htmlTable.Append("<table border='1'>");
                htmlTable.Append("<tr><th>Department Name</th><th>Department Description</th></tr>");

                foreach (DataRow row in departmentTable.Rows)
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>" + row["Dep_Name"].ToString() + "</td>");
                    htmlTable.Append("<td>" + row["Dep_Desc"].ToString() + "</td>");
                    htmlTable.Append("<td>");
                    // htmlTable.Append("<button type='button' onclick='btnEditDept_Click(" + row["Dep_ID"] + ")'>Edit</button>");
                    htmlTable.Append("<form method='post'>");
                    // htmlTable.Append("<input type='hidden' name='departmentID' value='" + row["Dep_ID"] + "' />");
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
            Response.Redirect("NewDept.aspx");
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

                // Refresh the table after deletion
                PopulateDataTable();
            }catch(Exception ex)
            {
                errorLabel.Text = "Deletion failed TODO "+ ex.Message;
            }
        }

        protected void btnEditDept_Click(int depID)
        {
            Response.Redirect("EditDept.aspx?departmentId=" + depID);
        }

    }
}