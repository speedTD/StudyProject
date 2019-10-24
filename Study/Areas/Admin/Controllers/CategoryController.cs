using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace Study.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }  
       /* [HttpPost]
        public ActionResult CreateCategory(Category enity)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                dao.Insert(enity);
            }
            return View("Index");
            
        }*/
        [HttpGet]
        public JsonResult listall(int page = 1, int pageSize = 5)
        {
            var result = new CategoryDao().getAll().Skip((page - 1) * pageSize).Take(pageSize);
            var totalrow = new CategoryDao().getAll().Count();
            return Json(new {
                data = result,
                total=totalrow,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        //change Status
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().changestatus(id);
            return Json(
                new { status = result });
        }
        [HttpPost]
        public JsonResult CreateCategory(Category model)
        {
            var session = (UserSession)Session[Constants.USER_SESSION];
            model.createdat = DateTime.Now;
            model.createby = session.UserName;
            var result = new CategoryDao().Insert(model);
            return Json(
               result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateCategory(Category model)
        {
            var session = (UserSession)Session[Constants.USER_SESSION];
            model.modifeiddat = DateTime.Now;
            model.modifeidby = session.UserName;
            var result = new CategoryDao().Update(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteCategory(long id)
        {
            var result = new CategoryDao().Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetByID(long id)
        {
            var data = new CategoryDao().getByid(id);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

    }
}