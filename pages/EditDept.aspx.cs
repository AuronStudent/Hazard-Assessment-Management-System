using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hazard_Assessment_Management_System
{
    public partial class EditDept : System.Web.UI.Page
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
                if (Request.QueryString["departmentId"] != null)
                {
                    int deptID = Convert.ToInt32(Request.QueryString["departmentId"]);
                    // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

                    // Define your SQL query to retrieve control categories from the database
                    string query = "SELECT Dep_ID, Dep_Name, Dep_Desc FROM Department WHERE Dep_ID = @depID";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable departmentTable = new DataTable();
                        command.Parameters.AddWithValue("@depID", deptID);
                        adapter.Fill(departmentTable);


                        foreach (DataRow row in departmentTable.Rows)
                        {
                            depName.Text = row["Dep_Name"].ToString();
                            depDesc.Text = row["Dep_Desc"].ToString();
                        }
                    }
                    //departmentId.Value = Request.QueryString["departmentId"];
                    // int departmentId = Convert.ToInt32(Request.QueryString["departmentId"]);
                    // saveEdit_Click(departmentId);
                }
            }
        }
        protected void cancelEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Departments.aspx");
        }

        protected void saveEdit_Click(object sender, EventArgs e)
        {
            int departmentId = Convert.ToInt32(Request.QueryString["departmentId"]);
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
            confirmSave.Text = "Successfully saved department changes";
        }
    }
}