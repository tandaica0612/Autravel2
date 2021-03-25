using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class SetupMailSMTPsController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Thiết lập tài khoản gửi email";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM [SetupMailSMTP]");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }


        [HttpPost]
        public ActionResult onRowUpdating(SetupMailSMTP item)
        {
            SetupMailSMTP row = new SetupMailSMTP().GetByID(item.SetupMailSMTP_ID);
            row.SetupMailSMTP_Email = item.SetupMailSMTP_Email;
            row.SetupMailSMTP_Password = item.SetupMailSMTP_Password;
            row.SetupMailSMTP_Host = item.SetupMailSMTP_Host;
            row.SetupMailSMTP_Port = item.SetupMailSMTP_Port;
            row.SetupMailSMTP_SSL = item.SetupMailSMTP_SSL;
            row.SetupMailSMTP_Header = item.SetupMailSMTP_Header;
            row.SetupMailSMTP_Footer = item.SetupMailSMTP_Footer;
            row.Agents_ID = item.Agents_ID;


            row.Update();
            return JsonMax(new { result = "Success" });

        }
        [HttpPost]
        public ActionResult onRowRemoving(SetupMailSMTP item)
        {
            SetupMailSMTP location = new SetupMailSMTP().GetByID(item.SetupMailSMTP_ID);
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
        public ActionResult onRowInserting(SetupMailSMTP item)
        {
            int id = item.Insert();
            return JsonMax(new { result = "Success", data = id });
        }
    }
}