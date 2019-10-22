

var user = {
    init:function(){
        user.registerEvent();
    },
    //toàn bộ sự kiện sẽ viết ở đây
    registerEvent: function (e) {

        //fuction load data

     
        console.log("event");
        $('.btn-active').click(function(event) {
            event.preventDefault();
            var btn = $(this);
                var id=$(this).data('id');
                $.ajax(
                    {
                        url: "/Admin/User/ChangeStatus",
                        data: { id: id },
                        type: "POST",
                        dataType: "json",
                    //    contentType: "application/json;charset=utf-8",
                        success: function (repone) {
                            console.log(repone);
                            if (repone.status == true) {
                                //kích hoạt
                                btn.text("Kích hoạt");
                            } else {
                                //hủy kích hoạt 
                                btn.text("Khóa");
                            }
                        }
                    });
        })
    }
}
user.init();