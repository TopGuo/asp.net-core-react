using application.services.bases;
using domain.entitys;
using domain.repository;

namespace application.services
{
    public class AccountService : BaseService1, IAccountService
    {
        public AdminUsers GetAdminUser(int id)
        {
            var users = this.First<AdminUsers>();
            return users;
        }

        public AdminUsers GetAdminUsers()
        {
            var users = this.First<AdminUsers>();
            return users;
        }
    }
}