using System.Web.Mvc;

namespace Autravel.Manager
{
    public class ManagerRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Travel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Travel_default",
                "Travel/{controller}/{action}/{id}",
                new { controller = "HomeManager", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}