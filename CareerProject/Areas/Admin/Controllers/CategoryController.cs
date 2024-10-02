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
    public class CategoryController : Controller
    {
        CategoryService service = new CategoryService();
        CareerDBContext dbContext = new CareerDBContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            ViewBag.categories = service.getListCategory();
            return View();
        }

        // GET: Admin/Category/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/Category/Add
        [HttpPost]
        public ActionResult Add(string name, HttpPostedFileBase image)
        {
            string picPhim = System.IO.Path.GetFileName(image.FileName);
            string pathPhim = System.IO.Path.Combine(Server.MapPath("~/Img"), picPhim);
            image.SaveAs(pathPhim);

            using (MemoryStream ms = new MemoryStream())
            {
                image.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
            }
            if (service.AddCategory(name, picPhim))
            {
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            tbl_Category category = service.getCategory(id);
            ViewBag.info = category;
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string name, HttpPostedFileBase image)
        {
            string picPhim = null;
            if (image != null)
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
            if (service.UpdateCategory(id, name, picPhim))
            {
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        // POST: Admin/Category/Remove/5
        public JsonResult Remove(int id)
        {
            var status = false;
            try
            {
                if (service.DeleteCategory(id))
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