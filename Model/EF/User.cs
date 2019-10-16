namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long id { get; set; }
        [Display(Name ="Tên Tài khoản")]
        [StringLength(50)]
        public string name { get; set; }

        [Display(Name = "Tên Đầy Đủ")]
        [StringLength(50)]
        public string fullname { get; set; }

        [Display(Name = "Mật Khẩu")]
        [StringLength(50)]
        public string pass { get; set; }


        [Display(Name = "Địa Chỉ")]
        [StringLength(50)]
        public string address { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [StringLength(50)]
        public string phone { get; set; }

        public bool? status { get; set; }

        public DateTime? createdat { get; set; }

        [StringLength(50)]
        public string createby { get; set; }
    }
}
