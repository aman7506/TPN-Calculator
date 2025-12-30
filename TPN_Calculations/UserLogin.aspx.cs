using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TNS_Calculations
{
    public partial class UserLogin : System.Web.UI.Page
    {
        SqlConnection cs = new SqlConnection(ConfigurationManager.ConnectionStrings["TPNCalculations"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            string connString = ConfigurationManager.ConnectionStrings["TPNCalculations"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM Authenticate WHERE UID = @Username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // In production, store hashed passwords!

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set specific session variables
                            Session["uid"] = reader["UID"].ToString();
                            Session["Actlocation"] = reader["LocationId"].ToString();
                            Session["Role"] = reader["Role"].ToString();
                            Response.Redirect("TransactionPage.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid username or password.";
                            lblMessage.Visible = true;
                        }
                    }
                }
            }
        }

    }
}