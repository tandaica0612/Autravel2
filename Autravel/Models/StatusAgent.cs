using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class StatusAgent
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public StatusAgent()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~StatusAgent()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Status_Id { get; set; }
        public string Status_Name { get; set; }
    }
}