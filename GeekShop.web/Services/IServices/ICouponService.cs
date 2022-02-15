using GeekShop.web.Models;
using GeekShop.Web.Models;

namespace GeekShop.web.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
    }
}
