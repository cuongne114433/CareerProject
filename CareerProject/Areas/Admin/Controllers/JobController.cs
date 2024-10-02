using CareerProject.Areas.Admin.Models;
using CareerProject.Common;
using CareerProject.Models.DTO;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CareerProject.Areas.Admin.Controllers
{
    public class JobController : Controller
    {
        JobService service = new JobService();
        CareerDBContext dbContext = new CareerDBContext();
        // GET: Admin/Job
        public ActionResult Index(long? idCompany = null)
        {

            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            if(session.Role == "Admin")
            {
                ViewBag.jobs = service.getListJob().Where(x => x.IDCompany == idCompany).ToList();

            }
            else
            {
                ViewBag.jobs = service.getListJob().Where(x => x.IDCompany == session.UserID).ToList();

            }
            return View();
        }

        public ActionResult Add()
        {
            ViewBag.IDCategory = dbContext.tbl_Category.ToList();
            return View();
        }
        // Add new job (POST)
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Add(string name, string detail, string requirement, string description, string benefit, string offer, string industry, DateTime creationDate, DateTime limitDate, int total, string type, string sex, string location, int idCategory )
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            ViewBag.IDCategory = dbContext.tbl_Category.ToList();

            if (session != null)
            {
                long? idCompany = session.UserID;
                if (service.AddJob(name, detail, requirement, description, benefit, offer, industry, creationDate, limitDate, total, type, sex, location, idCompany, idCategory))
                {
                    return RedirectToAction("Index", "Job");
                }
            }
           
            return View();
        }

        static long? idEdit;

        // Edit job (GET)
        public ActionResult Edit(long? id)
        {
            tbl_Job job = service.getJob(id);
            ViewBag.info = job;
            idEdit = id; // Save the ID of the job being edited
            return View(job);
        }

        // Edit job (POST)
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(string name, string detail, string requirement, string description, string benefit, string offer, string industry, DateTime creationDate, DateTime limitDate, int total, string type, string sex, string location, int idCategory)
        {
            if (service.UpdateJob(idEdit, name, detail, requirement, description, benefit, offer, industry, creationDate, limitDate, total, type, sex, location,idCategory))
            {
                return RedirectToAction("Index", "Job");
            }
            return View();
        }

        // Remove a job (AJAX - JSON result)
        public JsonResult Remove(long id)
        {
            var status = false;
            try
            {
                if (service.DeleteJob(id))
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