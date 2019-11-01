using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public JsonResult listall(int page = 1, int pageSize = 5)
        {
            var result = new ProductDao().getAll().Skip((page - 1) * pageSize).Take(pageSize);
            var totalrow = new ProductDao().getAll().Count();
            return Json(new
            {
                data = result,
                total = totalrow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //change Status
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().changestatus(id);
            return Json(
                new { status = result });
        }

        [HttpPost]
        public JsonResult CreateProduct(Product model)
        {
            var session = (UserSession)Session[Constants.USER_SESSION];
            model.createdat = DateTime.Now;
            model.createby = session.UserName;
            var result = new ProductDao().Insert(model);
            return Json(
               result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateProduct(Product model)
        {
            var session = (UserSession)Session[Constants.USER_SESSION];
            model.modifeiddat = DateTime.Now;
            model.modifeidby = session.UserName;
            var result = new ProductDao().Update(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteProduct(long id)
        {
            var result = new ProductDao().Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetByID(long id)
        {
            var result = new ProductDao().getByid(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult BindCategory()
        {
            var result = new CategoryDao().getAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult BindNameByIdCateGoryId(long id)
        {
            var result = new CategoryDao().getByid(id);
            return Json(result.name, JsonRequestBehavior.AllowGet);
        }
        //upload signle
        [HttpPost]
        public JsonResult UploadImage(Product model)
        {
            var file = model.ImageUpload;
            String url = "";
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                url = "/Data/" + file.FileName;
                file.SaveAs(Server.MapPath("/Data/" + file.FileName));

            }
          return Json(url, JsonRequestBehavior.AllowGet);
        }
        //upload muti image
        [HttpPost]
        public JsonResult UploadImages(Product model)
        {
            var file = model.ImageUpload;
            string url = "";
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                url = "/Data/" + file.FileName;
                file.SaveAs(Server.MapPath("/Data/" + file.FileName));

            }
            return Json(url, JsonRequestBehavior.AllowGet);
        }
    }
}
