using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class RoomType
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public RoomType()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~RoomType()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int RoomType_ID { get; set; }
        public string RoomType_Name { get; set; }
    }
}