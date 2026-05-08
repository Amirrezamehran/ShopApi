using Ganss.Xss;

namespace Common.Application.SecurityUtil
{

    // کار بکنه ckeditor این برای وقتیه که میخوایم کامنت بگیریم از کاربر یا قراره کاربر با
    // میاد و اگر اسکریپتی کاربر توی متنش باشه رو تشخیص میده و خودکار حذفش میکنه اون قسمت رو
    // و خیلی مهمه که حتما اینو استفاده کنیم در قسمتایی که ورودی های متنی میگریم از کاربر
    public static class XssSecurity
    {
        public static string SanitizeText(this string text)
        {
            var htmlSanitizer = new HtmlSanitizer();

            htmlSanitizer.KeepChildNodes = true;

            htmlSanitizer.AllowDataAttributes = true;

            return htmlSanitizer.Sanitize(text);
        }
    }

}