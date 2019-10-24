using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Study;

namespace Study.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HttpGet]
        public JsonResult GetByID(long id)
        {
            var result = new UserDao().getByid(id);
            // tra ve pass binh thuong
            var passEncrypt = Encrytor.MD5Hash(result.pass);
            result.pass = passEncrypt;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult listall(int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var list = dao.getAll().Skip((page - 1) * pageSize).Take(pageSize);

            var totalrow = dao.getAll().Count();
            //trả về list sau đó request bên Ajax
            return Json(new {
                data = list,
                total = totalrow,
                status=true
            }, JsonRequestBehavior.AllowGet);
        }

        // Update User
        [HttpPost]
        public JsonResult UpdateUser(User u)
        {
             bool reuslt = false;
             var dao = new UserDao();
             u.pass = Encrytor.MD5Hash(u.pass);
             reuslt = dao.Update(u);
             SetAlert("Cập nhật thành công", "success");
            return Json(reuslt, JsonRequestBehavior.AllowGet);
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
            if (id != 0)
            {
                SetAlert("Thêm mới thành công", "success");
            }
            else
            {
                SetAlert("Thêm mới Thất bại", "error");
            }
          
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
