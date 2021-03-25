using Autravel.Controllers.Huy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class BannerController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Banner";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM [Banner]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }
        [HttpPost]
        public ActionResult onRowRemoving(int Banner_ID)
        {
            SqlModule.ExcuteCommand("delete banner where Banner_ID="+ Banner_ID);
                return JsonMax(new { result = "OK" });
               
        }
        
        [HttpPost]
        public ActionResult Add(string TYPE,string img,int STT)
        {
            string sql = @"INSERT INTO [dbo].[BANNER]
           ([UrlFile]
           ,[STT]
           ,[TYPE])
     VALUES
           (N'#UrlFile#'
           ,#STT#
           ,'#TYPE#')";
            sql = sql.Replace("#UrlFile#",img);
            sql = sql.Replace("#STT#", STT.ToString());
            sql = sql.Replace("#TYPE#", TYPE);
            SqlModule.ExcuteCommand(sql);
                return JsonMax(new { result = "OK" });
               
        }
    }
}