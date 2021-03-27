using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Autravel.Controllers.Huy.Models;
using Autravel.Extentions;

namespace Autravel.Models
{
    public class BookingTour
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public BookingTour()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~BookingTour()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int BookingTour_ID { get; set; }
        public int Tour_ID { get; set; }
        public int BookingTour_NumberPassenger { get; set; }
        public DateTime Booking_CreateDate { get; set; }
        public DateTime Booking_DepartureDate { get; set; }
        public DateTime Booking_ArrivalDate { get; set; }
        public int Booking_Status { get; set; }
        public int Booking_UserHandler { get; set; }
        public int Booking_TotalPrice { get; set; }
        public int Booking_Surcharge { get; set; }
        public string Booking_Note { get; set; }
        public string HoTen { get; set; }
        public string Phone  { get; set; }
        public string Email { get; set; }
        public string YeuCauDacBiet { get; set; }
        public int adult_number { get; set; }
        public int child_number { get; set; }
        public int BABY_number { get; set; }
        public string CarType { get; set; }
        public string AirType { get; set; }
        public string Room_ID { get; set; }

        public void Insert()
        {
            string sql = @"INSERT INTO [dbo].[BookingTour]
           ([Tour_ID]
           ,[BookingTour_NumberPassenger]
           ,[Booking_CreateDate]
           ,[Booking_DepartureDate]
           ,[Booking_ArrivalDate]
           ,[Booking_Status]
            ,[Booking_TotalPrice]
            ,[HoTen]
           ,[Phone]
           ,[Email]
           ,[YeuCauDacBiet]
           ,[adult_number]
           ,[child_number]
           ,[BABY_number]
           ,[CarType]
           ,[AirType]
           ,[Room_ID]
)
     VALUES
           (N'#Tour_ID#'
           , N'#BookingTour_NumberPassenger#'
           , GETDATE()
           ,  '#Booking_DepartureDate#'
           , '#Booking_ArrivalDate#'
           , N'#Booking_Status#'
           , N'#Booking_TotalPrice#'
           , N'#HoTen#'
           , N'#Phone#'
           , N'#Email#'
           , N'#YeuCauDacBiet#'
           , #adult_number#
           , #child_number#
           , #BABY_number#
           , N'#CarType#'
           , N'#AirType#'
           , N'#Room_ID#'
)";
            sql = sql.Replace("#Tour_ID#", this.Tour_ID.ToString());
            sql = sql.Replace("#BookingTour_NumberPassenger#", this.BookingTour_NumberPassenger.ToString());
            var status = SqlModule.GetDataTable("SELECT top 1 Config_id  FROM [ConfigInfomation] WHERE [Config_Field]='BookingTourStatus' order by Config_Value").FirstOrDefault("Config_id");
            sql = sql.Replace("#Booking_Status#", status);
            sql = sql.Replace("#Booking_TotalPrice#", this.Booking_TotalPrice.ToString());
            sql = sql.Replace("#Phone#", this.Phone);
            sql = sql.Replace("#HoTen#", this.HoTen);
            sql = sql.Replace("#Email#", this.Email);
            sql = sql.Replace("#YeuCauDacBiet#", this.YeuCauDacBiet);
            sql = sql.Replace("#adult_number#", this.adult_number.ToString());
            sql = sql.Replace("#child_number#", this.child_number.ToString());
            sql = sql.Replace("#BABY_number#", this.BABY_number.ToString());
            sql = sql.Replace("#CarType#", this.CarType);
            sql = sql.Replace("#AirType#", this.AirType);
            sql = sql.Replace("#Room_ID#", this.Room_ID);
            sql = sql.Replace("#Booking_DepartureDate#", this.Booking_DepartureDate.ToString("yyyy-MM-dd"));
            sql = sql.Replace("#Booking_ArrivalDate#", this.Booking_ArrivalDate.ToString("yyyy-MM-dd"));
            SqlModule.ExcuteCommand(sql);
        }

    }
}

