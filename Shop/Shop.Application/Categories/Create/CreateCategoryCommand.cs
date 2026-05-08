using Common.Application;
using Shop.Domain.ProductAggregate.ValueObjects;


namespace Shop.Application.Categories.Create
{
    public record CreateCategoryCommand(string Title, string Slug, SeoData SeoData) : IBaseCommand;
}
