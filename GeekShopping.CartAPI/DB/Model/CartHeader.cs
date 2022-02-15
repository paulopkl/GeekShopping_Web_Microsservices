using GeekShopping.CouponAPI.DB.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.DB.Model
{
    [Table("TCartHeader")]
    public class CartHeader : BaseEntity
    {
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("coupon_code")]
        public string CouponCode { get; set; }
    }
}
