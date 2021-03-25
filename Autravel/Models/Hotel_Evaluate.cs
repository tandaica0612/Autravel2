using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Tour_Evaluate
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Tour_Evaluate()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Tour_Evaluate()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Tour_Evaluate_ID { get; set; }
        public int Tour_ID { get; set; }
        public string CreateBy { get; set; }
        public DateTime Created { get; set; }
        public float Score { get; set; }
        public string Content { get; set; }
        //--------------------------------------------------------------------------
        public List<Tour_Evaluate> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Tour_Evaluate> l_ContactPersons = new List<Tour_Evaluate>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Tour_Evaluate m_ContactPersons = new Tour_Evaluate();
                    m_ContactPersons.Tour_Evaluate_ID = smartReader.GetInt32("Tour_Evaluate_ID");
                    m_ContactPersons.Tour_ID = smartReader.GetInt32("Tour_ID");
                    m_ContactPersons.CreateBy = smartReader.GetString("CreateBy");
                    m_ContactPersons.Created = smartReader.GetDateTime("Created");
                    m_ContactPersons.Score = smartReader.GetFloat("Score");
                    m_ContactPersons.Content = smartReader.GetString("Content");
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
                SqlCommand cmd = new SqlCommand("Tour_EvaluateInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tour_ID", this.Tour_ID));
                cmd.Parameters.Add(new SqlParameter("@CreateBy", this.CreateBy));
                cmd.Parameters.Add(new SqlParameter("@Created", this.Created));
                cmd.Parameters.Add(new SqlParameter("@Score", this.Score));
                cmd.Parameters.Add(new SqlParameter("@Content", this.Content));
                cmd.Parameters.Add("@Tour_Evaluate_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Tour_Evaluate_ID = (cmd.Parameters["@Tour_Evaluate_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Tour_Evaluate_ID"].Value);
                return this.Tour_Evaluate_ID;
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
                SqlCommand cmd = new SqlCommand("Tour_EvaluateUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tour_ID", this.Tour_ID));
                cmd.Parameters.Add(new SqlParameter("@Tour_Evaluate_ID", this.Tour_Evaluate_ID));
                cmd.Parameters.Add(new SqlParameter("@CreateBy", this.CreateBy));
                cmd.Parameters.Add(new SqlParameter("@Created", this.Created));
                cmd.Parameters.Add(new SqlParameter("@Score", this.Score));
                cmd.Parameters.Add(new SqlParameter("@Content", this.Content));
                db.ExecuteSQL(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<Tour_Evaluate> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Tour_EvaluateGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Tour_Evaluate> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tour_Evaluate GetByTour_EvaluateID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Tour_EvaluateGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tour_EvaluateID", id));
                List<Tour_Evaluate> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as Tour_Evaluate;
                else return new Tour_Evaluate();
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
                SqlCommand cmd = new SqlCommand("Tour_EvaluateDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tour_Evaluate_ID", this.Tour_Evaluate_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}