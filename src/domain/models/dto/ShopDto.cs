using System;

namespace domain.models.dto
{
    public class ShopDto:BaseModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LogoPic { get; set; }
        public int LookCount { get; set; }
        public int Types { get; set; }
        public int ShopType { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int OpenTime { get; set; }
        public int CloseTime { get; set; }
        public string PhoneNum { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Pic { get; set; }
        public string NickName { get; set; }
    }
}