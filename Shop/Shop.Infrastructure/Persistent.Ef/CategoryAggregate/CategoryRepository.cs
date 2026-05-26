using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Infrastructure.Persistent.Ef._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAggregate
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext context) : base(context)
        {
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            var category = await Context.Categories
                            .Include(c => c.Childs)
                            .ThenInclude(c => c.Childs).FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                return false;

            // اینجا چک میکنیم اگر اون دسته بندی که میخوایم حذف کنیم یک محصول یا همان
            // داخلش دسته بندیش داشت نتونه اون دسته بندی رو حذف کنه product
            // و باید چک کنیم که چه داخل دسته بندی اصلی بود یا زیر دسته بندی ها بازم نتونه حذف کنه
            var isExistProduct = await Context.Products.AnyAsync(p => p.CategoryId == categoryId ||
                                    p.SubCategoryId == categoryId ||
                                    p.SecondarySubCategoryId == categoryId);

            if (isExistProduct)
                return false;

            // ما داخل خودش یک دسته بندی یا همان فرزند دیگری دارد Childs اینجا گفتیم چک کن ببین دسته بندی
            // و اگر داشت بیاد اون زیر دسته بندیشو حذف کن هرچندتا که بود
            if (category.Childs.Any(c => c.Childs.Any()))
            {
                Context.RemoveRange(category.Childs.SelectMany(c => c.Childs));
            }

            // رو حذف کن Childs و بعد بیا خود دسته بندی
            Context.RemoveRange(category.Childs);

            // و در آخر هم بیا و دسته بندی اصلی رو حذف کن
            Context.RemoveRange(category);

            return true;
        }
    }
}
