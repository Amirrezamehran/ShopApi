using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAggregate;

namespace Shop.Infrastructure.Persistent.Ef.ProductAggregate
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "product");

            // ایندکس کردیم که سریعتر سرچ بشه و اینکه یونیک کردیم تا تکراری نتونه کسی وارد کنه
            builder.HasIndex(p => p.Slug).IsUnique();

            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.ImageName).HasMaxLength(150).IsRequired();

            // چون مطمعنیم اسلاگ فقط با حروف انگلیسی وارد میشه امدیم قابلیت چندزبانه ثبت شدن این فیلدو غیرفعال کردیم
            builder.Property(p => p.Slug).IsRequired().IsUnicode(false);

            builder.OwnsOne(p => p.SeoData, option =>
            {
                option.Property(p => p.MetaTitle).HasMaxLength(500).HasColumnName("MetaTitle");
                option.Property(p => p.MetaDescription).HasMaxLength(500).HasColumnName("MetaDescription");
                option.Property(p => p.MetaKeywords).HasMaxLength(500).HasColumnName("MetaKeywords");
                option.Property(p => p.IndexPage).HasMaxLength(500).HasColumnName("IndexPage");
                option.Property(p => p.Canonical).HasMaxLength(500).HasColumnName("Canonical");
                option.Property(p => p.Schema).HasMaxLength(500).HasColumnName("Schema");

                // این کد زیر فقط مثاله که از خودم درآوردم تا توضیح بدم
                // اگر به فرض هر کدوم از پراپرتی هایی که تعریف کردیم خودش یک رابطه
                // یک به چند داخلش داشت میایم به صورت زیر عمل میکنیم
                // داخلش یک رابطه یک به چند داره میایم به اینصورت دقیقا داخل MetaTitle به فرض
                // SeoData خودش که میشه Parent رابطه یک به چند
                // اون رابطه یک به چندشو یک حتی یک به یک اگر داشت اینجا پیاده سازی میکنیم دوباره
                // تو اواسط قسمت 75 توضیح میده راجب این موضوع که چیه
                //builder.OwnsOne(p => p.MetaTitle, option =>
                //{
                //    option.Property(p => p.Title).HasMaxLength(500).HasColumnName("Canonical");
                //    option.Property(p => p.Count).HasMaxLength(500).HasColumnName("Schema");
                //});

            });


            builder.OwnsMany(p => p.Images, option =>
            {
                builder.ToTable("Images", "product");
                
                option.Property(p => p.ImageName).HasMaxLength(150).IsRequired();
            });

            builder.OwnsMany(p => p.Specifications, option =>
            {
                builder.ToTable("Specifications", "product");

                option.Property(p => p.Key).HasMaxLength(50).IsRequired();
                option.Property(p => p.Value).HasMaxLength(100).IsRequired();
            });

        }

    }

} // End Class
