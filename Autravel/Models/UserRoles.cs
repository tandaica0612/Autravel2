using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autravel.Models;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
namespace Autravel.Models
{
    
    public class UserRoles
    {
        
        private DBAccess db;
        //--------------------------------------------------------------------------
        public UserRoles()
        {
            db = new DBAccess();
        }
        //--------------------------------------------------------------------------
        ~UserRoles()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int UserRoleID { set; get; }
        public string UserRoleName { set; get; }
        public string UserRoleDescription { set; get; }

        //--------------------------------------------------------------------------
        public List<UserRoles> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<UserRoles> l_UserRoles = new List<UserRoles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    UserRoles m_UserRoles = new UserRoles();
                    m_UserRoles.UserRoleID = smartReader.GetInt32("UserRoleID");
                    m_UserRoles.UserRoleName = smartReader.GetString("UserRoleName");
                    m_UserRoles.UserRoleDescription = smartReader.GetString("UserRoleDescription");
                    l_UserRoles.Add(m_UserRoles);
                }
                smartReader.disposeReader(reader);
                return l_UserRoles;
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
                SqlCommand cmd = new SqlCommand("UserRolesInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserRoleName", this.UserRoleName));
                cmd.Parameters.Add(new SqlParameter("@UserRoleDescription", this.UserRoleDescription));
                cmd.Parameters.Add("@UserRoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.UserRoleID = (cmd.Parameters["@UserRoleID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@UserRoleID"].Value);
                return this.UserRoleID;
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
                SqlCommand cmd = new SqlCommand("UserRolesUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserRoleID", this.UserRoleID));
                cmd.Parameters.Add(new SqlParameter("@UserRoleName", this.UserRoleName));
                cmd.Parameters.Add(new SqlParameter("@UserRoleDescription", this.UserRoleDescription));
                db.ExecuteSQL(cmd);               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        
        //---------------------------------------------------------------------------------------------
        public UserRoles GetByUserRoleID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserRolesGetByUserRoleID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserRoleID", id));
                List<UserRoles> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as UserRoles;
                else return new UserRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //---------------------------------------------------------------------------------------------
        public List<UserRoles> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserRolesGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<UserRoles> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public void DeleteByID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UserRolesDelete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserRoleID", id));
                db.ExecuteSQL(cmd);  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}