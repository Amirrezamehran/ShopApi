using Common.Query;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetByParentId
{
    // از سمت ویو مقدار به این قسمت ارسال میشه. یعنی آیدی رو از فرم میفرستیم ورودی این کلاس سازنده
    public record GetCategoryByParentIdQuery(long ParentId) : IQuery<List<ChildCategoryDto>>;

}
