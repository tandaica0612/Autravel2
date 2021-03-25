using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class ConfigInfomationsController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Thiết lập Config Infomation";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM [ConfigInfomation]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }


        [HttpPost]
        public ActionResult onRowUpdating(ConfigInfomation item)
        {
            ConfigInfomation row = new ConfigInfomation().GetByID(item.Config_id);
             row.Config_Field = item.Config_Field;
            row.Config_Title = item.Config_Title;
            row.Config_Value = item.Config_Value;
           

            row.Update();
            return JsonMax(new { result = "Success" });

        }
        [HttpPost]
        public ActionResult onRowRemoving(ConfigInfomation item)
        {
            ConfigInfomation location = new ConfigInfomation().GetByID(item.Config_id);
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
        public ActionResult onRowInserting(ConfigInfomation item)
        {
            int id = item.Insert();
            return JsonMax(new { result = "Success", data = id });
        }
    }
}