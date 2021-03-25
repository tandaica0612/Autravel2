using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Hotel
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Hotel()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Hotel()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Hotel_ID { get; set; }
        public string Hotel_Name { get; set; }
        public int Hotel_Location { get; set; }
        public string Hotel_Address { get; set; }
        public string Hotel_Content { get; set; }
        public float Hotel_StarRate { get; set; }
        public string Hotel_ListImage { get; set; }
        public int Product_ID { get; set; }
        public int UserCreate { get; set; }
        public string HotelImage { get; set; }
        public string Hotel_RulesRefund { get; set; }
        public DateTime Hotel_CreateDate { get; set; }
        public string Hotel_Facilities_ID { get; set; }
        public string Tag_ID { get; set; }
        //--------------------------------------------------------------------------
        public List<Hotel> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Hotel> l_Hotel = new List<Hotel>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Hotel m_Hotel = new Hotel();
                    m_Hotel.Hotel_ID = smartReader.GetInt32("Hotel_ID");
                    m_Hotel.Hotel_Name = smartReader.GetString("Hotel_Name");
                    m_Hotel.Hotel_Location = smartReader.GetInt32("Hotel_Location");
                    m_Hotel.Hotel_Address = smartReader.GetString("Hotel_Address");
                    m_Hotel.Hotel_Content = smartReader.GetString("Hotel_Content");
                    m_Hotel.HotelImage = smartReader.GetString("HotelImage");
                    m_Hotel.Hotel_StarRate = smartReader.GetFloat("Hotel_StarRate");
                    m_Hotel.Hotel_ListImage = smartReader.GetString("Hotel_ListImage");
                    m_Hotel.Product_ID = smartReader.GetInt32("Product_ID");
                    m_Hotel.UserCreate = smartReader.GetInt32("UserCreate");
                    m_Hotel.Hotel_CreateDate = smartReader.GetDateTime("Hotel_CreateDate");
                    m_Hotel.Hotel_RulesRefund = smartReader.GetString("Hotel_RulesRefund");
                    m_Hotel.Hotel_Facilities_ID = smartReader.GetString("Hotel_Facilities_ID");
                    m_Hotel.Tag_ID = smartReader.GetString("Tag_ID");
                    l_Hotel.Add(m_Hotel);
                }
                smartReader.disposeReader(reader);
                return l_Hotel;
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
                SqlCommand cmd = new SqlCommand("HotelInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_Name", this.Hotel_Name));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Location", this.Hotel_Location));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Address", this.Hotel_Address));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Content", this.Hotel_Content));
                cmd.Parameters.Add(new SqlParameter("@HotelImage", this.HotelImage));
                cmd.Parameters.Add(new SqlParameter("@Hotel_StarRate", this.Hotel_StarRate));
                cmd.Parameters.Add(new SqlParameter("@Hotel_ListImage", this.Hotel_ListImage));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", this.Product_ID));
                cmd.Parameters.Add(new SqlParameter("@UserCreate", this.UserCreate));
                cmd.Parameters.Add(new SqlParameter("@Hotel_CreateDate", this.Hotel_CreateDate));
                cmd.Parameters.Add(new SqlParameter("@Hotel_RulesRefund", this.Hotel_RulesRefund));
                cmd.Parameters.Add("@Hotel_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Hotel_ID = (cmd.Parameters["@Hotel_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Hotel_ID"].Value);
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
                SqlCommand cmd = new SqlCommand("HotelUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", this.Hotel_ID));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Name", this.Hotel_Name));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Location", this.Hotel_Location));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Address", this.Hotel_Address));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Content", this.Hotel_Content));
                cmd.Parameters.Add(new SqlParameter("@HotelImage", this.HotelImage));
                cmd.Parameters.Add(new SqlParameter("@Hotel_StarRate", this.Hotel_StarRate));
                cmd.Parameters.Add(new SqlParameter("@Hotel_ListImage", this.Hotel_ListImage));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", this.Product_ID));
                cmd.Parameters.Add(new SqlParameter("@UserCreate", this.UserCreate));
                cmd.Parameters.Add(new SqlParameter("@Hotel_CreateDate", this.Hotel_CreateDate));
                cmd.Parameters.Add(new SqlParameter("@Hotel_RulesRefund", this.Hotel_RulesRefund));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Facilities_ID", this.Hotel_Facilities_ID));
                cmd.Parameters.Add(new SqlParameter("@Tag_ID", this.Tag_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Hotel> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("HotelGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Hotel> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Hotel> GetAllByLocation(int LocationID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("HotelGetByLocation");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LocationID", LocationID));
                List<Hotel> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Hotel GetAllByHotelID(int HotelID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("HotelGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", HotelID));
                List<Hotel> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as Hotel;
                else return new Hotel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}