namespace DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int ID { get; set; }

        public int IDORDER { get; set; }

        [Required]
        [StringLength(10)]
        public string IDPRODUCT { get; set; }

        public int COUNT { get; set; }

        public int UNITPRICE { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
