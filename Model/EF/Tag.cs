namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tag")]
    public partial class Tag
    {
        public long id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string fullname { get; set; }

        public DateTime? createdat { get; set; }

        [StringLength(50)]
        public string createby { get; set; }

        public DateTime? modifeiddat { get; set; }

        [StringLength(100)]
        public string modifeidby { get; set; }
    }
}
