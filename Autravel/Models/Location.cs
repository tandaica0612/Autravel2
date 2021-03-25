using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Location
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Location()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Location()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Location_ID { get; set; }
        public string Location_Name { get; set; }
        public string Location_Description { get; set; }
        public string Location_images { get; set; }
        //--------------------------------------------------------------------------
        public List<Location> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Location> l_ContactPersons = new List<Location>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Location m_ContactPersons = new Location();
                    m_ContactPersons.Location_ID = smartReader.GetInt32("Location_ID");
                    m_ContactPersons.Location_Name = smartReader.GetString("Location_Name");
                    m_ContactPersons.Location_Description = smartReader.GetString("Location_Description");
                    m_ContactPersons.Location_images = smartReader.GetString("Location_images");
                    l_ContactPersons.Add(m_ContactPersons);
                }
                smartReader.disposeReader(reader);
                return l_ContactPersons;
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
                SqlCommand cmd = new SqlCommand("LocationInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Location_Name", this.Location_Name));
                cmd.Parameters.Add(new SqlParameter("@Location_Description", this.Location_Description));
                cmd.Parameters.Add(new SqlParameter("@Location_images", this.Location_images));
                cmd.Parameters.Add("@Location_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Location_ID = (cmd.Parameters["@Location_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Location_ID"].Value);
                return this.Location_ID;
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
                SqlCommand cmd = new SqlCommand("LocationUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Location_ID", this.Location_ID));
                cmd.Parameters.Add(new SqlParameter("@Location_Name", this.Location_Name));
                cmd.Parameters.Add(new SqlParameter("@Location_Description", this.Location_Description));
                cmd.Parameters.Add(new SqlParameter("@Location_images", this.Location_images));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Location> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("LocationGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Location> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Location GetByLocationID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("LocationGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LocationID", id));
                List<Location> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as Location;
                else return new Location();
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
                SqlCommand cmd = new SqlCommand("LocationDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Location_ID", this.Location_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}