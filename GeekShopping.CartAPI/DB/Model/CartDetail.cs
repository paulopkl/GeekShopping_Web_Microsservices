using GeekShopping.CouponAPI.DB.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.DB.Model
{
    [Table("TCartDetail")]
    public class CartDetail : BaseEntity
    {
        /// CartHeaderId ForeignKey
        public long CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }

        /// ProductId ForeignKey
        public long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Column("count")]
        public int Count { get; set; }
    }
}
