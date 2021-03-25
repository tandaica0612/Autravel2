using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autravel.Controllers.Huy.Models;
using Autravel.Models;

namespace Autravel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

                      return RedirectToAction("index","Pcombo");
        }
       public ActionResult Support()
        {
            var data = SqlModule.GetDataTable("select * from [ContactInfo]");
            return PartialView(data);
        }

    }
}