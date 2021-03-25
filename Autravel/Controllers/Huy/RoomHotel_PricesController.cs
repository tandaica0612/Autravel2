using Autravel.Controllers.Huy.Models;
using Autravel.Extentions;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
  public class RoomHotel_PricesController : BaseController
    {
        public ActionResult Index(int ID = 0)
        {
            if (ID==0)
            {
                return Content("Vui lòng tạo phòng trước để thêm giá phòng");
            }
            ViewBag.title = "Quản lý giá phòng khách sạn";
            ViewBag.ID = ID;
            return PartialView();
        }
        [HttpPost]
        public ActionResult GetData(int ID = 0)
        {
            var data = SqlModule.GetDataTable("SELECT  * from [V_RoomHotel_Price] WHERE RoomHotel_ID="+ID);
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }
       

        [HttpPost]
        public ActionResult onRowUpdating(RoomHotel_Price item)
        {
            RoomHotel_Price row = new RoomHotel_Price().GetRoomHotel_PriceByID(item.ID);
            row.ID = item.ID;
            row.Price1Night = item.Price1Night;
            row.Price2Night = item.Price2Night;
            row.Price3Night = item.Price3Night;
            row.Price4Night = item.Price4Night;
            row.PriceMoreNight = item.PriceMoreNight;
            row.FromDate = item.FromDate;
            row.ToDate = item.ToDate;
            


            row.Update();
            return JsonMax(new { result = "Success" });

        }
        [HttpPost]
        public ActionResult onRowRemoving(RoomHotel_Price item)
        {
            RoomHotel_Price location = new RoomHotel_Price().GetRoomHotel_PriceByID(item.ID);
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
        public ActionResult onRowInserting(RoomHotel_Price item)
        {
            int id = item.Insert();
            return JsonMax(new { result = "Success", data = id });
        }
    }
}