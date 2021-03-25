using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Post
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Post()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Post()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Post_id { get; set; }
        public string Post_Title { get; set; }           
        public int Post_Type { get; set; }
        public string Post_Description { get; set; }
        public string Post_Content { get; set; }
        public DateTime Post_CreateDate { get; set; }
        public DateTime Post_UpdateDate { get; set; }
        public string Post_Images { get; set; }
        public string Post_CategoryID { get; set; }
        public string Post_Slug { get; set; }
        public string Post_tag { get; set; }
        public int Post_UserID { get; set; }
        public int Post_Location { get; set; }
        //--------------------------------------------------------------------------
        public List<Post> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Post> l_Post = new List<Post>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Post m_Post = new Post();
                    m_Post.Post_id = smartReader.GetInt32("Post_id");
                    m_Post.Post_Title = smartReader.GetString("Post_Tile");
                    m_Post.Post_Type = smartReader.GetInt32("Post_Type");
                    m_Post.Post_Description = smartReader.GetString("Post_Description");
                    m_Post.Post_Content = smartReader.GetString("Post_Content");
                    m_Post.Post_Images = smartReader.GetString("Post_Images");
                    m_Post.Post_CategoryID = smartReader.GetString("Post_CategoryID");
                    m_Post.Post_Slug = smartReader.GetString("Post_Slug");
                    m_Post.Post_tag = smartReader.GetString("Post_tag");
                    m_Post.Post_UserID = smartReader.GetInt32("Post_UserID");
                    m_Post.Post_Location = smartReader.GetInt32("Post_Location");
                    m_Post.Post_CreateDate = smartReader.GetDateTime("Post_CreateDate");
                    l_Post.Add(m_Post);
                }
                smartReader.disposeReader(reader);
                return l_Post;
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
                SqlCommand cmd = new SqlCommand("PostInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Post_Tile", this.Post_Title));
                cmd.Parameters.Add(new SqlParameter("@Post_Type", this.Post_Type));
                cmd.Parameters.Add(new SqlParameter("@Post_Description", this.Post_Description));
                cmd.Parameters.Add(new SqlParameter("@Post_Content", this.Post_Content));
                cmd.Parameters.Add(new SqlParameter("@Post_CreateDate", this.Post_CreateDate));
                cmd.Parameters.Add(new SqlParameter("@Post_UpdateDate", this.Post_UpdateDate));
                cmd.Parameters.Add(new SqlParameter("@Post_Images", this.Post_Images));
                cmd.Parameters.Add(new SqlParameter("@Post_CategoryID", this.Post_CategoryID));
                cmd.Parameters.Add(new SqlParameter("@Post_Slug", this.Post_Slug));
                cmd.Parameters.Add(new SqlParameter("@Post_tag", this.Post_tag));
                cmd.Parameters.Add(new SqlParameter("@Post_UserID", this.Post_UserID));
                cmd.Parameters.Add(new SqlParameter("@Post_Location", this.Post_Location));
                cmd.Parameters.Add("@Post_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Post_id = (cmd.Parameters["@Post_id"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Post_id"].Value);
                return this.Post_id;
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
                SqlCommand cmd = new SqlCommand("PostUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Post_id", this.Post_id));
                cmd.Parameters.Add(new SqlParameter("@Post_Tile", this.Post_Title));
                cmd.Parameters.Add(new SqlParameter("@Post_Type", this.Post_Type));
                cmd.Parameters.Add(new SqlParameter("@Post_Description", this.Post_Description));
                cmd.Parameters.Add(new SqlParameter("@Post_Content", this.Post_Content));
                cmd.Parameters.Add(new SqlParameter("@Post_Images", this.Post_Images));
                cmd.Parameters.Add(new SqlParameter("@Post_CategoryID", this.Post_CategoryID));
                cmd.Parameters.Add(new SqlParameter("@Post_Slug", this.Post_Slug));
                cmd.Parameters.Add(new SqlParameter("@Post_tag", this.Post_tag));
                cmd.Parameters.Add(new SqlParameter("@Post_UserID", this.Post_UserID));
                cmd.Parameters.Add(new SqlParameter("@Post_Location", this.Post_Location));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Post> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PostGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Post> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Post GetByID(int PostID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PostGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostID", PostID));
                List<Post> lAr = init(cmd);
                if (lAr.Count >= 1) return lAr[0] as Post;
                else return new Post();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}