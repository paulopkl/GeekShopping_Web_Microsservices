using GeekShop.web.Models;
using GeekShop.web.Services.IServices;
using GeekShop.web.Services.Utils;
using GeekShop.Web.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShop.web.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/coupon";

        public CouponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponViewModel> GetCoupon(string code, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/{code}");

            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK) return new CouponViewModel();

            // Convert from JSON to Object Entity
            return JsonSerializer.Deserialize<CouponViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
