using CareerProject.Areas.Admin.Models;
using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Controllers
{
    public class UserProfileController : Controller
    {
        UserService userService= new UserService();
        CVService cVService= new CVService();
        // GET: UserProfile
        public ActionResult Index()
        {
            long id = 1;
            idUser = id;
            ViewBag.listUser = userService.GetUser(id);
            return View(userService.GetUser(id));
        }

        public ActionResult CV()
        {
            ViewBag.listCV = cVService.GetAllCV(1);
            return View();
        }

        public ActionResult JobApplied()
        {
            ViewBag.listJobApplied = userService.GetListAppliedJob(1);
            return View();
        }

        [HttpPost]
        public ActionResult UploadCV(HttpPostedFileBase cvFile)
        {
            string picPhim = System.IO.Path.GetFileName(cvFile.FileName);
            string pathPhim = System.IO.Path.Combine(Server.MapPath("~/CV"), picPhim);
            cvFile.SaveAs(pathPhim);
            using (MemoryStream ms = new MemoryStream())
            {
                cvFile.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
            }

            if(cVService.AddCV(picPhim, DateTime.Now, 1))
            {
                return RedirectToAction("Index", "UserProfile");
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