namespace DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(100)]
        public string EMAIL { get; set; }

        [StringLength(500)]
        public string ADDRESS { get; set; }

        [StringLength(15)]
        public string PHONE { get; set; }

        [ScaffoldColumn(false)]
        public int TOTALPRICE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
