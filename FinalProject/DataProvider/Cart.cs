namespace DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        [Key]
        public int RecordID { get; set; }

        [StringLength(50)]
        public string CartID { get; set; }

        [Required]
        [StringLength(10)]
        public string idProduct { get; set; }

        public int count { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        public virtual Product Product { get; set; }



        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        public virtual Product Product { get; set; }
    }
}
