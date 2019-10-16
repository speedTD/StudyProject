using Model.Dao;
using Study.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Study.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.login(model.Username,Encrytor.MD5Hash(model.Password));
                if (result)
                {
                    //lấy dl ra từ csdln 
                    var user = dao.getByid(model.Username);
                    var session = new UserSession();
                    session.UserName = user.name;
                    session.UserID = user.id;
                    //gán vào session
                    Session.Add(Constants.USER_SESSION,session);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Xem lại Tài khoản mật khẩu");
                }
            }
           
          
            return RedirectToAction("Index", "Login");
        }
    }
}