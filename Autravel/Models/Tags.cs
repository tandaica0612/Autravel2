using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Autravel.Controllers.Huy.Models;

namespace Autravel.Models
{
    public class Tags
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Tags()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
       public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Tag_ID { get; set; }
        public string Name { get; set; }
          //--------------------------------------------------------------------------
        public List<Tags> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Tags> l_ContactPersons = new List<Tags>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Tags m_ContactPersons = new Tags();
                    m_ContactPersons.Tag_ID = smartReader.GetInt32("Tag_ID");
                    m_ContactPersons.Name = smartReader.GetString("Name");
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
                SqlCommand cmd = new SqlCommand("TagsInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
                  cmd.Parameters.Add("@Tag_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Tag_ID = (cmd.Parameters["@Tag_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Tag_ID"].Value);
                return this.Tag_ID;
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
                SqlCommand cmd = new SqlCommand("TagsUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tag_ID", this.Tag_ID));
                cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
                  db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Tags> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TagsGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Tags> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tags GetByTagsID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TagsGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TagsID", id));
                List<Tags> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as Tags;
                else return new Tags();
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
            var data = SqlModule.GetDataTable("SELECT  [Tag_ID] ,[Name]  FROM [Tags]");
            foreach (DataRow item in data.Rows)
            {
                if (ListID.Contains(item["Tag_ID"].ToString()))
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
                SqlCommand cmd = new SqlCommand("TagsDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tag_ID", this.Tag_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}