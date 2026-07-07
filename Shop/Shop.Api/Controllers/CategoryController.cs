using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Domain.RoleAggregate.Enums;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;
using System.Net;

namespace Shop.Api.Controllers
{
    [PermissionChecker(Permission.Category_Management)]
    public class CategoryController : ApiController
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<CategoryDto>>> GetCategoriesList()
        {
            var result = await _categoryFacade.GetCategoriesList();
            return QueryResult(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ApiResult<CategoryDto>> GetCategoryById(long id)
        {
            var result = await _categoryFacade.GetCategoryById(id);
            return QueryResult(result);
        }
        
        [HttpGet("getChild/{parentId}")]
        public async Task<ApiResult<List<ChildCategoryDto>>> GetCategoryByParentId(long parentId)
        {
            var result = await _categoryFacade.GetCategoriesByParentId(parentId);
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ApiResult<long>> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _categoryFacade.CreateCategory(command);
            // GetCategoryById میاد و دامنه سایت روهم میچسبونه قبل از مسیر  Request.Scheme
            // کردیم با اون آیدی رو برمیگردونه Create یعنی راحت میتونیم کپیش کنیم و داخل مرورگر بزنیم و مقادیری که
            var url = Url.Action("GetCategoryById", "Category", new {id = result.Data}, Request.Scheme);
            return CommandResult(result, HttpStatusCode.Created, url);
        }

        [HttpPost("AddChild")]
        public async Task<ApiResult<long>> AddChildCategory(AddChildCategoryCommand command)
        {
            var result = await _categoryFacade.AddChildCategory(command);
            var url = Url.Action("GetCategoryById", "Category", new { id = result.Data }, Request.Scheme);
            return CommandResult(result, HttpStatusCode.Created, url);
        }

        [HttpPut]
        public async Task<ApiResult> EditCategory(EditCategoryCommand command)
        {
            var result = await _categoryFacade.EditCategory(command);
            return CommandResult(result);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ApiResult> RemoveCategory(long categoryId)
        {
            var result = await _categoryFacade.RemoveCategory(categoryId);
            return CommandResult(result);
        }
    }
}
