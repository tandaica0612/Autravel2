using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class Hotel_EvaluatesController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Thiết lập nội dung đánh giá";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM [Hotel_Evaluate]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }
        [HttpPost]
        public ActionResult getHotel()
        {
            var data = SqlModule.GetDataTable(" SELECT [Hotel_ID] ,[Hotel_Name]  FROM  [Hotel]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }


        [HttpPost]
        public ActionResult onRowUpdating(Hotel_Evaluate item)
        {
            Hotel_Evaluate row = new Hotel_Evaluate().GetByHotel_EvaluateID(item.Hotel_Evaluate_ID);
            row.Hotel_ID = item.Hotel_ID;
            row.CreateBy = item.CreateBy;
            row.Created = item.Created;
            row.Score = item.Score;
            row.Content = item.Content;
            
            row.Update();
            return JsonMax(new { result = "Success" });

        }
        [HttpPost]
        public ActionResult onRowRemoving(Hotel_Evaluate item)
        {
            Hotel_Evaluate location = new Hotel_Evaluate().GetByHotel_EvaluateID(item.Hotel_Evaluate_ID);
            try
            {
                location.DeleteByID();
                return JsonMax(new { result = "Success" });
            }
            catch (Exception ex)
            {
                return JsonMax(new { result = "NG", data = ex.ToString() });
                ; throw ex;
            }
        }
        [HttpPost]
        public ActionResult onRowInserting(Hotel_Evaluate item)
        {
            int id = item.Insert();
            return JsonMax(new { result = "Success", data = id });
        }
    }
}