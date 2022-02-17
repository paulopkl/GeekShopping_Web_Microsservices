using GeekShopping.CartAPI.DB.Model.Base;

namespace GeekShopping.CartAPI.DB.Model
{
    public class Cart : BaseEntity
    {
        public CartHeader CartHeader { get; set; }

        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
