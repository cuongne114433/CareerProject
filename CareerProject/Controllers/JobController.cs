using CareerProject.Areas.Admin.Models;
using CareerProject.Common;
using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Controllers
{
    public class JobController : Controller
    {
        JobService jobService = new JobService();
        CVService cVService = new CVService();
        UserService userService = new UserService();
        ApplyService applyService = new ApplyService();
        CareerDBContext db = new CareerDBContext();
        // GET: Job
        public ActionResult Index(string search = null)
        {
            List<tbl_Job> jobs = jobService.getListJob();
            int countJob = 0;
            if (search != null) {
                jobs = jobs.Where(x=>x.Name.ToLower().Contains(search.ToLower()) || x.tbl_Company.Name.ToLower().Contains(search.ToLower()) || x.tbl_Category.Name.ToLower().Contains(search.ToLower())).ToList();
               
            }
            if (jobs != null && jobs.Count() > 0)
                countJob = jobs.Count();
            ViewBag.countJob = countJob;
            ViewBag.jobs = jobs;
            return View();
        }
        static long idJob;
        public ActionResult JobDetail(long id)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            idJob = id;
            ViewBag.jobDetail = jobService.getJob(id);
            ViewBag.cvs = cVService.GetAllCV(session.UserID);
            ViewBag.countApplied = countAppliedFromUser(session.UserID);
            return View();
        }

        public ActionResult FindMatchJob()
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            int countJob = 0;
            var jobs = jobService.getListMatchedJob(session.UserID);
            ViewBag.jobs = jobs;
            if (jobs != null && jobs.Count() > 0)
                countJob = jobs.Count();
            ViewBag.countJobs = countJob;
            return View();
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            return View("Index", "Job", new {search = search});
        }

        [HttpPost]
        public ActionResult Apply(string letter, string cdId = null, HttpPostedFileBase cvUpload = null)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            tbl_User user = userService.GetUser(session.UserID);
           
            string cv = "";
            if (cvUpload == null)
            {
                cv = cdId;
            }
            else
            {
                string picPhim = System.IO.Path.GetFileName(cvUpload.FileName);
                string pathPhim = System.IO.Path.Combine(Server.MapPath("~/CV"), picPhim);
                cvUpload.SaveAs(pathPhim);
                using (MemoryStream ms = new MemoryStream())
                {
                    cvUpload.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
                tbl_CV tblCV = new tbl_CV()
                {
                    IDUser = user.ID,
                    CreationDate = DateTime.Now,
                    FileCV = picPhim,
                };
                db.tbl_CV.Add(tblCV);
                db.SaveChanges();

                cv = tblCV.ID.ToString();
            }

            if (applyService.AddApplyJob(user.ID, idJob, DateTime.Now, user.Name, user.Email, cv, letter, "Applied"))
            {
                return RedirectToAction("Index", "Job");
            }
            return View();
        }

        int countAppliedFromUser(long userID)
        {
            var applied = db.tbl_ApplyJob.Where(x=>x.IDUser == userID && x.IDJob == idJob).ToList();
            if (applied != null && applied.Count > 0)
            {
                return applied.Count();
            }
            return 0;
        }

        string ConvertPdfToBase64(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(fileBytes);
                }
            }
        }
    }
}