using Microsoft.AspNetCore.Http;
using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;

namespace Common.Application.SecurityUtil
{
    // این کلاس چک میکنه که ایا عکس اپلود شده واقعا یک عکسه یا چیز دیگری است
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile? file)
        {
            if (file == null) return false;
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