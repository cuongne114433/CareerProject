using CareerProject.Areas.Admin.Models;
using CareerProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerProject.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        NewsService service = new NewsService();
        CareerDBContext dbContext = new CareerDBContext();

        // GET: Admin/News
        public ActionResult Index()
        {
            ViewBag.newsList = service.GetAllNews();
            return View();
        }

        // Add news (GET)
        public ActionResult Add()
        {
            return View();
        }

        // Add news (POST)
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(string Title, string Content, DateTime CreationDate, HttpPostedFileBase Image)
        {
            string picPhim = System.IO.Path.GetFileName(Image.FileName);
            string pathPhim = System.IO.Path.Combine(Server.MapPath("~/Img"), picPhim);
            Image.SaveAs(pathPhim);

            using (MemoryStream ms = new MemoryStream())
            {
                Image.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
            }
            if (service.AddNews(Title, CreationDate, Content, picPhim))
            {
                return RedirectToAction("Index", "News");
            }
            return View();
        }

        static long? idEdit;

        // Edit news (GET)
        public ActionResult Edit(long? id)
        {
            tlb_news news = service.GetNewsById(id);
            ViewBag.info = news;
            idEdit = id; // Save the ID of the news being edited
            return View(news);
        }

        // Edit news (POST)
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(string title, DateTime creationDate, string content, HttpPostedFileBase image)
        {
            string picPhim = null;
            if(image!= null)
            {
                picPhim = System.IO.Path.GetFileName(image.FileName);
                string pathPhim = System.IO.Path.Combine(Server.MapPath("~/Img"), picPhim);
                image.SaveAs(pathPhim);

                using (MemoryStream ms = new MemoryStream())
                {
                    image.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            if (service.UpdateNews(idEdit, title, creationDate, content, picPhim))
            {
                return RedirectToAction("Index", "News");
            }
            return View();
        }

        // Remove news (AJAX - JSON result)
        public JsonResult Remove(long id)
        {
            var status = false;
            try
            {
                if (service.DeleteNews(id))
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