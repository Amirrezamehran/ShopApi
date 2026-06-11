using Common.Application;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.DTOs;

namespace Shop.Presentation.Facade.Products
{
    public interface IProductFacade
    {
        // Commands //
        Task<OperationResult> AddImageProduct(AddProductImageCommand command);
        Task<OperationResult> CreateProduct(CreateProductCommand command);
        Task<OperationResult> EditProduct(EditProductCommand command);
        Task<OperationResult> RemoveImageProduct(RemoveProductImageCommand command);


        // Queries //
        Task<ProductDto?> GetProductById(long id);
        Task<ProductDto?> GetProductBySlug(string slug);
        Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);
        Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams);

    }
}
