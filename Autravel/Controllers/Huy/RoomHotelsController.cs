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
    public class RoomHotelsController : BaseController
    {
        public ActionResult Index(int Hotel_ID = 0)
        {
            ViewBag.title = "Quản lý phòng khách sạn";
            ViewBag.Hotel_ID = Hotel_ID;
            return PartialView();
        }
        [HttpPost]
        public ActionResult GetData(int Hotel_ID = 0)
        {
            var data = SqlModule.GetDataTable("SELECT  * from [V_RoomHotel] WHERE Hotel_ID="+ Hotel_ID);
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }
        public ActionResult Create(int ID=0,int Hotel_ID=0)
        {
            var Hotel_Name = SqlModule.GetDataTable("SELECT Hotel_Name FROM HOTEL WHERE Hotel_ID="+Hotel_ID).FirstOrDefault("Hotel_Name");
            ViewBag.title = "Quản lý phòng khách sạn : "+ Hotel_Name;
            ViewBag.Hotel_Name = Hotel_Name;
            ViewBag.RoomType = SqlModule.GetDataTable("SELECT *  FROM [RoomType]");
            ViewBag.RoomHotel_Extensions = SqlModule.GetDataTable("SELECT [ID] ,[Name] ,[Type]  FROM [RoomHotel_Extensions]");
            RoomHotel item = new RoomHotel().GetRoomHotelByID(ID);
            if (item.Hotel_ID==0)
            {
                item.Hotel_ID =Hotel_ID;
            }
            return View(item);
        }
        [ValidateInput(false)]
        public ActionResult CreatePost(FormCollection fc, RoomHotel item,string _thumbnail_id="",string List_Images_Combo="")
        {
             item.RoomHotel_Image = _thumbnail_id;
            item.RoomHotel_ListImage = List_Images_Combo;
            item.RoomHotel_Extensions = (fc["tour_Topic"]+"").Trim();
            RoomHotel row = new RoomHotel().GetRoomHotelByID(item.RoomHotel_ID);
            if (row.RoomHotel_ID==0)
            {
                item.RoomHotel_ID = item.Insert();
            }      
            else
            {
                 row.Hotel_ID = item.Hotel_ID;
                row.RoomHotel_Type = item.RoomHotel_Type;
                row.RoomHotel_Content = item.RoomHotel_Content;
                row.RoomHotel_Extensions = item.RoomHotel_Extensions;
                row.RoomHotel_DateStart = item.RoomHotel_DateStart;
                row.RoomHotel_DateEndArray = item.RoomHotel_DateEndArray;
                row.RoomHotel_Image = item.RoomHotel_Image;
                row.RoomHotel_ListImage = item.RoomHotel_ListImage;
                row.RoomHotel_Title = item.RoomHotel_Title;
                row.RoomHotel_View = item.RoomHotel_View;
                row.RoomHotel_Bed = item.RoomHotel_Bed;
                row.RoomHotel_Adutls = item.RoomHotel_Adutls;
                row.RoomHotel_Infants = item.RoomHotel_Infants;
                row.RoomHotel_Acreage = item.RoomHotel_Acreage;
                row.Hotel_TimeZone = item.Hotel_TimeZone;
                row.Hotel_Price = item.Hotel_Price;
                row.Hotel_PriceSale = item.Hotel_PriceSale;
                row.Hotel_Qty = item.Hotel_Qty;
            

                row.Update();

            }

            TempData["MessagePost"] = "Cập nhật Combo thành công";
            return RedirectToAction("Create", "RoomHotels", new { ID = item.RoomHotel_ID, Hotel_ID=item.Hotel_ID });
        }

        [HttpPost]
        public ActionResult onRowUpdating(RoomHotel item)
        {
            RoomHotel row = new RoomHotel().GetRoomHotelByID(item.RoomHotel_ID);
            row.Hotel_ID                 =  item.Hotel_ID                  ;
            row.RoomHotel_Type           =  item.RoomHotel_Type            ;
            row.RoomHotel_Content        =  item.RoomHotel_Content         ;
            row.RoomHotel_Extensions     =  item.RoomHotel_Extensions      ;
            row.RoomHotel_DateStart      =  item.RoomHotel_DateStart       ;
            row.RoomHotel_DateEndArray   =  item.RoomHotel_DateEndArray    ;
            row.RoomHotel_Image          =  item.RoomHotel_Image           ;
            row.RoomHotel_Title          =  item.RoomHotel_Title           ;
            row.RoomHotel_View           =  item.RoomHotel_View            ;
            row.RoomHotel_Bed            =  item.RoomHotel_Bed             ;
            row.RoomHotel_Adutls         =  item.RoomHotel_Adutls          ;
            row.RoomHotel_Infants        =  item.RoomHotel_Infants         ;
            row.RoomHotel_Acreage        = item.RoomHotel_Acreage          ;

            row.Hotel_TimeZone              = item.Hotel_TimeZone                 ;
            row.Hotel_Price              = item.Hotel_Price                 ;
            row.Hotel_PriceSale              = item.Hotel_PriceSale                 ;
            row.Hotel_Qty              = item.Hotel_Qty                 ;
 
            row.Update();
            return JsonMax(new { result = "Success" });

        }
        [HttpPost]
        public ActionResult onRowRemoving(RoomHotel item)
        {
            RoomHotel location = new RoomHotel().GetRoomHotelByID(item.RoomHotel_ID);
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
        public ActionResult onRowInserting(RoomHotel item)
        {
            int id = item.Insert();
            return JsonMax(new { result = "Success", data = id });
        }
    }
}