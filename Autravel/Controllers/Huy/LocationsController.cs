using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class LocationsController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Địa danh";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM [Location]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }


        [HttpPost]
        public ActionResult onRowUpdating(Location item)
        {
            Location location = new Location().GetByLocationID(item.Location_ID);
            location.Location_Name =  item.Location_Name;
            location.Location_Description = "";
            location.Location_images = "";

            location.Update();
            return JsonMax(new { result = "Success" });
        }
        [HttpPost]
        public ActionResult onRowRemoving(Location item)
        {
            Location location = new Location().GetByLocationID(item.Location_ID);
            try
            {
                location.DeleteByID();
                return JsonMax(new { result = "Success" });
            }
            catch (Exception ex)
            {
                return JsonMax(new { result = "NG",data=ex.ToString() });
             ; throw ex;
            }
        }
        [HttpPost]
        public ActionResult onRowInserting(Location item)
        {

            Location location = new Location();
            location.Location_Name = item.Location_Name;
            location.Location_Description = "";
            location.Location_images = "";
            int id = location.Insert();
            
            return JsonMax(new { result = "Success", data = id });
        }
    }
}