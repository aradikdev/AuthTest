using System.ComponentModel.DataAnnotations;

namespace AuthTest.ViewModels;

public class LoginViewModel
{

    [Required(ErrorMessage = "Не указан логин")]
    [DataType(DataType.Text)]
    [Display(Name = "Введите Логин")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Введите пароль")]
    public string? Password { get; set; }
}
