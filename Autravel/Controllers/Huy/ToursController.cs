
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
    public class ToursController : BaseController
    {
        // GET: Tours
        public ActionResult Index()
        {
           ViewBag.Status = SqlModule.GetDataTable(" select Tour_Active ,	StatusTour,count(StatusTour)  as Qty from VTour GROUP BY Tour_Active,	StatusTour ");
            return View();
        }
        [HttpPost]
        public ActionResult GridData(int Tour_Active=2)
        {
            var data = SqlModule.GetDataTable($"SELECT  * FROM VTour where {Tour_Active} in (Tour_Active,2)");
            data.Columns.Add("Tour_StarRateHTML");
            foreach (DataRow item in data.Rows)
            {
                float.TryParse(item["Tour_StarRate"].ToString(), out float star);
                item["Tour_StarRateHTML"] = Funtions.FillStar(star);
            }
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        } 
        
        [HttpPost]
        public ActionResult HuyTour(int ID=0)
        {
            SqlModule.ExcuteCommand(" UPDATE[Tour] SET[Tour_Active] = 0 WHERE Tour_ID=" + ID);
            return JsonMax(new { result = "Success"  });
        } 
        [HttpPost]
        public ActionResult DangLaiTour(int ID=0)
        {
            SqlModule.ExcuteCommand(" UPDATE[Tour] SET[Tour_Active] = 1 WHERE Tour_ID=" + ID);
            return JsonMax(new { result = "Success"  });
        }
        [HttpPost]
        public ActionResult DelTour(int ID=0)
        {
            SqlModule.ExcuteCommand(" DELETE [Tour] WHERE Tour_ID=" + ID);
            return JsonMax(new { result = "Success"  });
        }
    }
}