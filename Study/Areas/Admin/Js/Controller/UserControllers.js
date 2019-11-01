//Load Data in Table when documents is ready  
var config = {
    pageIndex: 1,
    PageSize:5
}

$(document).ready(function () {
    loadData();
    
});
function loadData() {
    $("#alertBox").removeClass("hide");
    $("#alertBox").delay(700).slideUp(500);
    $.ajax({
        url: "/Admin/User/listall",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data :{
            page:config.pageIndex,
            pageSize: config.PageSize
        },
        success: function (result) {
            var html = '';
            $.each(result.data, function (key, item) {
                var status = item.status ? "Hoạt động" : "Khóa";
                var Createby = item.createby == null ? "" : item.createby;
                //call function tra ve ten tuong ung voi categoryid 
                html += '<tr>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.fullname + '</td>';
                html += '<td>' + item.address + '</td>';
                html += '<td>' + item.phone + '</td>';
                html += '<td><a href="#" id="btn-active" onclick="return ChangeStatus(' + item.id + ')">' + status + '</a></td>';

                html += '<td>' + Createby + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            pagging(result.total, function () {
              loadData();
            })
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//paging 

function pagging(totalrow, callback) {   
    var totalpage = Math.ceil(totalrow / config.PageSize);
    $('#pagination-user').twbsPagination({
        totalPages: totalpage,
        visiblePages: 5,
        onPageClick: function (event, page) {
            config.pageIndex = page;
            setTimeout(callback, 300);
        }
    });
}
//Add Data Function   
function Add() {
    //validate

    var res = validate();
    if (res == false) {
        return false;
    }
    var user = {
        name: $('#name').val(),
        fullname: $('#fullname').val(),
        pass: $('#pass').val(),
        address: $('#address').val(),
        phone: $('#phone').val(),
        status: $('#address').val(),

    };
    $.ajax({
        url: "/Admin/User/CreateUser",
        data: JSON.stringify(user),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            // ẨN dialog và load lại data
            // đồng thời clear
       
            $('#myModal').modal('hide');
            ClearData();
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Update  
function Update() {
    //validate
    /* var res = validate();
     if (res == false) {
         return false;
     }*/
    var user = {
        id: $('#iduser').val(),
        name: $('#name').val(),
        fullname: $('#fullname').val(),
        pass: $('#pass').val(),
        address: $('#address').val(),
        phone: $('#phone').val(),
        status: $('#status').val(),

    };
    if (user.status == 1) {
        user.status = false;
    } else {
        user.status = true;
    }

    $.ajax({
        url: "/Admin/User/UpdateUser",
        data: JSON.stringify(user),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            // ẨN dialog và load lại data
            // đồng thời clear
            $('#myModal').modal('hide');
            ClearData();
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}



function getbyID(EmpID) {
    $.ajax({
        url: "/Admin/User/GetByID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var statu = result.status ? 0 : 1
            $('#iduser').val(result.id);
            $('#name').val(result.name);
            $('#fullname').val(result.fullname);
            $('#pass').val(result.pass);
            $('#address').val(result.address);
            $('#phone').val(result.phone);
            $('#status').val(statu);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


function ClearData() {
    $('#iduser').val(null);
    $('#name').val("");
    $('#fullname').val("");
    $('#pass').val("");
    $('#address').val("");
    $('#phone').val("");
    $('#status').val(1);
}


function ChangeStatus(ids) {
    $.ajax(
        {
            url: "/Admin/User/ChangeStatus",
            data: { id: ids },
            type: "POST",
            dataType: "json",
            success: function (repone) {
                console.log(repone);
                if (repone.status == true) {
                    //kích hoạt
                    loadData();
                } else {
                    //hủy kích hoạt 
                    loadData();
                }
            }
        });

}

function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/User/DeleteUser/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function validate() {
    var isValid = true;
    if ($('#status').val().trim() == "Chọn kích hoạt") {
        $('#status').css('border-color', 'Red');
        isValid = false;
    }
    if ($('#name').val().trim() == "") {
        $('#name').css('border-color', 'Red');
        isValid = false;
    }
    if ($('#pass').val().trim() == "") {
        $('#pass').css('border-color', 'Red');
        isValid = false;
    }
    return isValid;
}
