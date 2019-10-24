

//check login
function checkLogin() {
    window.location.href="/Admin/"
    $.ajax({
        url: "/Admin/Login",
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result == true) {
                // redirect to home ConTroller
                window.location.href =result.Url;
            } else {
                //thông báo Lỗi đăng nhập
                alert("tài Khoản Mật khẩu không chính Xác");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
   
}