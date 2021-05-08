using Autravel.Controllers.Huy.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers
{
    public class PComboController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.ChuDe = SqlModule.GetDataTable(" SELECT * FROM [TopicTour]");
            ViewBag.BANNER = SqlModule.GetDataTable(" SELECT * FROM  [BANNER] where type ='COMBO' order by ISNULL(stt,9999)");
            ViewBag.BanChay = SqlModule.GetDataTable(" SELECT top 10 Tour_ID,Tour_Name,Tour_Price,Tour_Image,Tour_TimeZoneText,Tour_StarRate FROM [VTour] WHERE Tour_Fixed =1 AND Tour_Active=1");
            ViewBag.DiaDiem = SqlModule.GetDataTable(" SELECT top 10 [Location_ID],[Location_Name] ,[Location_images] FROM [Location]");
            ViewBag.CamNang = SqlModule.GetDataTable(" SELECT top 10 t1.[Post_id] ,t1.[Post_Tile] ,t1.[Post_Images] ,t1.[Post_CategoryID] ,t1.[Post_Slug] FROM [Post] t1");
            return View();
        }
        public PartialViewResult ComboSale()
        {
            return PartialView();
        }
        public PartialViewResult ComboHot()
        {
            return PartialView();
        }
        public ActionResult ListCombo(string KeySearch, string From = "", string To = "")
        {
            ViewBag.KeySearch = KeySearch;
            ViewBag.From = From;
            ViewBag.To = To;
            return View();
        }

        public ActionResult ListComboGrid(string filter = "", string KeySearch = "", string From = "", string To = "")
        {
           
             if (string.IsNullOrEmpty(filter)) filter = "*";
            if (string.IsNullOrEmpty(KeySearch)) KeySearch = "*";
            

            string sql = $@"SELECT * FROM VTour where Tour_ID in (SELECT Tour_ID  FROM [VTour_FILTER] 
    CROSS APPLY STRING_SPLIT(filter, ',')   where value in (select value from string_split('#filter#',',')) )  
    AND CHARINDEX(dbo.fChuyenCoDauThanhKhongDau(N'#KeySearch#'),dbo.fChuyenCoDauThanhKhongDau(Tour_Name+'*'))  !=0";
            sql = sql.Replace("#filter#", filter);
            sql = sql.Replace("#KeySearch#", KeySearch);

            var data = SqlModule.GetDataTable(sql);
            ViewBag.Hotel_Facilities = SqlModule.GetDataTable("  SELECT   *  FROM [Hotel_Facilities]");
            ViewBag.KeySearch = KeySearch == "*" ? "Danh sách Combo" : KeySearch;
            return PartialView(data);
        }
        public ActionResult Checkout(int ID=0,int Room_ID=0, string CarType="", string AirType = "", int adult_number = 0, int child_number = 0, int BABY_number = 0,string From = null, string To =null)
        {
            var data = SqlModule.GetDataTable($@"SELECT  t1.*,t2.[Tour_Description]
                                                  , t2.[Tour_Content]
                                                  , t2.[Tour_Itinerary]
                                                  , t2.[Tour_Schedule]
                                                  , t2.[Tour_Rules]
                                                  , t2.[Tour_ListImage] FROM VTour t1 left join[Tour] t2 
                                                on t1.Tour_ID = t2.Tour_ID where t1.Tour_ID = {ID}");

              if (data.Rows.Count == 0)
            {
                return Content("Không tìm thấy");
            }
            ViewBag.Room = SqlModule.GetDataTable(" SELECT  *  FROM [Room] where Room_ID="+ Room_ID);
            ViewBag.AirType = SqlModule.GetDataTable($@"SELECT [Config_Title] as AirType,Config_Value as Price  FROM  [ConfigInfomation] where [Config_Field]='AirType' and Config_Title=N'{AirType}'");
             ViewBag.CarType = SqlModule.GetDataTable($@"SELECT [Config_Title] as CarType,Config_Value as Price  FROM  [ConfigInfomation] where [Config_Field]='CarType' and Config_Title=N'{CarType}'");
            ViewBag.adult_number = adult_number;
            ViewBag.child_number = child_number;
            ViewBag.BABY_number = BABY_number;
        
            var Start = DateTime.Parse(From);
            var End = DateTime.Parse(To);
              ViewBag.Night = (End - Start).TotalDays;
            ViewBag.From = Start.ToString("dd/MM/yyyy");
            ViewBag.To = End.ToString("dd/MM/yyyy");
             ViewBag.Booking_DepartureDate = Start.ToString("yyyy-MM-dd");
            ViewBag.Booking_ArrivalDate = End.ToString("yyyy-MM-dd");
            return View(data.Rows[0]);
         }
        [HttpPost]
        public ActionResult CheckoutFinish(Models.BookingTour item)
        {
            item.Insert();
            return View();
        }
        public ActionResult ComboSupport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetLocation()
        {
            var data = SqlModule.GetDataTable(" SELECT   [Location_Name] FROM [Location]");
            var json = new Controllers.Huy.BaseController().ToJson(data);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DanhGiaNoiBat(int TourID)
        {
            var data = SqlModule.GetDataTable("SELECT top 2  * FROM V_Tour_Evaluate ORDER BY Score desc");
            return PartialView(data);
        }
        public ActionResult DanhGia(int TourID)
        {
            var data = SqlModule.GetDataTable("SELECT  * FROM V_Tour_Evaluate ORDER BY Created desc");
            return PartialView(data);
        }
        public ActionResult HotelInfo(int Hotel_ID)
        {
            var data = SqlModule.GetDataTable(" SELECT    Hotel_Name,Hotel_Content ,Hotel_Facilities_ID,Tag_ID FROM Hotel WHERE Hotel_ID=" + Hotel_ID);
            return PartialView(data);
        }
        public ActionResult LienQuan(int Tour_Location)
        {
            var data = SqlModule.GetDataTable(" SELECT top 4 Tour_ID,Tour_Name,Tour_Price,Tour_Image,Tour_TimeZoneText,Tour_StarRate FROM [VTour] WHERE Tour_Fixed =1 AND Tour_Active=1 and Tour_Location=" + Tour_Location);

            return PartialView(data);
        }
        public ActionResult Details(int ID)
        {
            var data = SqlModule.GetDataTable($@"SELECT  t1.*,t2.[Tour_Description]
                                                  , t2.[Tour_Content]
                                                  , t2.[Tour_Itinerary]
                                                  , t2.[Tour_Schedule]
                                                  , t2.[Tour_Rules]
                                                  , t2.[Tour_ListImage] FROM VTour t1 left join[Tour] t2 
                                                on t1.Tour_ID = t2.Tour_ID where t1.Tour_ID = {ID}");

            ViewBag.Room = SqlModule.GetDataTable(" SELECT  *  FROM [Room]");
            ViewBag.AirType = SqlModule.GetDataTable(" SELECT [Config_Title] as AirType  FROM  [ConfigInfomation] where [Config_Field]='AirType'");
            ViewBag.CarType = SqlModule.GetDataTable(" SELECT [Config_Title] as CarType,Config_Value as Price  FROM  [ConfigInfomation] where [Config_Field]='CarType'");
            if (data.Rows.Count == 0)
            {
                return Content("Không tìm thấy");
            }
            return View(data.Rows[0]);
        }
    }
}