using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class ShortCode
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public ShortCode()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~ShortCode()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public string ShortCode_Id { get; set; }
        public string ShortCode_name { get; set; }
        public string ShortCode_Value { get; set; }
    }
}