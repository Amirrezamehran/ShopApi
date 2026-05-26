using Common.Application;
using MediatR;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByParentId;
using Shop.Query.Categories.GetList;


namespace Shop.Presentation.Facade.Categories
{
    internal class CategoryFacade : ICategoryFacade
    {
        private readonly IMediator _mediator;

        public CategoryFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult<long>> AddChildCategory(AddChildCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<long>> CreateCategory(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditCategory(EditCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveCategory(long categoryId)
        {
            return await _mediator.Send(new RemoveCategoryCommand(categoryId));
        }

        public async Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId)
        {
            return await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
        }

        public async Task<CategoryDto> GetCategoryById(long id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(id));
        }

        public async Task<List<CategoryDto>> GetCategoriesList()
        {
            return await _mediator.Send(new GetCategoryListQuery());
        }
    }
}
