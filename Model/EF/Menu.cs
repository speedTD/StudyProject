namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public long id { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        [StringLength(50)]
        public string link { get; set; }

        public int? displayoder { get; set; }

        [StringLength(50)]
        public string taget { get; set; }

        public bool? status { get; set; }

        public long? typeid { get; set; }
    }
}
