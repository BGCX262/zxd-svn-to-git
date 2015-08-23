using System.ComponentModel.DataAnnotations;

namespace ZxdFramework.WebUI.Controllers.Models
{
    /// <summary>
    /// ��¼��֤ģ��
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Required")]
        [UIHint("password")]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

    }
}