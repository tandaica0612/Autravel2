using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class ContactInfo
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public ContactInfo()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~ContactInfo()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int ContactInfo_ID { get; set; }
        public string ContactInfo_Name { get; set; }
        public string ContactInfo_Address { get; set; }
        public string ContactInfo_Email { get; set; }
        public string ContactInfo_Phone { get; set; }



        public List<ContactInfo> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ContactInfo> l_ContactPersons = new List<ContactInfo>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ContactInfo m_ContactPersons = new ContactInfo();
                    m_ContactPersons.ContactInfo_ID = smartReader.GetInt32("ContactInfo_ID");
                    m_ContactPersons.ContactInfo_Name = smartReader.GetString("ContactInfo_Name");
                    m_ContactPersons.ContactInfo_Address = smartReader.GetString("ContactInfo_Address");
                    m_ContactPersons.ContactInfo_Email = smartReader.GetString("ContactInfo_Email");
                    m_ContactPersons.ContactInfo_Phone = smartReader.GetString("ContactInfo_Phone");
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
                SqlCommand cmd = new SqlCommand("ContactInfoInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Name", this.ContactInfo_Name));
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Address", this.ContactInfo_Address));
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Email", this.ContactInfo_Email));
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Phone", this.ContactInfo_Phone));
                 cmd.Parameters.Add("@ContactInfo_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ContactInfo_ID = (cmd.Parameters["@ContactInfo_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@ContactInfo_ID"].Value);
                return this.ContactInfo_ID;
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
                SqlCommand cmd = new SqlCommand("ContactInfoUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_ID", this.ContactInfo_ID));
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Name", this.ContactInfo_Name));
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Address", this.ContactInfo_Address));
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Email", this.ContactInfo_Email));
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_Phone", this.ContactInfo_Phone));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<ContactInfo> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ContactInfoGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<ContactInfo> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ContactInfo GetByContactInfoID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ContactInfoGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ContactInfoID", id));
                List<ContactInfo> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as ContactInfo;
                else return new ContactInfo();
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
                SqlCommand cmd = new SqlCommand("ContactInfoDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ContactInfo_ID", this.ContactInfo_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}