﻿var url_String = document.URL;
var url_HTML = new URL(url_String);
var id = url_HTML.searchParams.get("id");
$(document).ready(function () {
    bindCategory();
    FillDataEdit(id);

});
//var check change Image Sigle;
var ImageSigle="";
function FillDataEdit(id) {
    $.ajax({
        type: "GET",
        url: "/Admin/Product/GetByID",
        data: { 'id': id },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            var statu = result.product.status ? 0 : 1
            $('#idproduct').val(result.product.id);
            $('#name').val(result.product.name);
            $('#despection').val(result.product.despection);
            $('#image').val(result.product.image);
            $('#moreImage').val(result.product.moreImage);
            $('#price').val(result.product.price);
            $('#quality').val(result.product.quality);
            $('#vat').val(result.product.includevat ? 0 : 1);
            $('#metatitle').val(result.product.metatitle);
            $('#categoryid').val(result.product.categoryid);
            $('#detail').val(result.product.detail);
            $('#status').val(statu);
            //hinh anh dai dien
            var viewImage = document.getElementById("fileDisplayArea");
            var html="";
            html += '<div class="ImageContainer" style="position:relative;text-align:center;">';
            html += '<div style="position:absolute;top:220px;right:16px;font-size:18px">';
            html += '<a href="#" onclick="removeImage()" style=" margin:10px; opacity: 0.7;" class="btn btn-warning"><img style=" width:25px;height:30px;" src="/Assets/Admin/image/recycle_bin.png"/></a>';
            html += '</div>';
            html += '<a href="' + result.product.image + '">';
            html += '<img src="' + result.product.image + '" width="530" height="300"/>';
            html += '</a>';
            html += '</div>';
            viewImage.innerHTML = html;
            ImageSigle = result.product.image;
            //result List Image From ProductDetail
            console.log(result.ListImgbyProductid);
            var viewImages = document.getElementById("viewImages");
            for (var i = 0; i <result.ListImgbyProductid.length; i++) {
                var html = "";
                html += '<div  id ="imagePreview'+i+'" class="ImageContainer" style="position:relative;text-align:center;">';
                html += '<div style="position:absolute;top:245px;right:16px;font-size:18px">';
                html += '<a href="#" onclick="removeImages('+i+')" style=" margin:10px; opacity: 0.7;" class="btn btn-warning"><img style=" width:25px;height:30px;" src="/Assets/Admin/image/recycle_bin.png"/></a>';
                html += '</div>';
                html += '<a href="' + result.ListImgbyProductid[i].content + '">';
                html += '<img src="' + result.ListImgbyProductid[i].content + '"  width="530" height="320"/>';
                html += '</a>';
                html += '</div>';
                viewImages.innerHTML += html;
            }
        },
        error: function (errormessage) {
            console.log(errormessage.responseText);
        }
    });
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
$("#imagesigle").change(function (e) {
    var fileInput = document.getElementById('imagesigle');
    var viewImages = document.getElementById("fileDisplayArea");
    var imageType = /image.*/;
    var reader = new FileReader();
    var file = fileInput.files[0];
    ImageSigle = "/Data/" + file.name;
    SrcUploadImage = file;
    reader.onload = function (event) {
        var image = new Image();
        image.src = event.target.result;
        //image.height = 300;
      //  image.width = 530;
        fileDisplayArea.innerHTML = "";
        var html = "";
        html += '<div  id ="imagePreview" class="ImageContainer" style="position:relative;text-align:center;">';
        html += '<div style="position:absolute;top:245px;right:16px;font-size:18px">';
        html += '<a href="#" onclick="removeImage()" style=" margin:10px; opacity: 0.7;" class="btn btn-warning"><img style=" width:25px;height:30px;" src="/Assets/Admin/image/recycle_bin.png"/></a>';
        html += '</div>';
        html += '<a href="' + image.src + '">';
        html += '<img src="' + image.src + '"  width="530" height="320"/>';
        html += '</a>';
        html += '</div>';
        fileDisplayArea.innerHTML = html;
      //  fileDisplayArea.appendChild(image);
    }
    reader.readAsDataURL(file);
    console.log(ImageSigle);
});
//remove image signle
function removeImage() {
    var x = document.getElementById("fileDisplayArea");
    x.innerHTML = "";
}
//remove image signle
function removeImages(id) {
    //imagePreview
    //fuction delete Request delete
    var x = document.getElementById("imagePreview"+id);
    x.innerHTML = "";
}
//Update  
function Update() {
    //validate
    /* var res = validate();
     if (res == false) {
         return false;
     }*/
    if (!ImageSigle == "" && !SrcUploadImage=="") {
        UploadImage();
    }   
    var obj = {
        id: $('#idproduct').val(),
        name: $('#name').val(),
        despection: $('#despection').val(),
        image: ImageSigle,
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
            alert("Update success !");
            //clear form
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
var SrcUploadImage="";
function UploadImage() {
    var data = new FormData;
    data.append("ImageUpload", SrcUploadImage);
    $.ajax({
        type: "POST",
        url: "/Admin/Product/UploadImage",
        contentType: false,
        processData: false,
        data: data,
        success: function (response) {

        }
    });
}

function DeleleImage(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/Product/DeleteMutiImage/"+ ID,
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