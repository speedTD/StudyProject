namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public long id { get; set; }

        [Column(TypeName = "ntext")]
        public string image { get; set; }

        public int? displayoder { get; set; }

        [StringLength(50)]
        public string link { get; set; }

        public DateTime? createdate { get; set; }

        [StringLength(50)]
        public string createby { get; set; }

        public bool? status { get; set; }
    }
}
