using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JwtUtility;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using UAParser;

namespace Shop.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IUserFacade _userFacade;
        private readonly IConfiguration _configuration;


        public AuthController(IUserFacade userFacade, IConfiguration configuration)
        {
            _userFacade = userFacade;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public async Task<ApiResult<LoginResultDto?>> Login(LoginViewModel loginViewModel)
        {
            // میگیره نمایش میده JoinErrors این شرطم چک کردیم اگر وضعیتش معتبر نبود ارور مسیج هاشو از تابع
            // میگیره نشون میده CommandResult البته اینجا نیازی بهش نداریم چون خود برناممون پیغام خطاهارو از
            //if (ModelState.IsValid == false)
            //{
            //    return new ApiResult<string>()
            //    {
            //        IsSuccess = false,
            //        Data = null,
            //        MetaData = new MetaData()
            //        {
            //            AppStatusCode = AppStatusCode.BadRequest,
            //            Message = JoinErrors()
            //        }
            //    };
            //}

            var user = await _userFacade.GetUserByPhoneNumber(loginViewModel.PhoneNumber);
            if (user == null)
            {
                var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
                return CommandResult(result);
            }

            if (Sha256Hasher.IsCompare(user.Password, loginViewModel.Password) == false)
            {
                var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
                return CommandResult(result);
            }

            if (user.IsActive == false)
            {
                var result = OperationResult<LoginResultDto>.Error("حساب کاربری شما غیرفعال است");
                return CommandResult(result);
            }


            var loginResult = await AddTokenAndGenerateJwt(user);
            return CommandResult(loginResult);
        }


        private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
        {
            // است و کارش اینه که دیوایس کاربری که لاگین کرده رو میگیره بهش نشون میده UAParser از پکیج Parser این/
            // در سه خط زیر هم ما اومدیم همینکارو کردیم
            var uaParser = Parser.GetDefault();
            var header = HttpContext.Request.Headers["user-agent"].ToString();
            var device = "Windows";

            if (header != null)
            {
                var info = uaParser.Parse(header);
                // این خط رو داخل متن توضیحات این پروژه توضیح دادیم چیکار میکنه
                device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}/{info.OS.Minor} - {info.UA.Family}";
            }

            var token = JwtTokenBuilder.BuildToken(user, _configuration);
            var refreshToken = Guid.NewGuid().ToString();

            // اینجا توکن و رفرش توکن رو هش کردیم و اضافه کردیم داخل توکن هامون
            var hashJwtToken = Sha256Hasher.Hash(token);
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwtToken, hashRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));

            if (tokenResult.Status != OperationResultStatus.Success)
                return OperationResult<LoginResultDto?>.Error();

            return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }


        [HttpPost("register")]
        public async Task<ApiResult> Register(RegisterViewModel register)
        {
            var command = new RegisterUserCommand(new PhoneNumber(register.PhoneNumber), register.Password);
            var result = await _userFacade.RegisterUser(command);
            return CommandResult(result);
        }


        [HttpPost("refreshToken")]
        public async Task<ApiResult<LoginResultDto?>> RefreshToken(string refreshToken)
        {
            var result = await _userFacade.GetUserTokenByRefreshToken(refreshToken);

            if (result == null)
                return CommandResult(OperationResult<LoginResultDto?>.NotFound());

            if (result.TokenExpireDate > DateTime.Now)
            {
                return CommandResult(OperationResult<LoginResultDto>.Error("توکن هنوز منقضی نشده است"));
            }

            if (result.RefreshTokenExpireDate < DateTime.Now)
            {
                return CommandResult(OperationResult<LoginResultDto>.Error("زمان رفرش توکن به پایان رسیده است"));
            }
            var user = await _userFacade.GetUserById(result.UserId);
            await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
            var loginResult = await AddTokenAndGenerateJwt(user);
            return CommandResult(loginResult);
        }


        // دسترسی داشته باشد Logout رو گذاشتیم چون کاربر حتما باید لاگین کند تا به Authorize این
        // و اگر لاگین نکرده باشد اصلا نیازی هم به خروج ندارد
        [Authorize]
        [HttpDelete("logout")]
        public async Task<ApiResult> Logout()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var result = await _userFacade.GetUserTokenByJwtToken(token);

            if (result == null)
                return CommandResult(OperationResult.NotFound());

            await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
            return CommandResult(OperationResult.Success());
        }



    }
}
