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

namespace Hazard_Assessment_Management_System
{
    public partial class index : System.Web.UI.Page
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
            string query = "SELECT * from form;";
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
                Response.Redirect("SpecificForm.aspx?id="+id);
            }
        }
    }
}