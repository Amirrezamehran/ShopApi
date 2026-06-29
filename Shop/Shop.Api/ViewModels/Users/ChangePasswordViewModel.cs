using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Users
{
    // و چیزای دیگشو چک کردیم اینه که MinLength دلیل اینکه اینجاهم
    // نذاریم حتما بره سمت سرور چک بشه برگرده تا جایی که میشه تو لایه های بالا و نزدیک به ویو این چیزارو
    // چک میکنیم و بعد اگر از این لایه ها عبور کرد میره لایه اصلی چکش میکنه
    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string CurrentPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]      
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نیستند")]
        public string ConfirmPassword { get; set; }
    }
}
