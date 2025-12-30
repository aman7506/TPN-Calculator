using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TNS_Calculations
{
    public class ExceptionLogging
    {
        private static String exepurl;
        static SqlConnection cn;
        private static void connecttion()
        {
            ///string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DigitizingAuditChecklists"].ConnectionString;
            ////cn = new SqlConnection(constring);
            //////cn.Open();
            ///
         // Retrieve connection string from the config file
            string constring = ConfigurationManager.ConnectionStrings["TPNCalculations"].ConnectionString;

            // Initialize the SqlConnection object
            cn = new SqlConnection(constring);

            // Open the connection
            cn.Open();
        }
        public static void SendExcepToDB(Exception exdb)
        {
            connecttion();
            exepurl = HttpContext.Current.Request.Url.ToString();
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            SqlCommand com = new SqlCommand("ExceptionLoggingToDataBase", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
            com.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
            com.Parameters.AddWithValue("@ExceptionURL", exepurl);
            com.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());
            com.ExecuteNonQuery();
        }
    }
}