using domain.entitys;

namespace domain.repository
{
    public interface IAccountService
    {
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         AdminUsers GetAdminUsers();
         /// <summary>
         /// 获取单一管理员信息
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
         AdminUsers GetAdminUser(int id);
    }
}