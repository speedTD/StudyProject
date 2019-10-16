using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Study.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tên tài khoản")]
        public String Username { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập tên Mật Khẩu")]
        public String Password { set; get; }
        public bool rememberme { set; get; }

    }
}