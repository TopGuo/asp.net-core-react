using domain.entitys;
using domain.repository;

namespace application.services
{
    public class OrderService : bases.BaseService1, IOrderService
    {
        public string GetOrderNum(string orderName)
        {
            var users = this.First<AdminUsers>();
            return "";
        }
    }
}