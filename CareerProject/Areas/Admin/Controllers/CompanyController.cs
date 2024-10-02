using CareerProject.Areas.Admin.Models;
using CareerProject.Common;
using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyService service = new CompanyService();
        private CareerDBContext dbContext = new CareerDBContext();

        // GET: Admin/Company
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];

            if (session.Role == "Admin")
            {
                ViewBag.companies = service.GetListCompany();

            }
            else
            {
                ViewBag.companies = service.GetListCompany().Where(x=>x.ID == session.UserID).ToList();

            }
            return View();
        }

        // Add new company (GET)
        public ActionResult Add()
        {
            return View();
        }

        // Add new company (POST)
        [HttpPost]
        public ActionResult Add(string name, string description, string avt, string phoneNumber, string email, string location)
        {
            if (service.AddCompany(name, description, avt, phoneNumber, email, location))
            {
                return RedirectToAction("Index", "Company");
            }
            return View();
        }

        static long? idEdit;

        // Edit company (GET)
        public ActionResult Edit(long? id)
        {
            tbl_Company company = service.GetCompany(id);
            ViewBag.info = company;
            idEdit = id; // Save the ID of the company being edited
            return View(company);
        }

        // Edit company (POST)
        [HttpPost]
        public ActionResult Edit(string name, string description, string avt, string phoneNumber, string email, string location)
        {
            if (service.UpdateCompany(idEdit, name, description, avt, phoneNumber, email, location))
            {
                return RedirectToAction("Index", "Company");
            }
            return View();
        }

        // Remove a company (AJAX - JSON result)
        public JsonResult Remove(long id)
        {
            var status = false;
            try
            {
                if (service.DeleteCompany(id))
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