using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class TourDetailPrice
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public TourDetailPrice()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~TourDetailPrice()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int TourDetailPrice_ID { get; set; }
        public int Tour_ID { get; set; }
        public DateTime TourDetailPrice_DepartureDate { get; set; }
        public DateTime TourDetailPrice_ArrivalDate { get; set; }
        public Int64 TourDetailPrice_Price1 { get; set; }
        public int TourDetailPrice_NumberPrice1 { get; set; }
        public Int64 TourDetailPrice_Price2 { get; set; }
        public int TourDetailPrice_NumberPrice2 { get; set; }
        public Int64 TourDetailPrice_Price3 { get; set; }
        public int TourDetailPrice_NumberPrice3 { get; set; }

        //--------------------------------------------------------------------------
        public List<TourDetailPrice> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<TourDetailPrice> l_TourDetailPrice = new List<TourDetailPrice>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    TourDetailPrice m_TourDetailPrice = new TourDetailPrice();
                    m_TourDetailPrice.TourDetailPrice_ID = smartReader.GetInt32("TourDetailPrice_ID");
                    m_TourDetailPrice.TourDetailPrice_ArrivalDate = smartReader.GetDateTime("TourDetailPrice_ArrivalDate");
                    m_TourDetailPrice.TourDetailPrice_DepartureDate = smartReader.GetDateTime("TourDetailPrice_DepartureDate");
                    m_TourDetailPrice.TourDetailPrice_Price1 = smartReader.GetInt64("TourDetailPrice_Price1");
                    m_TourDetailPrice.TourDetailPrice_NumberPrice1 = smartReader.GetInt32("TourDetailPrice_NumberPrice1");
                    m_TourDetailPrice.TourDetailPrice_Price2 = smartReader.GetInt64("TourDetailPrice_Price2");
                    m_TourDetailPrice.TourDetailPrice_NumberPrice2 = smartReader.GetInt32("TourDetailPrice_NumberPrice2");
                    m_TourDetailPrice.TourDetailPrice_Price3 = smartReader.GetInt64("TourDetailPrice_Price3");
                    m_TourDetailPrice.TourDetailPrice_NumberPrice3 = smartReader.GetInt32("TourDetailPrice_NumberPrice3");
                    l_TourDetailPrice.Add(m_TourDetailPrice);
                }
                smartReader.disposeReader(reader);
                return l_TourDetailPrice;
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
                SqlCommand cmd = new SqlCommand("TourDetailPriceInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tour_ID", this.Tour_ID));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_ArrivalDate", this.TourDetailPrice_ArrivalDate));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_DepartureDate", this.TourDetailPrice_DepartureDate));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_NumberPrice1", this.TourDetailPrice_NumberPrice1));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_Price1", this.TourDetailPrice_Price1));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_NumberPrice2", this.TourDetailPrice_NumberPrice2));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_Price2", this.TourDetailPrice_Price2));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_NumberPrice3", this.TourDetailPrice_NumberPrice3));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_Price3", this.TourDetailPrice_Price3));
                cmd.Parameters.Add("@TourDetailPrice_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.TourDetailPrice_ID = (cmd.Parameters["@TourDetailPrice_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@TourDetailPrice_ID"].Value);
                return this.TourDetailPrice_ID;
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
                SqlCommand cmd = new SqlCommand("TourDetailPriceUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_ID", this.TourDetailPrice_ID));
                cmd.Parameters.Add(new SqlParameter("@Tour_ID", this.Tour_ID));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_ArrivalDate", this.TourDetailPrice_ArrivalDate));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_DepartureDate", this.TourDetailPrice_DepartureDate));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_Price1", this.TourDetailPrice_Price1));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_NumberPrice1", this.TourDetailPrice_NumberPrice1));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_Price2", this.TourDetailPrice_Price2));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_NumberPrice2", this.TourDetailPrice_NumberPrice2));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_Price3", this.TourDetailPrice_Price3));
                cmd.Parameters.Add(new SqlParameter("@TourDetailPrice_NumberPrice3", this.TourDetailPrice_NumberPrice3));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<TourDetailPrice> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TourDetailPriceGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<TourDetailPrice> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TourDetailPrice GetByTourID(int TourID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TourDetailPriceGetByTourID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tour_ID", TourID));
                List<TourDetailPrice> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as TourDetailPrice;
                else return new TourDetailPrice();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}