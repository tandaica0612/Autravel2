using Autravel.Controllers.Huy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class VouchersController : BaseController
    {
         public ActionResult Index()
        {
            ViewBag.Status = SqlModule.GetDataTable(" select Voucher_Active ,	StatusVoucher,count(StatusVoucher)  as Qty from VVoucher GROUP BY Voucher_Active,	StatusVoucher ");
            return View();
        }
        [HttpPost]
        public ActionResult GridData(int Voucher_Active = 2)
        {
            var data = SqlModule.GetDataTable($"SELECT  * FROM VVoucher where {Voucher_Active} in (Voucher_Active,2)");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }

        [HttpPost]
        public ActionResult HuyVoucher(int ID = 0)
        {
            SqlModule.ExcuteCommand(" UPDATE[Voucher] SET[Voucher_Active] = 0 WHERE Voucher_ID=" + ID);
            return JsonMax(new { result = "Success" });
        }
        [HttpPost]
        public ActionResult DangLaiVoucher(int ID = 0)
        {
            SqlModule.ExcuteCommand(" UPDATE[Voucher] SET[Voucher_Active] = 1 WHERE Voucher_ID=" + ID);
            return JsonMax(new { result = "Success" });
        }
        [HttpPost]
        public ActionResult DelVoucher(int ID = 0)
        {
            SqlModule.ExcuteCommand(" DELETE [Voucher] WHERE Voucher_ID=" + ID);
            return JsonMax(new { result = "Success" });
        }
    }
}