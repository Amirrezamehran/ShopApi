using Common.Query;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetList
{
    public record GetCategoryListQuery : IQuery<List<CategoryDto>>;

    // مشخص کردیم IQueryHandler رو با Handle ورودی و خروجی متد



    
   


}
