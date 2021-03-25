using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class Tour_EvaluatesController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Thiết lập nội dung đánh giá Combo";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM [Tour_Evaluate]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }
        [HttpPost]
        public ActionResult getTour()
        {
            var data = SqlModule.GetDataTable(" SELECT [Tour_ID] ,[Tour_Name]  FROM  [Tour]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }


        [HttpPost]
        public ActionResult onRowUpdating(Tour_Evaluate item)
        {
            Tour_Evaluate row = new Tour_Evaluate().GetByTour_EvaluateID(item.Tour_Evaluate_ID);
            row.Tour_ID = item.Tour_ID;
            row.CreateBy = item.CreateBy;
            row.Created = item.Created;
            row.Score = item.Score;
            row.Content = item.Content;

            row.Update();
            return JsonMax(new { result = "Success" });

        }
        [HttpPost]
        public ActionResult onRowRemoving(Tour_Evaluate item)
        {
            Tour_Evaluate location = new Tour_Evaluate().GetByTour_EvaluateID(item.Tour_Evaluate_ID);
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
        public ActionResult onRowInserting(Tour_Evaluate item)
        {
            int id = item.Insert();
            return JsonMax(new { result = "Success", data = id });
        }
    }
}