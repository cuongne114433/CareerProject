using CareerProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Controllers
{
    public class HomeController : Controller
    {
        CategoryService categoryService = new CategoryService();
        JobService jobService = new JobService();   
        public ActionResult Index()
        {
            ViewBag.categories = categoryService.getListCategory();
            ViewBag.jobs = jobService.getListJob();
            return View();
        }
    }
}