using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CommentAggregate;
using Shop.Domain.OrderAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.RoleAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SiteEntities;
using Shop.Domain.UserAggregate;


namespace Shop.Infrastructure.Persistent.Ef
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // نکن و هر تغییری که میکنن متوجه نشو GetTracking اینجا میگیم کوئری هامونو
            // GetTracking مگر جاهایی که خودمون تو کوئری گفتیم
            // زیر اگر اینو ننویسیم سایت کند میشه چون هر کوئری که زده میشه از این کانتکست میاد رهگیری میکنه
            // و این رهگیری سربار اضافه داره چون خیلی جاها نیازی نیست اصلا
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // یک اسمبلی داره Shop.Infrastructure این کلاس لایبرری
            // هست IEntityTypeConfiguration و تابع زیر میاد داخل اسمبلی این میگرده و هرچی کلا با تایپ
            // رو پیدا میکنه و اپلای میکنه
            // ShopContext علتش اینکارشم اینه که ما گفتیم بیا برو سراغ اسمبلی کلاس
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }

} // End Class
