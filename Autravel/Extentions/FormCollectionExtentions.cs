using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Extentions
{
    public static class FormCollectionExtentions
    {
        public static DateTime ToDate(this FormCollection input, string col)
        {
            var val = (input[col] + "").ToString().Trim();
            DateTime.TryParse(val, out DateTime Result);
            return Result;
        }
        public static string ToString(this FormCollection input, string col)
        {
            var result = (input[col] + "").ToString().Trim();
             return result;
        }    public static int ToInt(this FormCollection input, string col)
        {
            var val = (input[col] + "").ToString().Trim();
            int.TryParse(val, out int result);

            return result;
        }
        public static float ToFloat(this FormCollection input, string col)
        {
            var val = (input[col] + "").ToString().Trim();
            float.TryParse(val, out float result);

            return result;
        }   public static double ToDouble(this FormCollection input, string col)
        {
            var val = (input[col]+"").ToString().Trim();
            double.TryParse(val, out double result);

            return result;
        }
    }
}