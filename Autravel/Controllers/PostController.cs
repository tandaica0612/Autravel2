using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autravel.Models;

namespace Autravel.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(int ID = 0)
        {
            Post m_posts = new Post();
            m_posts = new Post().GetByID(ID);
            if (ID == 0)
            {
                return RedirectToAction("Index", "Category");
            }
            return View(m_posts);
        }
    }
}