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
    }
}