using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class User
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public User()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~User()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int User_ID { get; set; }
        public string User_Name { get; set; }
        public string User_transactionName { get; set; }
        public DateTime Birthday { get; set; }
        public string User_Image { get; set; }
        public string User_Address { get; set; }
        public string User_Email { get; set; }
        public string User_Phone { get; set; }
        public string User_Pass { get; set; }
        public bool User_Active { get; set; }
        public int User_Role { get; set; }
        public bool User_InternalStaff { get; set; }
        public int Agents_ID { get; set; }
        //--------------------------------------------------------------------------
        public List<User> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<User> l_User = new List<User>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    User m_User = new User();
                    m_User.User_ID = smartReader.GetInt32("User_ID");
                    m_User.User_Name = smartReader.GetString("User_Name");
                    m_User.User_transactionName = smartReader.GetString("User_transactionName");
                    m_User.Birthday = smartReader.GetDateTime("Birthday");
                    m_User.User_Image = smartReader.GetString("User_Image");
                    m_User.User_Address = smartReader.GetString("User_Address");
                    m_User.User_Email = smartReader.GetString("User_Email");
                    m_User.User_Phone = smartReader.GetString("User_Phone");
                    m_User.User_Pass = smartReader.GetString("User_Pass");
                    m_User.User_Active = smartReader.GetBoolean("User_Active");
                    m_User.User_Role = smartReader.GetInt32("User_Role");
                    m_User.User_InternalStaff = smartReader.GetBoolean("User_InternalStaff");
                    m_User.Agents_ID = smartReader.GetInt32("Agents_ID");
                    l_User.Add(m_User);
                }
                smartReader.disposeReader(reader);
                return l_User;
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
                SqlCommand cmd = new SqlCommand("UserInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Location_Name", this.User_Name));
                cmd.Parameters.Add(new SqlParameter("@User_transactionName", this.User_transactionName));
                cmd.Parameters.Add(new SqlParameter("@Birthday", this.Birthday));
                cmd.Parameters.Add(new SqlParameter("@User_Image", this.User_Image));
                cmd.Parameters.Add(new SqlParameter("@User_Address", this.User_Address));
                cmd.Parameters.Add(new SqlParameter("@User_Email", this.User_Email));
                cmd.Parameters.Add(new SqlParameter("@User_Phone", this.User_Phone));
                cmd.Parameters.Add(new SqlParameter("@User_Pass", this.User_Pass));
                cmd.Parameters.Add(new SqlParameter("@User_Active", this.User_Active));
                cmd.Parameters.Add(new SqlParameter("@User_Role", this.User_Role));
                cmd.Parameters.Add(new SqlParameter("@User_InternalStaff", this.User_InternalStaff));
                cmd.Parameters.Add(new SqlParameter("@Agents_ID", this.Agents_ID));
                cmd.Parameters.Add("@User_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.User_ID = (cmd.Parameters["@User_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@User_ID"].Value);
                return this.User_ID;
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
                cmd.Parameters.Add(new SqlParameter("@User_ID", this.User_ID));
                cmd.Parameters.Add(new SqlParameter("@Location_Name", this.User_Name));
                cmd.Parameters.Add(new SqlParameter("@User_transactionName", this.User_transactionName));
                cmd.Parameters.Add(new SqlParameter("@Birthday", this.Birthday));
                cmd.Parameters.Add(new SqlParameter("@User_Image", this.User_Image));
                cmd.Parameters.Add(new SqlParameter("@User_Address", this.User_Address));
                cmd.Parameters.Add(new SqlParameter("@User_Email", this.User_Email));
                cmd.Parameters.Add(new SqlParameter("@User_Phone", this.User_Phone));
                cmd.Parameters.Add(new SqlParameter("@User_Pass", this.User_Pass));
                cmd.Parameters.Add(new SqlParameter("@User_Active", this.User_Active));
                cmd.Parameters.Add(new SqlParameter("@User_Role", this.User_Role));
                cmd.Parameters.Add(new SqlParameter("@User_InternalStaff", this.User_InternalStaff));
                cmd.Parameters.Add(new SqlParameter("@Agents_ID", this.Agents_ID));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<User> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<User> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<User> GetAllBySelectDynamic(int AgentID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserGetAllSelectDynamic");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AgentID", AgentID));
                List<User> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<User> GetAllByAgentID(int AgentID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserGetAllByAgentID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AgentID", AgentID));
                List<User> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User CheckLogin(string UserName, string PassWord)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CheckLogin");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                cmd.Parameters.Add(new SqlParameter("@PassWord", PassWord));
                List<User> lAr = init(cmd);      
                if (lAr.Count >= 1) return lAr[0] as User;
                else return new User();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User CheckUserName(string UserName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserGetByUserName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                List<User> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as User;
                else return new User();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetByUserID(int UserID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserGetByUserID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                List<User> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as User;
                else return new User();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}