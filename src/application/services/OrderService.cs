using domain.configs;
using domain.entitys;
using domain.repository;
using Microsoft.Extensions.Options;

namespace application.services
{
    public class OrderService : bases.BaseService1, IOrderService
    {
        public OrderService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }

        public string GetOrderNum(string orderName)
        {
            var users = this.First<AdminUsers>();
            return "";
        }
    }
}