using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;

namespace Common.Application.Validation.CustomValidation.IFormFile
{

    // است Data Annotation این کلاس برای اعتبارسنجی کردن با استفاده از
    // که میاد محتواشو چک میکنه ببینه عکسه یا نه FileImage میذاریم به نام Attribute که میایم بالای پراپرتی مورد نظرمون یک
    // باشه تا این دیتا انوتیشن عمل کنه و چکش کنه IFormFile البته اون پراپرتی حتما باید از نوع
    // رو انجام میدیم IFormFile چون در زیر هم داریم چک کردن محتوای یک
    public class FileImageAttribute : ValidationAttribute, IClientModelValidator
    {
        public override bool IsValid(object? value)
        {
            var fileInput = value as Microsoft.AspNetCore.Http.IFormFile;
            if (fileInput == null)
                return true;

            return fileInput.IsImage();
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
                context.Attributes.Add("data-val", "true");
            context.Attributes.Add("accept", "image/*");
            context.Attributes.Add("data-val-fileImage", ErrorMessage);
        }
    }
    static class Validation
    {
        public static bool IsImage(this Microsoft.AspNetCore.Http.IFormFile file)
        {
            try
            {
                var img = Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}