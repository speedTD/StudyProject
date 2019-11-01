namespace Model.Content
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About
    {
        public long id { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        [Column(TypeName = "ntext")]
        public string image { get; set; }

        public bool? active { get; set; }

        public bool? status { get; set; }

        public DateTime? createdat { get; set; }

        [StringLength(100)]
        public string createby { get; set; }

        public DateTime? modifeiddat { get; set; }

        [StringLength(100)]
        public string modifeidby { get; set; }
    }
}
