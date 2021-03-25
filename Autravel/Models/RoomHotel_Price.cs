using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class RoomHotel_Price
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public RoomHotel_Price()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~RoomHotel_Price()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int ID { get; set; }
        public int RoomHotel_ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Price1Night { get; set; }
        public int Price2Night { get; set; }
        public int Price3Night { get; set; }
        public int Price4Night { get; set; }
        public int PriceMoreNight { get; set; }
  
        public List<RoomHotel_Price> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RoomHotel_Price> l_RoomHotel_Price = new List<RoomHotel_Price>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RoomHotel_Price m_RoomHotel_Price = new RoomHotel_Price();
                    m_RoomHotel_Price.ID = smartReader.GetInt32("ID");
                    m_RoomHotel_Price.RoomHotel_ID = smartReader.GetInt32("RoomHotel_ID");
                    m_RoomHotel_Price.Price1Night = smartReader.GetInt32("Price1Night");
                    m_RoomHotel_Price.Price2Night = smartReader.GetInt32("Price2Night");
                    m_RoomHotel_Price.Price3Night = smartReader.GetInt32("Price3Night");
                    m_RoomHotel_Price.Price4Night = smartReader.GetInt32("Price4Night");
                    m_RoomHotel_Price.PriceMoreNight  = smartReader.GetInt32("PriceMoreNight");
                    m_RoomHotel_Price.ToDate = smartReader.GetDateTime("ToDate");
                    m_RoomHotel_Price.FromDate = smartReader.GetDateTime("FromDate");
                   
                    l_RoomHotel_Price.Add(m_RoomHotel_Price);
                }
                smartReader.disposeReader(reader);
                return l_RoomHotel_Price;
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
                SqlCommand cmd = new SqlCommand("RoomHotel_PriceInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add(new SqlParameter("@RoomHotel_ID", this.RoomHotel_ID));
                 cmd.Parameters.Add(new SqlParameter("@Price1Night", this.Price1Night));
                 cmd.Parameters.Add(new SqlParameter("@Price2Night", this.Price2Night));
                 cmd.Parameters.Add(new SqlParameter("@Price3Night", this.Price3Night));
                 cmd.Parameters.Add(new SqlParameter("@Price4Night", this.Price4Night));
                 cmd.Parameters.Add(new SqlParameter("@PriceMoreNight", this.PriceMoreNight));
                 cmd.Parameters.Add(new SqlParameter("@FromDate", this.FromDate));
                 cmd.Parameters.Add(new SqlParameter("@ToDate", this.ToDate));
               
                cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ID = (cmd.Parameters["@ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@ID"].Value);
                return this.ID;
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
                SqlCommand cmd = new SqlCommand("RoomHotel_PriceUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this.ID));
                cmd.Parameters.Add(new SqlParameter("@Price1Night", this.Price1Night));
                cmd.Parameters.Add(new SqlParameter("@Price2Night", this.Price2Night));
                cmd.Parameters.Add(new SqlParameter("@Price3Night", this.Price3Night));
                cmd.Parameters.Add(new SqlParameter("@Price4Night", this.Price4Night));
                cmd.Parameters.Add(new SqlParameter("@PriceMoreNight", this.PriceMoreNight));
                cmd.Parameters.Add(new SqlParameter("@FromDate", this.FromDate));
                cmd.Parameters.Add(new SqlParameter("@ToDate", this.ToDate));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<RoomHotel_Price> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotel_PriceGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<RoomHotel_Price> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
    
        public RoomHotel_Price GetRoomHotel_PriceByID(int RoomHotel_PriceID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RoomHotel_PriceGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", RoomHotel_PriceID));
                List<RoomHotel_Price> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as RoomHotel_Price;
                else return new RoomHotel_Price();
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
                SqlCommand cmd = new SqlCommand("RoomHotel_Price_DeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this.ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}