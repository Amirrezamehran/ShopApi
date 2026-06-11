using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.AspNetCore;
using Common.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Shop.Api.Infrastructure;
using Shop.Api.Infrastructure.JwtUtility;
using Shop.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// رو تغییر دادیم به قالب پیغامی که خواستیم BadRequest مربوط به Api اینجا اومدیم ریسپانس تایپ
// هامون رو تغییر میدیم Api میکنیم میایم رفتار Add وقتی داریم کنترلرهامون رو
// رو میخوایم تغییر بدیم به چیزی که خودمون میخوایم InvalidModelStateResponseFactory و میگیم
// است context پس میایم صداش میزنیم و یک اکشن دریافت میکنه که ورودی اکشنه یک
// .ورودیشه context و اون پرانتز هم خود اکشنه هست که
// GetModelStateErrors ارور هاشو دریافت کردیم با استفاده از متد context و بعد اومدیم از
// خودمون ساختیم که میاد هنگام ارور result و بعد ریختیم داخل مسیج. و درنهایت یک
// این قالب مارو نشون میده با مسیجی که ما گفتیم و کد خطایی که مشخص کردیم BadRequest
builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.InvalidModelStateResponseFactory = (context =>
    {
        var result = new ApiResult()
        {
            IsSuccess = false,
            MetaData = new()
            {
                AppStatusCode = AppStatusCode.BadRequest,
                Message = ModelStateUtility.GetModelStateErrors(context.ModelState)
            }
        };
        // تبدیل میکنه و برمیگردونه Json رو به result اینم خودش آبجکت
        return new BadRequestObjectResult(result);
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// کنیم Authentication بتوانیم Swagger کل محتوای داخل این تابع برای اینه که داخل خود
// استفاده کنیم PostMan که دیگه نیاز نباشه برای تست هامون از
// ها بکنیم Api فقط کافیه هنگام اجرای برنامه توکن را در بخش مربوطه وارد کنیم و شروع به تست
builder.Services.AddSwaggerGen(option =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter Token",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterShopDependency(connectionString);
builder.Services.RegisterApiDependency();
CommonBootstrapper.Init(builder.Services);
builder.Services.AddTransient<IFileService, FileService>();

// ما کانفیگ شد JWT به این صورت توکن
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

// است Production یک پروتکل امنیتی است که بهتره فقط وقتی پروژه روی حالت Cors این
// و فقط برای دامنه ای که قراره ازش استفاده کنه بازش بکنیم
app.UseCors("ShopApi");

// برای اینکه احراز هویت هنگام لاگین فعال بشه اینو باید بنویسیم اینجا
app.UseAuthentication();
app.UseAuthorization();
app.UseApiCustomExceptionHandler();
app.MapControllers();

app.Run();
