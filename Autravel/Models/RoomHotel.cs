using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class RoomHotel
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public RoomHotel()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~RoomHotel()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int RoomHotel_ID { get; set; }
        public int Hotel_ID { get; set; }
        public int RoomHotel_Type { get; set; }
        public string RoomHotel_Content { get; set; }
        public string RoomHotel_Title { get; set; }
        public string RoomHotel_Image { get; set; }
        public string RoomHotel_ListImage { get; set; }
        public string RoomHotel_Extensions { get; set; }
        public string RoomHotel_View { get; set; }
        public string RoomHotel_Bed { get; set; }
        public int RoomHotel_Adutls { get; set; }
        public int RoomHotel_Infants { get; set; }
        public float RoomHotel_Acreage { get; set; }
        public Int64 RoomHotel_TotalPrice { get; set; }
        public DateTime RoomHotel_DateStart { get; set; }
        public DateTime RoomHotel_DateEndArray { get; set; }
        public List<RoomHotel> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RoomHotel> l_RoomHotel = new List<RoomHotel>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RoomHotel m_RoomHotel = new RoomHotel();
                    m_RoomHotel.RoomHotel_ID = smartReader.GetInt32("RoomHotel_ID");
                    m_RoomHotel.RoomHotel_Title = smartReader.GetString("RoomHotel_Title");
                    m_RoomHotel.RoomHotel_Image = smartReader.GetString("RoomHotel_Image");
                    m_RoomHotel.RoomHotel_ListImage = smartReader.GetString("RoomHotel_ListImage");
                    m_RoomHotel.Hotel_ID = smartReader.GetInt32("Hotel_ID");
                    m_RoomHotel.RoomHotel_Type = smartReader.GetInt32("RoomHotel_Type");
                    m_RoomHotel.RoomHotel_Content = smartReader.GetString("RoomHotel_Content");
                    m_RoomHotel.RoomHotel_Extensions = smartReader.GetString("RoomHotel_Extensions");
                    m_RoomHotel.RoomHotel_View = smartReader.GetString("RoomHotel_View");
                    m_RoomHotel.RoomHotel_Bed = smartReader.GetString("RoomHotel_Bed");
                    m_RoomHotel.RoomHotel_Adutls = smartReader.GetInt32("RoomHotel_Adutls");
                    m_RoomHotel.RoomHotel_Infants = smartReader.GetInt32("RoomHotel_Infants");
                    m_RoomHotel.RoomHotel_Acreage = smartReader.GetFloat("RoomHotel_Acreage");
                    m_RoomHotel.RoomHotel_TotalPrice = smartReader.GetInt64("RoomHotel_TotalPrice");
                    m_RoomHotel.RoomHotel_DateStart = smartReader.GetDateTime("RoomHotel_DateStart");
                    m_RoomHotel.RoomHotel_DateEndArray = smartReader.GetDateTime("RoomHotel_DateEndArray");
                    l_RoomHotel.Add(m_RoomHotel);
                }
                smartReader.disposeReader(reader);
                return l_RoomHotel;
            }
            catch (SqlException err)
            {
                throw err;
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        //--------------------------------------------------------------------------
        public int Insert()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotelInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_Title", this.RoomHotel_Title));
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", this.Hotel_ID));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Type", this.RoomHotel_Type));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_Content", this.RoomHotel_Content));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Image", this.RoomHotel_Image));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Extensions", this.RoomHotel_Extensions));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_View", this.RoomHotel_View));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Bed", this.RoomHotel_Bed));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Acreage", this.RoomHotel_Acreage));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Adutls", this.RoomHotel_Adutls));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Infants", this.RoomHotel_Infants));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_TotalPrice", this.RoomHotel_TotalPrice));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_DateStart", this.RoomHotel_DateStart));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_DateEndArray", this.RoomHotel_DateEndArray));
                cmd.Parameters.Add("@RoomHotel_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.RoomHotel_ID = (cmd.Parameters["@RoomHotel_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@RoomHotel_ID"].Value);
                return this.Hotel_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public void Update()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotelUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_ID", this.RoomHotel_ID));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_Title", this.RoomHotel_Title));
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", this.Hotel_ID));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Type", this.RoomHotel_Type));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_Content", this.RoomHotel_Content));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Image", this.RoomHotel_Image));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_ListImage", this.RoomHotel_ListImage));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Extensions", this.RoomHotel_Extensions));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_View", this.RoomHotel_View));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Bed", this.RoomHotel_Bed));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Acreage", this.RoomHotel_Acreage));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Adutls", this.RoomHotel_Adutls));
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_Infants", this.RoomHotel_Infants));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_TotalPrice", this.RoomHotel_TotalPrice));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_DateStart", this.RoomHotel_DateStart));
                //cmd.Parameters.Add(new SqlParameter("@RoomHotel_DateEndArray", this.RoomHotel_DateEndArray));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<RoomHotel> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotelGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<RoomHotel> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<RoomHotel> GetAllByHotelID(int HotelID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotelGetAllByHotelID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", HotelID));
                List<RoomHotel> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RoomHotel GetRoomHotelByID(int RoomHotelID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotelGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_ID", RoomHotelID));
                List<RoomHotel> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as RoomHotel;
                else return new RoomHotel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteByID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotel_DeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RoomHotel_ID", this.RoomHotel_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}