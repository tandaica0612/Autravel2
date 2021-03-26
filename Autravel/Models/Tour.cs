using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Tour
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Tour()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Tour()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Tour_ID { get; set; }
        public string Tour_Name { get; set; }
        public string Tour_Topic { get; set; }         
        public int Tour_Location { get; set; }
        public string Tour_OrganizationalUnit { get; set; }
        public DateTime Tour_DepartureDate { get; set; }
        public int Tour_TimeZone { get; set; }
        public Int64 Tour_PriceRoom { get; set; }
        public Int64 Tour_PriceTicket { get; set; }
        public Int64 Tour_PriceFee { get; set; }
        public int Tour_Price { get; set; }
        public int Tour_PriceSale { get; set; }
        public int Tour_Qty { get; set; }
        public float Tour_Score { get; set; }
        public string Tour_Description { get; set; }
        public string Tour_Content { get; set; }
        public string Tour_Itinerary { get; set; }
        public string Tour_Schedule { get; set; }
        public string Tour_Rules { get; set; }
        public string Tour_ListImage { get; set; }
        public string Tour_Image { get; set; }
        public int Product_ID { get; set; }
        public int Hotel_ID { get; set; }
        public bool Tour_Fixed { get; set; }
        public int UserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Tour_Active { get; set; }
        public double Tour_StarRate { get; set; }

        //--------------------------------------------------------------------------
        public List<Tour> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Tour> l_Tour = new List<Tour>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Tour m_Tour = new Tour();
                    m_Tour.Tour_ID = smartReader.GetInt32("Tour_ID");
                    m_Tour.Tour_Name = smartReader.GetString("Tour_Name");
                    m_Tour.Tour_Location = smartReader.GetInt32("Tour_Location");
                    m_Tour.Tour_Topic = smartReader.GetString("Tour_Topic");
                    m_Tour.Tour_OrganizationalUnit = smartReader.GetString("Tour_OrganizationalUnit");
                    m_Tour.Tour_DepartureDate = smartReader.GetDateTime("Tour_DepartureDate");
                    m_Tour.Tour_TimeZone = smartReader.GetInt32("Tour_TimeZone");
                    m_Tour.Tour_Price = smartReader.GetInt32("Tour_Price");
                    m_Tour.Tour_PriceSale = smartReader.GetInt32("Tour_PriceSale");
                    m_Tour.Tour_Qty = smartReader.GetInt32("Tour_Qty");
                    m_Tour.Tour_Score = smartReader.GetFloat("Tour_Score");
                     m_Tour.Tour_Description = smartReader.GetString("Tour_Description");
                    m_Tour.Tour_Content = smartReader.GetString("Tour_Content");
                    m_Tour.Tour_Itinerary = smartReader.GetString("Tour_Itinerary");
                    m_Tour.Tour_Schedule = smartReader.GetString("Tour_Schedule");
                    m_Tour.Tour_Rules = smartReader.GetString("Tour_Rules"); 
                    m_Tour.Tour_ListImage = smartReader.GetString("Tour_ListImage");
                    m_Tour.Tour_Image = smartReader.GetString("Tour_Image");
                    m_Tour.Product_ID = smartReader.GetInt32("Product_ID");
                    m_Tour.Tour_Fixed = smartReader.GetBoolean("Tour_Fixed"); 
                    m_Tour.UserCreate = smartReader.GetInt32("UserCreate");
                    m_Tour.CreateDate = smartReader.GetDateTime("CreateDate");
                    m_Tour.Tour_Active = smartReader.GetBoolean("Tour_Active");
                    m_Tour.Tour_StarRate = smartReader.GetFloat("Tour_StarRate");
                    m_Tour.Hotel_ID = smartReader.GetInt32("Hotel_ID");
                    l_Tour.Add(m_Tour);
                }
                smartReader.disposeReader(reader);
                return l_Tour;
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
                SqlCommand cmd = new SqlCommand("TourInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", this.Hotel_ID));
                cmd.Parameters.Add(new SqlParameter("@Tour_Name", this.Tour_Name));
                cmd.Parameters.Add(new SqlParameter("@Tour_Location", this.Tour_Location));
                cmd.Parameters.Add(new SqlParameter("@Tour_OrganizationalUnit", this.Tour_OrganizationalUnit));
                cmd.Parameters.Add(new SqlParameter("@Tour_DepartureDate", this.Tour_DepartureDate));
                cmd.Parameters.Add(new SqlParameter("@Tour_TimeZone", this.Tour_TimeZone));
                cmd.Parameters.Add(new SqlParameter("@Tour_Price", this.Tour_Price));
                cmd.Parameters.Add(new SqlParameter("@Tour_Description", this.Tour_Description));
                cmd.Parameters.Add(new SqlParameter("@Tour_Content", this.Tour_Content));
                cmd.Parameters.Add(new SqlParameter("@Tour_Itinerary", this.Tour_Itinerary));
                cmd.Parameters.Add(new SqlParameter("@Tour_Schedule", this.Tour_Schedule)); 
                cmd.Parameters.Add(new SqlParameter("@Tour_Rules", this.Tour_Rules));
                cmd.Parameters.Add(new SqlParameter("@Tour_ListImage", this.Tour_ListImage));
                cmd.Parameters.Add(new SqlParameter("@Tour_Image", this.Tour_Image));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", this.Product_ID));
                cmd.Parameters.Add(new SqlParameter("@Tour_Fixed", this.Tour_Fixed));
                cmd.Parameters.Add(new SqlParameter("@UserCreate", this.UserCreate)); 
                cmd.Parameters.Add(new SqlParameter("@CreateDate", this.CreateDate));
                cmd.Parameters.Add(new SqlParameter("@Tour_Active", this.Tour_Active));
                cmd.Parameters.Add(new SqlParameter("@Tour_Topic", this.Tour_Topic));
                cmd.Parameters.Add(new SqlParameter("@Tour_StarRate", this.Tour_StarRate));
                cmd.Parameters.Add(new SqlParameter("@Tour_Qty", this.Tour_Qty));
                cmd.Parameters.Add(new SqlParameter("@Tour_Score", this.Tour_Score));
                 cmd.Parameters.Add(new SqlParameter("@Tour_PriceSale", this.Tour_PriceSale));
                cmd.Parameters.Add("@Tour_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Tour_ID = (cmd.Parameters["@Tour_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Tour_ID"].Value);
                return this.Tour_ID;
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
                SqlCommand cmd = new SqlCommand("TourUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tour_ID", this.Tour_ID));
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", this.Hotel_ID));
                cmd.Parameters.Add(new SqlParameter("@Tour_Name", this.Tour_Name));
                cmd.Parameters.Add(new SqlParameter("@Tour_Location", this.Tour_Location));
                cmd.Parameters.Add(new SqlParameter("@Tour_OrganizationalUnit", this.Tour_OrganizationalUnit));
                cmd.Parameters.Add(new SqlParameter("@Tour_DepartureDate", this.Tour_DepartureDate));
                cmd.Parameters.Add(new SqlParameter("@Tour_TimeZone", this.Tour_TimeZone));
                cmd.Parameters.Add(new SqlParameter("@Tour_Price", this.Tour_Price));
                cmd.Parameters.Add(new SqlParameter("@Tour_Description", this.Tour_Description));
                cmd.Parameters.Add(new SqlParameter("@Tour_Content", this.Tour_Content));
                cmd.Parameters.Add(new SqlParameter("@Tour_Itinerary", this.Tour_Itinerary));
                cmd.Parameters.Add(new SqlParameter("@Tour_Schedule", this.Tour_Schedule));
                cmd.Parameters.Add(new SqlParameter("@Tour_Rules", this.Tour_Rules));
                cmd.Parameters.Add(new SqlParameter("@Tour_ListImage", this.Tour_ListImage));
                cmd.Parameters.Add(new SqlParameter("@Tour_Image", this.Tour_Image));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", this.Product_ID));
                cmd.Parameters.Add(new SqlParameter("@Tour_Fixed", this.Tour_Fixed));
                cmd.Parameters.Add(new SqlParameter("@UserCreate", this.UserCreate));
                cmd.Parameters.Add(new SqlParameter("@Tour_Active", this.Tour_Active));
                cmd.Parameters.Add(new SqlParameter("@Tour_Topic", this.Tour_Topic));
                cmd.Parameters.Add(new SqlParameter("@Tour_StarRate", this.Tour_StarRate));
                cmd.Parameters.Add(new SqlParameter("@Tour_Qty", this.Tour_Qty));
                cmd.Parameters.Add(new SqlParameter("@Tour_Score", this.Tour_Score));
                 cmd.Parameters.Add(new SqlParameter("@Tour_PriceSale", this.Tour_PriceSale));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Tour> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TourGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Tour> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Tour> GetAllDynamic()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TourGetAllDynamic");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Tour> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Tour> GetAllByLocation(int LocationID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TourGetAllByLocation");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LocationID", LocationID));
                List<Tour> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tour GetByTourID(int TourID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TourGetAllByTourID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TourID", TourID));
                List<Tour> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as Tour;
                else return new Tour();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}