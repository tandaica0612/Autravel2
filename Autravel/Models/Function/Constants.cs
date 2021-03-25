using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Autravel.Models;
namespace Autravel.Models
{
    public class Constants
    {
        public static string CONNECTION_STRING = "Data Source=.\\SQLEXPRESS;Initial Catalog=Autravel;Integrated Security=True";
        public static int PAGESIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGESIZE"]);
        public static string DEFAULTEMAIL = ConfigurationManager.AppSettings["DEFAULTEMAIL"];
        public static string DEFAULTEMAILPASS = ConfigurationManager.AppSettings["DEFAULTEMAILPASS"];
        
    }
}