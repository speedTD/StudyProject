using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var session = (UserSession)Session[Constants.USER_SESSION];
            if (session == null)
            {
                //chuyen huong ve trang login
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext); 
        }
        //viết 1 base để alert thông báo cho người dùng 
        protected void SetAlert(String message,String type)
        {
            TempData["AlertMessage"] = message;
            switch (type)
            {
                case "success": TempData["AlertType"] = "alert-success"; break;
                case "warning": TempData["AlertType"] = "alert-warning"; break;
                case "error": TempData["AlertType"] = "alert-danger"; break;

            }
         
        }
    }
}