namespace Model.Content
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(500)]
        public string despection { get; set; }

        [StringLength(500)]
        public string image { get; set; }

        [Column(TypeName = "xml")]
        public string moreImage { get; set; }

        public int? categoryid { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        public int? wanarty { get; set; }

        public DateTime? createdat { get; set; }

        [StringLength(100)]
        public string createby { get; set; }

        public DateTime? modifeiddat { get; set; }

        [StringLength(100)]
        public string modifeidby { get; set; }

        public int? status { get; set; }

        public DateTime? Tophot { get; set; }

        public long? viewcount { get; set; }

        [StringLength(50)]
        public string tag { get; set; }
    }
}
