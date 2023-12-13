using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_img.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
         ErrorMessage = "password must be Minimum eight characters, at least one letter and one number")]
        public string password { get; set; }
        [NotMapped,Compare("password")]
        
        [Required(ErrorMessage = "*")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "*")]
        public string name { get; set; }
        [Required(ErrorMessage = "*")]
        public string location { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression("^01[0125][0-9]{8}")]
        public string phone { get; set; }
        public List<OrderProduct> orders { get; set; }
        

    }
}
