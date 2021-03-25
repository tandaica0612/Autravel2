using Autravel.Controllers.Huy.Models;
using Autravel.Models.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class HotelsController : BaseController
    {
        // GET:  Hotels
        public ActionResult Index()
        {
             return View();
        }
        [HttpPost]
        public ActionResult GridData()
        {
            var data = SqlModule.GetDataTable($"SELECT  * FROM VHotel");
            data.Columns.Add("Hotel_StarRateHTML");
            foreach (DataRow item in data.Rows)
            {
         float.TryParse(item["Hotel_StarRate"].ToString(),out  float star);
                item["Hotel_StarRateHTML"] = Funtions.FillStar(star);
            }
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }
 
        [HttpPost]
        public ActionResult DelHotel(int ID = 0)
        {
            SqlModule.ExcuteCommand(" DELETE [Hotel] WHERE Hotel_ID=" + ID);
            return JsonMax(new { result = "Success" });
        }
    }
}