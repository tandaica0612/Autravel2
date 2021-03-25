using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class SetupMailSMTP
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public SetupMailSMTP()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~SetupMailSMTP()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int SetupMailSMTP_ID { get; set; }
        public string SetupMailSMTP_Email { get; set; }
        public string SetupMailSMTP_Password { get; set; }
        public string SetupMailSMTP_Host { get; set; }
        public string SetupMailSMTP_Port { get; set; }
        public bool SetupMailSMTP_SSL { get; set; }
        public string SetupMailSMTP_Header { get; set; }
        public string SetupMailSMTP_Footer { get; set; }
        public int Agents_ID { get; set; }

        //--------------------------------------------------------------------------
        public int Insert()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SetupMailSMTPInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Email", this.SetupMailSMTP_Email));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Password", this.SetupMailSMTP_Password));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Host", this.SetupMailSMTP_Host));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Port", this.SetupMailSMTP_Port));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_SSL", this.SetupMailSMTP_SSL));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Header", this.SetupMailSMTP_Header));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Footer", this.SetupMailSMTP_Footer));
                cmd.Parameters.Add(new SqlParameter("@Agents_ID", this.Agents_ID));
                  cmd.Parameters.Add("@SetupMailSMTP_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SetupMailSMTP_ID = (cmd.Parameters["@SetupMailSMTP_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SetupMailSMTP_ID"].Value);
                return this.SetupMailSMTP_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SetupMailSMTP GetByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SetupMailSMTPGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_ID", id));
                List<SetupMailSMTP> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as SetupMailSMTP;
                else return new SetupMailSMTP();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SetupMailSMTP> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SetupMailSMTP> l_ContactPersons = new List<SetupMailSMTP>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SetupMailSMTP m_ContactPersons = new SetupMailSMTP();
                    m_ContactPersons.SetupMailSMTP_ID = smartReader.GetInt32("SetupMailSMTP_ID");
                    m_ContactPersons.SetupMailSMTP_Email = smartReader.GetString("SetupMailSMTP_Email");
                    m_ContactPersons.SetupMailSMTP_Password = smartReader.GetString("SetupMailSMTP_Password");
                    m_ContactPersons.SetupMailSMTP_Host = smartReader.GetString("SetupMailSMTP_Host");
                    m_ContactPersons.SetupMailSMTP_Port = smartReader.GetString("SetupMailSMTP_Port");
                    m_ContactPersons.SetupMailSMTP_SSL = smartReader.GetBoolean("SetupMailSMTP_SSL");
                    m_ContactPersons.SetupMailSMTP_Header = smartReader.GetString("SetupMailSMTP_Header");
                    m_ContactPersons.SetupMailSMTP_Footer = smartReader.GetString("SetupMailSMTP_Footer");
                    m_ContactPersons.Agents_ID = smartReader.GetInt32("Agents_ID");
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
                SqlCommand cmd = new SqlCommand("SetupMailSMTPUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_ID", this.SetupMailSMTP_ID));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Email", this.SetupMailSMTP_Email));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Password", this.SetupMailSMTP_Password));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Host", this.SetupMailSMTP_Host));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Port", this.SetupMailSMTP_Port));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_SSL", this.SetupMailSMTP_SSL));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Header", this.SetupMailSMTP_Header));
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_Footer", this.SetupMailSMTP_Footer));
                cmd.Parameters.Add(new SqlParameter("@Agents_ID", this.Agents_ID));
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
                SqlCommand cmd = new SqlCommand("SetupMailSMTPDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SetupMailSMTP_ID", this.SetupMailSMTP_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}