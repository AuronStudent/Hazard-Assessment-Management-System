using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Security.Cryptography;

namespace Hazard_Assessment_Management_System
{
    public partial class OHSForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)

        {
            if (IsPostBack)
            {
                if (ViewState["ControlCount"] != null)
                {
                    int controlCount = Convert.ToInt32(ViewState["ControlCount"]);



                    for (int i = 0; i < controlCount; i++)
                    {

                        TextBox newTask = new TextBox();
                        newTask.ID = "task" + i.ToString();
                        phTask.Controls.Add(new LiteralControl("Task <br />"));
                        phTask.Controls.Add(newTask);
                        phTask.Controls.Add(new LiteralControl("<br />"));

                    }

                }

                if (ViewState["ControlCounts"] != null)
                {
                    int controlCount;
                    if (int.TryParse(ViewState["ControlCounts"].ToString(), out controlCount))
                    {

                        for (int i = 0; i < controlCount; i++)
                        {
                            DropDownList newCatHaz = new DropDownList();
                            newCatHaz.ID = "ddlCatHaz" + i.ToString();
                            LoadHazardCategories(newCatHaz);

                            DropDownList newHaz = new DropDownList();
                            newHaz.ID = "ddlHaz" + i.ToString();
                            LoadHazards(newHaz);

                            TextBox newLikelyHood = new TextBox();
                            newLikelyHood.ID = "Likelyhood" + i.ToString();

                            TextBox newSeverity = new TextBox();
                            newSeverity.ID = "Severity" + i.ToString();
                            TextBox newFrequency = new TextBox();
                            newFrequency.ID = "Frequency" + i.ToString();

                            phHaz.Controls.Add(new LiteralControl("Hazard Category<br />"));
                            phHaz.Controls.Add(newCatHaz);
                            phHaz.Controls.Add(new LiteralControl("Hazard<br />"));
                            phHaz.Controls.Add(newHaz);
                            phHaz.Controls.Add(new LiteralControl("<br />Likelihood<br />"));
                            phHaz.Controls.Add(newLikelyHood);
                            phHaz.Controls.Add(new LiteralControl("<br />Severity<br />"));
                            phHaz.Controls.Add(newSeverity);
                            phHaz.Controls.Add(new LiteralControl("<br />Frequency<br />"));
                            phHaz.Controls.Add(newFrequency);
                            phHaz.Controls.Add(new LiteralControl("<br />"));

                        }
                    }
                }
                if (ViewState["ControlCounter"] != null)
                {
                    int controlCount;
                    if (int.TryParse(ViewState["ControlCounter"].ToString(), out controlCount))
                    {

                        for (int i = 0; i < controlCount; i++)
                        {
                            DropDownList newControl = new DropDownList();
                            newControl.ID = "ddlCon" + i.ToString();
                            LoadControls(newControl);

                            phCon.Controls.Add(new LiteralControl(" Control<br />"));
                            phCon.Controls.Add(newControl);
                            phCon.Controls.Add(new LiteralControl("<br />"));

                        }
                    }
                }
            }



            if (!IsPostBack)
            {

                LoadDepartments();
                LoadHazards();
                LoadControls();
                LoadHazardCategories();
            }

        }
        //Loads all the departments in the database into a dropdown menu
        private void LoadDepartments()
        {

            DataTable departments = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Dep_ID, Dep_Name FROM dbo.Department", con);
                    adapter.Fill(departments);

                    ddlDep.DataSource = departments;
                    //sets the text of each option as the department name
                    ddlDep.DataTextField = "Dep_Name";
                    //sets the value of each option as the department id
                    ddlDep.DataValueField = "Dep_ID";
                    ddlDep.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorDep.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlDep.Items.Insert(0, new ListItem("<Select Department>", "0"));
            con.Close();
        }
        private void LoadHazards()
        {

            DataTable hazards = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Hazard_ID, Hazard_Name FROM dbo.Hazard", con);
                    adapter.Fill(hazards);

                    ddlHaz.DataSource = hazards;
                    //sets the text for each option to hazard name
                    //TODO make it from categories and not jusst choosing a hazard
                    ddlHaz.DataTextField = "Hazard_Name";
                    //sets the value of each option to hazard ID
                    ddlHaz.DataValueField = "Hazard_ID";
                    ddlHaz.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorHaz.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlHaz.Items.Insert(0, new ListItem("<Select Hazard>", "0"));
            con.Close();

        }
        private void LoadControls()
        {

            DataTable controls = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Con_ID, Con_Name FROM dbo.Control", con);
                    adapter.Fill(controls);

                    ddlCon.DataSource = controls;
                    //same as above
                    //TODO make it come from a category not from control
                    ddlCon.DataTextField = "Con_Name";
                    //same as above
                    ddlCon.DataValueField = "Con_ID";
                    ddlCon.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorCon.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlCon.Items.Insert(0, new ListItem("<Select Control>", "0"));
            con.Close();
        }
        private void LoadHazardCategories()
        {
            DataTable hazardCategories = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Hazard_Cat_Name FROM HazardCategory", con);
                    adapter.Fill(hazardCategories);

                    ddlCatHaz.DataSource = hazardCategories;
                    ddlCatHaz.DataTextField = "Hazard_Cat_Name";
                    ddlCatHaz.DataValueField = "Hazard_Cat_Name";

                    ddlCatHaz.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorCon.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlCatHaz.Items.Insert(0, new ListItem("<Select Hazard Category>", "0"));
            con.Close();
        }
        private void LoadHazardCategories(DropDownList ddl)
        {
            DataTable hazardCategories = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Hazard_Cat_Name FROM HazardCategory", con);
                    adapter.Fill(hazardCategories);

                    ddlCatHaz.DataSource = hazardCategories;
                    ddlCatHaz.DataTextField = "Hazard_Cat_Name";
                    ddlCatHaz.DataValueField = "Hazard_Cat_Name";

                    ddlCatHaz.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorCon.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlCatHaz.Items.Insert(0, new ListItem("<Select Hazard Category>", "0"));
            con.Close();
        }
        protected void SubmitForm_Click(object sender, EventArgs e)
        {
            string formIDString = "";
            int formID = 0; //ID of the form
            //getting the current date and review date
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            int reviewYear = currentYear + 1; // added one to review year
            //setting both dates into one string
            string reviewDate = currentMonth.ToString() + "/" + currentDay.ToString() + "/" + reviewYear.ToString();
            string filledOutDate = currentMonth.ToString() + "/" + currentDay.ToString() + "/" + currentYear.ToString();
            //getting total risk
            int riskTotal = Int32.Parse(like.Text) + Int32.Parse(sev.Text) + Int32.Parse(freq.Text);
            try
            {
                con.Open(); //open connection

                string query = "insert into Form values(" + ddlDep.SelectedValue + ",'" + jobsite.Text + "','" + reviewDate + "','" + legalName.Text + "','" + filledOutDate + "',null,null,0," + riskTotal + ",'" + email.Text + "');"; //sql query string
                SqlCommand myCom = new SqlCommand(query, con); // put sql string into an sql command
                myCom.ExecuteNonQuery(); // execute the command and insert data into databse

                //This is to get the ID of the form we just inserted. if everything goes good, the other foreign key tables should be in sync with this one
                query = "SELECT TOP 1 form_id FROM form ORDER BY form_id DESC;";
                myCom = new SqlCommand(query, con);

                SqlDataReader reader = myCom.ExecuteReader();
                if (reader.Read())
                {
                    //read the formID
                    formIDString = reader["form_ID"].ToString();
                    reader.Close();
                }
                else
                {
                    reader.Close();
                }
                formID = Int32.Parse(formIDString);
                query = "insert into FormControl values(" + ddlCon.SelectedValue + "," + formID + ");";
                myCom = new SqlCommand(query, con);
                myCom.ExecuteNonQuery();

                query = "insert into FormHazard values(" + ddlHaz.SelectedValue + "," + formID + ");";
                myCom = new SqlCommand(query, con);
                myCom.ExecuteNonQuery();

                query = "insert into FormTask values(" + formID + "," + ddlHaz.SelectedValue + ",'" + task.Text + "');";
                myCom = new SqlCommand(query, con);
                myCom.ExecuteNonQuery();
                if (ViewState["ControlCount"]!=null) {
                
                for (int i = 0; i < (int)ViewState["ControlCount"]; i++)
                {

                    TextBox task = (TextBox)phTask.FindControl("task" + i.ToString());
                    if (task != null)
                    {
                        //EACH TASK MUST HAVE AT LEAST ONE HAZARD AND EACH HAZARD HAS AT LEAST ONE CONTROL

                        query = "insert into FormControl values(" + ddlCon.SelectedValue + "," + formID + ");";
                        myCom = new SqlCommand(query, con);
                        myCom.ExecuteNonQuery();
                        
                        query = "insert into FormHazard values(" + ddlHaz.SelectedValue + "," + formID + ");";
                        myCom = new SqlCommand(query, con);
                        myCom.ExecuteNonQuery();

                        query = "insert into FormTask values(" + formID + "," + ddlHaz.SelectedValue + ",'" + task.Text + "');";
                        myCom = new SqlCommand(query, con);
                        myCom.ExecuteNonQuery();
                    }
                }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                //error handling
                formError.Text = ex.Message;
            }
            finally
            {
                //set all values back to nothing to prevent postback errors.
                ddlDep.SelectedIndex = 0;
                jobsite.Text = "";
                legalName.Text = "";
                email.Text = "";
                like.Text = "";
                sev.Text = "";
                freq.Text = "";
                task.Text = "";
                ddlCon.SelectedIndex = 0;
                ddlHaz.SelectedIndex = 0;
            }


        }
        protected void btnAddTask_Click(object sender, EventArgs e) //add task button
        {
            //add task button
            //TODO add more tasks
            //TODO Tasks can have multiple hazards and hazards can have only one control. There needs to be one control per hazard on each task.
            //TODO add a way to remove tasks


            int controlCount = ViewState["ControlCount"] != null ? int.Parse(ViewState["ControlCount"].ToString()) : 0;
            controlCount++;
            ViewState["ControlCount"] = controlCount;


            TextBox newTask = new TextBox();
            newTask.ID = "task" + (controlCount - 1).ToString();

            phTask.Controls.Add(new LiteralControl("Task <br />"));
            phTask.Controls.Add(newTask);
            phTask.Controls.Add(new LiteralControl("<br />"));

        }
        private void LoadDepartments(DropDownList ddl)
        {

            DataTable departments = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Dep_ID, Dep_Name FROM dbo.Department", con);
                    adapter.Fill(departments);

                    ddl.DataSource = departments;
                    ddl.DataTextField = "Dep_Name";
                    ddl.DataValueField = "Dep_ID";
                    ddl.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorDep.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddl.Items.Insert(0, new ListItem("<Select Department>", "0"));
            con.Close();
        }
        protected void btnAddHazard_Click(object sender, EventArgs e) // add hazard button
        {
            //add hazard button
            //TODO add more hazards
            //TODO Tasks can have multiple hazards and hazards can have only one control. There needs to be one control per hazard on each task.
            //TODO add a way to remove hazards
            //TODO add a way to add more controls
            //TODO add a way to remove controls

            DropDownList newCatHaz = new DropDownList(); //create a new dropdown
            newCatHaz.ID = "ddlCatHaz" + phHaz.Controls.OfType<DropDownList>().Count().ToString(); // set the id of the dropdown to the number of dropdowns in the placeholder
            LoadHazardCategories(newCatHaz); //load hazard categories into the dropdown
            phHaz.Controls.Add(new LiteralControl("<br />")); //add a line break
            phHaz.Controls.Add(new LiteralControl("Hazard Category<br />")); //add hazard category label
            phHaz.Controls.Add(newCatHaz); //add hazard category dropdown

            DropDownList newHaz = new DropDownList(); //create a new dropdown
            newHaz.ID = "ddlHaz" + phHaz.Controls.OfType<DropDownList>().Count().ToString(); //set the id of the dropdown to the number of dropdowns in the placeholder
            LoadHazards(newHaz); //load hazards into the dropdown
            phHaz.Controls.Add(new LiteralControl("<br />")); // add a line break
            phHaz.Controls.Add(new LiteralControl("Hazard<br />")); //add hazard label
            phHaz.Controls.Add(newHaz); //add hazard dropdown


            TextBox newLikelyHood = new TextBox();
            newLikelyHood.ID = "like" + phHaz.Controls.OfType<TextBox>().Count().ToString();
            phHaz.Controls.Add(new LiteralControl("<br />Likelyhood <br />"));
            phHaz.Controls.Add(newLikelyHood);

            TextBox newSeverity = new TextBox();
            newSeverity.ID = "sev" + phHaz.Controls.OfType<TextBox>().Count().ToString();
            newSeverity.Text = sev.Text;
            phHaz.Controls.Add(new LiteralControl("<br />Severity <br />"));
            phHaz.Controls.Add(newSeverity);

            TextBox newFrequency = new TextBox();
            newFrequency.ID = "freq" + phTask.Controls.OfType<TextBox>().Count().ToString();
            newFrequency.Text = freq.Text;
            phHaz.Controls.Add(new LiteralControl("<br />Frequency <br />"));
            phHaz.Controls.Add(newFrequency);
            phHaz.Controls.Add(new LiteralControl("<br />"));
            ViewState["ControlCounts"] = phHaz.Controls.OfType<DropDownList>().Count().ToString();


        }
        protected void btnAddControl_Click(object sender, EventArgs e) //add control button
        {
            //add control button
            //TODO add more controls
            //TODO add a way to remove controls
            DropDownList newControl = new DropDownList();
            newControl.ID = "ddlCon" + phCon.Controls.OfType<DropDownList>().Count().ToString();
            LoadControls(newControl);
            phCon.Controls.Add(new LiteralControl("<br />"));
            phCon.Controls.Add(new LiteralControl("Control<br />"));
            phCon.Controls.Add(newControl);
            ViewState["ControlCounter"] = phCon.Controls.OfType<DropDownList>().Count().ToString();


        }
        protected void CancelForm_Click(object sender, EventArgs e)
        {
            //cancel button brings user back to home screen
            Response.Redirect("index.aspx");
        }
        private void LoadHazards(DropDownList ddlHaz)
        {

            DataTable hazards = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Hazard_ID, Hazard_Name FROM dbo.Hazard", con);
                    adapter.Fill(hazards);

                    ddlHaz.DataSource = hazards;
                    //sets the text for each option to hazard name
                    //TODO make it from categories and not jusst choosing a hazard
                    ddlHaz.DataTextField = "Hazard_Name";
                    //sets the value of each option to hazard ID
                    ddlHaz.DataValueField = "Hazard_ID";
                    ddlHaz.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorHaz.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlHaz.Items.Insert(0, new ListItem("<Select Hazard>", "0"));
            con.Close();

        }
        private void LoadControls(DropDownList ddlCon)
        {

            DataTable controls = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HazardAssessmentDatabase"].ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Con_ID, Con_Name FROM dbo.Control", con);
                    adapter.Fill(controls);

                    ddlCon.DataSource = controls;
                    //same as above
                    //TODO make it come from a category not from control
                    ddlCon.DataTextField = "Con_Name";
                    //same as above
                    ddlCon.DataValueField = "Con_ID";
                    ddlCon.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    errorCon.Text = ex.Message;
                }

            }

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            ddlCon.Items.Insert(0, new ListItem("<Select Control>", "0"));
            con.Close();
        }
    }
}