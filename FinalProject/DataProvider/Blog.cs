namespace DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Blog
    {
        [StringLength(10)]
        public string ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATEPOST { get; set; }

        [StringLength(200)]
        public string TITLE { get; set; }

        [StringLength(4000)]
        public string CONTENT { get; set; }

        [StringLength(600)]
        public string BLOGIMAGE { get; set; }


        [StringLength(200)]
        public string TITLE { get; set; }

        [StringLength(4000)]
        public string CONTENT { get; set; }

        [StringLength(600)]
        public string BLOGIMAGE { get; set; }
    }
}
