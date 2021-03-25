using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autravel.Models;

namespace Autravel.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tour()
        {
            List<Tour> tours = new Tour().GetAll();
            return View(tours);
        }
        public ActionResult Hotel()
        {
            List<Hotel> hotel = new Hotel().GetAll();
            return View();    
        }
    }
}