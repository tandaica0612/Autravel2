using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class Agents
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public Agents()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~Agents()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Agents_ID { get; set; }
        public string Agents_Email { get; set; }
        public string Agents_Name { get; set; }
        public string Agents_Address { get; set; }
        public string Agents_Phone { get; set; }
        public Int64 Agents_Remain { get; set; }
        public bool Agents_AccumulatePoint { get; set; }
        public bool Agents_Active { get; set; }
        public int Agents_Type { get; set; }
        public int Agents_Status { get; set; }
        public int Agent_SaleCare { get; set; }
    }
}