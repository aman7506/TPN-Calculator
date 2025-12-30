using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;

namespace TNS_Calculations
{
    public partial class TransactionPage : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TPNCalculations"].ConnectionString;
        private DataTable GetTransactionData()
        {
            DateTime fromDate = DateTime.Parse(txtFromDate.Text);
            DateTime toDate = DateTime.Parse(txtToDate.Text);
            toDate = toDate.Date.AddDays(1).AddTicks(-1); // Include full day

            DataTable dt = new DataTable();
            string regNo = string.IsNullOrWhiteSpace(TextBoxUHID.Text) ? null : TextBoxUHID.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetAllTransactionsByDate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add common parameters
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    if (regNo == null)
                    {
                        command.Parameters.AddWithValue("@Action", "GetAllTransactionsByDate");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Action", "GetTransactionByUHID");
                        command.Parameters.AddWithValue("@RegistrationNo", regNo);
                    }

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('Error fetching data: {ex.Message}');</script>");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dt;
        }





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindGridViewCurrent();
   
            }
        }

        private DataTable GETCURRENTDATA()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"EXEC GetTriggeredPatientDataByWard"; // Calling the stored procedure

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('Error fetching data: {ex.Message}');</script>");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dt;
        }

        private void BindGridView()
        {
            GridView1.DataSource = GetTransactionData();
            GridView1.DataBind();
        }

        private void BindGridViewCurrent()
        {
            GridView1.DataSource = GETCURRENTDATA();
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            DateTime fromDate = DateTime.Parse(txtFromDate.Text);
            DateTime toDate = DateTime.Parse(txtToDate.Text);

            if (fromDate.Date == toDate.Date)
            {
                BindGridViewCurrent(); // Method for same-date filter
            }
            else
            {
                BindGridView(); // Method for different-date range filter
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PendingDetails")
            {
                string commandId = e.CommandArgument.ToString(); // This is the actual "id", not index
                string registrationNo = "", Admissiondate = "", PatientName = "", AgewithGender = "", Encounter="", DaysOfTPN="";
                string id = "";

                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lblId = (Label)row.FindControl("lblField_id");
                    if (lblId != null && lblId.Text == commandId)
                    {
                        id = lblId.Text;

                        Label lblRegNo = (Label)row.FindControl("lblRegNo");
                        registrationNo = lblRegNo?.Text ?? "";

                        Label lblAdmissionDate = (Label)row.FindControl("DOBlbl");
                        Admissiondate = lblAdmissionDate?.Text ?? "";

                        Label lblPatientName = (Label)row.FindControl("lblPatientName");
                        PatientName = lblPatientName?.Text ?? "";

                        Label lblAgeGender = (Label)row.FindControl("lblAgeGender");
                        AgewithGender = lblAgeGender?.Text ?? "";

                        Label lblEncounter = (Label)row.FindControl("lblEncounter");
                         Encounter = lblEncounter?.Text ?? "";

                        Label lblDaysOfTPN = (Label)row.FindControl("lblDaysOfTPN");
                        DaysOfTPN = lblDaysOfTPN?.Text ?? "";

                        break; // Found the row, no need to continue
                    }
                }

                if (!string.IsNullOrEmpty(id))
                {
                    // Redirect only if id was found
                    Response.Redirect($"TPNMAIN.aspx?RegNo={registrationNo}&PatientName={PatientName}&Admissiondate={Admissiondate}&AgewithGender={AgewithGender}&Encounter={Encounter}&DaysOfTPN={DaysOfTPN}&id={id}");
                }
            }


            if (e.CommandName == "ViewDetails")
            {

                string commandId = e.CommandArgument.ToString(); // This is the actual "id", not index
                string registrationNo = "", Admissiondate = "", PatientName = "", AgewithGender = "", Encounter = "", DaysOfTPN = "";
                string id = "";

                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lblId = (Label)row.FindControl("lblField_id");
                    if (lblId != null && lblId.Text == commandId)
                    {
                        id = lblId.Text;

                        Label lblRegNo = (Label)row.FindControl("lblRegNo");
                        registrationNo = lblRegNo?.Text ?? "";

                        Label lblAdmissionDate = (Label)row.FindControl("DOBlbl");
                        Admissiondate = lblAdmissionDate?.Text ?? "";

                        Label lblPatientName = (Label)row.FindControl("lblPatientName");
                        PatientName = lblPatientName?.Text ?? "";

                        Label lblAgeGender = (Label)row.FindControl("lblAgeGender");
                        AgewithGender = lblAgeGender?.Text ?? "";

                        Label lblEncounter = (Label)row.FindControl("lblEncounter");
                        Encounter = lblEncounter?.Text ?? "";

                        Label lblDaysOfTPN = (Label)row.FindControl("lblDaysOfTPN");
                        DaysOfTPN = lblDaysOfTPN?.Text ?? "";


                        break; // Found the row, no need to continue
                    }
                }

                if (!string.IsNullOrEmpty(id))
                {
                    // Redirect only if id was found
                    Response.Redirect($"TPNMAIN.aspx?RegNo={registrationNo}&PatientName={PatientName}&Admissiondate={Admissiondate}&AgewithGender={AgewithGender}&Encounter={Encounter}&DaysOfTPN={DaysOfTPN}&id={id}");
                }
            }
        

            if (e.CommandName == "Draft")
            {
                string commandId = e.CommandArgument.ToString(); // This is the actual "id", not index
                string registrationNo = "", Admissiondate = "", PatientName = "", AgewithGender = "", Encounter = "", DaysOfTPN = "";
                string id = "";

                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lblId = (Label)row.FindControl("lblField_id");
                    if (lblId != null && lblId.Text == commandId)
                    {
                        id = lblId.Text;

                        Label lblRegNo = (Label)row.FindControl("lblRegNo");
                        registrationNo = lblRegNo?.Text ?? "";

                        Label lblAdmissionDate = (Label)row.FindControl("DOBlbl");
                        Admissiondate = lblAdmissionDate?.Text ?? "";

                        Label lblPatientName = (Label)row.FindControl("lblPatientName");
                        PatientName = lblPatientName?.Text ?? "";

                        Label lblAgeGender = (Label)row.FindControl("lblAgeGender");
                        AgewithGender = lblAgeGender?.Text ?? "";

                        Label lblEncounter = (Label)row.FindControl("lblEncounter");
                        Encounter = lblEncounter?.Text ?? "";

                        Label lblDaysOfTPN = (Label)row.FindControl("lblDaysOfTPN");
                        DaysOfTPN = lblDaysOfTPN?.Text ?? "";
                        break; // Found the row, no need to continue
                    }
                }

                if (!string.IsNullOrEmpty(id))
                {
                    // Redirect only if id was found
                    Response.Redirect($"TPNMAIN.aspx?RegNo={registrationNo}&PatientName={PatientName}&Admissiondate={Admissiondate}&AgewithGender={AgewithGender}&Encounter={Encounter}&DaysOfTPN={DaysOfTPN}&id={id}");
                }
            }

        
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.Parse(txtFromDate.Text);
            DateTime toDate = DateTime.Parse(txtToDate.Text);

          /////  GetTransactionData();
            BindGridView();

            // Your data-binding logic here, e.g.:
            // GridView1.DataSource = GetTransactions(fromDate, toDate);
            // GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the label that holds the status
                Label finalSaveLabel = (Label)e.Row.FindControl("FinalSavelbl");

                // Find the corresponding buttons
                Button btnPending = (Button)e.Row.FindControl("btnPending");
                Button btnEdit = (Button)e.Row.FindControl("btnEdit");
                Button btnComplete = (Button)e.Row.FindControl("btnComplete");

                if (finalSaveLabel != null)
                {
                    string status = finalSaveLabel.Text.Replace("<b>", "").Replace("</b>", "").Trim();

                    if (status == "Completed")
                    {
                        e.Row.Cells[11].BackColor = System.Drawing.Color.FromArgb(34, 139, 34); // ForestGreen
                        finalSaveLabel.ForeColor = System.Drawing.Color.White;

                        btnPending.Visible = false;
                        btnEdit.Visible = false;
                        btnComplete.Visible = true;
                    }
                    else if (status == "Pending")
                    {
                        e.Row.Cells[11].BackColor = System.Drawing.Color.FromArgb(255, 111, 0); // Custom orange
                        finalSaveLabel.ForeColor = System.Drawing.Color.White;

                        btnPending.Visible = true;
                        btnEdit.Visible = false;
                        btnComplete.Visible = false;
                    }
                    else if (status == "Drafted")
                    {
                        e.Row.Cells[11].BackColor = System.Drawing.Color.Orange;
                        finalSaveLabel.ForeColor = System.Drawing.Color.White;

                        btnPending.Visible = false;
                        btnEdit.Visible = true;
                        btnComplete.Visible = false;
                    }
                }
            }

        }
    }
}