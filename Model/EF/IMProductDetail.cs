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
     
        [Column(Order = 0)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long productid { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        [StringLength(500)]
        public string content { get; set; }
    }
}
