using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class TypeAgent
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public TypeAgent()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~TypeAgent()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Type_Id { get; set; }
        public string Type_Name { get; set; }
    }
}