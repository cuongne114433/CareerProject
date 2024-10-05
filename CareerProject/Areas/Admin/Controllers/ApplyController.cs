using CareerProject.Areas.Admin.Models;
using CareerProject.Common;
using CareerProject.Models.DTO;
using Microsoft.Reporting.WebForms;
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
            ViewBag.applyJobs = service.GetListApplyJob().Where(x => x.applyJob.tbl_Job.tbl_Company.ID == session.UserID).ToList();
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
        public ActionResult Report(string type = null)
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/ExportCV.rdlc");

            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            ReportDataSource rd = new ReportDataSource();
            rd.Name = "DataSet1";
            List<tbl_ApplyJob> list = new List<tbl_ApplyJob>();
            DateTime dt = new DateTime();
            List<tbl_ApplyJob>  data = new List<tbl_ApplyJob>();
            if (type != null)
            {
                data = dbContext.tbl_ApplyJob.Where(x => x.tbl_Job.IDCompany == session.UserID && x.ApplyStatus.Trim() == type).ToList();

            }
            else
            {
                data = dbContext.tbl_ApplyJob.Where(x => x.tbl_Job.IDCompany == session.UserID).ToList();

            }
            if (data != null && data.Count() > 0)
            {
                foreach (var item in data)
                {
                    tbl_ApplyJob view = new tbl_ApplyJob();
                    view.ID = item.ID;
                    view.Name = item.Name;
                    view.ApplyStatus = item.ApplyStatus;
                    view.CoverLetter = item.CoverLetter;
                    view.tbl_Job = item.tbl_Job;
                    view.AppliedDate = item.AppliedDate;
                    view.Mail = item.Mail;
                    list.Add(view);
                }
                rd.Value = list;

            }

            lr.DataSources.Add(rd);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;




            fileNameExtension = "pdf";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            string deviceInfo =

   "<DeviceInfo>" +
   "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
   "  <PageWidth>8in</PageWidth>" +
   "  <PageHeight>8in</PageHeight>" +
   "  <MarginTop>0.5in</MarginTop>" +
   "  <MarginLeft>0.2in</MarginLeft>" +
   "  <MarginRight>0.2in</MarginRight>" +
   "  <MarginBottom>0.5in</MarginBottom>" +
   "</DeviceInfo>";
            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

    }
}