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

                //option.Events = new JwtBearerEvents()
                //{
                //    OnTokenValidated = async context =>
                //    {
                //        var customValidate = context.HttpContext.RequestServices
                //            .GetRequiredService<CustomJwtValidation>();
                //        await customValidate.Validate(context);
                //    }
                //};
            });

        }
    }
}
