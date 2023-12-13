using System.ComponentModel.DataAnnotations;

namespace login_img.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
       ErrorMessage = "password must be Minimum eight characters, at least one letter and one number")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
    }
}
