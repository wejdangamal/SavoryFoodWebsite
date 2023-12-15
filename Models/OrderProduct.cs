using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_img.Models
{
    public class OrderProduct
    {
        [Key]
        public int CartId { get; set; }
        public string Date { get; set; }
        public double Price { get; set; }
        [ForeignKey("products")]
        public int productId { get; set; }
        public Product products { get; set; }
        [ForeignKey("customer")]
        public int customer_id { get; set; }
        public Customer customer { get; set; }
    }
}
