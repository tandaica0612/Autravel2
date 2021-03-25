using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class ConfigInfomation
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public ConfigInfomation()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~ConfigInfomation()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Config_id { get; set; }
        public string Config_Field { get; set; }
        public string Config_Title { get; set; }
        public string Config_Value { get; set; }

        public int Insert()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ConfigInfomationInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Config_Field", this.Config_Field));
                cmd.Parameters.Add(new SqlParameter("@Config_Title", this.Config_Title));
                cmd.Parameters.Add(new SqlParameter("@Config_Value", this.Config_Value));
                     cmd.Parameters.Add("@Config_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Config_id = (cmd.Parameters["@Config_id"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Config_id"].Value);
                return this.Config_id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ConfigInfomation GetByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ConfigInfomationGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Config_id", id));
                List<ConfigInfomation> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as ConfigInfomation;
                else return new ConfigInfomation();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ConfigInfomation> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ConfigInfomation> l_ContactPersons = new List<ConfigInfomation>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ConfigInfomation m_ContactPersons = new ConfigInfomation();
                    m_ContactPersons.Config_id = smartReader.GetInt32("Config_id");
                    m_ContactPersons.Config_Field = smartReader.GetString("Config_Field");
                    m_ContactPersons.Config_Title = smartReader.GetString("Config_Title");
                    m_ContactPersons.Config_Value = smartReader.GetString("Config_Value");
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
        //---------------------------------------------------------------------------------------------
        public void Update()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ConfigInfomationUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Config_id", this.Config_id));
                cmd.Parameters.Add(new SqlParameter("@Config_Field", this.Config_Field));
                cmd.Parameters.Add(new SqlParameter("@Config_Title", this.Config_Title));
                cmd.Parameters.Add(new SqlParameter("@Config_Value", this.Config_Value));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------

        public void DeleteByID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ConfigInfomationDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Config_id", this.Config_id));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}