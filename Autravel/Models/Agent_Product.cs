using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Agent_Product
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Agent_Product()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Agent_Product()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Agent_Product_ID { get; set; }
        public int Agents_ID { get; set; }
        public bool Hotel_Active { get; set; }
        public bool Tour_Active { get; set; }
        public bool Ticket_Active { get; set; }
    }
}