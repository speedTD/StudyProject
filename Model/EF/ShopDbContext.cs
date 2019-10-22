namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopDbContext : DbContext
    {
        public ShopDbContext()
            : base("name=Shop")
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContentTag> ContentTags { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Content> Contents { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.showonhome)
                .IsFixedLength();

            modelBuilder.Entity<ContentTag>()
                .Property(e => e.contentid)
                .IsUnicode(false);

            modelBuilder.Entity<ContentTag>()
                .Property(e => e.tagid)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.taget)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.categoryid)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.newprice)
                .HasPrecision(18, 0);
        }
    }
}
