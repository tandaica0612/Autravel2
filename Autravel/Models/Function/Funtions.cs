using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Autravel.Models;
using System.Globalization;
using System.Reflection;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Autravel.Models.Function
{
    public static class Funtions
    {
        public static string Money(Int64 mo)
        {
            string retval = "";
            retval = string.Format("{0:#,#}", (mo));
            if (string.IsNullOrEmpty(retval))
            {
                retval = "0";
            }
            return retval;
        }
        public static string Money(int mo)
        {
            string retval = "";
            retval = string.Format("{0:#,#}", (mo));
            if (string.IsNullOrEmpty(retval))
            {
                retval = "0";
            }
            return retval;
        }
        public static string ddMMyyyy(DateTime d)
        {
            string datetime = "";
            datetime = d.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            return datetime;
        }
        public static DateTime ddMMyyyy(string d)
        {
            DateTime datetime = new DateTime();
            if (d.Contains("-"))
            {
                datetime = DateTime.ParseExact(d, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                datetime = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            return datetime;
        }
        private static string keyValue = "AutravelStaff";
        public static string EncryptMd5(string password)
        {
            string encryptPassword = password + keyValue;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptPassword);
            HashAlgorithm hash = new MD5CryptoServiceProvider();
            byte[] hashBytes = hash.ComputeHash(passwordBytes);
            encryptPassword = Convert.ToBase64String(passwordBytes);
            return encryptPassword;
        }
        public static string ConvertToUnSignString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }
        public static string ConvertTitleToURL(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                {
                    str = str.Replace(" ", "-");
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
            }

            return str.Replace("*","");

        }
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };
        public static string SendMail(string txtEmail,string txtPassword, string txtTo, string txtMailCC, string txtSubject, string txtBody, string txtHost = "smtp.gmail.com", bool EnableSsl = true,int txtPort = 587)
        {
            string data = "";
            try
            {
                using (MailMessage mm = new MailMessage(txtEmail, txtTo))
                {

                    mm.From = new MailAddress(txtEmail, "Autravel");
                    mm.Subject = txtSubject;
                    mm.IsBodyHtml = true;
                    mm.Body = txtBody;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = txtHost;
                    smtp.EnableSsl = EnableSsl;
                    smtp.Port = txtPort;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(txtEmail, txtPassword);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    if (!string.IsNullOrEmpty(txtMailCC))
                    {
                        var ccEmails = txtMailCC.Split(',');
                        foreach (var ccEmail in ccEmails)
                        {
                            mm.CC.Add(ccEmail);
                        }
                    }
                    smtp.Send(mm);
                }
                data = "Đã gửi email thành công tới: " + txtTo;
            }
            catch (Exception ex) { data = "Đã có lỗi trong quá trình gửi mail."; }
            return data;
        }
        public static string ConvertListToJson<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }
        public static List<T> ConvertJsonToList<T>(string tt)
        {
            return new JavaScriptSerializer().Deserialize<List<T>>(tt);
        }
        public static bool ContainItemInList(string ListString, string ItemID)
        {
            if (ListString != null && ListString != "")
            {
                var ArrListString = ListString.Split(',');
                if (ArrListString.Contains(ItemID)) { return true; }
                else { return false; }
            }
            else { return false; }
        }
        public static string ContainItemInListReturnItemID(string ListString, string ItemID)
        {
            if (ListString != null && ListString != "")
            {
                var ArrListString = ListString.Split(',');
                if (ArrListString.Contains(ListString)) { return ItemID; }
                else { return ""; }
            }
            else { return ""; }
        }
        public static string StripHTML(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", String.Empty);
        }
        public static string LoadCategory(string ListCategory)
        {
            if (string.IsNullOrEmpty(ListCategory))
            {
                return string.Empty;
            }
            string Category_arr = "";
            var ArrListString = ListCategory.Split(',');
            foreach(var item in ArrListString)
            {
            
                var CategoryTitle = new PostCategory().GetByCategoryID(Convert.ToInt32(item)).PostCategory_Title;
                if (CategoryTitle != null || CategoryTitle != "")
                {
                    Category_arr += CategoryTitle + ",";
                }
            }
            Category_arr = Category_arr.Substring(0, Category_arr.Length - 1);
            return Category_arr;
        }
        public static string LoadTopicTour(string ListTopicTour)
        {
            string Category_arr = "";
            var ArrListString = ListTopicTour.Split(',');
            foreach (var item in ArrListString)
            {
                var CategoryTitle = new TopicTour().GetByTopicTourID(Convert.ToInt32(item)).TopicTour_Name;
                if (CategoryTitle != null || CategoryTitle != "")
                {
                    Category_arr += CategoryTitle + ",";
                }
            }
            Category_arr = Category_arr.Substring(0, Category_arr.Length - 1);
            return Category_arr;
        }
        public static string GetRandomForToNumber(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return GetRandomForToNumber(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        public static string FillStar(float rateStar = 0)
        {
            string result = "";
            int star = 0;
            if (rateStar == 0) { return result; }
            //result += string.Format("<span class=\"HBEHTLStar\" >", rateStar);
            if ((rateStar - (int)rateStar) == 0.5)
            {
                star = (int)(rateStar - 0.5);
                for (int i = 0; i < star; i++)
                {
                    result += "<i class=\"fa fa-star\"></i>";
                }
                result += "<i class=\"fa fa-star-half-o\"></i>";
            }
            else
            {
                for (int i = 0; i < (int)rateStar; i++)
                {
                    result += "<i class=\"fa fa-star\"></i>";
                }
            }
            for (int i = 5; i > Math.Ceiling(rateStar); i--)
            {
                result += "<i class=\"fa fa-star-o\"></i>";
            }
            //result += "</span>";
            return result;
        }
        public static string FillStarView(float rateStar = 0)
        {
            string result = "";
            int star = 0;
            if (rateStar == 0) { return result; }
            //result += string.Format("<span class=\"HBEHTLStar\" >", rateStar);
            if ((rateStar - (int)rateStar) == 0.5)
            {
                star = (int)(rateStar - 0.5);
                for (int i = 0; i < star; i++)
                {
                    result += "<i class=\"fa fa-star\"></i>";
                }
                result += "<i class=\"fa fa-star-half-o\"></i>";
            }
            else
            {
                for (int i = 0; i < (int)rateStar; i++)
                {
                    result += "<i class=\"fa fa-star\"></i>";
                }
            }
            
            return result;
        }
    }
}