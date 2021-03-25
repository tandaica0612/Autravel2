using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Passenger
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Passenger()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Passenger()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Passenger_ID { get; set; }
        public string Passenger_FullName { get; set; }
        public string Passenger_Type { get; set; }
        public bool Passenger_Gender { get; set; }
        public string Passenger_Identification { get; set; }
        public string Passenger_Address { get; set; }
        public string Passenger_Email { get; set; }
        public string Passenger_Phone { get; set; }
        public int BookingItem_ID { get; set; }
        public int Product_ID { get; set; }
        public int Booking_ID { get; set; }   
    }
}