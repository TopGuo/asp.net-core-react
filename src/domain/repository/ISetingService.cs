using domain.models.dto;

namespace domain.repository
{
    public interface ISetingService
    {
        //获取公告
        MyResult<object> GetAnnounces(AnnounceDto model);
        //添加公告
        MyResult<object> AddAnnounce(AnnounceDto model);
        //删除公告
        MyResult<object> DelAnnounce(int id);
        //修改公告
        MyResult<object> UpdateAnnounce(AnnounceDto model);
        //获取一条公告
        MyResult<object> GetOneAnnounce(int id);
        //获取公告标题
        MyResult<object> GetAnnounceTitle();


        //添加banner
        MyResult<object> AddBanner(BannerDto model);
        //获取banner
        MyResult<object> GetBanner(BannerDto model);
        //删除banner
        MyResult<object> DelBanner(int? id);


        //获取信息市场列表
        MyResult<object> GetMessage(MessageDto model);
        MyResult<object> GetOneMessage(MessageDto model);
        //删除信息
        MyResult<object> DelMessage(MessageDto model);
        //添加信息
        MyResult<object> AddMessage(MessageDto model);
        //修改信息
        MyResult<object> UpdateMessage(MessageDto model);

        //添加消息类别
        MyResult<object> AddMessageType(MessageTypeDto model);
        //获取消息类别
        MyResult<object> GetMessageType(MessageTypeDto model);
        //修改消息类别
        MyResult<object> DelMessageType(MessageTypeDto model);
        MyResult<object> UpdateMessageType(MessageTypeDto model);

        //添加景点
        MyResult<object> AddScenic(ScenicDto model);
        MyResult<object> DelScenic(ScenicDto model);
        //获取景点
        MyResult<object> GetScenic(ScenicDto model);
        //修改景点
        MyResult<object> UpdateScenic(ScenicDto model);
        //获取景点详情
        MyResult<object> GetOneScenic(ScenicDto model);

        //添加店铺
        MyResult<object> AddShop(ShopDto model);
        //获取店铺主要信息
        MyResult<object> GetShops(ShopDto model);
        //获取店铺详情
        MyResult<object> GetOneShop(ShopDto model);
        //修改店铺信息
        MyResult<object> UpdateShop(ShopDto model);
        //删除店铺
        MyResult<object> DelShop(ShopDto model);

        //添加店铺活动
        MyResult<object> AddShopDetail(ShopDetailDto model);
        //删除活动
        MyResult<object> DelShopDetail(ShopDetailDto model);
        //修改活动
        MyResult<object> UpdateShopDetail(ShopDetailDto model);

    }
}