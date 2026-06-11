using Common.Application;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.EditInventory;
using Shop.Query.Sellers.DTOs;

namespace Shop.Presentation.Facade.Sellers.Inventories
{
    public interface ISellerInventoryFacade
    {
        // Commands //
        Task<OperationResult> AddInventory(AddSellerInventoryCommand command);
        Task<OperationResult> EditInventory(EditSellerInventoryCommand command);


        // Queries //
        Task<InventoryDto?> GetInventoryById(long inventoryId);
        Task<List<InventoryDto?>> GetInventoryList(long sellerId);


    }
}
