using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Autravel.Models
{
    public class ConfigInfomation
    {
        private DBAccess db;
        //--------------------------------------------------------------------------
        public ConfigInfomation()
        {
            db = new DBAccess(Constants.CONNECTION_STRING);
        }
        //--------------------------------------------------------------------------
        ~ConfigInfomation()
        {

        }
        //--------------------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //--------------------------------------------------------------------------
        public int Config_id { get; set; }
        public string Config_Field { get; set; }
        public string Config_Title { get; set; }
        public string Config_Value { get; set; }
    }
}