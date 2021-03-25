using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class LogUserHandler
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public LogUserHandler()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~LogUserHandler()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int LogUserHandler_ID { get; set; }
        public int User_ID { get; set; }
        public int Booking_ID { get; set; }
        public int Booking_StatusOld { get; set; }
        public int Booking_StatusNew { get; set; }
        public DateTime LogUserHandler_CreateDate { get; set; }
    }
}