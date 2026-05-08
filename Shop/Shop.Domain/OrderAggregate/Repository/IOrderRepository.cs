using Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAggregate.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        // باید بعد از این متد کارشونو بکنن Order همه متدهای داخل کلاس
        // زیرا باید ابتدا سبدخرید جاری و فعال کاربر رو پیدا کنیم
        // بعد بیایم براش متدهای حذف و ادد و تغییر تعداد و... رو براش اعمال کنیم
        // زیرا ما فقط روی سبد خرید جاری که هنوز پرداخت نشده و فاینالی نشده فقط کار میکنیم
        Task<Order> GetCurrentUserOrder(long userId);
    }
}
