using CareerProject.Areas.Admin.Models;
using CareerProject.Common;
using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace CareerProject.Controllers
{
    public class UserProfileController : Controller
    {
        UserService userService = new UserService();
        CVService cVService = new CVService();
        // GET: UserProfile
        long idLogin;
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            idLogin = session.UserID;
            idUser = idLogin;
            ViewBag.listUser = userService.GetUser(idLogin);
            return View(userService.GetUser(idLogin));
        }

        public ActionResult CV()
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            idLogin = session.UserID;
            idUser = idLogin;
            ViewBag.listCV = cVService.GetAllCV(idLogin);
            return View();
        }

        public ActionResult JobApplied()
        {
            ViewBag.listJobApplied = userService.GetListAppliedJob(idLogin);
            return View();
        }

        [HttpPost]
        public ActionResult UploadCV(HttpPostedFileBase cvFile)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            idLogin = session.UserID;
            idUser = idLogin;
            string picPhim = System.IO.Path.GetFileName(cvFile.FileName);
            string pathPhim = System.IO.Path.Combine(Server.MapPath("~/CV"), picPhim);
            cvFile.SaveAs(pathPhim);
            using (MemoryStream ms = new MemoryStream())
            {
                cvFile.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
            }

            if (cVService.AddCV(picPhim, DateTime.Now, idLogin))
            {
                return RedirectToAction("CV", "UserProfile");
            }

            return View("CV", "UserProfile"); ;
        }

        static long idUser;
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateProfile(
            string name,
            string dob,
            string major,
            string jobCity,
            string ProfileUser,
            string skill,
            string expected,
            string experiences,
            string position,
            string email)
        {
            if (userService.UpdateUser(idUser, name, Convert.ToDateTime(dob), major, jobCity, ProfileUser, skill, float.Parse(expected), int.Parse(experiences), position, email))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}