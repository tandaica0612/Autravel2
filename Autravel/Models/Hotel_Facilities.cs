using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Autravel.Controllers.Huy.Models;

namespace Autravel.Models
{
    public class Hotel_Facilities
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Hotel_Facilities()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Hotel_Facilities()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Hotel_Facilities_ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
         //--------------------------------------------------------------------------
        public List<Hotel_Facilities> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Hotel_Facilities> l_ContactPersons = new List<Hotel_Facilities>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Hotel_Facilities m_ContactPersons = new Hotel_Facilities();
                    m_ContactPersons.Hotel_Facilities_ID = smartReader.GetInt32("Hotel_Facilities_ID");
                    m_ContactPersons.Name = smartReader.GetString("Name");
                    m_ContactPersons.Note = smartReader.GetString("Note");
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
                SqlCommand cmd = new SqlCommand("Hotel_FacilitiesInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
                cmd.Parameters.Add(new SqlParameter("@Note", this.Note));
                 cmd.Parameters.Add("@Hotel_Facilities_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Hotel_Facilities_ID = (cmd.Parameters["@Hotel_Facilities_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Hotel_Facilities_ID"].Value);
                return this.Hotel_Facilities_ID;
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
                SqlCommand cmd = new SqlCommand("Hotel_FacilitiesUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_Facilities_ID", this.Hotel_Facilities_ID));
                cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
                cmd.Parameters.Add(new SqlParameter("@Note", this.Note));
                 db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Hotel_Facilities> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotel_FacilitiesGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Hotel_Facilities> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Hotel_Facilities GetByHotel_FacilitiesID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotel_FacilitiesGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_FacilitiesID", id));
                List<Hotel_Facilities> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as Hotel_Facilities;
                else return new Hotel_Facilities();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<string> ConvertIDtoText(string ID="")
        {
            List<string> Result = new List<string>();
            var ListID = ID.Split(',');
            var data = SqlModule.GetDataTable("SELECT  [Hotel_Facilities_ID] ,[Name]  FROM [Hotel_Facilities]");
            foreach (DataRow item in data.Rows)
            {
                if (ListID.Contains(item["Hotel_Facilities_ID"].ToString()))
                {
                    Result.Add(item["Name"].ToString());
                }
            }
            return Result;
        }
        public void DeleteByID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotel_FacilitiesDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_Facilities_ID", this.Hotel_Facilities_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}