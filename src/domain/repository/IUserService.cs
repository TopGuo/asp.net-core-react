using domain.entitys;
using domain.models;
using domain.models.dto;
using System.Collections.Generic;

namespace domain.repository
{
    public interface IUserService
    {
        //........微信用户管理.............

        //wxLogin  
        MyResult<object> WxLogin(WxLoginDto model);

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MyResult<object> GetUserById(int id);
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MyResult<object> GetUserList(UserModel model);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        MyResult<object> UpdateStatusUser(User model);

    }
}