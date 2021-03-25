using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class BookingHotel
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public BookingHotel()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~BookingHotel()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int BookingHotel_ID { get; set; }
        public int Hotel_ID { get; set; }
        public int RoomHotel_ID { get; set; }
        public Int64 BookingHotel_TotalPrice { get; set; }
        public DateTime BookingHotel_CreateDate { get; set; }
        public int BookingHotel_NumberPassenger { get; set; }
    }
}