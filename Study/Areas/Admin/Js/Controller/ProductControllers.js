var config = {
    pageIndex: 1,
    PageSize: 5
}

$(document).ready(function () {
    loadData();
    bindCategory();
});

function loadData() {
    $.ajax({
        url: "/Admin/Product/listall",
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
                var x = "";
                bindCategoryByid(item.categoryid).then(function (result) {
                    // here you can use the result of promiseB
                    console.log(result);
                  
                });
                html += '<tr>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.price + '</td>';
                html += '<td>' + item.wanarty + '</td>';
                html += '<td>' + item.Category.name+ '</td>'; //đợi tý cái này sẽ lấy từ table khác
                html += '<td><a href="#" id="btn-active" onclick="return ChangeStatus(' + item.id + ')">' + status + '</a></td>';
                html += '<td>' + item.newprice + '</td>';
                html += '<td>' + item.includevat + '</td>';
                html += '<td>' + item.viewcount + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
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
function pagging(totalrow, callback) {

    var totalpage = Math.ceil(totalrow / config.PageSize);
    $('#pagination-product').twbsPagination({
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
        url: "/Admin/Product/GetByID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var statu = result.status ? 0 : 1           
            $('#idproduct').val(result.id);
            $('#name').val(result.name);
            $('#despection').val(result.despection);
            $('#image').val(result.image);
            $('#moreImage').val(result.moreImage);
            $('#price').val(result.price);
            $('#quality').val(result.quality);
            $('#includevat').val(result.includevat);
            $('#metatitle').val(result.metatitle);
            $('#categoryid').val(result.categoryid);
            $('#detail').val(result.detail);
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


/*Request Insert Product record*/
function Add() {
    //khai báo 1 object lấy dl từ form về
    var obj = {
        name: $('#name').val(),
        despection: $('#despection').val(),
        image: $('#image').val(),
        moreImage: $('#moreImage').val(),
        price: $('#price').val(),
        quality: $('#quality').val(),
        newprice: $('#newprice').val(),
        includevat: $('#includevat').val(),
        metatitle: $('#metatitle').val(),
        categoryid: $('#categoryid').val(),
        detail: $('#detail').val(),
        status: $('#status').val()
    }


    $.ajax({
        url: "/Admin/Product/CreateProduct",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (Reposne) {
            //load data   
            $('#myModal').modal('hide');
         //   ClearForm();
            loadData();

            //clear form
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }

    })
}

//Update  
function Update() {
    //validate
    /* var res = validate();
     if (res == false) {
         return false;
     }*/
    var obj = {
        id: $('#idproduct').val(),
        name: $('#name').val(),
        despection: $('#despection').val(),
        image: $('#image').val(),
        moreImage: $('#moreImage').val(),
        price: $('#price').val(),
        quality: $('#quality').val(),
        status: $('#status').val(),
        includevat: $('#includevat').val(),
        metatitle: $('#metatitle').val(),
        categoryid: $('#categoryid').val(),
        detail: $('#detail').val(),
        status: $('#status').val()

    };

    if (obj.status == 1) {
        obj.status = false;
    } else {
        obj.status = true;
    }

    $.ajax({
        url: "/Admin/Product/UpdateProduct",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            // ẨN dialog và load lại data
            // đồng thời clear
            $('#myModal').modal('hide');         
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/Product/DeleteProduct/" + ID,
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

function bindCategory() {
    $.ajax({
        type: "GET",
        url: "/Admin/Product/BindCategory",
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (respone) {
            $.each(respone, function (data, value) {
                $("#categoryid").append($("<option></option>").val(value.id).html(value.name));
            })
        }

    });
}
function bindCategoryByid(id) {
    var deferred = $.Deferred();
    return new Promise(function (resolve, reject) {

        $.ajax({
            type: "GET",
            url: "/Admin/Product/BindNameByIdCateGoryId/"+id,
            dataType: "json",
            async: false,
            contentType: "application/json;charset=UTF-8",
            success: function (v) {   
                deferred.resolve(v);
            }

        });
        return deferred.promise();
    });
       
}


    


