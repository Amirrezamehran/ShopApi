using Common.Application;
using Shop.Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Categories.Edit
{
    public record EditCategoryCommand(long Id, string Title, string Slug, SeoData SeoData) : IBaseCommand;
}
