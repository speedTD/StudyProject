using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        // GET: Admin/Tag
        public ActionResult Index()
        {
            return View();
        }
        public void setViewBag(long? selected = null)
        {
            var dao = new TagDao();
            ViewBag.id = new SelectList(dao.getAll(), "id", "name", selected);
        }

    }
}