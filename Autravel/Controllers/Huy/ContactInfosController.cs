using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class ContactInfosController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.title = "Hỗ trợ online";
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            var data = SqlModule.GetDataTable("SELECT  *  FROM ContactInfo");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }


        [HttpPost]
        public ActionResult onRowUpdating(ContactInfo item)
        {
            ContactInfo info = new ContactInfo().GetByContactInfoID(item.ContactInfo_ID);
            info.ContactInfo_Name = item.ContactInfo_Name;
            info.ContactInfo_Address = item.ContactInfo_Address;
            info.ContactInfo_Email = item.ContactInfo_Email;
            info.ContactInfo_Phone = item.ContactInfo_Phone;

            info.Update();
            return JsonMax(new { result = "Success" });
        }
        [HttpPost]
        public ActionResult onRowRemoving(ContactInfo item)
        {
            ContactInfo info = new ContactInfo().GetByContactInfoID(item.ContactInfo_ID);
            try
            {
                info.DeleteByID();
                return JsonMax(new { result = "Success" });
            }
            catch (Exception ex)
            {
                return JsonMax(new { result = "NG", data = ex.ToString() });
                ; throw ex;
            }
        }
        [HttpPost]
        public ActionResult onRowInserting(ContactInfo item)
        {
            int id = item.Insert();
            return JsonMax(new { result = "Success", data = id });
        }
    }
}