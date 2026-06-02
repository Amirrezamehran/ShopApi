namespace Shop.Api.Infrastructure
{
    public static class DependencyRegister
    {
        public static void RegisterApiDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile).Assembly);


            // است Production یک پروتکل امنیتی است که بهتره فقط وقتی پروژه روی حالت Cors این
            // ها اضافه کنیمش Middleware و فقط برای دامنه ای که قراره ازش استفاده کنه بازش بکنیم و درنهایت به قسمت
            services.AddCors(options =>
            {
                options.AddPolicy(name: "ShopApi",
                    builder =>
                    {
                        // درخواست بده CORS اینجا گفتیم هر درخواستی با هر مبدا و متدی با هر هدری میتونه با این وبسایت ما روی این پروتکل
                        // یا میتونیم محدودش کنیم بگیم فقط فلان سایت ها میتونن اینکارو بکنن
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }
    }
}
