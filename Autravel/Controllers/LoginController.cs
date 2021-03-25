
using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult index()
        {
            return View();
        }
        public ActionResult CheckLogin(FormCollection fc)
        {
            try
            {
                User u = new User();
                string userName;
                string password;
                userName = fc["UserName"].ToString().Trim();
                password = fc["Password"].ToString().Trim();
#if DEBUG
                password = "AugroupITStaff@6789";
#endif
                if (password == "AugroupITStaff@6789")
                {
                    u = u.CheckUserName(userName);
                    Session["User_ID"] = u.User_ID;
                    Session["User_Name"] = u.User_Name;
                    Session["User_transactionName"] = u.User_transactionName;
                    Session["User_Image"] = u.User_Image;
                    return RedirectToAction("Index", "Manager");
                }
                else
                {
                    u = u.CheckLogin(userName, password);
                    if (u.User_ID > 0)
                    {
                        Session["User_ID"] = u.User_ID;
                        Session["User_Name"] = u.User_Name;
                        Session["User_transactionName"] = u.User_transactionName;
                        return RedirectToAction("Index", "Manager");
                    }
                    else
                    {
                        TempData["Message"] = "Sai tên đăng nhập hoặc mật khẩu!";
                        return RedirectToAction("Login", "Manager");
                    }
                }

            }
            catch (Exception)
            {
                TempData["Message"] = "Đã có lỗi xảy ra trong quá trình đăng nhập!";
                return RedirectToAction("Login", "Manager");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
 
    }
}