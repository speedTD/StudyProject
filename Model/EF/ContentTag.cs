namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentTag")]
    public partial class ContentTag
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string contentid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string tagid { get; set; }
    }
}
