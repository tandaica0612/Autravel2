using Autravel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Autravel.Controllers.Huy.Models
{
    public class LoginAuth : AuthorizeAttribute
    {

        public static string NameSession = "User_ID";
        public static string LastUrl = "LastUrl";
        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            Contract.Assert(filterContext != null);
            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
        }
        public static User GetCookiesLogin()
        {
            var nd = HttpContext.Current.Request.Cookies[LoginAuth.NameSession];
            int UserID = 0;

            if (!string.IsNullOrEmpty(nd["_Key"]))
            {
                UserID = int.Parse(nd["_Key"]);
            }

            User user = new User();

            user = user.GetByUserID(UserID);
            return user;

        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (HttpContext.Current.Session[LoginAuth.NameSession] == null)
            {

                if (HttpContext.Current.Request.Cookies[LoginAuth.NameSession] != null)
                {
                    var StaffInfomation = GetCookiesLogin();
                    if (StaffInfomation != null)
                    {
                        HttpContext.Current.Session[LoginAuth.NameSession] = StaffInfomation;
                        return;
                    }
                }

                if (SkipAuthorization(filterContext)) return;
                var lastUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
                HttpContext.Current.Session["lastUrl"] = lastUrl;
                filterContext.Result = new RedirectResult("/Login/index");

            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result =
           new RedirectToRouteResult(
               new RouteValueDictionary{
                   { "area", "Default" },
                   { "controller", "Login" },
                   { "action", "index" }

            });

        }
        public static User StaffInfo(HttpSessionStateBase Session)
        {
            var nd = new User();
            if (Session == null)
            {
                return new User();
            }
            var User_ID = (int)Session[LoginAuth.NameSession];
            if (User_ID == 0)
            {
                nd = GetCookiesLogin();
            }
            else
            {
                nd.User_ID= (int)Session[LoginAuth.NameSession];
                nd.User_Name = (string)Session["User_Name"];
                nd.User_transactionName = (string)Session["User_transactionName"];
            }

             return nd;
        }
        public static User StaffInfo(HttpSessionState Session)
        {
            var nd = new User();
            if (Session == null)
            {
                return new User();
            }
            var User_ID = (int)Session[LoginAuth.NameSession];
            if (User_ID == 0)
            {
                nd = GetCookiesLogin();
            }
            else
            {
                nd.User_ID = (int)Session[LoginAuth.NameSession];
                nd.User_Name = (string)Session["User_Name"];
                nd.User_transactionName = (string)Session["User_transactionName"];
            }

            return nd;
        }
    }
}
