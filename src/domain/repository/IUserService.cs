using domain.entitys;
using domain.models;
using domain.models.dto;
using System.Collections.Generic;

namespace domain.repository
{
    public interface IUserService
    {
        //........΢���û�����.............

        //wxLogin  
        MyResult<object> WxLogin(WxLoginDto model);

        /// <summary>
        /// ����id��ȡ�û���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MyResult<object> GetUserById(int id);
        /// <summary>
        /// �û��б�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MyResult<object> GetUserList(UserModel model);

        /// <summary>
        /// ��ҳ�б�
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        MyResult<object> UpdateStatusUser(User model);

    }
}