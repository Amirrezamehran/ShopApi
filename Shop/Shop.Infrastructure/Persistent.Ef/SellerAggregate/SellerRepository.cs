using Dapper;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SellerAggregate
{
    internal class SellerRepository : BaseRepository<Seller>, ISellerRepository
    {
        private readonly DapperContext _dapperContext;

        public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
        {
            _dapperContext = dapperContext;
        }


        public async Task<InventoryResult?> GetInventoryById(long id)
        {
            using var connection = _dapperContext.CreateConnection();
            var sql = $"select * from {_dapperContext.Inventories} where Id = @InventoryId";

            return await connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new { InventoryId = id});
            
        }
    }
}
