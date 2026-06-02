using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Domain.RoleAggregate.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper;

        public UserController(IUserFacade userFacade, IMapper mapper)
        {
            _userFacade = userFacade;
            _mapper = mapper;
        }

        // به صورت خودکار میره از بادی این مقدار رو دریافت کنه و به خطا میخوره [FromQuery] اگر نگیم
        // بره دنبالش بگرده URL میفرستیم برات یعنی داخل QueryParam باید حتما بگیم که این مقدار رو به صورت
        [HttpGet]
        public async Task<ApiResult<UserFilterResult>> GetUsers([FromQuery] UserFilterParams filterParams)
        {
            var result = await _userFacade.GetUserByFilter(filterParams);
            return QueryResult(result);
        }

        [HttpGet("Current")]
        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            var result = await _userFacade.GetUserById(User.GetUserId());
            return QueryResult(result);
        }

        
        [HttpGet("{userId}")]
        public async Task<ApiResult<UserDto?>> GetById(long userId)
        {
            var result = await _userFacade.GetUserById(userId);
            return QueryResult(result);
        }

  
        [HttpPost]
        public async Task<ApiResult> Create(CreateUserCommand command)
        {
            var result = await _userFacade.CreateUser(command);
            return CommandResult(result);
        }

        //[HttpPut("ChangePassword")]
        //public async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
        //{
        //    var changePasswordModel = _mapper.Map<ChangeUserPasswordCommand>(command);
        //    changePasswordModel.UserId = User.GetUserId();
        //    var result = await _userFacade.ChangePassword(changePasswordModel);
        //    return CommandResult(result);
        //}

        //[HttpPut("Current")]
        //public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
        //{
        //    var commandModel = new EditUserCommand(User.GetUserId(), command.Avatar, command.Name, command.Family,
        //        command.PhoneNumber, command.Email, command.Gender);

        //    var result = await _userFacade.EditUser(commandModel);
        //    return CommandResult(result);
        //}

        [HttpPut]
        public async Task<ApiResult> Edit([FromForm] EditUserCommand command)
        {
            var result = await _userFacade.EditUser(command);
            return CommandResult(result);
        }
    }
}
