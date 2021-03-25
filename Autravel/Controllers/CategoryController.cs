using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autravel.Models;
using Autravel.Models.Function;

namespace Autravel.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index( string ID ="")
        {
            List<Post> L_posts = new List<Post>();
            L_posts = new Post().GetAll();
            if (ID != "")
            {
                L_posts = new Post().GetAll().Where(x => x.Post_CategoryID == Funtions.ContainItemInListReturnItemID(ID, x.Post_CategoryID)).ToList();
            }
            return View(L_posts);
        }
    }
}