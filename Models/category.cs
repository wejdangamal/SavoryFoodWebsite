using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_img.Models
{
    public class category
    {
        [Key]
        public int category_ID { get; set; }
        public string category_name { get; set; }
        public List<Product> products { get; set; }
    }
}
