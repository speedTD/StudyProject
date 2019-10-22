//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});
/*  <a href="#" class="btn-active" data-id="@item.id">
        @(item.status?"Kích hoạt":"Khóa")
           item.status ? "Kích Hoạt" : "Khóa"
    </a>
*/
function loadData() {
    $.ajax({
        url: "/Admin/User/listall",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                var status = item.status ? "Hoạt động" : "Khóa";
                var Createby = item.createby == null ? "" : item.createby;
            
                html += '<tr>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.fullname + '</td>';
                html += '<td>' + item.address + '</td>';
                html += '<td>' + item.phone + '</td>';
                html += '<td><a href="#" id="btn-active" onclick="return ChangeStatus(' + item.id + ')">'+status+'</a></td>';
            
                html += '<td>' + Createby + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
  
//Add Data Function   
function Add() {
    //validate
   /* var res = validate();
    if (res == false) {
        return false;
    }*/
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
            loadData();          
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getbyID(id) {

}
function ChangeStatus(ids) { 
        $.ajax(
            {
                url: "/Admin/User/ChangeStatus",
                data: {id : ids},
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