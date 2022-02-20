using GeekShop.web.Services.IServices;
using GeekShop.web.Services.Utils;
using GeekShop.Web.Models;
using System.Net.Http.Headers;

namespace GeekShop.web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/cart";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/find-cart/{userId}");

            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<Object> AddItemToCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJson($"{BasePath}/add-cart", model);

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CartViewModel>();
            else
            {
                var badResultContent = await response.Content.ReadAsStringAsync();

                return badResultContent;
            }
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PutAsJson($"{BasePath}/update-cart", model);

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CartViewModel>();
            else throw new Exception("Something went wrong!");
        }

        public async Task<bool> RemoveFromCart(long cartId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{BasePath}/remove-cart/{cartId}");

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong!");
        }

        public async Task<bool> ApplyCoupon(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJson($"{BasePath}/apply-coupon", model);

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong!");
        }

        public async Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{BasePath}/remove-coupon/{userId}");

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong!");
        }

        public async Task<Object> Checkout(CartHeaderViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJson($"{BasePath}/checkout", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CartHeaderViewModel>();
            }
            else if (response.StatusCode.ToString().Equals("PreconditionFailed"))
            {
                return "Coupon Price has changed, please confirm!";
            }
            else throw new Exception("Something went wrong!");
        }
    }
}
