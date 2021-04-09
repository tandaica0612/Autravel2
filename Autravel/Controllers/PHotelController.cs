using Autravel.Controllers.Huy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers
{
    public class PHotelController : Controller
    {
        // GET: PHotel
        public ActionResult Index()
        {
            ViewBag.BANNER = SqlModule.GetDataTable(" SELECT * FROM  [BANNER] where type ='HOTEL' order by ISNULL(stt,9999)");
            ViewBag.BanChay = SqlModule.GetDataTable(" SELECT top 4 Hotel_ID,Hotel_Name,Hotel_Price,HotelImage,Hotel_TimeZoneText,Hotel_StarRate FROM [VHotel] ");
            ViewBag.DiaDiem = SqlModule.GetDataTable(" SELECT top 8 [Location_ID],[Location_Name] ,[Location_images] FROM [Location]");
            ViewBag.CamNang = SqlModule.GetDataTable(" SELECT top 4 t1.[Post_id] ,t1.[Post_Tile] ,t1.[Post_Images] ,t1.[Post_CategoryID] ,t1.[Post_Slug] FROM [Post] t1");
            return View();
        }
        public ActionResult ListHotel(string KeySearch, string ThoiGian = "")
        {
            ViewBag.KeySearch = KeySearch;
            ViewBag.ThoiGian = ThoiGian;
            ViewBag.DiaDiem = SqlModule.GetDataTable(" SELECT  [Location_ID],[Location_Name] FROM [Location]");
            return View();
        }
        public ActionResult ListHotelGrid(string star = "", string price = "", string night = "", string KeySearch = "")
        {
            if (string.IsNullOrEmpty(star)) star = "*";
            if (string.IsNullOrEmpty(price)) price = "*";
            if (string.IsNullOrEmpty(night)) night = "*";
            if (string.IsNullOrEmpty(KeySearch)) KeySearch = "*";

            string sql = $@"SELECT TOP 10 * FROM VHotel where Hotel_ID in (select Hotel_ID FROM [VHotel_FILTER]  WHERE  
	(DayFILTER in (select value from string_split('#night#',','))  OR '#night#'='*')
AND	(PriceFILTER in (select value from string_split('#price#',','))  OR '#price#'='*')
AND	(StarFILTER in (select value from string_split('#star#',','))  OR '#star#'='*'))  
             AND CHARINDEX(dbo.fChuyenCoDauThanhKhongDau(N'*'),dbo.fChuyenCoDauThanhKhongDau(Hotel_Name+'*'))  !=0";
            sql = sql.Replace("#star#", star);
            sql = sql.Replace("#price#", price);
            sql = sql.Replace("#night#", night);
            sql = sql.Replace("#KeySearch#", KeySearch);

            var data = SqlModule.GetDataTable(sql);
            ViewBag.Hotel_Facilities = SqlModule.GetDataTable("  SELECT   *  FROM [Hotel_Facilities]");
            ViewBag.KeySearch = KeySearch == "*" ? "Danh sách Hotel" : KeySearch;
            return PartialView(data);
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutFinish()
        {
            return View();
        }
        public ActionResult Details(int ID)
        {
            var data = SqlModule.GetDataTable($@"SELECT  t1.* , t2.[Hotel_Content], t2.[Hotel_ListImage] 
                                        FROM VHotel t1
                                    left join[Hotel] t2  on t1.Hotel_ID = t2.Hotel_ID where t1.Hotel_ID = {ID}");
 
            if (data.Rows.Count == 0)
            {
                return Content("Không tìm thấy");
            }
            ViewBag.Room = SqlModule.GetDataTable("SELECT *  FROM [V_RoomHotel] where Hotel_ID=" + ID);
            ViewBag.RoomHotel_Extensions = SqlModule.GetDataTable("SELECT *  FROM  [RoomHotel_Extensions]");
            return View(data.Rows[0]);
        }

        public ActionResult LoadRoom(int ID)
        {
            
            ViewBag.RoomHotel_Extensions = SqlModule.GetDataTable("SELECT *  FROM  [RoomHotel_Extensions]");
            var Room = SqlModule.GetDataTable("SELECT *  FROM [V_RoomHotel] where Hotel_ID=" + ID);
              Room = SqlModule.GetDataTable("SELECT *  FROM [V_RoomHotel]");
             return PartialView(Room);
        }
        public ActionResult LienQuan(float Hotel_Score)
        {
            var data = SqlModule.GetDataTable(" SELECT top 4 Hotel_ID,Hotel_Name,Hotel_Price,HotelImage ,Hotel_Score,Hotel_StarRate FROM [VHotel] WHERE Hotel_Score=" + Hotel_Score);
             return PartialView(data);
        }
    }
}