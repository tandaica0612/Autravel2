using Autravel.Controllers.Huy.Models;
using Autravel.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Autravel.Controllers.Huy
{
    public class BookingHotelsController : BaseController
    {
        #region Danh sách
        public ActionResult Index()
        {
            var data = SqlModule.GetDataTable("SELECT [Config_id],[Config_Title] FROM [ConfigInfomation] WHERE [Config_Field]='BookingHotelStatus'");
            ViewBag.Status = SqlModule.GetDataTable(" SELECT Booking_StatusID,Booking_Status,count(Booking_Status) as Qty FROM  [VBookingHotel]  GROUP BY Booking_Status,Booking_StatusID");
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult GridGiaoDich(int Booking_StatusID = 0)
        {
            var data = SqlModule.GetDataTable($"SELECT  * FROM VBookingHotel where {Booking_StatusID} IN ( Booking_StatusID,0)");
            var json = ToJson(data);

            return JsonMax(new { result = "Success", data = json });
        }
        #endregion
        #region  Update
        public ActionResult UpdateGiaoDich(int ID)
        {
            var data = SqlModule.GetDataTable(" exec spBookingHotel_history " + ID);
            ViewBag.Config_id = data.Select("ID IS NULL and Config_id <> 1").ToList().Select(z => z["Config_id"]).FirstOrDefault();
            ViewBag.Booking_Surcharge = SqlModule.GetDataTable("SELECT  Booking_Surcharge FROM  [VBookingHotel] where BookingHotel_ID=" + ID).Rows[0][0].ToString();
            ViewBag.ID = ID;
            if (ViewBag.Config_id == null)
            {
                return Content("<H1>Giao dịch đã hoàn thành</H1>");
            }
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult UpdateGiaoDich(int ID, string NoiDung, string PhuPhi, int TinhTrang)
        {
            PhuPhi = (PhuPhi + "").Replace(",", "");
            var nd = LoginAuth.StaffInfo(Session);
            //1. Update tình trạng
            SqlModule.ExcuteCommand($"UPDATE BookingHotel SET Booking_Status={TinhTrang},Booking_Surcharge='{PhuPhi}' WHERE BookingHotel_ID=" + ID);
            //2. Lưu lại lịch sử
            SqlModule.ExcuteCommand($@"INSERT INTO [dbo].[BookingHotel_history]
           ([BookingHotel_ID]
           ,[Content]
           ,[Booking_Status]
           ,[CreatedBy]
           ,[Created])
           VALUES
           ({ID}
           ,N'{NoiDung}'
           ,{TinhTrang}
           ,{nd.User_ID}
           ,getdate())");
            return JsonMax("OK");
        }

        #endregion
        #region Email
        public ActionResult EmailGiaoDich(int ID)
        {
            var data = SqlModule.GetDataTable($"SELECT  * FROM VBookingHotel where BookingHotel_ID={ID}");
            if (data.Rows.Count == 0)
            {
                return Content("<H1>Không tìm thấy thông tin</H1>");

            }
            return PartialView(data.Rows[0]);

        }
        [HttpPost]
        public ActionResult SendEmailGiaoDich(int ID, string To)
        {
            var data = SqlModule.GetDataTable($"SELECT  * FROM VBookingHotel where BookingHotel_ID={ID}");
            if (data.Rows.Count == 0)
            {
                return Content("<H1>Không tìm thấy thông tin</H1>");

            }
            var item = data.Rows[0];

            string NewNoiDungMail = RenderPartialViewToString("~/Views/BookingHotels/EmailGiaoDich.cshtml", item);
            Task.Run(() =>
            {
                SendMailExtentions.SendEmail(new string[] { To }, $"Đơn hàng {item["BookingHotel_ID"]}:" + item["Hotel_Name"], NewNoiDungMail);
            });
            return JsonMax("OK");
        }
        #endregion

        public virtual string RenderPartialViewToString(string viewName, object viewmodel)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = viewmodel;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

    }
}