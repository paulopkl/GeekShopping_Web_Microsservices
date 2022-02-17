using GeekShopping.OrderAPI.DB.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderAPI.DB.Model
{
    [Table("order_detail")]
    public class OrderDetail : BaseEntity
    {
        /// CartHeaderId ForeignKey
        public long OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        /// ProductId ForeignKey
        [Column("ProductId")]
        public long ProductId { get; set; }
                
        [Column("count")]
        public int Count { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
