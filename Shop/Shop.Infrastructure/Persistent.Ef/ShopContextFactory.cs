//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


// این کلاس کمک خیلی بزرگی کرد وقتی که نمیتونستم میگریشن بگیرم و همش خطا میداد
// این کلاس باعث شد بتونم میگریشن بگیرم حتی وقتی خطا داشتم


//namespace Shop.Infrastructure.Persistent.Ef
//{
//    public class ShopContextFactory : IDesignTimeDbContextFactory<ShopContext>
//    {
//        public ShopContext CreateDbContext(string[] args)
//        {
//            // اینجا باید یک رشته اتصال معتبر برای Design-Time قرار دهید.
//            // این رشته اتصال لزوماً نباید به دیتابیس واقعی متصل شود،
//            // ولی باید ساختار و نوع Provider درست باشد.
//            // توصیه می‌شود از یک رشته اتصال ثابت یا از طریق فایل appsettings.json (که در زمان Design-Time قابل دسترسی باشد) استفاده کنید.
//            // مثال برای SQL Server:
//            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
//            optionsBuilder.UseSqlServer("Data Source=STUDIOAZAR\\MSSTUDIOAZAR;Initial Catalog=ShopApiCourse;Integrated Security=True;TrustServerCertificate=True", sqlServerOptions =>
//            {
//                // برای سازگاری بیشتر، می‌توانید تنظیمات اضافی SQL Server را اینجا اضافه کنید
//                sqlServerOptions.MigrationsAssembly(typeof(ShopContextFactory).Assembly.GetName().Name); // مهم برای Migrations
//            });

//            return new ShopContext(optionsBuilder.Options);
//        }

//    }
//}