using System.ComponentModel.DataAnnotations;

namespace AuthTest.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage ="Не указан Email")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Введите почту")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Не указано имя")]
    [DataType(DataType.Text)]
    [Display(Name = "Введите Имя")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Не указан логин")]
    [DataType(DataType.Text)]
    [Display(Name = "Введите Логин")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Введите пароль")]
    public string? Password { get; set; }


    [Compare("Password", ErrorMessage ="Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Повторите ввод пароля")]
    public string? ConfirmPassword { get; set; }
}
