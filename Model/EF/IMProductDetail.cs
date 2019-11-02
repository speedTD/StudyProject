namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IMProductDetail")]
    public partial class IMProductDetail
    {

   
        public long id { get; set; }

    
        public long productid { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        [StringLength(500)]
        public string content { get; set; }
    }
    public partial class ReturnIMProductDetail
    {
        public string code { set; get; }
        public IMProductDetail addIMProductDetail { set; get; }
        public List<IMProductDetail> lstIMProductDetail;

    }
}
