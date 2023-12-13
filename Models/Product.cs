using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_img.Models
{
    public class Product
    {
        [Key]
        public int product_ID { get; set; }
        public string product_name { get; set; }
        public double product_price { get; set; }
        public string product_image { get; set; }
        public string? description { get; set; }
        [ForeignKey("category")]
        public int category_ID { get; set; }
        public category category { get; set; }
        public List<OrderProduct> orderProducts { get; set; }

    }
}
