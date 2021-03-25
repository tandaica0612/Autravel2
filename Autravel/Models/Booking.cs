using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Booking
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Booking()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Booking()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Booking_ID { get; set; }
        public DateTime Booking_CreateDate { get; set; }
        public DateTime Booking_DepartureDate { get; set; }
        public DateTime Booking_ArrivalDate { get; set; }
        public int Booking_Status { get; set; }
        public int Booking_UserHandler { get; set; }
        public Int64 Booking_TotalPrice { get; set; }
        public Int64 Booking_Surcharge { get; set; }
        public int ContactInfo_ID { get; set; }
        public string Booking_Note { get; set; }
        public string Booking_Item { get; set; }
        public int Booking_CreateUser { get; set; }
        public int Booking_AgentID { get; set; }
    }
}