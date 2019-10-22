using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateContent()
        {
            setViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult CreateContent(Content model)
        {

            setViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult UpdateContent(long id)
        {
            // lấy ra bản ghi có id đó gán vào fill
            var dao = new ContentDao();
            setViewBag(dao.getByid(id).categoryid);
            return View();
        }

        [HttpPost]
        public ActionResult UpdateContent(Content model)
        {
            if (ModelState.IsValid)
            {
                //update Content
            }
            else
            {

            }
            return View();
        }

        public void setViewBag(long? selected=null)
        {
            var dao = new CategoryDao();
            ViewBag.categoryid = new SelectList(dao.getAll(), "id", "name", selected);
        }
    }
}