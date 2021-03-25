using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class PostCategory
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public PostCategory()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~PostCategory()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int PostCategory_ID { get; set; }
        public string PostCategory_Title { get; set; }
        //--------------------------------------------------------------------------
        public List<PostCategory> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PostCategory> l_PostCategory = new List<PostCategory>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PostCategory m_PostCategory = new PostCategory();
                    m_PostCategory.PostCategory_ID = smartReader.GetInt32("PostCategory_ID");
                    m_PostCategory.PostCategory_Title = smartReader.GetString("PostCategory_Title");
                    l_PostCategory.Add(m_PostCategory);
                }
                smartReader.disposeReader(reader);
                return l_PostCategory;
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
                SqlCommand cmd = new SqlCommand("PostCategoryInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostCategory_Title", this.PostCategory_Title));
                cmd.Parameters.Add("@PostCategory_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PostCategory_ID = (cmd.Parameters["@PostCategory_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@PostCategory_ID"].Value);
                return this.PostCategory_ID;
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
                SqlCommand cmd = new SqlCommand("PostCategoryUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostCategory_ID", this.PostCategory_ID));
                cmd.Parameters.Add(new SqlParameter("@PostCategory_Title", this.PostCategory_Title));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<PostCategory> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PostCategoryGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<PostCategory> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PostCategory GetByCategoryID(int PostCategory_ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PostCategoryGetByPostCategoryID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostCategory_ID", PostCategory_ID));
                List<PostCategory> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as PostCategory;
                else return new PostCategory();
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
                SqlCommand cmd = new SqlCommand("PostCategoryDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostCategoryID", this.PostCategory_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}