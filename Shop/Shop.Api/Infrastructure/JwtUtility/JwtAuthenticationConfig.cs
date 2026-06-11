using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shop.Api.Infrastructure.JwtUtility
{
    // این کلاس کانفیگ احراز هویت و نحوه احراز هویت است
    // احراز هویت میکنیم با این کانفیگ ها JWT که گفتیم با
    public static class JwtAuthenticationConfig
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"])),
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };

                // توکن کاربر رو بگیریم httpContext وقتی اینو بذاریم میتونیم وقتی کاربر لاگین کرد از
                // به شرطی که تو هدر هم ست شده باشه
                option.SaveToken = true;
                
                // وقتی این رویداد رو مینویسیم دیگه فعال یا غیرفعال بودن کاربر فقط موقع لاگین چک نمیشه
                // بلکن هم موقع لاگین چک میشه هم موقع استفاده از سیستم و با هر تابعی که فراخوانی میشه
                // و یا هر ریکوئستی که کاربر ارسال میکنه میاد چک میکنه آیا کاربر فعاله آیا توکنش معتبره یا نه
                option.Events = new JwtBearerEvents()
                {
                    // یک سری ایونت داره که به این صورت هرکدام کارهای متفاوتی انجام میدن که در قسمت 95 همه ایونت ها توضیح داده شده Jwt

                    OnTokenValidated = async context =>
                    {
                        var customValidate = context.HttpContext.RequestServices
                            .GetRequiredService<CustomJwtValidation>();
                        await customValidate.Validate(context);
                    }
                };
                
            });

        }
    }
}
