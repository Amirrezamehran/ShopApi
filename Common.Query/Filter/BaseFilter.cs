
namespace Common.Query.Filter
{
    public class BaseFilter
    {
        // چندتا موجودیت کلا داریم که بعد بیام براش مشخص کنیم این چندتا رو چجوری تقسیم کن و تو هر صفحه چندتاشو نشون بده
        public int EntityCount { get; private set; }
        public int CurrentPage { get; private set; } // شماره پیجی که روش هستیم درحال حاظر رو میگه
        public int PageCount { get; private set; } // تعداد کل صفحات رو میگه مثلا میگه 200 صفحه داریم
        // شروع پیجی که میبینیم
        // مثلا ما روی پیج 10 هستیم و تا پیج 5 و پیج 15 برای ما قابل نمایش هست
        // یعنی اجازه میده ما مستقیم روی پیج 5 کلیک کنیم یا روی پیج 15 کلیک کنیم
        // میشه همون پیج 15 ما EndPage میشه همون پیج 5 ما و StartPage پس
        // رو ببینیم Web Api برای درک بهتر میتونیم ویدیو شماره 78 از درس
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int Take { get; private set; } // چندتا تو هر صفحه نمایش بده

        public void GeneratePaging(IQueryable<Object> data, int take, int currentPage)
        {
            var entityCount = data.Count();
            var pageCount = (int)Math.Ceiling(entityCount / (double)take);
            PageCount = pageCount;
            CurrentPage = currentPage;
            EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
            EntityCount = entityCount;
            Take = take;
            StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
        }

        public void GeneratePaging(int Count, int take, int currentPage)
        {
            var entityCount = Count;
            var pageCount = (int)Math.Ceiling(entityCount / (double)take);
            PageCount = pageCount;
            CurrentPage = currentPage;
            EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
            EntityCount = entityCount;
            Take = take;
            StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
        }
    }


    public class BaseFilterParam
    {
        public int PageId { get; set; } = 1;
        public int Take { get; set; } = 10;
    }

    // محدودش کردیم که هر کلاسی رو نتونه پاس بده where مخصوصا با
    public class BaseFilter<TData, TParam> : BaseFilter
        where TData : BaseDto where TParam :BaseFilterParam
    {
        public List<TData> Data { get; set; }
        public TParam FilterParams { get; set; }

    }

}