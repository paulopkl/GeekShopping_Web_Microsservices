using GeekShopping.OrderAPI.DB.Model;

namespace GeekShopping.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader header);

        Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);
    }
}
