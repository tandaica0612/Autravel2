using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Autravel.Controllers.Huy;
using Autravel.Controllers.Huy.Models;
using Autravel.Models;
using Autravel.Models.Function;

namespace Autravel.Controllers
{
    public class ManagerController : BaseController
    {
        // GET: Manager
        public ActionResult Index()
        {
             return View();  
        }
        #region Account
        public ActionResult ManagerAccount()
        {
           GetDataAccount();
            return View(); 
        }
        public PartialViewResult GetDataAccount(string username = "", string NameTransac = "")
        {
            
                User user = new User();
                List<User> L_user = new User().GetAll();
                return PartialView("GetDataAccount", L_user);
       
         }
        public PartialViewResult LoadAccount(int AccountID = 0)
        {
            
                User user = new User().GetByUserID(AccountID);
                if (user == null) { user = new User(); }
                return PartialView("LoadAccount", user);
            
        }
        public void UpdateAccount(FormCollection fc)
        {
             User user = new User();
                if (Convert.ToInt32(fc["User_ID"].Trim()) != 0)
                {
                    user = user.GetByUserID(Convert.ToInt32(fc["User_ID"].Trim()));
                }
                user.Birthday = DateTime.Now;
                user.User_Name = fc["User_Name"].Trim();
                user.User_Pass = fc["User_Pass"].Trim();
                user.User_transactionName = fc["User_transactionName"].Trim();
                user.User_Image = fc["User_Image"].Trim();
                user.User_Address = fc["User_Address"].Trim();
                user.User_Email = fc["User_Email"].Trim();
                user.User_Phone = fc["User_Phone"].Trim();
                user.User_Active = true;// fc["UserName"].Trim();
                user.User_Role = 1;// fc["UserName"].Trim();
                user.User_InternalStaff = true;// fc["UserName"].Trim();
                user.Agents_ID = 1;// fc["UserName"].Trim();
                user.Update();
       

        }
        public ActionResult EditAccount()
        {
            
                try
                {
                    var id = Url.RequestContext.RouteData.Values["id"];
                    if (Convert.ToInt32(id) == Convert.ToInt32(Session["UserID"]))
                    {
                        User u = new User();
                        u = u.GetByUserID(Convert.ToInt32(id));
                        if (u.User_ID > 0)
                        {
                            return View(u);
                        }
                        else
                        {
                            return RedirectToAction("ListUsers");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Bạn không có quyền cập nhật thông tin người dùng";
                        return RedirectToAction("Index", "Users");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
        }
        #endregion
        public ActionResult Booking()
        {
            return View();
        }
        #region Post
        public ActionResult Post()
        {
             
                GetDataAllPost();
                return View();
             
        }
        public PartialViewResult GetDataAllPost(int Category = 0, string TitleLike = "", int UserID = 0)
        {
             
                try
                {
                    List<Autravel.Models.Post> l_Post = new Post().GetAll();
                    return PartialView("GetDataAllPost", l_Post);
                }
                catch (Exception ex)
                {
                    return PartialView("GetDataAllPost");
                }
           

        }
        public ActionResult AddPost()
        {
             
                Models.Post post = new Post();
                return View(post);
          
        }
        public ActionResult EditPost(int id = 0)
        {
            
                if (id != 0)
                {
                    Models.Post post = new Post().GetByID(id);
                    return View(post);
                }
                return View();
          
        }
        [ValidateInput(false)]
        public ActionResult UpdatePost(FormCollection fc)
        {
               if (fc["Post_id"].Trim() != null && fc["Post_id"].Trim() != "0")
                {
                    Models.Post postOld = new Post().GetByID(Convert.ToInt32(fc["Post_id"].Trim()));
                    postOld.Post_Title = fc["Post_Tile"].Trim();
                    postOld.Post_Type = Convert.ToInt32(fc["Post_Type"].Trim());
                    //string Description = "";
                    //if (Funtions.StripHTML(fc["editor"].Trim()).Length > 200)
                    //{
                    //    Description = Funtions.StripHTML(fc["editor"].Trim()).Substring(0, 200);
                    //}
                    //else { Description = Funtions.StripHTML(fc["editor"].Trim()); }
                    postOld.Post_Description = fc["editor_Description"].Trim(); ;
                    postOld.Post_Content = fc["editor"].Trim();
                    postOld.Post_UpdateDate = DateTime.Now;
                    postOld.Post_Images = fc["_thumbnail_id"].Trim();
                    postOld.Post_CategoryID = (fc["post_category"] + "").Trim();
                    postOld.Post_Location = Convert.ToInt32(fc["tour_Location"].Trim());
                    postOld.Post_Slug = Funtions.ConvertTitleToURL(fc["Post_Tile"].Trim());
                    postOld.Post_tag = fc["Post_tag"].Trim();
                    postOld.Post_UserID = Convert.ToInt32(Session["User_ID"]);
                    postOld.Update();
                    TempData["MessagePost"] = "Cập nhật bài viết thành công";
                    return RedirectToAction("EditPost", "Manager", new { id = postOld.Post_id });
                }
                else
                {
                    Models.Post postNew = new Post();
                    postNew.Post_Title = fc["Post_Tile"].Trim();
                    postNew.Post_Type = Convert.ToInt32(fc["Post_Type"].Trim());
                    string Description = "";
                    if (Funtions.StripHTML(fc["editor"].Trim()).Length > 200)
                    {
                        Description = Funtions.StripHTML(fc["editor"].Trim()).Substring(0, 200);
                    }
                    else { Description = Funtions.StripHTML(fc["editor"].Trim()); }
                    postNew.Post_Description = Description;
                    postNew.Post_Content = fc["editor"].Trim();
                    postNew.Post_CreateDate = DateTime.Now;
                    postNew.Post_UpdateDate = DateTime.Now;
                    postNew.Post_Images = fc["_thumbnail_id"].Trim();
                    postNew.Post_CategoryID = (fc["post_category"] + "").Trim();
                    postNew.Post_Slug = Funtions.ConvertTitleToURL(fc["Post_Tile"].Trim());
                    postNew.Post_tag = fc["Post_tag"].Trim();
                    postNew.Post_UserID = Convert.ToInt32(Session["User_ID"]);
                    int idnew = postNew.Insert();
                    if (idnew > 0)
                    {
                        TempData["MessagePost"] = "Đăng bài viết thành công";
                        return RedirectToAction("EditPost", "Manager", new { id = idnew });
                    }
                    else
                    {
                        TempData["MessagePost"] = "Có lỗi xảy ra, vui lòng thử lại";
                        return RedirectToAction("AddPost", "Manager", new { id = 0 });
                    }
                }
           

        }
         
        #endregion
        #region Category
        public ActionResult Category()
        {
            GetDataCategory();
            return View();
        }
        public PartialViewResult GetDataCategory()
        {
            List<Models.PostCategory> categories = new PostCategory().GetAll();
            return PartialView("GetDataLocation", categories);
        }
        public ActionResult EditCategory(FormCollection fc)
        {
            try
            {
                if (fc["CategoryID"].Trim() == "0" || fc["CategoryID"].Trim() == "")
                {
                    PostCategory Category = new PostCategory();
                    Category.PostCategory_Title = fc["CategoryName"].Trim();
                    int id = Category.Insert();
                    TempData["MessagePost"] = "Thêm mới thành công";
                    return RedirectToAction("Category", "Manager");
                }
                else
                {
                    PostCategory category = new PostCategory().GetByCategoryID(Convert.ToInt32(fc["CategoryID"].Trim()));
                    category.PostCategory_Title = fc["CategoryName"].Trim();
                    category.Update();
                    TempData["MessagePost"] = "Cập nhật thành công";
                    return RedirectToAction("Category", "Manager");
                }
            }
            catch { TempData["MessagePost"] = "Có lỗi phát sinh trong quá trình xử lý, vui lòng liên hệ kỹ thuật"; return RedirectToAction("Category", "Manager"); }
        }
        public int AddCategory(string NameCategory)
        {
            
                try
                {
                    PostCategory postCategory_new = new PostCategory();
                    postCategory_new.PostCategory_Title = NameCategory;
                    int id = postCategory_new.Insert();
                    if (id > 0) { return id; }
                    return 0;
                }
                catch (Exception ex) { throw ex; }
        
        }
        public ActionResult DeleteCategory(int id = 0)
        {
            
                PostCategory category = new PostCategory().GetByCategoryID(id);
                try
                {
                    category.DeleteByID();
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                    throw ex;
                }
           
        }
        #endregion
        #region Tuor
        public ActionResult Tour()
        {
           
                GetDataAllTour();
                return View();
            
        }
        public PartialViewResult GetDataAllTour(string Tourname = "", string location = "")
        {
            
                List<Tour> L_AllTour = new Tour().GetAll();
                return PartialView("GetDataAllTour", L_AllTour);
           

        }
        public ActionResult AddTour()
        {
            Models.Tour tuor = new Tour();
            return View(tuor);
        }
        public ActionResult EditTour(int id = 0)
        {
             
                if (id != 0)
                {
                    Models.Tour tuor = new Tour().GetByTourID(id);
                    return View(tuor);
                }
                return RedirectToAction("Index", "Manager");
           
        }
        [ValidateInput(false)]
        public ActionResult UpdateTour(FormCollection fc)
        {
          
                if (fc["Post_id"].Trim() != null && fc["Post_id"].Trim() != "0")
                {
                    Models.Tour TourOld = new Tour().GetByTourID(Convert.ToInt32(fc["Post_id"].Trim()));
                    TourOld.Tour_Name = fc["Post_Tile"].Trim();
                    TourOld.Tour_Content = fc["editor"].Trim();
                    TourOld.Tour_OrganizationalUnit = fc["Tour_OrganizationalUnit"].Trim();
                    string Description = "";
                    if (Funtions.StripHTML(fc["editor"].Trim()).Length > 200)
                    {
                        Description = Funtions.StripHTML(fc["editor"].Trim());
                    }
                    else { Description = Funtions.StripHTML(fc["editor"].Trim()); }
                    TourOld.Tour_Description = Description;
                    TourOld.Tour_Topic = fc["tour_Topic"].Trim();
                    TourOld.Tour_Location = Convert.ToInt32(fc["tour_Location"].Trim());
                    if (Convert.ToBoolean(Convert.ToInt32(fc["Tour_Fixed"].Trim())) == false)
                    {
                        TourOld.Tour_Price = Convert.ToInt64(fc["Tour_Price"].Trim().Replace(",", ""));
                    }
                    TourOld.Tour_DepartureDate = Funtions.ddMMyyyy(fc["Tour_DepartureDate"].Trim());
                    TourOld.Tour_TimeZone = Convert.ToInt32(fc["Tour_TimeZone"].Trim());
                    TourOld.Tour_Itinerary = fc["editor_Tour_Itinerary"].Trim();
                    TourOld.Tour_Schedule = fc["editor_Tour_Schedule"].Trim();
                    TourOld.Tour_Rules = fc["editor_Tour_Rules"].Trim();
                    TourOld.Tour_ListImage = fc["List_Images_Combo"].Trim();
                    TourOld.Tour_Image = fc["_thumbnail_id"].Trim();
                    TourOld.Product_ID = 1;
                    TourOld.UserCreate = Convert.ToInt32(Session["User_ID"]);
                    TourOld.Tour_Fixed = Convert.ToBoolean(Convert.ToInt32(fc["Tour_Fixed"].Trim()));
                    TourOld.Tour_StarRate =  Convert.ToDouble(fc["Star_Rating"].Trim());
                    TourOld.Update();
                    if (Convert.ToBoolean(Convert.ToInt32(fc["Tour_Fixed"].Trim())) == true)
                    {
                        TourDetailPrice tourDetailPrice = new TourDetailPrice().GetByTourID(TourOld.Tour_ID);
                        tourDetailPrice.Tour_ID = TourOld.Tour_ID;
                        tourDetailPrice.TourDetailPrice_DepartureDate = Funtions.ddMMyyyy(fc["Tour_DepartureDate"].Trim());
                        tourDetailPrice.TourDetailPrice_ArrivalDate = Funtions.ddMMyyyy(fc["Tour_DepartureDate"].Trim()).AddDays(Convert.ToInt32(fc["Tour_TimeZone"].Trim()));
                        tourDetailPrice.TourDetailPrice_NumberPrice1 = Convert.ToInt32(fc["TourDetailPrice_NumberPrice1"].Trim());
                        tourDetailPrice.TourDetailPrice_Price1 = Convert.ToInt64(fc["TourDetailPrice_Price1"].Trim().Replace(",", ""));
                        tourDetailPrice.TourDetailPrice_NumberPrice2 = Convert.ToInt32(fc["TourDetailPrice_NumberPrice2"].Trim());
                        tourDetailPrice.TourDetailPrice_Price2 = Convert.ToInt64(fc["TourDetailPrice_Price2"].Trim().Replace(",", ""));
                        tourDetailPrice.TourDetailPrice_NumberPrice3 = Convert.ToInt32(fc["TourDetailPrice_NumberPrice3"].Trim());
                        tourDetailPrice.TourDetailPrice_Price3 = Convert.ToInt64(fc["TourDetailPrice_Price3"].Trim().Replace(",", ""));
                        if (tourDetailPrice.TourDetailPrice_ID > 0) { tourDetailPrice.Update(); }
                        else { tourDetailPrice.Insert(); }
                    }
                    TempData["MessagePost"] = "Cập nhật Combo thành công";
                    return RedirectToAction("EditTour", "Manager", new { id = TourOld.Tour_ID });
                }
                else
                {
                    Models.Tour TourOld = new Tour();
                    TourOld.Tour_Name = fc["Post_Tile"].Trim();
                    TourOld.Tour_Content = fc["editor"].Trim();
                    TourOld.Tour_OrganizationalUnit = fc["Tour_OrganizationalUnit"].Trim();
                    string Description = "";
                    if (Funtions.StripHTML(fc["editor"].Trim()).Length > 200)
                    {
                        Description = Funtions.StripHTML(fc["editor"].Trim());
                    }
                    else { Description = Funtions.StripHTML(fc["editor"].Trim()); }
                    TourOld.Tour_Description = Description;
                    TourOld.Tour_Topic = fc["tour_Topic"].Trim();
                    TourOld.Tour_Location = Convert.ToInt32(fc["tour_Location"].Trim());
                    if (Convert.ToBoolean(Convert.ToInt32(fc["Tour_Fixed"].Trim())) == false)
                    {
                        TourOld.Tour_Price = Convert.ToInt64(fc["Tour_Price"].Trim().Replace(",", ""));
                    }
                    TourOld.Tour_DepartureDate = Funtions.ddMMyyyy(fc["Tour_DepartureDate"].Trim());
                    TourOld.Tour_TimeZone = Convert.ToInt32(fc["Tour_TimeZone"].Trim());
                    TourOld.Tour_Itinerary = fc["editor_Tour_Itinerary"].Trim();
                    TourOld.Tour_Schedule = fc["editor_Tour_Schedule"].Trim();
                    TourOld.Tour_Rules = fc["editor_Tour_Rules"].Trim();
                    TourOld.Tour_ListImage = fc["List_Images_Combo"].Trim();
                    TourOld.Tour_Image = fc["_thumbnail_id"].Trim();
                    TourOld.Product_ID = 1;
                    TourOld.UserCreate = Convert.ToInt32(Session["User_ID"]);
                    TourOld.CreateDate = DateTime.Now;
                    TourOld.Tour_Fixed = Convert.ToBoolean(Convert.ToInt32(fc["Tour_Fixed"].Trim()));
                    TourOld.Tour_StarRate =  Convert.ToDouble(fc["Star_Rating"].Trim());
                       int TourID = TourOld.Insert();
                    if (Convert.ToBoolean(Convert.ToInt32(fc["Tour_Fixed"].Trim())) == true)
                    {
                        TourDetailPrice tourDetailPrice = new TourDetailPrice();
                        tourDetailPrice.Tour_ID = TourID;
                        tourDetailPrice.TourDetailPrice_DepartureDate = Funtions.ddMMyyyy(fc["Tour_DepartureDate"].Trim());
                        tourDetailPrice.TourDetailPrice_ArrivalDate = Funtions.ddMMyyyy(fc["Tour_DepartureDate"].Trim()).AddDays(Convert.ToInt32(fc["Tour_TimeZone"].Trim()));
                        tourDetailPrice.TourDetailPrice_NumberPrice1 = Convert.ToInt32(fc["TourDetailPrice_NumberPrice1"].Trim());
                        tourDetailPrice.TourDetailPrice_Price1 = Convert.ToInt64(fc["TourDetailPrice_Price1"].Trim().Replace(",", ""));
                        tourDetailPrice.TourDetailPrice_NumberPrice2 = Convert.ToInt32(fc["TourDetailPrice_NumberPrice2"].Trim());
                        tourDetailPrice.TourDetailPrice_Price2 = Convert.ToInt64(fc["TourDetailPrice_Price2"].Trim().Replace(",", ""));
                        tourDetailPrice.TourDetailPrice_NumberPrice3 = Convert.ToInt32(fc["TourDetailPrice_NumberPrice3"].Trim());
                        tourDetailPrice.TourDetailPrice_Price3 = Convert.ToInt64(fc["TourDetailPrice_Price3"].Trim().Replace(",", ""));
                        tourDetailPrice.Insert();
                    }
                    TempData["MessagePost"] = "Thêm mới Combo thành công";
                    return RedirectToAction("EditTour", "Manager", new { id = TourID });
                }
            

        }
        #endregion
   

        #region Hotel
        public ActionResult Hotel()
        {
             
                GetDataAllHotel();
                return View();
           
        }
        public PartialViewResult GetDataAllHotel(string Hotelname = "", string location = "")
        {
            
                List<Hotel> L_Hotel = new Hotel().GetAll();
                return PartialView("GetDataAllHotel", L_Hotel);
            
        }
        public ActionResult AddHotel()
        {
          
                Models.Hotel hotel = new Hotel();
                return View(hotel);
            
        }
        public ActionResult EditHotel(int id = 0)
        {

              ViewBag.Hotel_Facilities = SqlModule.GetDataTable("SELECT  *  FROM  [Hotel_Facilities]");
              ViewBag.l_Tags = SqlModule.GetDataTable("SELECT  *  FROM  [tags]");
                if (id != 0)
                {
                    Models.Hotel hotel = new Hotel().GetAllByHotelID(id);
                    return View(hotel);
                }
                return RedirectToAction("Index", "Manager");
          
        }
        [ValidateInput(false)]
        public ActionResult UpdateHotel(FormCollection fc)
        {
           
                if (fc["Post_id"].Trim() != null && fc["Post_id"].Trim() != "0")
                {
                    //,@Hotel_RulesRefund varchar(MAX)
                    Models.Hotel hotels = new Hotel().GetAllByHotelID(Convert.ToInt32(fc["Post_id"].Trim()));
                    hotels.Hotel_Name = fc["Hotel_Name"].Trim();
                    hotels.Hotel_Address = fc["Hotel_Address"].Trim();
                    hotels.Hotel_Content = fc["editor"].Trim();
                    hotels.Tag_ID = fc["Tag_ID"].Trim();
                    hotels.Hotel_Facilities_ID = fc["Hotel_Facilities_ID"].Trim();
                    hotels.Hotel_ListImage = fc["List_Images_Combo"].Trim();
                    hotels.Hotel_Location = Convert.ToInt32(fc["tour_Location"].Trim());
                    hotels.Hotel_StarRate = Convert.ToSingle(fc["Star_Rating"].Trim());
                    hotels.Product_ID = 2;
                    hotels.HotelImage = fc["_thumbnail_id"].Trim();
                    hotels.Hotel_RulesRefund = fc["editor_Hotel_RulesRefund"].Trim();
                    hotels.Update();
                    TempData["MessagePost"] = "Cập nhật khách sạn thành công";
                    return RedirectToAction("EditHotel", "Manager", new { id = hotels.Hotel_ID });
                }
                else
                {
                    Models.Hotel hotels = new Hotel();
                    hotels.Hotel_Name = fc["Hotel_Name"].Trim();
                    hotels.Hotel_Address = fc["Hotel_Address"].Trim();
                    hotels.Hotel_Content = fc["editor"].Trim();
                    hotels.Hotel_CreateDate = DateTime.Now;
                    hotels.Tag_ID = fc["Tag_ID"].Trim();
                    hotels.Hotel_Facilities_ID = fc["Hotel_Facilities_ID"].Trim();
                    hotels.Hotel_ListImage = fc["List_Images_Combo"].Trim();
                    hotels.Hotel_Location = Convert.ToInt32(fc["tour_Location"].Trim());
                    hotels.Hotel_StarRate = float.Parse(fc["Star_Rating"].Trim());
                    hotels.Product_ID = 2;
                    hotels.UserCreate = Convert.ToInt32(Session["User_ID"]);
                    hotels.HotelImage = fc["_thumbnail_id"].Trim();
                    hotels.Hotel_RulesRefund = fc["editor_Hotel_RulesRefund"].Trim();
                    hotels.Hotel_CreateDate = DateTime.Now;
                int hotelId = hotels.Insert();
                    TempData["MessagePost"] = "Thêm mới khách sạn thành công";
                    return RedirectToAction("EditHotel", "Manager", new { id = hotelId });
                }
        
        }
        public PartialViewResult GetRoomHotel(int idHotel = 0)
        {
           
                List<RoomHotel> RoomHotels = new RoomHotel().GetAllByHotelID(idHotel);
                return PartialView("GetRoomHotel", RoomHotels);
           
        }
        #endregion
        #region Location
        public ActionResult Location()
        {
            GetDataLocation();
            return View();
        }
        public PartialViewResult GetDataLocation()
        {
            List<Models.Location> location = new Location().GetAll();
            return PartialView("GetDataLocation", location);
        }
        public string AddLocation(string LocationName)
        {
            Location location = new Location();
            location.Location_Name = LocationName;
            location.Location_Description = "";
            location.Location_images = "";
            int id = location.Insert();
            if (id > 0) { return id.ToString(); }
            else return "";
        }
        public string EditLocation(FormCollection fc)
        {
            Location location = new Location().GetByLocationID(Convert.ToInt32(fc["LocationID"].Trim()));
            location.Location_Name = fc["Location_Name"].Trim();
            location.Location_Description = fc["Location_Description"].Trim();
            location.Location_images = fc["Location_images"].Trim();
            try
            {
                location.Update();
                return "OK";
            }
            catch { return ""; }
        }
        public string Deletelocation(int id = 0)
        {
            Location location = new Location().GetByLocationID(id);
            try
            {
                location.DeleteByID();
                return "OK";
            }
            catch (Exception ex) { return ""; throw ex; }
        }
        #endregion
        #region TopicTour
        public ActionResult TopicTour()
        {
            GetDataTopicTour();
            return View();
        }
        public PartialViewResult GetDataTopicTour()
        {
            List<Models.TopicTour> TopicTours = new TopicTour().GetAll();
            return PartialView("GetDataLocation", TopicTours);
        }
        public string AddTopicTour(string TopicTourName)
        {
            TopicTour TopicTours = new TopicTour();
            TopicTours.TopicTour_Name = TopicTourName;
            TopicTours.TopicTour_Description = "";
            int id = TopicTours.Insert();
            if (id > 0) { return id.ToString(); }
            else return "";
        }
        public string EditTopicTour(FormCollection fc)
        {
            TopicTour TopicTours = new TopicTour().GetByTopicTourID(Convert.ToInt32(fc["LocationID"].Trim()));
            TopicTours.TopicTour_Name = fc["Location_Name"].Trim();
            TopicTours.TopicTour_Description = fc["Location_Description"].Trim();
            try
            {
                TopicTours.Update();
                return "OK";
            }
            catch { return ""; }
        }
        public string DeleteTopicTour(int id = 0)
        {
            TopicTour TopicTours = new TopicTour().GetByTopicTourID(id);
            try
            {
                TopicTours.DeleteByID(TopicTours.TopicTour_ID);
                return "OK";
            }
            catch (Exception ex) { return ""; throw ex; }
        }
        #endregion

        #region UploadImg
        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            DateTime date = DateTime.Now;
            foreach (var file in files)
            {
                string filePath = Path.GetExtension(file.FileName);
                string namePath = "~/Content/Uploads/" + date.Year.ToString() + "/" + ("0" + date.Month.ToString()).Substring(("0" + date.Month.ToString()).Length - 2, 2);
                if (Directory.Exists(Server.MapPath(namePath)))
                {
                    file.SaveAs(Path.Combine(Server.MapPath(namePath), file.FileName));
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath(namePath));
                    file.SaveAs(Path.Combine(Server.MapPath(namePath), file.FileName));
                }
                //Here you can write code for save this information in your database if you want
            }
            return Json("file uploaded successfully");
        }
        public async Task<ActionResult> AllImage()
        {
            LoadAllImage();
            return View();
        }
        public async Task<ActionResult> LoadAllImage()
        {
            
                ptLoadAllImage();
                return PartialView();
         
            //String path = Path.Combine(Server.MapPath("~/Content/Uploads"), "");
            //List<string> dirs = new List<string>(Directory.EnumerateDirectories(path));
            //var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            ////string[] imagearray = Directory.GetFiles(path +"/2020/04", "*.png", SearchOption.TopDirectoryOnly);
            //List<string> imagearray = new List<string>();
            //imagearray = RecursiveLoadImg(path, filters);
            ////var files = GetFilesFrom(path + "/2020/04", filters, false);
            //ViewBag.listimage = imagearray;
            //ptLoadAllImage();
            //return PartialView();
        }
        public async Task<PartialViewResult> ptLoadAllImage()
        {
          
                String path = Path.Combine(Server.MapPath("~/Content/Uploads"), "");
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(path));
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                //string[] imagearray = Directory.GetFiles(path +"/2020/04", "*.png", SearchOption.TopDirectoryOnly);
                List<string> imagearray = new List<string>();
                imagearray = RecursiveLoadImg(path, filters);
                //var files = GetFilesFrom(path + "/2020/04", filters, false);
                ViewBag.listimage = imagearray;
                return PartialView();
           

        }
        public List<string> Fullimglist = new List<string>();
        private List<string> RecursiveLoadImg(string path, String[] filters)
        {
            if (GetFilesFrom(path, filters, false).Count() > 0)
            {
                Fullimglist.AddRange(GetFilesFrom(path, filters, false));
            }
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(path));
            if (dirs.Count > 0) { foreach (var item in dirs) { RecursiveLoadImg(item, filters); } }
            return Fullimglist;
        }
        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
        public void DeleteImages(string urlImages)
        {
            string fullPath = Request.MapPath(urlImages);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
        #endregion
    }
}