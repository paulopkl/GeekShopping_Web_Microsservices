using GeekShopping.CouponAPI.DB.Model.Base;

namespace GeekShopping.CouponAPI.DB.Model
{
    public class Cart : BaseEntity
    {
        public CartHeader CartHeader { get; set; }

        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
