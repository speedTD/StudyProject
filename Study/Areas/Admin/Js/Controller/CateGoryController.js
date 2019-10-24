var config = {
    pageIndex: 1,
    PageSize: 5
}

$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Admin/Category/listall",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: {
            page: config.pageIndex,
            pageSize: config.PageSize
        },
        success: function (result) {
            var html = '';
            $.each(result.data, function (key, item) {
                var status = item.status ? "Hoạt động" : "Khóa";   
                html += '<tr>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.seotitle + '</td>';
                html += '<td>' + item.keyword + '</td>';
                html += '<td><a href="#" id="btn-active" onclick="return ChangeStatus(' + item.id + ')">' + status + '</a></td>';
                html += '<td>' + item.showonhome + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            pagging(result.total, function ()
            {
                loadData();
            })
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function pagging(totalrow, callback) {

    var totalpage = Math.ceil(totalrow / config.PageSize);
    $('#pagination-category').twbsPagination({
        totalPages: totalpage,
        visiblePages: 5,
        onPageClick: function (event, page) {
            config.pageIndex = page;
            setTimeout(callback, 300);
        }
    });
}

function getbyID(EmpID) {
    $.ajax({
        url: "/Admin/Category/GetByID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var statu = result.status ? 0 : 1
            $('#idcategory').val(result.id);
            $('#name').val(result.name);
            $('#image').val(result.image);
            $('#displayoder').val(result.displayoder);
            $('#seotitle').val(result.seotitle);
            $('#keyword').val(result.keyword); 
            $('#status').val(statu);
            $('#showonhome').val(result.showonhome);
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

/*Request Insert Category record*/
function Add() {
    //khai báo 1 object lấy dl từ form về
    var obj = {
        name: $('#name').val(),
        image: $('#image').val(),
        displayoder: $('#displayoder').val(),
        seotitle: $('#seotitle').val(),
        keyword: $('#keyword').val(),
        showonhome: $('#showonhome').val(),
        status: $('#status').val()
    }
    $.ajax({
        url: "/Admin/Category/CreateCategory",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (Reposne) {
            //load data   
            $('#myModal').modal('hide');
            ClearForm();
            loadData();
         
            //clear form
        },
        error: function (errormessage) {
        alert(errormessage.responseText);
        }

    })
}
function Update() {
    //val dữ liệu vào form
    var obj = {
        id: $('#idcategory').val(),
        name: $('#name').val(),
        image: $('#image').val(),
        displayoder: $('#displayoder').val(),
        seotitle: $('#seotitle').val(),
        keyword: $('#keyword').val(),
        showonhome: $('#showonhome').val(),
        status: $('#status').val()
    }
    if (obj.status == 1) {
        obj.status = false;
    } else {
        obj.status = true;
    }
    $.ajax({
        url: "/Admin/Category/UpdateCategory",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (Reposne) {
            $('#myModal').modal('hide');
            loadData();
            ClearForm();
            //clear form
        }
    })
}
function Delele(id) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/Category/DeleteCategory/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (respone) {
                if (respone == true) {
                    //load init data
                    loadData();
                } else {
                    loadData();
                    //show fail
                }
            }


        })
    }
}
function ChangeStatus(ids) {
    $.ajax(
        {
            url: "/Admin/Category/ChangeStatus",
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
 function ClearForm() {
        $('#idcategory').val(null);
        $('#name').val("");
        $('#displayoder').val("");
        $('#keyword').val("");
        $('#seotile').val("");
        $('#showonhome').val("");
        $('#status').val(1);
}


