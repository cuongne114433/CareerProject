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
    public class ApplyController : Controller
    {
        // GET: Admin/Apply
        ApplyService service = new ApplyService();
        CareerDBContext dbContext = new CareerDBContext();
        // GET: Admin/ApplyJob
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            ViewBag.applyJobs = service.GetListApplyJob().Where(x=>x.applyJob.tbl_Job.tbl_Company.ID == session.UserID).ToList();
            return View();
        }

        // GET: Admin/ApplyJob/Add
        public ActionResult Add()
        {
            ViewBag.Users = dbContext.tbl_User.ToList();
            ViewBag.Jobs = dbContext.tbl_Job.ToList();
            return View();
        }

        // Add new job application (POST)
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(long idUser, long idJob, DateTime appliedDate, string name, string mail, string cv, string coverLetter, string applyStatus)
        {
            ViewBag.Users = dbContext.tbl_User.ToList();
            ViewBag.Jobs = dbContext.tbl_Job.ToList();
            if (service.AddApplyJob(idUser, idJob, appliedDate, name, mail, cv, coverLetter, applyStatus))
            {
                return RedirectToAction("Index", "ApplyJob");
            }
            return View();
        }

        static long? idEdit;

        // Edit job application (GET)
        public ActionResult Edit(long? id)
        {
            tbl_ApplyJob applyJob = service.GetApplyJob(id);
            ViewBag.info = applyJob;
            ViewBag.Users = dbContext.tbl_User.ToList();
            ViewBag.Jobs = dbContext.tbl_Job.ToList();
            idEdit = id; // Save the ID of the application being edited
            return View(applyJob);
        }

        // Edit job application (POST)
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(long idUser, long idJob, DateTime appliedDate, string name, string mail, string cv, string coverLetter, string applyStatus)
        {
            if (service.UpdateApplyJob(idEdit, idUser, idJob, appliedDate, name, mail, cv, coverLetter, applyStatus))
            {
                return RedirectToAction("Index", "ApplyJob");
            }
            return View();
        }

        // Remove a job application (AJAX - JSON result)

        public JsonResult Remove(long id)
        {
            var status = false;
            try
            {
                if (service.DeleteApplyJob(id))
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
        public JsonResult Interested(long id)
        {
            var status = false;
            try
            {
                if (service.ProcessCV(id, "Interested"))
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

        public JsonResult Reject(long id)
        {
            var status = false;
            try
            {
                if (service.ProcessCV(id, "Rejected"))
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