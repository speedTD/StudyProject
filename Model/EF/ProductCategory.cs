namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        public long id { get; set; }

        public long? preceId { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public int? displayoder { get; set; }

        [StringLength(100)]
        public string seotitle { get; set; }

        [StringLength(100)]
        public string keyword { get; set; }

        public DateTime? createdat { get; set; }

        [StringLength(100)]
        public string createby { get; set; }

        public DateTime? modifeiddat { get; set; }

        [StringLength(100)]
        public string modifeidby { get; set; }

        public bool? status { get; set; }

        public bool? showonhome { get; set; }
    }
}
