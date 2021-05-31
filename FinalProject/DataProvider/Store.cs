namespace DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Store")]
    public partial class Store
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string IDPRO { get; set; }

        public int COUNTIN { get; set; }

        public int? COUNTOUT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATEIN { get; set; }

        public virtual Product Product { get; set; }
    }
}
