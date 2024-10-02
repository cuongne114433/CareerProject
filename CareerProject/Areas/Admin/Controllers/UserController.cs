using CareerProject.Areas.Admin.Models;
using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserService service = new UserService();
        CareerDBContext dbContext = new CareerDBContext();

        // GET: Admin/User
        public ActionResult Index()
        {
            ViewBag.users = service.GetListUser();
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        // Add new user (POST)
        [HttpPost]
        public ActionResult Add(string name, DateTime? dob, string major, string jobCity, string profileUser, string skill, float? expected, int? experiences, string position, string email, string password)
        {
            if (service.AddUser(name, dob, major, jobCity, profileUser, skill, expected, experiences, position, email, password))
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        static long? idEdit;

        // Edit user (GET)
        public ActionResult Edit(long? id)
        {
            tbl_User user = service.GetUser(id);
            ViewBag.info = user;
            idEdit = id; // Save the ID of the user being edited
            return View(user);
        }

        // Edit user (POST)
        [HttpPost]
        public ActionResult Edit(string name, DateTime? dob, string major, string jobCity, string profileUser, string skill, float? expected, int? experiences, string position, string email, string password)
        {
            if (service.UpdateUser(idEdit, name, dob, major, jobCity, profileUser, skill, expected, experiences, position, email))
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        // Remove a user (AJAX - JSON result)
        public JsonResult Remove(long id)
        {
            var status = false;
            try
            {
                if (service.DeleteUser(id))
                {
                    status = true;
                }
            }
            catch
            {
                status = false;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }

}