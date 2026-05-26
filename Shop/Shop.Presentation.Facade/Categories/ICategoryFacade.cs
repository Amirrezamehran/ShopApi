using Common.Application;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Query.Categories.DTOs;

namespace Shop.Presentation.Facade.Categories
{
    public interface ICategoryFacade
    {

        // Commands //
        Task<OperationResult<long>> AddChildCategory(AddChildCategoryCommand command);
        Task<OperationResult<long>> CreateCategory(CreateCategoryCommand command);
        Task<OperationResult> EditCategory(EditCategoryCommand command);
        Task<OperationResult> RemoveCategory(long categoryId);


        // Queries //
        Task<CategoryDto> GetCategoryById(long id);
        Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId);
        Task<List<CategoryDto>> GetCategoriesList();
    }
}
