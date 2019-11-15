using System.Collections.Generic;
using domain.entitys;
using domain.models.dto;

namespace domain.repository
{
    public interface IAccountService
    {

        MyResult<object> Login(BackstageUserAdd model);

        MyResult<object> UpdateAccount(BackstageUserAdd model);
        /// <summary>
        /// 添加后台管理员
        /// </summary>
        /// <param name="model">AccountAdd</param>
        /// <returns></returns>
        MyResult<object> AddAccount(BackstageUserAdd model);

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MyResult<object> UpdatePwd(BackstageUserAdd model);

        /// <summary>
        /// 获取后台用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MyResult<object> GetBackstageUser(string id);
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MyResult<object> GetBackstageUserList(AccountSearchModel model);

        /// <summary>
        /// 获取后台用户登录Cookie信息
        /// </summary>
        /// <returns></returns>
        BackstageCookie GetUserCook();
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        MyResult<object> LogoutUser();
        //分页获取管理员列表
        MyResult<List<AdminUsers>> GetAdminUsers(int pageIndex, int pageSize);
        /// <summary>
        /// 用户以及权限信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        MyResult<object> GetUserAuth(string name, string password);
    }
}