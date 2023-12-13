using login_img.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_img.LoginVM
{
    public class logV
    {
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
        ErrorMessage = "password must be Minimum eight characters, at least one letter and one number")]
        public string password { get; set; }
        public string Message { get; set; } = "good";
        public Admin admin { get; set; }
    }
}