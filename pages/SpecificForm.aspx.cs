using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace Hazard_Assessment_Management_System.pages
{
    public partial class SpecificForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                string username = Session["username"].ToString();
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                LoadForm();
                //PopulateDataTable();
            }
            string id = Request.QueryString["id"];
        }
        private void LoadForm()
        {
            try
            {
                string conDesc = "";//unused currently
                List<string> conName = new List<string>();
                List<string> conId = new List<string>();
                string hazDesc = "";//unused currently
                List<string> hazName = new List<string>();
                string depId = "";
                string jobType = "";
                string revDate = "";
                string nameFilledOut = "";
                string dateAssessed = "";
                string reviewdBy = "";
                string riskLevel = "";
                string userEmail = "";
                List<string> aTask = new List<string>();
                List<string> hazId = new List<string>();
                string depName = "";
                // get everything on form at specifyed ID
                int formId = Convert.ToInt32(Request.QueryString["id"]);
                string query = "SELECT * from form where form_id=" + formId + ";";
                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            //select all variables of form
                            depId = reader["Dep_ID"].ToString();
                            jobType = reader["Job_Type"].ToString();
                            revDate = reader["Review_Date"].ToString();
                            nameFilledOut = reader["Name_Filled_Out"].ToString();
                            dateAssessed = reader["Date_Filled_Out"].ToString();
                            reviewdBy = reader["Reviewed_By_Name"].ToString();
                            riskLevel = reader["Risk"].ToString();
                            userEmail = reader["User_Email"].ToString();
                            reader.Close();
                        }
                        else
                        {
                            reader.Close();

                        }

                    }
                    //get all from that specific forms tasks
                    query = "SELECT * from formtask where form_id=" + formId + ";";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            //assign variables
                            aTask.Add(reader["Task"].ToString());
                            hazId.Add(reader["Hazard_ID"].ToString());

                            
                        }

                        reader.Close();
                    }
                    //get haazards for this form
                    for (int i = 0; i< hazId.Count; i++){
                    query = "SELECT * from hazard where hazard_id=" + hazId[i] + ";";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                hazName.Add(reader["Hazard_Name"].ToString());
                                hazDesc = reader["Hazard_Desc"].ToString(); //unused currently
                            }
                            reader.Close();
                        }
                    }
                    //get controls form this form
                    query = "SELECT * from formcontrol where form_id=" + formId + ";";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {


                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            conId.Add( reader["con_id"].ToString());
                            
                        }
                        reader.Close();

                    }
                    for (int i = 0; i < conId.Count; i++)
                    {
                        //same as above
                        query = "SELECT * from control where con_id=" + conId[i] + ";";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                conName.Add(reader["con_name"].ToString());
                                conDesc = reader["con_desc"].ToString(); //TODO unused currently
                                
                            }
                            reader.Close();
                        }
                    }
                    query = "SELECT * from department where dep_id=" + depId + ";";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {


                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            depName = reader["dep_name"].ToString();

                            reader.Close();
                        }
                        else
                        {
                            reader.Close();

                        }

                    }

                    //asign all variable to the form boxes on the page
                    jobite.Text = jobType;
                    reviewDate.Text = revDate;
                    reviewBy.Text = reviewdBy;
                    name.Text = nameFilledOut;
                    dateOf.Text = dateAssessed;
                    //risk.Text = riskLevel;
                    email.Text = userEmail;
                    //task.Text = aTask;
                    //hazards.Text = hazName;
                    //controls.Text = conName;
                    department.Text = depName;



                    int dateBox = 0;
                    for (int i = 0; i < aTask.Count; i++)
                    {
                        if (task.Text == "")
                        {
                            task.Text = aTask[i];
                        }
                        else
                        {
                            moreTHRCD.Controls.Add(new LiteralControl("<tr><td>"));
                            TextBox newTask = new TextBox();
                            newTask.ID = "task" + i;
                            newTask.Text = aTask[i];
                            moreTHRCD.Controls.Add(newTask);
                            moreTHRCD.Controls.Add(new LiteralControl("</td>"));
                        }
                        if (hazards.Text == "")
                        {
                            hazards.Text = hazName[i];
                        }
                        else
                        {
                            moreTHRCD.Controls.Add(new LiteralControl("<td>"));
                            TextBox newHazard = new TextBox();
                            newHazard.ID = "hazard" + i;
                            newHazard.Text = hazName[i];
                            moreTHRCD.Controls.Add(newHazard);
                            moreTHRCD.Controls.Add(new LiteralControl("</td>"));
                        }
                        if (risk.Text == "")
                        {
                            risk.Text = riskLevel;
                        }
                        else
                        {
                            moreTHRCD.Controls.Add(new LiteralControl("<td>"));
                            TextBox risk2 = new TextBox();
                            risk2.ID = "risk" + i;
                            risk2.Text = riskLevel;
                            moreTHRCD.Controls.Add(risk2);
                            moreTHRCD.Controls.Add(new LiteralControl("</td>"));
                        }
                        if (controls.Text == "")
                        {
                            controls.Text = conName[i];
                        }
                        else
                        {
                            moreTHRCD.Controls.Add(new LiteralControl("<td>"));
                            TextBox newControl = new TextBox();
                            newControl.ID = "control" + i;
                            newControl.Text = conName[i];
                            moreTHRCD.Controls.Add(newControl);
                            moreTHRCD.Controls.Add(new LiteralControl("</td>"));
                        }
                        if (dateBox > 0)
                        {
                            moreTHRCD.Controls.Add(new LiteralControl("<td>"));
                            TextBox newDate = new TextBox();
                            newDate.ID = "date" + i;
                            
                            moreTHRCD.Controls.Add(newDate);
                            moreTHRCD.Controls.Add(new LiteralControl("</td></tr>"));
                        }
                        dateBox++;
                    }
                    
                    //close connection
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                //error checking
                errorForm.Text = "error loading form data " + ex.Message;
            }
        }
        protected void PopulateDataTable()
        {
            int formId = Convert.ToInt32(Request.QueryString["id"]);
            // Assuming you have a connection string named "HazardAssessmentDatabase" in your web.config
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString;

            // Define your SQL query to retrieve control categories from the database
            string query = "SELECT * from formtask where form_id=" + formId + ";"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //tasks
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter tAdapter = new SqlDataAdapter(command);
                DataTable taskTable = new DataTable();
                tAdapter.Fill(taskTable);

                //hazards
                string hazId = getHazardId(formId);
                query = "SELECT * from hazard where hazard_id=" + hazId + ";";
                command = new SqlCommand(query, connection);
                SqlDataAdapter hAdapter = new SqlDataAdapter(command);
                DataTable hazardTable = new DataTable();
                hAdapter.Fill(taskTable);

                string conId = getControlId(formId);
                query = "SELECT * from control where con_id=" + conId + ";";
                command = new SqlCommand(query, connection);
                SqlDataAdapter cAdapter = new SqlDataAdapter(command);
                DataTable controlTable = new DataTable();
                cAdapter.Fill(taskTable);



                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append(" <table class='formTable' style ='border:2px;border-style:solid;'>");
                htmlTable.Append("<tr><th>Tasks</th><th>Hazards</th><th>Risk</th><th>Controls</th><th>Date Implemented</th></tr>");

                foreach (DataRow row in taskTable.Rows)
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td><input type='text' value='" + row["Task"].ToString() + "' /></td>");
                    htmlTable.Append("<td><input type='text' value='" + row["Hazard_Name"].ToString() + "' /></td>");
                    htmlTable.Append("<td><input type='text' value='" + row["Con_Name"].ToString() + "' /></td>");
                    htmlTable.Append("</tr>");
                }

                htmlTable.Append("</table>");

               // dataContainer.InnerHtml = htmlTable.ToString();
            }
        }
        protected string getHazardId(int formId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
            string hazId = "";
            string query = "SELECT * from formtask where form_id=" + formId + ";";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //assign variables
                    
                    hazId = reader["Hazard_ID"].ToString();
                    reader.Close();

                }
                else
                {
                    reader.Close();

                }

            }

            return hazId;
        }
        protected string getControlId(int formId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
            string conId = "";
            string query = "SELECT * from formcontrol where form_id=" + formId + ";";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    conId = reader["con_id"].ToString();
                    reader.Close();
                }
                else
                {
                    reader.Close();

                }

            }
            return conId;

        }
        protected void DeleteForm_Click(object sender, EventArgs e)
        {
            con.Open();
            int formId = Convert.ToInt32(Request.QueryString["id"]);
            //TODO add confirmation?
            //delete task from the form
            string query = "Delete from formtask where form_id="+formId + ";";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            // deelte the control form that form
            query = "delete from formcontrol where form_id="+formId+ ";";
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            //delete the hazards from that form
            query = "delete from formhazard where form_id=" + formId + ";";
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            //delete the form itself
            query = "delete from form where form_id=" + formId + ";";
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            //It is done in this order to prevent foreign keys from preventing data deletion.

            con.Close();
            //redirect once done
            Response.Redirect("index.aspx");
        }
        protected void EditForm_Click(object sender, EventArgs e)
        {
            //get id and redirect to edit page with id
            string id = Request.QueryString["id"];
            Response.Redirect("SpecificFormEdit.aspx?id="+id);
        }
    }
}