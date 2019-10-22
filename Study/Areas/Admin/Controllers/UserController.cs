using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Study.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HttpGet]
        public JsonResult GetByID(long id)
        {
            var result = new UserDao().getByid(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var list = dao.getAllByPageSize(page, pageSize);
            return View(list);
        }

        [HttpGet]
        public JsonResult listall(int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var list = dao.getAllByPageSize(page, pageSize);
            //trả về list sau đó request bên Ajax
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            var u = new UserDao().ViewDetailUser(id);
            return View(u);
        }
        // Update User
        [HttpPost]
        public ActionResult UpdateUser(User u)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                u.pass = Encrytor.MD5Hash(u.pass);
                bool reuslt = dao.Update(u);
                if (reuslt == true)
                {
                    ModelState.AddModelError("", "Cap nhat Tài Khoản Thành Công");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat thất bại");
                }
            }
            return View("Index");


        }


        [HttpPost]
        public JsonResult CreateUser(User user)
        {
            long id = 0l;

            var dao = new UserDao();
            var session = (UserSession)Session[Constants.USER_SESSION];
            //thêm xem ai tạo và ngày tạo
            user.createby = session.UserName;
            if (user.pass != null)
            {
                user.pass = Encrytor.MD5Hash(user.pass);
            }
            id = dao.Insert(user);
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        //delete user
        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            var dao = new UserDao();
            var result = dao.deleteByPk(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //change Status
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().changestatus(id);
            return Json(
                new { status = result });
        }

    }
   

}
