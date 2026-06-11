using Common.Application;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Query.Sellers.DTOs;

namespace Shop.Presentation.Facade.Sellers
{
    public interface ISellerFacade
    {
        // Commands //
        Task<OperationResult> CreateSeller(CreateSellerCommand command);
        Task<OperationResult> EditSeller(EditSellerCommand command);


        // Queries //
        Task<SellerDto?> GetSellerById(long id);
        Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams);
        Task<SellerDto?> GetSellerByUserId(long userId);
    }
}
