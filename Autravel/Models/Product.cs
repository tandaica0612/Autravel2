using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Product
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Product()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Product()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public bool Product_available { get; set; }
    }
}