using Autravel.Controllers.Huy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers
{
    public class CamNangController : Controller
    {
        // GET: CamNang
        public ActionResult Index()
        {
            ViewBag.CamNang = SqlModule.GetDataTable(" SELECT top 4 t1.[Post_id] ,t1.[Post_Tile] ,t1.[Post_Images] ,t1.[Post_CategoryID] ,t1.[Post_Slug],t1.Post_CreateDate,T1.Post_Location FROM [Autravel].[dbo].[Post] t1");
            ViewBag.DiaDiem = SqlModule.GetDataTable(" SELECT top 4 [Location_ID],[Location_Name] ,[Location_images] FROM [Autravel].[dbo].[Location]");
            return View();
        }  
        public ActionResult DiemDuLich(int ID=0)
        {
            ViewBag.CamNang = SqlModule.GetDataTable(" SELECT top 4 t1.[Post_id] ,t1.[Post_Tile] ,t1.[Post_Images] ,t1.[Post_CategoryID] ,t1.[Post_Slug],t1.Post_CreateDate FROM [Autravel].[dbo].[Post] t1");
            var DiaDiem = SqlModule.GetDataTable(" SELECT  * FROM [Autravel].[dbo].[Location] where Location_ID="+ID);
            return View(DiaDiem.Rows[0]);
        } 
        public ActionResult DiemDenHapDan(int ID=0)
        {
            ViewBag.CamNang = SqlModule.GetDataTable(" SELECT  t1.[Post_id] ,t1.[Post_Tile] ,t1.[Post_Images] ,t1.[Post_CategoryID] ,t1.[Post_Slug],t1.Post_CreateDate FROM [Autravel].[dbo].[Post] t1");
            var DiaDiem = SqlModule.GetDataTable(" SELECT  * FROM [Autravel].[dbo].[Location] where Location_ID="+ID);
            return View(DiaDiem.Rows[0]);
        } 
        public ActionResult BaiVietNoiBat(int ID=0)
        {
            ViewBag.CamNang = SqlModule.GetDataTable(" SELECT  t1.[Post_id] ,t1.[Post_Tile] ,t1.[Post_Images] ,t1.[Post_CategoryID] ,t1.[Post_Slug],t1.Post_CreateDate FROM [Autravel].[dbo].[Post] t1");
            var DiaDiem = SqlModule.GetDataTable(" SELECT  * FROM [Autravel].[dbo].[Location] where Location_ID="+ID);
            return View(DiaDiem.Rows[0]);
        }
        public ActionResult ChiTiet(int ID=0)
        {
            ViewBag.CamNang = SqlModule.GetDataTable(" SELECT  top 4 t1.[Post_id] ,t1.[Post_Tile] ,t1.[Post_Images] ,t1.[Post_CategoryID] ,t1.[Post_Slug],t1.Post_CreateDate FROM [Autravel].[dbo].[Post] t1");
            var DiaDiem = SqlModule.GetDataTable(" SELECT  * FROM [Autravel].[dbo].[Post] where Post_id=" + ID);
            return View(DiaDiem.Rows[0]);
        }
    }
}