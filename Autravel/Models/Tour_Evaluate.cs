using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Hotel_Evaluate
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Hotel_Evaluate()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Hotel_Evaluate()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Hotel_Evaluate_ID { get; set; }
        public int Hotel_ID { get; set; }
        public string CreateBy { get; set; }
        public DateTime Created { get; set; }
        public float Score { get; set; }
        public string Content { get; set; }
        //--------------------------------------------------------------------------
        public List<Hotel_Evaluate> init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Hotel_Evaluate> l_ContactPersons = new List<Hotel_Evaluate>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Hotel_Evaluate m_ContactPersons = new Hotel_Evaluate();
                    m_ContactPersons.Hotel_Evaluate_ID = smartReader.GetInt32("Hotel_Evaluate_ID");
                    m_ContactPersons.Hotel_ID = smartReader.GetInt32("Hotel_ID");
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
                SqlCommand cmd = new SqlCommand("Hotel_EvaluateInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", this.Hotel_ID));
                cmd.Parameters.Add(new SqlParameter("@CreateBy", this.CreateBy));
                cmd.Parameters.Add(new SqlParameter("@Created", this.Created));
                cmd.Parameters.Add(new SqlParameter("@Score", this.Score));
                cmd.Parameters.Add(new SqlParameter("@Content", this.Content));
                cmd.Parameters.Add("@Hotel_Evaluate_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Hotel_Evaluate_ID = (cmd.Parameters["@Hotel_Evaluate_ID"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@Hotel_Evaluate_ID"].Value);
                return this.Hotel_Evaluate_ID;
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
                SqlCommand cmd = new SqlCommand("Hotel_EvaluateUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_ID", this.Hotel_ID));
                cmd.Parameters.Add(new SqlParameter("@Hotel_Evaluate_ID", this.Hotel_Evaluate_ID));
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
        public List<Hotel_Evaluate> GetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotel_EvaluateGetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                List<Hotel_Evaluate> lAr = init(cmd);
                return lAr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Hotel_Evaluate GetByHotel_EvaluateID(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotel_EvaluateGetByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_EvaluateID", id));
                List<Hotel_Evaluate> lAr = init(cmd);
                if (lAr.Count == 1) return lAr[0] as Hotel_Evaluate;
                else return new Hotel_Evaluate();
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
                SqlCommand cmd = new SqlCommand("Hotel_EvaluateDeleteByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Hotel_Evaluate_ID", this.Hotel_Evaluate_ID));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}