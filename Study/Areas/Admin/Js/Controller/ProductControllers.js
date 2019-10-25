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
                var Vat = item.includevat ? "Có" : "Không";
                html += '<tr>';
                html += '<td>' + item.name + '</td>';
                html += '<td  class="price">' + item.price + '</td>';
                html += '<td>' + item.wanarty + '</td>';
                html += '<td>' + item.Category.name+ '</td>'; //đợi tý cái này sẽ lấy từ table khác
                html += '<td><a href="#" id="btn-active" onclick="return ChangeStatus(' + item.id + ')">' + status + '</a></td>';
                html += '<td>' + item.newprice + '</td>';
                html += '<td>' + Vat+ '</td>';
                html += '<td>' + item.viewcount + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });

            $('.tbody').html(html);
            total();
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
            $('#vat').val(result.includevat ? 0 : 1);
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
        includevat: $('#vat').val(),
        metatitle: $('#metatitle').val(),
        categoryid: $('#categoryid').val(),
        detail: $('#detail').val(),
        status: $('#status').val()
    }
    if (obj.includevat == 1) {
        obj.includevat = false;
    } else {
        obj.includevat = true;
    }
    UploadImage();
    $.ajax({
        url: "/Admin/Product/CreateProduct",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (Reposne) {
            //load data   
            $('#myModal').modal('hide');
            //ClearForm();
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
        includevat: $('#vat').val(),
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
    if (obj.includevat == 1) {
        obj.includevat = false;
    } else {
        obj.includevat = true;
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
function ChangeStatus(ids) {
    $.ajax(
        {
            url: "/Admin/Product/ChangeStatus",
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
function UploadImage() {

    var files = $("#myFile").get(0).files;
        //var myID = 3; //uncomment this to make sure the ajax URL works
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }

                $.ajax({
                    type: "POST",
                    url: "/Admin/Product/UploadImage",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (result) {
                        $('#image').val(result.name);
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
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
function total() {
    var priceCells = document.getElementsByClassName("price"); //returns a list with all the elements that have class 'priceCell'

    var total = 0;

    //loop over the cells array and add to total price 
    for (var i = 0; i < priceCells.length; i++) {
        var thisPrice = parseFloat(priceCells[i].innerHTML); //get inner text of this cell in number format
        total = total + thisPrice;
    };
    total = total.toFixed(2); //give 2 decimal points to total - prices are, e.g 59.80 not 59.8
    console.log(total);
}


    


