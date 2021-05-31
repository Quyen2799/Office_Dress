namespace DataProvider
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyShopHTQDB : DbContext
    {
        public MyShopHTQDB()
            : base("name=MyShopHTQDB")
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.BLOGIMAGE)
                .IsUnicode(false);

            modelBuilder.Entity<Cart>()
                .Property(e => e.CartID)
                .IsUnicode(false);

            modelBuilder.Entity<Cart>()
                .Property(e => e.idProduct)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.IDCATE);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.IDPRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.IDORDER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.IDCATE)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.SIZE)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.PRODUCTIMAGE)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.idProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IDPRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Stores)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IDPRO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.IDPRO)
                .IsUnicode(false);
        }
    }
}
