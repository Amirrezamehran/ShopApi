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
            builder.HasIndex(b => b.Slug).IsUnique();

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(b => b.Description)
                .IsRequired();

            builder.Property(b => b.ImageName)
                .IsRequired()
                .HasMaxLength(150);

            // چون مطمعنیم اسلاگ فقط با حروف انگلیسی وارد میشه امدیم قابلیت چندزبانه ثبت شدن این فیلدو غیرفعال کردیم
            builder.Property(b => b.Slug)
                .IsRequired()
                .IsUnicode(false);

            builder.OwnsOne(b => b.SeoData, config =>
            {
                config.Property(b => b.MetaDescription)
                    .HasMaxLength(500)
                    .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle)
                    .HasMaxLength(500)
                    .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeywords)
                    .HasMaxLength(500)
                    .HasColumnName("MetaKeyWords");

                config.Property(b => b.IndexPage)
                    .HasColumnName("IndexPage");

                config.Property(b => b.Canonical)
                    .HasMaxLength(500)
                    .HasColumnName("Canonical");

                config.Property(b => b.Schema)
                    .HasColumnName("Schema");

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

            builder.OwnsMany(b => b.Images, option =>
            {
                option.ToTable("Images", "product");
                option.HasKey(b => b.Id);

                option.Property(b => b.ImageName)
                    .IsRequired()
                    .HasMaxLength(100);
            });


            builder.OwnsMany(b => b.Specifications, option =>
            {

                // بذاریم دوتا جدول جدید میسازه و شدیدا خرابکاری میشه builder بیایم option اگر اشتباهی بجای این
                option.ToTable("Specifications", "product");

                option.HasKey(b => b.Id);

                option.Property(b => b.Key)
                    .IsRequired()
                    .HasMaxLength(50);

                option.Property(b => b.Value)
                    .IsRequired()
                    .HasMaxLength(100);
            });


        }

    }

} // End Class
