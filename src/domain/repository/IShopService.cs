using domain.entitys;
using domain.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.repository
{
   public interface IShopService
    {
        MyResult<object> GetShopList(ShopModel model);
        MyResult<object> UpdateStatus(Shop model);
    }
}
