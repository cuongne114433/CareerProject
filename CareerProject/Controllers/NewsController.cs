using CareerProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Controllers
{
    public class NewsController : Controller
    {
        NewsService newsService = new NewsService();
        // GET: News
        public ActionResult Index()
        {
            ViewBag.news = newsService.GetAllNews();
            return View();
        }
        public ActionResult Details(long id)
        {
            ViewBag.newDetails = newsService.GetNewsById(id);
            return View();
        }
    }
}