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
    public class TopicTour
    {

        private DBAccess db;
        //--------------------------------------------------------------------------
        public TopicTour()
        {
            db = new DBAccess();
        }
        //--------------------------------------------------------------------------
        ~TopicTour()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int TopicTour_ID { set; get; }
        public string TopicTour_Name { set; get; }
        public string TopicTour_Description { set; get; }

        //--------------------------------------------------------------------------
        public List<TopicTour> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<TopicTour> l_TopicTour = new List<TopicTour>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    TopicTour m_TopicTour = new TopicTour();
                    m_TopicTour.TopicTour_ID = smartReader.GetInt32("TopicTour_ID");
                    m_TopicTour.TopicTour_Name = smartReader.GetString("TopicTour_Name");
                    m_TopicTour.TopicTour_Description = smartReader.GetString("TopicTour_Description");
                    l_TopicTour.Add(m_TopicTour);
                }
                smartReader.disposeReader(reader);
                return l_TopicTour;
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
                SqlCommand cmd = new SqlCommand("TopicTourInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TopicTour_Name", this.TopicTour_Name));
                cmd.Parameters.Add(new SqlParameter("@TopicTour_Description", this.TopicTour_Description));
                cmd.Parameters.Add("@TopicTour_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.TopicTour_ID = (cmd.Parameters["@TopicTour_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@TopicTour_ID"].Value);
                return this.TopicTour_ID;
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
                SqlCommand cmd = new SqlCommand("TopicTourUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TopicTour_ID", this.TopicTour_ID));
                cmd.Parameters.Add(new SqlParameter("@TopicTour_Name", this.TopicTour_Name));
                cmd.Parameters.Add(new SqlParameter("@TopicTour_Description", this.TopicTour_Description));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------------------------------------------------------------------------------
        public TopicTour GetByTopicTourID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TopicTourGetByTopicTourID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TopicTour_ID", id));
                List<TopicTour> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as TopicTour;
                else return new TopicTour();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------------------------------------------------------------------------------
        public List<TopicTour> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("TopicTourGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<TopicTour> lAr = init(cmd);
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
                SqlCommand cmd = new SqlCommand("TopicTourDelete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TopicTour_ID", id));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}