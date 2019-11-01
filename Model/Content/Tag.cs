namespace Model.Content
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tag")]
    public partial class Tag
    {
        [StringLength(50)]
        public string id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public DateTime? createdat { get; set; }

        [StringLength(100)]
        public string createby { get; set; }

        public DateTime? modifeiddat { get; set; }

        [StringLength(100)]
        public string modifeidby { get; set; }
    }
}
