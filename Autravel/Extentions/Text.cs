using Autravel.Controllers.Huy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autravel.Extentions
{
    public class Text
    {
        public static string Show(string key)
        {
            var data = SqlModule.GetDataTable($" SELECT [Config_Value] FROM [AuTravel].[dbo].[ConfigInfomation] where Config_Title='{key}'").FirstOrDefault("Config_Value");
            return data;
        }
    }
}