using Common.Application;
using Shop.Domain.ProductAggregate.ValueObjects;

namespace Shop.Application.Categories.AddChild
{
    public record AddChildCategoryCommand(long ParentId, string Title, string Slug, SeoData SeoData) : IBaseCommand<long>;
}
