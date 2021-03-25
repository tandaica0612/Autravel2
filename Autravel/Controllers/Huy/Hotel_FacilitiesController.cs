using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class Hotel_FacilitiesController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Tiện ích phòng/khách sạn";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM [Hotel_Facilities]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }


        [HttpPost]
        public ActionResult onRowUpdating(Hotel_Facilities item)
        {
            Hotel_Facilities Hotel_Facilities = new Hotel_Facilities().GetByHotel_FacilitiesID(item.Hotel_Facilities_ID);
            Hotel_Facilities.Name = item.Name;
            Hotel_Facilities.Note = item.Note;
 
            Hotel_Facilities.Update();
            return JsonMax(new { result = "Success" });
        }
        [HttpPost]
        public ActionResult onRowRemoving(Hotel_Facilities item)
        {
            Hotel_Facilities Hotel_Facilities = new Hotel_Facilities().GetByHotel_FacilitiesID(item.Hotel_Facilities_ID);
            try
            {
                Hotel_Facilities.DeleteByID();
                return JsonMax(new { result = "Success" });
            }
            catch (Exception ex)
            {
                return JsonMax(new { result = "NG", data = ex.ToString() });
                ; throw ex;
            }
        }
        [HttpPost]
        public ActionResult onRowInserting(Hotel_Facilities item)
        {

            Hotel_Facilities Hotel_Facilities = new Hotel_Facilities();
            Hotel_Facilities.Name = item.Name;
            Hotel_Facilities.Note = item.Note;
              int id = Hotel_Facilities.Insert();

            return JsonMax(new { result = "Success", data = id });
        }
    }
}