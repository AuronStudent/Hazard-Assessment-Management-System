using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hazard_Assessment_Management_System.pages
{
    public partial class SpecificFormEdit : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadForm();
            }
            string id = Request.QueryString["id"];
        }
        private void LoadForm()
        {
            try
            {
                string conDesc = "";//unused currently
                string conName = "";
                string conId = "";
                string hazDesc = "";//unused currently
                string hazName = "";
                string depId = "";
                string jobType = "";
                string revDate = "";
                string nameFilledOut = "";
                string dateAssessed = "";
                string reviewdBy = "";
                string riskLevel = "";
                string userEmail = "";
                string aTask = "";
                string hazId = "";
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
                            //seet all variables of form
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
                        if (reader.Read())
                        {
                            //assign variables
                            aTask = reader["Task"].ToString();
                            hazId = reader["Hazard_ID"].ToString();
                            reader.Close();

                        }
                        else
                        {
                            reader.Close();

                        }

                    }
                    //get haazards for this form
                    query = "SELECT * from hazard where hazard_id=" + hazId + ";";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {


                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            hazName = reader["Hazard_Name"].ToString();
                            hazDesc = reader["Hazard_Desc"].ToString();//unused currently
                            reader.Close();

                        }
                        else
                        {
                            reader.Close();

                        }

                    }
                    //get controls form this form
                    query = "SELECT * from formcontrol where form_id=" + formId + ";";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {


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
                    //same as above
                    query = "SELECT * from control where con_id=" + conId + ";";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {


                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            conName = reader["con_name"].ToString();
                            conDesc = reader["con_desc"].ToString(); //unused currently
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
                    risk.Text = riskLevel;
                    email.Text = userEmail;
                    task.Text = aTask;
                    hazards.Text = hazName;
                    controls.Text = conName;

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
        protected void CancelForm_Click(object sender, EventArgs e)
        {
            //cancel button brings user back to same page.
            string id = Request.QueryString["id"];
            Response.Redirect("SpecificForm.aspx?id="+ id);
        }
        protected void SaveForm_Click(object sender, EventArgs e)
        {
            int formID = Int32.Parse(Request.QueryString["id"]);
            try
            {
                con.Open();
                string query = "update form set Job_Type='"+jobite.Text+"',Name_Filled_Out='"+name.Text+"',Reviewed_By_Name='"+reviewBy.Text+"',Risk="+risk.Text+",User_Email='"+email.Text+"' where form_id="+formID+";";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                //redirect back once done
                Response.Redirect("SpecificForm.aspx?id=" + formID);
            }
            catch (Exception ex)
            {
                //error handling
            }
            
        }
    }
}